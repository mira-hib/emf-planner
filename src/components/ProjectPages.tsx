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
  const { project, results, task, verticalResults } = usePlanner(),
    [height, setHeight] = useState(1.5),
    [angle, setAngle] = useState(60),
    [distance, setDistance] = useState(200),
    [metric, setMetric] = useState("field"),
    [tab, setTab] = useState<"points" | "vfield" | "vexposure">("points");
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
  const vcutFieldRows = useMemo(
    () =>
      verticalResults.flatMap((r) => {
        const cut = project.vertical_cuts.find((c) => c.id === r.cut_id);
        const g = r.grid;
        if (!g.fields.length) return [];
        return Array.from(
          { length: Math.min(g.width * g.height, 500) },
          (_, idx) => {
            const col = idx % g.width;
            const row = Math.floor(idx / g.width);
            return {
              id: `${r.id}-${idx}`,
              azimut: cut?.azimuth_deg.toFixed(1) ?? "?",
              distance_m: (g.x0 + col * g.dx).toFixed(1),
              height_m: (g.y0 + row * g.dy).toFixed(1),
              field: g.fields[idx]?.toFixed(4) ?? "-",
              exposure: ((g.values[idx] ?? 0) * 100).toFixed(2),
            };
          },
        );
      }),
    [verticalResults, project.vertical_cuts],
  );
  return (
    <div className="tables-page">
      <div className="table-chart-controls">
        <h1>Tableaux et graphiques</h1>
        <div className="tab-bar" role="tablist">
          <button
            role="tab"
            aria-selected={tab === "points"}
            className={tab === "points" ? "active" : ""}
            onClick={() => setTab("points")}
          >
            Points de controle
          </button>
          <button
            role="tab"
            aria-selected={tab === "vfield"}
            className={tab === "vfield" ? "active" : ""}
            onClick={() => setTab("vfield")}
            disabled={!verticalResults.some((r) => r.grid.fields.length)}
          >
            Champ coupes V
          </button>
          <button
            role="tab"
            aria-selected={tab === "vexposure"}
            className={tab === "vexposure" ? "active" : ""}
            onClick={() => setTab("vexposure")}
            disabled={!verticalResults.some((r) => r.grid.values.length)}
          >
            Exposition coupes V
          </button>
        </div>
        {tab === "points" && (
          <>
            <NumberField
              label="Hauteur, m"
              value={height}
              onChange={setHeight}
            />
            <NumberField
              label="Azimut, deg"
              value={angle}
              onChange={setAngle}
            />
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
              <option value="field">Champ electrique, V/m</option>
              <option value="exposure">Exposition simultanee, %</option>
            </select>
          </>
        )}
        <button
          onClick={() =>
            void task(() => exportExcel(project), "Classeur exporte")
          }
        >
          Exporter Excel
        </button>
      </div>
      {tab === "points" ? (
        <>
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
        </>
      ) : (
        <div className="vcut-table">
          <DataGrid
            label={
              tab === "vfield"
                ? "Champ electrique des coupes verticales"
                : "Exposition des coupes verticales"
            }
            rows={vcutFieldRows}
            columns={
              tab === "vfield"
                ? [
                    { key: "azimut", label: "Azimut, deg", width: 90 },
                    { key: "distance_m", label: "Distance, m", width: 100 },
                    { key: "height_m", label: "Hauteur, m", width: 90 },
                    { key: "field", label: "E, V/m", width: 120 },
                  ]
                : [
                    { key: "azimut", label: "Azimut, deg", width: 90 },
                    { key: "distance_m", label: "Distance, m", width: 100 },
                    { key: "height_m", label: "Hauteur, m", width: 90 },
                    { key: "exposure", label: "Exposition, %", width: 120 },
                  ]
            }
          />
        </div>
      )}
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
  const { project, settings, commit } = usePlanner();
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
          Mode bâtiments
          <select
            value={project.settings.buildings_mode}
            onChange={(e) =>
              settings({
                buildings_mode: e.target.value as
                  "ignore" | "screen" | "attenuate",
              })
            }
          >
            <option value="ignore">Ignorer</option>
            <option value="screen">Écrantage</option>
            <option value="attenuate">Atténuation</option>
          </select>
        </label>
        <label>
          <input
            type="checkbox"
            checked={project.settings.consider_terrain}
            onChange={(e) => settings({ consider_terrain: e.target.checked })}
          />{" "}
          Utiliser le relief
        </label>
        <label>
          Interpolation du terrain
          <select
            value={project.settings.terrain_interpolation ?? "delaunay"}
            onChange={(e) =>
              settings({
                terrain_interpolation: e.target.value as
                  "original" | "delaunay",
              })
            }
          >
            <option value="delaunay">Delaunay</option>
            <option value="original">Original (linéaire)</option>
          </select>
        </label>
        <label>
          Jeu de limites actif
          <select
            value={project.settings.selected_limit}
            onChange={(e) =>
              settings({ selected_limit: Number(e.target.value) })
            }
          >
            {project.limits.map((l, i) => (
              <option key={l.id} value={i}>
                {l.name || `Limites ${i + 1}`}
              </option>
            ))}
          </select>
        </label>
      </fieldset>
      <fieldset>
        <legend>Contours</legend>
        <NumberField
          label="Épaisseur des contours, px"
          value={project.settings.contour_width ?? 2}
          min={0.5}
          max={10}
          onChange={(v) => settings({ contour_width: v })}
        />
        <NumberField
          label="Opacité des contours, %"
          value={(project.settings.contour_opacity ?? 1) * 100}
          min={10}
          max={100}
          onChange={(v) => settings({ contour_opacity: v / 100 })}
        />
      </fieldset>
      <fieldset>
        <legend>Coordonnées</legend>
        <label>
          Format des coordonnées
          <select
            value={project.settings.coordinate_format ?? "decimal"}
            onChange={(e) =>
              settings({
                coordinate_format: e.target.value as "decimal" | "dms" | "dm",
              })
            }
          >
            <option value="decimal">Degrés décimaux (DD)</option>
            <option value="dms">Degrés / minutes / secondes (DMS)</option>
            <option value="dm">Degrés / minutes (DM)</option>
          </select>
        </label>
      </fieldset>
      <fieldset>
        <legend>Propriétaires</legend>
        <p className="hint">
          Liste des propriétaires de sites, utilisée dans les sources.
        </p>
        {(project.owner_names ?? []).map((name, i) => (
          <label key={i}>
            <input
              value={name}
              onChange={(e) =>
                commit((p) => ({
                  ...p,
                  owner_names: (p.owner_names ?? []).map((n, j) =>
                    j === i ? e.target.value : n,
                  ),
                }))
              }
            />
            <button
              onClick={() =>
                commit((p) => ({
                  ...p,
                  owner_names: (p.owner_names ?? []).filter((_, j) => j !== i),
                }))
              }
            >
              ×
            </button>
          </label>
        ))}
        <button
          onClick={() =>
            commit((p) => ({
              ...p,
              owner_names: [...(p.owner_names ?? []), ""],
            }))
          }
        >
          + Ajouter un propriétaire
        </button>
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
      <fieldset>
        <legend>Fichiers et raccourcis</legend>
        <p>Ouvrir : .xemf d’origine, .emfp ou JSON du clone.</p>
        <p>Enregistrer : .emfp (format JSON du clone).</p>
        <p>
          Ctrl+O : ouvrir · Ctrl+S : enregistrer · Ctrl+Z / Ctrl+Y : annuler /
          rétablir · Échap : abandonner le dessin.
        </p>
      </fieldset>
    </div>
  );
}
