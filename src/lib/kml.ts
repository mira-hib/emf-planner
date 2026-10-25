import type { PlannerProject } from "../types";
import { uid } from "./project";
import { toLocal } from "./geometry";

/** KML XML may carry a UTF-16 BOM; never decode it as UTF-8 replacement characters. */
export function decodeKml(bytes: Uint8Array): string {
  const encoding =
    bytes[0] === 0xff && bytes[1] === 0xfe
      ? "utf-16le"
      : bytes[0] === 0xfe && bytes[1] === 0xff
        ? "utf-16be"
        : "utf-8";
  try {
    return new TextDecoder(encoding, { fatal: true }).decode(bytes);
  } catch {
    throw new Error("Encodage KML invalide (UTF-8 ou UTF-16 attendu).");
  }
}

function heightValue(text: string): number {
  const value = Number(text.trim().replace(",", "."));
  if (!text.trim() || !Number.isFinite(value) || value <= 0 || value > 9999)
    throw new Error(
      `Hauteur KML invalide : « ${text} » (0 < hauteur ≤ 9999 m).`,
    );
  return value;
}
export function importKmlBuildings(
  project: PlannerProject,
  text: string,
): PlannerProject {
  if (text.length > 50_000_000)
    throw new Error("Le fichier KML dépasse 50 Mo.");
  if (/<!DOCTYPE|<!ENTITY/i.test(text)) throw new Error("Les déclarations d’entités XML ne sont pas autorisées.");
  let depth = 0;
  for (const match of text.matchAll(/<\/?[A-Za-z_][^>]*>/g)) {
    if (match[0].startsWith("</")) depth--;
    else if (!match[0].endsWith("/>")) depth++;
    if (depth > 256) throw new Error("La structure XML dépasse 256 niveaux d’imbrication.");
  }
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
    for (const polygon of placemark.getElementsByTagNameNS("*", "Polygon")) {
      const fields = name?.split(";");
      const height =
        fields?.length === 2
          ? heightValue(fields[0])
          : data.has("height")
            ? heightValue(data.get("height") ?? "")
            : data.has("building:levels")
              ? heightValue(
                  String(heightValue(data.get("building:levels") ?? "") * 3),
                )
              : 10;
      const label = fields?.length === 2 ? fields[1] : name;
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
      const geo = coordinates.split(/\s+/).map((s) => {
        const parts = s.split(",");
        if (
          parts.length < 2 ||
          parts.length > 3 ||
          parts.some((v) => !v.trim())
        )
          throw new Error("Coordonnées KML incomplètes.");
        return parts.map(Number);
      });
      if (
        geo.length > 20000 ||
        geo.some(
          (c) =>
            !c.every(Number.isFinite) ||
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
      const area = points.reduce((sum, p, i) => {
        const q = points[(i + 1) % points.length];
        return sum + p[0] * q[1] - q[0] * p[1];
      }, 0);
      if (Math.abs(area) < 1e-8)
        throw new Error(`Contour KML dégénéré : ${name ?? "sans nom"}.`);
      if (buildings.length >= 10000)
        throw new Error("Import limité à 10 000 bâtiments.");
      buildings.push({
        id: uid(),
        name: label || `Bâtiment ${buildings.length + 1}`,
        height_m: height,
        foundation_m: 0,
        points,
        attenuation_db: 20,
        active: true,
        visible: true,
      });
    }
  }
  if (buildings.length === project.buildings.length)
    throw new Error("Aucun polygone de bâtiment trouvé dans le KML.");
  return { ...project, buildings };
}
