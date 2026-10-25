import {useMemo} from "react";
import {Marker} from "react-leaflet";
import {divIcon} from "leaflet";
import type {DrawingLine} from "../types";
export const annotationIcon=(icon?:string)=>({tower:"♜",tree:"♣",pole:"┃",sign:"⚑",antenna:"♜",house:"⌂"} as Record<string,string>)[icon??""]??"";
export default function MapTextLabel({line,position,onSelect}:{line:DrawingLine;position:[number,number];onSelect:()=>void}){
 const icon=useMemo(()=>{const div=document.createElement("div");div.style.whiteSpace="nowrap";div.style.color=line.color;div.style.fontFamily=line.font_name??"Arial";div.style.fontSize=`${line.font_size??10}px`;div.style.transform=`rotate(${line.angle_deg??0}deg)`;div.style.transformOrigin="0 0";div.textContent=[annotationIcon(line.icon),line.name].filter(Boolean).join(" ");return divIcon({className:"map-text-label",html:div,iconSize:[1,1],iconAnchor:[0,0]});},[line.name,line.color,line.font_name,line.font_size,line.angle_deg,line.icon]);
 return <Marker position={position} icon={icon} bubblingMouseEvents={false} eventHandlers={{click:onSelect}}/>;
}
