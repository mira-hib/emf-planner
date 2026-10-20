import type { RadiatingSource } from "../types";
import { deg, rad } from "../lib/geometry";
export default function AntennaPolar({
  source,
  vertical = false,
}: {
  source?: RadiatingSource;
  vertical?: boolean;
}) {
  const values = vertical ? source?.pattern_v : source?.pattern_h,
    width = vertical ? source?.beam_width_v_deg : source?.beam_width_h_deg;
  const path = Array.from({ length: 361 }, (_, i) => {
    const loss =
      values?.[i % 360] ??
      Math.min(30, 12 * (Math.min(i, 360 - i) / (width || 65)) ** 2);
    const r = Math.max(0, 1 - loss / 40) * 83,
      a = rad(i + (vertical ? 0 : (source?.azimuth_deg ?? 0)) - 90);
    return `${i ? "L" : "M"}${100 + r * Math.cos(a)},${100 + r * Math.sin(a)}`;
  }).join(" ");
  return (
    <svg
      className="polar"
      viewBox="0 0 200 200"
      aria-label={`Diagramme ${vertical ? "vertical" : "horizontal"} de l’antenne`}
    >
      <g fill="none" stroke="#bababa" strokeWidth=".5">
        {[21, 42, 63, 83].map((r) => (
          <circle key={r} cx="100" cy="100" r={r} />
        ))}
        {Array.from({ length: 24 }, (_, i) => {
          const a = rad(i * 15);
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
          x={100 + 93 * Math.sin(rad(a))}
          y={103 - 93 * Math.cos(rad(a))}
          fontSize="7"
          fill="#a32d31"
          textAnchor="middle"
        >
          {a}°
        </text>
      ))}
      <path d={path} fill="#6b9adb22" stroke="#e02530" strokeWidth="1.2" />
      <text x="101" y="104" fontSize="7" fill="#8a3333">
        −40 dB
      </text>
      <title>
        {source
          ? `${source.name} — ${vertical ? "V" : "H"}, ${width}°, azimut ${deg(rad(source.azimuth_deg)).toFixed(0)}°`
          : "Aucune antenne sélectionnée"}
      </title>
    </svg>
  );
}
