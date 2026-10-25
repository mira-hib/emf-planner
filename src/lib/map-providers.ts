import type { PlannerProject } from "../types";

export interface BasemapProvider {
  url: string;
  attribution: string;
  projection?: "spherical" | "ellipsoidal";
  maxZoom?: number;
  kind?: "image";
  configuration?: string;
}

// Order and labels follow Form1.LoadTScmbURL and CustomBaseMaps.set (EMFPlanner 7.1).
// Legacy credential strings shipped by the vendor are intentionally not distributed.
export const TILES: Record<string, BasemapProvider> = {
  "Calibrated map image": { url: "", attribution: "", kind: "image" },
  OpenStreetMap: {
    url: "https://tile.openstreetmap.org/[Z]/[X]/[Y].png",
    attribution:
      '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
    maxZoom: 19,
  },
  "Carto Basemap": {
    url: "https://basemaps.cartocdn.com/light_nolabels/[Z]/[X]/[Y]@2x.png",
    attribution:
      '© OpenStreetMap © <a href="https://carto.com/attributions">CARTO</a>',
    maxZoom: 20,
  },
  "OSM Relief": {
    url: "https://a.tile.thunderforest.com/cycle/[Z]/[X]/[Y].png",
    attribution: "© Thunderforest © OpenStreetMap",
    configuration:
      "Une clé Thunderforest est nécessaire. Configurez l’URL OSM Relief dans Fonds de carte personnalisés.",
    maxZoom: 22,
  },
  "Google Map": {
    url: "https://mt2.google.com/vt/lyrs=m@169000000&hl=en&x=[X]&y=[Y]&zoom=[17-Z]&s=Galile",
    attribution: "© Google",
    maxZoom: 21,
  },
  "Google Satellite": {
    // The packaged khms2 v=930 endpoint returns 404 (network audit 2026-10-22).
    url: "https://mt2.google.com/vt/lyrs=s&x=[X]&y=[Y]&z=[Z]",
    attribution: "© Google",
    maxZoom: 21,
  },
  "Esri Satellite": {
    url: "https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/[Z]/[Y]/[X].jpg",
    attribution: "© Esri, Maxar, Earthstar Geographics, GIS User Community",
    maxZoom: 23,
  },
  "HERE WeGo Satellite": {
    url: "",
    attribution: "© HERE",
    configuration:
      "Configurez une URL HERE avec votre clé dans Fonds de carte personnalisés (nom HERE WeGo Satellite).",
    maxZoom: 20,
  },
  "Mapquest Satellite": {
    url: "",
    attribution: "© MapQuest © Mapbox",
    configuration:
      "Le service MapQuest historique nécessite une URL et un jeton valides. Configurez Mapquest Satellite dans Fonds de carte personnalisés.",
    maxZoom: 20,
  },
  OpenTopoMap: {
    url: "https://a.tile.opentopomap.org/[Z]/[X]/[Y].png",
    attribution: "© OpenStreetMap, SRTM | © OpenTopoMap (CC-BY-SA)",
    maxZoom: 17,
  },
  "Bing Sat": {
    url: "https://ecn.t0.tiles.virtualearth.net/tiles/a[RES].jpeg?g=0",
    attribution: "© Microsoft Bing",
    maxZoom: 21,
  },
  "2GIS": {
    url: "https://tile1.maps.2gis.com/tiles?v=1112&x=[X]&y=[Y]&z=[Z]",
    attribution: "© 2GIS",
    maxZoom: 19,
  },
  "Yandex map": {
    url: "https://core-renderer-tiles.maps.yandex.net/tiles?l=map&x=[X]&y=[Y]&z=[Z]",
    attribution: "© Yandex",
    projection: "ellipsoidal",
    maxZoom: 21,
  },
  "Yandex Sat": {
    url: "https://sat01.maps.yandex.net/tiles?l=sat&x=[X]&y=[Y]&z=[Z]&g=[Gagarin.Substring(0,RND(0,8))]",
    attribution: "© Yandex",
    projection: "ellipsoidal",
    maxZoom: 21,
  },
};

