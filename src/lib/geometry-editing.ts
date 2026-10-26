import type { Building, PlannerProject, TerrainPoint } from "../types";
import { inside, segmentCrosses, terrainHeight, toGeo, toLocal } from "./geometry";
import { uid } from "./project";

export type XY = [number, number];
export type LocalBounds = { west: number; south: number; east: number; north: number; metresPerPixel?: number };
export type BaseMethod = "min" | "max" | "mean";
export function buildingFoundation(project: PlannerProject, building: Building, method: BaseMethod) {
  const heights = building.points.map(([x, y]) => terrainHeight(project, x, y));
  if (!heights.length) throw new Error("Le bâtiment ne contient aucun sommet.");
  return method === "min" ? Math.min(...heights) : method === "max" ? Math.max(...heights) : heights.reduce((a, b) => a + b, 0) / heights.length;
}

/** Module1.SquarePline: successive perpendicular projection, retaining the first edge direction. */
export function squareBuilding(points: XY[]): XY[] {
  if (points.length < 4 || points.length % 2) throw new Error("L’équarrissage nécessite un nombre pair de sommets (au moins quatre).");
  const ring = [...points.map((p) => [...p] as XY), [...points[0]] as XY];
  for (let i = 0; i < points.length; i++) {
    const a = ring[i], b = ring[i + 1], c = ring[i + 2 < ring.length ? i + 2 : 1];
    const dx = b[0] - a[0], dy = b[1] - a[1], den = dx * dx + dy * dy;
    if (den < 1e-10) throw new Error("Deux sommets consécutifs sont confondus.");
    const t = ((c[0] - a[0]) * dx + (c[1] - a[1]) * dy) / den;
    ring[i + 1] = [a[0] + dx * t, a[1] + dy * t];
    if (i + 1 === points.length) ring[0] = ring[i + 1];
  }
  return ring.slice(0, -1);
}

export function pointsIntersectBounds(points: XY[], bounds: LocalBounds, closed = false) {
  const inBox = ([x, y]: XY) => x >= bounds.west && x <= bounds.east && y >= bounds.south && y <= bounds.north;
  if (points.some(inBox)) return true;
  const box: XY[] = [[bounds.west, bounds.south], [bounds.east, bounds.south], [bounds.east, bounds.north], [bounds.west, bounds.north]];
  if (closed && box.some(([x, y]) => inside(x, y, points))) return true;
  for (let i = 0; i < points.length - (closed ? 0 : 1); i++)
    for (let j = 0; j < 4; j++)
      if (segmentCrosses(points[i], points[(i + 1) % points.length], box[j], box[(j + 1) % 4]) !== null) return true;
  return false;
}

export function deleteOutsideView(p: PlannerProject, bounds: LocalBounds, includeTerrain: boolean): PlannerProject {
  return { ...p,
    sources: p.sources.filter((s) => pointsIntersectBounds([toLocal(s.lat, s.lng, p.origin)], bounds)),
    control_points: p.control_points.filter((c) => pointsIntersectBounds([[c.x_m, c.y_m]], bounds)),
    buildings: p.buildings.filter((b) => pointsIntersectBounds(b.points, bounds, true)),
    lines: p.lines.filter((l) => pointsIntersectBounds(l.points, bounds)),
    ...(includeTerrain ? {
      terrain_points: p.terrain_points.filter((t) => pointsIntersectBounds([[t.x, t.y]], bounds)),
      terrain_contours: p.terrain_contours?.filter((t) => pointsIntersectBounds(t.points, bounds, t.closed)),
    } : {}),
  };
}

export function moveGeometry(p: PlannerProject, id: string, dx: number, dy: number, vertex?: number): PlannerProject {
  if (![dx, dy].every(Number.isFinite)) throw new Error("Déplacement non numérique.");
  const moved = (points: XY[]) => points.map(([x, y], i): XY => vertex === undefined || i === vertex ? [x + dx, y + dy] : [x, y]);
  return { ...p,
    buildings: p.buildings.map((b) => b.id === id ? { ...b, points: moved(b.points) } : b),
    lines: p.lines.map((l) => l.id === id ? { ...l, points: moved(l.points) } : l),
    terrain_contours: p.terrain_contours?.map((c) => c.id === id ? { ...c, points: moved(c.points) } : c),
    terrain_points: p.terrain_points.map((t) => t.id === id ? { ...t, x: t.x + dx, y: t.y + dy, wm_x: undefined, wm_y: undefined } : t),
  };
}

export function moveControlPoint(p: PlannerProject, id: string, lat: number, lng: number): PlannerProject {
  const [x_m, y_m] = toLocal(lat, lng, p.origin);
  return { ...p, control_points: p.control_points.map((c) => c.id === id ? { ...c, lat, lng, x_m, y_m } : c) };
}

