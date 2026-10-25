import { useEffect, useMemo, useRef, useState } from "react";
import { Maximize, Minus, Plus } from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
import { saveBytes, saveText, projectDxf } from "../lib/files";
import { styledContours } from "../lib/presentation";
import {
  buildCadDrawing,
  CAD_DRAW_ORDER,
  CAD_LAYERS,
  CAD_SCALES,
  type CadEntity,
  type CadOptions,
  type CadPaper,
} from "../lib/cad";
import "./CadView.css";
import { invoke, isTauri } from "@tauri-apps/api/core";

function DrawingEntity({
  entity: e,
  height,
}: {
  entity: CadEntity;
  height: number;
}) {
  if (e.kind === "image")
    return (
      <image
        x={e.position[0]}
        y={height - e.position[1] - e.height}
        width={e.width}
        height={e.height}
        href={e.url}
        opacity={e.opacity}
        preserveAspectRatio="none"
      />
    );
  if (e.kind === "text")
    return (
      <text
        x={e.position[0]}
        y={height - e.position[1]}
        fontSize={e.height}
        fill={e.color}
        textAnchor={
          e.align === "center"
            ? "middle"
            : e.align === "right"
              ? "end"
              : "start"
        }
        dominantBaseline={
          e.baseline === "middle"
            ? "central"
            : e.baseline === "top"
              ? "hanging"
              : "text-after-edge"
        }
      >
        {e.value}
      </text>
    );
  if (e.kind === "circle")
    return (
      <circle
        cx={e.center[0]}
        cy={height - e.center[1]}
        r={e.radius}
        fill={e.fill ?? "none"}
        stroke={e.color}
        strokeWidth={e.width}
      />
    );
  return (
    <path
      d={
        e.points
          .map(([x, y], i) => `${i ? "L" : "M"}${x} ${height - y}`)
          .join(" ") + (e.closed ? " Z" : "")
      }
      fill={e.fill ?? "none"}
      stroke={e.color}
      strokeWidth={e.width}
      strokeDasharray={e.dash?.join(" ")}
      strokeLinejoin="round"
    />
  );
}
export default function CadView() {
  const a = usePlanner(),
    { project, cutResults, task } = a,
    options: CadOptions = project.cad_options ?? { scale: 1000, paper: "A3" },
    contours = useMemo(
      () => styledContours(project, cutResults),
      [project, cutResults],
    ),
    drawing = useMemo(
      () => buildCadDrawing(project, contours, options),
      [project, contours, options],
    ),
    { width, height } = drawing,
    svg = useRef<SVGSVGElement>(null),
    drag = useRef<{ x: number; y: number; panX: number; panY: number } | null>(
      null,
    ),
    [view, setView] = useState({ zoom: 1, x: 0, y: 0 });
  useEffect(
    () => setView({ zoom: 1, x: 0, y: 0 }),
    [width, height, project.name],
  );
  const update = (changes: Partial<CadOptions>) =>
      a.commit(
        (p) => ({ ...p, cad_options: { ...options, ...changes } }),
        "Mise en page CAO modifiée",
        false,
      ),
    zoom = (factor: number) =>
      setView((v) => ({
        ...v,
        zoom: Math.max(0.25, Math.min(16, v.zoom * factor)),
      })),
    serialize = () => {
      if (!svg.current) return "";
      const copy = svg.current.cloneNode(true) as SVGSVGElement;
      // Navigation affects the viewport, never the saved sheet or its scale.
      copy.setAttribute("viewBox", `0 0 ${width} ${height}`);
      copy.setAttribute("width", `${width}mm`);
      copy.setAttribute("height", `${height}mm`);
      copy.removeAttribute("style");
      return new XMLSerializer().serializeToString(copy);
    };
  async function pdf() {
    if (!svg.current) return false;
    const { jsPDF } = await import("jspdf"),
      pdfDocument = new jsPDF({
        orientation: width > height ? "landscape" : "portrait",
        unit: "mm",
        format: [width, height],
      }),
      blob = new Blob([serialize()], { type: "image/svg+xml" }),
      url = URL.createObjectURL(blob);
    try {
      const image = new Image();
      image.src = url;
      await image.decode();
      const canvas = document.createElement("canvas");
      canvas.width = Math.round(width * 5);
      canvas.height = Math.round(height * 5);
      const context = canvas.getContext("2d")!;
      context.fillStyle = "white";
      context.fillRect(0, 0, canvas.width, canvas.height);
      context.drawImage(image, 0, 0, canvas.width, canvas.height);
      pdfDocument.addImage(
        canvas.toDataURL("image/png"),
        "PNG",
        0,
        0,
        width,
        height,
      );
      return saveBytes(
        `${project.name}-plan.pdf`,
        new Uint8Array(pdfDocument.output("arraybuffer")),
        "application/pdf",
      );
    } finally {
      URL.revokeObjectURL(url);
    }
  }
  const viewWidth = width / view.zoom,
    viewHeight = height / view.zoom;
  return (
    <div className="cad-view cad-original-layout">
      <aside className="cad-controls">
        <fieldset>
          <legend>CAO</legend>
          <div className="button-row cad-export-tools">
            <button
              title="Exporter la feuille et ses calques au format DXF"
              onClick={() =>
                void task(
                  () =>
                    saveText(
                      `${project.name}-plan.dxf`,
                      projectDxf(project, contours, { paper: options }),
                    ),
                  drawing.entities.some((e) => e.kind === "image")
                    ? "Plan DXF exporté (image de fond disponible en SVG/PDF)"
                    : "Plan DXF exporté",
                )
              }
            >
              <img src="/original-icons/UC_CAD.cmdExportToDWG.png" alt="" />
              DXF
            </button>
            <button disabled={!isTauri()} title={isTauri() ? "Exporter la feuille et ses calques au format DWG" : "L’export DWG nécessite l’application desktop"}
              onClick={() => void task(async () => {
                const bytes = await invoke<number[]>("dxf_to_dwg", { dxf: projectDxf(project, contours, { paper: options }) });
                return saveBytes(`${project.name}-plan.dwg`, new Uint8Array(bytes), "application/acad");
              }, "Plan DWG exporté")}>DWG</button>
            <button
              title="Exporter la feuille SVG avec son image de fond"
              onClick={() =>
                void task(
                  () =>
                    saveText(
                      `${project.name}-plan.svg`,
                      serialize(),
                      "image/svg+xml",
                    ),
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
              aria-label="Échelle CAO"
              value={options.scale}
              onChange={(e) => update({ scale: Number(e.target.value) })}
            >
              {CAD_SCALES.map((value) => (
                <option key={value} value={value}>
                  1:{value}
                </option>
              ))}
            </select>
          </label>
          <label>
            Format
            <select
              aria-label="Format CAO"
              value={options.paper}
              onChange={(e) => update({ paper: e.target.value as CadPaper })}
            >
              {["A3", "A4", "A2"].map((value) => (
                <option key={value}>{value}</option>
              ))}
            </select>
          </label>
          <fieldset className="cad-layer-list">
            <legend>Calques</legend>
            {CAD_LAYERS.map(([key, label]) => (
              <label key={key}>
                <input
                  type="checkbox"
                  checked={options.layers?.[key] !== false}
                  onChange={(e) =>
                    update({
                      layers: { ...options.layers, [key]: e.target.checked },
                    })
                  }
                />
                {label}
              </label>
            ))}
          </fieldset>
        </fieldset>
      </aside>
      <div
        className="cad-paper"
        onWheel={(e) => zoom(e.deltaY < 0 ? 1.15 : 1 / 1.15)}
      >
        <div
          className="cad-navigation"
          role="toolbar"
          aria-label="Navigation du plan CAO"
        >
          <button
            aria-label="Agrandir le plan CAO"
            title="Agrandir"
            onClick={() => zoom(1.3)}
          >
            <Plus size={16} />
          </button>
          <button
            aria-label="Réduire le plan CAO"
            title="Réduire"
            onClick={() => zoom(1 / 1.3)}
          >
            <Minus size={16} />
          </button>
          <button
            aria-label="Ajuster la feuille CAO"
            title="Ajuster la feuille"
            onClick={() => setView({ zoom: 1, x: 0, y: 0 })}
          >
            <Maximize size={16} />
          </button>
        </div>
        <svg
          ref={svg}
          xmlns="http://www.w3.org/2000/svg"
          viewBox={`${(width - viewWidth) / 2 + view.x} ${(height - viewHeight) / 2 + view.y} ${viewWidth} ${viewHeight}`}
          aria-label="Plan CAO à l’échelle"
          fontFamily="Arial, sans-serif"
          onPointerDown={(e) => {
            if (e.button !== 0 && e.button !== 1) return;
            e.currentTarget.setPointerCapture(e.pointerId);
            drag.current = {
              x: e.clientX,
              y: e.clientY,
              panX: view.x,
              panY: view.y,
            };
          }}
          onPointerMove={(e) => {
            if (!drag.current) return;
            const bounds = e.currentTarget.getBoundingClientRect(),
              ratio = Math.max(
                viewWidth / bounds.width,
                viewHeight / bounds.height,
              );
            setView((v) => ({
              ...v,
              x: drag.current!.panX - (e.clientX - drag.current!.x) * ratio,
              y: drag.current!.panY - (e.clientY - drag.current!.y) * ratio,
            }));
          }}
          onPointerUp={() => {
            drag.current = null;
          }}
          onPointerCancel={() => {
            drag.current = null;
          }}
          onDoubleClick={() => setView({ zoom: 1, x: 0, y: 0 })}
        >
          <rect width={width} height={height} fill="white" />
          {CAD_DRAW_ORDER.map((layer) => (
            <g key={layer} data-cad-layer={layer}>
              {drawing.entities
                .filter((e) => e.layer === layer)
                .map((entity, i) => (
                  <DrawingEntity key={i} entity={entity} height={height} />
                ))}
            </g>
          ))}
        </svg>
        <span className="cad-view-status">
          {options.paper} · 1:{options.scale} · {Math.round(view.zoom * 100)} %
        </span>
      </div>
    </div>
  );
}
