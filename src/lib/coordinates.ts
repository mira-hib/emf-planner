export type CoordinateFormat = "decimal" | "dm" | "dms";
export function formatCoordinate(
  value: number,
  format: CoordinateFormat,
  latitude: boolean,
): string {
  if (format === "decimal") return String(Number(value.toFixed(7)));
  const hemisphere = latitude ? (value < 0 ? "S" : "N") : value < 0 ? "W" : "E";
  if (format === "dm") {
    const minutes = Math.round(Math.abs(value) * 60 * 10000) / 10000,
      degrees = Math.floor(minutes / 60);
    return `${degrees}° ${(minutes - degrees * 60).toFixed(4)}′ ${hemisphere}`;
  }
  const seconds = Math.round(Math.abs(value) * 3600 * 100) / 100,
    degrees = Math.floor(seconds / 3600),
    minutes = Math.floor((seconds - degrees * 3600) / 60);
  return `${degrees}° ${minutes}′ ${(seconds - degrees * 3600 - minutes * 60).toFixed(2)}″ ${hemisphere}`;
}
export function parseCoordinate(text: string, latitude: boolean): number {
  const value = text.trim().replace(/,/g, ".").replace(/−/g, "-").toUpperCase();
  const hemispheres = value.match(/[NSEW]/g) ?? [];
  if (
    hemispheres.length > 1 ||
    (hemispheres.length === 1 && !/^[NSEW]|[NSEW]$/.test(value))
  )
    throw new Error(
      "Coordonnée ambiguë : indiquez un seul hémisphère en début ou en fin.",
    );
  const hemisphere = hemispheres[0];
  if (
    hemisphere &&
    (latitude ? /^[EW]$/.test(hemisphere) : /^[NS]$/.test(hemisphere))
  )
    throw new Error("Hémisphère incorrect pour cette coordonnée.");
  const tokens = value
    .replace(/[NSEW°′'″":]/g, " ")
    .trim()
    .split(/\s+/);
  if (
    tokens.length > 3 ||
    !tokens.length ||
    tokens.some((t) => !/^[-+]?\d*(?:\.\d*)?$/.test(t) || !t.length)
  )
    throw new Error("Coordonnée invalide.");
  const parts = tokens.map(Number);
  if (parts[0] < 0 && (hemisphere === "N" || hemisphere === "E"))
    throw new Error(
      "Le signe et l’hémisphère de la coordonnée se contredisent.",
    );
  if (parts.slice(1).some((v) => v < 0 || v >= 60))
    throw new Error(
      "Les minutes et secondes doivent être comprises entre 0 et 60.",
    );
  const sign =
    hemisphere === "S" || hemisphere === "W" || parts[0] < 0 ? -1 : 1;
  const result =
    sign * (Math.abs(parts[0]) + (parts[1] ?? 0) / 60 + (parts[2] ?? 0) / 3600);
  if (!Number.isFinite(result) || Math.abs(result) > (latitude ? 85 : 180))
    throw new Error("Coordonnée hors limites.");
  return result;
}
