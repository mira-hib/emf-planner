import test from "node:test";
import assert from "node:assert/strict";
import {
  addHorizontalHeights,
  addVerticalAngles,
  alignPointsToAzimuth,
  beamWidth,
  envelopePatterns,
  feederAttenuation,
  feederLoss,
  rangeValues,
  reorderGroup,
  symmetricPattern,
} from "../src/lib/editing.ts";
import { formatCoordinate, parseCoordinate } from "../src/lib/coordinates.ts";
import { newPoint, newProject, newSource } from "../src/lib/project.ts";
import feeders from "../src/data/feeders.json";
import { terrainHeight, terrainMesh } from "../src/lib/geometry.ts";
import { volumeSurface } from "../src/lib/volume.ts";
test("cut ranges reject zero steps and oversized work and retain existing heights", () => {
  assert.throws(() => rangeValues(0, 10, 0));
  assert.throws(() => rangeValues(0, 1000, 0.1));
  assert.deepEqual(rangeValues(1, 1.3, 0.1), [1, 1.1, 1.2, 1.3]);
  const p = newProject();
  p.horizontal_cuts = [
    { id: "existing", active: true, height_m: 10, color: "red" },
  ];
  const updated = addHorizontalHeights(p, rangeValues(0, 20, 10));
  assert.deepEqual(
    updated.horizontal_cuts.map((c) => c.height_m),
    [0, 10, 20],
  );
  assert.equal(updated.horizontal_cuts[1].id, "existing");
  assert.deepEqual(
    addVerticalAngles(p, rangeValues(0, 360, 90)).vertical_cuts.map(
      (c) => c.azimuth_deg,
    ),
    [0, 90, 180, 270],
  );
});
test("point projection uses the nearest forward active antenna, within tolerance", () => {
  const p = newProject();
  p.sources = [newSource(p.origin, "TX")];
  p.control_points = [
    { ...newPoint(p.origin, "near"), x_m: 1, y_m: 20 },
    { ...newPoint(p.origin, "far"), x_m: 3, y_m: 20 },
    { ...newPoint(p.origin, "behind"), x_m: 1, y_m: -20 },
  ];
  const aligned = alignPointsToAzimuth(p, 2);
  assert.equal(aligned.control_points[0].x_m, 0);
  assert.equal(aligned.control_points[0].y_m, 20);
  assert.equal(aligned.control_points[1].x_m, 3);
  assert.equal(aligned.control_points[2].x_m, 1);
  assert.notEqual(aligned.control_points[0].lat, p.origin[0]);
});
test("group moves retain all point parameters and stable ordering within groups", () => {
  const p = newProject();
  p.control_points = [
    { ...newPoint(p.origin, "a1"), group: "A" },
    { ...newPoint(p.origin, "b1"), group: "B" },
    { ...newPoint(p.origin, "a2"), group: "A" },
  ];
  assert.deepEqual(
    reorderGroup(p, "B", -1).control_points.map((c) => c.name),
    ["b1", "a1", "a2"],
  );
  assert.equal(reorderGroup(p, "A", -1), p);
});
test("36 original cables interpolate documented values and reject extrapolation", () => {
  assert.equal(feeders.length, 36);
  const f = feeders[0];
  assert.equal(feederAttenuation(f.points, 450), 12.9);
  assert.ok(
    Math.abs(feederAttenuation(f.points, Math.sqrt(450 * 1000)) - 16.3) < 1e-12,
  );
  assert.throws(() => feederAttenuation(f.points, 11000));
  for (const c of feeders)
    assert.ok(
      c.points.every(
        (v, i) =>
          Number.isFinite(v.loss) &&
          (!i || v.frequency > c.points[i - 1].frequency),
      ),
    );
});
test("feeder assembly applies the original logarithmic interpolation and three-decimal rounding", () => {
  const f = feeders[0];
  assert.equal(
    feederLoss(f.points, 900, 100, f.points, "0", 2, 0.1, 0).total,
    19.003,
  );
  const assembly = feederLoss(f.points, 900, 100, f.points, "3+3", 2, 0.05, 0);
  assert.equal(assembly.connectors, 6);
  assert.equal(assembly.jumperLoss, 1.128);
  assert.equal(assembly.total, 20.231);
  assert.throws(() =>
    feederLoss(f.points, 900, 100, f.points, "3+bad", 2, 0.1, 0),
  );
});
test("symmetric antenna generation follows the recovered Gq branches", () => {
  const p = symmetricPattern(7000, 31, 1.2);
  assert.equal(p.horizontal.length, 360);
  assert.equal(p.horizontal[0], 0);
  assert.deepEqual(p.horizontal, p.vertical);
  assert.equal(
    p.horizontal[90],
    Math.round(Math.abs(10 - 10 * Math.log10(1.2 / (300 / 7000)) - 31) * 10) /
      10,
  );
  assert.equal(p.horizontal[15], p.horizontal[345]);
  assert.ok(p.horizontal.every(Number.isFinite));
  assert.throws(() => symmetricPattern(7000, 2, 1.2));
  assert.equal(beamWidth(Array(360).fill(0)), 360);
});
test("antenna envelopes retain maximum absolute gain direction by direction", () => {
  const base = {
    id: "a",
    name: "A",
    gain: 10,
    horizontal: Array(360).fill(0),
    vertical: Array(360).fill(0),
  };
  const second = {
    ...base,
    id: "b",
    gain: 20,
    horizontal: Array(360).fill(15),
    vertical: Array(360).fill(0),
  };
  const envelope = envelopePatterns([base, second], "Envelope");
  assert.equal(envelope.gain, 20);
  assert.equal(envelope.horizontal[0], 10);
  assert.equal(envelope.vertical[0], 0);
});
test("geographic coordinates round trip through DMS and decimal minutes", () => {
  for (const value of [-84.1234567, -4.025, 0, 5.345, 84.999999])
    for (const format of ["dm", "dms"] as const)
      assert.ok(
        Math.abs(
          parseCoordinate(formatCoordinate(value, format, true), true) - value,
        ) < 0.000003,
      );
  assert.equal(parseCoordinate("4° 1′ 30″ W", false), -4.025);
  assert.throws(() => parseCoordinate("5° 61′ N", true));
  assert.throws(() => parseCoordinate("5 E", true));
});
test("terrain contour vertices participate in planar height interpolation", () => {
  const p = newProject();
  p.terrain_points = [];
  p.terrain_contours = [
    {
      id: "a",
      name: "Low",
      elevation: 10,
      closed: false,
      points: [
        [0, 0],
        [10, 0],
      ],
    },
    {
      id: "b",
      name: "High",
      elevation: 20,
      closed: false,
      points: [
        [0, 10],
        [10, 10],
      ],
    },
  ];
  assert.equal(terrainMesh(p).points.length, 4);
  assert.equal(terrainMesh(p).triangles.length, 6);
  assert.ok(Math.abs(terrainHeight(p, 5, 5) - 15) < 1e-10);
  p.settings.consider_terrain = false;
  assert.equal(terrainHeight(p, 5, 5), 0);
});
test("3D exposure isosurface converges to the analytic free-space sphere", () => {
  const p = newProject();
  p.settings.reflection = 1; // Analytic radius fixture is defined with k=1.
  p.settings.consider_terrain = false;
  p.limits[0].rows = [{ frequency: 100000, value: 1, exponent: 0 }];
  p.sources = [
    {
      ...newSource(p.origin, "Isotropic"),
      power_w: 100 / 30,
      gain_dbi: 0,
      height_m: 30,
      pattern_h: Array(360).fill(0),
      pattern_v: Array(360).fill(0),
    },
  ];
  const mesh = volumeSurface(p, {
    center_x: 0,
    center_y: 0,
    radius_m: 15,
    bottom_m: 15,
    top_m: 45,
    step_m: 1,
  });
  assert.ok(mesh.triangles > 100);
  assert.equal(mesh.clipped, false);
  let error = 0;
  for (let i = 0; i < mesh.positions.length; i += 3)
    error = Math.max(
      error,
      Math.abs(
        Math.hypot(
          mesh.positions[i],
          mesh.positions[i + 1],
          mesh.positions[i + 2] - 30,
        ) - 10,
      ),
    );
  assert.ok(error < 0.12, `radius error ${error}`);
  const fine = volumeSurface(p, {
    center_x: 0,
    center_y: 0,
    radius_m: 12,
    bottom_m: 18,
    top_m: 42,
    step_m: 0.5,
  });
  let fineError = 0;
  for (let i = 0; i < fine.positions.length; i += 3)
    fineError = Math.max(
      fineError,
      Math.abs(
        Math.hypot(
          fine.positions[i],
          fine.positions[i + 1],
          fine.positions[i + 2] - 30,
        ) - 10,
      ),
    );
  assert.ok(
    fineError < 0.04 && fineError < error / 3,
    `fine radius error ${fineError}, coarse ${error}`,
  );
  assert.equal(
    volumeSurface(p, {
      center_x: 0,
      center_y: 0,
      radius_m: 4,
      bottom_m: 26,
      top_m: 34,
      step_m: 2,
    }).clipped,
    true,
  );
});
test("3D volume guards reject unbounded sampling and invalid heights before calculation", () => {
  const p = newProject(),
    o = {
      center_x: 0,
      center_y: 0,
      radius_m: 100,
      bottom_m: 0,
      top_m: 100,
      step_m: 1,
    };
  assert.throws(() => volumeSurface(p, o), /maximum/);
  assert.throws(() => volumeSurface(p, { ...o, top_m: 0 }));
  assert.throws(() => volumeSurface(p, { ...o, step_m: 0 }));
});
