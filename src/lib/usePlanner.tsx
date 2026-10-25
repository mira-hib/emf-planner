import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useRef,
  useState,
} from "react";
import {
  newPoint,
  newProject,
  newSource,
  normalizeProject,
  uid,
} from "./project";
import { chooseFile, readProject, saveProject } from "./files";
import { terrainHeight, toGeo, toLocal } from "./geometry";
import { rebaseProject } from "./origin-grid";
import { CalculationPool } from "./calculation-pool";
import { readPreferences, writePreferences } from "./preferences";
import { configureNetwork } from "./network";
import { isTauri } from "@tauri-apps/api/core";
import type {
  Building,
  Contour,
  ControlPoint,
  FieldGrid,
  ExposureVolume,
  PlannerProject,
  PointResult,
  RadiatingSource,
  Section,
} from "../types";
import type { DisplayOptions, DrawTool } from "../components/PlannerMap";
import { PENDING_COMMANDS } from "./capabilities";
export interface CutResult {
  id: string;
  cut_id?: string;
  limit_index?: number;
  grid: FieldGrid;
  contours: Contour;
  contours_only?: boolean;
}
function initialProject() {
  try {
    const saved = localStorage.getItem("emf-planner-autosave-v2");
    if (saved) return normalizeProject(JSON.parse(saved));
  } catch {
    /* Corrupt autosave must not block startup. */
  }
  return newProject();
}
export function usePlannerState() {
  const [project, setProject] = useState(initialProject),
    projectRef = useRef(project),
    past = useRef<PlannerProject[]>([]),
    future = useRef<PlannerProject[]>([]);
  const [section, setSection] = useState<Section>("sources"),
    [selectedSource, setActiveSource] = useState(project.sources[0]?.id ?? ""),
    [selectedSourceIds, setSelectedSourceIds] = useState<string[]>(
      project.sources[0] ? [project.sources[0].id] : [],
    ),
    [selectedPoint, setSelectedPoint] = useState(""),
    [selectedObject, setSelectedObject] = useState(""),
    [selectedCut, setSelectedCut] = useState(
      project.vertical_cuts[0]?.id ?? "",
    );
  const [command, setCommand] = useState("");
  function setSelectedSource(id: string) {
    setActiveSource(id);
    setSelectedSourceIds(id ? [id] : []);
  }
  function setSourceSelection(ids: string[], activeId?: string) {
    setSelectedSourceIds(ids);
    if (activeId) setActiveSource(activeId);
  }
  const [selectedGroup, setSelectedGroup] = useState("");
  const [viewRequest, setViewRequest] = useState<{
    center: [number, number];
    version: number;
  } | null>(null);
  const [viewCenter, setViewCenter] = useState<[number, number]>([0, 0]);
  const [viewBounds, setViewBounds] = useState<{ west: number; south: number; east: number; north: number; metresPerPixel?: number }>({ west: -150, south: -150, east: 150, north: 150 });
  const [ruler, setRuler] = useState<[number, number][]>([]);
  function toggleRuler() {
    if (ruler.length === 2) {
      setRuler([]);
      setTool("pan");
    } else {
      setRuler([
        [viewCenter[0] - 25, viewCenter[1]],
        [viewCenter[0] + 25, viewCenter[1]],
      ]);
      setDraft([]);
      setTool("ruler");
    }
  }
  function centerView(center: [number, number]) {
    setViewRequest({ center, version: Date.now() });
  }
  function openCommand(name: string) {
    if (PENDING_COMMANDS[name]) {
      setStatus(PENDING_COMMANDS[name]);
      return;
    }
    if (name === "Mise en page CAO avancée") {
      setSection("cad");
      return;
    }
    setResultOpen(false);
    setCommand(name);
  }
  const [results, setResults] = useState<PointResult[]>([]),
    [volume, setVolume] = useState<ExposureVolume | null>(null),
    [cutResults, setCutResults] = useState<CutResult[]>(
      project.saved_cut_results?.horizontal ?? [],
    ),
    [verticalResults, setVerticalResults] = useState<CutResult[]>(
      project.saved_cut_results?.vertical ?? [],
    ),
    [resultOpen, setResultOpen] = useState(false),
    [dirty, setDirty] = useState(false),
    [status, setStatus] = useState("Prêt"),
    [error, setError] = useState(""),
    [busy, setBusy] = useState(false),
    [progress, setProgress] = useState(0),
    [elapsed, setElapsed] = useState(0);
  const [cutResultsStale, setCutResultsStale] = useState(project.saved_cut_results?.stale ?? false);
  const [display, setDisplay] = useState<DisplayOptions>({
      grid: false,
      buildings: true,
      terrain: false,
      triangulation: false,
      lines: true,
      sources: true,
      points: true,
      contours: true,
      labels: true,
      azimuths: true,
      ...project.settings.display_options,
    }),
    [tool, setTool] = useState<DrawTool>("pan"),
    [draft, setDraft] = useState<[number, number][]>([]),
    [cursor, setCursor] = useState<[number, number]>([0, 0]);
  const worker = useRef<CalculationPool | null>(null),
    job = useRef(0),
    started = useRef(0);
  const source =
      project.sources.find((s) => s.id === selectedSource) ??
      project.sources[0],
    point =
      project.control_points.find(
        (p) =>
          p.id === selectedPoint &&
          (!selectedGroup || p.group === selectedGroup),
      ) ??
      project.control_points.find(
        (p) => !selectedGroup || p.group === selectedGroup,
      );
  const resetCalculations = () => {
    worker.current?.terminate();
    job.current++;
    setBusy(false);
    setResults([]);
    setCutResults([]);
    setVerticalResults([]);
    setVolume(null);
    setCutResultsStale(false);
  };
  const commit = useCallback(
    (
      action: PlannerProject | ((p: PlannerProject) => PlannerProject),
      message = "Modifications non enregistrées",
      invalidate: boolean | "stale-cuts" = true,
    ) => {
      const before = projectRef.current,
        changed = typeof action === "function" ? action(before) : action,
        next = invalidate === true
          ? { ...changed, saved_cut_results: undefined }
          : invalidate === "stale-cuts" && changed.saved_cut_results
            ? { ...changed, saved_cut_results: { ...changed.saved_cut_results, stale: true } }
          : changed;
      past.current.push(before);
      if (past.current.length > 40) past.current.shift();
      future.current = [];
      projectRef.current = next;
      if (next.owner_names !== before.owner_names || next.owner_colors !== before.owner_colors)
        writePreferences({ owner_names: next.owner_names, owner_colors: next.owner_colors });
      setProject(next);
      setDirty(true);
      setStatus(message);
      if (invalidate === true) resetCalculations();
      else if (invalidate === "stale-cuts") {
        worker.current?.terminate();
        job.current++;
        setBusy(false);
        setResults([]);
        setVolume(null);
        setCutResultsStale(!!(next.saved_cut_results?.horizontal.length || next.saved_cut_results?.vertical.length));
      }
    },
    [],
  );
  const restore = useCallback((redo = false) => {
    const next = (redo ? future.current : past.current).pop();
    if (!next) return;
    (redo ? past.current : future.current).push(projectRef.current);
    projectRef.current = next;
    setProject(next);
    setDirty(true);
    resetCalculations();
    setCutResults(next.saved_cut_results?.horizontal ?? []);
    setVerticalResults(next.saved_cut_results?.vertical ?? []);
    setCutResultsStale(next.saved_cut_results?.stale ?? false);
    setStatus(redo ? "Modification rétablie" : "Modification annulée");
  }, []);
  const fail = useCallback((e: unknown) => {
    setError(e instanceof Error ? e.message : String(e));
    setStatus("L’opération n’a pas abouti");
  }, []);
  const task = useCallback(
    async (action: () => Promise<unknown>, message: string) => {
      try {
        setError("");
        const done = await action();
        if (done !== false && done !== null) setStatus(message);
      } catch (e) {
        fail(e);
      }
    },
    [fail],
  );
  useEffect(() => {
    const timer = setTimeout(() => {
      try {
        localStorage.setItem(
          "emf-planner-autosave-v2",
          JSON.stringify(project),
        );
      } catch {
        setStatus(
          "Sauvegarde locale indisponible. Enregistrez le projet dans un fichier.",
        );
      }
    }, 600);
    return () => clearTimeout(timer);
  }, [project]);
  useEffect(() => {
    const handler = (e: BeforeUnloadEvent) => {
      if (dirty) e.preventDefault();
    };
    window.addEventListener("beforeunload", handler);
    return () => window.removeEventListener("beforeunload", handler);
  }, [dirty]);
  useEffect(() => () => worker.current?.terminate(), []);
  useEffect(() => { if (isTauri()) void configureNetwork(readPreferences()).catch(fail); }, [fail]);
  function load(p: PlannerProject) {
    resetCalculations();
    past.current = [];
    future.current = [];
    projectRef.current = p;
    setProject(p);
    setCutResults(p.saved_cut_results?.horizontal ?? []);
    setVerticalResults(p.saved_cut_results?.vertical ?? []);
    setCutResultsStale(p.saved_cut_results?.stale ?? false);
    setDisplay((d) => ({ ...d, ...p.settings.display_options }));
    setSelectedSource(p.sources[0]?.id ?? "");
    setSelectedPoint(p.control_points[0]?.id ?? "");
    setSelectedObject("");
    setSelectedCut(p.vertical_cuts[0]?.id ?? "");
    setSelectedGroup("");
    setViewCenter([0, 0]);
    centerView([0, 0]);
    setRuler([]);
    setCommand("");
    setTool("pan");
    setDraft([]);
    setDirty(false);
    setStatus(`${p.name} — projet chargé`);
    setError("");
  }
  async function openProject() {
    await task(async () => {
      const file = await chooseFile(["emfp", "json", "xemf"]);
      if (file) {
        if (
          dirty &&
          !window.confirm(
            "Remplacer le projet actuel et ses modifications non enregistrées ?",
          )
        )
          return null;
        load(readProject(file.name, file.bytes));
      }
      return file;
    }, "Projet ouvert");
  }
  async function save() {
    await task(async () => {
      const p = projectRef.current,
        done = await saveProject(p);
      if (done && p === projectRef.current) {
        setDirty(false);
        try { localStorage.setItem("emf-planner-autosave-v2", JSON.stringify(p)); }
        catch { /* The project file is saved even if browser storage is full. */ }
      }
      return done;
    }, "Projet enregistré");
  }
  const saveRef = useRef(save),
    openRef = useRef(openProject);
  saveRef.current = save;
  openRef.current = openProject;
  useEffect(() => {
    const key = (e: KeyboardEvent) => {
      if (e.ctrlKey || e.metaKey) {
        const k = e.key.toLowerCase();
        if (k === "s") {
          e.preventDefault();
          void saveRef.current();
        }
        if (k === "o") {
          e.preventDefault();
          void openRef.current();
        }
        if (
          k === "z" &&
          !(e.target instanceof HTMLInputElement) &&
          !(e.target instanceof HTMLTextAreaElement)
        ) {
          e.preventDefault();
          restore(e.shiftKey);
        }
        if (k === "y") {
          e.preventDefault();
          restore(true);
        }
      }
      if (e.key === "Escape") {
        setResultOpen(false);
        setTool("pan");
        setDraft([]);
      }
    };
    window.addEventListener("keydown", key);
    return () => window.removeEventListener("keydown", key);
  }, [restore]);
  function settings(patch: Partial<PlannerProject["settings"]>) {
    if (patch.coordinate_format) writePreferences({ coordinate_format: patch.coordinate_format });
    const appearance = [
      "map_provider",
      "show_basemap",
      "coordinate_format",
      "contour_width",
      "contour_opacity",
      "display_options",
      "building_labels",
      "control_point_labels",
      "site_coordinates",
      "show_calculation_area",
      "show_origin",
      "antenna_capture_distance",
      "vertical_display",
      "terrain_line_height_m",
      "cut_add_owner",
      "cut_use_antenna_names",
    ];
    commit(
      (p) => ({
        ...p,
        settings: {
          ...p.settings,
          ...patch,
          ...(patch.radius_m !== undefined
            ? { calculation_area_m: undefined, calculation_area_wm: undefined }
            : {}),
        },
      }),
      "Modifications non enregistrées",
      Object.keys(patch).some((k) => !appearance.includes(k)),
    );
  }
  function setMapView(center: [number, number], zoom: number) {
    const before = projectRef.current;
    if (before.map_zoom === zoom && before.map_center[0] === center[0] && before.map_center[1] === center[1]) return;
    const next = { ...before, map_center: center, map_zoom: zoom };
    projectRef.current = next;
    setProject(next);
  }
  function updateSource(
    id: string,
    key: keyof RadiatingSource,
    value: unknown,
  ) {
    commit((p) => ({
      ...p,
      sources: p.sources.map((s) => {
        if (s.id !== id) return s;
        const next = { ...s, [key]: value };
        if (key === "x_m" || key === "y_m") {
          const [lat, lng] = toGeo(next.x_m ?? 0, next.y_m ?? 0, p.origin);
          Object.assign(next, { lat, lng });
        }
        if (key === "lat" || key === "lng") {
          const [x_m, y_m] = toLocal(next.lat, next.lng, p.origin);
          Object.assign(next, { x_m, y_m });
        }
        return next;
      }),
    }));
  }
  function rotateSourcePattern(delta: number) {
    if (!source?.pattern_v?.length || !Number.isInteger(delta)) return;
    commit((p) => ({
      ...p,
      sources: p.sources.map((s) =>
        s.id !== source.id || !s.pattern_v
          ? s
          : {
              ...s,
              electrical_downtilt_deg: s.electrical_downtilt_deg + delta,
              pattern_v: s.pattern_v.map(
                (_, i, values) =>
                  values[
                    (((i + delta) % values.length) + values.length) %
                      values.length
                  ],
              ),
            },
      ),
    }));
  }
  function updateDisplay(
    action: DisplayOptions | ((before: DisplayOptions) => DisplayOptions),
  ) {
    const next = typeof action === "function" ? action(display) : action;
    setDisplay(next);
    commit(
      (p) => ({ ...p, settings: { ...p.settings, display_options: next } }),
      "Affichage modifié",
      false,
    );
  }
  function addSource(x = 0, y = 0) {
    const s = newSource(project.origin),
      [lat, lng] = toGeo(x, y, project.origin);
    Object.assign(s, { lat, lng, x_m: x, y_m: y });
    commit((p) => ({ ...p, sources: [...p.sources, s] }));
    setSelectedSource(s.id);
    setTool("pan");
  }
  function addPoint(x = 0, y = 0, group = "Groupe 1") {
    const c = newPoint(project.origin, `${project.control_points.length + 1}`),
      [lat, lng] = toGeo(x, y, project.origin);
    Object.assign(c, { lat, lng, x_m: x, y_m: y, group });
    commit((p) => ({ ...p, control_points: [...p.control_points, c] }));
    setSelectedPoint(c.id);
    setTool("pan");
  }
  function updatePoint(id: string, key: keyof ControlPoint, value: unknown) {
    commit((p) => ({
      ...p,
      control_points: p.control_points.map((c) => {
        if (c.id !== id) return c;
        const n = { ...c, [key]: value };
        if (key === "x_m" || key === "y_m") {
          const [lat, lng] = toGeo(n.x_m, n.y_m, p.origin);
          Object.assign(n, { lat, lng });
        }
        if (key === "lat" || key === "lng") {
          const [x_m, y_m] = toLocal(n.lat, n.lng, p.origin);
          Object.assign(n, { x_m, y_m });
        }
        return n;
      }),
    }));
  }
  function calculate(
    kind: "points" | "horizontal" | "vertical" | "volume",
    fine = false,
    onlyId?: string,
    heightRange?: number[],
  ) {
    const project = projectRef.current;
    if (kind !== "points" && !project.sources.some((s) => s.active !== false)) {
      setError("Ajoutez et activez au moins une source avant de calculer.");
      return;
    }
    if (kind === "points" && !project.control_points.length) {
      setError("Ajoutez un point de contrôle avant de calculer.");
      return;
    }
    if (
      kind === "points" &&
      !project.control_points.some((p) => p.group_active !== false)
    ) {
      setError("Activez au moins un groupe de points avant de calculer.");
      return;
    }
    const cuts =
      kind === "horizontal" ? project.horizontal_cuts : project.vertical_cuts;
    if (
      (kind === "horizontal" || kind === "vertical") &&
      !cuts.some((c) => c.active)
    ) {
      setError("Ajoutez et activez au moins une coupe.");
      return;
    }
    worker.current?.terminate();
    worker.current = new CalculationPool();
    const id = ++job.current;
    started.current = performance.now();
    setBusy(true);
    setProgress(0);
    setError("");
    setStatus("Calcul en cours…");
    worker.current.onmessage = (e) => {
      if (e.data.id !== job.current) return;
      if (e.data.progress !== undefined) {
        setProgress(e.data.progress);
        return;
      }
      setBusy(false);
      setElapsed((performance.now() - started.current) / 1000);
      if (e.data.error) {
        fail(new Error(e.data.error));
        return;
      }
      if (kind === "volume") setVolume(e.data.result);
      else if (kind === "points") {
        setResults(e.data.result);
        setResultOpen(true);
      } else if (kind === "horizontal") setCutResults(e.data.result);
      else {
        setVerticalResults((previous) =>
          onlyId
            ? [...previous.filter((r) => r.cut_id !== onlyId), ...e.data.result]
            : e.data.result,
        );
        setSelectedCut(e.data.result[0]?.cut_id ?? e.data.result[0]?.id ?? "");
      }
      setProgress(100);
      if (kind === "vertical" || kind === "horizontal") setCutResultsStale(false);
      if (kind === "vertical" || kind === "horizontal") {
        const before = projectRef.current;
        const saved = before.saved_cut_results ?? { horizontal: [], vertical: [] };
        const compact = (e.data.result as CutResult[]).map(r => ({ ...r, contours_only: true as const,
          grid: { ...r.grid, fields: [], values: [] } }));
        const next = { ...before, saved_cut_results: { ...saved, stale: false,
          [kind]: kind === "vertical" && onlyId ? [...saved.vertical.filter(r => r.cut_id !== onlyId), ...compact] : compact } };
        projectRef.current = next;
        setProject(next);
        setDirty(true);
      }
      setStatus("Calcul terminé");
      worker.current?.terminate();
    };
    worker.current.onerror = (e) => {
      setBusy(false);
      fail(new Error(e.message));
    };
    const calculatedProject = onlyId
      ? {
          ...project,
          ...(kind === "points"
            ? {
                control_points: project.control_points.filter(
                  (p) => p.id === onlyId,
                ),
              }
            : {
                vertical_cuts: project.vertical_cuts
                  .filter((c) => c.id === onlyId)
                  .map((c) => ({ ...c, active: true })),
              }),
        }
      : project;
    worker.current.postMessage({
      id,
      kind,
      project: calculatedProject,
      fine,
      heightRange,
    });
  }
  function cancel() {
    worker.current?.terminate();
    job.current++;
    setBusy(false);
    setStatus("Calcul annulé");
  }
  function finish() {
    if (tool === "ruler") {
      setTool("pan");
      setDraft([]);
      return;
    }
    if (tool === "building" && draft.length < 3) {
      setError("Un bâtiment nécessite au moins trois sommets.");
      return;
    }
    if (tool === "terrain-contour" && draft.length < 2) {
      setError("Une courbe de niveau nécessite au moins deux sommets.");
      return;
    }
    if ((tool === "road" || tool === "barrier") && draft.length < 2) {
      setError("Une ligne nécessite au moins deux points.");
      return;
    }
    if (tool === "terrain-contour") {
      const contour = {
        id: uid(),
        name: `Courbe ${(project.terrain_contours?.length ?? 0) + 1}`,
        elevation: terrainHeight(project, ...draft[0]),
        closed: false,
        points: draft,
      };
      commit((p) => ({
        ...p,
        terrain_contours: [...(p.terrain_contours ?? []), contour],
      }));
      setSelectedObject(contour.id);
      setDisplay((d) => ({ ...d, terrain: true }));
    } else if (tool === "building") {
      const b: Building = {
        id: uid(),
        name: `${project.buildings.length + 1}`,
        height_m: 10,
        foundation_m: terrainHeight(project, draft[0][0], draft[0][1]),
        points: draft,
        active: true,
        attenuation_db: 20,
      };
      commit((p) => ({ ...p, buildings: [...p.buildings, b] }));
      setSelectedObject(b.id);
    } else if (tool === "road" || tool === "barrier")
      commit((p) => ({
        ...p,
        lines: [
          ...p.lines,
          {
            id: uid(),
            name: `Ligne ${p.lines.length + 1}`,
            kind: tool,
            color: "#555555",
            points: draft,
            width: 1,
          },
        ],
      }));
    setDraft([]);
    setTool("pan");
  }
  function mapClick(x: number, y: number) {
    x = Math.round(x * 10) / 10;
    y = Math.round(y * 10) / 10;
    if (tool === "ruler") {
      setRuler((points) => [points[0] ?? [x, y], [x, y]]);
      return;
    }
    if (tool === "source") addSource(x, y);
    else if (tool === "point") addPoint(x, y);
    else if (tool === "terrain") {
      commit((p) => ({
        ...p,
        terrain_points: [
          ...p.terrain_points,
          { id: uid(), x, y, elevation: terrainHeight(p, x, y) },
        ],
      }));
      setTool("pan");
    } else if (tool === "label") {
      commit((p) => ({
        ...p,
        lines: [
          ...p.lines,
          {
            id: uid(),
            name: "Annotation",
            kind: "label",
            points: [[x, y]],
            color: "#333333",
            width: 1,
          },
        ],
      }));
      setTool("pan");
    } else if (tool !== "pan") setDraft((d) => [...d, [x, y]]);
  }
  function changeOrigin(origin: [number, number]) {
    if (Math.abs(origin[1]) > 180)
      origin = [origin[0], ((((origin[1] + 180) % 360) + 360) % 360) - 180];
    const before = projectRef.current;
    const center = toGeo(...viewCenter, before.origin);
    commit((p) => ({ ...rebaseProject(p, origin), map_center: center }));
    setDraft([]);
    setRuler([]);
    setTool("pan");
    centerView(toLocal(...center, origin));
  }
  return {
    project,
    command,
    setCommand,
    openCommand,
    selectedGroup,
    setSelectedGroup,
    viewRequest,
    centerView,
    viewCenter,
    viewBounds,
    setViewBounds,
    setMapView,
    cutResultsStale,
    setViewCenter,
    ruler,
    setRuler,
    toggleRuler,
    section,
    setSection,
    source,
    point,
    selectedSource,
    setSelectedSource,
    selectedSourceIds,
    setSourceSelection,
    selectedPoint,
    setSelectedPoint,
    selectedObject,
    setSelectedObject,
    selectedCut,
    setSelectedCut,
    results,
    cutResults,
    verticalResults,
    volume,
    resultOpen,
    setResultOpen,
    dirty,
    status,
    setStatus,
    error,
    setError,
    busy,
    progress,
    elapsed,
    display,
    setDisplay: updateDisplay,
    tool,
    setTool,
    draft,
    setDraft,
    cursor,
    setCursor,
    commit,
    restore,
    fail,
    task,
    load,
    openProject,
    save,
    settings,
    updateSource,
    rotateSourcePattern,
    addSource,
    addPoint,
    updatePoint,
    calculate,
    cancel,
    finish,
    mapClick,
    changeOrigin,
    canUndo: past.current.length > 0,
    canRedo: future.current.length > 0,
  };
}
export type Planner = ReturnType<typeof usePlannerState>;
export const PlannerContext = createContext<Planner | null>(null);
export function usePlanner() {
  const p = useContext(PlannerContext);
  if (!p) throw new Error("Planner provider missing");
  return p;
}
