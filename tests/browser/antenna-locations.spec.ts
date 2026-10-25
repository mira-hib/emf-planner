import { test, expect, type Page } from "@playwright/test";
import { readFileSync } from "node:fs";
import { newProject, newSource } from "../../src/lib/project";
import { antennaLocalToGeo } from "../../src/lib/antenna-locations";
import { toLocal } from "../../src/lib/geometry";
import type { PlannerProject } from "../../src/types";

const nav = (page: Page, name: string) =>
  page.getByRole("navigation").getByRole("button", { name, exact: true });
const button = (page: Page, name: string) =>
  page.getByRole("button", { name, exact: true });
async function saved(page: Page) {
  const wait = page.waitForEvent("download");
  await button(page, "Enregistrer le projet").click();
  const file = test.info().outputPath(`positions-${Date.now()}.emfp`);
  await (await wait).saveAs(file);
  return JSON.parse(readFileSync(file, "utf8")) as PlannerProject;
}
async function seed(page: Page) {
  const p = newProject();
  p.settings.show_basemap = false;
  p.sources = [0, 1, 2].map((i) => {
    const [lat, lng] = antennaLocalToGeo(p, 20 + 30 * i, 30);
    return {
      ...newSource(p.origin, `Source ${i + 1}`),
      id: `s${i}`,
      antenna_model: `Model ${i}`,
      lat,
      lng,
      x_m: 20 + 30 * i,
      y_m: 30,
    };
  });
  await page.addInitScript(
    (p) => localStorage.setItem("emf-planner-autosave-v2", JSON.stringify(p)),
    p,
  );
  await page.goto("/");
  await nav(page, "Positions des antennes").click();
  return p;
}
test("native seven columns, selected-only moves, cancel, undo and capture persistence", async ({
  page,
}) => {
  const before = await seed(page),
    grid = page.getByRole("region", {
      name: "Positions des antennes",
      exact: true,
    });
  await expect(grid.getByRole("columnheader")).toHaveText([
    "",
    "Source name",
    "Latitude, deg",
    "Longitude, deg.",
    "X, m",
    "Y, m",
    "H, m",
    "Azimuth, deg.",
  ]);
  await expect(
    grid.getByRole("textbox", { name: "Latitude, deg", exact: true }),
  ).toHaveCount(0);
  await expect(
    grid.getByText("Source 1 (Model 0)", { exact: true }),
  ).toBeVisible();
  await grid
    .getByRole("rowheader", { name: "Sélectionner la source 1", exact: true })
    .click();
  await grid
    .getByRole("rowheader", { name: "Sélectionner la source 3", exact: true })
    .click({ modifiers: ["Control"] });
  page.once("dialog", (d) => d.dismiss());
  await button(page, "Antennes sélectionnées à l’origine").click();
  expect((await saved(page)).sources).toEqual(before.sources);
  page.once("dialog", (d) => d.accept());
  await button(page, "Antennes sélectionnées à l’origine").click();
  const after = await saved(page);
  for (const i of [0, 2])
    expect([after.sources[i].lat, after.sources[i].lng]).toEqual(before.origin);
  expect(after.sources[1]).toEqual(before.sources[1]);
  await button(page, "Annuler la dernière modification").click();
  expect((await saved(page)).sources).toEqual(before.sources);
  await page
    .getByRole("slider", { name: "Maximum capture distance" })
    .fill("27");
  expect((await saved(page)).settings.antenna_capture_distance).toBe(27);
  await page.screenshot({
    path: "reference/antenna-locations-interface/locations-selected.png",
  });
});
test("multi-row X/Y editing changes only the chosen coordinate and keeps source metadata", async ({
  page,
}) => {
  const before = await seed(page),
    grid = page.getByRole("region", {
      name: "Positions des antennes",
      exact: true,
    });
  await grid
    .getByRole("rowheader", { name: "Sélectionner la source 1", exact: true })
    .click();
  await grid
    .getByRole("rowheader", { name: "Sélectionner la source 2", exact: true })
    .click({ modifiers: ["Control"] });
  const x = grid.getByRole("textbox", { name: "X, m", exact: true }).first();
  await x.fill("125,5");
  await x.press("Tab");
  const after = await saved(page);
  for (const i of [0, 1]) {
    expect(after.sources[i].x_m).toBe(125.5);
    expect(after.sources[i].y_m).toBe(30);
    expect(after.sources[i].antenna_model).toBe(
      before.sources[i].antenna_model,
    );
    expect(after.sources[i].height_m).toBe(before.sources[i].height_m);
  }
  expect(after.sources[2]).toEqual(before.sources[2]);
});
test("map drag moves one source and can be undone; map centering and origin use geographic anchors", async ({
  page,
}) => {
  const before = await seed(page),
    handle = button(page, "Déplacer l’antenne 2");
  const box = await handle.boundingBox();
  expect(box).not.toBeNull();
  // The group includes a long azimuth line, so target the circle itself.
  const circle = handle.locator("circle"),
    c = await circle.boundingBox();
  expect(c).not.toBeNull();
  await page.mouse.move(c!.x + c!.width / 2, c!.y + c!.height / 2);
  await page.mouse.down();
  await page.mouse.move(c!.x + c!.width / 2 + 60, c!.y + c!.height / 2 - 30, {
    steps: 8,
  });
  await page.mouse.up();
  const after = await saved(page);
  expect(after.sources[1].lng).not.toBe(before.sources[1].lng);
  expect(after.sources[0]).toEqual(before.sources[0]);
  expect(after.sources[2]).toEqual(before.sources[2]);
  await button(page, "Annuler la dernière modification").click();
  expect((await saved(page)).sources).toEqual(before.sources);
  await page
    .getByRole("rowheader", { name: "Sélectionner la source 2", exact: true })
    .click();
  await button(page, "Centrer sur l’antenne").click();
  page.once("dialog", (d) => d.accept());
  await button(page, "Origine sur l’antenne active").click();
  const rebased = await saved(page);
  expect(rebased.origin).toEqual([
    before.sources[1].lat,
    before.sources[1].lng,
  ]);
  expect(rebased.sources.map((s) => [s.lat, s.lng])).toEqual(
    before.sources.map((s) => [s.lat, s.lng]),
  );
  expect(
    Math.hypot(
      ...toLocal(
        rebased.sources[1].lat,
        rebased.sources[1].lng,
        rebased.origin,
      ),
    ),
  ).toBe(0);
});

