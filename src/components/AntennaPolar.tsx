import type { RadiatingSource } from "../types";
import { deg, rad } from "../lib/geometry";
import {
  antennaPolarPath,
  POLAR_RADIUS,
  polarRadius,
} from "../lib/antenna-polar";
export default function AntennaPolar({
  source,
  vertical = false,
  both = false,
}: {
  source?: RadiatingSource;
  vertical?: boolean;
  both?: boolean;
}) {
  const singleVertical = vertical && !both;
  const width = singleVertical
    ? source?.beam_width_v_deg
    : source?.beam_width_h_deg;
  return (
    <svg
      className="polar"
      viewBox="0 0 200 200"
      aria-label={`Diagramme ${both ? "horizontal et vertical" : vertical ? "vertical" : "horizontal"} de l’antenne`}
    >
      {source && (
        <path
          data-plane={singleVertical ? "V" : "H"}
          d={antennaPolarPath(
            singleVertical ? source.pattern_v : source.pattern_h,
            width,
          )}
          fill={singleVertical ? "red" : "blue"}
          fillOpacity={60 / 255}
          stroke={singleVertical ? "red" : "blue"}
          strokeWidth="2"
        />
      )}
      {source && both && (
        <path
          data-plane="V"
          d={antennaPolarPath(source?.pattern_v, source?.beam_width_v_deg)}
          fill="red"
          fillOpacity={60 / 255}
          stroke="red"
          strokeWidth="2"
        />
      )}
      <g fill="none" stroke="black" strokeWidth=".8">
        {Array.from({ length: 72 }, (_, i) => {
          const a = rad(i * 5);
          return (
            <line
              key={i}
              x1="100"
              y1="100"
              x2={100 + POLAR_RADIUS * Math.cos(a)}
              y2={100 + POLAR_RADIUS * Math.sin(a)}
              strokeOpacity={i % 6 === 0 ? 0.78 : i % 2 === 0 ? 0.39 : 0.2}
            />
          );
        })}
        {[0, 10, 20].map((loss) => (
          <circle
            key={loss}
            data-loss-db={loss}
            cx="100"
            cy="100"
            r={polarRadius(loss)}
            strokeOpacity=".6"
          />
        ))}
        <circle
          data-loss-db="3"
          cx="100"
          cy="100"
          r={polarRadius(3)}
          stroke="#888"
          strokeDasharray="3 2"
        />
      </g>
      {[0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330].map((a) => (
        <text
          key={a}
          x={100 + 93 * Math.cos(rad(a))}
          y={103 + 93 * Math.sin(rad(a))}
          fontSize="8"
          fill="red"
          textAnchor="middle"
        >
          {a}°
        </text>
      ))}
      {[3, 10, 20, 30].map((d) => (
        <text key={d} x="102" y={100 - polarRadius(d)} fontSize="8" fill="red">
          −{d} dB
        </text>
      ))}
      <title>
        {source
          ? `${source.name} — ${both ? "H + V" : vertical ? "V" : "H"}, ${width}°, azimut ${deg(rad(source.azimuth_deg)).toFixed(0)}° ; échelle : 0 dB au bord, −30 dB au centre`
          : "Aucune antenne sélectionnée"}
      </title>
    </svg>
  );
}
