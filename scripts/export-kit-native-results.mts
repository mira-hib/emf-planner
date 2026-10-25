import { readFileSync, mkdirSync, writeFileSync } from "node:fs";
import { dirname } from "node:path";
import { platform, arch } from "node:os";
import { sourceField } from "../src/lib/engine.ts";
import { newProject, newSource } from "../src/lib/project.ts";
import { parseMsi } from "../src/lib/antenna.ts";

const destination = process.argv[2];
if (!destination) throw new Error("Specify the output JSON path.");
const fixture = JSON.parse(readFileSync("tests/fixtures/test-kit-native-oracle.json", "utf8"));
const patterns = new Map(["ok_isotropic.msi", "ok_sector65x7_17dBi.msi", "ok_backfire_180.msi"].map(name => [name,
  parseMsi(readFileSync(`test/emf_test_kit/data/msi/${name}`, "utf8"))]));
const cases = fixture.cases.map((c: Record<string, any>, index: number) => {
  const p = newProject();
  p.origin = [45.485337, 9.198818];
  p.settings.calculation_area_m = { x_min: -100, x_max: 100, y_min: -100, y_max: 100 };
  p.settings.horizontal_grid_divisions = undefined;
  p.settings.building_attenuation_high_db = undefined;
  const pattern = patterns.get(c.pattern)!;
  p.sources = [{ ...newSource(p.origin, "TS06_ISO_1W"), power_w: c.power, num_tx: c.tx,
    total_attenuation_db: c.loss, gain_dbi: c.gain, height_m: c.height,
    azimuth_deg: c.azimuth, mechanical_downtilt_deg: c.tilt,
    pattern_h: pattern.horizontal, pattern_v: pattern.vertical }];
  const actual = sourceField(p, p.sources[0], c.x, c.y, c.z).field;
  const relative = Math.abs(actual - c.native_field) / Math.max(c.native_field, 1e-12);
  return { index: index + 1, ...c, actual_field: actual, absolute_difference: Math.abs(actual - c.native_field), relative_difference: relative, passed: Number.isFinite(actual) && relative < 0.0001 };
});
const result = { created_at: new Date().toISOString(), host: { platform: platform(), arch: arch() },
  scope: "Detailed local replay of the same 1800 kit-oracle comparisons as tests/test-kit.test.ts. Original CalcPPE method reference only; not the original UI or complete native pipeline.",
  normalized_original_method_sha256: fixture.normalized_method_sha256,
  count: cases.length, passed: cases.filter((c: any) => c.passed).length,
  max_relative_difference: Math.max(...cases.map((c: any) => c.relative_difference)), relative_tolerance: 0.0001, cases };
mkdirSync(dirname(destination), { recursive: true });
writeFileSync(destination, JSON.stringify(result, null, 2) + "\n");
console.log(JSON.stringify({ count: result.count, passed: result.passed, max_relative_difference: result.max_relative_difference }));
if (result.passed !== result.count) process.exitCode = 1;
