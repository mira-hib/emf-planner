import type { Building, DrawingLine } from "../types";
import { uid } from "./project";
import { toLocal } from "./geometry";

const OVERPASS_URL = "https://overpass-api.de/api/interpreter";

interface OverpassElement {
  type: string;
  id: number;
  lat?: number;
  lon?: number;
  nodes?: number[];
  tags?: Record<string, string>;
  geometry?: { lat: number; lon: number }[];
}

async function query(ql: string): Promise<OverpassElement[]> {
  const body = `[out:json][timeout:25];${ql}`;
  const res = await fetch(OVERPASS_URL, {
    method: "POST",
    body: `data=${encodeURIComponent(body)}`,
    headers: { "Content-Type": "application/x-www-form-urlencoded" },
  });
  if (!res.ok) throw new Error(`Overpass API: ${res.status} ${res.statusText}`);
  const json = await res.json();
  return json.elements ?? [];
}

export async function fetchOsmBuildings(
  origin: [number, number],
  radiusM: number,
): Promise<Building[]> {
  const [lat, lon] = origin;
  const ql = `(way["building"](around:${radiusM},${lat},${lon}););out geom;`;
  const elements = await query(ql);
  return elements
    .filter((e) => e.geometry && e.geometry.length >= 3)
    .map((e) => {
      const pts = e.geometry!.map(
        (g) => toLocal(g.lat, g.lon, origin) as [number, number],
      );
      const name =
        e.tags?.name ??
        e.tags?.["addr:street"] ??
        `OSM ${e.id}`;
      const height = parseFloat(e.tags?.height ?? e.tags?.["building:levels"] ?? "0");
      return {
        id: uid(),
        name,
        height_m: height > 0 ? (e.tags?.height ? height : height * 3) : 10,
        foundation_m: 0,
        attenuation_db: 0,
        points: pts,
        active: true,
      };
    });
}

export async function fetchOsmRoads(
  origin: [number, number],
  radiusM: number,
): Promise<DrawingLine[]> {
  const [lat, lon] = origin;
  const ql = `(way["highway"](around:${radiusM},${lat},${lon});way["barrier"](around:${radiusM},${lat},${lon}););out geom;`;
  const elements = await query(ql);
  return elements
    .filter((e) => e.geometry && e.geometry.length >= 2)
    .map((e) => {
      const pts = e.geometry!.map(
        (g) => toLocal(g.lat, g.lon, origin) as [number, number],
      );
      const isBarrier = !!e.tags?.barrier;
      const name =
        e.tags?.name ??
        (isBarrier ? `Clôture ${e.id}` : `Route ${e.id}`);
      return {
        id: uid(),
        name,
        kind: (isBarrier ? "barrier" : "road") as "road" | "barrier",
        points: pts,
        color: isBarrier ? "#8B4513" : "#666666",
        width: isBarrier ? 1 : 2,
      };
    });
}
