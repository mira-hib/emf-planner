// UC_PRTO.PB1_Paint uses a linear dB radius with -30 dB at the centre.
// These are the antenna's local planes: azimuth and mechanical tilt are not
// applied here, and imported DnV samples already include electrical tilt.
export const POLAR_LOSS_DB = 30;
export const POLAR_RADIUS = 83;

export function polarRadius(lossDb: number): number {
  return Math.max(0, 1 - lossDb / POLAR_LOSS_DB) * POLAR_RADIUS;
}

export function polarPoint(angleDeg: number, lossDb: number): [number, number] {
  const angle = (angleDeg * Math.PI) / 180;
  const radius = polarRadius(lossDb);
  return [100 + radius * Math.cos(angle), 100 + radius * Math.sin(angle)];
}

export function antennaPolarPath(
  values: number[] | undefined,
  beamWidth = 65,
): string {
  return Array.from({ length: 361 }, (_, i) => {
    const sample = values?.[i % 360];
    const loss =
      sample !== undefined && Number.isFinite(sample)
        ? sample
        : Math.min(
            POLAR_LOSS_DB,
            12 * (Math.min(i, 360 - i) / (beamWidth || 65)) ** 2,
          );
    const [x, y] = polarPoint(i % 360, loss);
    return `${i ? "L" : "M"}${x},${y}`;
  }).join(" ");
}
