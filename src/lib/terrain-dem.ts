import type { PlannerProject, TerrainPoint } from "../types";
import type { LocalBounds } from "./geometry-editing";
import { toGeo } from "./geometry";
import { geographicTile } from "./map-providers";
import { cachedTile } from "./map-tile-cache";
import { loadImage } from "./map-image-calibration";
import { uid } from "./project";
export const terrariumHeight=(r:number,g:number,b:number)=>r*256+g+b/256-32768;
/** Public AWS Terrain Tiles / Mapzen Terrarium, sampled over the visible region. */
export async function onlineDem(p:PlannerProject,bounds:LocalBounds,divisions=16):Promise<TerrainPoint[]> {
  if(!Number.isInteger(divisions)||divisions<2||divisions>64)throw new Error("La grille MNT doit avoir entre 2 et 64 divisions.");
  const zoom=14,points:TerrainPoint[]=[],tiles=new Map<string,Promise<ImageData>>();
  const pixel=async(tx:number,ty:number)=>{
    const x=Math.floor(tx),y=Math.floor(ty),key=`${x}/${y}`;
    if(!tiles.has(key)){
      if(tiles.size>=64)throw new Error("Zone MNT trop grande : rapprochez la vue avant de charger.");
      tiles.set(key,(async()=>{const blob=await cachedTile(`https://s3.amazonaws.com/elevation-tiles-prod/terrarium/${zoom}/${x}/${y}.png`),url=URL.createObjectURL(blob);try{const image=await loadImage(url),canvas=document.createElement("canvas");canvas.width=canvas.height=256;const ctx=canvas.getContext("2d")!;ctx.drawImage(image,0,0,256,256);return ctx.getImageData(0,0,256,256);}finally{URL.revokeObjectURL(url);}})());
    }
    const image=await tiles.get(key)!,i=(Math.min(255,Math.floor((ty-y)*256))*256+Math.min(255,Math.floor((tx-x)*256)))*4;
    if(image.data[i+3]===0)throw new Error("Le MNT ne couvre pas entièrement cette région.");
    return terrariumHeight(image.data[i],image.data[i+1],image.data[i+2]);
  };
  for(let row=0;row<=divisions;row++)for(let col=0;col<=divisions;col++){
    const x=bounds.west+(bounds.east-bounds.west)*col/divisions,y=bounds.south+(bounds.north-bounds.south)*row/divisions;
    const [lat,lng]=toGeo(x,y,p.origin),[tx,ty]=geographicTile(lat,lng,zoom);
    points.push({id:uid(),x,y,elevation:await pixel(tx,ty)});
  }
  return points;
}
