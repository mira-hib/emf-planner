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
}: {
  onClick: Props["onClick"];
  onView?: Props["onView"];
}) {
  const map = useMapEvents({
    click: (e) => onClick(e.latlng.lat, e.latlng.lng),
    moveend: () =>
      onView?.([map.getCenter().lat, map.getCenter().lng], map.getZoom()),
  });
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
            p.buildings.map((b) => (
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
            p.control_points.map((c) => (
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
            p.buildings.map((b) => (
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
                  fontSize={10 * units}
                  fill={l.color}
                >
                  {l.name}
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
