import { useRef, useState, type PointerEvent } from "react";
import { Marker, Polygon, Polyline, Tooltip } from "react-leaflet";
import { divIcon } from "leaflet";
import type { PlannerProject } from "../types";
import { toGeo, toLocal } from "../lib/geometry";
import { areaFromBounds, calculationArea } from "../lib/origin-grid";
import { cadAxisTicks } from "../lib/cad";

type Pair = [number, number];
export interface OriginMapProps {
  project: PlannerProject;
  grid: boolean;
  leaflet?: boolean;
  units: number;
  editable: boolean;
  ruler?: Pair[];
  onOriginMove?: (x: number, y: number) => void;
  onRulerChange?: (points: Pair[]) => void;
  onAreaChange?: (patch: ReturnType<typeof areaFromBounds>) => void;
}
function SvgHandle({
  point,
  units,
  label,
  onCommit,
  cross = false,
}: {
  point: Pair;
  units: number;
  label: string;
  onCommit: (p: Pair) => void;
  cross?: boolean;
}) {
  const [preview, setPreview] = useState<Pair | null>(null);
  const active = useRef(false);
  const coordinate = (e: PointerEvent<SVGCircleElement>): Pair => {
    const matrix = e.currentTarget.ownerSVGElement?.getScreenCTM();
    if (!matrix) return point;
    const p = new DOMPoint(e.clientX, e.clientY).matrixTransform(
      matrix.inverse(),
    );
    return [p.x, -p.y];
  };
  const [x, y] = preview ?? point;
  return (
    <g className="origin-map-handle">
      <title>{label}</title>
      {cross && (
        <path
          d={`M${x - 6 * units} ${-y}h${12 * units}M${x} ${-y - 6 * units}v${12 * units}`}
          stroke="#006400"
          strokeWidth="1.5"
          vectorEffect="non-scaling-stroke"
          pointerEvents="none"
        />
      )}
      <circle
        role="button"
        aria-label={label}
        cx={x}
        cy={-y}
        r={(cross ? 7 : 5) * units}
        fill={cross ? "transparent" : "#ffffff"}
        stroke={cross ? "transparent" : "#00008b"}
        strokeWidth="1.5"
        vectorEffect="non-scaling-stroke"
        onClick={(e) => e.stopPropagation()}
        onPointerDown={(e) => {
          if (e.button !== 0) return;
          e.stopPropagation();
          active.current = true;
          e.currentTarget.setPointerCapture(e.pointerId);
        }}
        onPointerMove={(e) => {
          if (!active.current) return;
          e.stopPropagation();
          setPreview(coordinate(e));
        }}
        onPointerUp={(e) => {
          if (!active.current) return;
          e.stopPropagation();
          active.current = false;
          const p = coordinate(e);
          setPreview(null);
          if (Math.hypot(p[0] - point[0], p[1] - point[1]) > 0.001) onCommit(p);
        }}
        onPointerCancel={() => {
          active.current = false;
          setPreview(null);
        }}
      />
    </g>
  );
}

const handleIcon = (cross: boolean) =>
  divIcon({
    className: cross ? "origin-leaflet-handle" : "area-leaflet-handle",
    html: cross ? "+" : "",
    iconSize: [14, 14],
    iconAnchor: [7, 7],
  });
