import { useEffect, useRef, useState } from "react";
import {
  CircleMarker,
  ImageOverlay,
  MapContainer,
  Polygon,
  Polyline,
  Tooltip,
  useMap,
  useMapEvents,
} from "react-leaflet";
import type { Contour, PlannerProject } from "../types";
import { rad, toGeo } from "../lib/geometry";
import TerrainLayer from "./TerrainLayer";
import { dashPattern } from "../lib/presentation";
import "leaflet/dist/leaflet.css";
import OriginMapLayer, { type OriginMapProps } from "./OriginMapLayer";
import { CRS } from "leaflet";
import BasemapLayer from "./BasemapLayer";
import { LeafletAntennaHandle, SvgAntennaHandle } from "./AntennaMapHandle";
import {LeafletEditHandle} from "./MapEditHandles";
import MapTextLabel,{annotationIcon} from "./MapTextLabel";
import type {LocalBounds,XY} from "../lib/geometry-editing";
import {
  normalizeMapProvider,
  selectedMapProvider,
} from "../lib/map-providers";
export { TILES, mapProviders } from "../lib/map-providers";
export type DrawTool =
  | "pan"
  | "source"
  | "point"
  | "building"
  | "road"
  | "barrier"
  | "label"
  | "terrain"
  | "terrain-contour"
  | "ruler"
  | "calibrate-2lines"
  | "calibrate-line-point";
