import { mkdirSync, readFileSync, writeFileSync } from "node:fs";
import { resolve, join } from "node:path";
import { spawnSync } from "node:child_process";
import * as THREE from "three";
import { sceneMeshes, meshesIdtf, embedU3dPdf } from "../src/lib/interactive-pdf.ts";
import { newProject, newSource } from "../src/lib/project.ts";
import { projectDxf } from "../src/lib/files.ts";

const directory = resolve(process.argv[2] ?? "test validation/2026-10-25/native-exports");
mkdirSync(directory, { recursive: true });
const tools = resolve("src-tauri/native"), ext = process.platform === "win32" ? ".exe" : "";
const receipts: Record<string, unknown>[] = [];
function run(name: string, args: string[]) {
  const result = spawnSync(join(tools, name + ext), args, { cwd: tools, encoding: "utf8", timeout: 120000, windowsHide: true });
  writeFileSync(join(directory, name + ".log"), `${result.stdout ?? ""}\n${result.stderr ?? ""}`);
  if (result.error || result.status !== 0) throw new Error(`${name}: ${result.error ?? result.status}. See retained log.`);
  receipts.push({ name, exit_code: result.status });
}
const scene = new THREE.Group();
const box = new THREE.Mesh(new THREE.BoxGeometry(10, 15, 20), new THREE.MeshStandardMaterial({ color: "red" }));
box.position.set(5, 7.5, 0); scene.add(box);
const meshes = sceneMeshes(scene), idtf = join(directory, "scene.idtf"), u3d = join(directory, "scene.u3d");
writeFileSync(idtf, meshesIdtf(meshes));
run("IDTFConverter", ["-input", idtf, "-output", u3d]);
const bytes = readFileSync(u3d);
if (bytes.subarray(0, 4).toString("binary") !== "U3D\0") throw new Error("Invalid U3D header");
writeFileSync(join(directory, "scene-interactive.pdf"), embedU3dPdf(bytes, meshes));
const project = newProject(); project.sources = [newSource(project.origin, "Source validation")];
project.buildings = [{ id: "b", name: "Bâtiment", active: true, height_m: 10, foundation_m: 0, attenuation_db: 3, points: [[0, 0], [100, 0], [100, 50], [0, 50]] }];
const dxf = join(directory, "drawing.dxf"), dwg = join(directory, "drawing.dwg"), returned = join(directory, "drawing-roundtrip.dxf");
writeFileSync(dxf, projectDxf(project, [], { paper: { scale: 1000, paper: "A3" } }));
run("dxf2dwg", ["-y", "--as", "r2000", "-o", dwg, dxf]);
if (readFileSync(dwg).subarray(0, 6).toString() !== "AC1015") throw new Error("Invalid DWG R2000 signature");
run("dwg2dxf", ["-y", "-o", returned, dwg]);
const roundtrip = readFileSync(returned, "utf8");
if (!roundtrip.includes("Buildings")) throw new Error("DWG round trip lost drawing layers");
const decodedPath = join(directory, "drawing-libredwg-readback.json");
run("dwgread", ["-O", "JSON", "-o", decodedPath, dwg]);
const decoded = JSON.parse(readFileSync(decodedPath, "utf8"));
const objects = decoded.OBJECTS as { entity?: string; points?: number[][] }[];
const rectangle = objects?.find(object => {
  if (object.entity !== "LWPOLYLINE" || object.points?.length !== 4) return false;
  const xs = object.points.map(point => point[0]), ys = object.points.map(point => point[1]);
  return Math.abs(Math.max(...xs) - Math.min(...xs) - 100) < 1e-6 &&
    Math.abs(Math.max(...ys) - Math.min(...ys) - 50) < 1e-6;
});
if (!rectangle) throw new Error("DWG read-back lost the 100 × 50 mm building polygon at 1:1000");
writeFileSync(join(directory, "result.json"), JSON.stringify({ receipts, u3d_bytes: bytes.length,
  building_paper_dimensions_mm: [100, 50],
  scope: "Real encoders and LibreDWG read-back with geometry assertions. Interactive PDF viewer and third-party CAD visual checks remain separate." }, null, 2));
console.log(`Native format encoding PASS: ${directory}`);