test("Castelfranco positions preserve nine sources; Leaflet drag edits the selected source", async ({
  page,
}) => {
  await seed(page);
  page.on("dialog", (d) => d.accept());
  const chooser = page.waitForEvent("filechooser");
  await button(page, "Fichier").click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles("tests/fixtures/original-sample.xemf");
  await expect(page.locator(".status-message")).toHaveText("Projet ouvert");
  await nav(page, "Positions des antennes").click();
  const before = await saved(page);
  expect(before.sources).toHaveLength(9);
  const grid = page.getByRole("region", {
    name: "Positions des antennes",
    exact: true,
  });
  await expect(grid.locator("tbody tr")).toHaveCount(9);
  await page
    .getByRole("checkbox", { name: "Fond de carte", exact: true })
    .uncheck();
  await page.screenshot({
    path: "reference/antenna-locations-interface/locations-castelfranco.png",
  });
  await page.route("https://tile.openstreetmap.org/**", (route) =>
    route.fulfill({
      contentType: "image/png",
      body: Buffer.from(
        "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+aHe0AAAAASUVORK5CYII=",
        "base64",
      ),
    }),
  );
  await page
    .getByRole("combobox", { name: "Fond de carte", exact: true })
    .selectOption("OpenStreetMap");
  await page
    .getByRole("checkbox", { name: "Fond de carte", exact: true })
    .check();
  await grid
    .getByRole("rowheader", { name: "Sélectionner la source 1", exact: true })
    .click();
  await button(page, "Centrer sur l’antenne").click();
  const marker = page.locator(".antenna-leaflet-handle").first(),
    box = await marker.boundingBox();
  expect(box).not.toBeNull();
  await page.mouse.move(box!.x + box!.width / 2, box!.y + box!.height / 2);
  await page.mouse.down();
  await page.mouse.move(
    box!.x + box!.width / 2 + 60,
    box!.y + box!.height / 2 + 30,
    { steps: 8 },
  );
  await page.mouse.up();
  const after = await saved(page);
  expect(after.sources[0].lng).not.toBe(before.sources[0].lng);
  expect(after.sources.slice(1)).toEqual(before.sources.slice(1));
});

test("native cell and column selections edit only selected cells and columns can be reordered", async ({
  page,
}) => {
  const before = await seed(page),
    grid = page.getByRole("region", {
      name: "Positions des antennes",
      exact: true,
    });
  const x = grid.getByRole("textbox", { name: "X, m", exact: true });
  await x.nth(0).click();
  await x.nth(2).click({ modifiers: ["Control"] });
  await x.nth(0).fill("111");
  await x.nth(0).press("Tab");
  const after = await saved(page);
  expect(after.sources[0].x_m).toBe(111);
  expect(after.sources[2].x_m).toBe(111);
  expect(after.sources[1]).toEqual(before.sources[1]);
  await expect(
    button(page, "Antennes sélectionnées à l’origine"),
  ).toBeDisabled();
  await grid.getByRole("columnheader", { name: "Y, m", exact: true }).click();
  const y = grid.getByRole("textbox", { name: "Y, m", exact: true }).first();
  await y.fill("47");
  await y.press("Tab");
  expect((await saved(page)).sources.map((s) => s.y_m)).toEqual([47, 47, 47]);
  await grid
    .getByRole("columnheader", { name: "X, m", exact: true })
    .dragTo(
      grid.getByRole("columnheader", { name: "Source name", exact: true }),
    );
  await expect(grid.getByRole("columnheader").nth(1)).toHaveText("X, m");
  await expect(grid.getByRole("columnheader").nth(2)).toHaveText("Source name");
});
