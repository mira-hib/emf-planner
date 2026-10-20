import { useRef, useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import { saveBytes, saveText, projectDxf } from "../lib/files";
import { dashPattern, styledContours } from "../lib/presentation";
export default function CadView() {
  const { project, cutResults, task } = usePlanner(),
    [scale, setScale] = useState(1000),
    [size, setSize] = useState("A3"),
    [layers, setLayers] = useState({
      frame: true,
      contours: true,
      buildings: true,
      lines: true,
      sources: true,
      points: true,
      grid: true,
    }),
    svg = useRef<SVGSVGElement>(null);
  const width = size === "A4" ? 297 : size === "A2" ? 594 : 420,
    height = size === "A4" ? 210 : size === "A2" ? 420 : 297,
    unit = 1000 / scale;
  async function pdf() {
    if (!svg.current) return false;
    const { jsPDF } = await import("jspdf"),
      pdf = new jsPDF({
        orientation: "landscape",
        unit: "mm",
        format: size.toLowerCase(),
      });
    const blob = new Blob(
        [new XMLSerializer().serializeToString(svg.current)],
        { type: "image/svg+xml" },
      ),
      url = URL.createObjectURL(blob);
    try {
      const image = new Image();
      image.src = url;
      await image.decode();
      const canvas = document.createElement("canvas");
      canvas.width = width * 5;
      canvas.height = height * 5;
      const c = canvas.getContext("2d")!;
      c.fillStyle = "white";
      c.fillRect(0, 0, canvas.width, canvas.height);
      c.drawImage(image, 0, 0, canvas.width, canvas.height);
      pdf.addImage(canvas.toDataURL("image/png"), "PNG", 0, 0, width, height);
      return saveBytes(
        `${project.name}-plan.pdf`,
        new Uint8Array(pdf.output("arraybuffer")),
        "application/pdf",
      );
    } finally {
      URL.revokeObjectURL(url);
    }
  }
  return (
    <div className="cad-view">
      <aside className="cad-controls">
        <div className="button-row">
          <button
            onClick={() =>
              void task(
                () =>
                  saveText(
                    `${project.name}.dxf`,
                    projectDxf(
                      {
                        ...project,
                        sources: layers.sources ? project.sources : [],
                        control_points: layers.points
                          ? project.control_points
                          : [],
                        buildings: layers.buildings ? project.buildings : [],
                        lines: layers.lines ? project.lines : [],
                      },
                      layers.contours
                        ? styledContours(project, cutResults)
                        : [],
                    ),
                  ),
                "Plan DXF exporté",
              )
            }
          >
            DXF
          </button>
          <button
            onClick={() =>
              void task(
                () =>
                  svg.current
                    ? saveText(
                        `${project.name}-plan.svg`,
                        new XMLSerializer().serializeToString(svg.current),
                        "image/svg+xml",
                      )
                    : Promise.resolve(false),
                "Plan SVG exporté",
              )
            }
          >
            SVG
          </button>
          <button onClick={() => void task(pdf, "Plan PDF exporté")}>
            PDF
          </button>
        </div>
        <label>
          Échelle
          <select
            value={scale}
            onChange={(e) => setScale(Number(e.target.value))}
          >
            {[200, 500, 1000, 2000, 5000].map((v) => (
              <option key={v} value={v}>
                1:{v}
              </option>
            ))}
          </select>
        </label>
        <label>
          Format
          <select value={size} onChange={(e) => setSize(e.target.value)}>
            {["A4", "A3", "A2"].map((v) => (
              <option key={v}>{v}</option>
            ))}
          </select>
        </label>
        <fieldset>
          <legend>Calques</legend>
          {(Object.keys(layers) as (keyof typeof layers)[]).map((key, i) => (
            <label key={key}>
              <input
                type="checkbox"
                checked={layers[key]}
                onChange={(e) =>
                  setLayers((l) => ({ ...l, [key]: e.target.checked }))
                }
              />
              {
                [
                  "Cadre",
                  "Contours EMF",
                  "Bâtiments",
                  "Lignes et annotations",
                  "Antennes",
                  "Points de contrôle",
                  "Grille",
                ][i]
              }
            </label>
          ))}
        </fieldset>
      </aside>
      <div className="cad-paper">
        <svg
          ref={svg}
          xmlns="http://www.w3.org/2000/svg"
          viewBox={`0 0 ${width} ${height}`}
          aria-label="Plan CAO à l’échelle"
        >
          <rect width={width} height={height} fill="white" />
          {layers.frame && (
            <>
              <rect
                x="10"
                y="10"
                width={width - 20}
                height={height - 20}
                fill="none"
                stroke="#222"
                strokeWidth=".4"
              />
              <rect
                x={width - 140}
                y={height - 35}
                width="130"
                height="25"
                fill="white"
                stroke="#222"
                strokeWidth=".3"
              />
              <text x={width - 137} y={height - 27} fontSize="4" fill="#222">
                {project.name}
              </text>
              <text x={width - 137} y={height - 19} fontSize="3.2" fill="#222">
                EMF Planner · {size} · 1:{scale} · Unité : m
              </text>
            </>
          )}
          <defs>
            <clipPath id="drawing-clip">
              <rect x="12" y="12" width={width - 24} height={height - 50} />
            </clipPath>
          </defs>
          <g clipPath="url(#drawing-clip)">
            <g
              transform={`translate(${width / 2} ${height / 2}) scale(${unit} ${-unit})`}
              strokeWidth={0.3 / unit}
            >
              {layers.grid &&
                Array.from({ length: 21 }, (_, i) => (i - 10) * 25).map((v) => (
                  <g key={v} stroke="#ccc" strokeWidth={0.1 / unit}>
                    <line x1={v} y1={-250} x2={v} y2={250} />
                    <line x1={-250} y1={v} x2={250} y2={v} />
                  </g>
                ))}
              {layers.buildings &&
                project.buildings.map((b) => (
                  <polygon
                    key={b.id}
                    points={b.points.map((p) => p.join(",")).join(" ")}
                    fill="#eee"
                    stroke="#222"
                  />
                ))}
              {layers.lines &&
                project.lines
                  .filter((l) => l.kind !== "label")
                  .map((l) => (
                    <polyline
                      key={l.id}
                      points={l.points.map((p) => p.join(",")).join(" ")}
                      fill="none"
                      stroke={l.color}
                    />
                  ))}
              {layers.contours &&
                styledContours(project, cutResults).map((contour, i) => (
                  <path
                    key={i}
                    d={contour.segments
                      .map((s) => `M${s[0].join(",")}L${s[1].join(",")}`)
                      .join(" ")}
                    fill="none"
                    stroke={contour.color}
                    strokeDasharray={dashPattern(contour.line_style)}
                  />
                ))}
              {layers.sources &&
                project.sources.map((s) => (
                  <circle
                    key={s.id}
                    cx={s.x_m ?? 0}
                    cy={s.y_m ?? 0}
                    r={1 / unit}
                    fill="#427845"
                  />
                ))}
              {layers.points &&
                project.control_points.map((p) => (
                  <circle
                    key={p.id}
                    cx={p.x_m}
                    cy={p.y_m}
                    r={0.7 / unit}
                    stroke="#222"
                    fill="white"
                  />
                ))}
            </g>
          </g>
        </svg>
      </div>
    </div>
  );
}
