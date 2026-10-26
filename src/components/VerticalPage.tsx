import { Antenna, Grid2X2, Play, Plus, Trash2 } from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
import { uid } from "../lib/project";
import type { VerticalCut } from "../types";
import DataGrid from "./DataGrid";
import FieldPlot from "./FieldPlot";
import { CalculateButton, LimitSelector, Tool } from "./Controls";
import FeatureTools from "./FeatureTools";
import SplitWorkspace from "./SplitWorkspace";
import { sourceVerticalCuts } from "../lib/cut-tools";
export default function VerticalPage() {
  const a = usePlanner(),
    { project, commit } = a;
  return (
    <SplitWorkspace vertical defaultWidth={670}>
      <section className="property-pane">
        <div className="pane-caption">Coupes verticales</div>
        <FeatureTools section="vertical" />
        <div className="section-tools">
          <Tool
            label="Ajouter une coupe verticale"
            onClick={() =>
              commit((p) => ({
                ...p,
                vertical_cuts: [
                  ...p.vertical_cuts,
                  {
                    id: uid(),
                    active: true,
                    azimuth_deg: 0,
                    x: 0,
                    y: 0,
                    backward_m: 0,
                    forward_m: 150,
                    max_height_m: 100,
                  },
                ],
              }))
            }
          >
            <Plus />
          </Tool>
          <Tool
            label="Coupes selon les azimuts des antennes"
            onClick={() =>
              commit((p) => ({
                ...p,
                vertical_cuts: sourceVerticalCuts(p),
              }))
            }
          >
            <Antenna />
          </Tool>
          <Tool
            label="Calculer toutes les coupes verticales"
            onClick={() => a.calculate("vertical")}
          >
            <Play />
          </Tool>
          <Tool
            label="Calculer les coupes avec la grille 2"
            onClick={() => a.calculate("vertical", true)}
          >
            <Grid2X2 />
          </Tool>
          <Tool
            label="Supprimer la coupe verticale"
            disabled={!a.selectedCut}
            onClick={() =>
              commit((p) => ({
                ...p,
                vertical_cuts: p.vertical_cuts.filter(
                  (c) => c.id !== a.selectedCut,
                ),
              }))
            }
          >
            <Trash2 />
          </Tool>
        </div>
        <div className="grow">
          <DataGrid<VerticalCut>
            label="Coupes verticales"
            rows={project.vertical_cuts}
            selected={a.selectedCut}
            onSelect={a.setSelectedCut}
            columns={[
              { key: "active", label: "✓", kind: "boolean", width: 28 },
              {
                key: "azimuth_deg",
                label: "Azimut, °",
                kind: "number",
                width: 72,
              },
              { key: "x", label: "X₀, m", kind: "number", width: 65 },
              { key: "y", label: "Y₀, m", kind: "number", width: 65 },
              {
                key: "forward_m",
                label: "L1, m",
                kind: "number",
                min: 1,
                width: 65,
              },
              {
                key: "backward_m",
                label: "L2, m",
                kind: "number",
                min: 0,
                width: 65,
              },
              {
                key: "max_height_m",
                label: "H max., m",
                kind: "number",
                min: 1,
                width: 75,
              },
              { key: "min_height_m", label: "H2, m", kind: "number", min: 0, width: 65 },
              { key: "name", label: "Sources dans cette direction", readOnly: true, width: 220 },
            ]}
            onUpdate={(id, key, value) =>
              commit((p) => ({
                ...p,
                vertical_cuts: p.vertical_cuts.map((c) =>
                  c.id === id
                    ? {
                        ...c,
                        [key]: value,
                        ...(key === "x" || key === "y"
                          ? { geo_position: undefined }
                          : {}),
                      }
                    : c,
                ),
              }), "Coupe modifiée — recalcul nécessaire", "stale-cuts")
            }
          />
        </div>
        <LimitSelector multiple />
        <label><input type="checkbox" checked={project.settings.cut_add_owner ?? false} onChange={e => a.settings({ cut_add_owner: e.target.checked })}/>Ajouter le propriétaire au nom des sources</label>
        <label><input type="checkbox" checked={project.settings.cut_use_antenna_names ?? false} onChange={e => a.settings({ cut_use_antenna_names: e.target.checked })}/>Utiliser le modèle d’antenne pour les noms des coupes</label>
        {a.cutResultsStale && <p role="status">Paramètres modifiés. La dernière image calculée est conservée jusqu’au recalcul.</p>}
        <CalculateButton kind="vertical" />
      </section>
      <FieldPlot
        grid={
          (
            a.verticalResults.find(
              (r) => (r.cut_id ?? r.id) === a.selectedCut,
            ) ?? a.verticalResults[0]
          )?.grid
        }
        overlays={a.verticalResults
          .filter(
            (r) =>
              (r.cut_id ?? r.id) === a.selectedCut &&
              (r.limit_index === undefined ||
                project.limits[r.limit_index]?.active !== false),
          )
          .map((r) => r.contours)}
        legend={project.limits
          .filter((l) => l.active)
          .map((l) => ({ name: l.name, color: l.color }))}
      />
    </SplitWorkspace>
  );
}
