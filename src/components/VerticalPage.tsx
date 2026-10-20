import { Antenna, Grid2X2, Play, Plus, Trash2 } from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
import { uid } from "../lib/project";
import type { VerticalCut } from "../types";
import DataGrid from "./DataGrid";
import FieldPlot from "./FieldPlot";
import { CalculateButton, LimitSelector, Tool } from "./Controls";
import FeatureTools from "./FeatureTools";
import SplitWorkspace from "./SplitWorkspace";
export default function VerticalPage() {
  const a = usePlanner(),
    { project, commit } = a;
  return (
    <SplitWorkspace vertical>
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
                    azimuth_deg: 60,
                    x: 0,
                    y: 0,
                    backward_m: 100,
                    forward_m: 100,
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
                vertical_cuts: Array.from(
                  new Set(p.sources.map((s) => s.azimuth_deg)),
                ).map((azimuth_deg) => ({
                  id: uid(),
                  active: true,
                  azimuth_deg,
                  x: 0,
                  y: 0,
                  backward_m: 100,
                  forward_m: 100,
                  max_height_m: 100,
                })),
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
            ]}
            onUpdate={(id, key, value) =>
              commit((p) => ({
                ...p,
                vertical_cuts: p.vertical_cuts.map((c) =>
                  c.id === id ? { ...c, [key]: value } : c,
                ),
              }))
            }
          />
        </div>
        <LimitSelector multiple />
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
          .filter((r) => (r.cut_id ?? r.id) === a.selectedCut)
          .map((r) => r.contours)}
        legend={project.limits
          .filter((l) => l.active)
          .map((l) => ({ name: l.name, color: l.color }))}
      />
    </SplitWorkspace>
  );
}
