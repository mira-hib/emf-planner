import { useEffect, useRef } from "react";
import type { Contour, FieldGrid } from "../types";
import { contours } from "../lib/engine";
export default function FieldPlot({
  grid,
  label = "Coupe verticale",
  overlays,
  legend,
}: {
  grid?: FieldGrid;
  label?: string;
  overlays?: Contour[];
  legend?: { name: string; color: string }[];
}) {
  const canvas = useRef<HTMLCanvasElement>(null);
  useEffect(() => {
    if (!grid || !canvas.current) return;
    const context = canvas.current.getContext("2d");
    if (!context) return;
    const image = context.createImageData(grid.width, grid.height);
    grid.values.forEach((v, i) => {
      const idx =
          ((grid.height - 1 - Math.floor(i / grid.width)) * grid.width +
            (i % grid.width)) *
          4,
        t = Math.min(1, Math.max(0, v)),
        color =
          v >= 1
            ? [218, 42, 70]
            : t > 0.5
              ? [244, 173, 67]
              : t > 0.1
                ? [99, 162, 218]
                : [224, 240, 246];
      image.data[idx] = color[0];
      image.data[idx + 1] = color[1];
      image.data[idx + 2] = color[2];
      image.data[idx + 3] = v < 0.01 ? 0 : Math.round(30 + t * 170);
    });
    context.putImageData(image, 0, 0);
  }, [grid]);
  const lines = overlays?.length
    ? overlays
    : grid
      ? [
          { level: 1, color: "#c02148" },
          { level: 0.5, color: "#dd973c" },
          { level: 0.1, color: "#275bd4" },
        ].map((l) => contours(grid, l.level, l.color))
      : [];
  return (
    <div className="field-plot">
      <div className="plot-title">{label}</div>
      {grid ? (
        <>
          <div className="plot-area">
            <canvas
              ref={canvas}
              width={grid.width}
              height={grid.height}
              aria-label={label}
            />
            <svg
              viewBox={`${grid.x0} ${-grid.y0 - grid.dy * (grid.height - 1)} ${grid.dx * (grid.width - 1)} ${grid.dy * (grid.height - 1)}`}
              preserveAspectRatio="none"
            >
              {lines.map((c, i) => (
                <path
                  key={i}
                  d={c.segments
                    .map(
                      (s) => `M${s[0][0]} ${-s[0][1]}L${s[1][0]} ${-s[1][1]}`,
                    )
                    .join(" ")}
                  fill="none"
                  stroke={c.color}
                  strokeWidth="1.3"
                  vectorEffect="non-scaling-stroke"
                />
              ))}
            </svg>
            <div className="plot-axis-y">H, m</div>
            <div className="plot-axis-x">Distance, m</div>
            <span className="plot-top">
              {(grid.y0 + grid.dy * (grid.height - 1)).toFixed(0)}
            </span>
            <span className="plot-bottom">0</span>
            <span className="plot-left">{grid.x0.toFixed(0)}</span>
            <span className="plot-right">
              {(grid.x0 + grid.dx * (grid.width - 1)).toFixed(0)}
            </span>
          </div>
          <div className="plot-legend">
            {(
              legend ?? [
                { name: "100 %", color: "#c02148" },
                { name: "50 %", color: "#dd973c" },
                { name: "10 %", color: "#275bd4" },
              ]
            ).map((l, i) => (
              <span key={i} style={{ color: l.color }}>
                ― {l.name}
              </span>
            ))}
          </div>
        </>
      ) : (
        <div className="empty-workspace">
          <span>╱╲</span>
          <p>Sélectionnez une coupe et lancez le calcul.</p>
        </div>
      )}
    </div>
  );
}
