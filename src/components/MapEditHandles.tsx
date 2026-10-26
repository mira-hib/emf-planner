import {useMemo} from "react";
import {Marker,Tooltip} from "react-leaflet";
import {divIcon} from "leaflet";
export function LeafletEditHandle({position,label,onMove,onSelect,round=false}:{position:[number,number];label:string;onMove:(lat:number,lng:number)=>void;onSelect?:()=>void;round?:boolean}){
 const icon=useMemo(()=>divIcon({className:"geometry-leaflet-handle",iconSize:[14,14],iconAnchor:[7,7],html:`<span style="display:block;width:10px;height:10px;border:2px solid #0078d7;background:white;border-radius:${round?"50%":"0"}"></span>`}),[round]);
 return <Marker title={label} position={position} draggable icon={icon} bubblingMouseEvents={false} zIndexOffset={1500} eventHandlers={{click:onSelect,dragstart:onSelect,dragend:e=>{const p=e.target.getLatLng();onMove(p.lat,p.lng);}}}><Tooltip>{label}</Tooltip></Marker>;
}
