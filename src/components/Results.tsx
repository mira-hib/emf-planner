import { useEffect, useRef } from "react";
import { X } from "lucide-react";
import type { PointResult } from "../types";
import { usePlanner } from "../lib/usePlanner";
import { exportExcel, exportPdf, exportWord } from "../lib/files";
export function ResultTable({ results }: { results: PointResult[] }) {
  return (
    <div className="result-table">
      <table>
        <thead>
          <tr>
            <th>Point</th>
            <th>X, m</th>
            <th>Y, m</th>
            <th>Hauteur, m</th>
            <th>Champ E, V/m</th>
            <th>Exposition, %</th>
            <th>S, W/m²</th>
          </tr>
        </thead>
        <tbody>
          {results.map((r, i) => (
            <tr key={i}>
              <td>{r.name}</td>
              <td>{r.x.toFixed(1)}</td>
              <td>{r.y.toFixed(1)}</td>
              <td>{r.height_m}</td>
              <td>{r.total_field.toFixed(6)}</td>
              <td>{(r.exposure_ratio * 100).toFixed(6)}</td>
              <td>{r.total_power_density.toFixed(6)}</td>
            </tr>
          ))}
        </tbody>
      </table>
      {!results.length && (
        <div className="grid-empty">
          Ajoutez des points de contrôle pour afficher leurs résultats.
        </div>
      )}
    </div>
  );
}
export function ResultsDialog() {
  const { project, results, setResultOpen, task } = usePlanner(),
    ref = useRef<HTMLElement>(null);
  useEffect(() => {
    const previous = document.activeElement as HTMLElement | null;
    ref.current?.focus();
    return () => previous?.focus();
  }, []);
  return (
    <div className="modal-backdrop">
      <section
        ref={ref}
        tabIndex={-1}
        role="dialog"
        aria-modal="true"
        aria-label="Résultats du calcul aux points"
        className="results-dialog"
        onKeyDown={(e) => {
          if (e.key === "Tab") {
            const elements =
                ref.current?.querySelectorAll<HTMLButtonElement>("button"),
              first = elements?.[0],
              last = elements?.[elements.length - 1];
            if (e.shiftKey && document.activeElement === first) {
              e.preventDefault();
              last?.focus();
            } else if (!e.shiftKey && document.activeElement === last) {
              e.preventDefault();
              first?.focus();
            }
          }
        }}
      >
        <header>
          <span>Résultats du calcul aux points de contrôle</span>
          <button
            aria-label="Fermer les résultats"
            onClick={() => setResultOpen(false)}
          >
            <X size={16} />
          </button>
        </header>
        <div className="section-tools">
          <button
            onClick={() =>
              void task(() => exportExcel(project), "Classeur Excel exporté")
            }
          >
            Excel
          </button>
          <button
            onClick={() =>
              void task(() => exportWord(project), "Rapport Word exporté")
            }
          >
            Word
          </button>
          <button
            onClick={() =>
              void task(() => exportPdf(project), "Rapport PDF exporté")
            }
          >
            PDF
          </button>
        </div>
        <div className="result-matrix">
          <table>
            <thead>
              <tr>
                <th>Source / Point de contrôle</th>
                {results.map((r, i) => (
                  <th key={i}>{r.name}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {(
                [
                  ["Hauteur, m", (r) => r.height_m.toFixed(1)],
                  ["Altitude du terrain, m", (r) => r.elevation.toFixed(1)],
                  ["E mesuré, V/m", (r) => r.measured.toFixed(4)],
                  ["E calculé, V/m", (r) => r.total_field.toFixed(6)],
                  [
                    "Exposition simultanée, %",
                    (r) => (r.exposure_ratio * 100).toFixed(6),
                  ],
                  [
                    "Densité équivalente, W/m²",
                    (r) => r.total_power_density.toFixed(6),
                  ],
                ] as [string, (r: PointResult) => string][]
              ).map(([name, fn]) => (
                <tr key={name}>
                  <th>{name}</th>
                  {results.map((r, i) => (
                    <td key={i}>{fn(r)}</td>
                  ))}
                </tr>
              ))}
              {project.sources
                .filter((s) => s.active !== false)
                .map((s) => (
                  <tr key={s.id} style={{ background: s.color }}>
                    <th>E pour {s.name}, V/m</th>
                    {results.map((r, i) => (
                      <td key={i}>
                        {r.per_source
                          .find((c) => c.source_id === s.id)
                          ?.field_v_m.toFixed(6) ?? "—"}
                      </td>
                    ))}
                  </tr>
                ))}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
}
