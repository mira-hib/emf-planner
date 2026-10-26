import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { exportXemf, nativeProjectExtension } from "../src/lib/export-xemf";
import { writeNativeRecords } from "../src/lib/write-nrbf";
import { readNrbf } from "../src/lib/nrbf";
import { readProject } from "../src/lib/files";
import { newProject, newSource } from "../src/lib/project";
import { importXemf } from "../src/lib/import-xemf";
import { toGeo } from "../src/lib/geometry";
import { nativeLimitAt } from "../src/lib/limits";
const pngBase64 =
  "iVBORw0KGgoAAAANSUhEUgAAAAIAAAABCAYAAAD0In+KAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAA5JREFUGFdj+M/AAEL/AQ/5A/3z4h1AAAAAAElFTkSuQmCC";

test("PRJ-01 PRJ-02 CAL-07 ROB-01: native .xemf preserves the entire model and exact Unicode; checked extension detects corruption", () => {
  const p = newProject();
  p.name = "Projet é 漢字 😀";
  p.sources = [
    {
      ...newSource(p.origin, "عربي"),
      height_m: 12.125,
      power_w: 123.456789,
      color: "#123456",
    },
  ];
  p.report_aliases = { "{Client}": "<A&B> 😀" };
  p.background_image = {
    west: 1,
    east: 2,
    north: 3,
    south: 2,
    opacity: 0.5,
    data_url: `data:image/png;base64,${pngBase64}`,
  };
  const bytes = exportXemf(p);
  assert.equal(bytes[0], 0);
  assert.deepEqual(
    nativeProjectExtension(bytes),
    JSON.parse(JSON.stringify({ ...p, format_version: 2 })),
  );
  assert.deepEqual(readProject("saved.xemf", bytes).sources, p.sources);
  assert.deepEqual(
    readProject("saved.xemf", bytes).background_image,
    p.background_image,
  );
  assert.deepEqual(exportXemf(readProject("saved.xemf", bytes)), bytes);
  const corrupt = bytes.slice();
  corrupt[100] ^= 1;
  assert.throws(() => readProject("bad.xemf", corrupt), /corrompu/);
  const trailer = bytes.slice();
  trailer[trailer.length - 20] ^= 1;
  assert.throws(() => readProject("bad.xemf", trailer), /corrompu/);
});

test("PRJ-01: native core contains real typed settings, source patterns, colours, factors, geometry and aliases", () => {
  const p = newProject();
  p.sources = [
    {
      ...newSource(p.origin, "Émetteur"),
      power_w: 1000,
      height_m: 20,
      color: "#123456",
      limit_factors: [
        [0.2, 0.3, 0.4],
        [1, 0.9, 0.8],
        [1, 1, 1],
        [1, 1, 1],
      ],
    },
  ];
  p.report_aliases = { Nom: "Littéral & <é>" };
  const bytes = exportXemf(p),
    parsed = readNrbf(bytes.buffer);
  assert.equal(parsed.root.__type, "EMFPlanner.Public2+Project");
  const native = importXemf(bytes.buffer, "native.xemf");
  assert.equal(native.sources[0].name, "Émetteur");
  assert.equal(native.sources[0].power_w, 1000);
  assert.equal(native.sources[0].color, "#123456");
  assert.equal(native.sources[0].pattern_h?.length, 360);
  assert.equal(native.sources[0].pattern_v?.length, 360);
  native.sources[0].limit_factors![0].forEach((v, i) =>
    assert.ok(Math.abs(v - [0.2, 0.3, 0.4][i]) < 1e-6),
  );
  assert.deepEqual(native.report_aliases, p.report_aliases);
  for (const f of [11, 100, 400, 900, 2000, 3000, 100000])
    assert.ok(
      Math.abs(
        nativeLimitAt(native.limits[0], f) - nativeLimitAt(p.limits[0], f),
      ) < 1e-4,
    );
});

test("CAL-07 PRJ-01: data-only reader imports a Bitmap and calibrated bounds from a genuine native core", () => {
  const png = Uint8Array.from(
    Buffer.from(
      "iVBORw0KGgoAAAANSUhEUgAAAAIAAAABCAYAAAD0In+KAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAA5JREFUGFdj+M/AAEL/AQ/5A/3z4h1AAAAAAElFTkSuQmCC",
      "base64",
    ),
  );
  const p = newProject(),
    sw = toGeo(0, -50, p.origin),
    ne = toGeo(100, 0, p.origin);
  p.background_image = {
    data_url: `data:image/png;base64,${Buffer.from(png).toString("base64")}`,
    west: sw[1],
    south: sw[0],
    east: ne[1],
    north: ne[0],
    opacity: 0.7,
  };
  const bytes = exportXemf(p, { bytes: png, width: 2, height: 1 }),
    native = importXemf(bytes.buffer, "bitmap.xemf");
  const bg = native.background_image!;
  assert.ok(bg);
  assert.equal(bg.data_url, p.background_image.data_url);
  for (const key of ["west", "east", "north", "south"] as const)
    assert.ok(Math.abs(bg[key] - p.background_image[key]) < 1e-10, key);
});

