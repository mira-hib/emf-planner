import test from "node:test";
import assert from "node:assert/strict";
import {newProject,newPoint,newSource} from "../src/lib/project.ts";
import {buildingFoundation,deleteOutsideView,importOsmBuildings,importOsmLines,importSrtm,moveGeometry,moveControlPoint,pointsIntersectBounds,squareBuilding,type OsmData,type XY} from "../src/lib/geometry-editing.ts";
import {terrainHeight,terrainMesh,terrainDiagnostics,toGeo,toLocal} from "../src/lib/geometry.ts";
import {calibrationTransform,defaultCalibration,transformImagePoint} from "../src/lib/map-image-calibration.ts";
import {geographicTile,TILES,tileUrl,validateMapTemplate} from "../src/lib/map-providers.ts";
import {mastDimensions,mastFoundation,mastEdges} from "../src/lib/scene-geometry.ts";
import {terrariumHeight} from "../src/lib/terrain-dem.ts";
import {rebaseProject} from "../src/lib/origin-grid.ts";
const near=(a:number,b:number,tol=1e-7)=>assert.ok(Number.isFinite(a)&&Math.abs(a-b)<tol,`${a} != ${b}`);
const base=()=>{const p=newProject();p.origin=[45.485337,9.198818];p.settings.consider_terrain=true;return p;};
const building=(id="b",points:XY[]=[[0,0],[100,0],[100,100],[0,100]])=>({id,name:id,height_m:10,foundation_m:0,attenuation_db:3,active:true,visible:true,points});

test("BLD-07: OSM height/levels/defaults and repeat download preserve edited values",()=>{
 const p=base(),geo=(x:number,y:number)=>{const [lat,lon]=toGeo(x,y,p.origin);return {lat,lon};};
 const data:OsmData={elements:[{type:"way",id:1,tags:{building:"yes","building:levels":"4"},geometry:[geo(0,0),geo(10,0),geo(10,10),geo(0,0)]},{type:"way",id:2,tags:{building:"yes"},geometry:[geo(20,0),geo(30,0),geo(30,10),geo(20,0)]},{type:"way",id:3,tags:{building:"yes",height:"12,5 m","building:levels":"9"},geometry:[geo(40,0),geo(50,0),geo(50,10),geo(40,0)]}]};
 const imported=importOsmBuildings(p,data);assert.deepEqual(imported.buildings.map(b=>b.height_m),[12,20,12.5]);assert.ok(imported.buildings.every(b=>b.foundation_m===0&&b.points.length===3));
 imported.buildings[0].height_m=17;
 assert.equal(importOsmBuildings(imported,data).buildings.length,3);assert.equal(importOsmBuildings(imported,data).buildings[0].height_m,17);
 assert.deepEqual(p.buildings,[]);
});

test("BLD-08: malformed OSM data and invalid requested heights leave original project unchanged",()=>{
 const p=base();p.buildings=[building()];const before=JSON.stringify(p);
 assert.throws(()=>importOsmBuildings(p,{} as OsmData));assert.throws(()=>importOsmBuildings(p,{elements:[]},{defaultHeight:NaN,floorHeight:3,foundation:0}));assert.equal(JSON.stringify(p),before);
});

test("BLD-09: native perpendicular corner projection preserves a non-square rectangle size",()=>{
 const points:XY[]=[[0,0],[100,2],[98,42],[-1,40]],squared=squareBuilding(points);
 assert.equal(squared.length,4);
 for(let i=0;i<4;i++){const a=squared[(i+3)%4],b=squared[i],c=squared[(i+1)%4];const angle=Math.acos(((a[0]-b[0])*(c[0]-b[0])+(a[1]-b[1])*(c[1]-b[1]))/Math.hypot(a[0]-b[0],a[1]-b[1])/Math.hypot(c[0]-b[0],c[1]-b[1]))*180/Math.PI;near(angle,90,1);}
 assert.ok(Math.hypot(squared[1][0]-squared[0][0],squared[1][1]-squared[0][1])>90);
 assert.ok(Math.hypot(squared[2][0]-squared[1][0],squared[2][1]-squared[1][1])<50);
 assert.deepEqual(points,[[0,0],[100,2],[98,42],[-1,40]]);assert.throws(()=>squareBuilding([[0,0],[1,0],[0,1]]));
});

