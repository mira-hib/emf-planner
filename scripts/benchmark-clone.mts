import { readFileSync, writeFileSync, mkdirSync } from "node:fs";
import { importXemf } from "../src/lib/import-xemf.ts";
import { calculatePoints } from "../src/lib/engine.ts";
const file = process.argv[2] ?? "tests/fixtures/original-sample.xemf";
const bytes = readFileSync(file),
  project = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    file,
  ),
  ref = JSON.parse(
    readFileSync("tests/fixtures/original-results.json", "utf8"),
  );
const results = calculatePoints(project).map((r, i) => ({
  point: r.name,
  expected_field: ref.point_calculation.rows[i][5],
  actual_field: r.total_field,
  absolute_field_error: Math.abs(
    r.total_field - ref.point_calculation.rows[i][5],
  ),
  expected_exposure_percent: ref.point_calculation.rows[i][4],
  actual_exposure_percent: r.exposure_ratio * 100,
}));
const report = {
  date: new Date().toISOString(),
  sample: file,
  reference_sample_sha256: ref.sample_sha256,
  results,
  max_absolute_field_error: Math.max(
    ...results.map((r) => r.absolute_field_error),
  ),
  tolerance: 1.1e-6,
  scope:
    "15 observed positions/heights of one original sample. Does not establish general parity for all branches or native extensions.",
};
mkdirSync(".tools/clone-validation", { recursive: true });
writeFileSync(
  ".tools/clone-validation/numeric-parity.json",
  JSON.stringify(report, null, 2),
);
console.log(JSON.stringify(report, null, 2));