test("PRJ-01 PRJ-04: native sample cut contours survive native-core export and import without extension", () => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf"),
    p = readProject("Castelfranco.xemf", new Uint8Array(bytes));
  const native = exportXemf(p),
    roundtrip = importXemf(native.buffer, "resaved.xemf");
  assert.equal(roundtrip.sources.length, p.sources.length);
  assert.equal(roundtrip.buildings.length, p.buildings.length);
  assert.equal(
    roundtrip.saved_cut_results?.horizontal.length,
    p.saved_cut_results?.horizontal.length,
  );
  assert.equal(
    roundtrip.saved_cut_results?.vertical.length,
    p.saved_cut_results?.vertical.length,
  );
  const compare = (key: "horizontal" | "vertical") => {
    const a = p.saved_cut_results![key],
      b = roundtrip.saved_cut_results![key];
    a.forEach((r, i) => {
      assert.equal(b[i].contours.segments.length, r.contours.segments.length);
      r.contours.segments.forEach((path, j) =>
        path.forEach((point, k) =>
          point.forEach((v, l) =>
            assert.ok(
              Math.abs(b[i].contours.segments[j][k][l] - v) < 1e-4,
              `${key} ${i}/${j}`,
            ),
          ),
        ),
      );
    });
  };
  compare("horizontal");
  compare("vertical");
});

test("Native .xemf writer rejects values outside native numeric field ranges", () => {
  assert.throws(
    () => writeNativeRecords({ SETT: { ZOOM: Infinity } }),
    /invalide/,
  );
  const p = newProject();
  p.lines = [
    {
      id: "line",
      kind: "road",
      name: "Largeur invalide",
      points: [
        [0, 0],
        [1, 1],
      ],
      width: 256,
      color: "#000000",
    },
  ];
  assert.throws(() => exportXemf(p), /hors limites/);
});

test("PRJ-01: native aliases retain one pair of braces and the same effective values as Word", () => {
  const p = newProject();
  p.report_aliases = { Client: "ancien", ProjectOnly: "conservé" };
  p.settings.report_aliases = { Client: "valeur effective" };
  const bytes = exportXemf(p),
    native = importXemf(bytes.buffer, "aliases.xemf");
  assert.deepEqual(native.report_aliases, {
    Client: "valeur effective",
    ProjectOnly: "conservé",
  });
  const parsed = readNrbf(bytes.buffer),
    settings = parsed.deref(parsed.root.SETT) as Record<string, unknown>;
  const entries = parsed.deref(settings.ALS) as string[];
  assert.equal(entries[0], "{Client}");
  assert.equal(entries[1], "{ProjectOnly}");
  const defaults = exportXemf(newProject()),
    defaultProject = importXemf(defaults.buffer, "new.xemf");
  assert.equal(defaultProject.report_aliases?.ProjectCode, "ProjectCode");
  assert.equal(Object.keys(defaultProject.report_aliases!).length, 6);
});

test("PRJ-01: exporting independent limit frequency intervals preserves their uncovered-frequency fallback", () => {
  const p = newProject();
  p.limits[0].rows = [
    { frequency: 100, value: 2, exponent: 0.5 },
    { frequency: 500, value: 20, exponent: 0 },
  ];
  p.limits[1].rows = [
    { frequency: 1, value: 3, exponent: 0 },
    { frequency: 50, value: 4, exponent: 0 },
    { frequency: 2000, value: 5, exponent: 0 },
  ];
  p.limits[2].rows = [{ frequency: 1000, value: -999, exponent: 0 }];
  const bytes = exportXemf(p),
    native = importXemf(bytes.buffer, "limits.xemf");
  for (let i = 0; i < 4; i++)
    for (const f of [
      0.5, 1, 9, 10, 10.01, 50, 50.01, 100, 100.01, 400, 500, 500.1, 1000, 2000,
      5000, 1000001,
    ]) {
      const expected = nativeLimitAt(p.limits[i], f),
        actual = nativeLimitAt(native.limits[i], f);
      assert.ok(
        Math.abs(actual - expected) / Math.max(1, expected) < 1e-6,
        `${i} at ${f} MHz: ${actual} vs ${expected}`,
      );
    }
});

test("CAL-07 ROB-02: packed image bytes can exceed the object-array count limit without allocating millions of boxed primitives", () => {
  const data = new Uint8Array(2_100_000);
  data[0] = 17;
  data[data.length - 1] = 255;
  const bytes = writeNativeRecords({ RPL: { BTM: { Data: data } } });
  const parsed = readNrbf(bytes.buffer),
    obj = (v: unknown) => parsed.deref(v) as Record<string, unknown>;
  const saved = parsed.deref(obj(obj(parsed.root.RPL).BTM).Data);
  assert.ok(saved instanceof Uint8Array);
  assert.equal(saved.length, data.length);
  assert.equal(saved[0], 17);
  assert.equal(saved[saved.length - 1], 255);
});
