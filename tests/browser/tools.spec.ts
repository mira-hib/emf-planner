import { test, expect, type Page } from "@playwright/test";
import { readFileSync } from "node:fs";
async function navigate(page: Page, name: string) {
  await page
    .getByRole("navigation")
    .getByRole("button", { name, exact: true })
    .click();
}
test.beforeEach(async ({ page }) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page
    .getByRole("button", { name: "Projet de démonstration", exact: true })
    .click();
  // Exercise the offline drawing surface independently of the basemap default.
  await navigate(page, "Origine et grille");
  await page
    .getByRole("checkbox", { name: "Fond de carte", exact: true })
    .uncheck();
  await navigate(page, "Sources rayonnantes");
});
test("cable library and inverse power calculator update source parameters", async ({
  page,
}) => {
  await page
    .getByRole("button", { name: "Bibliothèque de câbles", exact: true })
    .click();
  const dialog = page.getByRole("dialog");
  await expect(
    dialog.getByLabel("Modèle de câble").locator("option"),
  ).toHaveCount(36);
  await dialog.getByLabel("Modèle de câble", { exact: true }).selectOption("0");
  await dialog
    .getByLabel("Longueurs des bretelles, m", { exact: true })
    .fill("0");
  await dialog
    .getByLabel("Perte par connecteur, dB", { exact: true })
    .fill("0.1");
  await dialog.getByLabel("Longueur du câble, m", { exact: true }).fill("100");
  await dialog
    .getByRole("button", { name: "Appliquer à la source", exact: true })
    .click();
  const loss = Number(
    await page
      .getByRole("region", { name: "Sources rayonnantes", exact: true })
      .getByLabel("Atténuation, dB", { exact: true })
      .first()
      .inputValue(),
  );
  expect(loss).toBeCloseTo(19.003, 3);
  await page
    .getByRole("button", { name: "Calcul inverse de puissance", exact: true })
    .click();
  await dialog.getByLabel("Valeur cible", { exact: true }).fill("1");
  await dialog
    .getByRole("button", { name: "Appliquer à la source", exact: true })
    .click();
  const power = Number(
    await page
      .getByRole("region", { name: "Sources rayonnantes", exact: true })
      .getByLabel("Puissance TX, W", { exact: true })
      .first()
      .inputValue(),
  );
  expect(power).toBeCloseTo(10 ** (loss / 10), 2);
  await page.screenshot({ path: ".tools/test-results/refined-sources.png" });
  await expect(
    page.locator('.source-inspector .polar path[data-plane="H"]'),
  ).toHaveCount(1);
  expect(
    await page
      .locator('.source-inspector .polar path[data-plane="H"]')
      .getAttribute("d"),
  ).not.toBe(
    await page
      .locator('.source-inspector .polar path[data-plane="V"]')
      .getAttribute("d"),
  );
});
test("antenna generation, library and sample editor apply actual diagram data", async ({
  page,
}) => {
  await page
    .getByRole("button", {
      name: "Antenne symétrique ITU-R F.699",
      exact: true,
    })
    .click();
  const dialog = page.getByRole("dialog");
  await dialog
    .getByLabel("Fréquence du diagramme, MHz", { exact: true })
    .fill("7000");
  await dialog.getByLabel("Gain du diagramme, dBi", { exact: true }).fill("31");
  await dialog.getByLabel("Diamètre, m", { exact: true }).fill("1.2");
  await dialog
    .getByRole("button", { name: "Créer et appliquer", exact: true })
    .click();
  await expect(
    page
      .getByRole("region", { name: "Sources rayonnantes", exact: true })
      .getByLabel("Gain, dBi", { exact: true })
      .first(),
  ).toHaveValue("31");
  await page
    .getByRole("button", { name: "Bibliothèque d’antennes", exact: true })
    .click();
  await expect(
    dialog.getByLabel("Diagrammes disponibles").locator("option"),
  ).not.toHaveCount(0);
  await dialog.getByRole("button", { name: "Fermer", exact: true }).click();
  await page
    .getByRole("button", { name: "Éditeur de diagramme", exact: true })
    .click();
  const samples = dialog.getByRole("region", {
    name: "Échantillons du diagramme",
  });
  await samples
    .getByRole("textbox", { name: "Atténuation, dB", exact: true })
    .first()
    .fill("2");
  await samples
    .getByRole("textbox", { name: "Atténuation, dB", exact: true })
    .first()
    .press("Tab");
  await page.screenshot({ path: ".tools/test-results/antenna-editor.png" });
  await dialog
    .getByRole("button", { name: "Appliquer le diagramme", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Éditeur de diagramme", exact: true })
    .click();
  await expect(
    samples
      .getByRole("textbox", { name: "Atténuation, dB", exact: true })
      .first(),
  ).toHaveValue("2");
  await dialog.getByRole("button", { name: "Annuler", exact: true }).click();
});
test("building vertex editing and KML import preserve usable footprints", async ({
  page,
}) => {
  await navigate(page, "Bâtiments");
  const grid = page.getByRole("region", { name: "Bâtiments", exact: true });
  await grid
    .getByRole("textbox", { name: "N° / Nom", exact: true })
    .first()
    .click();
  await page
    .getByRole("button", { name: "Ajouter un sommet", exact: true })
    .click();
  const dialog = page.getByRole("dialog");
  await dialog
    .getByRole("button", { name: "Insérer après le sommet", exact: true })
    .click();
  await expect(
    dialog
      .getByRole("region", { name: "Sommets de l’objet" })
      .locator("tbody tr"),
  ).toHaveCount(5);
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  const chooser = page.waitForEvent("filechooser");
  await page
    .getByRole("button", { name: "Importer les bâtiments KML", exact: true })
    .click();
  await (
    await chooser
  ).setFiles({
    name: "building.kml",
    mimeType: "application/vnd.google-earth.kml+xml",
    buffer: Buffer.from(
      '<kml xmlns="http://www.opengis.net/kml/2.2"><Placemark><name>Immeuble KML</name><ExtendedData><Data name="height"><value>14</value></Data></ExtendedData><Polygon><outerBoundaryIs><LinearRing><coordinates>-4.025,5.345 -4.0249,5.345 -4.0249,5.3451 -4.025,5.345</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark></kml>',
    ),
  });
  await expect(grid.locator("tbody tr")).toHaveCount(3);
  await expect(
    grid.getByRole("textbox", { name: "N° / Nom", exact: true }).last(),
  ).toHaveValue("Immeuble KML");
  await expect(
    grid.getByRole("textbox", { name: "Hauteur, m", exact: true }).last(),
  ).toHaveValue("14");
  const invalidChooser = page.waitForEvent("filechooser");
  await page
    .getByRole("button", { name: "Importer les bâtiments KML", exact: true })
    .click();
  await (
    await invalidChooser
  ).setFiles({
    name: "courtyard.kml",
    mimeType: "application/vnd.google-earth.kml+xml",
    buffer: Buffer.from(
      "<kml><Placemark><Polygon><outerBoundaryIs><LinearRing><coordinates>-4,5 -3.9,5 -3.9,5.1 -4,5</coordinates></LinearRing></outerBoundaryIs><innerBoundaryIs/></Polygon></Placemark></kml>",
    ),
  });
  await expect(page.getByRole("alert")).toContainText("avec des trous");
  await expect(grid.locator("tbody tr")).toHaveCount(3);
});
test("point groups and height ranges survive calculations and undo", async ({
  page,
}) => {
  await navigate(page, "Calculs aux points");
  await page
    .getByRole("button", { name: "Renommer le groupe", exact: true })
    .click();
  let dialog = page.getByRole("dialog");
  await dialog
    .getByLabel("Nom du groupe", { exact: true })
    .fill("Mesures terrain");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await expect(
    page.getByLabel("Groupe de points").locator("option"),
  ).toContainText(["Tous les groupes", "Mesures terrain"]);
  await page
    .getByRole("button", {
      name: "Calcul d’un point par plage de hauteurs",
      exact: true,
    })
    .click();
  dialog = page.getByRole("dialog");
  await dialog.getByLabel("Hauteur initiale, m", { exact: true }).fill("1");
  await dialog.getByLabel("Hauteur finale, m", { exact: true }).fill("3");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await expect(
    page
      .getByRole("dialog", { name: "Résultats du calcul aux points" })
      .locator(".result-matrix thead th"),
  ).toHaveCount(4);
  await page
    .getByRole("button", { name: "Fermer les résultats", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Supprimer le groupe", exact: true })
    .click();
  await page
    .getByRole("dialog")
    .getByRole("button", { name: "Supprimer le groupe", exact: true })
    .click();
  await expect(page.locator(".project-count")).toContainText("0 points");
  await page
    .getByRole("button", {
      name: "Annuler la dernière modification",
      exact: true,
    })
    .click();
  await expect(page.locator(".project-count")).toContainText("4 points");
});
test("range tools merge cuts and DMS coordinate input is saved as geographic numbers", async ({
  page,
}) => {
  await navigate(page, "Coupes horizontales");
  await page
    .getByRole("button", { name: "Hauteurs par intervalle", exact: true })
    .click();
  let dialog = page.getByRole("dialog");
  await dialog.getByLabel("Hauteur initiale, m", { exact: true }).fill("10");
  await dialog.getByLabel("Hauteur finale, m", { exact: true }).fill("30");
  await dialog.getByLabel("Pas en hauteur, m", { exact: true }).fill("10");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await expect(
    page
      .getByRole("region", { name: "Coupes horizontales", exact: true })
      .locator("tbody tr"),
  ).toHaveCount(6);
  await navigate(page, "Paramètres");
  await page
    .getByRole("button", {
      name: "Coordonnées en degrés / minutes / secondes",
      exact: true,
    })
    .click();
  dialog = page.getByRole("dialog");
  await dialog.getByRole("radio").last().check();
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await navigate(page, "Origine et grille");
  await page
    .getByLabel("Longitude du site, °", { exact: true })
    .fill("4° 01′ 30″ W");
  await page.getByLabel("Longitude du site, °", { exact: true }).press("Tab");
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  const path = ".tools/test-results/refined-project.emfp";
  await (await download).saveAs(path);
  const project = JSON.parse(readFileSync(path, "utf8"));
  expect(project.settings.site_coordinates[1]).toBeCloseTo(-4.025, 7);
  expect(
    project.horizontal_cuts.map((c: { height_m: number }) => c.height_m),
  ).toEqual([10, 18, 20, 24, 29, 30]);
});
test("terrain curves can be drawn, edited, triangulated and restored", async ({
  page,
}) => {
  await navigate(page, "Terrain");
  await page
    .getByRole("button", { name: "Courbe de niveau", exact: true })
    .click();
  const map = page.locator("svg.local-map"),
    box = (await map.boundingBox())!;
  for (const [x, y] of [
    [0.3, 0.4],
    [0.4, 0.35],
    [0.55, 0.4],
  ])
    await map.click({ position: { x: box.width * x, y: box.height * y } });
  await page.getByRole("button", { name: "Terminer (3)", exact: true }).click();
  const curves = page.getByRole("region", {
    name: "Courbes de niveau",
    exact: true,
  });
  await expect(curves.locator("tbody tr")).toHaveCount(1);
  await curves
    .getByRole("textbox", { name: "Altitude, m", exact: true })
    .fill("20");
  await curves
    .getByRole("textbox", { name: "Altitude, m", exact: true })
    .press("Tab");
  await page
    .getByRole("button", { name: "Fermer une courbe de niveau", exact: true })
    .click();
  await expect(curves.getByRole("checkbox")).toBeChecked();
  await page
    .getByRole("button", { name: "Ajouter un sommet de relief", exact: true })
    .click();
  const dialog = page.getByRole("dialog");
  await dialog
    .getByRole("button", { name: "Insérer après le sommet", exact: true })
    .click();
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await page
    .getByRole("button", { name: "Afficher la triangulation", exact: true })
    .click();
  await expect(map.getByLabel("Triangulation du terrain")).toBeVisible();
  await page
    .getByRole("button", {
      name: "Dupliquer le relief sélectionné",
      exact: true,
    })
    .click();
  await expect(curves.locator("tbody tr")).toHaveCount(2);
  await page
    .getByRole("button", {
      name: "Annuler la dernière modification",
      exact: true,
    })
    .click();
  await expect(curves.locator("tbody tr")).toHaveCount(1);
  await page.screenshot({ path: ".tools/test-results/terrain-curves.png" });
});
test("3D volume command computes and displays a real exposure surface", async ({
  page,
}) => {
  await navigate(page, "3D");
  await page
    .getByRole("button", { name: "Volumes de sécurité EMF 3D", exact: true })
    .click();
  const dialog = page.getByRole("dialog");
  await dialog
    .getByLabel("Demi-largeur du volume, m", { exact: true })
    .fill("12");
  await dialog.getByLabel("Pas du maillage, m", { exact: true }).fill("1");
  await dialog
    .getByRole("button", { name: "Calculer le volume", exact: true })
    .click();
  await expect(page.locator(".volume-summary")).toBeVisible();
  expect(
    Number(
      await page.locator(".volume-summary").getAttribute("data-triangles"),
    ),
  ).toBeGreaterThan(0);
  await expect(page.locator(".scene-3d canvas")).toBeVisible();
  await expect(page.getByRole("alert")).toHaveCount(0);
  await page.screenshot({ path: ".tools/test-results/exposure-volume.png" });
});
test("owner lists and custom tile providers are saved and usable in their screens", async ({
  page,
}) => {
  await navigate(page, "Paramètres");
  await page
    .getByRole("button", { name: "Liste des propriétaires", exact: true })
    .click();
  let dialog = page.getByRole("dialog");
  await dialog
    .getByLabel("Propriétaires", { exact: true })
    .fill("Opérateur A\nOpérateur B");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await page
    .getByRole("button", { name: "Fonds de carte personnalisés", exact: true })
    .click();
  dialog = page.getByRole("dialog");
  await dialog
    .getByRole("button", { name: "Ajouter un fond", exact: true })
    .click();
  await dialog
    .getByRole("textbox", { name: "Nom", exact: true })
    .fill("Carte de test");
  await dialog
    .getByRole("textbox", { name: "URL des tuiles", exact: true })
    .fill("https://cartes.example.invalid/{z}/{x}/{y}.png");
  await dialog
    .getByRole("textbox", { name: "Attribution", exact: true })
    .fill("Fond de test");
  await dialog.getByRole("button", { name: "Appliquer", exact: true }).click();
  await navigate(page, "Sources rayonnantes");
  await expect(
    page
      .getByRole("region", { name: "Sources rayonnantes", exact: true })
      .getByRole("combobox", { name: "Propriétaire", exact: true })
      .first()
      .locator("option"),
  ).toContainText(["Opérateur A", "Opérateur B"]);
  await navigate(page, "Origine et grille");
  await expect(
    page.getByRole("combobox", { name: "Fond de carte", exact: true }),
  ).toContainText("Carte de test");
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  const saved = ".tools/test-results/custom-settings.emfp";
  await (await download).saveAs(saved);
  const p = JSON.parse(readFileSync(saved, "utf8"));
  expect(p.owner_names).toEqual(["Opérateur A", "Opérateur B"]);
  expect(p.custom_maps[0].url).toBe(
    "https://cartes.example.invalid/{z}/{x}/{y}.png",
  );
});
test("contour appearance preserves computed results and property panes resize on smaller windows", async ({
  page,
}) => {
  await page.setViewportSize({ width: 1040, height: 760 });
  await navigate(page, "Coupes horizontales");
  await page
    .getByRole("button", { name: "Calculer avec la grille 1", exact: true })
    .click();
  await expect(page.locator(".status-message")).toHaveText("Calcul terminé");
  const contours = page.locator("svg.local-map > path[opacity]");
  const count = await contours.count();
  expect(count).toBeGreaterThan(0);
  await page
    .getByRole("button", { name: "Styles de ligne distincts", exact: true })
    .click();
  await expect(contours).toHaveCount(count);
  // HCT-07: the previous computed image retains its style until recalculation.
  await expect(page.locator('svg.local-map > path[stroke-dasharray="7 4"]')).toHaveCount(0);
  await page.getByRole("button", { name: "Calculer avec la grille 1", exact: true }).click();
  await expect(page.locator(".status-message")).toHaveText("Calcul terminé");
  await expect(
    page.locator('svg.local-map > path[stroke-dasharray="7 4"]'),
  ).not.toHaveCount(0);
  await page.getByLabel("Opacité des contours", { exact: true }).fill("0.5");
  await page.getByLabel("Opacité des contours", { exact: true }).press("Tab");
  await expect(contours.first()).toHaveAttribute("opacity", "0.5");
  const divider = page.getByRole("separator", {
    name: "Largeur du panneau de propriétés",
    exact: true,
  });
  await divider.focus();
  await divider.press("ArrowRight");
  await expect(divider).toHaveAttribute("aria-valuenow", "360");
  await expect(page.locator("svg.local-map")).toBeVisible();
  await page.setViewportSize({ width: 1480, height: 960 });
  await divider.press("End");
  await page.setViewportSize({ width: 1040, height: 760 });
  await expect
    .poll(async () => Number(await divider.getAttribute("aria-valuenow")))
    .toBeLessThan(800);
  expect(
    (await page.locator("svg.local-map").boundingBox())!.width,
  ).toBeGreaterThan(200);
  await page.screenshot({ path: ".tools/test-results/horizontal-refined.png" });
});
