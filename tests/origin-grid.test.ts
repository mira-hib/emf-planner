import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import {
  newProject,
  newSource,
  newPoint,
  normalizeProject,
} from "../src/lib/project.ts";
import {
  areaFromExtents,
  areaFromBounds,
  calculationArea,
  areaWorldCorners,
  rebaseProject,
  horizontalDimensions,
} from "../src/lib/origin-grid.ts";
import {
  mercatorPoint,
  nativeLocal,
  nativeScale,
} from "../src/lib/native-coordinates.ts";
import { toGeo } from "../src/lib/geometry.ts";
import { horizontalGrid } from "../src/lib/engine.ts";
import { importXemf } from "../src/lib/import-xemf.ts";
import { resolveAliases } from "../src/lib/files.ts";

const near = (a: number, b: number, eps = 1e-7) =>
  assert.ok(Math.abs(a - b) < eps, `${a} != ${b}`);
test("native project defaults keep report site separate and use WM +/-100 with 600/200 divisions", () => {
  const p = newProject(),
    [x, y] = mercatorPoint(p.origin),
    corners = areaWorldCorners(p);
  assert.deepEqual(corners, [
    [x - 100, y - 100],
    [x + 100, y + 100],
  ]);
  assert.deepEqual(p.settings.site_coordinates, p.origin);
  assert.notEqual(p.settings.site_coordinates, p.origin);
  assert.deepEqual(p.settings.horizontal_grid_divisions, [600, 200]);
  const legacy = structuredClone(p);
  delete legacy.settings.horizontal_grid_divisions;
  delete legacy.settings.calculation_area_m;
  delete legacy.settings.calculation_area_wm;
  assert.equal(
    normalizeProject(legacy).settings.horizontal_grid_divisions,
    undefined,
  );
  assert.deepEqual(calculationArea(normalizeProject(legacy)), {
    x_min: -150,
    x_max: 150,
    y_min: -150,
    y_max: 150,
  });
});

test("native area dimensions are asymmetric with exact WM margin and native bounds", () => {
  const p = newProject(),
    [x, y] = mercatorPoint(p.origin),
    scale = nativeScale(p.origin);
  const area = areaFromExtents(p.origin, {
    left: 0,
    right: 120,
    top: 40,
    bottom: 30,
  });
  assert.deepEqual(area.calculation_area_wm, [
    [x - 0.1, y - 30 / scale - 0.1],
    [x + 120 / scale + 0.1, y + 40 / scale + 0.1],
  ]);
  near(area.calculation_area_m.x_min, -0.1 * scale, 0.01);
  near(area.calculation_area_m.x_max, 120 + 0.1 * scale, 0.01);
  for (const patch of [
    { left: -1 },
    { right: 9.9 },
    { top: 5000.1 },
    { bottom: NaN },
  ])
    assert.throws(() =>
      areaFromExtents(p.origin, {
        left: 20,
        right: 20,
        top: 20,
        bottom: 20,
        ...patch,
      }),
    );
  assert.throws(() =>
    areaFromBounds(p.origin, { x_min: 1, x_max: 0, y_min: 0, y_max: 1 }),
  );
});

