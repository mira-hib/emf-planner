import { test, expect, type Page, type Locator } from "@playwright/test";
import { readFileSync, writeFileSync } from "node:fs";
import { newProject, newPoint, newSource } from "../../src/lib/project";
import { toGeo } from "../../src/lib/geometry";
import type { PlannerProject } from "../../src/types";

const kit = "test/emf_test_kit/data/";
const button = (page: Page, name: string) =>
  page.getByRole("button", { name, exact: true });
const nav = (page: Page, name: string) =>
  page.getByRole("navigation").getByRole("button", { name, exact: true });
function base() {
  const p = newProject();
  p.settings.reflection = 1; // Kit BASE-0 specifies k=1, independently of native defaults.
  p.origin = [45.485337, 9.198818];
  p.map_center = [...p.origin];
  p.settings.show_basemap = false;
  p.sources = [
    {
      ...newSource(p.origin, "TS06"),
      power_w: 1,
      gain_dbi: 0,
      height_m: 10,
      pattern_h: Array(360).fill(0),
      pattern_v: Array(360).fill(0),
    },
  ];
  p.limits = [6, 20, 4.5, 10].map((value, i) => ({
    ...p.limits[i],
    rows: [{ frequency: 1e6, value, exponent: 0 }],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  }));
  const c = newPoint(p.origin, "P1"),
    [lat, lng] = toGeo(100, 0, p.origin);
  Object.assign(c, { lat, lng, x_m: 100, y_m: 0 });
  p.control_points = [c];
  return p;
}
async function openProject(page: Page, project: PlannerProject) {
  const chooser = page.waitForEvent("filechooser");
  await button(page, "Fichier").click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (
    await chooser
  ).setFiles({
    name: "kit.emfp",
    mimeType: "application/json",
    buffer: Buffer.from(JSON.stringify(project)),
  });
  await expect(page.locator(".status-message")).toHaveText("Projet ouvert");
}
async function save(page: Page) {
  const wait = page.waitForEvent("download");
  await button(page, "Enregistrer le projet").click();
  const path = test.info().outputPath(`kit-${Date.now()}.emfp`);
  await (await wait).saveAs(path);
  return JSON.parse(readFileSync(path, "utf8")) as PlannerProject;
}
async function importFile(page: Page, action: string, file: string) {
  const wait = page.waitForEvent("filechooser");
  await button(page, action).click();
  await (await wait).setFiles(file);
}
async function edit(input: Locator, value: string) {
  await input.fill(value);
  await input.press("Tab");
}
const result = (page: Page, label: string) =>
  page
    .locator(".result-matrix tr")
    .filter({ has: page.getByRole("rowheader", { name: label, exact: true }) })
    .locator("td")
    .first();

test.beforeEach(async ({ page }) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  page.on("dialog", (d) => void d.accept());
});

test("PNT-03 PNT-04 PNT-05 RPT-07: UI, worker and Excel distinguish calculated, measured and total fields", async ({
  page,
}) => {
  await openProject(page, base());
  await nav(page, "Calculs aux points").click();
  const heights = page.getByRole("region", {
    name: "Hauteurs des points",
    exact: true,
  });
  await edit(heights.getByLabel("E mesuré, V/m", { exact: true }), "0,07");
  await button(page, "Calculer tous les points").click();
  await expect(
    page.getByRole("dialog", { name: "Résultats du calcul aux points" }),
  ).toBeVisible();
  const calculated = Math.sqrt(30) / Math.hypot(100, 8.5),
    total = Math.hypot(calculated, 0.07);
  expect(Number(await result(page, "E calculé, V/m").innerText())).toBeCloseTo(
    calculated,
    6,
  );
  expect(Number(await result(page, "E total, V/m").innerText())).toBeCloseTo(
    total,
    6,
  );
  expect(
    Number(await result(page, "Exposition simultanée, %").innerText()),
  ).toBeCloseTo((100 * total ** 2) / 36, 6);
  await page.screenshot({
    path: test.info().outputPath("kit-point-measured.png"),
  });
  const wait = page.waitForEvent("download");
  await page
    .getByRole("dialog")
    .getByRole("button", { name: "Excel", exact: true })
    .click();
  const path = test.info().outputPath("measured.xlsx");
  await (await wait).saveAs(path);
  const { default: ExcelJS } = await import("exceljs");
  const book = new ExcelJS.Workbook();
  await book.xlsx.readFile(path);
  const sheet = book.getWorksheet("Points de contrôle")!;
  expect(sheet.getRow(1).getCell(5).value).toBe("E total (V/m)");
  expect(Number(sheet.getRow(2).getCell(5).value)).toBeCloseTo(total, 12);
  expect(Number(sheet.getRow(2).getCell(8).value)).toBe(0.07);
  await button(page, "Fermer les résultats").click();
  const saved = await save(page);
  expect(saved.control_points[0].heights![0].measured).toBe(0.07);
});

