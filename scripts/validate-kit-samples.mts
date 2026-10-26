import { readdirSync, readFileSync, writeFileSync, mkdirSync } from "node:fs";
import { join, resolve } from "node:path";
import { performance } from "node:perf_hooks";
import { readProject } from "../src/lib/files.ts";
import { calculatePoints, horizontalGrid, verticalGrid, contours } from "../src/lib/engine.ts";
import type { FieldGrid } from "../src/types";
import { createHash } from "node:crypto";
const input=process.argv[2]??"extracted/scenario-tests/samples/Project Samples";
const output=resolve(process.argv[3]??"test validation/2026-10-25/native-samples");mkdirSync(output,{recursive:true});
const results=[];
function gridReceipt(grid: FieldGrid) {
  let min = Infinity, max = -Infinity;
  const hash = createHash("sha256");
  for (const values of [grid.values, grid.fields]) {
    for (const value of values) {
      if (!Number.isFinite(value)) throw new Error("Non-finite grid value");
      min = Math.min(min, value); max = Math.max(max, value);
    }
    const floats = values instanceof Float64Array ? values : Float64Array.from(values);
    hash.update(Buffer.from(floats.buffer, floats.byteOffset, floats.byteLength));
  }
  return { width: grid.width, height: grid.height, min, max, values_sha256: hash.digest("hex"),
    contour_segments: contours(grid, 1, "red").segments.length, rss_bytes: process.memoryUsage().rss };
}
for(const name of readdirSync(input).filter(name=>name.endsWith(".xemf"))){
  const started=performance.now();
  try{const bytes=readFileSync(join(input,name)),project=readProject(name,bytes),load_seconds=(performance.now()-started)/1000;
    const calculation=performance.now(),points=calculatePoints(project),nonfinite=points.filter(point=>![point.total_field,point.exposure_ratio,point.total_power_density].every(Number.isFinite));
    if(nonfinite.length)throw new Error(`${nonfinite.length} non-finite point results`);
    const values=points.map(point=>({name:point.name,height_m:point.height_m,field:point.total_field,exposure_ratio:point.exposure_ratio}));
    writeFileSync(join(output,name+".points.json"),JSON.stringify(values,null,2));
    const point_calculation_seconds=(performance.now()-calculation)/1000;
    const cuts: Record<string,unknown>[]=[];
    const row={name,status:"running",sources:project.sources.length,buildings:project.buildings.length,terrain_points:project.terrain_points.length,points:project.control_points.length,point_heights:points.length,
      points_status:points.length?"passed":"not-applicable-no-points",load_seconds,point_calculation_seconds,cuts,
      scope:"Native import, existing point heights and first active horizontal/vertical cut at the imported first-matrix resolution. Other cuts, UI, exports and original numeric comparison are separate checks."};
    results.push(row);
    writeFileSync(join(output,"results.json"),JSON.stringify(results,null,2));
    const divisions=project.settings.horizontal_grid_divisions?.[0]??600;
    const h=project.horizontal_cuts.find(c=>c.active),v=project.vertical_cuts.find(c=>c.active);
    const jobs:[string,()=>FieldGrid][]=[];
    if(h)jobs.push(["horizontal",()=>horizontalGrid(project,h.height_m,1,divisions)]);
    if(v)jobs.push(["vertical",()=>verticalGrid(project,v,1,divisions)]);
    for(const[kind,compute]of jobs){const started=performance.now(),receipt=gridReceipt(compute());
      cuts.push({kind,divisions,status:"passed",seconds:(performance.now()-started)/1000,...receipt});
      writeFileSync(join(output,"results.json"),JSON.stringify(results,null,2));
      console.log(JSON.stringify({name,kind,...receipt}));
    }
    row.status="passed";
  }catch(error){results.push({name,status:"failed",error:String(error)});}
}
writeFileSync(join(output,"results.json"),JSON.stringify(results,null,2));console.log(JSON.stringify(results,null,2));if(results.some(result=>result.status!=="passed"))process.exitCode=1;
