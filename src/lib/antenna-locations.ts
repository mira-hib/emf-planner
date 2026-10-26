import type { PlannerProject } from "../types";
import {
  mercatorGeo,
  mercatorPoint,
  nativeLocal,
  nativeScale,
} from "./native-coordinates";
import type { CoordinateFormat } from "./coordinates";

type Pair = [number, number];
// CalcCoords.ConvLatToStr/ConvLonToStr, including their hemisphere precision differences.
export function formatAntennaCoordinate(
  value: number,
  format: CoordinateFormat,
  latitude: boolean,
): string {
  const magnitude = Math.abs(value),
    prefix = value < 0 ? (latitude ? "S" : "W") : "";
  const fixed = (v: number, decimals: number) =>
    v.toFixed(decimals).padStart(decimals ? decimals + 3 : 2, "0");
  if (format === "decimal") return `${prefix}${fixed(magnitude, 6)}°`;
  const degrees = Math.floor(magnitude);
  if (format === "dm")
    return `${prefix}${fixed(degrees, 0)}°${fixed(Math.fround(Number(((magnitude - degrees) * 60).toFixed(4))), 4)}'`;
  const minutes = Math.floor(Number(((magnitude - degrees) * 60).toFixed(6)));
  const seconds = Math.fround(
    Number(((magnitude - degrees - minutes / 60) * 3600).toFixed(2)),
  );
  return `${prefix}${fixed(degrees, 0)}°${fixed(minutes, 0)}'${fixed(seconds, value >= 0 ? 1 : latitude ? 2 : 0)}"`;
}
// UC_Antennas and Form1.AntennaDrag: the slider is in tenths of a WM unit.
export function antennaCaptureDistance(project: PlannerProject): number {
  const value = project.settings.antenna_capture_distance ?? 15;
  return Number.isFinite(value)
    ? Math.max(1, Math.min(30, Math.round(value)))
    : 15;
}
export function antennaLocalToGeo(
  project: PlannerProject,
  x: number,
  y: number,
): Pair {
  const [ox, oy] = mercatorPoint(project.origin),
    scale = nativeScale(project.origin);
  return mercatorGeo([ox + x / scale, oy + y / scale]);
}
export function moveAntennas(
  project: PlannerProject,
  ids: string[],
  geo: Pair,
): PlannerProject {
  if (
    !geo.every(Number.isFinite) ||
    Math.abs(geo[0]) > 85 ||
    Math.abs(geo[1]) > 180
  )
    throw new Error("Position d’antenne invalide.");
  const selected = new Set(ids),
    [x_m, y_m] = nativeLocal(...geo, project.origin, true);
  return {
    ...project,
    sources: project.sources.map((s) =>
      selected.has(s.id) ? { ...s, lat: geo[0], lng: geo[1], x_m, y_m } : s,
    ),
  };
}
export function editAntennaCoordinate(
  project: PlannerProject,
  ids: string[],
  key: "x_m" | "y_m",
  value: number,
): PlannerProject {
  if (!Number.isFinite(value))
    throw new Error("Coordonnée d’antenne invalide.");
  const selected = new Set(ids);
  return {
    ...project,
    sources: project.sources.map((s) => {
      if (!selected.has(s.id)) return s;
      const next = { ...s, [key]: ids.length > 1 ? Math.fround(value) : value };
      const [lat, lng] = antennaLocalToGeo(
        project,
        next.x_m ?? 0,
        next.y_m ?? 0,
      );
      if (Math.abs(lat) > 85 || Math.abs(lng) > 180)
        throw new Error("Position d’antenne hors carte.");
      return { ...next, lat, lng };
    }),
  };
}
export function snapAntenna(
  project: PlannerProject,
  id: string,
  geo: Pair,
): Pair {
  const [x, y] = mercatorPoint(geo),
    tolerance = Math.fround(antennaCaptureDistance(project) / 10);
  const target = project.sources.find((s) => {
    if (s.id === id) return false;
    const [sx, sy] = mercatorPoint([s.lat, s.lng]);
    return Math.abs(sx - x) < tolerance && Math.abs(sy - y) < tolerance;
  });
  return target ? [target.lat, target.lng] : geo;
}
