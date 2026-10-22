import test from "node:test";
import assert from "node:assert/strict";
import { newProject } from "../src/lib/project.ts";
import { limitAt } from "../src/lib/engine.ts";
import {
  addLimitRow,
  commonLimitRows,
  completeLimit,
  editLimitCell,
  EMPTY_LIMIT,
  limitSegments,
  NATIVE_FALLBACK_LIMIT,
  parseLimitCell,
  removeLimitRows,
} from "../src/lib/limits.ts";

test("native limits keep row order, do not extrapolate, and use (lower, upper]", () => {
  const limit = newProject().limits[0];
  limit.rows = [
    { frequency: 30, value: 824, exponent: -1 },
    { frequency: 300, value: 27.5, exponent: 0 },
    { frequency: 1500, value: 1.585, exponent: 0.5 },
  ];
  assert.equal(limitAt(limit, 10), NATIVE_FALLBACK_LIMIT);
  assert.equal(limitAt(limit, 9), NATIVE_FALLBACK_LIMIT);
  assert.equal(limitAt(limit, 30), 824 / 30);
  assert.equal(limitAt(limit, 30.001), 27.5);
  assert.equal(limitAt(limit, 300), 27.5);
  assert.equal(limitAt(limit, 1500.001), NATIVE_FALLBACK_LIMIT);
  limit.rows = [limit.rows[1], limit.rows[0], limit.rows[2]];
  assert.equal(limitAt(limit, 20), 27.5); // No silent sorting.
  limit.rows = [
    { frequency: 1, value: 50, exponent: 0 },
    { frequency: 100, value: 20, exponent: 0 },
  ];
  assert.equal(limitAt(limit, 1), NATIVE_FALLBACK_LIMIT);
  assert.equal(limitAt(limit, 1.001), 20);
});

test("one empty cell invalidates the whole native set and its entire curve", () => {
  const limit = newProject().limits[0];
  assert.ok(completeLimit(limit));
  assert.equal(limitSegments(limit).length, 3);
  limit.rows[1].exponent = EMPTY_LIMIT;
  assert.equal(completeLimit(limit), false);
  assert.equal(limitAt(limit, 100), NATIVE_FALLBACK_LIMIT);
  assert.deepEqual(limitSegments(limit), []);
  limit.rows = [];
  assert.equal(limitAt(limit, 900), NATIVE_FALLBACK_LIMIT);
});

test("native numeric bounds, decimal comma, Single precision and blanks", () => {
  assert.equal(parseLimitCell("1 000", "frequency"), EMPTY_LIMIT);
  assert.equal(parseLimitCell("1000000", "frequency"), 1e6);
  for (const text of ["", "0", "-1", "1000001", "NaN", "Infinity"])
    assert.equal(parseLimitCell(text, "frequency"), EMPTY_LIMIT);
  assert.equal(parseLimitCell("0,688", "value"), Math.fround(0.688));
  assert.equal(parseLimitCell("100000", "value"), 1e5);
  assert.equal(parseLimitCell("100001", "value"), EMPTY_LIMIT);
  for (const value of [-3, 0, 3])
    assert.equal(parseLimitCell(String(value), "exponent"), value);
  for (const value of [-3.001, 3.001])
    assert.equal(parseLimitCell(String(value), "exponent"), EMPTY_LIMIT);
});

test("shared frequency edits, blank insertion and selected deletion affect all four sets independently", () => {
  const original = newProject().limits;
  const edited = editLimitCell(original, 1, 2, "frequency", 1800);
  assert.deepEqual(
    edited.map((l) => l.rows[1].frequency),
    [1800, 1800, 1800, 1800],
  );
  assert.equal(original[0].rows[1].frequency, 2000);
  const coefficient = editLimitCell(edited, 1, 2, "value", 15);
  assert.equal(coefficient[2].rows[1].value, 15);
  assert.equal(coefficient[0].rows[1].value, edited[0].rows[1].value);
  const added = addLimitRow(coefficient);
  assert.ok(
    added.every(
      (l) => l.rows.length === 4 && l.rows[3].frequency === EMPTY_LIMIT,
    ),
  );
  const restored = removeLimitRows(added, [3]);
  assert.deepEqual(restored, coefficient);
  const empty = removeLimitRows(restored, [0, 1, 2]);
  assert.ok(empty.every((l) => l.rows.length === 0));
  assert.ok(addLimitRow(empty).every((l) => l.rows.length === 1));
});

test("old independent clone tables refine onto a common frequency grid without aliasing", () => {
  const limits = newProject().limits.slice(0, 2);
  limits[0].rows = [
    { frequency: 400, value: 28, exponent: 0 },
    { frequency: 2000, value: 1.375, exponent: 0.5 },
  ];
  limits[1].rows = [
    { frequency: 900, value: 5, exponent: 0 },
    { frequency: 2000, value: 6, exponent: 0 },
  ];
  const rows = commonLimitRows(limits);
  assert.deepEqual(
    rows.map((set) => set.map((r) => r.frequency)),
    [
      [400, 900, 2000],
      [400, 900, 2000],
    ],
  );
  assert.equal(rows[0][1].value, 1.375);
  rows[0][0].value = 99;
  assert.equal(limits[0].rows[0].value, 28);
});

test("graph segments keep discontinuities separate and clip the native 10 MHz–100 GHz range", () => {
  const limit = newProject().limits[0];
  assert.deepEqual(limitSegments(limit), [
    [10, 28, 400, 28],
    [400, 27.5, 2000, 1.375 * Math.sqrt(2000)],
    [2000, 61, 100000, 61],
  ]);
});
