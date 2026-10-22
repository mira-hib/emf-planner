import { test, expect, type Page } from "@playwright/test";
import { readFileSync } from "node:fs";
import type { PlannerProject } from "../../src/types";

const grid = (page: Page) =>
  page.getByRole("region", { name: "Sources rayonnantes", exact: true });
const button = (page: Page, name: string) =>
  page.getByRole("button", { name, exact: true });
async function saved(page: Page): Promise<PlannerProject> {
  const download = page.waitForEvent("download");
  await button(page, "Enregistrer le projet").click();
  const file = test.info().outputPath(`sources-${Date.now()}.emfp`);
  await (await download).saveAs(file);
  return JSON.parse(readFileSync(file, "utf8"));
}
test.beforeEach(async ({ page }) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
});

test("source groups support bulk edits, ordered duplication, persistent clipboard and confirmed deletion", async ({
  page,
}) => {
  for (let i = 0; i < 3; i++) await button(page, "Ajouter une source").click();
  const table = grid(page),
    rows = table.locator("tbody tr");
  await table
    .getByRole("rowheader", { name: "Sélectionner la source 1", exact: true })
    .click();
  await table
    .getByRole("rowheader", { name: "Sélectionner la source 3", exact: true })
    .click({ modifiers: [process.platform === "darwin" ? "Meta" : "Control"] });
  await expect(table.locator('tr[aria-selected="true"]')).toHaveCount(2);
  const service = table.getByLabel("Service", { exact: true });
  await service.first().fill("Service privé");
  await service.first().press("Tab");
  await expect(service.nth(2)).toHaveValue("Service privé");
  const frequency = table.getByLabel("Fréquence, MHz", { exact: true });
  await frequency.first().fill("960-880");
  await frequency.first().press("Tab");
  await expect(frequency.nth(2)).toHaveValue("880-960");
  const count = table.getByLabel("Nombre de TX", { exact: true }).first();
  await count.fill("99.9");
  await count.press("Tab");
  await expect(count).toHaveValue("99");
  await count.fill("100");
  await count.press("Tab");
  await expect(count).toHaveAttribute("aria-invalid", "true");
  await count.press("Escape");
  await button(page, "Copier les sources sélectionnées").click();
  await button(page, "Dupliquer la source").click();
  await expect(rows).toHaveCount(5);
  const names = table.getByLabel("Nom de la source", { exact: true });
  await expect(names.nth(3)).toHaveValue("Source 1 - Copy");
  await expect(names.nth(4)).toHaveValue("Source 3 - Copy");
  await button(page, "Placer les sources au début").click();
  await expect(names.first()).toHaveValue("Source 1 - Copy");
  await button(page, "Placer les sources à la fin").click();
  await expect(names.nth(4)).toHaveValue("Source 3 - Copy");
  await button(page, "Coller les sources").click();
  await expect(rows).toHaveCount(7);
  const project = await saved(page);
  expect(new Set(project.sources.map((s) => s.id)).size).toBe(7);
  expect(
    project.sources.filter((s) => s.frequency_end_mhz === 960),
  ).toHaveLength(6);
  page.once("dialog", (d) => d.dismiss());
  await button(page, "Supprimer la source sélectionnée").click();
  await expect(rows).toHaveCount(7);
  page.once("dialog", (d) => d.accept());
  await table.focus();
  await table.press("Delete");
  await expect(rows).toHaveCount(5);
  await button(page, "Annuler la dernière modification").click();
  await expect(rows).toHaveCount(7);
  // The independent source buffer survives an application reload.
  await saved(page);
  await expect
    .poll(() =>
      page.evaluate(
        () =>
          JSON.parse(localStorage.getItem("emf-planner-autosave-v2") ?? "{}")
            .sources?.length,
      ),
    )
    .toBe(7);
  page.once("dialog", (d) => d.accept());
  await page.reload();
  await expect(button(page, "Coller les sources")).toBeEnabled();
  await button(page, "Coller les sources").click();
  await expect(rows).toHaveCount(9);
});

