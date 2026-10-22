import test from "node:test";
import assert from "node:assert/strict";
import { newSource } from "../src/lib/project.ts";
import {
  colorSourceRows,
  copySourceRows,
  duplicateSourceRows,
  moveSourceRows,
  parseSourceBuffer,
  pasteSourceRows,
  sourceSelection,
  toggleAllSourceRows,
  SOURCE_BUFFER_FORMAT,
  SOURCE_BUFFER_VERSION,
} from "../src/lib/source-rows.ts";
import type { RadiatingSource } from "../src/types/index.ts";

function rows(names = "ABCDE"): RadiatingSource[] {
  return [...names].map((name, i) => ({
    ...newSource([44.6, 11.05], name),
    id: name,
    power_w: 10 + i,
  }));
}
const ids = (sources: readonly RadiatingSource[]) =>
  sources.map((source) => source.id);
function factory() {
  let i = 0;
  return () => `copy-${++i}`;
}
function completeSource(): RadiatingSource {
  return {
    ...rows("A")[0],
    frequency_end_mhz: 960,
    pattern_h: Array.from({ length: 360 }, (_, i) => i / 10),
    pattern_v: Array(360).fill(3),
    limit_factors: [
      [0.5, 0.25, 0.8],
      [1, 1, 1],
      [1, 1, 1],
      [1, 1, 1],
    ],
    feeder: {
      cable: "LCF12-50",
      length_m: 32,
      jumper: "RG 213",
      jumper_lengths: "3+3",
      automatic_connectors: true,
      include_connectors: false,
      connectors: 6,
      connector_loss: 0.05,
      other_loss: 0.3,
    },
  };
}

test("duplication uses table order after the last selection and preserves every original row", () => {
  const sources = rows(),
    before = JSON.stringify(sources);
  assert.deepEqual(ids(sourceSelection(sources, ["D", "B", "B", "stale"])), [
    "B",
    "D",
  ]);
  const result = duplicateSourceRows(sources, ["D", "B"], factory());
  assert.deepEqual(ids(result.sources), [
    "A",
    "B",
    "C",
    "D",
    "copy-1",
    "copy-2",
    "E",
  ]);
  assert.deepEqual(
    result.sources.slice(4, 6).map((source) => [source.name, source.power_w]),
    [
      ["B - Copy", 11],
      ["D - Copy", 13],
    ],
  );
  assert.deepEqual(result.selectedIds, ["copy-1", "copy-2"]);
  assert.equal(result.activeId, "copy-1");
  assert.equal(result.changed, true);
  assert.equal(JSON.stringify(sources), before);
  assert.equal(duplicateSourceRows(sources, ["stale"]).changed, false);
});

test("duplicated antenna patterns, limit factors and cable settings are deeply independent", () => {
  const source = completeSource(),
    result = duplicateSourceRows([source], [source.id], factory()),
    copy = result.sources[1];
  copy.pattern_h![0] = 999;
  copy.pattern_v![0] = 888;
  copy.limit_factors![0][0] = 777;
  copy.feeder!.length_m = 666;
  assert.equal(source.pattern_h![0], 0);
  assert.equal(source.pattern_v![0], 3);
  assert.equal(source.limit_factors![0][0], 0.5);
  assert.equal(source.feeder!.length_m, 32);
  assert.equal(copy.frequency_end_mhz, 960);
  assert.equal(copy.feeder!.include_connectors, false);
  const idSequence = ["A", "fresh"];
  assert.equal(
    duplicateSourceRows([source], ["A"], () => idSequence.shift()!).sources[1]
      .id,
    "fresh",
  );
  assert.throws(
    () => duplicateSourceRows([source], ["A"], () => "A"),
    /unique/,
  );
});

test("up and down move selected blocks one row without crossing the native boundary", () => {
  const sources = rows(),
    before = ids(sources);
  const up = moveSourceRows(sources, ["D", "C"], "up");
  assert.deepEqual(ids(up.sources), ["A", "C", "D", "B", "E"]);
  assert.deepEqual(up.selectedIds, ["C", "D"]);
  assert.equal(up.activeId, "D");
  assert.deepEqual(ids(moveSourceRows(sources, ["B", "C"], "down").sources), [
    "A",
    "D",
    "B",
    "C",
    "E",
  ]);
  assert.deepEqual(ids(moveSourceRows(sources, ["B", "D"], "up").sources), [
    "B",
    "A",
    "D",
    "C",
    "E",
  ]);
  assert.deepEqual(ids(moveSourceRows(sources, ["B", "D"], "down").sources), [
    "A",
    "C",
    "B",
    "E",
    "D",
  ]);
  for (const direction of ["up", "top"] as const) {
    const result = moveSourceRows(sources, ["A", "C"], direction);
    assert.equal(result.changed, false);
    assert.deepEqual(ids(result.sources), before);
  }
  for (const direction of ["down", "bottom"] as const) {
    const result = moveSourceRows(sources, ["B", "E"], direction);
    assert.equal(result.changed, false);
    assert.deepEqual(ids(result.sources), before);
  }
  assert.deepEqual(ids(sources), before);
});