export function normalizeMapProvider(name: string) {
  return (
    (
      {
        "Carto clair": "Carto Basemap",
        "Satellite Esri": "Esri Satellite",
        "MAPNIK (OpenStreetMap)": "OpenStreetMap",
      } as Record<string, string>
    )[name] ?? name.trim()
  );
}

export function mapProviders(
  project: Pick<PlannerProject, "custom_maps">,
): Record<string, BasemapProvider> {
  return {
    ...TILES,
    ...Object.fromEntries(
      (project.custom_maps ?? [])
        .filter((m) => m.name.trim() && /^https?:\/\//i.test(m.url))
        .map((m) => [
          m.name.trim(),
          {
            ...TILES[m.name.trim()],
            url: m.url,
            attribution: m.attribution,
            projection: m.url.toLowerCase().includes("yandex")
              ? "ellipsoidal"
              : "spherical",
            configuration: undefined,
          },
        ]),
    ),
  };
}

export function selectedMapProvider(
  project: Pick<PlannerProject, "custom_maps" | "settings">,
) {
  return mapProviders(project)[
    normalizeMapProvider(project.settings.map_provider)
  ];
}

export function tileQuadkey(x: number, y: number, z: number) {
  let key = "";
  for (let i = z - 1; i >= 0; i--)
    key += String(
      (Math.floor(x / 2 ** i) % 2) + 2 * (Math.floor(y / 2 ** i) % 2),
    );
  return key;
}

/** Native CalcTileWeb.CalcNameTile placeholders, plus Leaflet custom-map syntax. */
export function tileUrl(
  template: string,
  x: number,
  y: number,
  z: number,
  random = Math.random,
) {
  const replacements: Record<string, string | number> = {
    "[X]": x,
    "[Y]": y,
    "[Z]": z,
    "[17-Z]": 17 - z,
    "[Z+1]": z + 1,
    "[FX]": Math.floor(x / 1024),
    "[FY]": Math.floor(y / 1024),
    "[RES]": tileQuadkey(x, y, z),
    "[Gagarin.Substring(0,RND(0,8))]": "Gagarin".slice(
      0,
      Math.floor(random() * 8),
    ),
    "[RND(a,b,c)]": "abc"[Math.floor(random() * 3)],
    "{x}": x,
    "{y}": y,
    "{z}": z,
    "{-y}": 2 ** z - 1 - y,
    "{s}": "abc"[(x + y) % 3],
    "{r}": "",
    "{quadkey}": tileQuadkey(x, y, z),
  };
  return template.replace(/\[[^\]]+\]|\{[^}]+\}/g, (token) =>
    String(replacements[token] ?? token),
  );
}

export function validateMapTemplate(url: string) {
  const hasCoordinates =
    ["{x}", "{y}", "{z}"].every((s) => url.includes(s)) ||
    (["[X]", "[Y]"].every((s) => url.includes(s)) &&
      ["[Z]", "[17-Z]", "[Z+1]"].some((s) => url.includes(s))) ||
    /\[RES\]|\{quadkey\}/.test(url);
  if (!hasCoordinates)
    throw new Error(
      "L’URL doit contenir {x}, {y}, {z}, les marqueurs natifs [X], [Y], [Z] ou [RES].",
    );
  const parsed = new URL(tileUrl(url, 0, 0, 0));
  if (!["https:", "http:"].includes(parsed.protocol))
    throw new Error("Utilisez une URL HTTP ou HTTPS.");
}

/** Tile coordinates for the same ellipsoidal Mercator used by native Yandex maps. */
export function geographicTile(
  lat: number,
  lng: number,
  zoom: number,
  projection?: BasemapProvider["projection"],
) {
  const n = 2 ** zoom,
    phi = (Math.max(-85.084, Math.min(85.084, lat)) * Math.PI) / 180;
  let mercator = Math.log(Math.tan(Math.PI / 4 + phi / 2));
  if (projection === "ellipsoidal") {
    const e = 0.08181919084262149;
    mercator -=
      (e / 2) * Math.log((1 + e * Math.sin(phi)) / (1 - e * Math.sin(phi)));
  }
  return [((lng + 180) / 360) * n, ((1 - mercator / Math.PI) / 2) * n];
}
