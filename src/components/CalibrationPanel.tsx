import {useEffect,useRef,useState,type PointerEvent} from "react";
import {usePlanner} from "../lib/usePlanner";
import {chooseFile} from "../lib/files";
import {toGeo} from "../lib/geometry";
import {calibratedBackground,calibrationTransform,decodeMapImage,defaultCalibration,type CalibrationReference,type CalibratedImage} from "../lib/map-image-calibration";
import type {XY} from "../lib/geometry-editing";
import {CALIBRATION_COMMANDS} from "../lib/useGeometryActions";

export default function CalibrationPanel(){
 const a=usePlanner(),p=a.project,image=p.background_image as CalibratedImage|undefined;
 const [reference,setReference]=useState<CalibrationReference|undefined>(image?.calibration),[active,setActive]=useState(1),[pickMap,setPickMap]=useState<number|null>(null),[error,setError]=useState(""),[busy,setBusy]=useState(false);
 const drag=useRef<number|null>(null),svg=useRef<SVGSVGElement>(null);
 useEffect(()=>{if(image?.calibration)setReference(image.calibration);},[image?.calibration]);
 useEffect(()=>{
  if(!reference||!CALIBRATION_COMMANDS.includes(a.command)||a.command==="Actualiser la pyramide de tuiles")return;
  if(a.command==="Repères de calibration au centre"){
   const [x,y]=a.viewCenter;setReference(v=>v?{...v,imagePoints:[[v.width/2,v.height/2],[v.width/4,v.height/2],[v.width*3/4,v.height/2]],mapPoints:[[x,y],[x-30,y],[x+30,y]]}:v);
  }else setReference(v=>v?{...v,mode:a.command==="Calibrer avec deux lignes"?"two-lines":"line-point"}:v);
  a.setCommand("");
 },[a.command,!!reference]);
 useEffect(()=>{
  if(pickMap===null||!a.draft.length)return;
  const pos=a.draft[a.draft.length-1];
  setReference(v=>v?{...v,mapPoints:v.mapPoints.map((xy,i)=>i===pickMap?pos:xy) as [XY,XY,XY]}:v);
  setPickMap(null);a.setDraft([]);a.setTool("pan");
 },[a.draft,pickMap]);
 const point=(e:PointerEvent<SVGSVGElement>):XY=>{
  const matrix=svg.current?.getScreenCTM();if(!matrix||!reference)return [0,0];
  const p=new DOMPoint(e.clientX,e.clientY).matrixTransform(matrix.inverse());return [Math.max(0,Math.min(reference.width,p.x)),Math.max(0,Math.min(reference.height,p.y))];
 };
 const updateImagePoint=(i:number,xy:XY)=>setReference(v=>v?{...v,imagePoints:v.imagePoints.map((p,j)=>j===i?xy:p) as [XY,XY,XY]}:v);
 const numeric=(label:string,value:number,update:(n:number)=>void)=><label>{label}<input aria-label={label} inputMode="decimal" value={Number.isNaN(value)?"":value} onChange={e=>update(e.target.value.trim()?Number(e.target.value.replace(",",".")):NaN)}/></label>;
 const load=async()=>{
  setError("");setBusy(true);
  try{const file=await chooseFile(["png","jpg","jpeg","bmp","gif","tif","tiff","webp"]);if(!file)return;
   const decoded=await decodeMapImage(file.bytes,file.name),ref=defaultCalibration(decoded.data,decoded.width,decoded.height);
   const sw=toGeo(-150,-150*decoded.height/decoded.width,p.origin),ne=toGeo(150,150*decoded.height/decoded.width,p.origin);
   const next:CalibratedImage={data_url:decoded.data,west:sw[1],south:sw[0],east:ne[1],north:ne[0],opacity:0.7,calibration:ref};
   a.commit(v=>({...v,background_image:next,settings:{...v.settings,map_provider:"Calibrated map image",show_basemap:true}}));setReference(ref);
  }catch(e){setError(e instanceof Error?e.message:String(e));}finally{setBusy(false);}
 };
 const apply=async()=>{if(!reference)return;setError("");setBusy(true);try{const next=await calibratedBackground(p,reference,image?.opacity??0.7);a.commit(v=>({...v,background_image:next,settings:{...v.settings,map_provider:"Calibrated map image"}}));setReference(next.calibration);a.setStatus("Image calibrée ; tuiles régénérées depuis les repères.");}catch(e){setError(e instanceof Error?e.message:String(e));}finally{setBusy(false);}};
 return <div className="calibration-panel">
  <button disabled={busy} onClick={()=>void load()}>Charger une image</button>
  {reference&&<>
   <p>{reference.width} × {reference.height} pixels</p>
   <label>Méthode de calibration<select aria-label="Méthode de calibration" value={reference.mode} onChange={e=>setReference(v=>v?{...v,mode:e.target.value as CalibrationReference["mode"]}:v)}><option value="line-point">Point de référence et longueur</option><option value="two-lines">Deux lignes de référence</option></select></label>
   <p>Choisissez un repère puis cliquez sur l’image ou faites glisser son cercle. Les coordonnées précises peuvent être saisies.</p>
   <svg ref={svg} role="img" aria-label="Image et repères de calibration" viewBox={`0 0 ${reference.width} ${reference.height}`} style={{width:"100%",maxHeight:320,border:"1px solid #999",touchAction:"none"}}
    onPointerDown={e=>{const xy=point(e);updateImagePoint(active,xy);drag.current=active;e.currentTarget.setPointerCapture(e.pointerId);}}
    onPointerMove={e=>{if(drag.current!==null)updateImagePoint(drag.current,point(e));}}
    onPointerUp={()=>drag.current=null} onPointerCancel={()=>drag.current=null}>
    <image href={reference.original_data_url} width={reference.width} height={reference.height}/>
    <line x1={reference.imagePoints[1][0]} y1={reference.imagePoints[1][1]} x2={reference.imagePoints[2][0]} y2={reference.imagePoints[2][1]} stroke="#ffdf00" strokeWidth={Math.max(2,reference.width/150)}/>
    {reference.imagePoints.map(([x,y],i)=><g key={i} onPointerDown={e=>{e.stopPropagation();setActive(i);drag.current=i;svg.current?.setPointerCapture(e.pointerId);}}><circle cx={x} cy={y} r={Math.max(3,reference.width/45)} fill="none" stroke={i===active?"#0078d7":"#edc800"} strokeWidth={Math.max(1,reference.width/200)}/><text x={x+reference.width/40} y={y} fontSize={reference.width/35} fill="#0051a0">{i===0?"P":i}</text></g>)}
   </svg>
   <fieldset><legend>Repères de l’image, pixels</legend>
    {reference.imagePoints.map(([x,y],i)=><div key={i}><label><input type="radio" name="image-reference" checked={active===i} onChange={()=>setActive(i)}/>{i===0?"Point P":`Extrémité ${i}`}</label>{numeric(`Image ${i} X, px`,x,n=>updateImagePoint(i,[n,y]))}{numeric(`Image ${i} Y, px`,y,n=>updateImagePoint(i,[x,n]))}</div>)}
   </fieldset>
   {reference.mode==="line-point"&&numeric("Longueur de référence, m",reference.length,n=>setReference(v=>v?{...v,length:n}:v))}
   <fieldset><legend>Repères sur la carte, coordonnées locales</legend>{(reference.mode==="line-point"?[0]:[1,2]).map(i=>{const [x,y]=reference.mapPoints[i];const set=(xy:XY)=>setReference(v=>v?{...v,mapPoints:v.mapPoints.map((p,j)=>j===i?xy:p) as [XY,XY,XY]}:v);return <div key={i}>{numeric(`Carte ${i} X, m`,x,n=>set([n,y]))}{numeric(`Carte ${i} Y, m`,y,n=>set([x,n]))}<button onClick={()=>{setPickMap(i);a.setTool("calibrate-line-point");a.setDraft([]);}}>Placer le repère carte {i}</button></div>;})}</fieldset>
   {pickMap!==null&&<p role="status">Cliquez sur le plan pour placer le repère {pickMap}.</p>}
   <p aria-label="Échelle de calibration">{(()=>{try{return `${calibrationTransform(reference).scale.toFixed(6)} m/pixel`;}catch{return "Repères à compléter";}})()}</p>
   <button disabled={busy} onClick={()=>void apply()}>Appliquer la calibration</button>
   {image&&numeric("Opacité",image.opacity,n=>{if(n>=0&&n<=1)a.commit(v=>({...v,background_image:v.background_image?{...v.background_image,opacity:n}:undefined}),"Opacité de l’image modifiée",false);})}
   <button onClick={()=>{a.commit(v=>({...v,background_image:undefined}));setReference(undefined);}}>Retirer l’image</button>
  </>}
  {error&&<p className="error" role="alert">{error}</p>}
 </div>;
}
