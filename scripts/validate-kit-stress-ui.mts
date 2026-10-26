/** Real application/Worker counterpart of the ROB-05 engine workload. */
import { chromium } from "@playwright/test";
import { readFileSync, writeFileSync, mkdirSync } from "node:fs";
import { resolve, join } from "node:path";
import { spawn } from "node:child_process";
import { performance } from "node:perf_hooks";
import { createHash } from "node:crypto";

const output=resolve(process.argv[2]??"test validation/2026-10-25/stress-rob05-ui");
mkdirSync(output,{recursive:true});
const input=JSON.parse(readFileSync("test validation/2026-10-25/stress-rob05-final/stress-project.emfp","utf8"));
input.settings.show_basemap=false;input.settings.worker_count=4;
const bytes=Buffer.from(JSON.stringify(input));writeFileSync(join(output,"stress-project.emfp"),bytes);
const record:Record<string,unknown>={scenario:"ROB-05",status:"running",sources:input.sources.length,buildings:input.buildings.length,cuts:input.horizontal_cuts.length,
  input_sha256:createHash("sha256").update(bytes).digest("hex"),started_at:new Date().toISOString(),
  scope:"Real browser application, four calculation Workers, all 50 grids retained until project save. JS heap is not total process memory; no 4GB hardware limit is imposed."};
const persist=()=>writeFileSync(join(output,"result.json"),JSON.stringify(record,null,2));persist();
const logs:string[]=[];
const server=spawn(process.execPath,["node_modules/vite/bin/vite.js","preview","--outDir",resolve(".tools/kit-preview-dist"),"--host","127.0.0.1","--port","1434","--strictPort"],{windowsHide:true,stdio:["ignore","pipe","pipe"]});
server.stdout.on("data",data=>logs.push(String(data)));server.stderr.on("data",data=>logs.push(String(data)));
let browser:Awaited<ReturnType<typeof chromium.launch>>|undefined;
try{
  for(let i=0;i<100;i++){try{if((await fetch("http://127.0.0.1:1434")).ok)break;}catch{}if(i===99)throw new Error("Preview server unavailable");await new Promise(done=>setTimeout(done,200));}
  browser=await chromium.launch({channel:process.platform==="win32"?"msedge":undefined,headless:true,args:["--use-angle=swiftshader","--enable-precise-memory-info"]});
  const page=await browser.newPage({viewport:{width:1480,height:960}}),errors:string[]=[];
  page.setDefaultTimeout(60000);page.on("pageerror",error=>errors.push(error.message));
  page.on("crash",()=>{record.crashed=true;persist();});
  await page.goto("http://127.0.0.1:1434");
  const choose=page.waitForEvent("filechooser");await page.getByRole("button",{name:"Fichier",exact:true}).click();
  await page.getByRole("button",{name:/^Ouvrir/}).click();
  const importStart=performance.now();await(await choose).setFiles({name:"ROB05.emfp",mimeType:"application/json",buffer:bytes});
  await page.locator(".project-count").filter({hasText:"1000 sources"}).waitFor();
  record.import_seconds=(performance.now()-importStart)/1000;persist();
  await page.getByRole("navigation").getByRole("button",{name:"Coupes horizontales",exact:true}).click();
  const calculate=page.getByRole("button",{name:"Calculer avec la grille 1",exact:true});
  const started=performance.now();await calculate.click();
  // Navigation must stay usable while the calculation runs in Workers.
  await page.getByRole("navigation").getByRole("button",{name:"Sources rayonnantes",exact:true}).click();
  await page.getByRole("navigation").getByRole("button",{name:"Coupes horizontales",exact:true}).click();
  record.navigation_during_calculation=true;persist();
  const heartbeat=setInterval(()=>{record.elapsed_seconds=(performance.now()-started)/1000;persist();},10000);
  try{await page.locator(".status-message").filter({hasText:/^Calcul terminé$/}).waitFor({timeout:50*60*1000});}finally{clearInterval(heartbeat);}
  record.calculation_seconds=(performance.now()-started)/1000;
  await page.screenshot({path:join(output,"calculated-map.png")});
  const download=page.waitForEvent("download");await page.getByRole("button",{name:"Enregistrer le projet",exact:true}).click();
  const savedPath=join(output,"calculated-project.emfp");await(await download).saveAs(savedPath);
  const saved=JSON.parse(readFileSync(savedPath,"utf8"));
  record.retained_cuts=saved.saved_cut_results?.horizontal?.length??0;
  if(record.retained_cuts!==50)throw new Error(`Expected 50 saved calculated cuts, got ${record.retained_cuts}`);
  if(errors.length)throw new Error(errors.join("; "));
  record.renderer_js_heap_bytes=await page.evaluate(()=> (performance as Performance&{memory?:{usedJSHeapSize:number}}).memory?.usedJSHeapSize);
  record.status="passed-browser-workload";
}catch(error){record.status="failed";record.error=String(error);process.exitCode=1;}
finally{record.finished_at=new Date().toISOString();persist();writeFileSync(join(output,"preview.log"),logs.join(""));await browser?.close();server.kill();}
console.log(JSON.stringify(record,null,2));
