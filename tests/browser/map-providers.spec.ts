import { test, expect, type Page } from "@playwright/test";
import { TILES } from "../../src/lib/map-providers";
import { readFileSync } from "node:fs";

const png = Buffer.from(
  "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+aHe0AAAAASUVORK5CYII=",
  "base64",
);
const selector = (page: Page) =>
  page.getByRole("combobox", { name: "Fond de carte", exact: true });
const requested: string[] = [];
test.beforeEach(async ({ page }) => {
  requested.length = 0;
  await page.route("**/*", async (route) => {
    const url = route.request().url();
    if (/^https?:\/\//.test(url) && !url.startsWith("http://127.0.0.1:1420")) {
      requested.push(url);
      await route.fulfill({ contentType: "image/png", body: png });
    } else await route.continue();
  });
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Origine et grille", exact: true })
    .click();
  await page
    .getByRole("checkbox", { name: "Fond de carte", exact: true })
    .check();
});

test("complete native list selects each tile service using its own URL", async ({
  page,
}) => {
  await expect(selector(page).locator("option")).toHaveText(Object.keys(TILES));
  for (const name of [
    "OpenStreetMap",
    "Carto Basemap",
    "Google Map",
    "Google Satellite",
    "Esri Satellite",
    "OpenTopoMap",
    "Bing Sat",
    "2GIS",
    "Yandex map",
    "Yandex Sat",
  ]) {
    requested.length = 0;
    await selector(page).selectOption(name);
    const expected = new URL(TILES[name].url).hostname;
    await expect
      .poll(async () =>
        page
          .locator(".leaflet-tile")
          .first()
          .getAttribute("src")
          .then((src) => (src ? new URL(src).hostname : "")),
      )
      .toBe(expected);
    const sources = await page
      .locator(".leaflet-tile")
      .evaluateAll((images) =>
        images.map((image) => image.getAttribute("src") ?? ""),
      );
    expect(
      sources.every((url) => !/\[[^\]]+\]|\{[^}]+\}/.test(url)),
    ).toBeTruthy();
    await expect(page.locator(".leaflet-tile-loaded").first()).toBeVisible();
    await expect(page.locator(".map-provider-status")).toHaveCount(0);
    await expect(selector(page)).toHaveValue(name);
  }
});

test("credentialed services explain configuration and calibrated image sends no tile requests", async ({
  page,
}) => {
  await expect(page.locator(".leaflet-tile-loaded").first()).toBeVisible();
  for (const name of [
    "OSM Relief",
    "HERE WeGo Satellite",
    "Mapquest Satellite",
  ]) {
    await selector(page).selectOption(name);
    await expect(page.locator(".map-provider-status")).toContainText(
      "Fonds de carte personnalisés",
    );
    await expect(page.locator(".leaflet-tile")).toHaveCount(0);
  }
  await selector(page).selectOption("Calibrated map image");
  await expect(page.locator(".leaflet-tile")).toHaveCount(0);
  await expect(page.locator(".map-provider-status")).toHaveCount(0);
});

test("failed tile loads report an error without replacing the selected source", async ({
  page,
}) => {
  await page.route("https://mt2.google.com/**", (route) => route.abort());
  await selector(page).selectOption("Google Satellite");
  await expect(page.locator(".map-provider-status")).toContainText(
    "Tuiles indisponibles",
  );
  await expect(selector(page)).toHaveValue("Google Satellite");
  await expect(page.locator(".leaflet-tile").first()).toHaveAttribute(
    "src",
    /mt2\.google\.com/,
  );
  await selector(page).selectOption("OpenStreetMap");
  await expect(page.locator(".leaflet-tile-loaded").first()).toBeVisible();
  await expect(page.locator(".map-provider-status")).toHaveCount(0);
});

test("native same-name provider customization applies and survives project saving", async ({
  page,
}) => {
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Paramètres", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Fonds de carte personnalisés", exact: true })
    .click();
  const dialog = page.getByRole("dialog");
  await dialog
    .getByRole("button", { name: "Ajouter un fond", exact: true })
    .click();
  await dialog
    .getByRole("textbox", { name: "Nom", exact: true })
    .fill("HERE WeGo Satellite");
  await dialog
    .getByRole("textbox", { name: "URL des tuiles", exact: true })
    .fill("https://tiles.example.invalid/[Z]/[X]/[Y].png?apiKey=user-test");
  await dialog
    .getByRole("textbox", { name: "Attribution", exact: true })
    .fill("My HERE account");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Origine et grille", exact: true })
    .click();
  await selector(page).selectOption("HERE WeGo Satellite");
  await expect
    .poll(() =>
      requested.some((url) => url.startsWith("https://tiles.example.invalid/")),
    )
    .toBeTruthy();
  await expect(page.locator(".leaflet-tile-loaded").first()).toBeVisible();
  await expect(page.locator(".map-provider-status")).toHaveCount(0);
  await expect(page.locator(".leaflet-control-attribution")).toContainText(
    "My HERE account",
  );
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  const saved = test.info().outputPath("custom-provider.emfp");
  await (await download).saveAs(saved);
  const p = JSON.parse(readFileSync(saved, "utf8"));
  expect(p.settings.map_provider).toBe("HERE WeGo Satellite");
  expect(p.custom_maps[0]).toEqual({
    name: "HERE WeGo Satellite",
    url: "https://tiles.example.invalid/[Z]/[X]/[Y].png?apiKey=user-test",
    attribution: "My HERE account",
  });
});