test("BLD-10: all four terrain corners contribute to minimum maximum mean foundation",()=>{
 const p=base();p.terrain_points=[[0,0,100],[100,0,102],[100,100,104],[0,100,106]].map(([x,y,elevation],i)=>({id:String(i),x,y,elevation}));
 const b=building();near(buildingFoundation(p,b,"min"),100);near(buildingFoundation(p,b,"max"),106);near(buildingFoundation(p,b,"mean"),103);
});

test("BLD-17: offscreen deletion retains crossings and enclosing polygons; terrain variants differ",()=>{
 const p=base(),bounds={west:-10,south:-10,east:10,north:10};
 p.buildings=[building("outside",[[20,20],[30,20],[30,30],[20,30]]),building("crossing",[[-20,-2],[20,-2],[20,2],[-20,2]]),building("enclosing",[[-20,-20],[20,-20],[20,20],[-20,20]])];
 p.terrain_points=[{id:"in",x:0,y:0,elevation:100},{id:"out",x:100,y:0,elevation:200}];p.lines=[{id:"cross",kind:"road",name:"road",width:4,color:"black",points:[[-100,0],[100,0]]}];
 p.sources=[newSource(toGeo(100,100,p.origin),"out"),newSource(p.origin,"in")];p.control_points=[newPoint(p.origin,"in")];
 const without=deleteOutsideView(p,bounds,false),withTerrain=deleteOutsideView(p,bounds,true);
 assert.deepEqual(withTerrain.buildings.map(b=>b.id),["crossing","enclosing"]);assert.equal(withTerrain.lines.length,1);assert.equal(withTerrain.sources.length,1);assert.equal(withTerrain.control_points.length,1);
 assert.deepEqual(without.terrain_points,p.terrain_points);assert.deepEqual(withTerrain.terrain_points.map(t=>t.id),["in"]);assert.equal(p.buildings.length,3);
 assert.equal(pointsIntersectBounds([[0,0]],bounds),true);
});

test("BLD-09/TER-02/08/LBL-05: whole-object and one-node moves are isolated immutable operations",()=>{
 const p=base();p.buildings=[building()];p.lines=[{id:"line",kind:"road",name:"road",width:3,color:"black",points:[[0,0],[20,0],[20,20]]}];p.terrain_contours=[{id:"contour",name:"contour",elevation:12,points:[[0,0],[10,0]],closed:false}];p.terrain_points=[{id:"terrain",x:0,y:0,elevation:10,wm_x:4,wm_y:5}];
 const moved=moveGeometry(p,"b",2,-3,1);assert.deepEqual(moved.buildings[0].points,[[0,0],[102,-3],[100,100],[0,100]]);
 assert.deepEqual(moveGeometry(p,"line",5,10).lines[0].points,[[5,10],[25,10],[25,30]]);
 assert.deepEqual(moveGeometry(p,"contour",2,3,0).terrain_contours![0].points,[[2,3],[10,0]]);
 const terrain=moveGeometry(p,"terrain",5,-5).terrain_points[0];assert.equal(terrain.x,5);assert.equal(terrain.y,-5);assert.equal(terrain.wm_x,undefined);assert.equal(p.terrain_points[0].wm_x,4);
});

test("PNT-01: dragging a control point updates both local and geographic coordinates",()=>{
 const p=base();p.control_points=[newPoint(p.origin,"P1")];const geo=toGeo(100,-50,p.origin),moved=moveControlPoint(p,p.control_points[0].id,...geo);
 near(moved.control_points[0].x_m,100);near(moved.control_points[0].y_m,-50);assert.deepEqual([moved.control_points[0].lat,moved.control_points[0].lng],geo);assert.equal(p.control_points[0].x_m,0);
});

