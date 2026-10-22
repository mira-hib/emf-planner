import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { importXemf } from "../src/lib/import-xemf";
import { normalizeProject, newProject } from "../src/lib/project";
import { horizontalGrid, exposureAt } from "../src/lib/engine";
import { contourPaths } from "../src/lib/scene-geometry";

test("Castelfranco keeps native extent, display settings, colors and recorded contours", () => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const p = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "Castelfranco.xemf",
  );
  assert.deepEqual(
    p.limits.map((l) => l.color),
    ["#ff0000", "#ff8000", "#0000ff", "#008000"],
  );
  const area = p.settings.calculation_area_m!;
  assert.ok(Math.abs(area.x_min + 137.25220217206717) < 1e-9);
  assert.ok(Math.abs(area.y_max - 137.25039538926322) < 1e-9);
  assert.equal(p.settings.building_labels?.foundation, false);
  assert.equal(p.settings.control_point_labels?.prefix, "");
  assert.equal(p.settings.display_options?.terrain, false);
  assert.equal(p.saved_cut_results?.horizontal.length, 4);
  assert.equal(p.saved_cut_results?.vertical.length, 24);
  const horizontal = p.saved_cut_results!.horizontal;
  assert.deepEqual(
    horizontal.map((r) => r.contours.segments.length),
    [245, 193, 287, 312],
  );
  assert.ok(horizontal.every((r) => r.contours.height_m === 18));
  assert.ok(
    contourPaths(horizontal[3].contours).some(
      (path) =>
        path.length > 100 &&
        Math.hypot(path[0][0] - path.at(-1)![0], path[0][1] - path.at(-1)![1]) <
          1e-6,
    ),
  );
  const vertical = p.saved_cut_results!.vertical[0];
  assert.equal(vertical.grid.x0, -100);
  assert.equal(vertical.grid.y0, -2);
  assert.equal(vertical.contours.segments.length, 318);
  // Archived geometry must never be presented as newly computed field samples.
  assert.ok(
    [...horizontal, ...p.saved_cut_results!.vertical].every(
      (r) => r.contours_only && !r.grid.fields.length && !r.grid.values.length,
    ),
  );
  const restored = normalizeProject(JSON.parse(JSON.stringify(p)));
  assert.deepEqual(restored.saved_cut_results, p.saved_cut_results);
});

test("horizontal sampling covers a displaced rectangular calculation area", () => {
  const p = newProject();
  p.settings.calculation_area_m = { x_min: 5, x_max: 17, y_min: -3, y_max: 1 };
  const grid = horizontalGrid(p, 2, 2);
  assert.equal(grid.width, 7);
  assert.equal(grid.height, 3);
  assert.equal(grid.x0, 5);
  assert.equal(grid.y0, -3);
  assert.equal(grid.fields.at(-1), exposureAt(p, 17, 1, 2).total_field);
});