test("origin rebasing preserves geographic anchors, independent report site and world rectangle", () => {
  const p = newProject();
  const geo = toGeo(33, 17, p.origin);
  p.sources = [
    {
      ...newSource(p.origin, "TX"),
      lat: geo[0],
      lng: geo[1],
      x_m: 33,
      y_m: 17,
    },
  ];
  p.control_points = [
    { ...newPoint(p.origin, "CP"), lat: geo[0], lng: geo[1], x_m: 33, y_m: 17 },
  ];
  p.vertical_cuts = [
    {
      id: "v",
      active: true,
      x: 33,
      y: 17,
      geo_position: geo,
      azimuth_deg: 0,
      forward_m: 100,
      backward_m: 30,
      max_height_m: 10,
    },
  ];
  p.lines = [
    {
      id: "l",
      name: "Road",
      kind: "road",
      color: "#000000",
      width: 1,
      points: [
        [30, 10],
        [40, 20],
      ],
    },
  ];
  p.terrain_points = [{ id: "t", x: 25, y: 15, elevation: 10 }];
  p.settings.site_coordinates = [5.4, -4.1];
  const before = structuredClone(p),
    origin = toGeo(50, -20, p.origin),
    result = rebaseProject(p, origin);
  assert.deepEqual(p, before);
  assert.deepEqual(result.settings.site_coordinates, [5.4, -4.1]);
  assert.deepEqual(
    result.settings.calculation_area_wm,
    p.settings.calculation_area_wm,
  );
  assert.notDeepEqual(
    result.settings.calculation_area_m,
    p.settings.calculation_area_m,
  );
  assert.equal(result.settings.radius_m, p.settings.radius_m);
  assert.deepEqual([result.sources[0].lat, result.sources[0].lng], geo);
  assert.deepEqual(
    [result.sources[0].x_m, result.sources[0].y_m],
    nativeLocal(...geo, origin, true),
  );
  assert.deepEqual(
    [result.control_points[0].x_m, result.control_points[0].y_m],
    nativeLocal(...geo, origin, true),
  );
  assert.deepEqual(
    [result.vertical_cuts[0].x, result.vertical_cuts[0].y],
    nativeLocal(...geo, origin, true),
  );
  const originalGeo = toGeo(...p.lines[0].points[0], p.origin),
    resultGeo = toGeo(...result.lines[0].points[0], origin);
  near(originalGeo[0], resultGeo[0], 1e-10);
  near(originalGeo[1], resultGeo[1], 1e-10);
  const twice = rebaseProject(result, p.origin);
  assert.deepEqual(
    [twice.vertical_cuts[0].x, twice.vertical_cuts[0].y],
    nativeLocal(...geo, p.origin, true),
  );
});

test("native horizontal grids divide the largest dimension and round the smaller axis to even", () => {
  const p = newProject();
  p.settings.calculation_area_m = {
    x_min: -100,
    x_max: 100,
    y_min: -30,
    y_max: 30,
  };
  assert.deepEqual(horizontalDimensions(p, 50), {
    width: 51,
    height: 16,
    dx: 4,
    dy: 4,
  });
  const grid = horizontalGrid(p, 1.5, 99, 50);
  assert.equal(grid.width, 51);
  assert.equal(grid.height, 16);
  assert.equal(grid.dx, 4);
  assert.equal(grid.values.length, 51 * 16);
  p.settings.calculation_area_m = { x_min: 0, x_max: 100, y_min: 0, y_max: 5 };
  assert.equal(horizontalDimensions(p, 50).height, 3); // 2.5 -> 2, inclusive nodes
  for (const invalid of [49, 1001, 50.5, NaN])
    assert.throws(() => horizontalDimensions(p, invalid));
});

test("original imports preserve native report coordinates, world rectangle, both matrix sizes and cut anchors", () => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const p = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "original.xemf",
  );
  assert.ok(p.settings.site_coordinates?.every(Number.isFinite));
  assert.ok(p.settings.calculation_area_wm?.flat().every(Number.isFinite));
  assert.equal(p.settings.horizontal_grid_divisions?.length, 2);
  assert.ok(p.vertical_cuts[0].geo_position?.every(Number.isFinite));
  const restored = normalizeProject(JSON.parse(JSON.stringify(p)));
  assert.deepEqual(restored.settings, p.settings);
  const newOrigin = toGeo(25, -15, p.origin),
    moved = rebaseProject(p, newOrigin);
  assert.deepEqual(
    moved.settings.calculation_area_wm,
    p.settings.calculation_area_wm,
  );
  assert.deepEqual(
    moved.settings.site_coordinates,
    p.settings.site_coordinates,
  );
  assert.equal(moved.saved_cut_results, undefined);
  p.settings.site_coordinates = [5.5, -4.025];
  assert.equal(resolveAliases(p, "Site {Coordinates}"), "Site 5.5; -4.025");
});
