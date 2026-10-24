import { test } from "node:test";
import assert from "node:assert/strict";
import { newProject, newSource, normalizeProject } from "../src/lib/project";
import {
  antennaCaptureDistance,
  antennaLocalToGeo,
  editAntennaCoordinate,
  formatAntennaCoordinate,
  moveAntennas,
  snapAntenna,
} from "../src/lib/antenna-locations";
import {
  mercatorGeo,
  mercatorPoint,
  nativeLocal,
  nativeScale,
} from "../src/lib/native-coordinates";
import { importXemf } from "../src/lib/import-xemf";
import { readFileSync } from "node:fs";

function fixture() {
  const p = newProject();
  p.sources = [0, 1, 2].map((i) => ({
    ...newSource(p.origin, `A${i}`),
    id: `s${i}`,
    height_m: 20 + i,
  }));
  return p;
}
test("geographic labels follow native degree padding and hemisphere-specific precision", () => {
  assert.equal(formatAntennaCoordinate(5.345, "decimal", true), "05.345000°");
  assert.equal(
    formatAntennaCoordinate(-4.025, "decimal", false),
    "W04.025000°",
  );
  assert.equal(formatAntennaCoordinate(-5.5, "dm", true), "S05°30.0000'");
  assert.equal(formatAntennaCoordinate(5.5, "dms", true), "05°30'00.0\"");
  assert.equal(formatAntennaCoordinate(-5.5, "dms", true), "S05°30'00.00\"");
  assert.equal(formatAntennaCoordinate(-4.5, "dms", false), "W04°30'00\"");
});
test("moving selected antennas keeps unselected source identity, technical parameters and geographic target", () => {
  const p = fixture(),
    geo: [number, number] = [5.346, -4.024];
  const next = moveAntennas(p, ["s0", "s2"], geo);
  assert.equal(next.sources[1], p.sources[1]);
  for (const i of [0, 2]) {
    assert.deepEqual([next.sources[i].lat, next.sources[i].lng], geo);
    assert.deepEqual(
      [next.sources[i].x_m, next.sources[i].y_m],
      nativeLocal(...geo, p.origin, true),
    );
    assert.equal(next.sources[i].height_m, p.sources[i].height_m);
  }
  assert.deepEqual(moveAntennas(next, ["s0"], p.origin).sources[0].x_m, 0);
  assert.equal(p.sources[0].lat, p.origin[0]);
});
test("X/Y edits use native scaled WM conversion and group Single precision", () => {
  const p = fixture(),
    value = 123.456789;
  const next = editAntennaCoordinate(p, ["s0", "s2"], "x_m", value);
  const [ox, oy] = mercatorPoint(p.origin),
    [wx, wy] = mercatorPoint([next.sources[0].lat, next.sources[0].lng]);
  assert.ok(
    Math.abs(wx - ox - Math.fround(value) / nativeScale(p.origin)) < 1e-8,
  );
  assert.ok(Math.abs(wy - oy) < 1e-8);
  assert.equal(next.sources[0].x_m, Math.fround(value));
  assert.equal(
    editAntennaCoordinate(p, ["s0"], "x_m", value).sources[0].x_m,
    value,
  );
  assert.equal(next.sources[1], p.sources[1]);
  assert.throws(() => editAntennaCoordinate(p, ["s0"], "y_m", Infinity));
  assert.throws(() => moveAntennas(p, ["s0"], [86, 0]));
});
test("capture follows native rectangular WM threshold and first other antenna", () => {
  const p = fixture();
  p.sources[1] = {
    ...p.sources[1],
    ...(() => {
      const [lat, lng] = antennaLocalToGeo(p, 10, 10);
      return { lat, lng };
    })(),
  };
  p.sources[2] = {
    ...p.sources[2],
    lat: p.sources[1].lat,
    lng: p.sources[1].lng,
  };
  const target: [number, number] = [p.sources[1].lat, p.sources[1].lng],
    wm = mercatorPoint(target);
  assert.deepEqual(
    snapAntenna(p, "s0", mercatorGeo([wm[0] + 1.4, wm[1] + 1.4])),
    target,
  );
  const outside = mercatorGeo([wm[0] + 1.6, wm[1]]);
  assert.deepEqual(snapAntenna(p, "s0", outside), outside);
  p.settings.antenna_capture_distance = 1;
  assert.deepEqual(
    snapAntenna(p, "s0", mercatorGeo([wm[0] + 1, wm[1]])),
    mercatorGeo([wm[0] + 1, wm[1]]),
  );
});
test("capture setting defaults, bounds and project round trip survive native import", () => {
  const p = fixture();
  assert.equal(antennaCaptureDistance(p), 15);
  p.settings.antenna_capture_distance = 30;
  assert.equal(
    antennaCaptureDistance(normalizeProject(JSON.parse(JSON.stringify(p)))),
    30,
  );
  p.settings.antenna_capture_distance = NaN;
  assert.equal(antennaCaptureDistance(p), 15);
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const imported = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "sample.xemf",
  );
  assert.equal(antennaCaptureDistance(imported), 15);
});
