import { test, expect, type Page } from "@playwright/test";
import { readFileSync } from "node:fs";
import { newProject, newSource } from "../../src/lib/project";
import { toGeo } from "../../src/lib/geometry";
import type { PlannerProject } from "../../src/types";
const button=(page:Page,name:string)=>page.getByRole("button",{name,exact:true});
const nav=(page:Page,name:string)=>page.getByRole("navigation").getByRole("button",{name,exact:true});
async function open(page:Page,p:PlannerProject){const chooser=page.waitForEvent("filechooser");await button(page,"Fichier").click();await page.getByRole("button",{name:/^Ouvrir/}).click();await(await chooser).setFiles({name:"kit.emfp",mimeType:"application/json",buffer:Buffer.from(JSON.stringify(p))});await expect(page.locator(".status-message")).toHaveText("Projet ouvert");}
async function save(page:Page):Promise<PlannerProject>{const pending=page.waitForEvent("download");await button(page,"Enregistrer le projet").click();const path=test.info().outputPath(`project-${Date.now()}.emfp`);await(await pending).saveAs(path);return JSON.parse(readFileSync(path,"utf8"));}
function base(){const p=newProject();p.settings.show_basemap=false;p.settings.reflection=1;p.settings.consider_terrain=true;p.settings.horizontal_grid_divisions=[50,50];p.settings.calculation_area_m={x_min:-100,x_max:100,y_min:-100,y_max:100};return p;}
test.beforeEach(async({page})=>{await page.goto("/");await page.evaluate(()=>localStorage.clear());await page.reload();});

test("BLD-09 BLD-10 BLD-16: foundation methods, actual one-pixel movement and undo preserve geometry",async({page})=>{
 const p=base();p.buildings=[{id:"b",name:"Test building",height_m:15,foundation_m:0,attenuation_db:3,active:true,points:[[0,0],[100,0],[100,100],[0,100]]}];p.terrain_points=[[0,0,100],[100,0,102],[100,100,104],[0,100,106]].map(([x,y,elevation],i)=>({id:String(i),x,y,elevation}));
 await open(page,p);await nav(page,"Bâtiments").click();const table=page.getByRole("region",{name:"Bâtiments",exact:true});await table.getByLabel("N° / Nom",{exact:true}).click();
 await button(page,"Altitude de la base sélectionnée").click();let dialog=page.getByRole("dialog");await dialog.getByLabel("Méthode d’altitude de base").selectOption("mean");await dialog.getByRole("button",{name:"Appliquer",exact:true}).click();expect((await save(page)).buildings[0].foundation_m).toBe(103);
 const pixel=await page.getByLabel("Plan de calcul, coordonnées en mètres",{exact:true}).evaluate(element=>1/(element as unknown as SVGSVGElement).getScreenCTM()!.a);
 await button(page,"Déplacer l’objet d’un pixel à droite").click();expect((await save(page)).buildings[0].points[0][0]).toBeCloseTo(pixel,9);
 await button(page,"Annuler la dernière modification").click();expect((await save(page)).buildings[0].points[0]).toEqual([0,0]);
 await page.screenshot({path:test.info().outputPath("buildings-foundation.png")});
});

test("BLD-07 BLD-08: OSM options and duplicate download use real dialog; network failure leaves project intact",async({page})=>{
 const p=base(),a=toGeo(0,0,p.origin),b=toGeo(10,0,p.origin),c=toGeo(10,10,p.origin);const data={elements:[{type:"way",id:1,tags:{building:"yes"},geometry:[a,b,c,a].map(([lat,lon])=>({lat,lon}))}]};
 await open(page,p);await nav(page,"Bâtiments").click();await page.route("https://overpass-api.de/api/interpreter",route=>route.fulfill({json:data}));
 for(let i=0;i<2;i++){await button(page,"Bâtiments OpenStreetMap").click();const dialog=page.getByRole("dialog");await dialog.getByLabel("Hauteur par défaut, m").fill("37");await dialog.getByRole("button",{name:"Appliquer",exact:true}).click();await expect(dialog).toHaveCount(0);}
 let saved=await save(page);expect(saved.buildings).toHaveLength(1);expect(saved.buildings[0].height_m).toBe(37);
 await page.unroute("https://overpass-api.de/api/interpreter");await page.route("https://overpass-api.de/api/interpreter",route=>route.fulfill({status:503,body:"offline"}));
 await button(page,"Bâtiments OpenStreetMap").click();await page.getByRole("dialog").getByRole("button",{name:"Appliquer",exact:true}).click();await expect(page.getByRole("dialog").getByRole("alert")).toContainText("503");await page.getByRole("dialog").getByRole("button",{name:"Annuler",exact:true}).click();saved=await save(page);expect(saved.buildings).toHaveLength(1);
});

