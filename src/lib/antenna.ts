import type { RadiatingSource } from "../types";
import { patternAmplitude } from "./engine";

/** Original MSI files use Windows-1251; retain modern UTF-8 files as well. */
export function decodeMsi(bytes: Uint8Array): string {
  try {
    return new TextDecoder("utf-8", { fatal: true }).decode(bytes);
  } catch {
    return new TextDecoder("windows-1251").decode(bytes);
  }
}

// DnV is already the plotted/measured pattern. ElectUg is descriptive metadata
// in the original project, not a second rotation to apply to these samples.
export function sourceAntenna(source?: RadiatingSource) {
  const samples = (values: number[] | undefined, width: number) =>
    values?.length === 360
      ? [...values]
      : Array.from(
          { length: 360 },
          (_, i) => -20 * Math.log10(patternAmplitude(undefined, i, width)),
        );
  return {
    name: source?.antenna_model || "Nouveau diagramme",
    gain: source?.gain_dbi ?? 16,
    horizontal: samples(source?.pattern_h, source?.beam_width_h_deg ?? 65),
    vertical: samples(source?.pattern_v, source?.beam_width_v_deg ?? 10),
  };
}

export function parseMsi(text: string): {
  name: string;
  gain: number;
  horizontal: number[];
  vertical: number[];
} {
  const lines = text
    .replace(/\r/g, "")
    .split("\n")
    .map((l) => l.trim())
    .filter(Boolean);
  const name =
    lines.find((l) => /^NAME\s/i.test(l))?.replace(/^NAME\s+/i, "") ??
    "Antenne importée";
  const gainLine = lines.find((l) => /^GAIN\s/i.test(l));
  const gainMatch = gainLine?.match(
    /^GAIN\s+([+-]?(?:\d+(?:[.,]\d*)?|[.,]\d+)(?:e[+-]?\d+)?)\s*(?:\(?\s*(dBi|dBd)\s*\)?)?\s*$/i,
  );
  if (gainLine && !gainMatch) throw new Error("Gain MSI invalide.");
  const gain = gainMatch
    ? Number(gainMatch[1].replace(",", ".")) +
      (gainMatch[2]?.toLowerCase() === "dbd" ? 2.15 : 0)
    : 0;
  if (!Number.isFinite(gain)) throw new Error("Gain MSI invalide.");
  function plane(key: string) {
    const start = lines.findIndex((l) =>
      new RegExp(`^${key}\\s+`, "i").test(l),
    );
    if (start < 0) throw new Error(`Plan ${key} absent du fichier MSI.`);
    const n = Number(lines[start].split(/\s+/)[1]);
    if (!Number.isInteger(n) || n < 2 || n > 10000)
      throw new Error("Nombre de points MSI invalide.");
    const rawPoints = lines
      .slice(start + 1, start + 1 + n)
      .map((l) => l.split(/[\s,;:]+/).map(Number));
    if (
      rawPoints.length !== n ||
      rawPoints.some((p) => p.length !== 2 || !p.every(Number.isFinite))
    )
      throw new Error("Diagramme MSI tronqué ou invalide.");
    const unique = new Map<number, number>();
    for (const [angle, value] of rawPoints) {
      const normalizedAngle = ((angle % 360) + 360) % 360;
      const loss = Math.abs(value);
      if (unique.has(normalizedAngle) && unique.get(normalizedAngle) !== loss)
        throw new Error("Plusieurs atténuations pour le même angle MSI.");
      unique.set(normalizedAngle, loss);
    }
    const points = [...unique.entries()];
    if (points.length < 2) throw new Error("Diagramme MSI incomplet.");
    points.sort((a, b) => a[0] - b[0]);
    return Array.from({ length: 360 }, (_, angle) => {
      let low = points[points.length - 1],
        high = [points[0][0] + 360, points[0][1]];
      for (let i = 0; i < points.length - 1; i++)
        if (angle >= points[i][0] && angle <= points[i + 1][0]) {
          low = points[i];
          high = points[i + 1];
          break;
        }
      const a = angle < low[0] ? angle + 360 : angle,
        t = (a - low[0]) / (high[0] - low[0]);
      return low[1] + t * (high[1] - low[1]);
    });
  }
  return {
    name,
    gain,
    horizontal: plane("HORIZONTAL"),
    vertical: plane("VERTICAL"),
  };
}
export function writeMsi(
  name: string,
  gain: number,
  horizontal: number[],
  vertical: number[],
): string {
  return [
    `NAME ${name.replace(/[\r\n]/g, " ")}`,
    `GAIN ${gain} dBi`,
    "HORIZONTAL 360",
    ...horizontal.map((v, i) => `${i} ${v.toFixed(4)}`),
    "VERTICAL 360",
    ...vertical.map((v, i) => `${i} ${v.toFixed(4)}`),
  ].join("\r\n");
}
