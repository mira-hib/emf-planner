import type { PlannerProject } from "../types";
import { rad, toGeo } from "./geometry";
// Advancing-front triangulation reconstructed from Triangul.CalcTriangulate.
// Point order and the original sentinel are significant for cocircular samples.
export function originalTerrainTriangles(project: PlannerProject): Uint32Array {
  const points = project.terrain_points.map((p) => {
    if (p.wm_x !== undefined && p.wm_y !== undefined) return [p.wm_x, p.wm_y];
    const [lat, lng] = toGeo(p.x, p.y, project.origin);
    return [
      6378137 * rad(lng),
      6378137 * Math.log(Math.tan(Math.PI / 4 + rad(lat) / 2)),
    ];
  });
  const n = points.length;
  points.push([-50, -50], [-40, -50]);
  const edges: [number, number][] = [[n, n + 1]],
    triangles: number[][] = [];
  const same = (a: number, b: number) =>
    points[a][0] === points[b][0] && points[a][1] === points[b][1];
  const add = (a: number, b: number) => {
    const i = edges.findIndex(
      (e) =>
        (same(e[0], a) && same(e[1], b)) || (same(e[1], a) && same(e[0], b)),
    );
    if (i >= 0) edges.splice(i, 1);
    else edges.push([a, b]);
  };
  let flag = false,
    iterations = 0;
  while (edges.length && iterations++ < n * 10 + 100) {
    const [ai, bi] = edges.pop()!,
      a = points[ai],
      b = points[bi];
    let best = 0,
      index = -1;
    for (let i = 0; i <= n; i++) {
      const p = points[i];
      if ((p[0] - a[0]) * (b[1] - a[1]) - (p[1] - a[1]) * (b[0] - a[0]) >= 0)
        continue;
      const m1 = a[0] === p[0] ? 9999999999999 : (a[1] - p[1]) / (a[0] - p[0]),
        m2 = b[0] === p[0] ? 9999999999999 : (b[1] - p[1]) / (b[0] - p[0]);
      let angle = Math.atan2(m2 - m1, 1 + m1 * m2);
      if (angle < 0) angle += Math.PI;
      angle = Math.abs(angle);
      if (angle > best) {
        best = angle;
        index = i;
      }
    }
    if (index < 0) continue;
    if (flag && edges.length === 0) break;
    flag = true;
    add(ai, index);
    add(index, bi);
    triangles.push([ai, bi, index]);
    if (triangles.length - 1 > 2.5 * (n - 1)) break;
  }
  return new Uint32Array(triangles.filter((t) => t.every((i) => i < n)).flat());
}
