import { test, expect, type Page } from "@playwright/test";
import { readFileSync } from "node:fs";
import type { PlannerProject } from "../../src/types";

const button = (page: Page, name: string) =>
  page.getByRole("button", { name, exact: true });
const table = (page: Page) =>
  page.getByRole("region", {
    name: "Tableau commun des limites EMF",
    exact: true,
  });
const field = (page: Page, name: string) =>
  page.getByLabel(name, { exact: true });
async function save(page: Page) {
  const download = page.waitForEvent("download");
  await button(page, "Enregistrer le projet").click();
  const path = test.info().outputPath(`limits-${Date.now()}.emfp`);
  await (await download).saveAs(path);
  return {
    path,
    project: JSON.parse(readFileSync(path, "utf8")) as PlannerProject,
  };
}
async function open(page: Page, path: string) {
  const chooser = page.waitForEvent("filechooser");
  await button(page, "Fichier").click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles(path);
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Limites EMF", exact: true })
    .click();
}
async function set(page: Page, label: string, value: string) {
  await field(page, label).fill(value);
  await field(page, label).press("Tab");
}
test.beforeEach(async ({ page }) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Limites EMF", exact: true })
    .click();
});

test("shared intervals edit, validate, delete and undo across the four saved limits", async ({
  page,
}) => {
  const errors: string[] = [];
  page.on("pageerror", (error) => errors.push(error.message));
  await expect(table(page).locator("tbody tr")).toHaveCount(3);
  await expect(page.locator('[data-limit-curve="1"] line')).toHaveCount(3);
  await expect(page.locator('[data-limit-curve="3"] line')).toHaveCount(0);
  await set(page, "Fréquence intervalle 2, MHz", "1800");
  await set(page, "Coefficient limites 1, intervalle 1", "29,5");
  let saved = await save(page);
  expect(saved.project.limits.map((l) => l.rows[1].frequency)).toEqual([
    1800, 1800, 1800, 1800,
  ]);
  expect(saved.project.limits[0].rows[0].value).toBe(29.5);
  expect(saved.project.limits[1].rows[0].value).toBe(61);
  await set(page, "Exposant limites 1, intervalle 2", "3.01");
  await expect(field(page, "Exposant limites 1, intervalle 2")).toHaveValue("");
  await expect(page.locator('[data-limit-curve="1"] line')).toHaveCount(0);
  await expect(
    page.getByLabel("État des quatre jeux de limites"),
  ).toContainText("incomplet");
  await set(page, "Exposant limites 1, intervalle 2", "0.5");
  await expect(page.locator('[data-limit-curve="1"] line')).toHaveCount(3);
  await button(page, "Ajouter un intervalle").click();
  await expect(table(page).locator("tbody tr")).toHaveCount(4);
  await expect(field(page, "Fréquence intervalle 4, MHz")).toHaveValue("");
  await table(page).focus();
  await table(page).press("Delete");
  await expect(table(page).locator("tbody tr")).toHaveCount(3);
  await button(page, "Annuler la dernière modification").click();
  await expect(table(page).locator("tbody tr")).toHaveCount(4);
  await button(page, "Rétablir").click();
  await expect(table(page).locator("tbody tr")).toHaveCount(3);
  saved = await save(page);
  await open(page, saved.path);
  expect((await save(page)).project.limits).toEqual(saved.project.limits);
  expect(errors).toEqual([]);
});

test("historical presets confirm replacing all four sets while preserving colors and factor choices", async ({
  page,
}) => {
  await page
    .getByRole("checkbox", { name: "Utiliser Ftdc", exact: true })
    .first()
    .uncheck();
  await field(page, "Couleur limites 1").fill("#123456");
  const before = (await save(page)).project.limits;
  page.once("dialog", (dialog) => dialog.dismiss());
  await button(page, "Préréglage : Italie").click();
  expect((await save(page)).project.limits).toEqual(before);
  page.once("dialog", (dialog) => dialog.accept());
  await button(page, "Préréglage : Italie").click();
  await expect(table(page).locator("tbody tr")).toHaveCount(1);
  const italian = (await save(page)).project.limits;
  expect(italian.map((l) => l.rows[0].value)).toEqual([6, 20, 4.5, 3]);
  expect(italian[0].color).toBe("#123456");
  expect(italian[0].use_ftdc).toBe(false);
  expect(italian.map((l) => l.id)).toEqual(before.map((l) => l.id));
  await set(
    page,
    "Description limites 1",
    "Description personnalisée\nDeuxième ligne",
  );
  page.once("dialog", (dialog) => dialog.accept());
  await button(page, "Préréglage : Grèce").click();
  await expect(field(page, "Description limites 1")).toHaveValue(
    "Description personnalisée\nDeuxième ligne",
  );
  page.once("dialog", (dialog) => dialog.accept());
  await button(page, "Préréglage : ICNIRP").click();
  await expect(table(page).locator("tbody tr")).toHaveCount(3);
  await expect(field(page, "Coefficient limites 3, intervalle 1")).toHaveValue(
    "",
  );
  const saved = await save(page);
  await open(page, saved.path);
  expect((await save(page)).project.limits).toEqual(saved.project.limits);
  await page.screenshot({ path: test.info().outputPath("limits-icnirp.png") });
});

test("Castelfranco limits retain their native table and chart after import, resizing and round trip", async ({
  page,
}) => {
  await open(page, "tests/fixtures/original-sample.xemf");
  const imported = (await save(page)).project;
  await expect(page.locator('[data-limit-curve="1"] line')).toHaveCount(1);
  expect(imported.limits.map((l) => l.rows[0].value)).toEqual([20, 6, 4.5, 3]);
  expect(imported.limits.map((l) => l.color)).toEqual([
    "#ff0000",
    "#ff8000",
    "#0000ff",
    "#008000",
  ]);
  await page.screenshot({
    path: test.info().outputPath("limits-castelfranco.png"),
  });
  await page.setViewportSize({ width: 1000, height: 800 });
  await expect(field(page, "Description limites 4")).toBeVisible();
  const dimensions = await page
    .locator(".limits-page")
    .evaluate((el) => ({ width: el.clientWidth, scroll: el.scrollWidth }));
  expect(dimensions.scroll).toBeLessThanOrEqual(dimensions.width + 1);
  await page.screenshot({ path: test.info().outputPath("limits-narrow.png") });
  const saved = await save(page);
  await open(page, saved.path);
  expect((await save(page)).project.limits).toEqual(imported.limits);
});