test("TER-01/02: triangular interpolation is planar and rebuilt after elevation/contour changes",()=>{
 const p=base();p.terrain_points=[{id:"a",x:0,y:0,elevation:100},{id:"b",x:100,y:0,elevation:110},{id:"c",x:0,y:100,elevation:100}];
 near(terrainHeight(p,50,0),105,0.05);near(terrainHeight(p,25,25),102.5,0.05);near(terrainHeight(p,0,50),100,0.05);
 const edited={...p,terrain_points:p.terrain_points.map(t=>({...t,elevation:t.elevation+100}))};near(terrainHeight(edited,25,25),202.5,0.05);
 const contours={...p,terrain_points:[],terrain_contours:[{id:"c",name:"c",elevation:35,closed:false,points:[[0,0],[100,0],[0,100]] as XY[]}]};near(terrainHeight(contours,25,25),35);assert.ok(terrainMesh(contours).triangles.length>=3);
});

test("TER-03: SRTM big-endian samples replace all existing relief and reject malformed tiles",()=>{
 const p=base();p.origin=[45.5,9.5];p.terrain_points=[{id:"old",x:0,y:0,elevation:999}];p.terrain_contours=[{id:"oldc",name:"old",points:[[0,0],[1,1]],closed:false,elevation:88}];
 const bytes=new Uint8Array(1201*1201*2),view=new DataView(bytes.buffer);for(let i=0;i<1201*1201;i++)view.setInt16(i*2,-123,false);view.setInt16((600*1201+600)*2,-32768,false);
 const imported=importSrtm(p,bytes,"N45E009.hgt",{west:-150,south:-150,east:150,north:150});
 assert.ok(imported.terrain_points.length>3);assert.ok(imported.terrain_points.every(t=>t.elevation===-123&&t.id!=="old"));assert.deepEqual(imported.terrain_contours,[]);assert.equal(p.terrain_points[0].elevation,999);
 assert.throws(()=>importSrtm(p,new Uint8Array(100),"N45E009.hgt",{west:-10,south:-10,east:10,north:10}));near(terrariumHeight(128,100,128),100.5);
});

test("TER-06/07: empty sparse collinear duplicate and extreme elevations never fabricate triangles",()=>{
 for(const coordinates of [[],[[0,0,100]],[[0,0,100],[1,0,110]],[[0,0,100],[1,0,110],[2,0,120]],[[0,0,100],[0,0,200],[1,0,110]]]){
  const p=base();p.terrain_points=coordinates.map(([x,y,elevation],i)=>({id:String(i),x,y,elevation}));assert.equal(terrainMesh(p).triangles.length,0);assert.ok(Number.isFinite(terrainHeight(p,5,5)));if(coordinates.length)assert.ok(terrainDiagnostics(p).length);
 }
 const p=base();near(terrainHeight(p,100,100),0);p.terrain_points=[{id:"low",x:0,y:0,elevation:-500},{id:"high",x:100,y:0,elevation:9000},{id:"mid",x:0,y:100,elevation:0}];near(terrainHeight(p,0,0),-500);near(terrainHeight(p,100,0),9000);
});

test("LBL-01: OSM roads/fences have real geometry widths and deduplicate repeat loads",()=>{
 const p=base(),geo=(x:number,y:number)=>{const [lat,lon]=toGeo(x,y,p.origin);return {lat,lon};};const data:OsmData={elements:[{type:"way",id:1,tags:{highway:"service",width:"6"},geometry:[geo(0,0),geo(100,0)]},{type:"way",id:2,tags:{barrier:"fence"},geometry:[geo(0,10),geo(100,10)]}]};
 const imported=importOsmLines(p,data);assert.deepEqual(imported.lines.map(l=>[l.kind,l.width]),[["road",6],["barrier",1]]);near(imported.lines[0].points[1][0],100);assert.equal(importOsmLines(imported,data).lines.length,2);
});

test("LBL-03/04: valid Tower annotations produce roof-based 3D edges; malformed variants remain text",()=>{
 const p=base();p.buildings=[{...building(),foundation_m:100,height_m:12}];
 const tower=mastDimensions("Tower/15/15/0.5/1.2")!;assert.equal(tower.upperHeight+tower.taperedHeight,30);assert.equal(mastFoundation(p,50,50),112);assert.ok(mastEdges(tower,50,50,112,0).some(edge=>edge.some(point=>point[1]===142)));
 assert.equal(mastDimensions("Tower/15/0/0.5/0.5")!.taperedHeight,0);assert.equal(mastDimensions("Tower/15/15/0,5/1,2")!.lowerWidth,1.2);
 for(const text of ["tower/15/15/0.5/1.2","Tower/15/15/0.5","Tower/-1/15/0.5/1","Tower/15/15/0/1","Tower/a/15/0.5/1"])assert.equal(mastDimensions(text),null);
});