test("the native EIRP scratchpad calculates forward and inverse without editing the project", async ({
  page,
}) => {
  await button(page, "Ajouter une source").click();
  const before = (await saved(page)).sources[0];
  const field = (label: string) =>
    page.getByLabel(`Calculateur : ${label}`, { exact: true });
  await field("Puissance TX").fill("40");
  await field("Nombre de TX").fill("2");
  await field("Atténuation").fill("3");
  await field("Gain antenne").fill("10");
  await button(page, "Calculer la puissance au port antenne et l’EIRP").click();
  expect(Number(await field("Port antenne").inputValue())).toBeCloseTo(
    40.095,
    3,
  );
  expect(Number(await field("EIRP").inputValue())).toBeCloseTo(400.9, 1);
  await expect(field("EIRP")).toHaveAttribute("readonly", "");
  await page
    .getByLabel("Unité EIRP du calculateur", { exact: true })
    .selectOption("dBm");
  await button(page, "Calculer la puissance au port antenne et l’EIRP").click();
  expect(Number(await field("EIRP").inputValue())).toBeCloseTo(56, 1);
  await field("Port antenne").fill("20");
  await button(page, "Calculer la puissance TX depuis le port antenne").click();
  expect(Number(await field("Puissance TX").inputValue())).toBeCloseTo(
    19.953,
    3,
  );
  await field("Puissance TX").fill("40");
  await button(
    page,
    "Calculer l’atténuation depuis les puissances TX et antenne",
  ).click();
  expect(Number(await field("Atténuation").inputValue())).toBeCloseTo(6.021, 3);
  await field("Nombre de TX").fill("0");
  await button(page, "Calculer la puissance au port antenne et l’EIRP").click();
  await expect(page.locator(".source-power-error")).toBeVisible();
  expect((await saved(page)).sources[0]).toEqual(before);
  await page.screenshot({
    path: test.info().outputPath("source-scratchpad.png"),
  });
});

test("MSI patterns, native beamwidth commands and feeder options survive saving", async ({
  page,
}) => {
  await button(page, "Ajouter une source").click();
  const f = grid(page).getByLabel("Fréquence, MHz", { exact: true });
  await f.fill("900-1800");
  await f.press("Tab");
  const pattern = Array.from(
    { length: 360 },
    (_, i) => `${i} ${2 + Math.min(i, 360 - i) / 10}`,
  ).join("\n");
  const chooser = page.waitForEvent("filechooser");
  await button(page, "Importer le diagramme MSI").click();
  await (
    await chooser
  ).setFiles({
    name: "test.msi",
    mimeType: "text/plain",
    buffer: Buffer.from(
      `NAME Mesure\nGAIN 10 dBi\nHORIZONTAL 360\n${pattern}\nVERTICAL 360\n${pattern}`,
    ),
  });
  await expect(
    grid(page).getByLabel("Modèle d’antenne", { exact: true }),
  ).toHaveValue("Mesure");
  const width = grid(page).getByLabel("Ouverture H, °", { exact: true });
  await expect(width).toHaveValue("20");
  await width.fill("99");
  await width.press("Tab");
  await button(
    page,
    "Calculer les ouvertures de la source sélectionnée",
  ).click();
  await expect(width).toHaveValue("20");
  await button(page, "Bibliothèque de câbles").click();
  const dialog = page.getByRole("dialog");
  await expect(
    dialog.getByLabel("Fréquence de la source, MHz", { exact: true }),
  ).toHaveValue("1350");
  await dialog.getByLabel("Modèle de câble", { exact: true }).selectOption("0");
  await dialog.getByLabel("Longueur du câble, m", { exact: true }).fill("100");
  await dialog
    .getByLabel("Longueurs des bretelles, m", { exact: true })
    .fill("0");
  await dialog
    .getByRole("checkbox", {
      name: "Prendre en compte les pertes des connecteurs",
      exact: true,
    })
    .uncheck();
  await dialog
    .getByLabel("Perte par connecteur, dB", { exact: true })
    .fill("10");
  await dialog
    .getByRole("button", { name: "Appliquer à la source", exact: true })
    .click();
  const source = (await saved(page)).sources[0];
  expect(source.frequency_mhz).toBe(900);
  expect(source.frequency_end_mhz).toBe(1800);
  expect(source.pattern_h).toHaveLength(360);
  expect(source.feeder?.include_connectors).toBe(false);
  expect(source.total_attenuation_db).toBeLessThan(40); // 20 dB of disabled connector losses are excluded.
  await expect(button(page, "Importer Excel .xlt")).toBeDisabled();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Limites EMF", exact: true })
    .click();
  await page
    .getByRole("checkbox", { name: "Utiliser Ftdc", exact: true })
    .first()
    .uncheck();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Sources rayonnantes", exact: true })
    .click();
  await expect(
    page.getByLabel("Facteur Ftdc limites 1", { exact: true }),
  ).toBeDisabled();
  await page.screenshot({ path: test.info().outputPath("sources-menu.png") });
});
