import { test, expect } from "@playwright/test";
import { readFileSync } from "node:fs";
import { importXemf } from "../../src/lib/import-xemf";

test("Castelfranco 3D renders terrain, orthographic views, navigation and layers", async ({
  page,
}) => {
  const bytes = readFileSync("tests/fixtures/original-sample.xemf");
  const project = importXemf(
    bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
    "Castelfranco Emilia TAV2",
  );
  project.settings.show_basemap = false;
  const errors: string[] = [];
  page.on("pageerror", (error) => errors.push(error.message));
  await page.goto("/");
  await page.evaluate(
    (p) => localStorage.setItem("emf-planner-autosave-v2", JSON.stringify(p)),
    project,
  );
  await page.reload();
  await page
    .getByRole("navigation")
    .getByRole("button", { name: "3D", exact: true })
    .click();
  const canvas = page.locator(".scene-3d canvas");
  await expect(canvas).toBeVisible();
  await expect(
    page.getByRole("toolbar", { name: "Navigation 3D" }),
  ).toBeVisible();
  const homeView = await canvas.screenshot();
  await page.screenshot({
    path: ".tools/scene-captures/castelfranco-3d-home.png",
  });
  await page
    .getByRole("button", { name: "Vue de dessus", exact: true })
    .click();
  expect(homeView.equals(await canvas.screenshot())).toBe(false);
  await page.screenshot({
    path: ".tools/scene-captures/castelfranco-3d-top.png",
  });
  await page.getByLabel("Orientation 3D").selectOption("right");
  await page
    .getByRole("button", { name: "Zoom avant 3D", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Zoom arrière 3D", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Zoom sur l’ensemble", exact: true })
    .click();
  await page
    .getByRole("button", { name: "Déplacement 3D", exact: true })
    .click();
  await expect(
    page.getByRole("button", { name: "Déplacement 3D", exact: true }),
  ).toHaveAttribute("aria-pressed", "true");
  await page.getByRole("button", { name: "Affichage 3D", exact: true }).click();
  const withBuildings = await canvas.screenshot();
  await page
    .getByRole("checkbox", { name: "Bâtiments", exact: true })
    .uncheck();
  await expect(
    page.getByRole("checkbox", { name: "Bâtiments", exact: true }),
  ).not.toBeChecked();
  expect(withBuildings.equals(await canvas.screenshot())).toBe(false);
  await page.getByRole("checkbox", { name: "Bâtiments", exact: true }).check();
  await page.getByRole("button", { name: "Vue initiale", exact: true }).click();
  await expect(page.locator(".scene-3d canvas")).toHaveCount(1);
  expect(errors).toEqual([]);
});
