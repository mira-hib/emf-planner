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
    [selectedSource, setSelectedSource] = useState(""),
    [selectedPoint, setSelectedPoint] = useState(""),
    [selectedObject, setSelectedObject] = useState(""),
    [selectedCut, setSelectedCut] = useState(project.vertical_cuts[0]?.id ?? "");
  const [command, setCommand] = useState("");
  const [selectedGroup, setSelectedGroup] = useState("");
  const [viewRequest, setViewRequest] = useState<{
    center: [number, number];
    version: number;
  } | null>(null);
  const [viewCenter, setViewCenter] = useState<[number, number]>([0, 0]);
  const [ruler, setRuler] = useState<[number, number][]>([]);
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
  const worker = useRef<Worker | null>(null),
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
  };
  const commit = useCallback(
    (
      action: PlannerProject | ((p: PlannerProject) => PlannerProject),
      message = "Modifications non enregistrées",
      invalidate = true,
    ) => {
      const before = projectRef.current,
        changed = typeof action === "function" ? action(before) : action,
        next = invalidate
          ? { ...changed, saved_cut_results: undefined }
          : changed;
      past.current.push(before);
      if (past.current.length > 40) past.current.shift();
      future.current = [];
      projectRef.current = next;
      setProject(next);
      setDirty(true);
      setStatus(message);
      if (invalidate) resetCalculations();
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
  function load(p: PlannerProject) {
    resetCalculations();
    past.current = [];
    future.current = [];
    projectRef.current = p;
    setProject(p);
    setCutResults(p.saved_cut_results?.horizontal ?? []);
    setVerticalResults(p.saved_cut_results?.vertical ?? []);
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
      if (done && p === projectRef.current) setDirty(false);
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
    const appearance = [
      "map_provider",
      "show_basemap",
      "coordinate_format",
      "contour_width",
      "contour_opacity",
      "display_options",
      "building_labels",
      "control_point_labels",
    ];
    commit(
      (p) => ({
        ...p,
        settings: {
          ...p.settings,
          ...patch,
          ...(patch.radius_m !== undefined
            ? { calculation_area_m: undefined }
            : {}),
        },
      }),
      "Modifications non enregistrées",
      Object.keys(patch).some((k) => !appearance.includes(k)),
    );
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
    const s = newSource(project.origin, `Source ${project.sources.length + 1}`),
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
  ) {
    const project = projectRef.current;
    if (!project.sources.some((s) => s.active !== false)) {
      setError("Ajoutez et activez au moins une source avant de calculer.");
      return;
    }
    if (kind === "points" && !project.control_points.length) {
      setError("Ajoutez un point de contrôle avant de calculer.");
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
    worker.current = new Worker(
      new URL("./calculation.worker.ts", import.meta.url),
      { type: "module" },
    );
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
    worker.current.postMessage({ id, kind, project: calculatedProject, fine });
  }
  function cancel() {
    worker.current?.terminate();
    job.current++;
    setBusy(false);
    setStatus("Calcul annulé");
  }
  function finish() {
    if (tool === "ruler") {
      setRuler(draft);
      if (draft.length >= 2)
        setStatus(
          `Distance mesurée : ${draft
            .slice(1)
            .reduce(
              (s, p, i) =>
                s + Math.hypot(p[0] - draft[i][0], p[1] - draft[i][1]),
              0,
            )
            .toFixed(2)} m`,
        );
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
    const convert = (xy: [number, number]): [number, number] => {
      const [lat, lng] = toGeo(xy[0], xy[1], project.origin);
      return toLocal(lat, lng, origin);
    };
    commit((p) => ({
      ...p,
      origin,
      map_center: origin,
      sources: p.sources.map((s) => {
        const [x_m, y_m] = toLocal(s.lat, s.lng, origin);
        return { ...s, x_m, y_m };
      }),
      control_points: p.control_points.map((c) => {
        const [x_m, y_m] = toLocal(c.lat, c.lng, origin);
        return { ...c, x_m, y_m };
      }),
      buildings: p.buildings.map((b) => ({
        ...b,
        points: b.points.map(convert),
      })),
      lines: p.lines.map((l) => ({ ...l, points: l.points.map(convert) })),
      vertical_cuts: p.vertical_cuts.map((c) => {
        const [x, y] = convert([c.x, c.y]);
        return { ...c, x, y };
      }),
      terrain_points: p.terrain_points.map((t) => {
        const [x, y] = convert([t.x, t.y]);
        return { ...t, x, y };
      }),
      terrain_contours: p.terrain_contours?.map((c) => ({
        ...c,
        points: c.points.map(convert),
      })),
    }));
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
    setViewCenter,
    ruler,
    section,
    setSection,
    source,
    point,
    selectedSource,
    setSelectedSource,
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
