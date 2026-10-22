import { test, expect } from "@playwright/test";
import { readFileSync } from "node:fs";
import { importXemf } from "../../src/lib/import-xemf";

test("Castelfranco antenna editor retains the inspector samples and embedded electrical tilt", async ({
  page,
}) => {
  await page.goto("/");
  await page.evaluate(() => localStorage.clear());
  await page.reload();
  const chooser = page.waitForEvent("filechooser");
  await page.getByRole("button", { name: "Fichier", exact: true }).click();
  await page.getByRole("button", { name: /^Ouvrir/ }).click();
  await (await chooser).setFiles("tests/fixtures/original-sample.xemf");
  const inspector = page.locator(".source-inspector");
  await expect(
    page
      .getByRole("region", { name: "Sources rayonnantes", exact: true })
      .getByRole("textbox", { name: "Modèle d’antenne", exact: true })
      .first(),
  ).toHaveValue("742265V02");
  const h = await inspector.locator('path[data-plane="H"]').getAttribute("d");
  const v = await inspector.locator('path[data-plane="V"]').getAttribute("d");
  await expect(
    inspector
      .getByRole("toolbar", { name: "Outils du diagramme d’antenne" })
      .getByRole("button"),
  ).toHaveCount(6);
  const tilt = page
    .getByRole("region", { name: "Sources rayonnantes", exact: true })
    .getByRole("textbox", { name: "Tilt élec., °", exact: true })
    .first();
  await expect(tilt).toHaveValue("6");
  await inspector
    .getByRole("button", {
      name: "Tourner le diagramme vertical vers le haut (+1°)",
      exact: true,
    })
    .click();
  await expect(tilt).toHaveValue("7");
  const rotatedPath = await inspector
    .locator('path[data-plane="V"]')
    .getAttribute("d");
  const peak = rotatedPath!.split(" ")[5].slice(1).split(",").map(Number);
  expect(peak[0]).toBeCloseTo(100 + 83 * Math.cos((5 * Math.PI) / 180), 10);
  expect(peak[1]).toBeCloseTo(100 + 83 * Math.sin((5 * Math.PI) / 180), 10);
  await page
    .getByRole("button", {
      name: "Annuler la dernière modification",
      exact: true,
    })
    .click();
  await expect(tilt).toHaveValue("6");
  await expect(inspector.locator('path[data-plane="V"]')).toHaveAttribute(
    "d",
    v!,
  );
  await page.getByRole("button", { name: "Rétablir", exact: true }).click();
  await expect(tilt).toHaveValue("7");
  await inspector
    .getByRole("button", {
      name: "Tourner le diagramme vertical vers le bas (−1°)",
      exact: true,
    })
    .click();
  await expect(tilt).toHaveValue("6");
  await expect(inspector.locator('path[data-plane="V"]')).toHaveAttribute(
    "d",
    v!,
  );
  await expect(inspector.locator('circle[data-loss-db="3"]')).toHaveAttribute(
    "r",
    "74.7",
  );
  await inspector.locator("fieldset").first().screenshot({
    path: ".tools/test-results/antenna-castelfranco-refined.png",
  });
  await page
    .getByRole("button", {
      name: "Modifier le diagramme d’antenne",
      exact: true,
    })
    .click();
  const dialog = page.getByRole("dialog");
  await expect(dialog.locator('path[data-plane="H"]')).toHaveAttribute("d", h!);
  await dialog.getByRole("button", { name: "Plan V", exact: true }).click();
  await expect(dialog.locator('path[data-plane="V"]')).toHaveAttribute("d", v!);
  await expect(
    dialog
      .getByRole("textbox", { name: "Atténuation, dB", exact: true })
      .nth(6),
  ).toHaveValue("0");
  await dialog
    .getByRole("button", { name: "Appliquer le diagramme", exact: true })
    .click();
  await expect(inspector.locator('path[data-plane="V"]')).toHaveAttribute(
    "d",
    v!,
  );
  const download = page.waitForEvent("download");
  await page
    .getByRole("button", { name: "Enregistrer le projet", exact: true })
    .click();
  const saved = await download;
  const savedPath = await saved.path();
  const savedProject = JSON.parse(readFileSync(savedPath!, "utf8"));
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const original = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "Castelfranco Emilia TAV2.xemf",
  );
  expect(savedProject.sources[0].pattern_h).toEqual(
    original.sources[0].pattern_h,
  );
  expect(savedProject.sources[0].pattern_v).toEqual(
    original.sources[0].pattern_v,
  );
  expect(savedProject.sources[0].electrical_downtilt_deg).toBe(6);
});
