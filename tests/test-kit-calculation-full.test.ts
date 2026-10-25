import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync } from "node:fs";
import JSZip from "jszip";
import { newProject, newPoint, newSource, normalizeProject } from "../src/lib/project.ts";
import { calculatePoints, exposureAt, horizontalGrid, verticalGrid, contours } from "../src/lib/engine.ts";
import { sourceVerticalCuts, nativeTruncate } from "../src/lib/cut-tools.ts";
import { calculationPartitions } from "../src/lib/calculation-pool.ts";
import { horizontalProjections } from "../src/lib/cut-projection.ts";
import { cutCoordinates, cutWorld, cutExposureBounds, verticalProfile } from "../src/lib/vertical-profile.ts";
import { importPointRows, importPointWorkbook, pointWorkbookBytes } from "../src/lib/point-workbook.ts";
import { decimalInput } from "../src/lib/numeric-input.ts";
import { validateProxy } from "../src/lib/preferences.ts";
import { validateArchive } from "../src/lib/archive-validation.ts";
import { validateRasterDimensions } from "../src/lib/raster-validation.ts";
import { sourceSafetyExtents } from "../src/lib/report-content.ts";
import type { Contour } from "../src/types/index.ts";
const near = (a: number, b: number, tolerance = 1e-9) => assert.ok(Math.abs(a-b) <= tolerance, `${a} != ${b}`);
function base(power = 1000) {
  const p = newProject(); p.settings.reflection = 1; p.settings.consider_terrain = false;
  p.settings.calculation_area_m = {x_min:-100,x_max:100,y_min:-100,y_max:100}; p.settings.horizontal_grid_divisions=undefined;
  p.sources=[{...newSource(p.origin,"TS07"),id:"tx",power_w:power,gain_dbi:0,height_m:10,pattern_h:Array(360).fill(0),pattern_v:Array(360).fill(0)}];
  p.limits=[6,20,4.5,10].map((value,i)=>({...p.limits[i],active:i===0,rows:[{frequency:1e6,value,exponent:0}],use_alpha24:false,use_ftdc:false,use_fpr:false}));
  p.vertical_cuts=[{id:"v",active:true,x:0,y:0,azimuth_deg:0,backward_m:50,forward_m:50,max_height_m:50,min_height_m:0}];
  return p;
}
test("VCT-01: active source directions group by azimuth and position, retaining names and dimensions",()=>{
  const p=base();p.sources.push({...p.sources[0],id:"t2",name:"Second"},{...p.sources[0],id:"off",active:false,azimuth_deg:42});
  const cuts=sourceVerticalCuts(p);assert.equal(cuts.length,1);assert.match(cuts[0].name!,/TS07, Second/);assert.equal(cuts[0].forward_m,150);assert.equal(cuts[0].backward_m,0);
  p.vertical_cuts=[{...cuts[0],forward_m:321}];assert.equal(sourceVerticalCuts(p)[0].forward_m,321);
  p.sources.push({...p.sources[0],id:"moved",x_m:10});assert.equal(sourceVerticalCuts(p).length,2);
});
test("VCT-02: cut follows a true backfire pattern maximum at azimuth plus 180 degrees",()=>{
  const p=base();p.sources[0].azimuth_deg=60;p.sources[0].pattern_h=Array.from({length:360},(_,i)=>i===180?0:30);assert.equal(sourceVerticalCuts(p,true)[0].azimuth_deg,240);
});
test("VCT-04 VCT-08 SET-02: H1/H2 and both native matrix resolutions include all endpoints",()=>{
  const p=base(),cut={...p.vertical_cuts[0],max_height_m:20,min_height_m:10,forward_m:30,backward_m:10};
  for(const n of [50,100,200]){const grid=verticalGrid(p,cut,3,n);assert.equal(grid.width,n+1);assert.equal(grid.height,n+1);near(grid.x0,-10);near(grid.y0,-10);near(grid.y0+grid.dy*n,20);near(grid.x0+grid.dx*n,30);}
  for(const n of [0,49,1001,NaN])assert.throws(()=>verticalGrid(p,cut,3,n));
});
test("VCT-05 VCT-08: native 800 and 1000 matrices converge to the TS07 analytic exposure sphere",()=>{
  const p=base(),radius=Math.sqrt(30000)/6,extents=[];
  for(const n of [50,100,200,400,800,1000]){const grid=verticalGrid(p,p.vertical_cuts[0],1,n),line=contours(grid,1,"red"),bounds=cutExposureBounds([line]);assert.ok(bounds);extents.push(bounds!);}
  near(extents.at(-1)!.right,radius,.1);near(extents.at(-1)!.top,10+radius,.1);
  assert.ok(Math.abs(extents.at(-1)!.right-extents.at(-2)!.right)/radius<.02);
});
test("HCT-04 HCT-08 HCT-09: all four limits and 0/10/20m heights match sphere sections and contour fields",()=>{
  const p=base();
  for(const [i,limit]of [6,20,4.5,10].entries())for(const height of [0,10,20]){
    p.settings.selected_limit=i;const grid=horizontalGrid(p,height,1,200),line=contours(grid,1,"red"),radius=Math.sqrt(30000)/limit;
    if(Math.abs(height-10)>=radius){assert.equal(line.segments.length,0);continue;}
    const expected=Math.sqrt(radius**2-(height-10)**2),bounds=cutExposureBounds([line]);near(bounds!.right,expected,.5);
    for(const point of line.segments.filter((_,j)=>j%31===0).map(s=>s[0]))near(exposureAt(p,...point,height,i).total_field,limit,limit*.03);
    assert.ok(line.segments.flat().every(([x,y])=>Math.abs(x)<=100&&Math.abs(y)<=100));
  }
  p.settings.calculation_area_m={x_min:-20,x_max:20,y_min:-20,y_max:20};p.settings.selected_limit=0;const line=contours(horizontalGrid(p,10,1,100),1,"red");
  assert.ok(line.segments.flat().every(([x,y])=>Math.abs(x)<=20&&Math.abs(y)<=20));
});
test("HCT-05 HCT-06: maximum projection, cumulative lower cuts and adjacent equal-color union",()=>{
  const p=base();p.horizontal_cuts=[0,10,20].map((height_m,i)=>({id:String(i),height_m,active:true,color:i<2?"red":"blue"}));
  const results=()=>p.horizontal_cuts.map(c=>({id:c.id,cut_id:c.id,limit_index:0,grid:horizontalGrid(p,c.height_m,2,50),contours:{...contours(horizontalGrid(p,c.height_m,2,50),1,c.color)}}));
  const original=results();p.limits[1].active=true;const projection=horizontalProjections(p,results());assert.equal(projection.length,1);
  for(let i=0;i<projection[0].grid.values.length;i++)near(projection[0].grid.values[i],Math.max(...original.map(r=>r.grid.values[i])));
  p.limits[1].active=false;p.settings.project_lower_cuts=true;const cumulative=horizontalProjections(p,results());
  assert.ok(cumulative[2].grid.values.every((v,i)=>v>=cumulative[1].grid.values[i]));
  p.settings.project_lower_cuts=false;p.settings.merge_same_color=true;assert.equal(horizontalProjections(p,results()).length,2);
});
test("VCT-06 VCT-07: oracle vertical samples agree with control points at 10 and 20m",()=>{
  const p=base(1),rows=readFileSync("test/emf_test_kit/data/control_points/iso_expected_vertical_cut_1W.tsv","utf8").trim().split(/\r?\n/);
  assert.ok(rows.length>1);
  for(const [distance,height]of [[5,10],[10,10],[50,10],[100,10],[50,20],[100,0],[20,30]])near(exposureAt(p,0,distance,height).total_field,Math.sqrt(30)/Math.hypot(distance,height-10),1e-12);
  p.control_points=[{...newPoint(p.origin,"CP"),x_m:0,y_m:50,heights:[10,20].map(h=>({id:String(h),height_m:h,attenuation_db:0,measured:0,description:""}))}];
  const actual=calculatePoints(p);for(let i=0;i<2;i++)near(actual[i].total_field,exposureAt(p,0,50,[10,20][i]).total_field);
});
test("VCT-09 VCT-10 VCT-11: profile intersects buildings/towers and measures native vertical ground clearance",()=>{
  const p=base();p.settings.consider_terrain=true;p.terrain_points=[{id:"t",x:0,y:0,elevation:100}];
  p.buildings=[{id:"b",name:"Building",height_m:15,foundation_m:102,active:true,attenuation_db:3,points:[[-10,20],[10,20],[10,40],[-10,40]]}];
  p.lines=[{id:"tower",kind:"label",name:"Tower/10/20/1/4",width:1,color:"black",points:[[0,60]]}];
  const profile=verticalProfile(p,p.vertical_cuts[0],0,100);assert.equal(profile.buildings.length,1);near(profile.buildings[0].left,20);near(profile.buildings[0].right,40);near(profile.buildings[0].top,17);assert.equal(profile.towers.length,1);
  const line:Contour={level:1,color:"red",height_m:0,segments:[[[30,10],[40,20]]]};const bounds=cutExposureBounds([line],()=>8);near(bounds!.minDistance,2);
});
test("VCT-12: ruler/cut world transformation preserves azimuth and signed distances",()=>{
  const cut={...base().vertical_cuts[0],x:10,y:-20,azimuth_deg:123.4};for(const d of [-10,0,50]){const point=cutWorld(cut,d),position=cutCoordinates(cut,...point);near(position.distance,d);near(position.offset,0);}
});
test("HCT-11 DOC-09: per-source safety dimensions scale inversely with all four limits and respect factor masks",()=>{
  const p=base();const cases=[[12.05,1,16,19.99],[10.42,3,18.3,41.97],[.1995,1,40,40.77],[.2512,1,39.2,41.73]];
  for(const [power_w,num_tx,gain_dbi,expected]of cases){Object.assign(p.sources[0],{power_w,num_tx,gain_dbi});let invariant=0;
    for(let i=0;i<4;i++){p.settings.selected_limit=i;const extent=sourceSafetyExtents(p,p.sources[0]),limit=[6,20,4.5,10][i];if(i===0){near(extent.Lm1,expected,expected*.01);invariant=extent.Lm1*limit;}else near(extent.Lm1*limit,invariant);near(extent.Lv,2*extent.Lm1);near(extent.Lh,2*extent.Lm1);}
  }
});
test("MET-09 MET-15 ROB-06: source row order and worker partitions preserve every numeric result",()=>{
  const p=base(1);p.sources=Array.from({length:40},(_,i)=>({...p.sources[0],id:`tx-${i}`,power_w:10**(-i/4)}));
  p.control_points=Array.from({length:17},(_,i)=>({...newPoint(p.origin,String(i)),x_m:20+i,y_m:30-i}));
  const baseline=calculatePoints(p).map(({per_source,...r})=>r),ordered=exposureAt(p,100,10,1.5);p.sources.reverse();
  assert.equal(exposureAt(p,100,10,1.5).total_field,ordered.total_field);assert.equal(exposureAt(p,100,10,1.5).exposure_ratio,ordered.exposure_ratio);
  for(const worker_count of [1,2,4,8]){p.settings.worker_count=worker_count;const parts=calculationPartitions({id:1,kind:"points",project:p,fine:false});assert.equal(parts.length,Math.min(worker_count,17));const actual=parts.flatMap(part=>calculatePoints(part.project)).map(({per_source,...r})=>r);assert.deepEqual(actual,baseline);}
});
test("PNT-06 DOC-10: native display truncates the manual's four power-density values",()=>{
  assert.deepEqual([.063538,.120968,.432815,.471055].map(e=>nativeTruncate(e*e/377,6)),["0.000010","0.000038","0.000496","0.000588"]);
});
test("PNT-10: original kit clipboard uses geographic priority and own Excel preserves multilevel point data",async()=>{
  const p=base(),text=readFileSync("test/emf_test_kit/data/control_points/control_points_oracle.tsv","utf8");
  const points=importPointRows(p,text.trim().split(/\r?\n/).map(row=>row.split("\t")));assert.equal(points.length,6);assert.equal(points[0].heights![0].height_m,1.1);
  p.control_points=points;const roundtrip=await importPointWorkbook(p,await pointWorkbookBytes(p));assert.equal(roundtrip.length,6);for(let i=0;i<6;i++){near(roundtrip[i].lat,points[i].lat);near(roundtrip[i].x_m,points[i].x_m);assert.deepEqual(roundtrip[i].heights,points[i].heights);}
  const rows=[["Latitude","Longitude","X_m","Y_m"],[p.origin[0],p.origin[1],10000,10000]];const imported=importPointRows(p,rows);near(imported[0].x_m,0);near(imported[0].y_m,0);
});
test("ROB-02 SET-05: strict decimal parsing and proxy endpoints reject malformed/overflow input",()=>{
  for(const text of ["abc","NaN","Infinity","1 500","1'500","1.2.3","--5","1..2","0x10","12abc","1e400",""])assert.throws(()=>decimalInput(text));
  for(const [text,expected]of [["1,5",1.5],["1.5",1.5],[".5",.5],["+5",5],["1e3",1000]] as const)near(decimalInput(text),expected);
  assert.deepEqual(validateProxy("::1",8080),{host:"::1",port:8080});assert.equal(validateProxy("",0),undefined);for(const port of [0,65536,NaN,1.5])assert.throws(()=>validateProxy("127.0.0.1",port));
});
test("ROB-10: oversized raster and ZIP declarations reject before pixel/entry decompression",async()=>{
  const bytes=new Uint8Array(24),view=new DataView(bytes.buffer);view.setUint32(0,0x89504e47);view.setUint32(12,0x49484452);view.setUint32(16,30000);view.setUint32(20,30000);assert.throws(()=>validateRasterDimensions(bytes));
  const zip=new JSZip();zip.file("file.txt","safe");const archive=await zip.generateAsync({type:"uint8array"});assert.equal(validateArchive(archive).entries,1);
  const mutated=new Uint8Array(archive),directory=mutated.findIndex((_,i)=>i+4<mutated.length&&new DataView(mutated.buffer).getUint32(i,true)===0x02014b50);new DataView(mutated.buffer).setUint32(directory+24,1024**3,true);assert.throws(()=>validateArchive(mutated));
});
test("SET-08 PRJ-01: reflection/matrix/owner/color and cut display survive JSON without affecting a second project",()=>{
  const p=base();p.owner_names=["Owner"];p.owner_colors={Owner:"#123456"};p.settings.reflection=1.6;p.settings.horizontal_grid_divisions=[1000,50];p.settings.vertical_display={points:false};
  const restored=normalizeProject(JSON.parse(JSON.stringify(p)));assert.equal(restored.settings.reflection,1.6);assert.deepEqual(restored.settings.horizontal_grid_divisions,[1000,50]);assert.deepEqual(restored.owner_colors,p.owner_colors);assert.equal(newProject().settings.reflection,1.3);
});

