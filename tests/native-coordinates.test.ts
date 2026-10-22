import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import {
  mercatorGeo,
  mercatorPoint,
  nativeLocal,
  nativeScale,
} from "../src/lib/native-coordinates.ts";
import { readNrbf } from "../src/lib/nrbf.ts";
import { importXemf } from "../src/lib/import-xemf.ts";

test("native local coordinates retain Single distance and azimuth before rounding", () => {
  const origin: [number, number] = [50, 14];
  assert.deepEqual(nativeLocal(50, 14, origin), [0, 0]);
  const target: [number, number] = [50.001, 14.002];
  const lat1 = (origin[0] * Math.PI) / 180;
  const lat2 = (target[0] * Math.PI) / 180;
  const delta = ((target[1] - origin[1]) * Math.PI) / 180;
  const east = Math.cos(lat2) * Math.sin(delta);
  const north =
    Math.cos(lat1) * Math.sin(lat2) -
    Math.sin(lat1) * Math.cos(lat2) * Math.cos(delta);
  const distance = Math.fround(
    6378137 *
      Math.atan2(
        Math.hypot(east, north),
        Math.sin(lat1) * Math.sin(lat2) +
          Math.cos(lat1) * Math.cos(lat2) * Math.cos(delta),
      ),
  );
  const azimuth = Math.fround(Math.atan2(east, north));
  const result = nativeLocal(...target, origin);
  assert.ok(Math.abs(result[0] - distance * Math.sin(azimuth)) < 1e-5);
  assert.ok(Math.abs(result[1] - distance * Math.cos(azimuth)) < 1e-5);
  assert.deepEqual(nativeLocal(...target, origin, true), [143.1, 111.3]);
  const west = nativeLocal(50, 13.999, origin);
  assert.ok(west[0] < 0);
});

test("Web Mercator helpers and one-meter native scale follow CalcCoords", () => {
  for (const point of [
    [50, 14],
    [-20, -30],
    [5.345, -4.025],
  ] as [number, number][]) {
    const wm = mercatorPoint(point);
    const geo = mercatorGeo(wm);
    assert.ok(Math.abs(geo[0] - point[0]) < 1e-12);
    assert.ok(Math.abs(geo[1] - point[1]) < 1e-12);
    assert.ok(
      Math.abs(nativeScale(point) - Math.cos((point[0] * Math.PI) / 180)) <
        1e-7,
    );
    assert.equal(
      nativeScale(point),
      nativeLocal(...mercatorGeo([wm[0] + 1, wm[1]]), point)[0],
    );
  }
});

test("native decimal rounding uses even midpoints rather than Math.round", () => {
  const origin: [number, number] = [50, 14];
  const latitudeAt = (meters: number) =>
    origin[0] + ((meters / 6378137) * 180) / Math.PI;
  assert.equal(nativeLocal(latitudeAt(0.25), 14, origin)[1], 0.25);
  assert.equal(nativeLocal(latitudeAt(0.25), 14, origin, true)[1], 0.2);
  assert.equal(nativeLocal(latitudeAt(0.75), 14, origin)[1], 0.75);
  assert.equal(nativeLocal(latitudeAt(0.75), 14, origin, true)[1], 0.8);
});

test("original XEMF imported geographic locations reproduce saved native rounded TX coordinates", () => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const buffer = bytes.buffer.slice(
    bytes.byteOffset,
    bytes.byteOffset + bytes.byteLength,
  ) as ArrayBuffer;
  const project = importXemf(buffer, "original-sample.xemf");
  const { root, deref } = readNrbf(buffer);
  const tx = deref(root.TX) as unknown[];
  assert.equal(project.sources.length, tx.length);
  assert.ok(tx.length > 0);
  for (let index = 0; index < tx.length; index++) {
    const original = deref(tx[index]) as Record<string, unknown>;
    const coord = deref(original.CoordM) as { Xm: number; Ym: number };
    const source = project.sources[index];
    const result = nativeLocal(source.lat, source.lng, project.origin, true);
    assert.equal(result[0], coord.Xm, `TX ${index + 1} X`);
    assert.equal(result[1], coord.Ym, `TX ${index + 1} Y`);
  }
});
