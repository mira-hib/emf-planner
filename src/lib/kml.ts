import type { PlannerProject } from "../types";
import { uid } from "./project";
import { terrainHeight, toLocal } from "./geometry";
export function importKmlBuildings(
  project: PlannerProject,
  text: string,
): PlannerProject {
  const document = new DOMParser().parseFromString(text, "application/xml");
  if (document.getElementsByTagName("parsererror").length)
    throw new Error("Le document KML est invalide.");
  const buildings = [...project.buildings];
  for (const placemark of document.getElementsByTagNameNS("*", "Placemark")) {
    const name = placemark
      .getElementsByTagNameNS("*", "name")[0]
      ?.textContent?.trim();
    const data = new Map(
      [...placemark.getElementsByTagNameNS("*", "Data")].map((d) => [
        d.getAttribute("name"),
        d.textContent?.trim(),
      ]),
    );
    const h = Number(data.get("height")),
      levels = Number(data.get("building:levels"));
    for (const polygon of placemark.getElementsByTagNameNS("*", "Polygon")) {
      if (polygon.getElementsByTagNameNS("*", "innerBoundaryIs").length)
        throw new Error(
          "Les polygones KML avec des trous ne sont pas encore pris en charge.",
        );
      const ring =
        polygon.getElementsByTagNameNS("*", "outerBoundaryIs")[0] ?? polygon;
      const coordinates = ring
        .getElementsByTagNameNS("*", "coordinates")[0]
        ?.textContent?.trim();
      if (!coordinates) continue;
      const geo = coordinates.split(/\s+/).map((s) => s.split(",").map(Number));
      if (
        geo.length > 20000 ||
        geo.some(
          (c) =>
            !Number.isFinite(c[0] + c[1]) ||
            Math.abs(c[0]) > 180 ||
            Math.abs(c[1]) > 85,
        )
      )
        throw new Error("Coordonnées KML invalides ou contour trop détaillé.");
      const points = geo.map(([lng, lat]) => toLocal(lat, lng, project.origin));
      if (
        points.length > 1 &&
        Math.hypot(
          points[0][0] - points[points.length - 1][0],
          points[0][1] - points[points.length - 1][1],
        ) < 0.001
      )
        points.pop();
      if (points.length < 3) continue;
      buildings.push({
        id: uid(),
        name: name || `Bâtiment ${buildings.length + 1}`,
        height_m: h > 0 ? h : levels > 0 ? levels * 3 : 10,
        foundation_m: terrainHeight(project, ...points[0]),
        points,
        attenuation_db: 20,
        active: true,
      });
    }
  }
  if (buildings.length === project.buildings.length)
    throw new Error("Aucun polygone de bâtiment trouvé dans le KML.");
  return { ...project, buildings };
}
