import test from "node:test";
import assert from "node:assert/strict";
import type { StoredAntenna } from "../src/types/index.ts";
import { envelopeForSource, feederLoss } from "../src/lib/editing.ts";
import { decodeMsi, parseMsi } from "../src/lib/antenna.ts";
import feeders from "../src/data/feeders.json";

test("MSI names decode both the native Windows-1251 files and modern UTF-8 files", () => {
  const name = Uint8Array.from([0xc0, 0xed, 0xf2, 0xe5, 0xed, 0xed, 0xe0]);
  const prefix = new TextEncoder().encode("NAME ");
  const suffix = new TextEncoder().encode(
    "\nGAIN 10 dBi\nHORIZONTAL 2\n0 0\n180 30\nVERTICAL 2\n0 0\n180 30",
  );
  const bytes = new Uint8Array(prefix.length + name.length + suffix.length);
  bytes.set(prefix);
  bytes.set(name, prefix.length);
  bytes.set(suffix, prefix.length + name.length);
  assert.equal(parseMsi(decodeMsi(bytes)).name, "Антенна");
  const utf8 = new TextEncoder().encode(
    "NAME Antenne été\n" + new TextDecoder().decode(suffix),
  );
  assert.equal(parseMsi(decodeMsi(utf8)).name, "Antenne été");
});

function patterns(): StoredAntenna[] {
  const a = {
    id: "a",
    name: "A",
    gain: 10,
    horizontal: Array(360).fill(30),
    vertical: Array(360).fill(5),
  };
  const b = {
    id: "b",
    name: "B",
    gain: 20,
    horizontal: Array(360).fill(40),
    vertical: Array(360).fill(12),
  };
  a.horizontal[0] = 5;
  a.horizontal[90] = 15;
  b.horizontal[0] = 20;
  b.horizontal[90] = 10;
  b.vertical[180] = 2;
  return [a, b];
}

test("native envelope normalizes every input plane and ignores gains by default", () => {
  const inputs = patterns();
  const before = JSON.stringify(inputs);
  const result = envelopeForSource(inputs, "Enveloppe", 7);
  assert.equal(result.gain, 7);
  assert.equal(result.horizontal[0], 0);
  assert.equal(result.horizontal[90], 0);
  assert.equal(result.horizontal[180], 25);
  assert.deepEqual(result.vertical, Array(360).fill(0));
  assert.equal(JSON.stringify(inputs), before);
});

test("native envelope applies edited pattern gains while preserving the source gain", () => {
  const inputs = patterns();
  const result = envelopeForSource(inputs, "Enveloppe", 7, true);
  assert.equal(result.gain, 7);
  assert.equal(result.horizontal[0], 10);
  assert.equal(result.horizontal[90], 0);
  assert.equal(result.horizontal[180], 30);
  assert.equal(result.vertical[0], 10);
  assert.equal(result.vertical[180], 0);
  inputs[1].gain = 0;
  const edited = envelopeForSource(inputs, "Enveloppe", 7, true);
  assert.equal(edited.horizontal[0], 0);
  assert.equal(edited.horizontal[90], 10);
  assert.equal(edited.gain, 7);
});

test("native envelope retains its -60 dB working floor and rejects unusable samples", () => {
  const input = patterns()[0];
  input.horizontal.fill(200);
  input.horizontal[0] = 0;
  assert.equal(envelopeForSource([input], "E", 3).horizontal[1], 60);
  assert.equal(envelopeForSource([input], "E", 3, true).horizontal[1], 70);
  assert.throws(() => envelopeForSource([], "E", 3));
  assert.throws(() => envelopeForSource([input], "E", NaN));
  input.vertical[3] = NaN;
  assert.throws(() => envelopeForSource([input], "E", 3));
});

test("the native cable checkbox excludes all connector losses without changing legacy manual mode", () => {
  const cable = feeders[0].points;
  const legacy = feederLoss(cable, 900, 100, cable, "0", 4, 0.1, 0, false);
  assert.equal(legacy.connectors, 4);
  assert.equal(legacy.total, 19.203);
  const excluded = feederLoss(
    cable,
    900,
    100,
    cable,
    "0",
    4,
    0.1,
    0,
    false,
    false,
  );
  assert.equal(excluded.connectors, 0);
  assert.equal(excluded.total, 18.803);
  assert.equal(
    feederLoss(cable, 900, 100, cable, "0", 4, NaN, 0, true, false).total,
    18.803,
  );
});