export interface DisplayOptions {
  grid: boolean;
  buildings: boolean;
  terrain: boolean;
  triangulation: boolean;
  lines: boolean;
  sources: boolean;
  points: boolean;
  contours: boolean;
  labels: boolean;
  azimuths: boolean;
}
interface Props {
  ruler?: OriginMapProps["ruler"];
  onOriginMove?: OriginMapProps["onOriginMove"];
  onRulerChange?: OriginMapProps["onRulerChange"];
  onAreaChange?: OriginMapProps["onAreaChange"];
  editableOriginGrid?: boolean;
  editableAntennas?: boolean;
  selectedSourceIds?: string[];
  onMoveSource?: (id: string, lat: number, lng: number) => void;
  onMovePoint?: (id:string,lat:number,lng:number)=>void;
  onMoveObject?: (id:string,dx:number,dy:number,vertex?:number)=>void;
  onBoundsChange?: (bounds:LocalBounds)=>void;
  onContextCommand?: (label:string)=>void;
  onSelectVerticalCut?: (id: string) => void;
  respectBuildingVisibility?: boolean;
  project: PlannerProject;
  contours: Contour[];
  display: DisplayOptions;
  tool: DrawTool;
  draft: [number, number][];
  onClick: (x: number, y: number) => void;
  onCursor?: (x: number, y: number) => void;
  onFinish: () => void;
  onSelectSource?: (id: string) => void;
  onSelectPoint?: (id: string) => void;
  onView?: (center: [number, number], zoom: number) => void;
  viewRequest?: { center: [number, number]; version: number } | null;
  onCenterChange?: (center: [number, number]) => void;
  selectedObject?: string;
  onSelectObject?: (id: string) => void;
}
function LeafletEvents({
  onClick,
  onView,
  onCursor,onBounds,onContext,
}: {
  onClick: Props["onClick"];
  onView?: Props["onView"];
  onCursor?:Props["onCursor"];
  onBounds?:(south:number,west:number,north:number,east:number)=>void;
  onContext?:(lat:number,lng:number)=>void;
}) {
  const map = useMapEvents({
    click: (e) => onClick(e.latlng.lat, e.latlng.lng),
    moveend: () =>
      {onView?.([map.getCenter().lat, map.getCenter().lng], map.getZoom());const b=map.getBounds();onBounds?.(b.getSouth(),b.getWest(),b.getNorth(),b.getEast());},
    mousemove:e=>onCursor?.(e.latlng.lat,e.latlng.lng),
    contextmenu:e=>{e.originalEvent.preventDefault();onContext?.(e.latlng.lat,e.latlng.lng);},
  });
  useEffect(()=>{const b=map.getBounds();onBounds?.(b.getSouth(),b.getWest(),b.getNorth(),b.getEast());},[map]);
  return null;
}
function ResetMap({
  origin,
  request,
  action,
}: {
  origin: [number, number];
  request?: Props["viewRequest"];
  action?: { version: number; delta: number };
}) {
  const map = useMap();
  useEffect(() => {
    map.setView(origin, map.getZoom());
  }, [map, origin[0], origin[1]]);
  useEffect(() => {
    if (request) map.setView(toGeo(...request.center, origin), map.getZoom());
  }, [map, request?.version]);
  useEffect(() => {
    if (action) {
      if (action.delta) map.setZoom(map.getZoom() + action.delta);
      else map.setView(origin, map.getZoom());
    }
  }, [map, action?.version]);
  useEffect(() => {
    const o = new ResizeObserver(() => map.invalidateSize());
    o.observe(map.getContainer());
    return () => o.disconnect();
  }, [map]);
  return null;
}
export default function PlannerMap(props: Props) {
  const {
    project: p,
    display,
    contours,
    tool,
    draft,
    onClick,
    onCursor,
    onFinish,
    onSelectSource,
  } = props;
  const [radius, setRadius] = useState(p.settings.radius_m),
    [offset, setOffset] = useState<[number, number]>([0, 0]),
    [mapAction, setMapAction] = useState<{ version: number; delta: number }>();
  const provider = selectedMapProvider(p);
  const [mapStatus, setMapStatus] = useState("");
  const [context,setContext]=useState<XY|null>(null);
  const drag = useRef<{
      x: number;
      y: number;
      offset: [number, number];
    } | null>(null),
    svg = useRef<SVGSVGElement>(null);
  useEffect(() => {
    setRadius(p.settings.radius_m);
    setOffset([0, 0]);
  }, [p.origin, p.settings.radius_m]);
  useEffect(() => {
    if (props.viewRequest) setOffset(props.viewRequest.center);
  }, [props.viewRequest?.version]);
  useEffect(() => props.onCenterChange?.(offset), [offset[0], offset[1]]);
  const geo = (pt: [number, number]) => toGeo(pt[0], pt[1], p.origin);
  const local = (lat: number, lng: number): [number, number] => {
    const R = 6378137,
      scale = Math.cos(rad(p.origin[0]));
    return [
      R * rad(lng - p.origin[1]) * scale,
      R *
        (Math.log(Math.tan(Math.PI / 4 + rad(lat) / 2)) -
          Math.log(Math.tan(Math.PI / 4 + rad(p.origin[0]) / 2))) *
        scale,
    ];
  };
  const fromEvent = (e: {
    clientX: number;
    clientY: number;
  }): [number, number] => {
    const c = svg.current?.getScreenCTM();
    if (!c) return [0, 0];
    const point = new DOMPoint(e.clientX, e.clientY).matrixTransform(
      c.inverse(),
    );
    return [point.x, -point.y];
  };
  const units = radius / 250;
  useEffect(()=>{
    if(p.settings.show_basemap || !svg.current)return;
    const element=svg.current;
    const refresh=()=>{const matrix=element.getScreenCTM();props.onBoundsChange?.({west:offset[0]-radius,south:offset[1]-radius,east:offset[0]+radius,north:offset[1]+radius,
      metresPerPixel:matrix?.a?1/Math.abs(matrix.a):undefined});};
    refresh();const observer=new ResizeObserver(refresh);observer.observe(element);return()=>observer.disconnect();
  },[p.settings.show_basemap,offset[0],offset[1],radius]);
  const selectedGeometry=p.buildings.find(b=>b.id===props.selectedObject)??p.lines.find(l=>l.id===props.selectedObject)??p.terrain_contours?.find(c=>c.id===props.selectedObject);
  const selectedTerrain=p.terrain_points.find(t=>t.id===props.selectedObject);
  const objectHandles:{id:string;point:XY;vertex?:number;label:string}[]=selectedGeometry?.points.length?[
    {id:selectedGeometry.id,point:[selectedGeometry.points.reduce((s,p)=>s+p[0],0)/selectedGeometry.points.length,selectedGeometry.points.reduce((s,p)=>s+p[1],0)/selectedGeometry.points.length],label:"Déplacer l’objet sélectionné"},
    ...selectedGeometry.points.map((point,vertex)=>({id:selectedGeometry.id,point,vertex,label:`Déplacer le sommet ${vertex+1}`})),
  ]:selectedTerrain?[{id:selectedTerrain.id,point:[selectedTerrain.x,selectedTerrain.y],label:"Déplacer le point de terrain"}]:[];
  const originLayer = {
    project: p,
    grid: display.grid,
    units,
    editable: props.editableOriginGrid ?? false,
    ruler: props.ruler,
    onOriginMove: props.onOriginMove,
    onRulerChange: props.onRulerChange,
    onAreaChange: props.onAreaChange,
  };
  return (
    <div className={`planner-map tool-${tool}`}>
      <div className="map-tools">
        <button
          title="Recentrer sur l’origine"
          onClick={() => {
            setOffset([0, 0]);
            setRadius(p.settings.radius_m);
            setMapAction({ version: Date.now(), delta: 0 });
          }}
        >
          ⌖
        </button>
        <button
          title="Zoom avant"
          onClick={() => {
            setRadius((r) => Math.max(10, r / 1.25));
            setMapAction({ version: Date.now(), delta: 1 });
          }}
        >
          ＋
        </button>
        <button
          title="Zoom arrière"
          onClick={() => {
            setRadius((r) => Math.min(20000, r * 1.25));
            setMapAction({ version: Date.now(), delta: -1 });
          }}
        >
          −
        </button>
        <span>
          {p.settings.show_basemap
            ? normalizeMapProvider(p.settings.map_provider)
            : "Plan local"}
        </span>
        <span className="map-mode">
          {tool === "pan"
            ? "Déplacer le plan"
            : tool === "ruler"
              ? "Mesurer une distance"
              : "Cliquer pour placer"}
        </span>
        {draft.length > 0 && (
          <button onClick={onFinish}>Terminer ({draft.length})</button>
        )}
      </div>
      {p.settings.show_basemap && mapStatus && (
        <div
          role="status"
          className="map-provider-status"
          style={{
            position: "absolute",
            top: 34,
            left: 8,
            zIndex: 900,
            maxWidth: 440,
            background: "#fffbe5",
            border: "1px solid #d4c56b",
            padding: "4px 8px",
            fontSize: 12,
          }}
        >
          {mapStatus}
        </div>
      )}
      {p.settings.show_basemap ? (
        <MapContainer
          key={provider?.projection ?? "spherical"}
          crs={
            provider?.projection === "ellipsoidal" ? CRS.EPSG3395 : CRS.EPSG3857
          }
          center={p.map_center}
          zoom={p.map_zoom}
          zoomControl={false}
          doubleClickZoom={false}
        >
          <BasemapLayer provider={provider} onStatus={setMapStatus} />
          <ResetMap
            origin={p.origin}
            request={props.viewRequest}
            action={mapAction}
          />
          <LeafletEvents
            onClick={(lat, lng) => onClick(...local(lat, lng))}
            onCursor={(lat,lng)=>onCursor?.(...local(lat,lng))}
            onBounds={(s,w,n,e)=>{const sw=local(s,w),ne=local(n,e);props.onBoundsChange?.({west:sw[0],south:sw[1],east:ne[0],north:ne[1]});}}
            onContext={(lat,lng)=>{if(tool!=="pan")onFinish();else setContext(local(lat,lng));}}
            onView={(center, zoom) => {
              props.onView?.(center, zoom);
              props.onCenterChange?.(local(...center));
            }}
          />
          <TerrainLayer
            project={p}
            display={display}
            leaflet
            selected={props.selectedObject}
            onSelect={tool === "pan" ? props.onSelectObject : undefined}
          />
          <OriginMapLayer {...originLayer} leaflet />
          {display.buildings &&
            p.buildings.filter(b => !props.respectBuildingVisibility || b.visible !== false).map((b) => (
              <Polygon
                key={b.id}
                positions={b.points.map(geo)}
                pathOptions={{
                  color: props.selectedObject === b.id ? "#0078d7" : "#333",
                  weight: 1,
                  fillColor: "#ddd",
                  fillOpacity: 0.3,
                }}
                bubblingMouseEvents={tool !== "pan"}
                eventHandlers={{
                  click: () => {
                    if (tool === "pan") props.onSelectObject?.(b.id);
                  },
                }}
              >
                <Tooltip>
                  {b.name} · {b.height_m} m
                </Tooltip>
              </Polygon>
            ))}
          {display.lines &&
            p.lines
              .filter((l) => l.points.length > 1)
              .map((l) => (
                <Polyline
                  key={l.id}
                  positions={l.points.map(geo)}
                  pathOptions={{
                    color: props.selectedObject === l.id ? "#0078d7" : l.color,
                    weight: l.width,
                    dashArray: l.kind === "barrier" ? "5 4" : undefined,
                  }}
                  bubblingMouseEvents={tool !== "pan"}
                  eventHandlers={{
                    click: () => {
                      if (tool === "pan") props.onSelectObject?.(l.id);
                    },
                  }}
                />
              ))}
          {display.lines&&p.lines.filter(l=>l.kind==="label"&&l.points[0]).map(l=><MapTextLabel key={l.id} line={l} position={geo(l.points[0])} onSelect={()=>props.onSelectObject?.(l.id)}/>)}
          {display.contours &&
            p.vertical_cuts.filter(c => c.active).map(c => <Polyline key={`vertical-${c.id}`}
              positions={[-c.backward_m, c.forward_m].map(d => geo([c.x + d * Math.sin(rad(c.azimuth_deg)), c.y + d * Math.cos(rad(c.azimuth_deg))]))}
              pathOptions={{ color: "#8b248e", weight: 2, dashArray: "7 5" }}
              bubblingMouseEvents={false} eventHandlers={{ click: () => props.onSelectVerticalCut?.(c.id) }}><Tooltip>Coupe verticale {c.azimuth_deg}°</Tooltip></Polyline>)}
          {display.contours &&
            contours.map((c, i) => (
              <Polyline
                key={i}
                positions={c.segments.map((s) => s.map(geo))}
                pathOptions={{
                  color: c.color,
                  weight: p.settings.contour_width ?? 1.4,
                  opacity: p.settings.contour_opacity ?? 1,
                  dashArray: dashPattern(c.line_style),
                }}
                interactive={false}
              />
            ))}
          {display.sources &&
            p.sources.map((s) =>
              props.editableAntennas && tool === "pan" ? (
                <LeafletAntennaHandle
                  key={s.id}
                  source={s}
                  selected={props.selectedSourceIds?.includes(s.id) ?? false}
                  onSelect={() => onSelectSource?.(s.id)}
                  onMove={(lat, lng) => props.onMoveSource?.(s.id, lat, lng)}
                />
              ) : (
                <CircleMarker
                  key={s.id}
                  center={[s.lat, s.lng]}
                  radius={4}
                  pathOptions={{
                    color: s.active === false ? "#999" : "#1a6748",
                    fillOpacity: 1,
                  }}
                  eventHandlers={{
                    click: (e) => {
                      if (tool === "pan") {
                        e.originalEvent.stopPropagation();
                        onSelectSource?.(s.id);
                      }
                    },
                  }}
                  bubblingMouseEvents={tool !== "pan"}
                >
                  <Tooltip>
                    {s.name} · {s.frequency_mhz} MHz
                  </Tooltip>
                </CircleMarker>
              ),
            )}
          {display.points &&
            p.control_points.map((c) => props.onMovePoint&&tool==="pan"?<LeafletEditHandle key={c.id} position={[c.lat,c.lng]} label={`Déplacer le point ${c.name}`} round onSelect={()=>props.onSelectPoint?.(c.id)} onMove={(lat,lng)=>props.onMovePoint?.(c.id,lat,lng)}/>:(
              <CircleMarker
                key={c.id}
                center={[c.lat, c.lng]}
                radius={3}
                pathOptions={{
                  color: "#252525",
                  fillColor: "white",
                  fillOpacity: 1,
                }}
                bubblingMouseEvents={tool !== "pan"}
                eventHandlers={{
                  click: () => {
                    if (tool === "pan") props.onSelectPoint?.(c.id);
                  },
                }}
              >
                <Tooltip permanent direction="right">
                  {c.name}
                </Tooltip>
              </CircleMarker>
            ))}
          {p.background_image && provider?.kind === "image" && (
            <ImageOverlay
              url={p.background_image.data_url}
              bounds={[
                [p.background_image.south, p.background_image.west],
                [p.background_image.north, p.background_image.east],
              ]}
              opacity={p.background_image.opacity}
            />
          )}
          {draft.length > 1 && (
            <Polyline
              positions={draft.map(geo)}
              pathOptions={{ color: "#0078d7", dashArray: "5 4" }}
            />
          )}
          {props.onMoveObject&&tool==="pan"&&objectHandles.map((h,i)=><LeafletEditHandle key={`${h.id}:${i}`} label={h.label} position={geo(h.point)} onMove={(lat,lng)=>{const pos=local(lat,lng);props.onMoveObject?.(h.id,pos[0]-h.point[0],pos[1]-h.point[1],h.vertex);}}/>)}
        </MapContainer>
      ) : (
        <svg
          ref={svg}
          className="local-map"
          viewBox={`${offset[0] - radius} ${-offset[1] - radius} ${radius * 2} ${radius * 2}`}
          preserveAspectRatio="xMidYMid meet"
          aria-label="Plan de calcul, coordonnées en mètres"
          onWheel={(e) =>
            setRadius((r) =>
              Math.min(20000, Math.max(10, r * (e.deltaY > 0 ? 1.1 : 0.9))),
            )
          }
          onPointerDown={(e) => {
            if (tool === "pan") {
              // Capturing a selectable object's pointer here would redirect its
              // click to the background and prevent its selection/preview.
              if (e.target instanceof Element && e.target.closest('[role="button"], polygon, polyline, circle, text')) return;
              drag.current = { x: e.clientX, y: e.clientY, offset };
              e.currentTarget.setPointerCapture(e.pointerId);
            }
          }}
          onPointerMove={(e) => {
            const [x, y] = fromEvent(e);
            onCursor?.(x, y);
            if (drag.current) {
              const scale =
                (2 * radius) /
                Math.min(
                  e.currentTarget.clientWidth,
                  e.currentTarget.clientHeight,
                );
              setOffset([
                drag.current.offset[0] - (e.clientX - drag.current.x) * scale,
                drag.current.offset[1] + (e.clientY - drag.current.y) * scale,
              ]);
            }
          }}
          onPointerUp={() => {
            drag.current = null;
          }}
          onClick={(e) => {
            if (tool !== "pan") onClick(...fromEvent(e));
          }}
          onDoubleClick={() => {
            if (draft.length) onFinish();
          }}
          onContextMenu={e=>{e.preventDefault();if(tool!=="pan")onFinish();else setContext(fromEvent(e));}}
        >
          <defs>
            <pattern
              id="plan-grid"
              width={50}
              height={50}
              patternUnits="userSpaceOnUse"
            >
              <path
                d="M 50 0 L 0 0 0 50"
                fill="none"
                stroke="#c6c6c6"
                strokeWidth=".5"
                vectorEffect="non-scaling-stroke"
              />
            </pattern>
          </defs>
          <rect
            x={offset[0] - radius}
            y={-offset[1] - radius}
            width={radius * 2}
            height={radius * 2}
            fill="#fcfcfc"
          />
          {p.background_image &&
            (() => {
              const a = local(
                  p.background_image.north,
                  p.background_image.west,
                ),
                b = local(p.background_image.south, p.background_image.east);
              return (
                <image
                  href={p.background_image.data_url}
                  x={a[0]}
                  y={-a[1]}
                  width={b[0] - a[0]}
                  height={a[1] - b[1]}
                  opacity={p.background_image.opacity}
                />
              );
            })()}
          <TerrainLayer
            project={p}
            display={display}
            units={units}
            selected={props.selectedObject}
            onSelect={tool === "pan" ? props.onSelectObject : undefined}
          />
          {display.buildings &&
            p.buildings.filter(b => !props.respectBuildingVisibility || b.visible !== false).map((b) => (
              <g
                key={b.id}
                onClick={(e) => {
                  if (tool === "pan") {
                    e.stopPropagation();
                    props.onSelectObject?.(b.id);
                  }
                }}
              >
                <polygon
                  points={b.points.map(([x, y]) => `${x},${-y}`).join(" ")}
                  fill="#e9e9e9"
                  fillOpacity=".7"
                  stroke={props.selectedObject === b.id ? "#0078d7" : "#303030"}
                  strokeWidth="1.2"
                  vectorEffect="non-scaling-stroke"
                />
                {props.selectedObject === b.id &&
                  b.points.map(([x, y], i) => (
                    <circle
                      key={i}
                      cx={x}
                      cy={-y}
                      r={2.2 * units}
                      fill="white"
                      stroke="#0078d7"
                      vectorEffect="non-scaling-stroke"
                    />
                  ))}
                {display.labels && b.points.length > 0 && (
                  <text
                    x={b.points.reduce((s, v) => s + v[0], 0) / b.points.length}
                    y={
                      -b.points.reduce((s, v) => s + v[1], 0) / b.points.length
                    }
                    textAnchor="middle"
                    fontSize={9 * units}
                    fill="#333"
                  >
                    {b.name}
                    <tspan
                      x={
                        b.points.reduce((s, v) => s + v[0], 0) / b.points.length
                      }
                      dy={11 * units}
                    >
                      {b.height_m} m
                    </tspan>
                  </text>
                )}
              </g>
            ))}
          {display.lines &&
            p.lines.map((l) =>
              l.kind === "label" ? (
                <text
                  key={l.id}
                  onClick={(e) => {
                    if (tool === "pan") {
                      e.stopPropagation();
                      props.onSelectObject?.(l.id);
                    }
                  }}
                  x={l.points[0]?.[0]}
                  y={-l.points[0]?.[1]}
                  fontSize={(l.font_size??10) * units}
                  fontFamily={l.font_name??"Arial"}
                  transform={`rotate(${l.angle_deg??0} ${l.points[0]?.[0]??0} ${-(l.points[0]?.[1]??0)})`}
                  fill={l.color}
                >
                  {annotationIcon(l.icon)} {l.name}
                </text>
              ) : (
                <polyline
                  key={l.id}
                  onClick={(e) => {
                    if (tool === "pan") {
                      e.stopPropagation();
                      props.onSelectObject?.(l.id);
                    }
                  }}
                  points={l.points.map(([x, y]) => `${x},${-y}`).join(" ")}
                  fill="none"
                  stroke={props.selectedObject === l.id ? "#0078d7" : l.color}
                  strokeWidth={l.width}
                  strokeDasharray={l.kind === "barrier" ? "5 4" : undefined}
                  vectorEffect="non-scaling-stroke"
                />
              ),
            )}
          {display.contours &&
            p.vertical_cuts.filter(c => c.active).map(c => {
              const x1 = c.x - c.backward_m * Math.sin(rad(c.azimuth_deg)), y1 = c.y - c.backward_m * Math.cos(rad(c.azimuth_deg));
              const x2 = c.x + c.forward_m * Math.sin(rad(c.azimuth_deg)), y2 = c.y + c.forward_m * Math.cos(rad(c.azimuth_deg));
              return <g key={`vertical-${c.id}`} role="button" tabIndex={0} aria-label={`Coupe verticale ${c.azimuth_deg}°`}
                onKeyDown={e => { if(e.key === "Enter" || e.key === " "){e.preventDefault();props.onSelectVerticalCut?.(c.id);} }}
                onClick={e => { if (tool === "pan") { e.stopPropagation(); props.onSelectVerticalCut?.(c.id); } }}>
                <rect x={0} y={-3*units} width={Math.hypot(x2-x1,y2-y1)} height={6*units} fill="transparent" pointerEvents="all"
                  transform={`translate(${x1} ${-y1}) rotate(${Math.atan2(y1-y2,x2-x1)*180/Math.PI})`}/>
                <line x1={x1} y1={-y1} x2={x2} y2={-y2} stroke="transparent" strokeWidth={8} vectorEffect="non-scaling-stroke" pointerEvents="stroke"/>
                <line x1={x1} y1={-y1} x2={x2} y2={-y2} stroke="#8b248e" strokeDasharray="7 5" strokeWidth={2} vectorEffect="non-scaling-stroke" pointerEvents="none"/>
              </g>;
            })}
          {display.contours &&
            contours.map((c, i) => (
              <path
                key={i}
                d={c.segments
                  .map((s) => `M${s[0][0]},${-s[0][1]}L${s[1][0]},${-s[1][1]}`)
                  .join(" ")}
                fill="none"
                stroke={c.color}
                strokeWidth={p.settings.contour_width ?? 1.4}
                opacity={p.settings.contour_opacity ?? 1}
                strokeDasharray={dashPattern(c.line_style)}
                vectorEffect="non-scaling-stroke"
              />
            ))}
          {display.sources &&
            p.sources
              .map((s, sourceIndex) => ({ s, sourceIndex }))
              .sort(
                (a, b) =>
                  Number(props.selectedSourceIds?.includes(a.s.id) ?? false) -
                  Number(props.selectedSourceIds?.includes(b.s.id) ?? false),
              )
              .map(({ s, sourceIndex }) => {
                const [x, y] = local(s.lat, s.lng);
                const coincident = p.sources
                  .map((v, i) => ({ v, i }))
                  .filter(
                    ({ v }) =>
                      Math.abs(v.lat - s.lat) < 1e-8 &&
                      Math.abs(v.lng - s.lng) < 1e-8,
                  );
                const first = coincident[0]?.i === sourceIndex;
                const markerText =
                  coincident.length > 2 &&
                  coincident.every((v, i) => v.i === coincident[0].i + i)
                    ? `A${coincident[0].i + 1}–A${coincident[coincident.length - 1].i + 1}`
                    : coincident.map((v) => `A${v.i + 1}`).join(", ");
                const marker = (
                  <g
                    key={s.id}
                    onClick={(e) => {
                      if (tool === "pan") {
                        e.stopPropagation();
                        onSelectSource?.(s.id);
                      }
                    }}
                  >
                    {display.azimuths && (
                      <line
                        x1={x}
                        y1={-y}
                        x2={x + radius * 0.6 * Math.sin(rad(s.azimuth_deg))}
                        y2={-y - radius * 0.6 * Math.cos(rad(s.azimuth_deg))}
                        stroke={s.active === false ? "#bbb" : "#343434"}
                        vectorEffect="non-scaling-stroke"
                        strokeDasharray="4 2"
                        pointerEvents="none"
                      />
                    )}
                    <circle
                      cx={x}
                      cy={-y}
                      r={3 * units}
                      fill={s.active === false ? "#aaa" : "#7ecf78"}
                      stroke={
                        props.selectedSourceIds?.includes(s.id)
                          ? "#0078d7"
                          : "#333"
                      }
                      strokeWidth={
                        props.selectedSourceIds?.includes(s.id) ? 2.5 : 1
                      }
                      vectorEffect="non-scaling-stroke"
                    />
                    {display.labels && first && (
                      <text
                        x={x + 5 * units}
                        y={-y - 5 * units}
                        fontSize={8 * units}
                        fill="#2b377f"
                      >
                        {markerText}
                        <title>
                          {coincident.map((v) => v.v.name).join(", ")}
                        </title>
                      </text>
                    )}
                  </g>
                );
                return props.editableAntennas && tool === "pan" ? (
                  <SvgAntennaHandle
                    key={s.id}
                    point={[x, y]}
                    label={`Déplacer l’antenne ${sourceIndex + 1}`}
                    onSelect={() => onSelectSource?.(s.id)}
                    onMove={(mx, my) =>
                      props.onMoveSource?.(s.id, ...geo([mx, my]))
                    }
                  >
                    {marker}
                  </SvgAntennaHandle>
                ) : (
                  marker
                );
              })}
          {display.points &&
            p.control_points.map((c) => (
              <g
                key={c.id}
                onClick={(e) => {
                  if (tool === "pan") {
                    e.stopPropagation();
                    props.onSelectPoint?.(c.id);
                  }
                }}
              >
                <circle
                  cx={c.x_m}
                  cy={-c.y_m}
                  r={2 * units}
                  fill="white"
                  stroke="#222"
                  vectorEffect="non-scaling-stroke"
                />
                {display.labels && (
                  <text
                    x={c.x_m + 4 * units}
                    y={-c.y_m}
                    fontSize={8 * units}
                    fill="#27327a"
                  >
                    {c.name} (
                    {c.heights?.map((h) => h.height_m).join(";") ?? c.height_m}{" "}
                    m)
                  </text>
                )}
              </g>
            ))}
          <OriginMapLayer {...originLayer} />
          {props.onMoveObject&&tool==="pan"&&objectHandles.map((h,i)=><SvgAntennaHandle key={`${h.id}:${i}`} point={h.point} label={h.label} onSelect={()=>props.onSelectObject?.(h.id)} onMove={(x,y)=>props.onMoveObject?.(h.id,x-h.point[0],y-h.point[1],h.vertex)}><circle cx={h.point[0]} cy={-h.point[1]} r={(h.vertex===undefined?4:2.5)*units} fill="white" stroke="#0078d7" vectorEffect="non-scaling-stroke"/></SvgAntennaHandle>)}
          {props.onMovePoint&&tool==="pan"&&display.points&&p.control_points.map(c=><SvgAntennaHandle key={`point-handle:${c.id}`} point={[c.x_m,c.y_m]} label={`Déplacer le point ${c.name}`} onSelect={()=>props.onSelectPoint?.(c.id)} onMove={(x,y)=>props.onMovePoint?.(c.id,...toGeo(x,y,p.origin))}><circle cx={c.x_m} cy={-c.y_m} r={4*units} fill="white" stroke="#0078d7" vectorEffect="non-scaling-stroke"/></SvgAntennaHandle>)}
          {draft.length > 0 && (
            <>
              <polyline
                points={draft.map(([x, y]) => `${x},${-y}`).join(" ")}
                fill="#0078d712"
                stroke="#0078d7"
                strokeDasharray="5 3"
                vectorEffect="non-scaling-stroke"
              />
              {draft.map(([x, y], i) => (
                <circle key={i} cx={x} cy={-y} r={2 * units} fill="#0078d7" />
              ))}
            </>
          )}
        </svg>
      )}
      {context&&<div role="menu" aria-label="Actions du plan" style={{position:"absolute",zIndex:1200,left:"35%",top:"35%",background:"white",border:"1px solid #999",padding:5,display:"grid",gap:4}}>
        {props.onOriginMove&&<button role="menuitem" onClick={()=>{props.onOriginMove?.(...context);setContext(null);}}>Définir l’origine ici</button>}
        {props.onContextCommand&&["Supprimer les objets hors écran","Supprimer les objets hors écran sans terrain"].map(label=><button role="menuitem" key={label} onClick={()=>{props.onContextCommand?.(label);setContext(null);}}>{label}</button>)}
        <button role="menuitem" onClick={()=>setContext(null)}>Fermer le menu</button>
      </div>}
      <div className="north-arrow">
        N<span>↑</span>
      </div>
      <div className="map-scale">
        <span />
        {Math.round(radius / 3)} m
      </div>
    </div>
  );
}