test("moving to either end preserves selected and unselected relative order", () => {
  const sources = rows();
  const top = moveSourceRows(sources, ["D", "B"], "top");
  assert.deepEqual(ids(top.sources), ["B", "D", "A", "C", "E"]);
  assert.equal(top.activeId, "B");
  const bottom = moveSourceRows(sources, ["D", "B"], "bottom");
  assert.deepEqual(ids(bottom.sources), ["A", "C", "E", "B", "D"]);
  assert.equal(bottom.activeId, "D");
  assert.deepEqual(bottom.selectedIds, ["B", "D"]);
  assert.equal(moveSourceRows(sources, [], "top").changed, false);
});

test("versioned clipboard preserves complete source data and pastes after the active row without overwriting", () => {
  const source = completeSource(),
    sources = [source, ...rows("BC")];
  const buffer = copySourceRows(sources, ["C", "A"])!;
  const envelope = JSON.parse(buffer);
  assert.equal(envelope.format, SOURCE_BUFFER_FORMAT);
  assert.equal(envelope.version, SOURCE_BUFFER_VERSION);
  assert.deepEqual(parseSourceBuffer(buffer), [source, sources[2]]);
  const result = pasteSourceRows(sources, "A", buffer, factory());
  assert.deepEqual(ids(result.sources), ["A", "copy-1", "copy-2", "B", "C"]);
  assert.deepEqual(
    result.sources.map((source) => source.name),
    ["A", "A", "C", "B", "C"],
  );
  assert.deepEqual(result.selectedIds, ["copy-1", "copy-2"]);
  assert.equal(result.activeId, "copy-1");
  result.sources[1].pattern_h![0] = 200;
  result.sources[1].feeder!.other_loss = 300;
  assert.equal(source.pattern_h![0], 0);
  assert.equal(parseSourceBuffer(buffer)[0].feeder!.other_loss, 0.3);
  assert.equal(copySourceRows(sources, []), null);
});

test("pasting into an empty list selects its first row and every paste renews IDs", () => {
  const source = completeSource(),
    buffer = copySourceRows([source], ["A"])!,
    nextId = factory();
  const empty = pasteSourceRows([], null, buffer, nextId);
  assert.deepEqual(ids(empty.sources), ["copy-1"]);
  assert.equal(empty.activeId, "copy-1");
  const again = pasteSourceRows(empty.sources, "copy-1", buffer, nextId);
  assert.deepEqual(ids(again.sources), ["copy-1", "copy-2"]);
  assert.deepEqual(again.selectedIds, ["copy-2"]);
  const appended = pasteSourceRows(
    rows(),
    "new-row-placeholder",
    buffer,
    nextId,
  );
  assert.deepEqual(ids(appended.sources), ["A", "B", "C", "D", "E", "copy-3"]);
  const collision = ["A", "fresh"];
  assert.equal(
    pasteSourceRows([], null, buffer, () => collision.shift()!).sources[0].id,
    "fresh",
  );
});

test("clipboard rejects malformed, incompatible and non-data source payloads", () => {
  const source = completeSource(),
    payload = JSON.parse(copySourceRows([source], ["A"])!);
  for (const text of [
    "",
    "not JSON",
    "null",
    "[]",
    JSON.stringify({ ...payload, version: 2 }),
    JSON.stringify({ ...payload, format: "BinaryFormatter" }),
    JSON.stringify({ ...payload, sources: [] }),
  ])
    assert.throws(() => parseSourceBuffer(text), /Tampon de sources invalide/);
  for (const [key, value] of [
    ["power_w", "10"],
    ["gain_dbi", null],
    ["active", 1],
    ["pattern_h", [0, 1]],
    ["limit_factors", [[1, 1]]],
    ["feeder", { ...source.feeder, connectors: "6" }],
    ["__proto__", { polluted: true }],
  ] as const) {
    const bad = JSON.parse(JSON.stringify(payload));
    Object.defineProperty(bad.sources[0], key, { value, enumerable: true });
    assert.throws(
      () => parseSourceBuffer(JSON.stringify(bad)),
      /Tampon de sources invalide/,
    );
  }
  assert.equal(({} as Record<string, unknown>).polluted, undefined);
  assert.throws(
    () => copySourceRows([{ ...source, power_w: Number.NaN }], ["A"]),
    /power_w/,
  );
  let invoked = false;
  const accessor = { ...source };
  Object.defineProperty(accessor, "name", {
    get() {
      invoked = true;
      return "bad";
    },
    enumerable: true,
  });
  assert.throws(() => copySourceRows([accessor], ["A"]), /propriété name/);
  assert.equal(invoked, false);
});

test("row color affects the full selection and global activation follows the FIRST source", () => {
  const sources = rows("ABC");
  sources[1].active = false;
  const colored = colorSourceRows(sources, ["C", "A", "stale"], "#a5eda2");
  assert.deepEqual(
    colored.map((source) => source.color),
    ["#a5eda2", "#ffffff", "#a5eda2"],
  );
  assert.equal(sources[0].color, "#ffffff");
  assert.throws(() => colorSourceRows(sources, ["A"], "bad"), /Couleur/);
  assert.deepEqual(
    toggleAllSourceRows(sources).map((source) => source.active),
    [false, false, false],
  );
  sources[0].active = false;
  assert.deepEqual(
    toggleAllSourceRows(sources).map((source) => source.active),
    [true, true, true],
  );
  delete sources[0].active;
  assert.deepEqual(
    toggleAllSourceRows(sources).map((source) => source.active),
    [false, false, false],
  );
  assert.deepEqual(toggleAllSourceRows([]), []);
});
