import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { parseMsi, sourceAntenna, writeMsi } from "../src/lib/antenna.ts";
import { importXemf } from "../src/lib/import-xemf.ts";

const pattern = `HORIZONTAL 4
0 0
90 -10
180 -30
270 -20
VERTICAL 4
-180 -30
-90 -20
0 0
90 -10`;

test("MSI dBd gains convert to dBi and signed losses use the original magnitude convention", () => {
  const p = parseMsi(`NAME Test\nGAIN 10 (dBd)\n${pattern}`);
  assert.equal(p.gain, 12.15);
  assert.equal(p.horizontal.length, 360);
  assert.equal(p.vertical.length, 360);
  assert.deepEqual(p.horizontal, p.vertical);
  assert.equal(p.horizontal[90], 10);
  assert.equal(p.horizontal[180], 30);
  assert.equal(p.horizontal[270], 20);
  assert.equal(p.horizontal[315], 10);
  assert.equal(parseMsi(`GAIN -2 dBi\n${pattern}`).gain, -2);
  assert.equal(parseMsi(`GAIN 10,5 dBd\n${pattern}`).gain, 12.65);
  assert.equal(
    parseMsi(`GAIN 10 dBi\n${pattern.replace(/ /g, "\t")}`).horizontal[90],
    10,
  );
  assert.throws(() => parseMsi(`GAIN unknown dBd\n${pattern}`), /Gain MSI/);
});

test("MSI closed angular tables accept matching endpoints and reject conflicting angles", () => {
  const closed = `HORIZONTAL 3\n0 0\n180 30\n360 0\nVERTICAL 3\n0 0\n180 30\n360 0`;
  const p = parseMsi(closed);
  assert.equal(p.horizontal[90], 15);
  assert.equal(p.horizontal[270], 15);
  assert.throws(
    () => parseMsi(closed.replace("360 0", "360 1")),
    /même angle MSI/,
  );
  assert.throws(() => parseMsi(closed.replace("180 30", "180 NaN")));
});

test("editing an imported source preserves the measured vertical tilt and every original sample", () => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const project = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "Castelfranco Emilia TAV2.xemf",
  );
  const source = project.sources[0];
  const pattern = sourceAntenna(source);
  assert.equal(pattern.vertical.indexOf(0), 6);
  assert.equal(source.electrical_downtilt_deg, 6);
  assert.deepEqual(pattern.horizontal, source.pattern_h);
  assert.deepEqual(pattern.vertical, source.pattern_v);
  assert.notEqual(pattern.horizontal, source.pattern_h);
  assert.notEqual(pattern.vertical, source.pattern_v);
  const exported = parseMsi(
    writeMsi(pattern.name, pattern.gain, pattern.horizontal, pattern.vertical),
  );
  assert.equal(exported.vertical.indexOf(0), 6);
  assert.equal(exported.gain, source.gain_dbi);
  assert.ok(
    exported.vertical.every(
      (value, i) => Math.abs(value - source.pattern_v![i]) <= 0.00005,
    ),
  );
});
