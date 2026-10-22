import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { importXemf } from "../src/lib/import-xemf.ts";
import { newProject } from "../src/lib/project.ts";
import { terrainHeight } from "../src/lib/geometry.ts";
import {
  closedContourPolygons,
  contourPaths,
  horizontalPosition,
  mastDimensions,
  mastEdges,
  mastFoundation,
  scenePosition,
  verticalPosition,
} from "../src/lib/scene-geometry.ts";

test("3D differentiates absolute terrain altitude from relative cut heights", () => {
  assert.deepEqual(scenePosition(10, 20, 100, 80), [10, 20, -20]);
  assert.deepEqual(horizontalPosition([10, 20], 18), [10, 18, -20]);
  const cut = {
    id: "v",
    active: true,
    x: 10,
    y: 20,
    azimuth_deg: 90,
    backward_m: 10,
    forward_m: 50,
    max_height_m: 30,
  };
  const point = verticalPosition([-5, 18], cut);
  assert.ok(Math.abs(point[0] - 5) < 1e-10);
  assert.equal(point[1], 18);
  assert.ok(Math.abs(point[2] + 20) < 1e-10);
});

test("only original explicit Tower annotations produce dimensioned lattice masts", () => {
  assert.equal(mastDimensions("Tower"), null);
  assert.equal(mastDimensions("Source 1"), null);
  assert.equal(mastDimensions("Tower/0/30/0/2"), null);
  const dimensions = mastDimensions("Tower/10/20/2/4")!;
  const edges = mastEdges(dimensions, 3, 5, 100, 80);
  assert.ok(edges.length > 30);
  assert.equal(Math.min(...edges.flat(1).map((point) => point[1])), 20);
  assert.equal(Math.max(...edges.flat(1).map((point) => point[1])), 50);
  assert.equal(Math.min(...edges.flat(1).map((point) => point[0])), 1);
  assert.equal(Math.max(...edges.flat(1).map((point) => point[0])), 5);
  const p = newProject();
  p.settings.consider_terrain = false;
  p.buildings = [
    {
      id: "roof",
      name: "Roof",
      points: [
        [0, 0],
        [10, 0],
        [10, 10],
        [0, 10],
      ],
      foundation_m: 12,
      height_m: 8,
      attenuation_db: 0,
      active: true,
    },
  ];
  assert.equal(mastFoundation(p, 3, 5), 20);
  assert.equal(mastFoundation(p, -3, -5), 0);
});

test("3D stitches exact contour edges, fills closed rings and preserves holes", () => {
  const paths = contourPaths({
    segments: [
      [
        [0, 0],
        [10, 0],
      ],
      [
        [10, 10],
        [0, 10],
      ],
      [
        [10, 0],
        [10, 10],
      ],
      [
        [0, 0],
        [0, 10],
      ],
      [
        [2, 2],
        [3, 2],
        [3, 3],
        [2, 3],
        [2, 2],
      ],
      [
        [20, 20],
        [21, 20],
      ],
    ],
  });
  assert.equal(paths.length, 3);
  const polygons = closedContourPolygons(paths);
  assert.equal(polygons.length, 1);
  assert.equal(polygons[0].outer.length, 5);
  assert.equal(polygons[0].holes.length, 1);
});

test("Castelfranco 3D uses its imported terrain and explicit mast, without inferred towers", () => {
  const buffer = readFileSync("tests/fixtures/original-sample.xemf");
  const p = importXemf(
    buffer.buffer.slice(
      buffer.byteOffset,
      buffer.byteOffset + buffer.byteLength,
    ),
    "Castelfranco",
  );
  const masts = p.lines.filter(
    (line) => line.kind === "label" && mastDimensions(line.name),
  );
  assert.equal(masts.length, 1);
  const point = masts[0].points[0];
  const edges = mastEdges(
    mastDimensions(masts[0].name)!,
    ...point,
    mastFoundation(p, ...point),
    terrainHeight(p, 0, 0),
  );
  assert.ok(edges.length > 30);
  assert.ok(edges.flat(1).every((point) => point.every(Number.isFinite)));
  assert.ok(p.sources.length > masts.length);
});
