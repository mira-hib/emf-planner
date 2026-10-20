import { test, expect } from "@playwright/test";
import { existsSync, readFileSync } from "node:fs";
const sample = "tests/fixtures/original-sample.xemf";
test.beforeEach(async ({ page }) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
});
test("source editing, calculation, save and reopen", async ({ page }) => {
  const errors: string[] = [];
  page.on("pageerror", (e) => errors.push(e.message));
  await page
    .getByRole("button", { name: "Ajouter une source", exact: true })
    .click();
  const sources = page.getByRole("region", {
    name: "Sources rayonnantes",
    exact: true,
  });
  await sources
    .getByRole("textbox", { name: "Nom de la source", exact: true })
    .fill("Source de test");
  await sources
    .getByRole("textbox", { name: "Nom de la source", exact: true })
    .press("Tab");
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Calculs aux points", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Ajouter un point au centre", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Calculer les points", exact: true })
    .click();
  await expect(
    page.getByRole("dialog", {
      name: "Résultats du calcul aux points",
      exact: true,
    }),
  ).toBeVisible();
  await page.getByRole("button", { name: "Fermer les résultats" }).click();
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  const file = await download;
  await file.saveAs(".tools/test-results/roundtrip.emfp");
  await expect(page.getByRole("status")).toHaveText("Projet enregistré");
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles(".tools/test-results/roundtrip.emfp");
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Sources rayonnantes", exact: true })
    .click();
  await expect(
    sources.getByRole("textbox", { name: "Nom de la source", exact: true }),
  ).toHaveValue("Source de test");
  expect(errors).toEqual([]);
});
test("demo navigation, horizontal and vertical workers, CAD and 3D", async ({
  page,
}) => {
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page
    .getByRole("button", { name: "Projet de démonstration", exact: true })
    .click();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Coupes horizontales", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Calculer les coupes actives", exact: true })
    .click();
  await expect(page.getByRole("status")).toHaveText("Calcul terminé", {
    timeout: 60000,
  });
  await page.screenshot({ path: ".tools/test-results/horizontal.png" });
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Coupes verticales", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Calculer les coupes actives", exact: true })
    .click();
  await expect(page.locator(".plot-area canvas")).toBeVisible();
  await expect(page.getByRole("status")).toHaveText("Calcul terminé", {
    timeout: 60000,
  });
  await expect(page.locator(".plot-legend > span")).toHaveCount(4);
  await page.screenshot({ path: ".tools/test-results/vertical.png" });
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "CAO", exact: true })
    .click();
  await expect(page.getByLabel("Plan CAO à l’échelle")).toBeVisible();
  await expect(
    page.getByRole("button", { name: "Exporter DWG", exact: true }),
  ).toBeDisabled();
  const cadDownload = page.waitForEvent("download");
  await page.getByRole("button", { name: "DXF", exact: true }).click();
  await (await cadDownload).saveAs(".tools/test-results/calculated-plan.dxf");
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "3D", exact: true })
    .click();
  await expect(page.locator(".scene-3d canvas")).toBeVisible();
  for (const name of [
    "Limites EMF",
    "Origine et grille",
    "Positions des antennes",
    "Bâtiments",
    "Terrain",
    "Lignes et annotations",
    "Tableaux et graphiques",
    "Rapport par modèle",
    "Calibrer le fond de plan",
    "Paramètres",
  ]) {
    await page
      .getByRole("navigation")
      .getByRole("button", { name, exact: true })
      .click();
    await expect(page.getByRole("main", { name, exact: true })).toBeVisible();
  }
});
test("secondary commands stay visible, unavailable commands are disabled, duplicate and undo work", async ({
  page,
}) => {
  await expect(
    page.getByRole("button", { name: "Éditeur de diagramme", exact: true }),
  ).toBeDisabled();
  await expect(
    page.getByRole("button", { name: "Éditeur de diagramme", exact: true }),
  ).toHaveAttribute("title", /sélectionnez/);
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page
    .getByRole("button", { name: "Projet de démonstration", exact: true })
    .click();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Bâtiments", exact: true })
    .click();
  const grid = page.getByRole("region", { name: "Bâtiments", exact: true });
  await expect(grid.locator("tbody tr")).toHaveCount(2);
  await grid
    .getByRole("textbox", { name: "N° / Nom", exact: true })
    .first()
    .click();
  await page
    .getByRole("button", { name: "Dupliquer le bâtiment", exact: true })
    .click();
  await expect(grid.locator("tbody tr")).toHaveCount(3);
  await page
    .getByRole("button", {
      name: "Annuler la dernière modification",
      exact: true,
    })
    .click();
  await expect(grid.locator("tbody tr")).toHaveCount(2);
  await expect(
    page.getByRole("button", {
      name: "Importer les bâtiments KML",
      exact: true,
    }),
  ).toBeEnabled();
  await page.screenshot({ path: ".tools/test-results/buildings.png" });
});
test("original xemf import and real Office/PDF exports", async ({ page }) => {
  test.skip(!existsSync(sample), "Original archive is a local test fixture.");
  const errors: string[] = [];
  page.on("pageerror", (e) => errors.push(e.message));
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles(sample);
  await expect(page.locator(".project-count")).toContainText("9 sources");
  await page.screenshot({
    path: ".tools/test-results/sources-original-sample.png",
  });
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Calculs aux points", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Calculer les points", exact: true })
    .click();
  await expect(page.locator(".result-matrix thead th")).toHaveCount(16);
  await page.screenshot({ path: ".tools/test-results/point-results.png" });
  for (const [format, extension] of [
    ["Excel", "xlsx"],
    ["Word", "docx"],
    ["PDF", "pdf"],
  ]) {
    const download = page.waitForEvent("download");
    await page
      .getByRole("dialog")
      .getByRole("button", { name: format, exact: true })
      .click();
    const file = await download;
    expect(file.suggestedFilename()).toMatch(new RegExp(`\\.${extension}$`));
    await file.saveAs(`.tools/test-results/export.${extension}`);
  }
  expect(errors).toEqual([]);
});
test("report selection is reflected in exported Word, PDF and Excel content", async ({
  page,
}) => {
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page
    .getByRole("button", { name: "Projet de démonstration", exact: true })
    .click();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Rapport par modèle", exact: true })
    .click();
  await page
    .getByRole("combobox", { name: "Contenu", exact: true })
    .selectOption("sources");
  for (const format of ["word", "pdf"]) {
    await page
      .getByRole("combobox", { name: "Format", exact: true })
      .selectOption(format);
    const download = page.waitForEvent("download");
    await page
      .getByRole("button", { name: "Générer le rapport", exact: true })
      .click();
    const path = `.tools/test-results/report-with-sources.${format === "word" ? "docx" : "pdf"}`;
    await (await download).saveAs(path);
    const bytes = readFileSync(path);
    if (format === "word") {
      const { default: JSZip } = await import("jszip");
      const zip = await JSZip.loadAsync(bytes);
      const xml = await zip.file("word/document.xml")!.async("string");
      expect(xml).toContain("900_0");
      expect(xml).toContain("Calculs aux points");
    } else {
      expect(bytes.subarray(0, 5).toString()).toBe("%PDF-");
      expect(bytes.toString("latin1")).toContain("900_0");
    }
  }
  await page
    .getByRole("combobox", { name: "Contenu", exact: true })
    .selectOption("points");
  await page
    .getByRole("combobox", { name: "Format", exact: true })
    .selectOption("excel");
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Générer le rapport", exact: true })
    .click();
  const path = ".tools/test-results/report-points-only.xlsx";
  await (await download).saveAs(path);
  const { default: ExcelJS } = await import("exceljs");
  const book = new ExcelJS.Workbook();
  await book.xlsx.readFile(path);
  expect(book.worksheets.map((s) => s.name)).toEqual(["Points de contrôle"]);
  expect(book.worksheets[0].rowCount).toBe(5);
});
