import type { PlannerProject, TerrainPoint } from "../types";
import Delaunator from "delaunator";
import { originalTerrainTriangles } from "./original-terrain";
const R = 6378137;
export const rad = (v: number) => (v * Math.PI) / 180;
export const deg = (v: number) => (v * 180) / Math.PI;
export const wrap = (v: number) => ((v % 360) + 360) % 360;
export function toLocal(
  lat: number,
  lng: number,
  origin: [number, number],
): [number, number] {
  const scale = Math.cos(rad(origin[0]));
  return [
    R * rad(lng - origin[1]) * scale,
    R *
      (Math.log(Math.tan(Math.PI / 4 + rad(lat) / 2)) -
        Math.log(Math.tan(Math.PI / 4 + rad(origin[0]) / 2))) *
      scale,
  ];
}
export function toGeo(
  x: number,
  y: number,
  origin: [number, number],
): [number, number] {
  const scale = Math.cos(rad(origin[0]));
  return [
    deg(
      2 *
        Math.atan(
          Math.exp(
            y / (R * scale) +
              Math.log(Math.tan(Math.PI / 4 + rad(origin[0]) / 2)),
          ),
        ),
    ) - 90,
    origin[1] + deg(x / (R * scale)),
  ];
}
export function inside(
  x: number,
  y: number,
  points: [number, number][],
): boolean {
  let result = false;
  for (let i = 0, j = points.length - 1; i < points.length; j = i++) {
    const a = points[i],
      b = points[j];
    if (
      a[1] > y !== b[1] > y &&
      x < ((b[0] - a[0]) * (y - a[1])) / (b[1] - a[1]) + a[0]
    )
      result = !result;
  }
  return result;
}
const terrainCache = new WeakMap<
  PlannerProject,
  { points: TerrainPoint[]; triangles: Uint32Array; memo: Map<string, number> }
>();
export function terrainMesh(project: PlannerProject) {
  let cache = terrainCache.get(project);
  if (!cache) {
    const rawPoints = [
      ...project.terrain_points,
      ...(project.terrain_contours ?? []).flatMap((c) =>
        c.points.map(([x, y], i) => ({
          id: `${c.id}:${i}`,
          x,
          y,
          elevation: c.elevation,
        })),
      ),
    ];
    const seen=new Set<string>();
    const points=rawPoints.filter(p=>{
      if(![p.x,p.y,p.elevation].every(Number.isFinite))return false;
      const key=`${p.x},${p.y}`;if(seen.has(key))return false;seen.add(key);return true;
    });
    cache = {
      points,
      triangles:points.length<3?new Uint32Array():
        project.settings.terrain_interpolation === "original"
          ? originalTerrainTriangles({ ...project, terrain_points: points })
          : Delaunator.from(
              points,
              (p) => p.x,
              (p) => p.y,
            ).triangles,
      memo: new Map(),
    };
    cache.triangles=new Uint32Array(Array.from({length:cache.triangles.length/3},(_,i)=>Array.from(cache!.triangles.slice(i*3,i*3+3))).filter(([ai,bi,ci])=>{
      const a=points[ai],b=points[bi],c=points[ci];return a&&b&&c&&Math.abs((b.x-a.x)*(c.y-a.y)-(b.y-a.y)*(c.x-a.x))>1e-8;
    }).flat());
    terrainCache.set(project, cache);
  }
  return cache;
}
export function terrainDiagnostics(project:PlannerProject):string[]{
  const {points,triangles}=terrainMesh(project);
  if(!points.length)return [];
  const messages:string[]=[];
  if(points.length<3)messages.push("Triangulation impossible : au moins trois points distincts sont nécessaires. Les altitudes ponctuelles restent consultables.");
  else if(!triangles.length)messages.push("Triangulation impossible : les points sont alignés ou confondus.");
  if(project.terrain_contours?.some(c=>c.points.length<2))messages.push("Une courbe de niveau doit contenir au moins deux sommets.");
  return messages;
}
export function terrainHeight(
  project: PlannerProject,
  x: number,
  y: number,
): number {
  if (
    !project.settings.consider_terrain ||
    (!project.terrain_points.length &&
      !project.terrain_contours?.some((c) => c.points.length))
  )
    return 0;
  const cache = terrainMesh(project);
  const key = `${x},${y}`,
    existing = cache.memo.get(key);
  if (existing !== undefined) return existing;
  for (let i = 0; i < cache.triangles.length; i += 3) {
    const a = cache.points[cache.triangles[i]],
      b = cache.points[cache.triangles[i + 1]],
      c = cache.points[cache.triangles[i + 2]],
      det = (b.y - c.y) * (a.x - c.x) + (c.x - b.x) * (a.y - c.y);
    if (Math.abs(det) < 1e-10) continue;
    const u = ((b.y - c.y) * (x - c.x) + (c.x - b.x) * (y - c.y)) / det,
      v = ((c.y - a.y) * (x - c.x) + (a.x - c.x) * (y - c.y)) / det;
    if (u >= -1e-8 && v >= -1e-8 && u + v <= 1 + 1e-8) {
      const h = u * a.elevation + v * b.elevation + (1 - u - v) * c.elevation;
      cache.memo.set(key, h);
      return h;
    }
  }
  const nearest = cache.points
    .map((p) => ({ ...p, d: Math.hypot(x - p.x, y - p.y) }))
    .sort((a, b) => a.d - b.d)
    .slice(0, 3);
  if (nearest[0].d < 1e-6) return nearest[0].elevation;
  if (nearest.length === 3) {
    const [a, b, c] = nearest,
      det = (b.y - c.y) * (a.x - c.x) + (c.x - b.x) * (a.y - c.y);
    if (Math.abs(det) > 1e-8) {
      const u = ((b.y - c.y) * (x - c.x) + (c.x - b.x) * (y - c.y)) / det,
        v = ((c.y - a.y) * (x - c.x) + (a.x - c.x) * (y - c.y)) / det;
      if (u >= 0 && v >= 0 && u + v <= 1)
        return u * a.elevation + v * b.elevation + (1 - u - v) * c.elevation;
    }
  }
  const sum = nearest.reduce((s, p) => s + 1 / (p.d * p.d), 0);
  return nearest.reduce((s, p) => s + p.elevation / (p.d * p.d), 0) / sum;
}
export function segmentCrosses(
  a: [number, number],
  b: [number, number],
  c: [number, number],
  d: [number, number],
): number | null {
  const dx = b[0] - a[0],
    dy = b[1] - a[1],
    ex = d[0] - c[0],
    ey = d[1] - c[1],
    den = dx * ey - dy * ex;
  if (Math.abs(den) < 1e-10) return null;
  const t = ((c[0] - a[0]) * ey - (c[1] - a[1]) * ex) / den,
    u = ((c[0] - a[0]) * dy - (c[1] - a[1]) * dx) / den;
  return t > 1e-6 && t < 1 - 1e-6 && u >= 0 && u <= 1 ? t : null;
}
