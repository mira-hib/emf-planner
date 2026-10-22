// CalcCoords.cs:352-406, 533-600 and 652-657 (EMFPlanner 7.1).
// These helpers deliberately keep the native spherical inverse calculation
// separate from the map's scaled Web Mercator coordinate system.
const RADIUS = 6378137;
const RAD = Math.PI / 180;
type Pair = readonly [number, number];

export function mercatorPoint([lat, lng]: Pair): [number, number] {
  return [
    lng * RADIUS * RAD,
    Math.log(Math.tan(lat * RAD * 0.5 + Math.PI / 4)) * RADIUS,
  ];
}

export function mercatorGeo([x, y]: Pair): [number, number] {
  return [
    (2 * Math.atan(Math.exp(y / RADIUS)) - Math.PI / 2) / RAD,
    x / 111319.49079327358,
  ];
}

function roundTenthEven(value: number): number {
  const scaled = value * 10;
  const lower = Math.floor(scaled);
  const fraction = scaled - lower;
  const rounded =
    fraction === 0.5
      ? lower % 2 === 0
        ? lower
        : lower + 1
      : Math.round(scaled);
  return rounded / 10;
}

export function nativeLocal(
  lat: number,
  lng: number,
  origin: Pair,
  rounded = false,
): [number, number] {
  if (lat === origin[0] && lng === origin[1]) return [0, 0];
  const lat1 = origin[0] * RAD;
  const lat2 = lat * RAD;
  const longitudeDelta = lng * RAD - origin[1] * RAD;
  // OGZ uses a=b=6378.137 km and flattening=0. Its inverse-geodesic
  // iterations therefore reduce to these spherical terms.
  const tan1 = Math.tan(lat1),
    tan2 = Math.tan(lat2);
  const cos1 = 1 / Math.sqrt(1 + tan1 * tan1);
  const cos2 = 1 / Math.sqrt(1 + tan2 * tan2);
  const sin1 = tan1 * cos1,
    sin2 = tan2 * cos2;
  const east = cos2 * Math.sin(longitudeDelta);
  const north = cos1 * sin2 - sin1 * cos2 * Math.cos(longitudeDelta);
  const sinArc = Math.sqrt(east * east + north * north);
  const cosArc = sin1 * sin2 + cos1 * cos2 * Math.cos(longitudeDelta);
  const distance = Math.fround(6378.137 * Math.atan2(sinArc, cosArc) * 1000);
  let azimuth = Math.fround(Math.atan2(east, north));
  if (azimuth < 0) azimuth = Math.fround(Math.PI * 2 + azimuth);
  const x = distance * Math.sin(azimuth);
  const y = distance * Math.cos(azimuth);
  return rounded ? [roundTenthEven(x), roundTenthEven(y)] : [x, y];
}

export function nativeScale(origin: Pair): number {
  const [x, y] = mercatorPoint(origin);
  const [lat, lng] = mercatorGeo([x + 1, y]);
  return nativeLocal(lat, lng, origin)[0];
}