test("LIM-04 PNT-01: measured-only project, measurement frequency and group activation survive reopening", async ({
  page,
}) => {
  const p = base();
  p.sources = [];
  p.control_points[0].heights![0].measured = 6;
  await openProject(page, p);
  await nav(page, "Calculs aux points").click();
  await edit(
    page.getByLabel("Fréquence de mesure, MHz", { exact: true }),
    "900",
  );
  await button(page, "Calculer tous les points").click();
  await expect(result(page, "Exposition simultanée, %")).toHaveText(
    "100.000000",
  );
  await expect(result(page, "E calculé, V/m")).toHaveText("0.000000");
  await button(page, "Fermer les résultats").click();
  await page
    .getByLabel("Activer le groupe de points", { exact: true })
    .uncheck();
  const saved = await save(page);
  expect(saved.control_points[0].group_active).toBe(false);
  expect(saved.settings.point_measurement_frequency_mhz).toBe(900);
  await openProject(page, saved);
  await nav(page, "Calculs aux points").click();
  await expect(
    page.getByLabel("Activer le groupe de points"),
  ).not.toBeChecked();
  await button(page, "Calculer tous les points").click();
  await expect(page.getByRole("alert")).toContainText(
    "Activez au moins un groupe",
  );
});

test("PNT-07 PNT-08: height range ignores manual losses and measurements and preserves point heights", async ({
  page,
}) => {
  const p = base();
  p.control_points[0].heights![0].attenuation_db = 6;
  p.control_points[0].heights![0].measured = 0.07;
  await openProject(page, p);
  await nav(page, "Calculs aux points").click();
  await button(page, "Calcul d’un point par plage de hauteurs").click();
  const dialog = page.getByRole("dialog");
  await edit(dialog.getByLabel("Hauteur initiale, m", { exact: true }), "0");
  await edit(dialog.getByLabel("Hauteur finale, m", { exact: true }), "10");
  await edit(dialog.getByLabel("Pas en hauteur, m", { exact: true }), "1");
  await expect(dialog).toContainText("sans modifier les hauteurs enregistrées");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await expect(result(page, "E mesuré, V/m")).toHaveText("0.0000");
  await expect(page.locator(".result-matrix thead th")).toHaveCount(12);
  expect(Number(await result(page, "E total, V/m").innerText())).toBeCloseTo(
    Math.sqrt(30) / Math.hypot(100, 10),
    6,
  );
  await button(page, "Fermer les résultats").click();
  expect((await save(page)).control_points[0].heights).toEqual(
    p.control_points[0].heights,
  );
});

test("BLD-02 BLD-04 BLD-05: kit KML heights, labels, encodings and transactional failure", async ({
  page,
}) => {
  const p = base();
  p.sources = [];
  p.control_points = [];
  await openProject(page, p);
  await nav(page, "Bâtiments").click();
  for (const filename of [
    "buildings_ok.kml",
    "buildings_utf8_bom.kml",
    "buildings_crlf.kml",
    "buildings_utf16.kml",
  ]) {
    await importFile(
      page,
      "Importer les bâtiments KML",
      kit + "buildings/" + filename,
    );
    await expect(page.locator(".status-message")).toHaveText(
      "Bâtiments KML importés",
    );
  }
  const before = await save(page);
  expect(before.buildings).toHaveLength(16);
  expect(before.buildings.slice(0, 4).map((b) => [b.height_m, b.name])).toEqual(
    [
      [10, "B01"],
      [12.5, "B02"],
      [15, "L-shape"],
      [4, "2"],
    ],
  );
  expect(before.buildings.every((b) => b.foundation_m === 0)).toBe(true);
  for (const filename of [
    "buildings_malformed_truncated.kml",
    "buildings_empty.kml",
    "buildings_edge.kml",
  ]) {
    await importFile(
      page,
      "Importer les bâtiments KML",
      kit + "buildings/" + filename,
    );
    await expect(page.getByRole("alert")).toBeVisible();
    expect((await save(page)).buildings).toEqual(before.buildings);
  }
  await page.screenshot({ path: test.info().outputPath("kit-kml-import.png") });
});

test("BLD-03 ROB-09: each edge placemark is classified independently, decimal comma remains 12.5", async ({
  page,
}) => {
  await openProject(page, base());
  const xml = readFileSync(kit + "buildings/buildings_edge.kml", "utf8");
  test.setTimeout(180000);
  const placemarks = await page.evaluate((xml) => [...new DOMParser()
    .parseFromString(xml, "application/xml").getElementsByTagNameNS("*", "Placemark")]
    .map(pm => ({ name: pm.getElementsByTagNameNS("*", "name")[0].textContent,
      xml: new XMLSerializer().serializeToString(pm) })), xml);
  const rows: {name:string|null;status:string;count?:number;height?:number;error?:string}[]=[];
  for (const placemark of placemarks) {
    await openProject(page, base());
    await nav(page, "B?timents").click();
    const chooser=page.waitForEvent("filechooser");
    await button(page,"Importer les b?timents KML").click();
    await (await chooser).setFiles({name:"edge.kml",mimeType:"application/vnd.google-earth.kml+xml",buffer:Buffer.from(placemark.xml)});
    await expect.poll(async()=>await page.getByRole("alert").count()>0 ||
      await page.locator(".status-message").textContent()==="B?timents KML import?s").toBe(true);
    if(await page.getByRole("alert").count()) rows.push({name:placemark.name,status:"rejected",error:await page.getByRole("alert").innerText()});
    else {const project=await save(page);rows.push({name:placemark.name,status:"accepted",count:project.buildings.length,height:project.buildings[0]?.height_m});}
  }
  writeFileSync(
    test.info().outputPath("kml-edge-observations.json"),
    JSON.stringify(rows, null, 2),
  );
  expect(rows).toHaveLength(19);
  expect(rows.find((r) => r.name === "12,5;CommaDecimal")?.height).toBe(12.5);
  for (const suffix of [
    "Zero",
    "Negative",
    "Huge",
    "Hole",
    "Collinear",
    "Bowtie",
    "PointNotPolygon",
    "LineNotPolygon",
  ])
    expect(
      rows.find((r) => r.name?.endsWith(";" + suffix))?.status,
      suffix,
    ).toBe("rejected");
});

