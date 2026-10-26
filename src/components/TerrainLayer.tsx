import { CircleMarker, Polyline, Tooltip } from "react-leaflet";
import type { PlannerProject } from "../types";
import { terrainMesh, toGeo } from "../lib/geometry";
import type { DisplayOptions } from "./PlannerMap";
export default function TerrainLayer({
  project: p,
  display,
  leaflet = false,
  units = 1,
  selected,
  onSelect,
}: {
  project: PlannerProject;
  display: DisplayOptions;
  leaflet?: boolean;
  units?: number;
  selected?: string;
  onSelect?: (id: string) => void;
}) {
  if (!display.terrain && !display.triangulation) return null;
  const { points, triangles } = terrainMesh(p),
    edges: [number, number][] = [],
    seen = new Set<string>();
  if (display.triangulation)
    for (let i = 0; i < triangles.length; i += 3)
      for (let j = 0; j < 3; j++) {
        const a = triangles[i + j],
          b = triangles[i + ((j + 1) % 3)],
          key = `${Math.min(a, b)}:${Math.max(a, b)}`;
        if (!seen.has(key)) {
          seen.add(key);
          edges.push([a, b]);
        }
      }
  const geo = (xy: [number, number]) => toGeo(...xy, p.origin);
  return (
    <>
      {display.triangulation &&
        (leaflet ? (
          <Polyline
            positions={edges.map(([a, b]) => [
              geo([points[a].x, points[a].y]),
              geo([points[b].x, points[b].y]),
            ])}
            pathOptions={{ color: "#9c977d", weight: 0.7, opacity: 0.65 }}
            interactive={false}
          />
        ) : (
          <path
            aria-label="Triangulation du terrain"
            d={edges
              .map(
                ([a, b]) =>
                  `M${points[a].x} ${-points[a].y}L${points[b].x} ${-points[b].y}`,
              )
              .join(" ")}
            fill="none"
            stroke="#9c977d"
            strokeWidth=".7"
            opacity=".65"
            vectorEffect="non-scaling-stroke"
            pointerEvents="none"
          />
        ))}
      {display.terrain &&
        (p.terrain_contours ?? []).map((c) => {
          const line = c.closed ? [...c.points, c.points[0]] : c.points,
            color = selected === c.id ? "#0078d7" : "#997f50";
          return leaflet ? (
            <Polyline
              key={c.id}
              positions={line.map(geo)}
              pathOptions={{ color, weight: 1.2 }}
              bubblingMouseEvents={!onSelect}
              eventHandlers={{ click: () => onSelect?.(c.id) }}
            >
              <Tooltip>
                {c.name} · {c.elevation} m
              </Tooltip>
            </Polyline>
          ) : (
            <g
              key={c.id}
              data-terrain-contour={c.id}
              onClick={(e) => {
                if (onSelect) {
                  e.stopPropagation();
                  onSelect(c.id);
                }
              }}
            >
              <polyline
                points={line.map(([x, y]) => `${x},${-y}`).join(" ")}
                fill="none"
                stroke={color}
                strokeWidth="1.2"
                vectorEffect="non-scaling-stroke"
              />
              {selected === c.id &&
                c.points.map(([x, y], i) => (
                  <circle
                    key={i}
                    cx={x}
                    cy={-y}
                    r={2 * units}
                    fill="white"
                    stroke={color}
                    vectorEffect="non-scaling-stroke"
                  />
                ))}
              {display.labels && c.points[0] && (
                <text
                  x={c.points[0][0] + 3 * units}
                  y={-c.points[0][1] - 3 * units}
                  fontSize={8 * units}
                  fill={color}
                >
                  {c.name} · {c.elevation} m
                </text>
              )}
            </g>
          );
        })}
      {display.terrain &&
        p.terrain_points.map((t) =>
          leaflet ? (
            <CircleMarker
              key={t.id}
              center={geo([t.x, t.y])}
              radius={2}
              pathOptions={{ color: "#997f50", weight: 1 }}
              interactive={!!onSelect}
              bubblingMouseEvents={!onSelect}
              eventHandlers={{click:()=>onSelect?.(t.id)}}
            >
              {display.labels && (
                <Tooltip permanent>{t.elevation.toFixed(1)}</Tooltip>
              )}
            </CircleMarker>
          ) : (
            <g key={t.id} role={onSelect?"button":undefined} aria-label={`Point de terrain ${t.id}`} onClick={e=>{if(onSelect){e.stopPropagation();onSelect(t.id);}}} style={{cursor:onSelect?"pointer":undefined}}>
              <path
                d={`M${t.x - 2 * units} ${-t.y}h${4 * units}m${-2 * units} ${-2 * units}v${4 * units}`}
                stroke="#997f50"
                vectorEffect="non-scaling-stroke"
              />
              {display.labels && (
                <text
                  x={t.x + 2 * units}
                  y={-t.y}
                  fontSize={7 * units}
                  fill="#997f50"
                >
                  {t.elevation.toFixed(1)}
                </text>
              )}
            </g>
          ),
        )}
    </>
  );
}
