import { useEffect, useRef, useState } from "react";
import type { LimitSet } from "../types";
import { pasteLimitCells, limitFrequencyWarnings } from "../lib/limit-clipboard";
import { usePlanner } from "../lib/usePlanner";
import {
  addLimitRow,
  commonLimitRows,
  completeLimit,
  editLimitCell,
  EMPTY_LIMIT,
  limitSegments,
  parseLimitCell,
  removeLimitRows,
  type LimitField,
} from "../lib/limits";

function LimitCell({
  value,
  field,
  label,
  onChange,
}: {
  value: number;
  field: LimitField;
  label: string;
  onChange: (value: number) => void;
}) {
  const formatted =
    value === EMPTY_LIMIT ? "" : String(Number(value.toPrecision(7)));
  const [text, setText] = useState(formatted);
  const cancelled = useRef(false);
  useEffect(() => setText(formatted), [formatted]);
  return (
    <input
      aria-label={label}
      inputMode="decimal"
      value={text}
      title={
        field === "frequency"
          ? "0 < F ≤ 1 000 000 MHz"
          : field === "value"
            ? "0 < E ≤ 100 000"
            : "−3 ≤ A ≤ 3"
      }
      onChange={(event) => setText(event.target.value)}
      onBlur={() => {
        if (cancelled.current) {
          cancelled.current = false;
          return;
        }
        if (text === formatted) return;
        const parsed = parseLimitCell(text, field);
        if (parsed === EMPTY_LIMIT) setText("");
        if (parsed !== value) onChange(parsed);
      }}
      onKeyDown={(event) => {
        if (event.key === "Enter") event.currentTarget.blur();
        if (event.key === "Escape") {
          cancelled.current = true;
          setText(formatted);
          event.currentTarget.blur();
        }
      }}
    />
  );
}

function LimitsChart({ limits }: { limits: LimitSet[] }) {
  const x = (f: number) => 48 + (Math.log10(f) - 1) * 108;
  const y = (e: number) => 270 - Math.log10(e) * 80;
  return (
    <figure className="limits-chart">
      <figcaption>Limites d’exposition selon la fréquence</figcaption>
      <svg
        viewBox="0 0 510 310"
        role="img"
        aria-label="Graphique logarithmique des quatre limites EMF"
      >
        <defs>
          <clipPath id="limits-plot-clip">
            <rect x="48" y="30" width="432" height="240" />
          </clipPath>
        </defs>
        <rect
          x="48"
          y="30"
          width="432"
          height="240"
          fill="white"
          stroke="#8998a5"
        />
        {Array.from({ length: 4 }, (_, decade) =>
          Array.from({ length: 9 }, (_, i) => {
            const f = (i + 1) * 10 ** (decade + 1);
            return (
              <line
                key={`x-${decade}-${i}`}
                x1={x(f)}
                x2={x(f)}
                y1="30"
                y2="270"
                stroke="#e0e5e9"
              />
            );
          }),
        )}
        {Array.from({ length: 3 }, (_, decade) =>
          Array.from({ length: 9 }, (_, i) => {
            const e = (i + 1) * 10 ** decade;
            return (
              <line
                key={`y-${decade}-${i}`}
                x1="48"
                x2="480"
                y1={y(e)}
                y2={y(e)}
                stroke="#e0e5e9"
              />
            );
          }),
        )}
        {[10, 100, 1000, 10000, 100000].map((f, i) => (
          <text key={f} x={x(f)} y="290" textAnchor="middle">
            {["10 MHz", "100 MHz", "1 GHz", "10 GHz", "100 GHz"][i]}
          </text>
        ))}
        {[1, 10, 100, 1000].map((e) => (
          <text key={e} x="40" y={y(e) + 4} textAnchor="end">
            {e}
          </text>
        ))}
        <text x="48" y="18">
          E, V/m
        </text>
        <g clipPath="url(#limits-plot-clip)">
          {limits.map((limit, li) => (
            <g key={limit.id} data-limit-curve={li + 1}>
              {limitSegments(limit).map(([f1, e1, f2, e2], i) => (
                <line
                  key={i}
                  x1={x(f1)}
                  y1={y(e1)}
                  x2={x(f2)}
                  y2={y(e2)}
                  stroke={limit.color}
                  strokeWidth="2.5"
                >
                  <title>{`Limites ${li + 1} : ${f1}–${f2} MHz`}</title>
                </line>
              ))}
            </g>
          ))}
        </g>
      </svg>
      <div className="limits-chart-legend">
        {limits.map((limit, i) => (
          <span key={limit.id}>
            <i style={{ backgroundColor: limit.color }} />
            {i + 1}
          </span>
        ))}
      </div>
    </figure>
  );
}

