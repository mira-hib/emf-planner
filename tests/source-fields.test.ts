import { test } from "node:test";
import assert from "node:assert/strict";
import {
  sourceFrequency,
  sourceFrequencyLabel,
  nativeBeamWidth,
  fillSourceBeamWidths,
} from "../src/lib/source-fields";
import { newSource } from "../src/lib/project";

test("frequency bands are ordered and can return to a single frequency", () => {
  const band = sourceFrequency("960-880,5");
  assert.deepEqual(band, { frequency_mhz: 880.5, frequency_end_mhz: 960 });
  assert.equal(sourceFrequencyLabel(band), "880.5-960");
  assert.deepEqual(sourceFrequency("900"), {
    frequency_mhz: 900,
    frequency_end_mhz: undefined,
  });
  for (const value of ["-900", "0", "30-900", "garbage", "900-NaN", "1e999"])
    assert.throws(() => sourceFrequency(value));
});

test("native beam widths use absolute 3 dB crossings around zero", () => {
  const p = Array.from(
    { length: 360 },
    (_, i) => 2 + Math.min(i, 360 - i) / 10,
  );
  assert.equal(nativeBeamWidth(p), 20); // A peak-relative threshold would give 60°.
  assert.equal(nativeBeamWidth(Array(360).fill(0)), 360);
  assert.equal(nativeBeamWidth(Array(360).fill(4)), 360);
  assert.throws(() => nativeBeamWidth([0, 3]));
  assert.throws(() => nativeBeamWidth([...Array(359).fill(0), NaN]));
});

test("recalculating widths preserves source parameters and skips incomplete patterns", () => {
  const source = newSource([0, 0], "A");
  assert.equal(fillSourceBeamWidths(source), source);
  const p = Array.from({ length: 360 }, (_, i) => Math.min(i, 360 - i));
  const full = { ...source, pattern_h: p, pattern_v: p, beam_width_h_deg: 99 };
  const next = fillSourceBeamWidths(full);
  assert.equal(next.beam_width_h_deg, 6);
  assert.equal(next.beam_width_v_deg, 6);
  assert.equal(next.pattern_h, p);
  assert.equal(next.gain_dbi, source.gain_dbi);
  assert.equal(next.power_w, source.power_w);
});
