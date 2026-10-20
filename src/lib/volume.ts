import type { ExposureVolume, PlannerProject, VolumeOptions } from "../types";
import { exposureAt } from "./engine";
import { inside, terrainHeight, toLocal } from "./geometry";
type Vec = [number, number, number];
function antennaPositions(p: PlannerProject): Vec[] {
  return p.sources
    .filter((s) => s.active !== false)
    .map((s) => {
      const xy = toLocal(s.lat, s.lng, p.origin),
        x = s.x_m ?? xy[0],
        y = s.y_m ?? xy[1];
      const b =
        s.location === "Roof" || s.location === "Facade"
          ? p.buildings.find((b) => inside(x, y, b.points))
          : undefined;
      return [
        x,
        y,
        s.height_m + (b ? b.foundation_m : terrainHeight(p, ...xy)),
      ];
    });
}
export function defaultVolumeOptions(p: PlannerProject): VolumeOptions {
  const base = terrainHeight(p, 0, 0),
    positions = antennaPositions(p);
  return {
    center_x: 0,
    center_y: 0,
    radius_m: Math.min(50, p.settings.radius_m),
    bottom_m: base,
    top_m: Math.max(base + 20, ...positions.map((v) => v[2] + 20)),
    step_m: Math.max(1, p.settings.grid_step_m),
  };
}
function axis(
  start: number,
  end: number,
  step: number,
  seeds: number[],
): number[] {
  const n = Math.ceil((end - start) / step);
  if (n > 1000)
    throw new Error(
      "Le volume est trop grand pour ce pas. Augmentez le pas ou réduisez la zone.",
    );
  return [
    ...new Set([
      ...Array.from({ length: n + 1 }, (_, i) =>
        Math.min(end, start + i * step),
      ),
      ...seeds.filter((v) => v > start && v < end),
    ]),
  ]
    .sort((a, b) => a - b)
    .filter((v, i, values) => !i || v - values[i - 1] > 1e-7);
}
// Six tetrahedra share the same cube diagonal; each crossing is interpolated in
// exposure ratio. Positions use local X/Y and absolute elevation, like exposureAt.
const tetrahedra = [
  [0, 1, 2, 6],
  [0, 2, 3, 6],
  [0, 3, 7, 6],
  [0, 7, 4, 6],
  [0, 4, 5, 6],
  [0, 5, 1, 6],
];
export function volumeSurface(
  p: PlannerProject,
  o: VolumeOptions,
  onProgress?: (percent: number) => void,
): ExposureVolume {
  if (
    !Object.values(o).every(Number.isFinite) ||
    o.radius_m <= 0 ||
    o.top_m <= o.bottom_m ||
    o.step_m < 0.2
  )
    throw new Error(
      "Définissez une zone positive, une altitude haute supérieure à l’altitude basse et un pas d’au moins 0,2 m.",
    );
  const antennas = antennaPositions(p),
    xs = axis(
      o.center_x - o.radius_m,
      o.center_x + o.radius_m,
      o.step_m,
      antennas.map((v) => v[0]),
    ),
    ys = axis(
      o.center_y - o.radius_m,
      o.center_y + o.radius_m,
      o.step_m,
      antennas.map((v) => v[1]),
    ),
    zs = axis(
      o.bottom_m,
      o.top_m,
      o.step_m,
      antennas.map((v) => v[2]),
    );
  const nx = xs.length,
    ny = ys.length,
    nz = zs.length,
    nodes = nx * ny * nz;
  if (nodes > 220000)
    throw new Error(
      `Le volume demande ${nodes.toLocaleString("fr-FR")} points (maximum 220 000). Augmentez le pas ou réduisez la zone.`,
    );
  const values = new Float64Array(nodes),
    index = (x: number, y: number, z: number) => (z * ny + y) * nx + x;
  let clipped = false;
  for (let z = 0; z < nz; z++) {
    for (let y = 0; y < ny; y++)
      for (let x = 0; x < nx; x++) {
        const value = exposureAt(p, xs[x], ys[y], zs[z]).exposure_ratio;
        values[index(x, y, z)] = value;
        if (
          value >= 1 &&
          (x === 0 ||
            y === 0 ||
            z === 0 ||
            x === nx - 1 ||
            y === ny - 1 ||
            z === nz - 1)
        )
          clipped = true;
      }
    onProgress?.(Math.round(((z + 1) / nz) * 70));
  }
  const positions: number[] = [];
  const triangle = (a: Vec, b: Vec, c: Vec, direction: Vec) => {
    const u = b.map((v, i) => v - a[i]),
      v = c.map((v, i) => v - a[i]);
    const normal = [
      u[1] * v[2] - u[2] * v[1],
      u[2] * v[0] - u[0] * v[2],
      u[0] * v[1] - u[1] * v[0],
    ];
    if (normal.every((n) => Math.abs(n) < 1e-12)) return;
    if (normal.reduce((s, n, i) => s + n * direction[i], 0) < 0)
      [b, c] = [c, b];
    positions.push(...a, ...b, ...c);
    if (positions.length > 9000000)
      throw new Error("Surface trop complexe : augmentez le pas du maillage.");
  };
  for (let z = 0; z < nz - 1; z++) {
    for (let y = 0; y < ny - 1; y++)
      for (let x = 0; x < nx - 1; x++) {
        const coords: Vec[] = [
          [xs[x], ys[y], zs[z]],
          [xs[x + 1], ys[y], zs[z]],
          [xs[x + 1], ys[y + 1], zs[z]],
          [xs[x], ys[y + 1], zs[z]],
          [xs[x], ys[y], zs[z + 1]],
          [xs[x + 1], ys[y], zs[z + 1]],
          [xs[x + 1], ys[y + 1], zs[z + 1]],
          [xs[x], ys[y + 1], zs[z + 1]],
        ];
        const vals = [
          values[index(x, y, z)],
          values[index(x + 1, y, z)],
          values[index(x + 1, y + 1, z)],
          values[index(x, y + 1, z)],
          values[index(x, y, z + 1)],
          values[index(x + 1, y, z + 1)],
          values[index(x + 1, y + 1, z + 1)],
          values[index(x, y + 1, z + 1)],
        ];
        if (vals.every((v) => v < 1) || vals.every((v) => v >= 1)) continue;
        const cross = (a: number, b: number): Vec => {
          const t = (1 - vals[a]) / (vals[b] - vals[a]);
          return coords[a].map((v, i) => v + t * (coords[b][i] - v)) as Vec;
        };
        for (const tet of tetrahedra) {
          const hi = tet.filter((i) => vals[i] >= 1),
            lo = tet.filter((i) => vals[i] < 1);
          if (!hi.length || !lo.length) continue;
          const direction = [0, 1, 2].map(
            (k) =>
              lo.reduce((s, i) => s + coords[i][k] / lo.length, 0) -
              hi.reduce((s, i) => s + coords[i][k] / hi.length, 0),
          ) as Vec;
          if (hi.length === 1 || lo.length === 1) {
            const single = hi.length === 1 ? hi[0] : lo[0],
              others = tet.filter((i) => i !== single);
            triangle(
              cross(single, others[0]),
              cross(single, others[1]),
              cross(single, others[2]),
              direction,
            );
          } else {
            const ac = cross(hi[0], lo[0]),
              ad = cross(hi[0], lo[1]),
              bc = cross(hi[1], lo[0]),
              bd = cross(hi[1], lo[1]);
            triangle(ac, ad, bc, direction);
            triangle(ad, bd, bc, direction);
          }
        }
      }
    onProgress?.(70 + Math.round(((z + 1) / (nz - 1)) * 30));
  }
  const limit_index = p.settings.selected_limit;
  return {
    positions: new Float32Array(positions),
    nodes,
    triangles: positions.length / 9,
    step_m: o.step_m,
    clipped,
    limit_index,
    color: p.limits[limit_index]?.color ?? "#d32246",
  };
}
