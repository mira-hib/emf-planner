/** ROB-05 calculation workload. Resumes only receipts from the identical input hash.
 * UI responsiveness, Task Manager and visual output are separate scenario steps. */
import { readFileSync, writeFileSync, mkdirSync, existsSync } from "node:fs";
import { join, resolve } from "node:path";
import { spawn } from "node:child_process";
import { createHash } from "node:crypto";
import { performance } from "node:perf_hooks";
import { setPriority, constants } from "node:os";
import { newProject, normalizeProject } from "../src/lib/project.ts";
import { pasteSourceCells } from "../src/lib/source-clipboard.ts";
import { horizontalGrid, contours } from "../src/lib/engine.ts";
import { parseMsi } from "../src/lib/antenna.ts";
import { toLocal } from "../src/lib/geometry.ts";
import type { PlannerProject, Building } from "../src/types";

const output = resolve(process.argv[2] ?? "test validation/2026-10-25/stress-rob05");
mkdirSync(output, { recursive: true });
const projectPath = join(output, "stress-project.emfp");
if (process.argv[3] === "--cut") {
  try { setPriority(0, constants.priority.PRIORITY_BELOW_NORMAL); } catch { /* Workload stays measurable without priority privileges. */ }
  const index = Number(process.argv[4]);
  const bytes = readFileSync(projectPath), project = normalizeProject(JSON.parse(bytes.toString()));
  const input_sha256 = createHash("sha256").update(bytes).digest("hex");
  const start = performance.now(), grid = horizontalGrid(project, project.horizontal_cuts[index].height_m, 1, 1000);
  const checksum = createHash("sha256");
  for (const values of [grid.values, grid.fields]) {
    if (!values.every(Number.isFinite)) throw new Error("Non-finite stress grid");
    const data = values instanceof Float64Array ? values : Float64Array.from(values);
    checksum.update(Buffer.from(data.buffer, data.byteOffset, data.byteLength));
  }
  const lines = contours(grid, 1, "red");
  const receipt = { index, height_m: grid.height_m, status: "passed", input_sha256,
    width: grid.width, height: grid.height, grid_sha256: checksum.digest("hex"),
    contour_segments: lines.segments.length, seconds: (performance.now() - start) / 1000,
    max_rss_bytes: process.resourceUsage().maxRSS * 1024 };
  const target = join(output, `cut-${String(index).padStart(2, "0")}.json`);
  writeFileSync(target, JSON.stringify(receipt, null, 2));
  console.log(JSON.stringify(receipt));
} else {
  if (!existsSync(projectPath)) {
    const project = newProject(); project.name = "ROB-05 full workload";
    project.origin = project.map_center = [45.485337, 9.198818];
    project.sources = pasteSourceCells([], project.origin,
      readFileSync("test/emf_test_kit/data/sources/sources_stress_1000.tsv", "utf8"), undefined, "name");
    // The kit's stress sheet says "generic" without embedding diagrams. Use the
    // kit's explicit sector fixture; retain this assumption with the result.
    const pattern = parseMsi(readFileSync("test/emf_test_kit/data/msi/ok_sector65x7_17dBi.msi", "utf8"));
    project.sources = project.sources.map((source, i) => ({ ...source, id: `stress-source-${i}`,
      antenna_model: "sector65x7 (stress generic)", pattern_h: pattern.horizontal, pattern_v: pattern.vertical }));
    const geometries = JSON.parse(readFileSync(".tools/kit-stress-buildings.json", "utf8")) as { name: string; height: number; geo: [number, number][] }[];
    project.buildings = geometries.map((building, i) => ({ id: `stress-building-${i}`, name: building.name,
      height_m: building.height, foundation_m: 0, active: true, visible: true, attenuation_db: 3,
      points: building.geo.map(([lng,lat]) => toLocal(lat,lng,project.origin)) } satisfies Building));
    const points = project.buildings.flatMap(building => building.points);
    project.settings.calculation_area_m = { x_min: Math.floor(Math.min(...points.map(p=>p[0])))-10,
      x_max: Math.ceil(Math.max(...points.map(p=>p[0])))+10,
      y_min: Math.floor(Math.min(...points.map(p=>p[1])))-10,
      y_max: Math.ceil(Math.max(...points.map(p=>p[1])))+10 };
    project.settings.horizontal_grid_divisions = [1000,1000];
    project.settings.buildings_mode = "screen"; project.settings.consider_terrain = false;
    project.settings.selected_limit = 0; project.limits.forEach((limit,i)=>{limit.active=i===0;});
    project.horizontal_cuts = Array.from({length:50},(_,i)=>({id:`stress-height-${i}`,height_m:i,active:true,color:"red"}));
    writeFileSync(projectPath, JSON.stringify(project));
  }
  const bytes = readFileSync(projectPath), project = JSON.parse(bytes.toString()) as PlannerProject;
  if (project.sources.length!==1000 || project.buildings.length!==2000 || project.horizontal_cuts.length!==50)
    throw new Error("Incomplete ROB-05 fixture");
  const input_sha256 = createHash("sha256").update(bytes).digest("hex");
  const start = performance.now(), failures: unknown[] = [];
  const completed: Record<string, unknown>[] = [];
  const queue: number[] = [];
  for(let i=0;i<50;i++){
    const path=join(output,`cut-${String(i).padStart(2,"0")}.json`);
    const previous=existsSync(path)?JSON.parse(readFileSync(path,"utf8")):undefined;
    if(previous?.status==="passed"&&previous.input_sha256===input_sha256)completed.push(previous);else queue.push(i);
  }
  const persist=()=>writeFileSync(join(output,"result.json"),JSON.stringify({scenario:"ROB-05",status:failures.length?"failed":completed.length===50?"passed-calculation-workload":"running",
    input_sha256,sources:1000,buildings:2000,cuts:50,completed_cuts:completed.length,divisions:1000,workers:8,
    elapsed_this_session_seconds:(performance.now()-start)/1000,assumptions:"Heights 0–49m; bounding area includes all kit buildings; generic antenna model uses kit sector65x7 pattern with each source's own gain; first limit only.",
    scope:"Calculation workload and per-process peak RSS. UI responsiveness, export, 3D and Windows Task Manager are separate checks.",
    failures,receipts:completed.sort((a,b)=>Number(a.index)-Number(b.index))},null,2));
  persist();
  async function worker(){while(queue.length){const index=queue.shift()!;
    await new Promise<void>(done=>{const child=spawn(process.execPath,["--import","tsx",resolve("scripts/validate-kit-stress.mts"),output,"--cut",String(index)],{windowsHide:true,stdio:["ignore","pipe","pipe"]});
      let log="";child.stdout.on("data",chunk=>{log+=chunk;});child.stderr.on("data",chunk=>{log+=chunk;});
      child.on("error",error=>{failures.push({index,error:String(error)});persist();done();});
      child.on("exit",code=>{writeFileSync(join(output,`cut-${index}.log`),log);if(code===0)completed.push(JSON.parse(readFileSync(join(output,`cut-${String(index).padStart(2,"0")}.json`),"utf8")));else failures.push({index,exit_code:code});persist();console.log(`ROB-05 ${completed.length}/50, failures ${failures.length}`);done();});
    });
  }}
  await Promise.all(Array.from({length:Math.min(8,queue.length)},worker));persist();
  if(failures.length)process.exitCode=1;
}
