import type { RadiatingSource } from "../types";
import { deg, rad } from "../lib/geometry";
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
  const makePath = (
    values: number[] | undefined,
    beamWidth: number | undefined,
  ) =>
    Array.from({ length: 361 }, (_, i) => {
      const loss =
        values?.[i % 360] ??
        Math.min(40, 12 * (Math.min(i, 360 - i) / (beamWidth || 65)) ** 2);
      const r = Math.max(0, 1 - loss / 40) * 83,
        a = rad(i);
      return `${i ? "L" : "M"}${100 + r * Math.cos(a)},${100 + r * Math.sin(a)}`;
    }).join(" ");
  return (
    <svg
      className="polar"
      viewBox="0 0 200 200"
      aria-label={`Diagramme ${vertical ? "vertical" : "horizontal"} de l’antenne`}
    >
      <g fill="none" stroke="#bababa" strokeWidth=".5">
        {[3, 10, 20, 30, 40]
          .map((d) => 83 * (1 - d / 40))
          .map((r) => (
            <circle key={r} cx="100" cy="100" r={r} />
          ))}
        {Array.from({ length: 72 }, (_, i) => {
          const a = rad(i * 5);
          return (
            <line
              key={i}
              x1="100"
              y1="100"
              x2={100 + 83 * Math.cos(a)}
              y2={100 + 83 * Math.sin(a)}
            />
          );
        })}
      </g>
      {[0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330].map((a) => (
        <text
          key={a}
          x={100 + 93 * Math.cos(rad(a))}
          y={103 + 93 * Math.sin(rad(a))}
          fontSize="7"
          fill="#a32d31"
          textAnchor="middle"
        >
          {a}°
        </text>
      ))}
      {source && (
        <path
          d={makePath(
            singleVertical ? source.pattern_v : source.pattern_h,
            width,
          )}
          fill="none"
          stroke={singleVertical ? "#e02530" : "#274fdb"}
          strokeWidth="1.1"
        />
      )}
      {source && both && (
        <path
          d={makePath(source?.pattern_v, source?.beam_width_v_deg)}
          fill="none"
          stroke="#e02530"
          strokeWidth="1.1"
        />
      )}
      {[3, 10, 20, 30].map((d) => (
        <text
          key={d}
          x="102"
          y={100 - 83 * (1 - d / 40)}
          fontSize="6.5"
          fill="#9c6262"
        >
          {d} dB
        </text>
      ))}
      <title>
        {source
          ? `${source.name} — ${vertical ? "V" : "H"}, ${width}°, azimut ${deg(rad(source.azimuth_deg)).toFixed(0)}°`
          : "Aucune antenne sélectionnée"}
      </title>
    </svg>
  );
}
