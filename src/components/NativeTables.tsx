import { useMemo, useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import { DEFAULT_TABLE_SAMPLING, sampleVerticalTable, nativeSuperFormat } from "../lib/report-content";
import { exportAliasWord } from "../lib/files";
import { NumberField } from "./Controls";
export default function NativeTables() {
  const { project, task } = usePlanner();
  const [options, setOptions] = useState({ ...DEFAULT_TABLE_SAMPLING, limitIndex: project.settings.selected_limit });
  const cuts = project.vertical_cuts.filter((c) => c.active), [cutId, setCutId] = useState(""), [page, setPage] = useState(0), [metric, setMetric] = useState<"field" | "exposure">("field");
  const cut = cuts.find((c) => c.id === cutId) ?? cuts[0];
  const sampled = useMemo(() => { try { return cut ? { value: sampleVerticalTable(project, cut, options), error: "" } : { error: "Ajoutez une coupe verticale active pour calculer le tableau.", value: null }; } catch (error) { return { error: String((error as Error).message), value: null }; } }, [project, cut, options]);
  const value = sampled.value, pages = Math.ceil((value?.distances.length ?? 0) / 10), selectedPage = Math.min(page, Math.max(0, pages - 1)), offset = selectedPage * 10;
  const rows = value?.rows[value.rows.length - 1] ?? [], yvalues = rows.map((r) => metric === "field" ? r.total_field : r.exposure_ratio * 100), max = Math.max(1e-9, ...yvalues);
  return <section className="native-tables" aria-label="Tableaux et graphiques des coupes verticales"><h2>Tableaux et graphiques des coupes verticales</h2>
    <label>Coupe verticale<select aria-label="Coupe verticale du tableau" value={cut?.id ?? ""} onChange={(e) => setCutId(e.target.value)}>{cuts.map((c) => <option key={c.id} value={c.id}>{c.azimuth_deg}°</option>)}</select></label>
    <div className="table-chart-controls">{([["distance", "Distance du tableau, m"], ["distanceStep", "Pas de distance, m"], ["height", "Hauteur du tableau, m"], ["heightStep", "Pas de hauteur, m"]] as const).map(([key, label]) => <NumberField key={key} label={label} value={options[key]} onChange={(number) => { setOptions((o) => ({ ...o, [key]: number })); setPage(0); }}/>)}</div>
    <fieldset><legend>Jeu de limites du tableau et du graphique</legend>{project.limits.map((limit, i) => <label key={i}><input type="radio" name="table-limit" checked={options.limitIndex === i} onChange={() => setOptions((o) => ({ ...o, limitIndex: i }))}/>{limit.name}</label>)}</fieldset>
    <label>Grandeur<select aria-label="Grandeur du tableau vertical" value={metric} onChange={(e) => setMetric(e.target.value as "field" | "exposure")}><option value="field">Champ électrique, V/m</option><option value="exposure">Exposition simultanée, %</option></select></label>
    {sampled.error && <p role="alert">{sampled.error}</p>}
    {value && <><p>{value.distances.length} colonnes × {value.heights.length} lignes — graphique à {value.heights[value.heights.length - 1]} m.</p>
      <svg role="img" aria-label={metric === "field" ? "Graphique du champ électrique" : "Graphique de l’exposition simultanée"} viewBox="0 0 720 280" style={{ width: "100%", maxWidth: 900, background: "white" }}><path d="M45 20V240H700" stroke="black" fill="none"/><text x="45" y="15">{nativeSuperFormat(max)} {metric === "field" ? "V/m" : "%"}</text><text x="600" y="265">{options.distance} m</text><polyline fill="none" stroke="#24639c" strokeWidth="2" points={yvalues.map((v, i) => `${45 + (value.distances[i] / options.distance) * 650},${240 - v / max * 210}`).join(" ")}/></svg>
      <div><button disabled={!selectedPage} onClick={() => setPage(selectedPage - 1)}>Page précédente</button> Page {selectedPage + 1}/{pages} <button disabled={selectedPage + 1 >= pages} onClick={() => setPage(selectedPage + 1)}>Page suivante</button></div>
      <div style={{ overflow: "auto" }}><table aria-label="Valeurs calculées de la coupe verticale"><thead><tr><th>Hauteur m / Distance m</th>{value.distances.slice(offset, offset + 10).map((d) => <th key={d}>{d}</th>)}</tr></thead><tbody>{value.rows.map((row, y) => <tr key={y}><th>{value.heights[y]}</th>{row.slice(offset, offset + 10).map((r, x) => <td key={x} title={String(metric === "field" ? r.total_field : r.exposure_ratio * 100)}>{nativeSuperFormat(metric === "field" ? r.total_field : r.exposure_ratio * 100)}</td>)}</tr>)}</tbody></table></div></>}
    <button disabled={!value} onClick={() => void task(() => exportAliasWord(project, metric === "field" ? "EFieldStrengthVerticalCutTables_1" : "SimultaneousExposureVerticalCutTables_1", options), "Tableau Word exporté")}>Tableau vers Word</button>
    <button disabled={!value} onClick={() => void task(() => exportAliasWord(project, metric === "field" ? "EFieldStrengthGraph_1" : "SimultaneousExposureGraph_1", options), "Graphique Word exporté")}>Graphique vers Word</button>
    <details><summary>Aide des alias de tableaux et graphiques</summary><code>{"{EFieldStrengthVerticalCutTables_1} {SimultaneousExposureVerticalCutTables_1} {EFieldStrengthGraph_1} {SimultaneousExposureGraph_1}"}</code></details>
  </section>;
}
