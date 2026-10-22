import {
  test,
  expect,
  type Page,
  type Locator,
  type Dialog,
} from "@playwright/test";
import { readFileSync } from "node:fs";
import type { PlannerProject } from "../../src/types";

const button = (page: Page, name: string) =>
  page.getByRole("button", { name, exact: true });
const nav = (page: Page, name: string) =>
  page.getByRole("navigation").getByRole("button", { name, exact: true });
async function save(page: Page) {
  const wait = page.waitForEvent("download");
  await button(page, "Enregistrer le projet").click();
  const path = test.info().outputPath(`origin-${Date.now()}.emfp`);
  await (await wait).saveAs(path);
  return {
    path,
    project: JSON.parse(readFileSync(path, "utf8")) as PlannerProject,
  };
}
async function open(page: Page, path: string) {
  const confirm = (dialog: Dialog) => void dialog.accept();
  page.on("dialog", confirm);
  const wait = page.waitForEvent("filechooser");
  await button(page, "Fichier").click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await wait).setFiles(path);
  await expect(page.locator(".status-message")).toHaveText("Projet ouvert");
  page.off("dialog", confirm);
  await nav(page, "Origine et grille").click();
}
async function edit(page: Page, label: string, value: string) {
  await page.getByLabel(label, { exact: true }).fill(value);
  await page.getByLabel(label, { exact: true }).press("Tab");
}
async function drag(page: Page, handle: Locator, dx: number, dy: number) {
  const box = await handle.boundingBox();
  expect(box).not.toBeNull();
  const x = box!.x + box!.width / 2,
    y = box!.y + box!.height / 2;
  await page.mouse.move(x, y);
  await page.mouse.down();
  await page.mouse.move(x + dx, y + dy, { steps: 8 });
  await page.mouse.up();
}
test.beforeEach(async ({ page }) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  await nav(page, "Origine et grille").click();
  await page
    .getByRole("checkbox", { name: "Fond de carte", exact: true })
    .uncheck();
});

test("report coordinates stay independent, preserve saved contours and feed Word/PDF/Excel", async ({
  page,
}) => {
  await open(page, "tests/fixtures/original-sample.xemf");
  const before = (await save(page)).project;
  expect(before.sources).toHaveLength(9);
  await edit(page, "Latitude du site, °", "N5° 30′ 0″");
  await edit(page, "Longitude du site, °", "4° 01′ 30″ W");
  const after = (await save(page)).project;
  expect(after.origin).toEqual(before.origin);
  expect(after.sources).toEqual(before.sources);
  expect(after.settings.site_coordinates).toEqual([5.5, -4.025]);
  expect(after.saved_cut_results).toEqual(before.saved_cut_results);
  await nav(page, "Rapport par modèle").click();
  await expect(page.locator(".report-preview")).toContainText("5.5; -4.025");
  for (const format of ["word", "pdf", "excel"]) {
    await page
      .getByRole("combobox", { name: "Format", exact: true })
      .selectOption(format);
    const wait = page.waitForEvent("download");
    await button(page, "Générer le rapport").click();
    const path = test
      .info()
      .outputPath(
        `site.${format === "word" ? "docx" : format === "pdf" ? "pdf" : "xlsx"}`,
      );
    await (await wait).saveAs(path);
    if (format === "word") {
      const { default: JSZip } = await import("jszip");
      const zip = await JSZip.loadAsync(readFileSync(path));
      expect(await zip.file("word/document.xml")!.async("string")).toContain(
        "5.5; -4.025",
      );
    } else if (format === "pdf")
      expect(readFileSync(path).toString("latin1")).toContain(
        "5.5000000; -4.0250000",
      );
    else {
      const { default: ExcelJS } = await import("exceljs");
      const workbook = new ExcelJS.Workbook();
      await workbook.xlsx.readFile(path);
      expect(workbook.subject).toContain("5.5; -4.025");
    }
  }
  await nav(page, "Origine et grille").click();
  await button(page, "Copier l’origine vers le site").click();
  expect((await save(page)).project.settings.site_coordinates).toEqual(
    before.origin,
  );
});

