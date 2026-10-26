import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import { Document, Paragraph, Packer } from "docx";
import JSZip from "jszip";
import { newProject, newSource, newPoint, normalizeProject } from "../src/lib/project.ts";
import { sourceCellPatch } from "../src/lib/source-clipboard.ts";
import { renderWordTemplate } from "../src/lib/word-template.ts";

test("ROB-01: every kit fuzz string survives project round-trip and Word literal aliases, including 5000 characters and reserved alias text",async()=>{
  const strings=readFileSync("test/emf_test_kit/data/fuzz/strings_text.txt","utf8").replace(/^\uFEFF/,"").split(/\r?\n/);
  if(strings.at(-1)==="")strings.pop();
  assert.equal(strings.length,28);assert.equal(Math.max(...strings.map(text=>text.length)),5000);
  const project=newProject();
  project.sources=strings.map((text,index)=>{
    let source=newSource(project.origin);
    for(const key of ["name","sector_id","service_type","antenna_model","owner"] as const)source={...source,...sourceCellPatch(key,text)};
    source.id=`fuzz-${index}`;return source;
  });
  project.buildings=strings.map((name,index)=>({id:`building-${index}`,name,active:true,height_m:10,foundation_m:0,attenuation_db:3,points:[[0,0],[10,0],[10,10],[0,10]]}));
  project.lines=strings.map((text,index)=>({id:`label-${index}`,name:text,text,kind:"label",color:"black",width:1,points:[[index,index]]}));
  project.control_points=strings.map((text,index)=>({...newPoint(project.origin,`P${index}`),description:text,heights:[{id:`h-${index}`,height_m:1.5,attenuation_db:0,measured:0,description:text}]}));
  const aliases=Object.fromEntries(strings.map((text,index)=>[`fuzz_${index}`,text]));
  project.report_aliases=aliases;project.settings.report_aliases=aliases;
  const reopened=normalizeProject(JSON.parse(JSON.stringify(project)));
  for(const key of ["name","sector_id","service_type","antenna_model","owner"] as const)assert.deepEqual(reopened.sources.map(source=>source[key]),strings,key);
  assert.deepEqual(reopened.buildings.map(building=>building.name),strings);
  assert.deepEqual(reopened.lines.map(line=>line.text),strings);
  assert.deepEqual(reopened.control_points.map(point=>point.heights![0].description),strings);
  assert.deepEqual(reopened.report_aliases,aliases);
  const template=new Uint8Array(await Packer.toArrayBuffer(new Document({sections:[{children:strings.map((_,index)=>new Paragraph(`{fuzz_${index}}`))}]})));
  const result=await renderWordTemplate(reopened,template);assert.equal(result.replaced,28);assert.deepEqual(result.unknownAliases,[]);
  const zip=await JSZip.loadAsync(result.bytes),xml=await zip.file("word/document.xml")!.async("string");
  const entities:Record<string,string>={amp:"&",lt:"<",gt:">",quot:'"',apos:"'"};
  const actual=[...xml.matchAll(/<w:t(?=[\s>])[^>]*>([\s\S]*?)<\/w:t>/g)].map(match=>match[1].replace(/&(amp|lt|gt|quot|apos);/g,(_,entity:string)=>entities[entity]));
  assert.deepEqual(actual,strings);assert.doesNotMatch(xml,/<script>|<w:tbl>/);
});
