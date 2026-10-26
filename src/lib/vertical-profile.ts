import type { Contour, PlannerProject, VerticalCut } from "../types";
import { inside, segmentCrosses, terrainHeight } from "./geometry";
import { mastDimensions, mastFoundation } from "./scene-geometry";
export function cutWorld(cut: VerticalCut, distance: number): [number, number] {
  const angle = cut.azimuth_deg * Math.PI / 180;
  return [cut.x + distance * Math.sin(angle), cut.y + distance * Math.cos(angle)];
}
export function cutCoordinates(cut: VerticalCut, x: number, y: number) {
  const angle = cut.azimuth_deg * Math.PI / 180, dx = x - cut.x, dy = y - cut.y;
  return { distance: dx * Math.sin(angle) + dy * Math.cos(angle), offset: dx * Math.cos(angle) - dy * Math.sin(angle) };
}
export function verticalProfile(project: PlannerProject, cut: VerticalCut, left: number, right: number, count = 100) {
  const base = terrainHeight(project, 0, 0), start = cutWorld(cut, left), end = cutWorld(cut, right);
  const terrain = Array.from({ length: count + 1 }, (_, i): [number, number] => {
    const d = left + (right - left) * i / count; return [d, terrainHeight(project, ...cutWorld(cut, d)) - base];
  });
  const buildings = project.buildings.filter(b => b.visible !== false).flatMap(building => {
    const crossings = building.points.map((p, i) => segmentCrosses(start, end, p, building.points[(i + 1) % building.points.length])).filter((value): value is number => value !== null);
    if (inside(...start, building.points)) crossings.push(0);
    if (inside(...end, building.points)) crossings.push(1);
    const sorted = [...new Set(crossings.map(value => Math.round(value * 1e10) / 1e10))].sort((a, b) => a - b), spans = [];
    for (let i = 0; i + 1 < sorted.length; i += 2) spans.push({ id: `${building.id}:${i}`, name: building.name,
      left: left + sorted[i] * (right - left), right: left + sorted[i + 1] * (right - left),
      bottom: building.foundation_m - base, top: building.foundation_m + building.height_m - base });
    return spans;
  });
  const towers = project.lines.flatMap(line => {
    if (line.kind !== "label" || !line.points[0]) return [];
    const dimensions = mastDimensions(line.name); if (!dimensions) return [];
    const position = cutCoordinates(cut, ...line.points[0]); if (Math.abs(position.offset) > dimensions.lowerWidth / 2) return [];
    const foundation = mastFoundation(project, ...line.points[0]) - base;
    return [{ id: line.id, distance: position.distance, foundation, top: foundation + dimensions.upperHeight + dimensions.taperedHeight, width: dimensions.lowerWidth }];
  });
  return { terrain, buildings, towers };
}
export function cutExposureBounds(lines: Contour[], ground: (distance: number) => number = () => 0) {
  let left = Infinity, right = -Infinity, bottom = Infinity, top = -Infinity, maxHeight = 0, minDistance = Infinity;
  for (const line of lines) for (const segment of line.segments) for (const [x, y] of segment) {
    if (x > right) { right = x; maxHeight = y; } left = Math.min(left, x); bottom = Math.min(bottom, y); top = Math.max(top, y);
    // CalcSZZ.DimensionLowerestZO measures vertical clearance above terrain.
    minDistance = Math.min(minDistance, y - ground(x));
  }
  return Number.isFinite(left) ? { left, right, bottom, top, maxHeight, minDistance } : null;
}
