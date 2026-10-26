import { test, expect } from "@playwright/test";
import { readFileSync } from "node:fs";
import { readProject } from "../../src/lib/files";
import { importXemf } from "../../src/lib/import-xemf";
import { newProject, newSource } from "../../src/lib/project";
import { toGeo } from "../../src/lib/geometry";

test("PRJ-01 PRJ-04: save the original sample as native .xemf and reopen its sources, buildings and archived cuts", async ({
  page,
}) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles("tests/fixtures/original-sample.xemf");
  await expect(page.locator(".project-count")).toContainText("9 sources");
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Rapport par modèle", exact: true })
    .click();
  await expect(
    page.getByRole("textbox", { name: "Valeur SiteName", exact: true }),
  ).toHaveValue("Castelfranco Emilia TAV2");
  const wait = page.waitForEvent("download");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page
    .getByRole("button", {
      name: "Enregistrer au format original .xemf",
      exact: true,
    })
    .click();
  const download = await wait;
  expect(download.suggestedFilename()).toMatch(/\.xemf$/);
  const file = test.info().outputPath("original-native-saved.xemf");
  await download.saveAs(file);
  const bytes = new Uint8Array(readFileSync(file)),
    p = readProject("saved.xemf", bytes),
    core = importXemf(bytes.buffer, "core.xemf");
  expect(core.sources).toHaveLength(9);
  expect(core.buildings).toHaveLength(13);
  expect(core.saved_cut_results?.horizontal).toHaveLength(4);
  expect(core.saved_cut_results?.vertical).toHaveLength(24);
  expect(p.saved_cut_results?.horizontal).toHaveLength(4);
  await page.reload();
  const open = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await open).setFiles(file);
  await expect(page.locator(".project-count")).toContainText("9 sources");
  await page.screenshot({
    path: test.info().outputPath("native-reopened.png"),
  });
  await expect(page.getByRole("alert")).toHaveCount(0);
});

test("CAL-07 PRJ-01: native saving embeds a real calibrated PNG and retains exact alias values", async ({
  page,
}) => {
  const p = newProject();
  p.sources = [
    {
      ...newSource(p.origin, "Source é 漢字"),
      power_w: 1000,
      height_m: 20,
      color: "#123456",
    },
  ];
  p.report_aliases = { Client: "<A&B> 😀" };
  const sw = toGeo(0, -50, p.origin),
    ne = toGeo(100, 0, p.origin);
  p.background_image = {
    data_url:
      "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAIAAAABCAYAAAD0In+KAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAA5JREFUGFdj+M/AAEL/AQ/5A/3z4h1AAAAAAElFTkSuQmCC",
    west: sw[1],
    south: sw[0],
    east: ne[1],
    north: ne[0],
    opacity: 0.5,
  };
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (
    await chooser
  ).setFiles({
    name: "calibrated.emfp",
    mimeType: "application/json",
    buffer: Buffer.from(JSON.stringify(p)),
  });
  await expect(page.locator(".project-count")).toContainText("1 sources");
  const wait = page.waitForEvent("download");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page
    .getByRole("button", {
      name: "Enregistrer au format original .xemf",
      exact: true,
    })
    .click();
  const file = test.info().outputPath("calibrated-native.xemf");
  await (await wait).saveAs(file);
  const bytes = new Uint8Array(readFileSync(file)),
    reopened = readProject("calibrated.xemf", bytes),
    core = importXemf(bytes.buffer, "core.xemf");
  expect(reopened.background_image).toEqual(p.background_image);
  expect(reopened.report_aliases).toEqual(p.report_aliases);
  expect(core.background_image?.data_url).toMatch(/^data:image\/png;base64,/);
  expect(core.report_aliases).toEqual(p.report_aliases);
  for (const k of ["west", "east", "north", "south"] as const)
    expect(core.background_image![k]).toBeCloseTo(p.background_image[k], 9);
  await expect(page.getByRole("alert")).toHaveCount(0);
});