test("asymmetric area dialog validates, cancels, persists, resizes on the map and supports undo", async ({
  page,
}) => {
  const before = (await save(page)).project;
  await button(page, "Dimensions de la zone de calcul").last().click();
  let dialog = page.getByRole("dialog");
  await dialog.getByLabel("À droite, m", { exact: true }).fill("9");
  await dialog
    .getByRole("button", { name: "Appliquer les dimensions", exact: true })
    .click();
  await expect(dialog.getByRole("alert")).toBeVisible();
  await dialog.getByRole("button", { name: "Annuler", exact: true }).click();
  expect((await save(page)).project.settings.calculation_area_m).toEqual(
    before.settings.calculation_area_m,
  );
  await button(page, "Dimensions de la zone de calcul").last().click();
  dialog = page.getByRole("dialog");
  for (const [label, value] of [
    ["À gauche, m", "20"],
    ["À droite, m", "110"],
    ["En haut, m", "70"],
    ["En bas, m", "30"],
  ])
    await dialog.getByLabel(label, { exact: true }).fill(value);
  await dialog
    .getByRole("button", { name: "Appliquer les dimensions", exact: true })
    .click();
  const asymmetric = (await save(page)).project.settings;
  expect(asymmetric.calculation_area_m!.x_min).toBeCloseTo(-20, 0);
  expect(asymmetric.calculation_area_m!.x_max).toBeCloseTo(110, 0);
  await page
    .getByRole("checkbox", { name: "Afficher la grille", exact: true })
    .check();
  await expect(
    page.locator('[data-calculation-area="true"] text'),
  ).not.toHaveCount(0);
  await drag(
    page,
    page.getByRole("button", {
      name: "Redimensionner la zone : coin 3",
      exact: true,
    }),
    -30,
    20,
  );
  const resized = (await save(page)).project.settings;
  expect(resized.calculation_area_m!.x_max).toBeLessThan(
    asymmetric.calculation_area_m!.x_max,
  );
  expect(resized.calculation_area_m!.y_max).toBeLessThan(
    asymmetric.calculation_area_m!.y_max,
  );
  await button(page, "Annuler la dernière modification").click();
  expect((await save(page)).project.settings.calculation_area_m).toEqual(
    asymmetric.calculation_area_m,
  );
  const saved = await save(page);
  await open(page, saved.path);
  expect((await save(page)).project.settings.calculation_area_wm).toEqual(
    saved.project.settings.calculation_area_wm,
  );
  await page.screenshot({
    path: test.info().outputPath("origin-asymmetric-grid.png"),
  });
});

test("origin handles and screen-center command preserve world anchors and ruler has two adjustable ends", async ({
  page,
}) => {
  await open(page, "tests/fixtures/original-sample.xemf");
  await page
    .getByRole("checkbox", { name: "Fond de carte", exact: true })
    .uncheck();
  const before = (await save(page)).project;
  expect(before.sources).toHaveLength(9);
  await button(page, "Mesurer sur le plan").click();
  await expect(
    page.getByLabel("Mesure de la règle", { exact: true }),
  ).toContainText("50.0 m");
  await drag(
    page,
    page.getByRole("button", { name: "Extrémité 2 de la règle", exact: true }),
    40,
    -20,
  );
  await expect(
    page.getByLabel("Mesure de la règle", { exact: true }),
  ).not.toContainText("50.0 m");
  await button(page, "Mesurer sur le plan").click();
  await expect(page.locator('[data-ruler="true"]')).toHaveCount(0);
  await drag(
    page,
    page.getByRole("button", { name: "Déplacer l’origine", exact: true }),
    40,
    -25,
  );
  const moved = (await save(page)).project;
  expect(moved.origin).not.toEqual(before.origin);
  expect(moved.sources.map((s) => [s.lat, s.lng])).toEqual(
    before.sources.map((s) => [s.lat, s.lng]),
  );
  expect(moved.settings.site_coordinates).toEqual(
    before.settings.site_coordinates,
  );
  expect(moved.settings.calculation_area_wm).toEqual(
    before.settings.calculation_area_wm,
  );
  expect(moved.saved_cut_results?.horizontal ?? []).toHaveLength(0);
  const previousOrigin = moved.origin;
  page.once("dialog", (d) => d.dismiss());
  await button(page, "Origine au centre de l’écran").click();
  expect((await save(page)).project.origin).toEqual(previousOrigin);
  page.once("dialog", (d) => d.accept());
  await button(page, "Origine au centre de l’écran").click();
  expect((await save(page)).project.settings.calculation_area_wm).toEqual(
    before.settings.calculation_area_wm,
  );
  await page.screenshot({
    path: test.info().outputPath("origin-rebased-castelfranco.png"),
  });
});

test("both native grids use the largest-axis division count in worker results", async ({
  page,
}) => {
  await edit(page, "Divisions grille 1", "50");
  await edit(page, "Divisions grille 2", "60");
  await nav(page, "Sources rayonnantes").click();
  await button(page, "Ajouter une source").click();
  await nav(page, "Coupes horizontales").click();
  await button(page, "Calculer avec la grille 1").click();
  await expect(page.getByRole("status")).toHaveText("Calcul terminé", {
    timeout: 60000,
  });
  const first = page.getByLabel("Grille calculée", { exact: true });
  expect(
    Math.max(
      Number(await first.getAttribute("data-grid-width")),
      Number(await first.getAttribute("data-grid-height")),
    ),
  ).toBe(51);
  await button(page, "Calculer avec la grille 2").click();
  await expect(page.getByRole("status")).toHaveText("Calcul terminé", {
    timeout: 60000,
  });
  const second = page.getByLabel("Grille calculée", { exact: true });
  expect(
    Math.max(
      Number(await second.getAttribute("data-grid-width")),
      Number(await second.getAttribute("data-grid-height")),
    ),
  ).toBe(61);
});
