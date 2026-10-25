import {useEffect,useState} from "react";
import {usePlanner} from "../lib/usePlanner";
import {GEOMETRY_COMMANDS,currentViewBounds} from "../lib/useGeometryActions";
import {buildingFoundation,deleteOutsideView,importOsmBuildings,importOsmLines,importSrtm,OSM_BUILDING_DEFAULTS,type BaseMethod,type OsmData} from "../lib/geometry-editing";
import {toGeo} from "../lib/geometry";
import {chooseFile} from "../lib/files";
import {onlineDem} from "../lib/terrain-dem";
import {mapCacheStats,clearMapCache} from "../lib/map-tile-cache";
import Modal from "./Modal";
import {networkFetch} from "../lib/network";

export default function GeometryDialogs(){
 const a=usePlanner(),p=a.project,command=a.command,close=()=>a.setCommand("");
 const [method,setMethod]=useState<BaseMethod>("min"),[error,setError]=useState(""),[busy,setBusy]=useState(false),[defaults,setDefaults]=useState(OSM_BUILDING_DEFAULTS),[largeAccepted,setLargeAccepted]=useState(false),[demMode,setDemMode]=useState("online"),[divisions,setDivisions]=useState(16),[cache,setCache]=useState({count:0,bytes:0,available:true});
 useEffect(()=>{setError("");setBusy(false);setLargeAccepted(false);if(command==="Cache des cartes")void mapCacheStats().then(setCache).catch(e=>setError(String(e)));},[command]);
 if(!GEOMETRY_COMMANDS.includes(command))return null;
 const bounds=currentViewBounds(a),area=(bounds.east-bounds.west)*(bounds.north-bounds.south),large=area>4000000||p.map_zoom<=12;
 const run=async()=>{
  setError("");setBusy(true);
  try{
   if(command.startsWith("Altitude")){
    a.commit(v=>({...v,buildings:v.buildings.map(b=>command==="Altitude des bases depuis le terrain"||b.id===a.selectedObject?{...b,foundation_m:buildingFoundation(v,b,method)}:b)}));
   }else if(command.startsWith("Supprimer les objets hors écran")){
    a.commit(v=>deleteOutsideView(v,bounds,!command.includes("sans terrain")));
   }else if(command.includes("OpenStreetMap")){
    if(large&&!largeAccepted)throw new Error("La zone visible est grande. Confirmez le chargement ou rapprochez la vue.");
    const sw=toGeo(bounds.west,bounds.south,p.origin),ne=toGeo(bounds.east,bounds.north,p.origin),box=`${sw[0]},${sw[1]},${ne[0]},${ne[1]}`;
    const building=command==="Bâtiments OpenStreetMap",selection=building?`way["building"](${box});`:`(way["highway"](${box});way["barrier"="fence"](${box}););`;
    const response=await networkFetch("https://overpass-api.de/api/interpreter",{method:"POST",body:`data=${encodeURIComponent(`[out:json][timeout:25];${selection}out body;>;out skel qt;`)}`,headers:{"Content-Type":"application/x-www-form-urlencoded"},signal:AbortSignal.timeout(30000)});
    if(!response.ok)throw new Error(`Le serveur OSM a répondu HTTP ${response.status}. Les objets existants sont conservés.`);
    const json=await response.json() as OsmData;
    let count=0;
    a.commit(v=>{const next=building?importOsmBuildings(v,json,defaults):importOsmLines(v,json);count=building?next.buildings.length-v.buildings.length:next.lines.length-v.lines.length;return next;});
    a.setStatus(`${count} ${building?"bâtiments":"routes et clôtures"} OSM ajoutés ; doublons conservés sans modification.`);
   }else if(command==="Relief DEM / SRTM"){
    if(demMode==="file"){
     const file=await chooseFile(["hgt"]);if(!file){setBusy(false);return;}
     const next=importSrtm(p,file.bytes,file.name,bounds);a.commit(v=>({...v,terrain_points:next.terrain_points,terrain_contours:[]}));
    }else{
     const points=await onlineDem(p,bounds,divisions);a.commit(v=>({...v,terrain_points:points,terrain_contours:[]}));
    }
    a.setDisplay(d=>({...d,terrain:true}));a.settings({consider_terrain:true});
   }
   close();
  }catch(e){setError(e instanceof Error?e.message:String(e));}finally{setBusy(false);}
 };
 const numeric=(label:string,value:number,onChange:(n:number)=>void)=><label>{label}<input aria-label={label} value={Number.isNaN(value)?"":value} inputMode="decimal" onChange={e=>onChange(e.target.value.trim()?Number(e.target.value.replace(",",".")):NaN)}/></label>;
 return <Modal title={command} onClose={()=>!busy&&close()}>
  {command.startsWith("Altitude")&&<><p>Calculer l’altitude de base à partir de tous les sommets du bâtiment.</p><label>Méthode<select aria-label="Méthode d’altitude de base" value={method} onChange={e=>setMethod(e.target.value as BaseMethod)}><option value="min">Minimum</option><option value="max">Maximum</option><option value="mean">Moyenne</option></select></label></>}
  {command.startsWith("Supprimer les objets hors écran")&&<p>Supprimer les antennes, points, bâtiments et lignes entièrement hors de la vue actuelle{command.includes("sans terrain")?", en conservant le terrain":" ainsi que les points et courbes de terrain hors écran"}. Les objets qui croisent le bord restent conservés. Cette opération peut être annulée.</p>}
  {command.includes("OpenStreetMap")&&<><p>Charger les objets de la zone visible. Les doublons et les hauteurs déjà éditées restent conservés.</p>{command==="Bâtiments OpenStreetMap"&&<>{numeric("Hauteur par défaut, m",defaults.defaultHeight,n=>setDefaults(v=>({...v,defaultHeight:n})))}{numeric("Hauteur par étage, m",defaults.floorHeight,n=>setDefaults(v=>({...v,floorHeight:n})))}{numeric("Altitude de base OSM, m",defaults.foundation,n=>setDefaults(v=>({...v,foundation:n})))}</>}{large&&<div role="status"><p>Zone large : ne chargez pas plus de données que nécessaire. Rapprochez la vue pour limiter le volume.</p><label><input type="checkbox" checked={largeAccepted} onChange={e=>setLargeAccepted(e.target.checked)}/>Confirmer le chargement de cette grande zone</label></div>}</>}
  {command==="Relief DEM / SRTM"&&<><p>Le chargement remplace tous les points et courbes de niveau existants. Il peut être annulé. Seule la zone visible est importée.</p><label>Source du MNT<select aria-label="Source du MNT" value={demMode} onChange={e=>setDemMode(e.target.value)}><option value="online">MNT en ligne (AWS Terrain Tiles)</option><option value="file">Fichier SRTM .hgt</option></select></label>{demMode==="online"&&numeric("Divisions de la grille MNT",divisions,setDivisions)}</>}
  {command==="Cache des cartes"&&<><p>Cache local persistant de l’application : {cache.count} tuiles, {(cache.bytes/1024/1024).toFixed(2)} Mo. Les tuiles déjà téléchargées sont réutilisées hors ligne. Limite : 1500 tuiles.</p>{!cache.available&&<p role="alert">Le stockage persistant est indisponible dans ce navigateur.</p>}<button onClick={()=>void clearMapCache().then(mapCacheStats).then(setCache).catch(e=>setError(String(e)))}>Vider le cache des cartes</button><button onClick={()=>void mapCacheStats().then(setCache)}>Actualiser le compte du cache</button></>}
  {command==="Cache des cartes"&&<button onClick={()=>a.openCommand("Dossier du cache")}>Dossier du cache…</button>}
  {error&&<p className="error" role="alert">{error}</p>}
  <div className="dialog-actions"><button disabled={busy} onClick={close}>{command==="Cache des cartes"?"Fermer":"Annuler"}</button>{command!=="Cache des cartes"&&<button disabled={busy} onClick={()=>void run()}>{busy?"Chargement…":command.startsWith("Supprimer")?"Supprimer les objets hors écran":"Appliquer"}</button>}</div>
 </Modal>;
}