test("BLD-06 PRJ-05: import 2000 kit buildings, save and reopen without loss", async ({
  page,
}) => {
  test.setTimeout(120000);
  const p = base();
  p.sources = [];
  p.control_points = [];
  await openProject(page, p);
  await nav(page, "Bâtiments").click();
  const started = Date.now();
  await importFile(
    page,
    "Importer les bâtiments KML",
    kit + "buildings/buildings_stress_2000.kml",
  );
  await expect(page.locator(".status-message")).toHaveText(
    "Bâtiments KML importés",
  );
  const saved = await save(page);
  expect(saved.buildings).toHaveLength(2000);
  await openProject(page, saved);
  const reopened = await save(page);
  expect(reopened.buildings).toEqual(saved.buildings);
  writeFileSync(
    test.info().outputPath("kml-stress-timing.json"),
    JSON.stringify({ buildings: 2000, total_ms: Date.now() - started }),
  );
});

test("SRC-31: invalid MSI files preserve the previous complete diagram", async ({
  page,
}) => {
  await openProject(page, base());
  await nav(page, "Sources rayonnantes").click();
  await importFile(
    page,
    "Importer le diagramme MSI",
    kit + "msi/ok_sector65x7_17dBi.msi",
  );
  await expect(page.locator(".status-message")).toHaveText(
    "Diagramme MSI importé",
  );
  const before = (await save(page)).sources[0];
  for (const filename of [
    "bad_no_vertical.msi",
    "bad_horizontal_359_lines.msi",
    "bad_horizontal_361_lines.msi",
    "bad_gain_text.msi",
    "bad_missing_gain.msi",
    "bad_huge_attenuation.msi",
    "bad_comma_decimal.msi",
    "bad_empty.msi",
    "bad_binary.msi",
  ]) {
    await importFile(
      page,
      "Importer le diagramme MSI",
      kit + "msi/" + filename,
    );
    await expect(page.getByRole("alert")).toBeVisible();
    expect((await save(page)).sources[0], filename).toEqual(before);
  }
});

test("SET-01 ROB-02 PRJ-02: native reflection bounds, decimal comma, invalid project keeps current state", async ({
  page,
}) => {
  await openProject(page, base());
  await nav(page, "Paramètres").click();
  const coefficient = page.getByLabel("Coefficient de réflexion", {
    exact: true,
  });
  await edit(coefficient, "1,3");
  expect((await save(page)).settings.reflection).toBe(1.3);
  for (const value of ["0.99", "1.61", "NaN", "Infinity", "-1"]) {
    await edit(coefficient, value);
    await expect(coefficient).toHaveAttribute("aria-invalid", "true");
    expect((await save(page)).settings.reflection).toBe(1.3);
  }
  await coefficient.press("Escape");
  const before = await save(page),
    invalid = { ...before, settings: { ...before.settings, reflection: 0 } };
  const chooser = page.waitForEvent("filechooser");
  await button(page, "Fichier").click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (
    await chooser
  ).setFiles({
    name: "bad.emfp",
    mimeType: "application/json",
    buffer: Buffer.from(JSON.stringify(invalid)),
  });
  await expect(page.getByRole("alert")).toContainText(
    "Paramètres de calcul invalides",
  );
  expect((await save(page)).sources).toEqual(before.sources);
  expect((await save(page)).settings.reflection).toBe(1.3);
});

test("INS-02 ROB-07: points and results remain reachable at compact window sizes", async ({
  page,
}) => {
  await openProject(page, base());
  for (const viewport of [
    { width: 1366, height: 768 },
    { width: 1040, height: 720 },
  ]) {
    await page.setViewportSize(viewport);
    await nav(page, "Calculs aux points").click();
    const calculate = button(page, "Calculer les points");
    await calculate.scrollIntoViewIfNeeded();
    await expect(calculate).toBeInViewport();
    await calculate.click();
    await expect(result(page, "E calculé, V/m")).toHaveText("0.054575");
    await button(page, "Fermer les résultats").click();
    await page.screenshot({
      path: test.info().outputPath(`kit-points-${viewport.width}.png`),
    });
  }
});
