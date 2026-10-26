import type { BackgroundImage, PlannerProject } from "../types";
import { toGeo } from "./geometry";
import type { XY } from "./geometry-editing";
import { validateRasterDimensions } from "./raster-validation";

export interface CalibrationReference {
  original_data_url: string;
  width: number;
  height: number;
  imagePoints: [XY, XY, XY];
  mapPoints: [XY, XY, XY];
  length: number;
  mode: "line-point" | "two-lines";
  version: number;
  transform?: Similarity;
}
export interface CalibratedImage extends BackgroundImage { calibration?: CalibrationReference; }
export interface Similarity { a:number;b:number;tx:number;ty:number; scale:number; }
export function transformImagePoint(t: Similarity, [u,v]: XY): XY { return [t.a*u+t.b*v+t.tx,t.b*u-t.a*v+t.ty]; }
export function calibrationTransform(reference: CalibrationReference): Similarity {
  const [anchor,start,end]=reference.imagePoints, [mapAnchor,mapStart,mapEnd]=reference.mapPoints;
  if ([...reference.imagePoints.flat(),...reference.mapPoints.flat()].some((v)=>!Number.isFinite(v))) throw new Error("Les coordonnées des repères doivent être numériques.");
  const du=end[0]-start[0],dv=end[1]-start[1],pixels=Math.hypot(du,dv);
  if(pixels<2) throw new Error("La ligne de référence doit mesurer au moins 2 pixels.");
  let a:number,b:number,from:XY,to:XY;
  if(reference.mode==="line-point") {
    if(!Number.isFinite(reference.length)||reference.length<5||reference.length>1000) throw new Error("La longueur de référence doit être comprise entre 5 et 1000 m.");
    a=reference.length/pixels;b=0;from=anchor;to=mapAnchor;
  } else {
    const dx=mapEnd[0]-mapStart[0],dy=mapEnd[1]-mapStart[1],metres=Math.hypot(dx,dy);
    if(metres<0.01||metres>1000000) throw new Error("La ligne de référence sur la carte doit mesurer entre 0,01 m et 1000 km.");
    a=(dx*du-dy*dv)/(pixels*pixels);b=(dx*dv+dy*du)/(pixels*pixels);from=start;to=mapStart;
  }
  return {a,b,tx:to[0]-a*from[0]-b*from[1],ty:to[1]-b*from[0]+a*from[1],scale:Math.hypot(a,b)};
}
export function defaultCalibration(data:string,width:number,height:number):CalibrationReference {
  const x=width/2,y=height/2;
  return {original_data_url:data,width,height,imagePoints:[[x,y],[Math.max(0,x-width/4),y],[Math.min(width,x+width/4),y]],mapPoints:[[0,0],[-50,0],[50,0]],length:100,mode:"line-point",version:0};
}
export async function loadImage(data:string):Promise<HTMLImageElement> {
  return new Promise((resolve,reject)=>{const image=new Image();image.onload=()=> image.naturalWidth&&image.naturalHeight?resolve(image):reject(new Error("L’image est vide."));image.onerror=()=>reject(new Error("Image illisible ou corrompue. L’image précédente est conservée."));image.src=data;});
}
export async function decodeMapImage(bytes:Uint8Array,name:string):Promise<{data:string;width:number;height:number}> {
  if(!bytes.length||bytes.length>100*1024*1024) throw new Error("Image vide ou supérieure à 100 Mo.");
  validateRasterDimensions(bytes);
  let data:string;
  const buffer=bytes.buffer.slice(bytes.byteOffset,bytes.byteOffset+bytes.byteLength) as ArrayBuffer;
  if(/\.tiff?$/i.test(name)||bytes[0]===73&&bytes[1]===73&&bytes[2]===42||bytes[0]===77&&bytes[1]===77&&bytes[3]===42) {
    const {default:UTIF}=await import("utif");
    const ifds=UTIF.decode(buffer),first=ifds[0];
    if(!first) throw new Error("Le TIFF ne contient aucune image.");
    const width=first.t256?.[0]??0,height=first.t257?.[0]??0;
    if(width*height>100000000||width<1||height<1) throw new Error("Dimensions TIFF invalides ou supérieures à 100 millions de pixels.");
    UTIF.decodeImage(buffer,first);
    const rgba=UTIF.toRGBA8(first),canvas=document.createElement("canvas");canvas.width=width;canvas.height=height;
    canvas.getContext("2d")!.putImageData(new ImageData(new Uint8ClampedArray(rgba),width,height),0,0);
    data=canvas.toDataURL("image/png");
  } else {
    const mime=/\.jpe?g$/i.test(name)?"image/jpeg":/\.gif$/i.test(name)?"image/gif":/\.bmp$/i.test(name)?"image/bmp":/\.webp$/i.test(name)?"image/webp":"image/png";
    data=await new Promise<string>((resolve,reject)=>{const reader=new FileReader();reader.onload=()=>resolve(String(reader.result));reader.onerror=()=>reject(new Error("Lecture de l’image impossible."));reader.readAsDataURL(new Blob([buffer],{type:mime}));});
  }
  const image=await loadImage(data);
  if(image.naturalWidth*image.naturalHeight>100000000) throw new Error("Image supérieure à 100 millions de pixels.");
  return {data,width:image.naturalWidth,height:image.naturalHeight};
}
/** Rasterize the calibrated image north-up so the same geographic bounds work in SVG, Leaflet and 3D. */
export async function calibratedBackground(p:PlannerProject,reference:CalibrationReference,opacity=0.7):Promise<CalibratedImage> {
  const t=calibrationTransform(reference),image=await loadImage(reference.original_data_url);
  const corners:XY[]=[[0,0],[reference.width,0],[reference.width,reference.height],[0,reference.height]].map((xy)=>transformImagePoint(t,xy as XY));
  const west=Math.min(...corners.map(p=>p[0])),east=Math.max(...corners.map(p=>p[0])),south=Math.min(...corners.map(p=>p[1])),north=Math.max(...corners.map(p=>p[1]));
  const pixelsPerMetre=Math.min(1/t.scale,8192/Math.max(east-west,north-south));
  const canvas=document.createElement("canvas");canvas.width=Math.max(1,Math.ceil((east-west)*pixelsPerMetre));canvas.height=Math.max(1,Math.ceil((north-south)*pixelsPerMetre));
  const ctx=canvas.getContext("2d")!;
  ctx.setTransform(t.a*pixelsPerMetre,-t.b*pixelsPerMetre,t.b*pixelsPerMetre,t.a*pixelsPerMetre,(t.tx-west)*pixelsPerMetre,(north-t.ty)*pixelsPerMetre);ctx.drawImage(image,0,0);
  const sw=toGeo(west,south,p.origin),ne=toGeo(east,north,p.origin);
  return {data_url:canvas.toDataURL("image/png"),west:sw[1],south:sw[0],east:ne[1],north:ne[0],opacity,calibration:{...reference,transform:t,version:reference.version+1}};
}
