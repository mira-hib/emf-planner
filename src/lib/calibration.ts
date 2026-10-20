export interface CalibrationResult {
  scale: number;
  rotation: number;
  offsetX: number;
  offsetY: number;
}

export function calibrateTwoLines(
  mapP1: [number, number],
  mapP2: [number, number],
  realP1: [number, number],
  realP2: [number, number],
): CalibrationResult {
  const mdx = mapP2[0] - mapP1[0],
    mdy = mapP2[1] - mapP1[1];
  const rdx = realP2[0] - realP1[0],
    rdy = realP2[1] - realP1[1];
  const mapLen = Math.hypot(mdx, mdy),
    realLen = Math.hypot(rdx, rdy);
  if (mapLen < 1e-9 || realLen < 1e-9) throw new Error("Les segments sont trop courts.");
  const scale = realLen / mapLen;
  const mapAngle = Math.atan2(mdy, mdx);
  const realAngle = Math.atan2(rdy, rdx);
  const rotation = realAngle - mapAngle;
  const cos = Math.cos(rotation),
    sin = Math.sin(rotation);
  const offsetX = realP1[0] - scale * (cos * mapP1[0] - sin * mapP1[1]);
  const offsetY = realP1[1] - scale * (sin * mapP1[0] + cos * mapP1[1]);
  return { scale, rotation, offsetX, offsetY };
}

export function calibrateLineAndPoint(
  mapP1: [number, number],
  mapP2: [number, number],
  realP1: [number, number],
  realPoint: [number, number],
  mapPoint: [number, number],
): CalibrationResult {
  const mdx = mapP2[0] - mapP1[0],
    mdy = mapP2[1] - mapP1[1];
  const mapLen = Math.hypot(mdx, mdy);
  const mpx = mapPoint[0] - mapP1[0],
    mpy = mapPoint[1] - mapP1[1];
  const rpx = realPoint[0] - realP1[0],
    rpy = realPoint[1] - realP1[1];
  const realLen = Math.hypot(rpx, rpy);
  const mapDist = Math.hypot(mpx, mpy);
  if (mapLen < 1e-9 || mapDist < 1e-9) throw new Error("Segments trop courts.");
  const scale = realLen / mapDist;
  const mapAngle = Math.atan2(mpy, mpx);
  const realAngle = Math.atan2(rpy, rpx);
  const rotation = realAngle - mapAngle;
  const cos = Math.cos(rotation),
    sin = Math.sin(rotation);
  const offsetX = realP1[0] - scale * (cos * mapP1[0] - sin * mapP1[1]);
  const offsetY = realP1[1] - scale * (sin * mapP1[0] + cos * mapP1[1]);
  return { scale, rotation, offsetX, offsetY };
}

export function applyCalibration(
  result: CalibrationResult,
  points: [number, number][],
): [number, number][] {
  const { scale, rotation, offsetX, offsetY } = result;
  const cos = Math.cos(rotation),
    sin = Math.sin(rotation);
  return points.map(([x, y]) => [
    scale * (cos * x - sin * y) + offsetX,
    scale * (sin * x + cos * y) + offsetY,
  ]);
}
