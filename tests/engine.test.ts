import test from "node:test";
import assert from "node:assert/strict";
import { existsSync, readFileSync } from "node:fs";
import { newProject, newSource, normalizeProject } from "../src/lib/project.ts";
import {
  calculatePoints,
  contours,
  exposureAt,
  horizontalGrid,
  limitAt,
} from "../src/lib/engine.ts";
import { importXemf } from "../src/lib/import-xemf.ts";
import { readNrbf } from "../src/lib/nrbf.ts";
import { parseMsi, writeMsi } from "../src/lib/antenna.ts";
import { importDxf, projectDxf } from "../src/lib/files.ts";
const sample = "tests/fixtures/original-sample.xemf";
test("frequency intervals use the original upper-bound convention", () => {
  const l = newProject().limits[0];
  l.rows = [
    { frequency: 400, value: 28, exponent: 0 },
    { frequency: 2000, value: 1.375, exponent: 0.5 },
    { frequency: 100000, value: 61, exponent: 0 },
  ];
  assert.equal(limitAt(l, 400), 28);
  assert.equal(limitAt(l, 900), 41.25);
  assert.equal(limitAt(l, 2100), 61);
});
test("DXF exports calculated contour vertices, height and layer", () => {
  const dxf = projectDxf(newProject(), [
    {
      level: 1,
      color: "#ff0000",
      height_m: 18,
      segments: [
        [
          [2, 3],
          [4, 5],
        ],
      ],
    },
  ]);
  assert.match(dxf, /0\r\nLINE\r\n8\r\nEMF_1_H18/);
  assert.match(dxf, /10\r\n2\r\n20\r\n3\r\n30\r\n18\r\n11\r\n4\r\n21\r\n5/);
});
test("isotropic free-space analytic field, inverse distance and inactive exclusion", () => {
  const p = newProject(),
    s = {
      ...newSource(p.origin, "Isotropic"),
      power_w: 10,
      gain_dbi: 0,
      height_m: 0,
      pattern_h: Array(360).fill(0),
      pattern_v: Array(360).fill(0),
    };
  p.sources = [s];
  const e = exposureAt(p, 10, 0, 0);
  assert.ok(Math.abs(e.total_field - Math.sqrt(300) / 10) < 1e-12);
  assert.ok(
    Math.abs(exposureAt(p, 20, 0, 0).total_field - e.total_field / 2) < 1e-12,
  );
  s.active = false;
  assert.equal(exposureAt(p, 10, 0, 0).total_field, 0);
});
test("power reduction and per-limit factors act on power, not amplitude", () => {
  const p = newProject(),
    s = {
      ...newSource(p.origin, "Isotropic"),
      power_w: 10,
      gain_dbi: 0,
      height_m: 0,
      pattern_h: Array(360).fill(0),
      pattern_v: Array(360).fill(0),
    };
  p.sources = [s];
  const e = exposureAt(p, 10, 0, 0).total_field;
  s.duty_cycle = 0.25;
  assert.ok(Math.abs(exposureAt(p, 10, 0, 0).total_field - e / 2) < 1e-12);
});
test("an active opaque building screens a ray below its roof", () => {
  const p = newProject();
  p.sources = [
    {
      ...newSource(p.origin, "TX"),
      height_m: 1,
      pattern_h: Array(360).fill(0),
      pattern_v: Array(360).fill(0),
    },
  ];
  p.buildings = [
    {
      id: "b",
      name: "B",
      height_m: 5,
      foundation_m: 0,
      attenuation_db: 20,
      active: true,
      points: [
        [4, -1],
        [6, -1],
        [6, 1],
        [4, 1],
      ],
    },
  ];
  assert.ok(exposureAt(p, 10, 0, 1).total_field > 0);
  p.settings.buildings_mode = "screen";
  assert.equal(exposureAt(p, 10, 0, 1).total_field, 0);
});
test("MSI import/export preserves the 360 measured attenuation samples", () => {
  const h = Array.from({ length: 360 }, (_, i) => Math.min(i, 360 - i) / 10),
    v = h.map((n) => n * 2),
    m = parseMsi(writeMsi("Test", 16, h, v));
  assert.equal(m.name, "Test");
  assert.equal(m.gain, 16);
  assert.deepEqual(m.horizontal, h);
  assert.deepEqual(m.vertical, v);
  assert.throws(() => parseMsi("NAME Broken\nHORIZONTAL 360\n0 1"));
});
test("closed DXF polylines round-trip as building footprints", () => {
  const p = newProject();
  p.buildings = [
    {
      id: "1",
      name: "B",
      height_m: 10,
      foundation_m: 3,
      attenuation_db: 20,
      active: true,
      points: [
        [1, 2],
        [10, 2],
        [10, 20],
        [1, 20],
      ],
    },
  ];
  const restored = importDxf(newProject(), projectDxf(p));
  assert.deepEqual(restored.buildings[0].points, p.buildings[0].points);
  assert.equal(restored.buildings[0].foundation_m, 3);
});
test("grid guard prevents unbounded work and contours use the calculated ratio", () => {
  const p = newProject();
  assert.throws(() => horizontalGrid(p, 1.5, 0.01), /Grille/);
  const c = contours(
    {
      width: 2,
      height: 2,
      x0: 0,
      y0: 0,
      dx: 10,
      dy: 10,
      height_m: 1.5,
      fields: [0, 0, 0, 0],
      values: [0, 2, 0, 2],
    },
    1,
    "red",
  );
  assert.deepEqual(c.segments, [
    [
      [5, 0],
      [5, 10],
    ],
  ]);
});
test("legacy JSON projects are upgraded without dropping source parameters", () => {
  const p = newProject();
  p.sources = [newSource(p.origin, "Legacy")];
  const old = JSON.parse(JSON.stringify(p));
  delete old.settings;
  delete old.origin;
  delete old.control_points;
  const restored = normalizeProject(old);
  assert.equal(restored.sources[0].power_w, 10);
  assert.equal(restored.control_points.length, 0);
  assert.equal(restored.settings.reflection, 1);
});
test(
  "original sample: 15 captured reference fields and exposure percentages",
  { skip: !existsSync(sample) },
  () => {
    const bytes = readFileSync(sample),
      p = importXemf(
        bytes.buffer.slice(
          bytes.byteOffset,
          bytes.byteOffset + bytes.byteLength,
        ),
        "Castelfranco",
      );
    const references = JSON.parse(
      readFileSync("tests/fixtures/original-results.json", "utf8"),
    ).point_calculation.rows;
    const rows = calculatePoints(p);
    assert.equal(rows.length, 15);
    assert.equal(p.sources.length, 9);
    assert.equal(p.horizontal_cuts[0].height_m, 18);
    assert.equal(p.vertical_cuts[0].azimuth_deg, 60);
    rows.forEach((row, i) => {
      assert.ok(
        Math.abs(row.total_field - references[i][5]) < 1.1e-6,
        `${row.name}: ${row.total_field} vs ${references[i][5]}`,
      );
      assert.ok(
        Math.abs(row.exposure_ratio * 100 - references[i][4]) < 1.1e-6,
        `exposure ${row.name}`,
      );
    });
    assert.throws(
      () =>
        readNrbf(
          bytes.buffer.slice(
            bytes.byteOffset,
            bytes.byteOffset + bytes.byteLength - 10,
          ),
        ),
      /tronqué|inattendue/,
    );
  },
);
