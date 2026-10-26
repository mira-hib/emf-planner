import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import {
  TILES,
  mapProviders,
  normalizeMapProvider,
  selectedMapProvider,
  tileQuadkey,
  tileUrl,
  geographicTile,
  validateMapTemplate,
} from "../src/lib/map-providers.ts";
import { newProject } from "../src/lib/project.ts";
import { importXemf } from "../src/lib/import-xemf.ts";

test("native menu order exactly follows the packaged list with calibrated image first", () => {
  const lines = readFileSync(
    "extracted/original/app/CustomBaseMaps.set",
    "utf8",
  )
    .trim()
    .split(/\r?\n/);
  assert.deepEqual(Object.keys(TILES), [
    "Calibrated map image",
    ...lines.map((line) => line.split("\t")[0].trim()),
  ]);
  assert.equal(TILES["Yandex map"].projection, "ellipsoidal");
  assert.equal(TILES["Yandex Sat"].projection, "ellipsoidal");
});

test("native URL substitutions cover quadkeys, inverted zoom, cache folders and server randomization", () => {
  assert.equal(tileQuadkey(3, 5, 3), "213");
  assert.equal(tileQuadkey(0, 0, 0), "");
  const template =
    "https://[RND(a,b,c)]/[Z]/[X]/[Y]/[17-Z]/[Z+1]/[FX]/[FY]/[RES]?g=[Gagarin.Substring(0,RND(0,8))]";
  assert.equal(
    tileUrl(template, 3, 5, 3, () => 0.5),
    "https://b/3/3/5/14/4/0/0/213?g=Gaga",
  );
  assert.equal(
    tileUrl("https://tiles/{z}/{x}/{y}/{-y}/{s}{r}", 3, 5, 3),
    "https://tiles/3/3/5/2/c",
  );
  assert.equal(tileUrl("[FX]/[FY]", 2049, 3072, 12), "2/3");
});

test("provider selection preserves a named satellite source and never substitutes Esri", () => {
  const p = newProject();
  p.settings.map_provider = "Google Satellite";
  assert.match(selectedMapProvider(p)!.url, /google\.com.*lyrs=s/);
  p.settings.map_provider = "Unknown server";
  assert.equal(selectedMapProvider(p), undefined);
  p.settings.map_provider = "Carto clair";
  assert.equal(selectedMapProvider(p), TILES["Carto Basemap"]);
  assert.equal(normalizeMapProvider("Satellite Esri"), "Esri Satellite");
});

test("custom same-name entries replace built-in endpoints and clear configuration requirements", () => {
  const p = newProject();
  p.custom_maps = [
    {
      name: "OSM Relief",
      url: "https://tiles.example/{z}/{x}/{y}.png?apikey=user",
      attribution: "User account",
    },
  ];
  const provider = mapProviders(p)["OSM Relief"];
  assert.equal(provider.configuration, undefined);
  assert.match(provider.url, /apikey=user/);
  assert.equal(provider.attribution, "User account");
  assert.equal(Object.keys(mapProviders(p)).length, 14);
  assert.equal(TILES["HERE WeGo Satellite"].url, "");
  assert.equal(TILES["Mapquest Satellite"].url, "");
});

test("custom map validation accepts native and Leaflet schemas, rejects incomplete schemas", () => {
  for (const template of [
    "https://example/{z}/{x}/{y}",
    "https://example/[Z]/[X]/[Y]",
    "https://example/a[RES].jpeg",
    "https://example/{quadkey}.jpeg",
    TILES["Google Map"].url,
  ])
    validateMapTemplate(template);
  assert.throws(() => validateMapTemplate("https://example/{x}"));
  assert.throws(() => validateMapTemplate("file:///{z}/{x}/{y}"));
});

test("Yandex projection matches native ellipsoidal Mercator, preventing spherical misalignment", () => {
  const spherical = geographicTile(45, 0, 10);
  const yandex = geographicTile(45, 0, 10, "ellipsoidal");
  assert.equal(yandex[0], 512);
  // Independent evaluation of CalcTileWeb.CoordToTileNew's rounded native constants.
  assert.ok(Math.abs(yandex[1] - 369.1307626371508) < 2e-6);
  assert.ok(Math.abs(yandex[1] - spherical[1]) * 256 > 190);
  assert.ok(Math.abs(geographicTile(0, 0, 0, "ellipsoidal")[1] - 0.5) < 1e-14);
});

test("xemf import preserves its actual native provider", () => {
  const data = readFileSync("tests/fixtures/original-sample.xemf");
  const p = importXemf(
    data.buffer.slice(data.byteOffset, data.byteOffset + data.byteLength),
    "basic.xemf",
  );
  assert.ok(Object.hasOwn(TILES, p.settings.map_provider));
});