export default function OriginMapLayer(props: OriginMapProps) {
  const { project: p, grid, units, editable, ruler = [] } = props;
  const a = calculationArea(p),
    color = "#00008b";
  const corners: Pair[] = [
    [a.x_min, a.y_min],
    [a.x_max, a.y_min],
    [a.x_max, a.y_max],
    [a.x_min, a.y_max],
  ];
  const geo = (point: Pair) => toGeo(...point, p.origin);
  const xt = grid ? cadAxisTicks(a.x_min, a.x_max) : [];
  const yt = grid ? cadAxisTicks(a.y_min, a.y_max) : [];
  const showArea = p.settings.show_calculation_area !== false;
  const rulerText =
    ruler.length === 2
      ? `${Math.hypot(ruler[1][0] - ruler[0][0], ruler[1][1] - ruler[0][1]).toFixed(1)} m · ${(((Math.atan2(ruler[1][0] - ruler[0][0], ruler[1][1] - ruler[0][1]) * 180) / Math.PI + 360) % 360).toFixed(1)}°`
      : "";
  function resize(index: number, point: Pair) {
    const area = {
      ...a,
      [index === 0 || index === 3 ? "x_min" : "x_max"]: point[0],
      [index < 2 ? "y_min" : "y_max"]: point[1],
    };
    const ordered = {
      x_min: Math.min(area.x_min, area.x_max),
      x_max: Math.max(area.x_min, area.x_max),
      y_min: Math.min(area.y_min, area.y_max),
      y_max: Math.max(area.y_min, area.y_max),
    };
    if (ordered.x_min < ordered.x_max && ordered.y_min < ordered.y_max)
      props.onAreaChange?.(areaFromBounds(p.origin, ordered));
  }
  function moveRuler(i: number, point: Pair) {
    props.onRulerChange?.(ruler.map((v, j) => (j === i ? point : v)));
  }
  if (props.leaflet) {
    const handle = (
      point: Pair,
      label: string,
      commit: (point: Pair) => void,
      cross = false,
    ) => (
      <Marker
        key={label}
        position={geo(point)}
        icon={handleIcon(cross)}
        draggable
        title={label}
        alt={label}
        eventHandlers={{
          dragend: (e) => {
            const pos = e.target.getLatLng();
            commit(toLocal(pos.lat, pos.lng, p.origin));
          },
        }}
      />
    );
    return (
      <>
        {showArea && (
          <>
            <Polygon
              positions={corners.map(geo)}
              pathOptions={{
                color,
                weight: 2,
                fill: false,
                interactive: false,
              }}
            />
            {xt.map((x) => (
              <Polyline
                key={`x${x}`}
                positions={[geo([x, a.y_min]), geo([x, a.y_max])]}
                pathOptions={{
                  color,
                  weight: 1,
                  opacity: 0.35,
                  interactive: false,
                }}
              >
                <Tooltip
                  permanent
                  direction="bottom"
                  position={geo([x, a.y_min])}
                  className="origin-grid-tick"
                >
                  {x}
                </Tooltip>
              </Polyline>
            ))}
            {yt.map((y) => (
              <Polyline
                key={`y${y}`}
                positions={[geo([a.x_min, y]), geo([a.x_max, y])]}
                pathOptions={{
                  color,
                  weight: 1,
                  opacity: 0.35,
                  interactive: false,
                }}
              >
                <Tooltip
                  permanent
                  direction="left"
                  position={geo([a.x_min, y])}
                  className="origin-grid-tick"
                >
                  {y}
                </Tooltip>
              </Polyline>
            ))}
            {editable &&
              corners.map((pt, i) =>
                handle(pt, `Redimensionner la zone : coin ${i + 1}`, (v) =>
                  resize(i, v),
                ),
              )}
          </>
        )}
        {p.settings.show_origin !== false &&
          (editable ? (
            handle(
              [0, 0],
              "Déplacer l’origine",
              (v) => props.onOriginMove?.(...v),
              true,
            )
          ) : (
            <Marker
              position={p.origin}
              icon={handleIcon(true)}
              interactive={false}
            />
          ))}
        {ruler.length === 2 && (
          <>
            <Polyline
              positions={ruler.map(geo)}
              pathOptions={{ color: "#111", weight: 2 }}
            >
              <Tooltip permanent direction="top">
                {rulerText}
              </Tooltip>
            </Polyline>
            {ruler.map((v, i) =>
              handle(v, `Extrémité ${i + 1} de la règle`, (v) =>
                moveRuler(i, v),
              ),
            )}
          </>
        )}
      </>
    );
  }
  return (
    <g data-origin-grid-layer="true">
      {showArea && (
        <g data-calculation-area="true">
          <rect
            x={a.x_min}
            y={-a.y_max}
            width={a.x_max - a.x_min}
            height={a.y_max - a.y_min}
            fill="none"
            stroke={color}
            strokeWidth="2"
            vectorEffect="non-scaling-stroke"
            pointerEvents="none"
          />
          {xt.map((x) => (
            <g key={`x${x}`} pointerEvents="none">
              <path
                d={`M${x} ${-a.y_min}V${-a.y_max}`}
                stroke={color}
                strokeWidth=".6"
                opacity=".45"
                vectorEffect="non-scaling-stroke"
              />
              <text
                x={x}
                y={-a.y_min + 11 * units}
                fontSize={8 * units}
                fill={color}
                textAnchor="middle"
              >
                {x}
              </text>
            </g>
          ))}
          {yt.map((y) => (
            <g key={`y${y}`} pointerEvents="none">
              <path
                d={`M${a.x_min} ${-y}H${a.x_max}`}
                stroke={color}
                strokeWidth=".6"
                opacity=".45"
                vectorEffect="non-scaling-stroke"
              />
              <text
                x={a.x_min - 4 * units}
                y={-y + 3 * units}
                fontSize={8 * units}
                fill={color}
                textAnchor="end"
              >
                {y}
              </text>
            </g>
          ))}
          {editable &&
            corners.map((pt, i) => (
              <SvgHandle
                key={i}
                point={pt}
                units={units}
                label={`Redimensionner la zone : coin ${i + 1}`}
                onCommit={(v) => resize(i, v)}
              />
            ))}
        </g>
      )}
      {p.settings.show_origin !== false &&
        (editable ? (
          <SvgHandle
            point={[0, 0]}
            units={units}
            label="Déplacer l’origine"
            cross
            onCommit={(v) => props.onOriginMove?.(...v)}
          />
        ) : (
          <path
            d={`M${-5 * units} 0H${5 * units}M0 ${-5 * units}V${5 * units}`}
            stroke="#006400"
            strokeWidth="1.5"
            vectorEffect="non-scaling-stroke"
          />
        ))}
      {ruler.length === 2 && (
        <g data-ruler="true">
          <path
            d={`M${ruler[0][0]} ${-ruler[0][1]}L${ruler[1][0]} ${-ruler[1][1]}`}
            stroke="#111"
            strokeWidth="2"
            vectorEffect="non-scaling-stroke"
            pointerEvents="none"
          />
          {ruler.map((v, i) => (
            <SvgHandle
              key={i}
              point={v}
              units={units}
              label={`Extrémité ${i + 1} de la règle`}
              onCommit={(v) => moveRuler(i, v)}
            />
          ))}
          <text
            x={(ruler[0][0] + ruler[1][0]) / 2}
            y={-(ruler[0][1] + ruler[1][1]) / 2 - 8 * units}
            fontSize={10 * units}
            textAnchor="middle"
          >
            {Math.hypot(
              ruler[1][0] - ruler[0][0],
              ruler[1][1] - ruler[0][1],
            ).toFixed(1)}{" "}
            m
          </text>
        </g>
      )}
    </g>
  );
}
