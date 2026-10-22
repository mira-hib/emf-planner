import type { Contour, PlannerProject, VerticalCut } from "../types";
import { inside, terrainHeight } from "./geometry";

export type ScenePoint = [number, number, number];
export type SceneEdge = [ScenePoint, ScenePoint];

/** Eyeshot's world (east, north, altitude) represented in Three's Y-up world. */
export function scenePosition(
  x: number,
  y: number,
  altitude: number,
  originAltitude: number,
): ScenePoint {
  return [x, altitude - originAltitude, -y];
}

/** Horizontal-cut heights are relative to the origin, unlike terrain altitudes. */
export function horizontalPosition(
  point: [number, number],
  height: number,
): ScenePoint {
  return [point[0], height, -point[1]];
}

/** Vertical-grid coordinates already contain signed distance and relative height. */
export function verticalPosition(
  point: [number, number],
  cut: VerticalCut,
): ScenePoint {
  const azimuth = (cut.azimuth_deg * Math.PI) / 180;
  return [
    cut.x + point[0] * Math.sin(azimuth),
    point[1],
    -cut.y - point[0] * Math.cos(azimuth),
  ];
}

export interface MastDimensions {
  upperHeight: number;
  taperedHeight: number;
  upperWidth: number;
  lowerWidth: number;
}

/** Form1_Public.MastDimension and UC_3D.DrawMast3D: only explicit tower annotations. */
export function mastDimensions(text: string): MastDimensions | null {
  const fields = text.split("/");
  if (!text.includes("Tower") || fields.length !== 5) return null;
  const values = fields
    .slice(1)
    .map((value) => Number(value.trim().replace(",", ".")));
  if (
    values.some((value) => !Number.isFinite(value) || value < 0) ||
    values[2] <= 0 ||
    values[3] <= 0
  )
    return null;
  return {
    upperHeight: values[0],
    taperedHeight: values[1],
    upperWidth: values[2],
    lowerWidth: values[3],
  };
}

export function mastFoundation(
  project: PlannerProject,
  x: number,
  y: number,
): number {
  return project.buildings.reduce(
    (height, building) =>
      inside(x, y, building.points)
        ? Math.max(height, building.foundation_m + building.height_m)
        : height,
    terrainHeight(project, x, y),
  );
}

/** Four uprights, square rings and crossed braces, as the recovered native renderer. */
export function mastEdges(
  dimensions: MastDimensions,
  x: number,
  y: number,
  foundation: number,
  originAltitude: number,
): SceneEdge[] {
  const edges: SceneEdge[] = [];
  const corner = (width: number, height: number, index: number): ScenePoint =>
    scenePosition(
      x + ([-1, 1, 1, -1][index] * width) / 2,
      y + ([-1, -1, 1, 1][index] * width) / 2,
      foundation + height,
      originAltitude,
    );
  const section = (
    baseHeight: number,
    height: number,
    bottomWidth: number,
    topWidth: number,
  ) => {
    if (height <= 0) return;
    const count = Math.max(
      1,
      Math.ceil((0.5 * height) / dimensions.upperWidth),
    );
    for (let c = 0; c < 4; c++)
      edges.push([
        corner(bottomWidth, baseHeight, c),
        corner(topWidth, baseHeight + height, c),
      ]);
    for (let level = 1; level <= count; level++) {
      const t = level / count,
        previous = (level - 1) / count;
      const h = baseHeight + height * t,
        hp = baseHeight + height * previous;
      const w = bottomWidth + (topWidth - bottomWidth) * t,
        wp = bottomWidth + (topWidth - bottomWidth) * previous;
      for (let c = 0; c < 4; c++) {
        const next = (c + 1) % 4;
        edges.push(
          [corner(w, h, c), corner(w, h, next)],
          [corner(wp, hp, c), corner(w, h, next)],
          [corner(wp, hp, next), corner(w, h, c)],
        );
      }
    }
  };
  section(
    0,
    dimensions.taperedHeight,
    dimensions.lowerWidth,
    dimensions.upperWidth,
  );
  section(
    dimensions.taperedHeight,
    dimensions.upperHeight,
    dimensions.upperWidth,
    dimensions.upperWidth,
  );
  return edges;
}

/** Join marching-square edges without smoothing or filling an open calculation boundary. */
export function contourPaths(
  contour: Pick<Contour, "segments">,
): [number, number][][] {
  type Point = [number, number];
  const key = (point: Point) =>
    `${Math.round(point[0] * 1e6)},${Math.round(point[1] * 1e6)}`;
  const edges: [Point, Point][] = contour.segments.flatMap((segment) =>
    segment.slice(1).map((p, index) => [segment[index], p] as [Point, Point]),
  );
  const adjacency = new Map<string, number[]>();
  edges.forEach(([a, b], index) => {
    for (const point of [a, b]) {
      const k = key(point);
      adjacency.set(k, [...(adjacency.get(k) ?? []), index]);
    }
  });
  const visited = new Set<number>(),
    paths: Point[][] = [];
  const walk = (index: number, start: Point) => {
    const path = [start];
    let next = index,
      current = start;
    while (!visited.has(next)) {
      visited.add(next);
      const [a, b] = edges[next];
      current = key(a) === key(current) ? b : a;
      path.push(current);
      const candidate = adjacency
        .get(key(current))
        ?.find((edge) => !visited.has(edge));
      if (candidate === undefined) break;
      next = candidate;
    }
    paths.push(path);
  };
  edges.forEach(([a, b], index) => {
    if (
      !visited.has(index) &&
      (adjacency.get(key(a))?.length === 1 ||
        adjacency.get(key(b))?.length === 1)
    )
      walk(index, adjacency.get(key(a))?.length === 1 ? a : b);
  });
  edges.forEach(([a], index) => {
    if (!visited.has(index)) walk(index, a);
  });
  return paths;
}

export function closedContourPolygons(paths: [number, number][][]) {
  const rings = paths.filter(
    (path) =>
      path.length >= 4 &&
      Math.hypot(
        path[0][0] - path[path.length - 1][0],
        path[0][1] - path[path.length - 1][1],
      ) < 1e-5,
  );
  const parents = rings.map((ring, index) =>
    rings
      .map((outer, j) =>
        j !== index && inside(ring[0][0], ring[0][1], outer) ? j : -1,
      )
      .filter((j) => j >= 0),
  );
  return rings.flatMap((outer, i) =>
    parents[i].length % 2
      ? []
      : [
          {
            outer,
            holes: rings.filter(
              (_, j) =>
                parents[j].length === parents[i].length + 1 &&
                parents[j].includes(i),
            ),
          },
        ],
  );
}