test("CAL-03: point-plus-line gives 0.2 m per pixel and independent reference point translation",()=>{
 const r=defaultCalibration("data:image/png;base64,",800,800);r.imagePoints=[[100,100],[100,100],[600,100]];r.mapPoints=[[10,20],[0,0],[100,0]];r.length=100;
 const t=calibrationTransform(r);near(t.scale,0.2);assert.deepEqual(transformImagePoint(t,[100,100]),[10,20]);near(Math.hypot(...transformImagePoint(t,[400,100]).map((v,i)=>v-[10,20][i]) as [number,number]),60);near(transformImagePoint(t,[100,600])[1],-80);
});

test("CAL-04: two reference lines correct 30-degree rotation as well as scale",()=>{
 const r=defaultCalibration("data:image/png;base64,",1000,1000);r.mode="two-lines";r.imagePoints=[[100,100],[100,100],[100+500*Math.cos(Math.PI/6),350]];r.mapPoints=[[0,0],[0,0],[100,0]];
 const t=calibrationTransform(r),end=transformImagePoint(t,r.imagePoints[2]),check=transformImagePoint(t,[100-250,100+500*Math.cos(Math.PI/6)]);near(t.scale,0.2);near(end[0],100);near(end[1],0);near(check[0],0);near(check[1],-100);
});

test("CAL-06: zero negative nonnumeric absurd lengths and zero/one-pixel lines reject explicitly",()=>{
 const r=defaultCalibration("",800,800);for(const length of [0,-100,NaN,1e9])assert.throws(()=>calibrationTransform({...r,length}));
 for(const px of [0,1])assert.throws(()=>calibrationTransform({...r,imagePoints:[[0,0],[100,100],[100+px,100]]}));
});

test("CAL-08/09/DOC-11: kit manual-origin tile numbers inverse zoom and original catalogue",()=>{
 const tile18=geographicTile(45.485337,9.198818,18),tile19=geographicTile(45.485337,9.198818,19);assert.deepEqual(tile18.map(Math.floor),[137770,93797]);assert.deepEqual(tile19.map(Math.floor),[275540,187595]);
 assert.equal(tileUrl("http://127.0.0.1:8000/g?x=[X]&y=[Y]&zoom=[17-Z]",137770,93797,18),"http://127.0.0.1:8000/g?x=137770&y=93797&zoom=-1");assert.ok(TILES["Bing Sat"]);assert.equal(Object.keys(TILES).length,14);
 assert.throws(()=>validateMapTemplate("http://127.0.0.1:8000/t/[X]/[Y].png"));
});

test("ORI-04/07/08: local metre distances bearings and anchors survive origins at 0 45.5 and 60 degrees",()=>{
 for(const latitude of [0,45.5,60]){
  const p=base();p.origin=[latitude,9];for(const [x,y,bearing]of [[100,0,90],[0,100,0],[-70.71,-70.71,225]]){const geo=toGeo(x,y,p.origin),xy=toLocal(...geo,p.origin);near(Math.hypot(...xy),100,0.5);near((Math.atan2(xy[0],xy[1])*180/Math.PI+360)%360,bearing,0.5);}
  p.sources=[newSource(toGeo(100,50,p.origin),"TX")];p.control_points=[newPoint(toGeo(-50,30,p.origin),"CP")];const geo=[p.sources[0].lat,p.sources[0].lng];const rebased=rebaseProject(p,toGeo(100,0,p.origin));assert.deepEqual([rebased.sources[0].lat,rebased.sources[0].lng],geo);assert.deepEqual([rebased.control_points[0].lat,rebased.control_points[0].lng],[p.control_points[0].lat,p.control_points[0].lng]);
 }
});
