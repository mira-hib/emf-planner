import { test, expect, type Page, type Locator } from "@playwright/test";
import { readFileSync } from "node:fs";
import JSZip from "jszip";
import { newProject, newSource } from "../../src/lib/project";
import type { PlannerProject } from "../../src/types";
const kit = "test/emf_test_kit/data/";
const additiveModifier = process.platform === "darwin" ? "Meta" : "Control";
const grid = (page: Page) => page.getByRole("region", { name: "Sources rayonnantes", exact: true });
const button = (page: Page, name: string) => page.getByRole("button", { name, exact: true });
async function save(page: Page): Promise<PlannerProject> {
  const wait = page.waitForEvent("download"); await button(page, "Enregistrer le projet").click();
  const path = test.info().outputPath(`sources-${Date.now()}.emfp`); await (await wait).saveAs(path); return JSON.parse(readFileSync(path, "utf8"));
}
async function open(page: Page, project: PlannerProject) {
  const wait = page.waitForEvent("filechooser"); await button(page, "Fichier").click(); await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await wait).setFiles({ name: "kit.emfp", mimeType: "application/json", buffer: Buffer.from(JSON.stringify(project)) });
  await expect(page.locator(".status-message")).toHaveText("Projet ouvert");
}
async function paste(target: Locator, text: string) {
  await target.dispatchEvent("paste", { clipboardData: { getData: () => text } });
}
async function nativePaste(target: Locator, text: string) {
  await target.evaluate((element, value) => { const data = new DataTransfer(); data.setData("text/plain", value); element.dispatchEvent(new ClipboardEvent("paste", { clipboardData: data, bubbles: true, cancelable: true })); }, text);
}
test.beforeEach(async ({ page }) => { await page.goto("/"); await page.evaluate(() => localStorage.clear()); await page.reload(); });
test("SRC-01 startup creates an empty project in the Sources screen", async ({ page }) => {
  await expect(grid(page)).toBeVisible(); await expect(grid(page).locator("tbody tr")).toHaveCount(0); expect((await save(page)).sources).toEqual([]);
  await button(page, "Ajouter une source").click();
  const source=(await save(page)).sources[0];
  expect({name:source.name,service:source.service_type,frequency:source.frequency_mhz,power:source.power_w,gain:source.gain_dbi,height:source.height_m,size:source.antenna_size_m,model:source.antenna_model,existing:source.is_existing})
    .toEqual({name:"",service:"Other",frequency:1000,power:0,gain:0,height:0,size:1,model:"",existing:true});
});
test("SRC-02 column paste, full oracle paste and Shift+Insert apply actual clipboard values", async ({ page }) => {
  const table = grid(page); await nativePaste(table, "Name 1\nName 2");
  await expect(table.getByLabel("Nom de la source", { exact: true }).nth(1)).toHaveValue("Name 2");
  const power = table.getByLabel("Puissance TX, W", { exact: true }).first(); await power.focus();
  await nativePaste(power, "12,05\n20"); await expect(power).toHaveValue("12.05");
  await page.evaluate(() => Object.defineProperty(navigator, "clipboard", { configurable: true, value: { readText: async () => "3\n2" } }));
  const count = table.getByLabel("Nombre de TX", { exact: true }).first(); await count.focus(); await count.press("Shift+Insert");
  await expect(count).toHaveValue("3"); await expect(table.getByLabel("Nombre de TX", { exact: true }).nth(1)).toHaveValue("2");
  await nativePaste(table.getByLabel("Nom de la source", { exact: true }).first(), readFileSync(kit + "sources/sources_oracle.tsv", "utf8"));
  const saved = await save(page); expect(saved.sources[0].name).toBe("TS01_900_man"); expect(saved.sources[0].power_w).toBe(12.05); expect(saved.sources[1].num_tx).toBe(3);
});
test("SRC-03 header toggles all source activity and saves the resulting flags", async ({ page }) => {
  await button(page, "Ajouter une source").click(); await button(page, "Ajouter une source").click();
  await grid(page).getByRole("columnheader", { name: "✓" }).click();
  expect((await save(page)).sources.map((source) => source.active)).toEqual([false, false]);
  await button(page, "Activer ou désactiver toutes les sources").click(); expect((await save(page)).sources.map((source) => source.active)).toEqual([true, true]);
});
test("SRC-04 row selection shows current triangle and Delete removes only the Ctrl selection", async ({ page }) => {
  for (let i = 0; i < 4; i++) {
    await button(page, "Ajouter une source").click();
    const name=grid(page).getByLabel("Nom de la source",{exact:true}).nth(i);await name.fill(`Source ${i+1}`);await name.press("Tab");
  }
  const table = grid(page); await table.getByRole("rowheader", { name: "Sélectionner la source 1", exact: true }).click();
  await table.getByRole("rowheader", { name: "Sélectionner la source 3", exact: true }).click({ modifiers: [additiveModifier] });
  await expect(table.locator('tr[aria-selected="true"]')).toHaveCount(2); await expect(table.locator('tr[data-current="true"] th')).toHaveText("▶");
  page.once("dialog", (dialog) => dialog.accept()); await table.focus(); await table.press("Delete");
  expect((await save(page)).sources.map((source) => source.name)).toEqual(["Source 2", "Source 4"]);
});
test("SRC-05 disjoint cells and a complete column support bulk edits", async ({ page }) => {
  for (let i = 0; i < 4; i++) await button(page, "Ajouter une source").click();
  const table = grid(page), fields = table.getByLabel("Puissance TX, W", { exact: true });
  await fields.nth(0).click(); await fields.nth(2).click({ modifiers: [additiveModifier] }); await fields.nth(3).click({ modifiers: [additiveModifier] });
  await fields.nth(0).fill("7"); await fields.nth(0).press("Tab");
  expect((await save(page)).sources.map((source) => source.power_w)).toEqual([7, 0, 7, 7]);
  await table.getByRole("columnheader", { name: "Nombre de TX" }).click();
  const counts = table.getByLabel("Nombre de TX", { exact: true }); await counts.first().fill("7"); await counts.first().press("Tab");
  expect((await save(page)).sources.map((source) => source.num_tx)).toEqual([7, 7, 7, 7]);
});
test("SRC-09 dragging and resizing columns persist after application reload", async ({ page }) => {
  await button(page, "Ajouter une source").click(); const table = grid(page);
  await table.getByRole("columnheader", { name: "Gain, dBi" }).dragTo(table.getByRole("columnheader", { name: "Service" }));
  const separator = table.getByRole("separator", { name: "Largeur Gain, dBi", exact: true }), bounds = await separator.boundingBox(); expect(bounds).toBeTruthy();
  await page.mouse.move(bounds!.x + 2, bounds!.y + 2); await page.mouse.down(); await page.mouse.move(bounds!.x + 62, bounds!.y + 2); await page.mouse.up();
  const before = await page.evaluate(() => localStorage.getItem("emf-planner-source-columns-v1")); expect(JSON.parse(before!).widths.gain_dbi).toBeGreaterThan(80);
  page.on("dialog", (dialog) => void dialog.accept()); await page.reload();
  const after = await table.getByRole("columnheader").allTextContents(); expect(after.findIndex((name) => name.includes("Gain"))).toBeLessThan(after.findIndex((name) => name.includes("Service")));
  expect(await page.evaluate(() => localStorage.getItem("emf-planner-source-columns-v1"))).toBe(before);
});
test("SRC-10 invalid azimuth displays the native range sentinel and persists for reports", async ({ page }) => {
  await button(page, "Ajouter une source").click(); const input = grid(page).getByLabel("Azimut, °", { exact: true }); await input.fill("x"); await input.press("Tab");
  await expect(input).toHaveValue("0÷360"); expect((await save(page)).sources[0].azimuth_deg).toBe(-1);
});
test("SRC-11 SRC-12 power double-click converts four units and only OK writes watts", async ({ page }) => {
  await button(page, "Ajouter une source").click(); const power = grid(page).getByLabel("Puissance TX, W", { exact: true }); await power.dblclick();
  let dialog = page.getByRole("dialog", { name: "Conversion de puissance", exact: true }); await dialog.getByLabel("Puissance, W", { exact: true }).fill("12,05");
  await expect(dialog.getByLabel("Puissance, mW", { exact: true })).toHaveValue("12050"); await expect(dialog.getByLabel("Puissance, dBW", { exact: true })).toHaveValue("10.8"); await expect(dialog.getByLabel("Puissance, dBm", { exact: true })).toHaveValue("40.8");
  await dialog.getByRole("button", { name: "Annuler", exact: true }).click(); await expect(power).toHaveValue("0");
  await power.dblclick(); dialog = page.getByRole("dialog", { name: "Conversion de puissance", exact: true }); await dialog.getByLabel("Puissance, W", { exact: true }).fill("0"); await expect(dialog.getByRole("alert")).toBeVisible(); await expect(dialog.getByRole("button", { name: "OK", exact: true })).toBeDisabled();
  await dialog.getByLabel("Puissance, dBm", { exact: true }).fill("40.8"); await dialog.getByRole("button", { name: "OK", exact: true }).click(); expect((await save(page)).sources[0].power_w).toBe(12.0226);
});
test("SRC-13 SRC-16 attenuation double-click opens feeder tools and manual override retains feeder configuration", async ({ page }) => {
  await button(page, "Ajouter une source").click(); const attenuation = grid(page).getByLabel("Atténuation, dB", { exact: true }); await attenuation.dblclick();
  const dialog = page.getByRole("dialog", { name: "Bibliothèque de câbles", exact: true }); await expect(dialog).toBeVisible();
  await dialog.getByLabel("Longueur du câble, m", { exact: true }).fill("50"); await dialog.getByRole("button", { name: "Appliquer à la source", exact: true }).click();
  const before = (await save(page)).sources[0]; expect(before.feeder?.length_m).toBe(50); expect(before.total_attenuation_db).toBeGreaterThan(0);
  await attenuation.fill("2"); await attenuation.press("Tab"); await attenuation.dblclick(); await expect(dialog.getByLabel("Longueur du câble, m", { exact: true })).toHaveValue("50"); await dialog.getByRole("button", { name: "Annuler", exact: true }).click();
  const after = (await save(page)).sources[0]; expect(after.total_attenuation_db).toBe(2); expect(after.feeder).toEqual(before.feeder);
});
test("SRC-26 owner selection applies saved color and manual owner entry remains editable", async ({ page }) => {
  const project = newProject(); project.sources = [newSource(project.origin, "Owner test")]; project.owner_names = ["Opérateur A"]; project.owner_colors = { "Opérateur A": "#90ee90" }; await open(page, project);
  const owner = grid(page).getByLabel("Propriétaire", { exact: true }); await owner.selectOption("Opérateur A"); let source = (await save(page)).sources[0]; expect(source.owner).toBe("Opérateur A"); expect(source.color).toBe("#90ee90");
  page.once("dialog", (dialog) => dialog.accept("Propriétaire libre")); await owner.selectOption("Specify manually"); source = (await save(page)).sources[0]; expect(source.owner).toBe("Propriétaire libre");
});
test("SRC-29 native MSI NAME and gain are loaded then manually editable", async ({ page }) => {
  await button(page, "Ajouter une source").click(); const wait = page.waitForEvent("filechooser"); await button(page, "Importer le diagramme MSI").click(); await (await wait).setFiles(kit + "msi/ok_sector65x7_17dBi.msi");
  const model = grid(page).getByLabel("Modèle d’antenne", { exact: true }); await expect(model).toHaveValue("sector65x7");
  await model.fill("Modèle libre"); await model.press("Tab"); const gain = grid(page).getByLabel("Gain, dBi", { exact: true }); await gain.fill("18.2"); await gain.press("Tab");
  const source = (await save(page)).sources[0]; expect(source.antenna_model).toBe("Modèle libre"); expect(source.gain_dbi).toBe(18.2); expect(source.pattern_h).toHaveLength(360);
});
test("SRC-33 ZIP library previews and applies a selected valid diagram while reporting a bad entry", async ({ page }) => {
  await button(page, "Ajouter une source").click(); await button(page, "Bibliothèque d’antennes").click(); const dialog = page.getByRole("dialog", { name: "Bibliothèque d’antennes", exact: true });
  const zip = new JSZip(); zip.file("nested/valid.msi", readFileSync(kit + "msi/ok_sector65x7_17dBi.msi")); zip.file("bad.msi", "broken");
  const wait = page.waitForEvent("filechooser"); await dialog.getByRole("button", { name: "Ouvrir une archive ZIP…", exact: true }).click(); await (await wait).setFiles({ name: "patterns.zip", mimeType: "application/zip", buffer: await zip.generateAsync({ type: "nodebuffer" }) });
  await expect(dialog.getByRole("status")).toHaveText("1 diagramme(s), 1 rejeté(s)"); await expect(dialog.getByLabel("Diagrammes disponibles").locator("option")).toHaveCount(1);
  await dialog.getByRole("button", { name: "Appliquer à la source", exact: true }).click(); const source = (await save(page)).sources[0]; expect(source.gain_dbi).toBe(17); expect(source.pattern_v).toHaveLength(360);
});
test("LIM-02 LIM-03 LIM-05 LIM-06 real table paste applies flat and piecewise limits, rejects invalid and signals duplicates", async ({ page }) => {
  await page.getByRole("navigation").getByRole("button", { name: "Limites EMF", exact: true }).click(); const table = page.getByRole("region", { name: "Tableau commun des limites EMF", exact: true });
  await nativePaste(table, readFileSync(kit + "limits/limits_flat.tsv", "utf8")); let limits = (await save(page)).limits; expect(limits.map((limit) => limit.rows[0].value)).toEqual([6, 20, 4.5, 10]);
  await nativePaste(table, readFileSync(kit + "limits/limits_icnirp.tsv", "utf8")); limits = (await save(page)).limits; expect(limits[0].rows.slice(0, 4).map((row) => row.frequency)).toEqual([30, 300, 1500, 1e6]);
  await nativePaste(table, "900\n300\n300"); await expect(page.getByRole("alert").filter({ hasText: "fréquence non croissante" })).toHaveCount(2);
  const before = (await save(page)).limits; await nativePaste(table, "1000001"); expect((await save(page)).limits).toEqual(before);
});
