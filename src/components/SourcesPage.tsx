import { useState } from "react";
import {
  ArrowDown,
  ArrowUp,
  ArrowUpToLine,
  ArrowDownToLine,
  ClipboardCopy,
  ClipboardPaste,
  CheckSquare,
  Copy,
  FileSpreadsheet,
  FileText,
  Plus,
  Trash2,
  Upload,
} from "lucide-react";
import type { RadiatingSource } from "../types";
import { usePlanner } from "../lib/usePlanner";
import {
  chooseFile,
  exportSourcesExcel,
  exportWord,
  importExcel,
  saveText,
} from "../lib/files";
import { decodeMsi, parseMsi, sourceAntenna, writeMsi } from "../lib/antenna";
import {
  nativeBeamWidth,
  sourceFrequencyLabel,
} from "../lib/source-fields";
import {
  copySourceRows,
  duplicateSourceRows,
  moveSourceRows,
  pasteSourceRows,
  colorSourceRows,
  toggleAllSourceRows,
} from "../lib/source-rows";
import SourcePowerCalculator from "./SourcePowerCalculator";
import PowerConversionDialog from "./PowerConversionDialog";
import { pasteSourceCells, sourceCellPatch } from "../lib/source-clipboard";
import DataGrid, { type Column } from "./DataGrid";
import AntennaPolar from "./AntennaPolar";
import { Tool } from "./Controls";
import FeatureTools from "./FeatureTools";
import "./SourcesPage.css";
const COLUMNS: Column<RadiatingSource>[] = [
  { key: "active", label: "✓", kind: "boolean", width: 28 },
  { key: "name", label: "Nom de la source", width: 110 },
  { key: "sector_id", label: "Secteur", width: 76 },
  {
    key: "service_type",
    label: "Service",
    width: 70,
  },
  {
    key: "frequency_mhz",
    label: "Fréquence, MHz",
    editValue: sourceFrequencyLabel,
    width: 76,
  },
  {
    key: "power_w",
    label: "Puissance TX, W",
    kind: "number",
    min: 0,
    max: 100000,
    width: 78,
  },
  {
    key: "num_tx",
    label: "Nombre de TX",
    kind: "number",
    integer: "floor",
    min: 1,
    maxExclusive: 100,
    width: 62,
  },
  {
    key: "total_attenuation_db",
    label: "Atténuation, dB",
    kind: "number",
    min: 0,
    max: 100,
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
    kind: "text",
    editValue: (source) => source.azimuth_deg < 0 ? "0÷360" : source.azimuth_deg,
    width: 65,
  },
  {
    key: "gain_dbi",
    label: "Gain, dBi",
    kind: "number",
    minExclusive: -100,
    maxExclusive: 100,
    width: 62,
  },
  {
    key: "height_m",
    label: "Hauteur, m",
    kind: "number",
    min: 0,
    maxExclusive: 1000,
    width: 68,
  },
  {
    key: "antenna_size_m",
    label: "Taille, m",
    kind: "number",
    minExclusive: 0,
    maxExclusive: 30,
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
  {
    key: "is_existing",
    label: "Existant",
    kind: "select",
    options: ["New", "Existing"],
    editValue: (source) => (source.is_existing ? "Existing" : "New"),
    parse: (value) => value === "Existing",
    width: 55,
  },
  { key: "owner", label: "Propriétaire", width: 90 },
];
export default function SourcesPage() {
  const a = usePlanner();
  const {
    project,
    source,
    setSelectedSource,
    commit,
    updateSource,
    addSource,
    task,
    openCommand,
    rotateSourcePattern,
    selectedSourceIds,
    setSourceSelection,
  } = a;
  const selected = selectedSourceIds.filter((id) =>
    project.sources.some((s) => s.id === id),
  );
  const [buffer, setBuffer] = useState(() => {
    try {
      return localStorage.getItem("emf-planner-source-buffer-v1") ?? "";
    } catch {
      return "";
    }
  });
  const [powerSource, setPowerSource] = useState<string | null>(null);
  const [layout, setLayout] = useState<{ order: (keyof RadiatingSource)[]; widths: Record<string, number> }>(() => {
    try { return JSON.parse(localStorage.getItem("emf-planner-source-columns-v1") ?? "null") ?? { order: [], widths: {} }; }
    catch { return { order: [], widths: {} }; }
  });
  function saveLayout(next: typeof layout) { setLayout(next); localStorage.setItem("emf-planner-source-columns-v1", JSON.stringify(next)); }
  const orderedColumns = [...COLUMNS].sort((a, b) => {
    const ai = layout.order.indexOf(a.key), bi = layout.order.indexOf(b.key);
    return (ai < 0 ? COLUMNS.indexOf(a) + COLUMNS.length : ai) - (bi < 0 ? COLUMNS.indexOf(b) + COLUMNS.length : bi);
  });
  function pasteCells(text: string, rowId: string | undefined, key: keyof RadiatingSource) {
    try { const sources = pasteSourceCells(project.sources, project.origin, text, rowId, key, orderedColumns.filter((c) => !c.readOnly).map((c) => c.key)); commit((p) => ({ ...p, sources })); }
    catch (reason) { a.fail(reason); }
  }
  function applyRows(result: ReturnType<typeof moveSourceRows>) {
    if (!result.changed) return;
    commit((p) => ({ ...p, sources: result.sources }));
    setSourceSelection(result.selectedIds, result.activeId);
  }
  function move(direction: "up" | "down" | "top" | "bottom") {
    applyRows(moveSourceRows(project.sources, selected, direction));
  }
  function removeSelected() {
    if (
      !selected.length ||
      !window.confirm(
        `Supprimer ${selected.length} source(s) sélectionnée(s) ?`,
      )
    )
      return;
    const rows = project.sources.filter((s) => !selected.includes(s.id));
    commit((p) => ({ ...p, sources: rows }));
    setSelectedSource(rows[0]?.id ?? "");
  }
  function copySelected() {
    const value = copySourceRows(project.sources, selected, project.origin);
    if (!value) return;
    try {
      localStorage.setItem("emf-planner-source-buffer-v1", value);
      setBuffer(value);
      a.setStatus(`${selected.length} source(s) copiée(s)`);
    } catch (error) {
      a.fail(error);
    }
  }
  function pasteSelected() {
    try {
      applyRows(
        pasteSourceRows(
          project.sources,
          source?.id ?? "",
          buffer,
          undefined,
          project.origin,
        ),
      );
    } catch (error) {
      a.fail(error);
    }
  }
  function updateRows(
    ids: string[],
    key: keyof RadiatingSource,
    value: unknown,
  ) {
    try {
      if (key === "owner" && value === "Specify manually") {
        const entered = window.prompt("Propriétaire", "");
        if (entered === null) return;
        value = entered;
      }
      const patch = key === "limit_factors" || key === "pattern_h" || key === "pattern_v" || key === "feeder" ? { [key]: value } : sourceCellPatch(key, value);
      commit(
        (p) => ({
          ...p,
          sources: p.sources.map((s) =>
            ids.includes(s.id) ? { ...s, ...patch, ...(key === "owner" && p.owner_colors?.[String(value)] ? { color: p.owner_colors[String(value)] } : {}) } : s,
          ),
        }),
        "Sources modifiées",
        key !== "color",
      );
    } catch (error) {
      a.fail(error);
    }
  }
  function importXlsx() {
    void task(async () => {
      const file = await chooseFile(["xlsx", "xltx", "xls", "xlt"]);
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
        const antenna = parseMsi(decodeMsi(file.bytes));
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
                  beam_width_h_deg: nativeBeamWidth(antenna.horizontal),
                  beam_width_v_deg: nativeBeamWidth(antenna.vertical),
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
      const { horizontal: h, vertical: v } = sourceAntenna(source);
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
            disabled={!selected.length}
            onClick={removeSelected}
          >
            <Trash2 />
          </Tool>
          <Tool
            label="Dupliquer la source"
            disabled={!selected.length}
            onClick={() =>
              applyRows(duplicateSourceRows(project.sources, selected))
            }
          >
            <Copy />
          </Tool>
          <Tool
            label="Monter la source"
            disabled={
              !selected.length || selected.includes(project.sources[0]?.id)
            }
            onClick={() => move("up")}
          >
            <ArrowUp />
          </Tool>
          <Tool
            label="Descendre la source"
            disabled={
              !selected.length ||
              selected.includes(
                project.sources[project.sources.length - 1]?.id ?? "",
              )
            }
            onClick={() => move("down")}
          >
            <ArrowDown />
          </Tool>
          <Tool
            label="Placer les sources au début"
            disabled={
              !selected.length || selected.includes(project.sources[0]?.id)
            }
            onClick={() => move("top")}
          >
            <ArrowUpToLine />
          </Tool>
          <Tool
            label="Placer les sources à la fin"
            disabled={
              !selected.length ||
              selected.includes(
                project.sources[project.sources.length - 1]?.id ?? "",
              )
            }
            onClick={() => move("bottom")}
          >
            <ArrowDownToLine />
          </Tool>
          <Tool
            label="Activer ou désactiver toutes les sources"
            onClick={() =>
              commit((p) => ({
                ...p,
                sources: toggleAllSourceRows(p.sources),
              }))
            }
          >
            <CheckSquare />
          </Tool>
          <Tool
            label="Copier les sources sélectionnées"
            disabled={!selected.length}
            onClick={copySelected}
          >
            <ClipboardCopy />
          </Tool>
          <Tool
            label="Coller les sources"
            disabled={!buffer}
            onClick={pasteSelected}
          >
            <ClipboardPaste />
          </Tool>
          <span className="toolbar-separator" />
          <Tool label="Importer les sources Excel" onClick={importXlsx}>
            <Upload />
          </Tool>
          <Tool
            label="Exporter les tableaux Excel"
            onClick={() =>
              void task(() => exportSourcesExcel(project), "Classeur Excel exporté")
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
              "#f08080",
              "#e0ffff",
              "#fafad2",
              "#90ee90",
              "#ffb6c1",
              "#ffa07a",
              "#20b2aa",
              "#87cefa",
              "#ffffe0",
              "#b0c4de",
              "#ffff00",
            ].map((color) => (
              <button
                key={color}
                aria-label={`Couleur ${color}`}
                style={{ background: color }}
                disabled={!selected.length}
                onClick={() =>
                  commit(
                    (p) => ({
                      ...p,
                      sources: colorSourceRows(p.sources, selected, color),
                    }),
                    "Couleurs des sources modifiées",
                    false,
                  )
                }
              />
            ))}
          </div>
          <FeatureTools section="sources" />
        </div>
        <DataGrid
          label="Sources rayonnantes"
          rows={project.sources}
          columns={orderedColumns.map((c) => ({
            ...c,
            width: layout.widths[String(c.key)] ?? Math.max(24, Math.round((c.width ?? 90) * 0.72)),
            ...(c.key === "owner"
              ? {
                  kind: "select" as const,
                  options: [
                    ...new Set([
                      "",
                      ...(project.owner_names ?? []),
                      ...project.sources.map((s) => s.owner),
                      "Specify manually",
                    ]),
                  ],
                }
              : {}),
          }))}
          selected={source?.id}
          selectedIds={selected}
          onSelectionChange={setSourceSelection}
          onUpdate={(id, key, value) => updateRows([id], key, value)}
          onUpdateMany={updateRows}
          onDeleteSelection={removeSelected}
          onCopySelection={copySelected}
          onPasteSelection={pasteSelected}
          selectCells
          onPasteCells={pasteCells}
          onReorderColumns={(order) => saveLayout({ ...layout, order })}
          onResizeColumn={(key, width) => saveLayout({ ...layout, widths: { ...layout.widths, [key]: width } })}
          onColumnClick={(key) => { if (key !== "active") return false; commit((p) => ({ ...p, sources: toggleAllSourceRows(p.sources) })); return true; }}
          onCellDoubleClick={(id, key) => { if (key === "power_w") setPowerSource(id); if (key === "total_attenuation_db") { setSelectedSource(id); openCommand("Bibliothèque de câbles"); } }}
          onAdd={() => addSource()}
        />
        <div className="source-workspace-note">
          {project.sources.length
            ? `${selected.length} sélectionnée(s) · Ctrl/clic et Maj/clic sur les numéros : sélection multiple · Tab : cellule suivante · Entrée : valider.`
            : "Ajoutez une source avec ＋ ou ouvrez un projet .xemf / .emfp."}
        </div>
      </section>
      <aside className="source-inspector">
        <fieldset className="antenna-panel">
          <legend>Diagramme d’antenne</legend>
          <div
            className="antenna-toolbar"
            role="toolbar"
            aria-label="Outils du diagramme d’antenne"
          >
            {[
              {
                label: "Importer le diagramme MSI",
                resource: "cmdLoadDN",
                run: importMsi,
              },
              {
                label: "Choisir un diagramme d’antenne",
                resource: "cmdLoadDN2",
                run: () => openCommand("Bibliothèque d’antennes"),
              },
              {
                label: "Créer une enveloppe de diagrammes",
                resource: "cmdEnvelope",
                run: () => openCommand("Enveloppe de diagrammes"),
              },
              {
                label: "Créer un diagramme d’antenne symétrique",
                resource: "cmdApert",
                run: () => openCommand("Antenne symétrique ITU-R F.699"),
              },
              {
                label: "Exporter le diagramme MSI",
                resource: "cmdSaveMSI",
                run: exportMsi,
              },
              {
                label: "Modifier le diagramme d’antenne",
                resource: "cmdEditorDN",
                run: () => openCommand("Éditeur de diagramme"),
              },
            ].map(({ label, resource, run }) => (
              <button
                key={resource}
                title={label}
                aria-label={label}
                disabled={!source}
                onClick={run}
              >
                <img
                  className="original-icon"
                  src={`/original-icons/UC_PRTO.${resource}.png`}
                  alt=""
                  draggable={false}
                />
              </button>
            ))}
          </div>
          <div className="antenna-plot">
            <AntennaPolar source={source} both />
            <button
              className="antenna-rotate antenna-rotate-up"
              title="Tourner le diagramme vertical vers le haut (+1°)"
              aria-label="Tourner le diagramme vertical vers le haut (+1°)"
              disabled={source?.pattern_v?.length !== 360}
              onClick={() => rotateSourcePattern(1)}
            >
              +
            </button>
            <button
              className="antenna-rotate antenna-rotate-down"
              title="Tourner le diagramme vertical vers le bas (−1°)"
              aria-label="Tourner le diagramme vertical vers le bas (−1°)"
              disabled={source?.pattern_v?.length !== 360}
              onClick={() => rotateSourcePattern(-1)}
            >
              −
            </button>
          </div>
        </fieldset>
        <SourcePowerCalculator source={source} />
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
                        min="0.1"
                        max="1"
                        disabled={
                          !source || ![l.use_alpha24, l.use_ftdc, l.use_fpr][k]
                        }
                        value={
                          ![l.use_alpha24, l.use_ftdc, l.use_fpr][k]
                            ? ""
                            : (source?.limit_factors?.[i]?.[k] ??
                              (source
                                ? [
                                    source.alpha24,
                                    source.duty_cycle,
                                    source.power_reduction,
                                  ][k]
                                : 1))
                        }
                        onChange={(e) => {
                          if (!source) return;
                          const v = Number(e.target.value);
                          if (!Number.isFinite(v) || v < 0.1 || v > 1) return;
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
      {powerSource && <PowerConversionDialog value={project.sources.find((s) => s.id === powerSource)?.power_w ?? 0} onApply={(value) => updateRows([powerSource], "power_w", value)} onClose={() => setPowerSource(null)} />}
    </div>
  );
}
