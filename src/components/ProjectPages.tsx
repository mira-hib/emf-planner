import { useMemo, useState } from "react";
import { FileChartColumn } from "lucide-react";
import {
  Line,
  LineChart,
  CartesianGrid,
  ResponsiveContainer,
  XAxis,
  YAxis,
  Tooltip,
} from "recharts";
import { usePlanner } from "../lib/usePlanner";
import { calculatePoints, exposureAt } from "../lib/engine";
import { rad, terrainHeight } from "../lib/geometry";
import { exportExcel, exportPdf, exportWord } from "../lib/files";
import { mapProviders } from "./PlannerMap";
import DataGrid from "./DataGrid";
import { NumberField } from "./Controls";
import { ResultTable } from "./Results";
export function LimitsPage() {
  const { project, commit } = usePlanner();
  return (
    <div className="limits-page">
      <h1>Limites d’exposition EMF</h1>
      <p className="muted">
        Chaque ligne définit E = coefficient × fréquence<sup>exposant</sup>. Les
        fréquences sont en MHz.
      </p>
      <div className="limits-grid">
        {project.limits.map((limit, li) => (
          <fieldset key={limit.id}>
            <legend style={{ color: limit.color }}>Limites {li + 1}</legend>
            <label>
              Description
              <input
                value={limit.name}
                onChange={(e) =>
                  commit((p) => ({
                    ...p,
                    limits: p.limits.map((l, i) =>
                      i === li ? { ...l, name: e.target.value } : l,
                    ),
                  }))
                }
              />
            </label>
            <label>
              Couleur
              <input
                type="color"
                value={limit.color}
                onChange={(e) =>
                  commit((p) => ({
                    ...p,
                    limits: p.limits.map((l, i) =>
                      i === li ? { ...l, color: e.target.value } : l,
                    ),
                  }))
                }
              />
            </label>
            <DataGrid
              label={`Limites ${li + 1}`}
              rows={limit.rows.map((r, i) => ({ ...r, id: String(i) }))}
              columns={[
                {
                  key: "frequency",
                  label: "Jusqu’à, MHz",
                  kind: "number",
                  min: 0,
                  width: 100,
                },
                {
                  key: "value",
                  label: "Coefficient",
                  kind: "number",
                  min: 0.000001,
                  width: 85,
                },
                {
                  key: "exponent",
                  label: "Exposant",
                  kind: "number",
                  width: 80,
                },
              ]}
              onUpdate={(id, key, value) =>
                commit((p) => ({
                  ...p,
                  limits: p.limits.map((l, i) =>
                    i === li
                      ? {
                          ...l,
                          rows: l.rows.map((r, j) =>
                            j === Number(id) ? { ...r, [key]: value } : r,
                          ),
                        }
                      : l,
                  ),
                }))
              }
              onAdd={() =>
                commit((p) => ({
                  ...p,
                  limits: p.limits.map((l, i) =>
                    i === li
                      ? {
                          ...l,
                          rows: [
                            ...l.rows,
                            { frequency: 100000, value: 28, exponent: 0 },
                          ],
                        }
                      : l,
                  ),
                }))
              }
            />
            <button
              disabled={limit.rows.length < 2}
              onClick={() =>
                commit((p) => ({
                  ...p,
                  limits: p.limits.map((l, i) =>
                    i === li ? { ...l, rows: l.rows.slice(0, -1) } : l,
                  ),
                }))
              }
            >
              Retirer la dernière ligne
            </button>
            {(["use_alpha24", "use_ftdc", "use_fpr"] as const).map((key, i) => (
              <label key={key}>
                <input
                  type="checkbox"
                  checked={limit[key]}
                  onChange={(e) =>
                    commit((p) => ({
                      ...p,
                      limits: p.limits.map((l, j) =>
                        j === li ? { ...l, [key]: e.target.checked } : l,
                      ),
                    }))
                  }
                />{" "}
                Utiliser {["α24", "Ftdc", "Fpr"][i]}
              </label>
            ))}
          </fieldset>
        ))}
      </div>
    </div>
  );
}
export function TablesPage() {
  const { project, results, task } = usePlanner(),
    [height, setHeight] = useState(1.5),
    [angle, setAngle] = useState(60),
    [distance, setDistance] = useState(200),
    [metric, setMetric] = useState("field");
  const profile = useMemo(
    () =>
      Array.from({ length: 101 }, (_, i) => {
        const d = (i * distance) / 100,
          r = exposureAt(
            project,
            d * Math.sin(rad(angle)),
            d * Math.cos(rad(angle)),
            height + terrainHeight(project, 0, 0),
          );
        return {
          distance: d,
          field: r.total_field,
          exposure: r.exposure_ratio * 100,
        };
      }),
    [project, distance, angle, height],
  );
  const rows = useMemo(
    () => (results.length ? results : calculatePoints(project)),
    [project, results],
  );
  return (
    <div className="tables-page">
      <div className="table-chart-controls">
        <h1>Tableaux et graphiques</h1>
        <NumberField label="Hauteur, m" value={height} onChange={setHeight} />
        <NumberField label="Azimut, °" value={angle} onChange={setAngle} />
        <NumberField
          label="Distance max., m"
          value={distance}
          min={1}
          onChange={setDistance}
        />
        <select
          aria-label="Grandeur du graphique"
          value={metric}
          onChange={(e) => setMetric(e.target.value)}
        >
          <option value="field">Champ électrique, V/m</option>
          <option value="exposure">Exposition simultanée, %</option>
        </select>
        <button
          onClick={() =>
            void task(() => exportExcel(project), "Classeur exporté")
          }
        >
          Exporter Excel
        </button>
      </div>
      <div className="graph">
        <ResponsiveContainer width="100%" height="100%">
          <LineChart data={profile}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis
              dataKey="distance"
              type="number"
              label={{
                value: "Distance, m",
                position: "insideBottomRight",
                offset: -4,
              }}
            />
            <YAxis width={65} />
            <Tooltip formatter={(v) => Number(v).toFixed(4)} />
            <Line
              type="linear"
              dataKey={metric}
              name={metric === "field" ? "E, V/m" : "Exposition, %"}
              stroke="#24639c"
              dot={false}
              isAnimationActive={false}
            />
          </LineChart>
        </ResponsiveContainer>
      </div>
      <ResultTable results={rows} />
    </div>
  );
}
export function ReportPage() {
  const { project, commit, results, task } = usePlanner(),
    [content, setContent] = useState("points"),
    [format, setFormat] = useState("pdf");
  const rows = useMemo(
    () => (results.length ? results : calculatePoints(project)),
    [project, results],
  );
  return (
    <div className="report-page">
      <div className="report-controls">
        <h1>Rapport par modèle</h1>
        <label>
          Titre du projet
          <input
            value={project.name}
            onChange={(e) => commit((p) => ({ ...p, name: e.target.value }))}
          />
        </label>
        <label>
          Description
          <textarea
            value={project.description}
            onChange={(e) =>
              commit((p) => ({ ...p, description: e.target.value }))
            }
          />
        </label>
        <label>
          Contenu
          <select value={content} onChange={(e) => setContent(e.target.value)}>
            <option value="points">Résultats des points de contrôle</option>
            <option value="sources">Sources et points de contrôle</option>
          </select>
        </label>
        <label>
          Format
          <select value={format} onChange={(e) => setFormat(e.target.value)}>
            <option value="pdf">PDF</option>
            <option value="word">Word (.docx)</option>
            <option value="excel">Excel (.xlsx)</option>
          </select>
        </label>
        <button
          className="primary-action"
          onClick={() =>
            void task(
              () =>
                format === "word"
                  ? exportWord(project, content === "sources")
                  : format === "excel"
                    ? exportExcel(project, content === "sources")
                    : exportPdf(project, content === "sources"),
              "Rapport exporté",
            )
          }
        >
          <FileChartColumn size={15} /> Générer le rapport
        </button>
        <p className="hint">
          Le rapport utilise les données actuelles et le jeu de limites
          sélectionné.
        </p>
      </div>
      <article className="report-preview">
        <h1>{project.name}</h1>
        <p>{project.description}</p>
        <p>Limites : {project.limits[project.settings.selected_limit].name}</p>
        {content === "sources" && (
          <>
            <h2>Sources rayonnantes</h2>
            <table>
              <thead>
                <tr>
                  <th>Source</th>
                  <th>MHz</th>
                  <th>W</th>
                  <th>dBi</th>
                  <th>m</th>
                </tr>
              </thead>
              <tbody>
                {project.sources.map((s) => (
                  <tr key={s.id}>
                    <td>{s.name}</td>
                    <td>{s.frequency_mhz}</td>
                    <td>{s.power_w}</td>
                    <td>{s.gain_dbi}</td>
                    <td>{s.height_m}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </>
        )}
        <h2>Calculs aux points</h2>
        <ResultTable results={rows} />
      </article>
    </div>
  );
}
export function SettingsPage() {
  const { project, settings } = usePlanner();
  return (
    <div className="settings-page">
      <h1>Paramètres du projet</h1>
      <fieldset>
        <legend>Calculs</legend>
        <NumberField
          label="Coefficient de réflexion"
          value={project.settings.reflection}
          min={0.01}
          max={5}
          onChange={(v) => settings({ reflection: v })}
        />
        <NumberField
          label="Pas grille 1, m"
          value={project.settings.grid_step_m}
          min={0.2}
          onChange={(v) => settings({ grid_step_m: v })}
        />
        <NumberField
          label="Pas grille 2, m"
          value={project.settings.fine_step_m}
          min={0.2}
          onChange={(v) => settings({ fine_step_m: v })}
        />
        <NumberField
          label="Demi-largeur de la zone, m"
          value={project.settings.radius_m}
          min={5}
          max={20000}
          onChange={(v) => settings({ radius_m: v })}
        />
        <NumberField
          label="Atténuation bâtiment, dB"
          value={project.settings.building_attenuation_db}
          min={0}
          onChange={(v) => settings({ building_attenuation_db: v })}
        />
        <label>
          <input
            type="checkbox"
            checked={project.settings.consider_terrain}
            onChange={(e) => settings({ consider_terrain: e.target.checked })}
          />{" "}
          Utiliser le relief
        </label>
      </fieldset>
      <fieldset>
        <legend>Fichiers et raccourcis</legend>
        <p>Ouvrir : .xemf d’origine, .emfp ou JSON du clone.</p>
        <p>Enregistrer : .emfp (format JSON du clone).</p>
        <p>
          Ctrl+O : ouvrir · Ctrl+S : enregistrer · Ctrl+Z / Ctrl+Y : annuler /
          rétablir · Échap : abandonner le dessin.
        </p>
        <p>
          Une copie de travail est conservée localement dans cette application.
        </p>
      </fieldset>
      <fieldset>
        <legend>Affichage</legend>
        <label>
          Fond de carte
          <select
            value={project.settings.map_provider}
            onChange={(e) => settings({ map_provider: e.target.value })}
          >
            {Object.keys(mapProviders(project)).map((n) => (
              <option key={n}>{n}</option>
            ))}
          </select>
        </label>
      </fieldset>
    </div>
  );
}
