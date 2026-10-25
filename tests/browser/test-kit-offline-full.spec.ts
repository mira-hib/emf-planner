import { test, expect } from "@playwright/test";

test("CAL-11 ROB-08: previously viewed map tiles reload from persistent browser cache while offline",async({page,baseURL})=>{
  const png=Buffer.from("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+aHe0AAAAASUVORK5CYII=","base64");
  let remoteRequests=0;
  await page.route("**/*",async route=>{
    const url=route.request().url();
    if(/^https?:/.test(url)&&new URL(url).origin!==new URL(baseURL!).origin){remoteRequests++;await route.fulfill({contentType:"image/png",body:png});}
    else await route.continue();
  });
  await page.goto("/");
  await page.getByRole("navigation").getByRole("button",{name:"Origine et grille",exact:true}).click();
  const providers=page.getByRole("combobox",{name:"Fond de carte",exact:true});
  await providers.selectOption("OpenStreetMap");
  await expect(page.locator(".leaflet-tile-loaded").first()).toBeVisible();
  const endpoints=await page.locator(".leaflet-tile-loaded").evaluateAll(images=>images.map(image=>image.getAttribute("data-tile-url")));
  expect(endpoints.length).toBeGreaterThan(0);expect(remoteRequests).toBeGreaterThan(0);
  await providers.selectOption("Calibrated map image");await expect(page.locator(".leaflet-tile")).toHaveCount(0);
  // Abort any attempted download: this proves use of the persistent pixel cache,
  // independently from the mocked server used to seed the first view.
  await page.unroute("**/*");await page.context().setOffline(true);
  await providers.selectOption("OpenStreetMap");
  await expect(page.locator(".leaflet-tile-loaded").first()).toBeVisible();
  const restored=await page.locator(".leaflet-tile-loaded").evaluateAll(images=>images.map(image=>image.getAttribute("data-tile-url")));
  expect(restored.some(endpoint=>endpoints.includes(endpoint))).toBe(true);
  await expect(page.locator(".map-provider-status")).toHaveCount(0);
  await page.screenshot({path:test.info().outputPath("cached-tiles-offline.png")});
});