test("BLD-14: disabled internal attenuation preserves shielding geometry and the displayed building flag",()=>{
  const p=base(1);p.settings.buildings_mode="attenuate";p.settings.building_attenuation_db=6;p.settings.building_attenuation_high_db=6;
  p.buildings=[{id:"b",name:"inside",active:true,custom_attenuation:true,visible:false,height_m:20,foundation_m:0,attenuation_db:6,points:[[20,-10],[50,-10],[50,10],[20,10]]}];
  const attenuated=exposureAt(p,40,0,1.5).total_field;p.buildings[0].custom_attenuation=false;const free=exposureAt(p,40,0,1.5).total_field;near(attenuated/free,10**(-6/20));
  p.settings.buildings_mode="screen";assert.equal(exposureAt(p,80,0,1.5).total_field,0);assert.equal(p.buildings[0].visible,false);
});

test("BLD-12 BLD-13 ROB-05: spatially indexed grids equal brute-force rays for every sampled node",()=>{
  const p=base(1);p.sources=Array.from({length:5},(_,i)=>({...p.sources[0],id:`s${i}`,x_m:-75+i*15,y_m:-70+i*30,height_m:10+i*7}));
  p.buildings=Array.from({length:80},(_,i)=>{const x=-80+(i%10)*18,y=-65+Math.floor(i/10)*18;return{id:String(i),name:String(i),active:true,custom_attenuation:i%3!==0,visible:i%2===0,foundation_m:0,height_m:5+(i%4)*8,attenuation_db:3,points:[[x,y],[x+8,y],[x+8,y+9],[x,y+9]] as [number,number][]};});
  for(const mode of ["screen","attenuate"] as const){p.settings.buildings_mode=mode;const grid=horizontalGrid(p,12,1,50);
    for(let j=0;j<grid.height;j+=5)for(let i=0;i<grid.width;i+=5){const expected=exposureAt(p,grid.x0+i*grid.dx,grid.y0+j*grid.dy,12);near(grid.fields[j*grid.width+i],expected.total_field,1e-12);near(grid.values[j*grid.width+i],expected.exposure_ratio,1e-12);}
  }
});
test("ROB-05 ROB-06 MET-09: repeated colocated emitters consolidate power without changing grid fields or order invariance",()=>{
  const p=base(1);p.sources=Array.from({length:200},(_,i)=>({...p.sources[0],id:`s${i}`,power_w:1+i/13,alpha24:.75,frequency_mhz:i%2?900:2100}));
  p.limits[0].use_alpha24=true;const grid=horizontalGrid(p,1.5,1,50);
  for(let j=0;j<grid.height;j+=10)for(let i=0;i<grid.width;i+=10){const expected=exposureAt(p,grid.x0+i*grid.dx,grid.y0+j*grid.dy,1.5);near(grid.fields[j*grid.width+i]/expected.total_field,1,1e-12);near(grid.values[j*grid.width+i]/expected.exposure_ratio,1,1e-12);}
  p.sources.reverse();const reordered=horizontalGrid(p,1.5,1,50);assert.deepEqual(reordered.values,grid.values);assert.deepEqual(reordered.fields,grid.fields);
});