test("CAL-01 CAL-02 CAL-04 CAL-06 CAL-12: image calibration validates length and survives save/reopen",async({page})=>{
 const p=base();await open(page,p);await nav(page,"Calibrer le fond de plan").click();const image=await page.evaluate(()=>{const canvas=document.createElement("canvas");canvas.width=400;canvas.height=200;const ctx=canvas.getContext("2d")!;ctx.fillStyle="white";ctx.fillRect(0,0,400,200);ctx.fillStyle="black";ctx.fillRect(100,100,200,2);return canvas.toDataURL("image/png").split(",")[1];});
 const chooser=page.waitForEvent("filechooser");await button(page,"Charger une image").click();await(await chooser).setFiles({name:"map.png",mimeType:"image/png",buffer:Buffer.from(image,"base64")});await expect(page.getByLabel("Échelle de calibration",{exact:true})).toHaveText("0.500000 m/pixel");
 await page.getByLabel("Longueur de référence, m",{exact:true}).fill("0");await button(page,"Appliquer la calibration").click();await expect(page.getByRole("alert")).toContainText("5 et 1000");
 await page.getByLabel("Longueur de référence, m",{exact:true}).fill("100");await page.getByLabel("Carte 0 X, m",{exact:true}).fill("50");await button(page,"Appliquer la calibration").click();await expect(page.locator(".status-message")).toContainText("Image calibrée");
 const saved=await save(page);expect(saved.background_image).toBeDefined();expect((saved.background_image as any).calibration.transform.scale).toBe(.5);
 await page.screenshot({path:test.info().outputPath("calibrated-image.png")});await page.reload();await nav(page,"Calibrer le fond de plan").click();await expect(page.getByLabel("Carte 0 X, m",{exact:true})).toHaveValue("50");
});

test("VCT-04 VCT-09 VCT-10 HCT-10: real matrix calculation, stale image, dimension toggles and map cut preview",async({page})=>{
 const p=base();p.settings.consider_terrain=false;p.sources=[{...newSource(p.origin,"ISO"),power_w:1000,gain_dbi:0,height_m:10,pattern_h:Array(360).fill(0),pattern_v:Array(360).fill(0)}];p.limits=p.limits.map((l,i)=>({...l,active:i===0,use_alpha24:false,use_ftdc:false,use_fpr:false,rows:[{frequency:1e6,value:6,exponent:0}]}));p.vertical_cuts=[{id:"v",active:true,name:"ISO",x:0,y:0,azimuth_deg:0,forward_m:50,backward_m:50,max_height_m:50}];
 await open(page,p);await nav(page,"Coupes verticales").click();await button(page,"Calculer toutes les coupes verticales").click();await expect(page.locator(".status-message")).toHaveText("Calcul terminé");const canvas=page.getByLabel("Coupe verticale",{exact:true});await expect(canvas).toHaveAttribute("width","51");
 const table=page.getByRole("region",{name:"Coupes verticales",exact:true});const height=table.getByLabel("H max., m",{exact:true});await height.fill("80");await height.press("Tab");await expect(page.getByText("Paramètres modifiés. La dernière image calculée est conservée jusqu’au recalcul.")).toBeVisible();await expect(canvas).toHaveAttribute("height","51");
 await button(page,"Ajouter une cote").click();expect((await save(page)).vertical_cuts[0].dimensions).toHaveLength(1);await button(page,"Supprimer toutes les cotes").click();expect((await save(page)).vertical_cuts[0].dimensions??[]).toHaveLength(0);
 await page.getByText("Options d’affichage de la coupe",{exact:true}).click();await page.getByRole("checkbox",{name:"Ligne au-dessus du terrain",exact:true}).check();await expect(page.locator('polyline[aria-label="Ligne au-dessus du terrain"]')).toHaveCount(1);
 await nav(page,"Coupes horizontales").click();const cutLine=page.getByRole("button",{name:"Coupe verticale 0°",exact:true}),bounds=await cutLine.boundingBox();expect(bounds).not.toBeNull();await cutLine.click({position:{x:bounds!.width/2,y:15}});const dialog=page.getByRole("dialog");await dialog.getByRole("button",{name:"Afficher la coupe",exact:true}).click();await expect(table).toBeVisible();
});

test("SET-06 SET-07 SET-08: owners/colors and coordinates persist globally while calculation settings stay per project",async({page})=>{
 const p=base();await open(page,p);await nav(page,"Paramètres").click();await button(page,"+ Ajouter un propriétaire").click();const owner=page.locator(".settings-page fieldset").filter({has:page.locator("legend",{hasText:"Propriétaires"})});
 await owner.locator('input:not([type="color"])').fill("Owner kit");await owner.locator('input[type="color"]').fill("#123456");await page.getByLabel("Format des coordonnées",{exact:true}).selectOption("dms");await page.reload();await nav(page,"Paramètres").click();await expect(page.getByLabel("Format des coordonnées",{exact:true})).toHaveValue("dms");
 const saved=await save(page);expect(saved.owner_names).toContain("Owner kit");expect(saved.owner_colors?.["Owner kit"]).toBe("#123456");
});
