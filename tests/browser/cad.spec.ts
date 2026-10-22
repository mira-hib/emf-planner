import { test, expect } from "@playwright/test";
import { readFileSync } from "node:fs";
test("Castelfranco CAD uses ten native layers, saved contours and paper scale in each export", async ({
  page,
}) => {
  const errors: string[] = [];
  page.on("pageerror", (error) => errors.push(error.message));
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles("tests/fixtures/original-sample.xemf");
  await expect(page.locator(".project-count")).toContainText("9 sources");
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "CAO", exact: true })
    .click();
  const plan = page.getByLabel("Plan CAO à l’échelle");
  await expect(plan).toHaveAttribute("viewBox", "0 0 420 297");
  await expect(
    page.locator(".cad-layer-list input[type=checkbox]"),
  ).toHaveCount(10);
  await expect(plan.locator('[data-cad-layer="sources"] circle')).toHaveCount(
    9,
  );
  await expect(plan.locator('[data-cad-layer="origin"] path')).toHaveCount(2);
  await expect(
    plan.locator('[data-cad-layer="buildings"] path'),
  ).not.toHaveCount(0);
  await page.screenshot({
    path: ".tools/cad-fidelity/cad-castelfranco-refined.png",
  });
  await page
    .locator(".cad-layer-list")
    .getByLabel("Bâtiments", { exact: true })
    .uncheck();
  await expect(plan.locator('[data-cad-layer="buildings"] path')).toHaveCount(
    0,
  );
  await page.getByLabel("Format CAO", { exact: true }).selectOption("A4");
  await expect(plan).toHaveAttribute("viewBox", "0 0 210 297");
  await page.getByLabel("Échelle CAO", { exact: true }).selectOption("2000");
  await page
    .getByRole("button", { name: "Agrandir le plan CAO", exact: true })
    .click();
  await expect(plan).not.toHaveAttribute("viewBox", "0 0 210 297");
  const svgDownload = page.waitForEvent("download");
  await page.getByRole("button", { name: "SVG", exact: true }).click();
  await (await svgDownload).saveAs(".tools/cad-fidelity/cad-portrait.svg");
  const svg = readFileSync(".tools/cad-fidelity/cad-portrait.svg", "utf8");
  expect(svg).toContain('viewBox="0 0 210 297"');
  expect(svg).toContain('width="210mm"');
  expect(svg).toContain("M1:2000");
  const dxfDownload = page.waitForEvent("download");
  await page.getByRole("button", { name: "DXF", exact: true }).click();
  await (await dxfDownload).saveAs(".tools/cad-fidelity/cad-portrait.dxf");
  const dxf = readFileSync(".tools/cad-fidelity/cad-portrait.dxf", "utf8");
  expect(dxf).toContain("$INSUNITS\r\n70\r\n4");
  expect(dxf).toContain("M1:2000");
  const saved = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  await (await saved).saveAs(".tools/cad-fidelity/cad-options.emfp");
  const data = JSON.parse(
    readFileSync(".tools/cad-fidelity/cad-options.emfp", "utf8"),
  );
  expect(data.cad_options).toEqual({
    paper: "A4",
    scale: 2000,
    layers: { buildings: false },
  });
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "Coupes horizontales", exact: true })
    .click();
  await page
    .getByRole("button", {
      name: "Rapport des coupes horizontales",
      exact: true,
    })
    .click();
  const report = page.getByRole("dialog", {
    name: "Rapport des coupes horizontales",
    exact: true,
  });
  await expect(
    report.getByRole("button", { name: "Exporter Word", exact: true }),
  ).toBeDisabled();
  const reportDownload = page.waitForEvent("download");
  await report
    .getByRole("button", { name: "Exporter PDF", exact: true })
    .click();
  await (
    await reportDownload
  ).saveAs(".tools/cad-fidelity/horizontal-summary.pdf");
  expect(
    readFileSync(".tools/cad-fidelity/horizontal-summary.pdf")
      .subarray(0, 5)
      .toString(),
  ).toBe("%PDF-");
  expect(errors).toEqual([]);
});
