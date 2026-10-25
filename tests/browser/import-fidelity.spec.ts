import { test, expect } from "@playwright/test";
import { readFileSync } from "node:fs";

test("imported contours survive display changes and reload, then invalidate on source edits", async ({
  page,
}) => {
  const errors: string[] = [];
  page.on("pageerror", (error) => errors.push(error.message));
  const navigate = async (name: string) =>
    page
      .getByRole("navigation")
      .getByRole("button", { name, exact: true })
      .click();
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles("tests/fixtures/original-sample.xemf");
  await navigate("Coupes verticales");
  await expect(page.locator(".field-plot [data-field-contour]")).toHaveCount(4);
  await expect(
    page.getByText("Contours enregistrés dans le projet d’origine.", {
      exact: false,
    }),
  ).toBeVisible();
  await navigate("Tableaux et graphiques");
  await expect(
    page.getByRole("button", {
      name: "Tableaux de champ des coupes verticales",
      exact: true,
    }),
  ).toBeEnabled();
  await navigate("CAO");
  await expect(
    page.getByRole("button", { name: "Exporter DWG", exact: true }),
  ).toBeDisabled();
  await expect(
    page.getByRole("button", { name: "Exporter DXF", exact: true }),
  ).toBeEnabled();
  await page
    .getByRole("checkbox", { name: "Bâtiments", exact: true })
    .uncheck();
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  const path = test.info().outputPath("imported-contours.emfp");
  await (await download).saveAs(path);
  const saved = JSON.parse(readFileSync(path, "utf8"));
  expect(saved.saved_cut_results.horizontal).toHaveLength(4);
  expect(saved.saved_cut_results.vertical).toHaveLength(24);
  expect(saved.cad_options.layers.buildings).toBe(false);
  page.on("dialog", (dialog) => dialog.dismiss());
  await page.reload();
  await navigate("Coupes verticales");
  await expect(page.locator(".field-plot [data-field-contour]")).toHaveCount(4);
  await navigate("Sources rayonnantes");
  const power = page
    .getByRole("region", { name: "Sources rayonnantes", exact: true })
    .getByRole("textbox", { name: "Puissance TX, W", exact: true })
    .first();
  await power.fill("13");
  await power.press("Tab");
  await navigate("Coupes verticales");
  await expect(page.locator(".field-plot [data-field-contour]")).toHaveCount(0);
  await page
    .getByRole("button", {
      name: "Annuler la dernière modification",
      exact: true,
    })
    .click();
  await expect(page.locator(".field-plot [data-field-contour]")).toHaveCount(4);
  await navigate("Paramètres");
  await expect(
    page.getByRole("button", { name: "Paramètres proxy", exact: true }),
  ).toBeEnabled();
  await expect(
    page.getByRole("button", { name: "Langue de l’interface", exact: true }),
  ).toBeDisabled();
  expect(errors).toEqual([]);
});