export interface OsmElement { type: string; id: number; lat?: number; lon?: number; nodes?: number[]; geometry?: {lat:number;lon:number}[]; tags?: Record<string, string>; }
export interface OsmData { elements: OsmElement[]; }
export interface OsmBuildingOptions { defaultHeight: number; floorHeight: number; foundation: number; }
export const OSM_BUILDING_DEFAULTS: OsmBuildingOptions = { defaultHeight: 20, floorHeight: 3, foundation: 0 };
function numberTag(value?: string) {
  if (!value) return undefined;
  const n = Number(value.trim().replace(/\s*m\s*$/i, "").replace(",", "."));
  return Number.isFinite(n) && n > 0 ? n : undefined;
}
function osmGeometries(data: OsmData, origin: XY) {
  if (!Array.isArray(data.elements)) throw new Error("Réponse OSM invalide.");
  const nodes = new Map(data.elements.filter((el) => el.type === "node" && Number.isFinite(el.lat) && Number.isFinite(el.lon)).map((el) => [el.id, {lat: el.lat!, lon: el.lon!}]));
  return data.elements.filter((el) => el.type === "way").map((el) => ({el, points: (el.geometry ?? el.nodes?.map((id) => nodes.get(id)).filter((node): node is {lat:number;lon:number} => !!node) ?? []).map(({lat,lon}) => toLocal(lat, lon, origin)) }));
}
export function importOsmBuildings(p: PlannerProject, data: OsmData, options = OSM_BUILDING_DEFAULTS): PlannerProject {
  if (![options.defaultHeight, options.floorHeight].every((n) => Number.isFinite(n) && n > 0 && n <= 300) || !Number.isFinite(options.foundation)) throw new Error("Hauteurs OSM invalides.");
  const existing = new Set(p.buildings.flatMap((b) => b.points[0] ? [`${b.points[0][0].toFixed(2)},${b.points[0][1].toFixed(2)}`] : []));
  const added: Building[] = [];
  for (const {el, points} of osmGeometries(data, p.origin)) {
    if (!el.tags?.building || points.length < 3) continue;
    if (points.length > 3 && Math.hypot(points[0][0] - points[points.length-1][0], points[0][1] - points[points.length-1][1]) < 0.001) points.pop();
    const key = `${points[0][0].toFixed(2)},${points[0][1].toFixed(2)}`;
    if (existing.has(key)) continue;
    existing.add(key);
    const levels = numberTag(el.tags["building:levels"]);
    const height = numberTag(el.tags.height) ?? (levels ? levels * options.floorHeight : options.defaultHeight);
    if (height > 300) throw new Error(`Hauteur OSM supérieure à 300 m : ${el.id}.`);
    added.push({ id: uid(), name: el.tags.name || (levels ? `${levels} étages` : `OSM ${el.id}`), height_m: height, foundation_m: options.foundation, attenuation_db: 0, active: true, visible: true, points });
  }
  return {...p, buildings:[...p.buildings, ...added]};
}

export function importOsmLines(p: PlannerProject, data: OsmData): PlannerProject {
  const signatures = new Set(p.lines.map((l) => `${l.kind}:${JSON.stringify(l.points)}`));
  const added: PlannerProject["lines"] = [];
  for (const {el, points} of osmGeometries(data, p.origin)) {
    const tags = el.tags ?? {}, kind = tags.barrier === "fence" ? "barrier" : "road";
    if (!tags.highway && tags.barrier !== "fence" || points.length < 2) continue;
    const signature = `${kind}:${JSON.stringify(points)}`;
    if (signatures.has(signature)) continue;
    signatures.add(signature);
    added.push({id:uid(), kind, name:tags.name || `OSM ${el.id}`, points, color:kind === "road" ? "#666666" : "#8b4513", width:numberTag(tags.width) ?? (kind === "road" ? 4 : 1)});
  }
  return {...p, lines:[...p.lines,...added]};
}

/** SRTM HGT signed big-endian samples, north-to-south; existing relief is replaced. */
export function importSrtm(p: PlannerProject, bytes: Uint8Array, name: string, bounds: LocalBounds): PlannerProject {
  const size = Math.sqrt(bytes.byteLength / 2);
  if (![1201,3601].includes(size)) throw new Error("Format HGT invalide : grille attendue 1201×1201 ou 3601×3601.");
  const match = /([NS])(\d{2})([EW])(\d{3})/i.exec(name);
  if (!match) throw new Error("Le nom HGT doit indiquer la tuile, par exemple N45E009.hgt.");
  const lat = Number(match[2]) * (/N/i.test(match[1]) ? 1 : -1), lng = Number(match[4]) * (/E/i.test(match[3]) ? 1 : -1);
  const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength), points: TerrainPoint[] = [];
  const sw = toGeo(bounds.west, bounds.south, p.origin), ne = toGeo(bounds.east, bounds.north, p.origin);
  const row0 = Math.max(0, Math.ceil((lat+1-ne[0])*(size-1))), row1 = Math.min(size-1,Math.floor((lat+1-sw[0])*(size-1)));
  const col0 = Math.max(0,Math.ceil((sw[1]-lng)*(size-1))), col1 = Math.min(size-1,Math.floor((ne[1]-lng)*(size-1)));
  if ((row1-row0+1)*(col1-col0+1)>100000) throw new Error("Zone trop grande : réduisez la vue avant d’importer le MNT (100000 points maximum).");
  for(let row=row0;row<=row1;row++) for(let col=col0;col<=col1;col++) {
    const elevation=view.getInt16((row*size+col)*2,false);
    if(elevation===-32768) continue;
    const [x,y]=toLocal(lat+1-row/(size-1),lng+col/(size-1),p.origin);
    points.push({id:uid(),x,y,elevation});
  }
  if(points.length<3) throw new Error("Pas assez de données SRTM dans la zone visible. Le terrain existant est conservé.");
  return {...p,terrain_points:points,terrain_contours:[]};
}
