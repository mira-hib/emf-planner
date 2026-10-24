import { useMemo } from "react";
import {
  Antenna,
  FileSpreadsheet,
  FileText,
  Grid2X2,
  MapPin,
  Play,
  Plus,
  Trash2,
  Upload,
} from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
import { COLORS, uid } from "../lib/project";
import { toGeo } from "../lib/geometry";
import { chooseFile, exportExcel, exportWord } from "../lib/files";
import type {
  Building,
  ControlPoint,
  HorizontalCut,
  TerrainPoint,
  TerrainContour,
} from "../types";
import DataGrid, { type Column } from "./DataGrid";
import PlannerMap from "./PlannerMap";
import SplitWorkspace from "./SplitWorkspace";
import { CalculateButton, LimitSelector, NumberField, Tool } from "./Controls";
import FeatureTools from "./FeatureTools";
import { styledContours } from "../lib/presentation";
import { OriginPanel, GridSettings } from "./OriginPanel";
import AntennaLocationsPanel from "./AntennaLocationsPanel";
import { moveAntennas, snapAntenna } from "../lib/antenna-locations";
const POINT_COLUMNS: Column<ControlPoint>[] = [
  { key: "name", label: "Point", width: 70 },
  {
    key: "lat",
    label: "Latitude, °",
    kind: "number",
    min: -85,
    max: 85,
    width: 100,
  },
  {
    key: "lng",
    label: "Longitude, °",
    kind: "number",
    min: -180,
    max: 180,
    width: 100,
  },
  { key: "x_m", label: "X, m", kind: "number", width: 70 },
  { key: "y_m", label: "Y, m", kind: "number", width: 70 },
  {
    key: "location",
    label: "Position",
    kind: "select",
    options: ["Ground", "Building", "Roof"],
    width: 80,
  },
  { key: "description", label: "Description", width: 130 },
];
export default function MapWorkspace({ title }: { title: string }) {
  const a = usePlanner(),
    { project: p, section, point, commit, task, settings } = a;
  const group = a.selectedGroup,
    setGroup = a.setSelectedGroup;
  const contours = useMemo(
    () => styledContours(p, a.cutResults),
    [a.cutResults, p.horizontal_cuts, p.limits],
  );
  const groups = Array.from(
    new Set(p.control_points.map((c) => c.group || "Groupe 1")),
  );
  return (
    <SplitWorkspace
      key={section === "antennas" ? "antennas" : "map"}
      storageKey={section === "antennas" ? "antennas" : undefined}
      defaultWidth={section === "antennas" ? 510 : 350}
    >
      <section
        className={`property-pane ${section === "points" ? "points-pane" : section === "antennas" ? "antennas-pane" : ""}`}
      >
        <div className="pane-caption">{title}</div>
        <FeatureTools section={section} />
        {section === "points" && (
          <>
            <div className="section-tools">
              <Tool
                label="Ajouter un point au centre"
                onClick={() => a.addPoint(0, 0, group || "Groupe 1")}
              >
                <Plus />
              </Tool>
              <Tool
                label="Placer un point sur le plan"
                onClick={() => a.setTool("point")}
              >
                <MapPin />
              </Tool>
              <Tool
                label="Supprimer le point"
                disabled={!point}
                onClick={() =>
                  commit((p) => ({
                    ...p,
                    control_points: p.control_points.filter(
                      (c) => c.id !== point?.id,
                    ),
                  }))
                }
              >
                <Trash2 />
              </Tool>
              <span className="toolbar-separator" />
              <Tool
                label="Calculer tous les points"
                onClick={() => a.calculate("points")}
              >
                <Play />
              </Tool>
              <Tool
                label="Rapport des points Word"
                onClick={() =>
                  void task(() => exportWord(p), "Rapport Word exporté")
                }
              >
                <FileText />
              </Tool>
              <Tool
                label="Points vers Excel"
                onClick={() =>
                  void task(() => exportExcel(p), "Classeur Excel exporté")
                }
              >
                <FileSpreadsheet />
              </Tool>
            </div>
            <fieldset className="group-selector">
              <legend>Groupes de points</legend>
              <select
                aria-label="Groupe de points"
                value={group}
                onChange={(e) => {
                  setGroup(e.target.value);
                  a.setSelectedPoint(
                    p.control_points.find(
                      (c) => !e.target.value || c.group === e.target.value,
                    )?.id ?? "",
                  );
                }}
              >
                <option value="">Tous les groupes</option>
                {groups.map((g) => (
                  <option key={g}>{g}</option>
                ))}
              </select>
              <label>
                <input
                  type="checkbox"
                  aria-label="Activer le groupe de points"
                  disabled={!p.control_points.length}
                  checked={p.control_points
                    .filter((c) => !group || c.group === group)
                    .every((c) => c.group_active !== false)}
                  onChange={(e) =>
                    commit((v) => ({
                      ...v,
                      control_points: v.control_points.map((c) =>
                        !group || c.group === group
                          ? { ...c, group_active: e.target.checked }
                          : c,
                      ),
                    }))
                  }
                />
                Groupe actif
              </label>
              <button
                onClick={() => {
                  const name = `Groupe ${groups.length + 1}`;
                  setGroup(name);
                  a.addPoint(0, 0, name);
                }}
              >
                ＋ Groupe
              </button>
            </fieldset>
            <div className="point-list">
              <DataGrid
                label="Points de contrôle"
                coordinateFormat={p.settings.coordinate_format}
                rows={p.control_points.filter(
                  (c) => !group || c.group === group,
                )}
                columns={POINT_COLUMNS}
                selected={point?.id}
                onSelect={a.setSelectedPoint}
                onUpdate={a.updatePoint}
                onAdd={() => a.addPoint(0, 0, group || "Groupe 1")}
              />
            </div>
            <fieldset className="point-heights">
              <legend>Hauteurs du point {point?.name}</legend>
              <DataGrid
                label="Hauteurs des points"
                rows={point?.heights ?? []}
                columns={[
                  {
                    key: "height_m",
                    label: "Hauteur, m",
                    kind: "number",
                    min: 0,
                    width: 82,
                  },
                  {
                    key: "attenuation_db",
                    label: "Atténuation, dB",
                    kind: "number",
                    min: 0,
                    width: 86,
                  },
                  {
                    key: "measured",
                    label: "E mesuré, V/m",
                    kind: "number",
                    min: 0,
                    width: 90,
                  },
                  { key: "description", label: "Description", width: 130 },
                ]}
                onUpdate={(id, key, value) =>
                  point &&
                  commit((p) => ({
                    ...p,
                    control_points: p.control_points.map((c) =>
                      c.id === point.id
                        ? {
                            ...c,
                            heights: c.heights?.map((h) =>
                              h.id === id ? { ...h, [key]: value } : h,
                            ),
                          }
                        : c,
                    ),
                  }))
                }
                onAdd={() =>
                  point &&
                  commit((p) => ({
                    ...p,
                    control_points: p.control_points.map((c) =>
                      c.id === point.id
                        ? {
                            ...c,
                            heights: [
                              ...(c.heights ?? []),
                              {
                                id: uid(),
                                height_m: 1.5,
                                attenuation_db: 0,
                                measured: 0,
                                description: "",
                              },
                            ],
                          }
                        : c,
                    ),
                  }))
                }
              />
              <button
                disabled={!point || (point.heights?.length ?? 0) < 2}
                onClick={() =>
                  point &&
                  commit((p) => ({
                    ...p,
                    control_points: p.control_points.map((c) =>
                      c.id === point.id
                        ? { ...c, heights: c.heights?.slice(0, -1) }
                        : c,
                    ),
                  }))
                }
              >
                Retirer la dernière hauteur
              </button>
            </fieldset>
            <LimitSelector />
            <NumberField
              label="Fréquence de mesure, MHz"
              value={p.settings.point_measurement_frequency_mhz ?? 1000}
              min={30}
              max={300000}
              onChange={(v) => settings({ point_measurement_frequency_mhz: v })}
            />
            <CalculateButton kind="points" />
          </>
        )}
        {section === "horizontal" && (
          <>
            <div className="section-tools">
              <Tool
                label="Ajouter une coupe horizontale"
                onClick={() =>
                  commit((p) => ({
                    ...p,
                    horizontal_cuts: [
                      ...p.horizontal_cuts,
                      {
                        id: uid(),
                        active: true,
                        height_m: 1.5,
                        color: COLORS[p.horizontal_cuts.length % 4],
                      },
                    ],
                  }))
                }
              >
                <Plus />
              </Tool>
              <Tool
                label="Ajouter les hauteurs des antennes"
                onClick={() =>
                  commit((p) => ({
                    ...p,
                    horizontal_cuts: Array.from(
                      new Set(
                        p.sources
                          .filter((s) => s.active !== false)
                          .map((s) => s.height_m),
                      ),
                    )
                      .sort((a, b) => a - b)
                      .map((height_m, i) => ({
                        id: uid(),
                        active: true,
                        height_m,
                        color: COLORS[i % 4],
                      })),
                  }))
                }
              >
                <Antenna />
              </Tool>
              <Tool
                label="Calculer avec la grille 1"
                onClick={() => a.calculate("horizontal")}
              >
                <Play />
              </Tool>
              <Tool
                label="Calculer avec la grille 2"
                onClick={() => a.calculate("horizontal", true)}
              >
                <Grid2X2 />
              </Tool>
            </div>
            <fieldset className="grow">
              <legend>Paramètres des coupes</legend>
              <DataGrid<HorizontalCut>
                label="Coupes horizontales"
                rows={p.horizontal_cuts}
                columns={[
                  { key: "active", label: "✓", kind: "boolean", width: 30 },
                  {
                    key: "height_m",
                    label: "Hauteur, m",
                    kind: "number",
                    min: 0,
                    width: 90,
                  },
                  { key: "color", label: "Couleur", width: 90 },
                ]}
                selected={a.selectedCut}
                onSelect={a.setSelectedCut}
                onUpdate={(id, key, value) =>
                  commit(
                    (p) => ({
                      ...p,
                      horizontal_cuts: p.horizontal_cuts.map((c) =>
                        c.id === id ? { ...c, [key]: value } : c,
                      ),
                    }),
                    "Coupe horizontale modifiée",
                    key !== "color",
                  )
                }
                onAdd={() =>
                  commit((p) => ({
                    ...p,
                    horizontal_cuts: [
                      ...p.horizontal_cuts,
                      {
                        id: uid(),
                        active: true,
                        height_m:
                          p.horizontal_cuts[p.horizontal_cuts.length - 1]
                            ?.height_m ?? 1.5,
                        color: COLORS[0],
                      },
                    ],
                  }))
                }
              />
              <button
                disabled={!a.selectedCut}
                onClick={() =>
                  commit((p) => ({
                    ...p,
                    horizontal_cuts: p.horizontal_cuts.filter(
                      (c) => c.id !== a.selectedCut,
                    ),
                  }))
                }
              >
                Supprimer la coupe sélectionnée
              </button>
            </fieldset>
            <LimitSelector multiple />
            <NumberField
              label="Demi-largeur de la zone, m"
              value={p.settings.radius_m}
              min={5}
              max={20000}
              onChange={(v) => settings({ radius_m: v })}
            />
            <GridSettings />
            {a.cutResults[0]?.grid.fields.length > 0 && (
              <p
                className="hint"
                aria-label="Grille calculée"
                data-grid-width={a.cutResults[0].grid.width}
                data-grid-height={a.cutResults[0].grid.height}
              >
                Grille calculée : {a.cutResults[0].grid.width} ×{" "}
                {a.cutResults[0].grid.height} points
              </p>
            )}
            <CalculateButton kind="horizontal" />
            <fieldset>
              <legend>Visualisation des contours</legend>
              <NumberField
                label="Épaisseur du trait"
                value={p.settings.contour_width ?? 1.4}
                min={0.2}
                max={8}
                onChange={(v) => settings({ contour_width: v })}
              />
              <NumberField
                label="Opacité des contours"
                value={p.settings.contour_opacity ?? 1}
                min={0}
                max={1}
                onChange={(v) => settings({ contour_opacity: v })}
              />
            </fieldset>
          </>
        )}
        {section === "origin" && <OriginPanel />}
        {section === "antennas" && <AntennaLocationsPanel />}
        {section === "buildings" && (
          <>
            <div className="section-tools">
              <Tool
                label="Dessiner un bâtiment"
                onClick={() => {
                  a.setTool("building");
                  a.setDraft([]);
                }}
              >
                <Plus />
              </Tool>
              <Tool
                label="Supprimer le bâtiment"
                disabled={!a.selectedObject}
                onClick={() =>
                  commit((p) => ({
                    ...p,
                    buildings: p.buildings.filter(
                      (b) => b.id !== a.selectedObject,
                    ),
                  }))
                }
              >
                <Trash2 />
              </Tool>
            </div>
            <div className="grow">
              <DataGrid<Building>
                label="Bâtiments"
                rows={p.buildings}
                columns={[
                  { key: "name", label: "N° / Nom", width: 100 },
                  {
                    key: "height_m",
                    label: "Hauteur, m",
                    kind: "number",
                    min: 0,
                    width: 85,
                  },
                  {
                    key: "foundation_m",
                    label: "Altitude base, m",
                    kind: "number",
                    width: 90,
                  },
                  {
                    key: "attenuation_db",
                    label: "Atténuation, dB",
                    kind: "number",
                    min: 0,
                    width: 90,
                  },
                  { key: "active", label: "Écran", kind: "boolean", width: 55 },
                ]}
                selected={a.selectedObject}
                onSelect={a.setSelectedObject}
                onUpdate={(id, key, value) =>
                  commit((p) => ({
                    ...p,
                    buildings: p.buildings.map((b) =>
                      b.id === id ? { ...b, [key]: value } : b,
                    ),
                  }))
                }
              />
            </div>
            <p className="hint">
              Placez au moins trois sommets sur le plan puis cliquez sur «
              Terminer ».
            </p>
            <label>
              Traitement des bâtiments
              <select
                value={p.settings.buildings_mode}
                onChange={(e) =>
                  settings({
                    buildings_mode: e.target
                      .value as PlannerProjectSettingsMode,
                  })
                }
              >
                <option value="ignore">Ignorer</option>
                <option value="screen">Écrans opaques</option>
                <option value="attenuate">Atténuation à l’intérieur</option>
              </select>
            </label>
          </>
        )}
        {section === "terrain" && (
          <>
            <div className="section-tools">
              <Tool
                label="Ajouter un point de terrain sur le plan"
                onClick={() => a.setTool("terrain")}
              >
                <Plus />
              </Tool>
              <Tool
                label="Retirer le dernier point de terrain"
                disabled={!p.terrain_points.length}
                onClick={() =>
                  commit((p) => ({
                    ...p,
                    terrain_points: p.terrain_points.slice(0, -1),
                  }))
                }
              >
                <Trash2 />
              </Tool>
            </div>
            <fieldset className="terrain-curves">
              <legend>Courbes de niveau</legend>
              <DataGrid<TerrainContour>
                label="Courbes de niveau"
                rows={p.terrain_contours ?? []}
                columns={[
                  { key: "name", label: "Nom", width: 130 },
                  {
                    key: "elevation",
                    label: "Altitude, m",
                    kind: "number",
                    width: 90,
                  },
                  {
                    key: "closed",
                    label: "Fermée",
                    kind: "boolean",
                    width: 55,
                  },
                ]}
                selected={a.selectedObject}
                onSelect={a.setSelectedObject}
                onUpdate={(id, key, value) =>
                  commit((v) => ({
                    ...v,
                    terrain_contours: v.terrain_contours?.map((c) =>
                      c.id === id ? { ...c, [key]: value } : c,
                    ),
                  }))
                }
              />
              <button
                disabled={
                  !p.terrain_contours?.some((c) => c.id === a.selectedObject)
                }
                onClick={() =>
                  commit((v) => ({
                    ...v,
                    terrain_contours: v.terrain_contours?.filter(
                      (c) => c.id !== a.selectedObject,
                    ),
                  }))
                }
              >
                Supprimer la courbe sélectionnée
              </button>
            </fieldset>
            <div className="grow">
              <DataGrid<TerrainPoint>
                label="Points du terrain"
                rows={p.terrain_points}
                columns={[
                  { key: "x", label: "X, m", kind: "number", width: 90 },
                  { key: "y", label: "Y, m", kind: "number", width: 90 },
                  {
                    key: "elevation",
                    label: "Altitude, m",
                    kind: "number",
                    width: 90,
                  },
                ]}
                onUpdate={(id, key, value) =>
                  commit((p) => ({
                    ...p,
                    terrain_points: p.terrain_points.map((t) =>
                      t.id === id
                        ? {
                            ...t,
                            [key]: value,
                            ...(key === "x" || key === "y"
                              ? { wm_x: undefined, wm_y: undefined }
                              : {}),
                          }
                        : t,
                    ),
                  }))
                }
              />
            </div>
            <label>
              <input
                type="checkbox"
                checked={p.settings.consider_terrain}
                onChange={(e) =>
                  settings({ consider_terrain: e.target.checked })
                }
              />{" "}
              Prendre en compte le relief
            </label>
            <p className="hint">
              Les points et sommets des courbes de niveau participent à
              l’interpolation triangulée du terrain.
            </p>
          </>
        )}
        {section === "lines" && (
          <>
            <div className="section-tools">
              <button
                onClick={() => {
                  a.setTool("road");
                  a.setDraft([]);
                }}
              >
                ＋ Route
              </button>
              <button
                onClick={() => {
                  a.setTool("barrier");
                  a.setDraft([]);
                }}
              >
                ＋ Clôture
              </button>
              <button onClick={() => a.setTool("label")}>＋ Texte</button>
            </div>
            <DataGrid
              label="Lignes et annotations"
              rows={p.lines}
              columns={[
                { key: "name", label: "Libellé", width: 160 },
                { key: "kind", label: "Type", readOnly: true, width: 70 },
                {
                  key: "width",
                  label: "Épaisseur",
                  kind: "number",
                  min: 0.1,
                  max: 20,
                  width: 70,
                },
                { key: "color", label: "Couleur", width: 80 },
              ]}
              selected={a.selectedObject}
              onSelect={a.setSelectedObject}
              onUpdate={(id, key, value) =>
                commit((p) => ({
                  ...p,
                  lines: p.lines.map((l) =>
                    l.id === id ? { ...l, [key]: value } : l,
                  ),
                }))
              }
            />
            <button
              disabled={!a.selectedObject}
              onClick={() =>
                commit((p) => ({
                  ...p,
                  lines: p.lines.filter((l) => l.id !== a.selectedObject),
                }))
              }
            >
              Supprimer la ligne sélectionnée
            </button>
          </>
        )}
        {section === "calibrate" && (
          <>
            <button
              onClick={() =>
                void task(async () => {
                  const f = await chooseFile(["png", "jpg", "jpeg", "webp"]);
                  if (!f) return null;
                  if (f.bytes.length > 20 * 1024 * 1024)
                    throw new Error("Image trop volumineuse (20 Mo maximum).");
                  const data = await new Promise<string>((resolve, reject) => {
                    const reader = new FileReader();
                    reader.onload = () => resolve(reader.result as string);
                    reader.onerror = reject;
                    reader.readAsDataURL(new Blob([new Uint8Array(f.bytes)]));
                  });
                  const [south, west] = toGeo(-150, -150, p.origin),
                    [north, east] = toGeo(150, 150, p.origin);
                  commit((p) => ({
                    ...p,
                    settings: {
                      ...p.settings,
                      map_provider: "Calibrated map image",
                    },
                    background_image: {
                      data_url: data,
                      west,
                      south,
                      east,
                      north,
                      opacity: 0.7,
                    },
                  }));
                  return f;
                }, "Fond de plan chargé")
              }
            >
              <Upload size={14} /> Charger une image
            </button>
            {p.background_image && (
              <fieldset>
                <legend>Emprise géographique de l’image</legend>
                {(["west", "east", "south", "north", "opacity"] as const).map(
                  (k, i) => (
                    <NumberField
                      key={k}
                      label={
                        [
                          "Longitude ouest",
                          "Longitude est",
                          "Latitude sud",
                          "Latitude nord",
                          "Opacité",
                        ][i]
                      }
                      value={p.background_image![k]}
                      min={k === "opacity" ? 0 : undefined}
                      max={k === "opacity" ? 1 : undefined}
                      onChange={(v) =>
                        commit((p) => ({
                          ...p,
                          background_image: p.background_image
                            ? { ...p.background_image, [k]: v }
                            : undefined,
                        }))
                      }
                    />
                  ),
                )}
                <button
                  onClick={() =>
                    commit((p) => ({ ...p, background_image: undefined }))
                  }
                >
                  Retirer l’image
                </button>
              </fieldset>
            )}
            <p className="hint">
              Renseignez les coordonnées des bords de l’image pour la
              positionner sur le plan.
            </p>
          </>
        )}
      </section>
      <PlannerMap
        project={p}
        contours={contours}
        display={a.display}
        tool={a.tool}
        draft={a.draft}
        onClick={a.mapClick}
        onCursor={(x, y) => a.setCursor([x, y])}
        onFinish={a.finish}
        onSelectSource={a.setSelectedSource}
        editableAntennas={section === "antennas"}
        selectedSourceIds={a.selectedSourceIds}
        onMoveSource={(id, lat, lng) =>
          commit((v) => moveAntennas(v, [id], snapAntenna(v, id, [lat, lng])))
        }
        onSelectPoint={(id) => {
          a.setSelectedGroup("");
          a.setSelectedPoint(id);
        }}
        viewRequest={a.viewRequest}
        onCenterChange={a.setViewCenter}
        selectedObject={a.selectedObject}
        onSelectObject={a.setSelectedObject}
        ruler={a.ruler}
        editableOriginGrid={section === "origin" || section === "horizontal"}
        onRulerChange={a.setRuler}
        onOriginMove={(x, y) => a.changeOrigin(toGeo(x, y, p.origin))}
        onAreaChange={(bounds) => a.settings(bounds)}
      />
    </SplitWorkspace>
  );
}
type PlannerProjectSettingsMode = "ignore" | "screen" | "attenuate";
