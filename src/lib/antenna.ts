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
  const gain =
    Number(lines.find((l) => /^GAIN\s/i.test(l))?.split(/\s+/)[1]) || 0;
  function plane(key: string) {
    const start = lines.findIndex((l) => l.toUpperCase().startsWith(key + " "));
    if (start < 0) throw new Error(`Plan ${key} absent du fichier MSI.`);
    const n = Number(lines[start].split(/\s+/)[1]);
    if (!Number.isInteger(n) || n < 2 || n > 10000)
      throw new Error("Nombre de points MSI invalide.");
    const points = lines
      .slice(start + 1, start + 1 + n)
      .map((l) => l.split(/\s+/).map(Number));
    if (
      points.length !== n ||
      points.some((p) => p.length < 2 || !p.every(Number.isFinite))
    )
      throw new Error("Diagramme MSI tronqué ou invalide.");
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
