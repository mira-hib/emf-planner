import { usePlanner } from "./usePlanner";
import { moveGeometry, squareBuilding, type LocalBounds } from "./geometry-editing";
import { uid } from "./project";
import { toGeo } from "./geometry";
import type { CalibratedImage } from "./map-image-calibration";
import { calibratedBackground } from "./map-image-calibration";

export const GEOMETRY_COMMANDS=["Altitude des bases depuis le terrain","Altitude de la base sélectionnée","Bâtiments OpenStreetMap","Routes et clôtures OpenStreetMap","Relief DEM / SRTM","Supprimer les objets hors écran","Supprimer les objets hors écran sans terrain","Cache des cartes"];
export const CALIBRATION_COMMANDS=["Calibrer avec deux lignes","Calibrer avec une ligne et un point","Repères de calibration au centre","Actualiser la pyramide de tuiles"];
export function currentViewBounds(a:ReturnType<typeof usePlanner>):LocalBounds {
  // The provider supplies exact viewport bounds after the first rendered map frame.
  return (a as unknown as {viewBounds?:LocalBounds}).viewBounds??{west:a.viewCenter[0]-a.project.settings.radius_m,south:a.viewCenter[1]-a.project.settings.radius_m,east:a.viewCenter[0]+a.project.settings.radius_m,north:a.viewCenter[1]+a.project.settings.radius_m};
}
export function useGeometryActions():Record<string,{run:()=>void;enabled:boolean}>{
  const a=usePlanner(),p=a.project;
  const selected=p.buildings.find(b=>b.id===a.selectedObject)??p.lines.find(l=>l.id===a.selectedObject)??p.terrain_contours?.find(c=>c.id===a.selectedObject);
  const image=p.background_image as CalibratedImage|undefined;
  const actions:Record<string,{run:()=>void;enabled:boolean}>=Object.fromEntries(GEOMETRY_COMMANDS.map(label=>[label,{run:()=>a.openCommand(label),enabled:!a.busy}]));
  actions["Altitude des bases depuis le terrain"].enabled=!!p.buildings.length;
  actions["Altitude de la base sélectionnée"].enabled=p.buildings.some(b=>b.id===a.selectedObject);
  actions["Équarrir les angles"]={enabled:p.buildings.some(b=>b.id===a.selectedObject),run:()=>void a.task(async()=>{a.commit(v=>({...v,buildings:v.buildings.map(b=>b.id===a.selectedObject?{...b,points:squareBuilding(b.points)}:b)}));return true;},"Angles du bâtiment équarris")};
  for(const label of CALIBRATION_COMMANDS)actions[label]={enabled:!!image,run:()=>{
    if(label==="Actualiser la pyramide de tuiles"){
      if(!image?.calibration)return a.setStatus("Calibrez l’image avec ses repères avant de régénérer les tuiles.");
      void a.task(async()=>{const next=await calibratedBackground(p,image.calibration!,image.opacity);a.commit(v=>({...v,background_image:next}));return true;},"Image calibrée et pyramide régénérées");
    }else{a.setSection("calibrate");a.openCommand(label);}
  }};
  const translate=(dx:number,dy:number)=>a.commit(v=>moveGeometry(v,a.selectedObject,dx,dy));
  const metresPerPixel=(!p.settings.show_basemap?currentViewBounds(a).metresPerPixel:undefined)??Math.cos(p.map_center[0]*Math.PI/180)*2*Math.PI*6378137/(256*2**p.map_zoom);
  for(const [label,dx,dy]of [["Déplacer l’objet d’un pixel à gauche",-1,0],["Déplacer l’objet d’un pixel à droite",1,0],["Déplacer l’objet d’un pixel vers le haut",0,1],["Déplacer l’objet d’un pixel vers le bas",0,-1]] as [string,number,number][])actions[label]={enabled:!!selected||p.terrain_points.some(t=>t.id===a.selectedObject),run:()=>translate(dx*metresPerPixel,dy*metresPerPixel)};
  actions["Supprimer tous les bâtiments"]={enabled:!!p.buildings.length,run:()=>{if(window.confirm("Supprimer tous les bâtiments ? Cette opération peut être annulée."))a.commit(v=>({...v,buildings:[]}));}};
  actions["Supprimer tout le terrain"]={enabled:!!p.terrain_points.length||!!p.terrain_contours?.length,run:()=>{if(window.confirm("Supprimer tous les points et courbes de niveau ? Cette opération peut être annulée."))a.commit(v=>({...v,terrain_points:[],terrain_contours:[]}));}};
  actions["Dupliquer le point de terrain"]={enabled:p.terrain_points.some(t=>t.id===a.selectedObject),run:()=>{const t=p.terrain_points.find(t=>t.id===a.selectedObject)!;const copy={...t,id:uid(),x:t.x+5,y:t.y+5,wm_x:undefined,wm_y:undefined};a.commit(v=>({...v,terrain_points:[...v.terrain_points,copy]}));a.setSelectedObject(copy.id);}};
  actions["Origine au point du clic"]={enabled:true,run:()=>a.changeOrigin(toGeo(...a.cursor,p.origin))};
  return actions;
}
