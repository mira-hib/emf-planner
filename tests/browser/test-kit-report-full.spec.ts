import { test, expect } from "@playwright/test";
import { readFileSync } from "node:fs";
import JSZip from "jszip";

test("RPT-05 VCT-13 HCT-14: horizontal and vertical Word reports download actual OOXML with cut images", async ({page})=>{
  test.setTimeout(120000);
  const errors:string[]=[];page.on("pageerror",error=>errors.push(error.message));
  await page.goto("/");await page.evaluate(()=>localStorage.clear());await page.reload();
  const chooser=page.waitForEvent("filechooser");
  await page.getByRole("button",{name:"Fichier",exact:true}).click();
  await page.getByRole("button",{name:/^Ouvrir/}).click();
  await (await chooser).setFiles("tests/fixtures/original-sample.xemf");
  await expect(page.locator(".project-count")).toContainText("9 sources");
  for(const [section,command,title]of [
    ["Coupes horizontales","Rapport des coupes horizontales","Horizontal cut at height"],
    ["Coupes verticales","Rapport des coupes verticales","Vertical cut along azimuth"],
  ]){
    await page.getByRole("navigation").getByRole("button",{name:section,exact:true}).click();
    await page.getByRole("button",{name:command,exact:true}).click();
    const dialog=page.getByRole("dialog",{name:command,exact:true});
    const wait=page.waitForEvent("download");await dialog.getByRole("button",{name:"Exporter Word",exact:true}).click();
    const path=test.info().outputPath(`${section}.docx`);await (await wait).saveAs(path);
    const zip=await JSZip.loadAsync(readFileSync(path)),xml=await zip.file("word/document.xml")!.async("string");
    expect(xml).toContain(title);expect(xml).toContain("<w:drawing>");
    expect(Object.keys(zip.files).filter(name=>/^word\/media\/.*\.svg$/.test(name)).length).toBeGreaterThan(0);
    await expect(page.getByRole("alert")).toHaveCount(0);
    await dialog.getByRole("button",{name:"Fermer la fenêtre",exact:true}).click();
  }
  expect(errors).toEqual([]);
});
