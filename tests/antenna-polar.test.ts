import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { createElement } from "react";
import { renderToStaticMarkup } from "react-dom/server";
import AntennaPolar from "../src/components/AntennaPolar.tsx";
import {
  antennaPolarPath,
  POLAR_RADIUS,
  polarPoint,
  polarRadius,
} from "../src/lib/antenna-polar.ts";
import { importXemf } from "../src/lib/import-xemf.ts";

test("antenna preview uses the native 30 dB radial scale and clockwise local angles", () => {
  assert.equal(polarRadius(0), POLAR_RADIUS);
  assert.equal(polarRadius(3), 74.7);
  assert.ok(Math.abs(polarRadius(10) - (2 * POLAR_RADIUS) / 3) < 1e-12);
  assert.equal(polarRadius(30), 0);
  assert.equal(polarRadius(65), 0);
  assert.deepEqual(polarPoint(0, 0), [183, 100]);
  assert.deepEqual(polarPoint(90, 0), [100, 183]);
  assert.deepEqual(polarPoint(180, 30), [100, 100]);
});

test("Castelfranco preview retains all measured lobes and the electrical tilt already in DnV", () => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const project = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "Castelfranco Emilia TAV2.xemf",
  );
  const source = project.sources[0];
  assert.equal(source.antenna_model, "742265V02");
  assert.equal(source.pattern_h?.length, 360);
  assert.equal(source.pattern_v?.length, 360);
  assert.equal(source.electrical_downtilt_deg, 6);
  assert.equal(source.pattern_v?.indexOf(0), 6);
  const points = antennaPolarPath(source.pattern_v, source.beam_width_v_deg)
    .split(" ")
    .map((p) => p.slice(1).split(",").map(Number));
  assert.equal(points.length, 361);
  assert.deepEqual(points[6], polarPoint(6, 0));
  assert.deepEqual(points[90], [100, 100]);
  assert.deepEqual(points[300], polarPoint(300, source.pattern_v![300]));
  assert.deepEqual(points[360], points[0]);
  const html = renderToStaticMarkup(
    createElement(AntennaPolar, { source, both: true }),
  );
  assert.match(html, /data-plane="H"[^>]*fill="blue"/);
  assert.match(html, /data-plane="V"[^>]*fill="red"/);
  assert.match(html, /fill-opacity="0\.23529411764705882"/);
  assert.match(html, /data-loss-db="3"[^>]*stroke-dasharray="3 2"/);
  assert.match(html, /−30 dB au centre/);
  assert.doesNotMatch(html, /NaN|Infinity/);
});
