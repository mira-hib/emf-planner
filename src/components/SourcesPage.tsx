import { useState } from "react";
import {
  ArrowDown,
  ArrowUp,
  Cable,
  CheckSquare,
  Copy,
  FileSpreadsheet,
  FileText,
  Plus,
  Trash2,
  Upload,
  X,
} from "lucide-react";
import type { RadiatingSource } from "../types";
import { usePlanner } from "../lib/usePlanner";
import {
  chooseFile,
  exportExcel,
  exportWord,
  importExcel,
  saveText,
} from "../lib/files";
import { parseMsi, writeMsi } from "../lib/antenna";
import { eirp, patternAmplitude } from "../lib/engine";
import { uid } from "../lib/project";
import DataGrid, { type Column } from "./DataGrid";
import AntennaPolar from "./AntennaPolar";
import { NumberField, Tool } from "./Controls";
const COLUMNS: Column<RadiatingSource>[] = [
  { key: "active", label: "✓", kind: "boolean", width: 28 },
  { key: "name", label: "Nom de la source", width: 110 },
  { key: "sector_id", label: "Secteur", width: 76 },
  {
    key: "service_type",
    label: "Service",
    kind: "select",
    options: ["GSM", "UMTS", "LTE", "5G", "FM", "TV", "PtP", "Autre"],
    width: 70,
  },
  {
    key: "frequency_mhz",
    label: "Fréquence, MHz",
    kind: "number",
    min: 0.001,
    width: 76,
  },
  {
    key: "power_w",
    label: "Puissance TX, W",
    kind: "number",
    min: 0,
    width: 78,
  },
  {
    key: "num_tx",
    label: "Nombre de TX",
    kind: "number",
    min: 1,
    max: 10000,
    width: 62,
  },
  {
    key: "total_attenuation_db",
    label: "Atténuation, dB",
    kind: "number",
    min: 0,
    width: 72,
  },
  {
    key: "id",
    label: "Puissance antenne, W",
    readOnly: true,
    format: (s) =>
      (s.power_w * s.num_tx * 10 ** (-s.total_attenuation_db / 10)).toFixed(4),
    width: 88,
  },
  { key: "antenna_model", label: "Modèle d’antenne", width: 108 },
  {
    key: "azimuth_deg",
    label: "Azimut, °",
    kind: "number",
    min: 0,
    max: 360,
    width: 65,
  },
  { key: "gain_dbi", label: "Gain, dBi", kind: "number", width: 62 },
  { key: "height_m", label: "Hauteur, m", kind: "number", min: 0, width: 68 },
  {
    key: "antenna_size_m",
    label: "Taille, m",
    kind: "number",
    min: 0,
    width: 58,
  },
  {
    key: "mechanical_downtilt_deg",
    label: "Tilt méca., °",
    kind: "number",
    min: -90,
    max: 90,
    width: 68,
  },
  {
    key: "electrical_downtilt_deg",
    label: "Tilt élec., °",
    kind: "number",
    min: -90,
    max: 90,
    width: 68,
  },
  {
    key: "beam_width_h_deg",
    label: "Ouverture H, °",
    kind: "number",
    min: 0.1,
    max: 360,
    width: 73,
  },
  {
    key: "beam_width_v_deg",
    label: "Ouverture V, °",
    kind: "number",
    min: 0.1,
    max: 360,
    width: 73,
  },
  {
    key: "location",
    label: "Position",
    kind: "select",
    options: ["Tower", "Roof", "Facade"],
    width: 76,
  },
  { key: "is_existing", label: "Existant", kind: "boolean", width: 55 },
  { key: "owner", label: "Propriétaire", width: 90 },
];
export default function SourcesPage() {
  const {
    project,
    source,
    setSelectedSource,
    commit,
    updateSource,
    addSource,
    task,
  } = usePlanner();
  const [vertical, setVertical] = useState(false),
    [lossOpen, setLossOpen] = useState(false),
    [loss, setLoss] = useState(0);
  function move(direction: number) {
    if (!source) return;
    const rows = [...project.sources],
      i = rows.findIndex((s) => s.id === source.id),
      next = i + direction;
    if (next < 0 || next >= rows.length) return;
    [rows[i], rows[next]] = [rows[next], rows[i]];
    commit((p) => ({ ...p, sources: rows }));
  }
  function importXlsx() {
    void task(async () => {
      const file = await chooseFile(["xlsx"]);
      if (file) {
        const sources = await importExcel(project, file.bytes);
        commit((p) => ({ ...p, sources: [...p.sources, ...sources] }));
      }
      return file;
    }, "Sources importées");
  }
  function importMsi() {
    void task(async () => {
      if (!source) throw new Error("Sélectionnez une source.");
      const file = await chooseFile(["msi", "txt"]);
      if (file) {
        const antenna = parseMsi(new TextDecoder().decode(file.bytes));
        commit((p) => ({
          ...p,
          sources: p.sources.map((s) =>
            s.id === source.id
              ? {
                  ...s,
                  antenna_model: antenna.name,
                  gain_dbi: antenna.gain,
                  pattern_h: antenna.horizontal,
                  pattern_v: antenna.vertical,
                }
              : s,
          ),
        }));
      }
      return file;
    }, "Diagramme MSI importé");
  }
  function exportMsi() {
    void task(async () => {
      if (!source) return false;
      const h =
          source.pattern_h ??
          Array.from(
            { length: 360 },
            (_, i) =>
              -20 *
              Math.log10(
                patternAmplitude(undefined, i, source.beam_width_h_deg),
              ),
          ),
        v =
          source.pattern_v ??
          Array.from(
            { length: 360 },
            (_, i) =>
              -20 *
              Math.log10(
                patternAmplitude(undefined, i, source.beam_width_v_deg),
              ),
          );
      return saveText(
        `${source.antenna_model || source.name}.msi`,
        writeMsi(source.antenna_model, source.gain_dbi, h, v),
      );
    }, "Diagramme MSI exporté");
  }
  return (
    <div className="sources-layout">
      <section className="source-table-area">
        <div className="section-tools">
          <Tool label="Ajouter une source" onClick={() => addSource()}>
            <Plus />
          </Tool>
          <Tool
            label="Supprimer la source sélectionnée"
            disabled={!source}
            onClick={() =>
              commit((p) => ({
                ...p,
                sources: p.sources.filter((s) => s.id !== source?.id),
              }))
            }
          >
            <Trash2 />
          </Tool>
          <Tool
            label="Dupliquer la source"
            disabled={!source}
            onClick={() => {
              if (source) {
                const c = {
                  ...source,
                  id: uid(),
                  name: `${source.name} (copie)`,
                };
                commit((p) => ({ ...p, sources: [...p.sources, c] }));
                setSelectedSource(c.id);
              }
            }}
          >
            <Copy />
          </Tool>
          <Tool label="Monter la source" onClick={() => move(-1)}>
            <ArrowUp />
          </Tool>
          <Tool label="Descendre la source" onClick={() => move(1)}>
            <ArrowDown />
          </Tool>
          <Tool
            label="Activer ou désactiver toutes les sources"
            onClick={() =>
              commit((p) => ({
                ...p,
                sources: p.sources.map((s) => ({
                  ...s,
                  active: !p.sources.every((s) => s.active !== false),
                })),
              }))
            }
          >
            <CheckSquare />
          </Tool>
          <span className="toolbar-separator" />
          <Tool label="Importer les sources Excel" onClick={importXlsx}>
            <Upload />
          </Tool>
          <Tool
            label="Exporter les tableaux Excel"
            onClick={() =>
              void task(() => exportExcel(project), "Classeur Excel exporté")
            }
          >
            <FileSpreadsheet />
          </Tool>
          <Tool
            label="Exporter le rapport Word"
            onClick={() =>
              void task(() => exportWord(project, true), "Rapport Word exporté")
            }
          >
            <FileText />
          </Tool>
          <div className="color-tools">
            {[
              "#ffffff",
              "#f8b3b3",
              "#b5edf0",
              "#f4eb9a",
              "#a5eda2",
              "#d3c1f3",
              "#fbc496",
            ].map((color) => (
              <button
                key={color}
                aria-label={`Couleur ${color}`}
                style={{ background: color }}
                disabled={!source}
                onClick={() =>
                  source && updateSource(source.id, "color", color)
                }
              />
            ))}
          </div>
        </div>
        <DataGrid
          label="Sources rayonnantes"
          rows={project.sources}
          columns={COLUMNS}
          selected={source?.id}
          onSelect={setSelectedSource}
          onUpdate={updateSource}
          onAdd={() => addSource()}
        />
        <div className="source-workspace-note">
          {project.sources.length
            ? "Saisissez les paramètres dans les cellules. Tab : cellule suivante · Entrée : valider."
            : "Ajoutez une source avec ＋ ou ouvrez un projet .xemf / .emfp."}
        </div>
      </section>
      <aside className="source-inspector">
        <fieldset>
          <legend>Diagramme d’antenne</legend>
          <div className="button-row">
            <button
              className={!vertical ? "pressed" : ""}
              onClick={() => setVertical(false)}
            >
              H
            </button>
            <button
              className={vertical ? "pressed" : ""}
              onClick={() => setVertical(true)}
            >
              V
            </button>
            <button onClick={importMsi} disabled={!source}>
              MSI ↓
            </button>
            <button
              onClick={exportMsi}
              disabled={!source}
              title="Exporter le diagramme MSI"
            >
              ↑
            </button>
          </div>
          <AntennaPolar source={source} vertical={vertical} />
          <div className="inspector-note">
            {source?.antenna_model || "Aucune source"}
          </div>
        </fieldset>
        <fieldset>
          <legend>Calculateur EIRP</legend>
          {source ? (
            <>
              <NumberField
                label="Puissance TX, W"
                value={source.power_w}
                min={0}
                onChange={(v) => updateSource(source.id, "power_w", v)}
              />
              <NumberField
                label="Nombre de TX"
                value={source.num_tx}
                min={1}
                step="1"
                onChange={(v) => updateSource(source.id, "num_tx", v)}
              />
              <NumberField
                label="Atténuation, dB"
                value={source.total_attenuation_db}
                min={0}
                onChange={(v) =>
                  updateSource(source.id, "total_attenuation_db", v)
                }
              />
              <NumberField
                label="Gain, dBi"
                value={source.gain_dbi}
                onChange={(v) => updateSource(source.id, "gain_dbi", v)}
              />
              <div className="calculated-value">
                <span>Puissance antenne</span>
                <output>
                  {(
                    source.power_w *
                    source.num_tx *
                    10 ** (-source.total_attenuation_db / 10)
                  ).toFixed(4)}{" "}
                  W
                </output>
              </div>
              <div className="calculated-value">
                <span>EIRP</span>
                <output>{eirp(source).toFixed(3)} W</output>
              </div>
              <button
                className="text-button"
                onClick={() => {
                  setLoss(source.total_attenuation_db);
                  setLossOpen(true);
                }}
              >
                <Cable size={12} /> Câble / connecteurs
              </button>
            </>
          ) : (
            <p className="muted">Sélectionnez une source.</p>
          )}
        </fieldset>
        <fieldset>
          <legend>Facteurs d’atténuation</legend>
          <table className="factor-table">
            <thead>
              <tr>
                <th>Limites</th>
                <th>α24</th>
                <th>Ftdc</th>
                <th>Fpr</th>
              </tr>
            </thead>
            <tbody>
              {project.limits.map((l, i) => (
                <tr key={l.id}>
                  <td>{i + 1}</td>
                  {[0, 1, 2].map((k) => (
                    <td key={k}>
                      <input
                        aria-label={`Facteur ${["alpha24", "Ftdc", "Fpr"][k]} limites ${i + 1}`}
                        type="number"
                        step="0.01"
                        min="0"
                        max="1"
                        disabled={!source}
                        value={
                          source?.limit_factors?.[i]?.[k] ??
                          (source
                            ? [
                                source.alpha24,
                                source.duty_cycle,
                                source.power_reduction,
                              ][k]
                            : 1)
                        }
                        onChange={(e) => {
                          if (!source) return;
                          const v = Number(e.target.value);
                          if (v < 0 || v > 1) return;
                          const factors = (
                            source.limit_factors ??
                            Array.from({ length: 4 }, () => [
                              source.alpha24,
                              source.duty_cycle,
                              source.power_reduction,
                            ])
                          ).map((r) => [...r]);
                          factors[i][k] = v;
                          updateSource(source.id, "limit_factors", factors);
                        }}
                      />
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </fieldset>
      </aside>
      {lossOpen && source && (
        <div className="modal-backdrop">
          <section
            className="small-dialog"
            role="dialog"
            aria-modal="true"
            aria-label="Calcul des pertes"
          >
            <header>
              Pertes câble et connecteurs
              <button
                aria-label="Fermer le calcul des pertes"
                onClick={() => setLossOpen(false)}
              >
                <X size={15} />
              </button>
            </header>
            <NumberField
              label="Atténuation totale, dB"
              value={loss}
              min={0}
              onChange={setLoss}
            />
            <p className="hint">
              Perte à {source.frequency_mhz} MHz, câble et connecteurs compris.
            </p>
            <button
              className="primary-action"
              onClick={() => {
                updateSource(source.id, "total_attenuation_db", loss);
                setLossOpen(false);
              }}
            >
              Appliquer à {source.name}
            </button>
          </section>
        </div>
      )}
    </div>
  );
}
