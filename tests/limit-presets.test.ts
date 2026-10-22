import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import {
  applyProjectPreset,
  PRESETS,
  NATIVE_LIMIT_MISSING,
} from "../src/lib/presets.ts";
import { newProject } from "../src/lib/project.ts";

const native = readFileSync(
  "reference/original-decompiled/EMFPlanner/Public2.cs",
  "utf8",
)
  .split("public static void SetLimitsPRJ(string CountryIndex)")[1]
  .split("public static void SetLimitsAPP()")[0];
const firstBranch = native.slice(0, native.indexOf("switch (CountryIndex)"));

test("all 16 historical presets match every native frequency, coefficient, exponent and description", () => {
  assert.equal(PRESETS.length, 16);
  assert.equal(new Set(PRESETS.map((preset) => preset.label)).size, 16);
  for (const preset of PRESETS) {
    const branch = ["ICNIRP", "EU"].includes(preset.country)
      ? firstBranch
      : native.split(`case "${preset.country}":`)[1].split("break;")[0];
    const expected = [...branch.matchAll(/new LimitPoint\(([^)]+)\)/g)].map(
      (match) =>
        match[1]
          .split(",")
          .map((part) => Number(part.trim().replace(/f$/, ""))),
    );
    const expectedDescriptions = branch.match(
      /LimitDescription = new string\[4\] \{ (.+) \}/,
    );
    const descriptions = expectedDescriptions
      ? [...expectedDescriptions[1].matchAll(/"([^"]*)"/g)].map(
          (match) => match[1],
        )
      : null;
    assert.deepEqual(preset.rows, expected, preset.country);
    assert.deepEqual(preset.descriptions, descriptions, preset.country);
    const applied = applyProjectPreset(preset.label, [])!;
    assert.equal(applied.length, 4);
    for (let index = 0; index < 4; index++) {
      assert.deepEqual(
        applied[index].rows,
        expected.map((row) => ({
          frequency: row[0],
          value: row[index * 2 + 1],
          exponent: row[index * 2 + 2],
        })),
        `${preset.country} set ${index + 1}`,
      );
    }
  }
});

test("applying presets preserves project identity, colors, activity and factor switches", () => {
  const existing = newProject().limits.map((limit, index) => ({
    ...limit,
    active: index % 2 === 0,
    use_alpha24: index === 0,
    use_ftdc: index === 1,
    use_fpr: index === 2,
  }));
  const before = structuredClone(existing);
  const result = applyProjectPreset("Italie", existing)!;
  assert.deepEqual(existing, before);
  for (let index = 0; index < 4; index++) {
    for (const key of [
      "id",
      "color",
      "active",
      "use_alpha24",
      "use_ftdc",
      "use_fpr",
    ] as const)
      assert.equal(result[index][key], existing[index][key]);
  }
  assert.deepEqual(
    result.map((limit) => limit.rows[0].value),
    [6, 20, 4.5, 3],
  );
  assert.equal(result[2].name, "75% of EMF Limits 2");
  assert.deepEqual(
    applyProjectPreset("Grèce", existing)!.map((limit) => limit.name),
    existing.map((limit) => limit.name),
  );
});

test("blank sets retain shared native frequencies and missing-value sentinels", () => {
  const result = applyProjectPreset("Russie", [])!;
  assert.deepEqual(
    result.map((limit) => limit.color),
    ["#ff0000", "#0000ff", "#00ff00", "#00ffff"],
  );
  assert.deepEqual(
    result[0].rows.map((row) => row.frequency),
    [300, 100000],
  );
  for (const limit of result.slice(1)) {
    assert.equal(limit.name, "");
    assert.ok(
      limit.rows.every(
        (row) =>
          row.value === NATIVE_LIMIT_MISSING &&
          row.exponent === NATIVE_LIMIT_MISSING,
      ),
    );
    assert.equal(limit.use_alpha24 && limit.use_ftdc && limit.use_fpr, true);
  }
  result[0].rows[0].value = 99;
  assert.equal(applyProjectPreset("Russie", [])![0].rows[0].value, 3);
  assert.equal(applyProjectPreset("unknown", []), null);
});