export function LimitsPage() {
  const { project, commit, fail } = usePlanner();
  const activeCell = useRef({ row: 0, column: 0 });
  function pasteCells(text: string) {
    try { const limits = pasteLimitCells(project.limits, text, activeCell.current.row, activeCell.current.column); commit((p) => ({ ...p, limits })); }
    catch (reason) { fail(reason); }
  }
  const [selected, setSelected] = useState<number[]>([]);
  const anchor = useRef(0);
  const limits = project.limits;
  const common = commonLimitRows(limits);
  const count = common[0]?.length ?? 0;
  const validSelection = selected.filter((i) => i < count);
  const blank = (limit: LimitSet) =>
    !limit.rows.length ||
    limit.rows.every(
      (row) => row.value === EMPTY_LIMIT && row.exponent === EMPTY_LIMIT,
    );
  function removeSelected() {
    if (!validSelection.length) return;
    commit((p) => ({
      ...p,
      limits: removeLimitRows(p.limits, validSelection),
    }));
    setSelected([]);
  }
  return (
    <div className="limits-page">
      <h1>Limites d’exposition EMF</h1>
      <p className="muted">
        E = coefficient × F<sup>exposant</sup> ; F en MHz, E en V/m. Chaque
        fréquence termine un intervalle.
      </p>
      <div className="limits-overview">
        <fieldset className="limits-properties">
          <legend>Descriptions et facteurs des quatre jeux</legend>
          <table>
            <thead>
              <tr>
                <th>Jeu</th>
                <th>Description</th>
                <th>α24</th>
                <th>Ftdc</th>
                <th>Fpr</th>
              </tr>
            </thead>
            <tbody>
              {limits.map((limit, li) => (
                <tr key={limit.id}>
                  <th>
                    <label className="limit-color">
                      <span>{li + 1}</span>
                      <input
                        type="color"
                        aria-label={`Couleur limites ${li + 1}`}
                        value={limit.color}
                        onChange={(event) =>
                          commit((p) => ({
                            ...p,
                            limits: p.limits.map((l, i) =>
                              i === li
                                ? { ...l, color: event.target.value }
                                : l,
                            ),
                          }))
                        }
                      />
                    </label>
                  </th>
                  <td>
                    <textarea
                      aria-label={`Description limites ${li + 1}`}
                      value={limit.name}
                      rows={2}
                      onChange={(event) =>
                        commit((p) => ({
                          ...p,
                          limits: p.limits.map((l, i) =>
                            i === li ? { ...l, name: event.target.value } : l,
                          ),
                        }))
                      }
                    />
                  </td>
                  {(["use_alpha24", "use_ftdc", "use_fpr"] as const).map(
                    (key, k) => (
                      <td key={key}>
                        <input
                          type="checkbox"
                          aria-label={`Utiliser ${["α24", "Ftdc", "Fpr"][k]}`}
                          checked={limit[key]}
                          title={`Limites ${li + 1} — ${["α24", "Ftdc", "Fpr"][k]}`}
                          onChange={(event) =>
                            commit((p) => ({
                              ...p,
                              limits: p.limits.map((l, i) =>
                                i === li
                                  ? { ...l, [key]: event.target.checked }
                                  : l,
                              ),
                            }))
                          }
                        />
                      </td>
                    ),
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </fieldset>
        <LimitsChart limits={limits} />
      </div>
      <div className="limits-table-toolbar">
        <strong>Tableau des limites</strong>
        <button
          onClick={() => {
            commit((p) => ({ ...p, limits: addLimitRow(p.limits) }));
            setSelected([count]);
          }}
        >
          Ajouter un intervalle
        </button>
        <button disabled={!validSelection.length} onClick={removeSelected}>
          Supprimer les intervalles sélectionnés
        </button>
        <span className="muted">
          Ctrl/Maj : sélection multiple · Suppr : supprimer
        </span>
      </div>
      <div
        className="data-grid limits-table"
        role="region"
        aria-label="Tableau commun des limites EMF"
        tabIndex={0}
        onPaste={(event) => { const text = event.clipboardData.getData("text/plain"); if (text) { event.preventDefault(); pasteCells(text); } }}
        onKeyDown={(event) => {
          if (event.shiftKey && event.key === "Insert" && navigator.clipboard?.readText) { event.preventDefault(); void navigator.clipboard.readText().then(pasteCells); return; }
          if (event.target instanceof HTMLInputElement) return;
          if (event.key === "Delete") {
            event.preventDefault();
            removeSelected();
          }
          if (
            (event.ctrlKey || event.metaKey) &&
            event.key.toLowerCase() === "a"
          ) {
            event.preventDefault();
            setSelected(Array.from({ length: count }, (_, i) => i));
          }
        }}
      >
        <table>
          <thead>
            <tr>
              <th rowSpan={2}>N°</th>
              <th rowSpan={2}>Jusqu’à F, MHz</th>
              {limits.map((limit, i) => (
                <th key={limit.id} colSpan={2} style={{ color: limit.color }}>
                  Limites {i + 1}
                </th>
              ))}
            </tr>
            <tr>
              {limits.map((limit) => (
                <ColumnHeaders key={limit.id} />
              ))}
            </tr>
          </thead>
          <tbody>
            {Array.from({ length: count }, (_, ri) => (
              <tr
                key={ri}
                aria-selected={validSelection.includes(ri)}
                className={validSelection.includes(ri) ? "selected" : ""}
              >
                <th scope="row">
                  <button
                    aria-label={`Sélectionner l’intervalle ${ri + 1}`}
                    onClick={(event) => {
                      if (event.shiftKey)
                        setSelected(
                          Array.from(
                            { length: Math.abs(ri - anchor.current) + 1 },
                            (_, i) => Math.min(ri, anchor.current) + i,
                          ),
                        );
                      else if (event.ctrlKey || event.metaKey) {
                        setSelected((v) =>
                          v.includes(ri)
                            ? v.filter((i) => i !== ri)
                            : [...v, ri],
                        );
                        anchor.current = ri;
                      } else {
                        setSelected([ri]);
                        anchor.current = ri;
                      }
                    }}
                  >
                    {ri + 1}
                  </button>
                </th>
                <td onFocus={() => { activeCell.current = { row: ri, column: 0 }; }}>
                  <LimitCell
                    value={common[0][ri].frequency}
                    field="frequency"
                    label={`Fréquence intervalle ${ri + 1}, MHz`}
                    onChange={(value) =>
                      commit((p) => ({
                        ...p,
                        limits: editLimitCell(
                          p.limits,
                          ri,
                          0,
                          "frequency",
                          value,
                        ),
                      }))
                    }
                  />
                </td>
                {limits.map((limit, li) =>
                  (["value", "exponent"] as const).map((field) => (
                    <td key={`${limit.id}-${field}`} onFocus={() => { activeCell.current = { row: ri, column: 1 + li * 2 + (field === "exponent" ? 1 : 0) }; }}>
                      <LimitCell
                        value={common[li][ri][field]}
                        field={field}
                        label={`${field === "value" ? "Coefficient" : "Exposant"} limites ${li + 1}, intervalle ${ri + 1}`}
                        onChange={(value) =>
                          commit((p) => ({
                            ...p,
                            limits: editLimitCell(
                              p.limits,
                              ri,
                              li,
                              field,
                              value,
                            ),
                          }))
                        }
                      />
                    </td>
                  )),
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <p className="limits-state" aria-label="État des quatre jeux de limites">
        {limits.map((limit, i) => (
          <span key={limit.id}>
            <i style={{ backgroundColor: limit.color }} />
            Jeu {i + 1} :{" "}
            {completeLimit(limit)
              ? "défini"
              : blank(limit)
                ? "non défini"
                : "incomplet — courbe indisponible"}
          </span>
        ))}
      </p>
      {limitFrequencyWarnings(limits).map((message) => <p role="alert" key={message}>{message}</p>)}
      <p className="muted limits-preset-note">
        Les préréglages reproduisent les valeurs historiques d’EMFPlanner 7.1.
        Ils remplacent les quatre descriptions et tableaux ; couleurs et
        facteurs sont conservés.
      </p>
    </div>
  );
}

function ColumnHeaders() {
  return (
    <>
      <th>Coefficient E</th>
      <th>Exposant A</th>
    </>
  );
}
