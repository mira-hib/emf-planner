import { useState, useCallback } from "react";
import { invoke } from "@tauri-apps/api/core";
import { save, open } from "@tauri-apps/plugin-dialog";
import { writeTextFile, readTextFile } from "@tauri-apps/plugin-fs";
import MapView from "./components/MapView";
import SourcePanel from "./components/SourcePanel";
import ControlPointPanel from "./components/ControlPointPanel";
import Toolbar from "./components/Toolbar";
import type { RadiatingSource, ControlPoint, ProjectData } from "./types";
import "./App.css";

function App() {
  const [project, setProject] = useState<ProjectData>({
    name: "Nouveau Projet",
    description: "",
    sources: [],
    antennas: [],
    control_points: [],
    propagation_model: "FreeSpace",
    safety_limit_v_m: 28.0,
    map_center: [5.345, -4.025],
    map_zoom: 13,
  });

  const [safetyDistances, setSafetyDistances] = useState<Map<string, number>>(new Map());
  const [activeTab, setActiveTab] = useState<"sources" | "control_points">("sources");

  const recalcSafetyZone = useCallback(async (source: RadiatingSource) => {
    const limit: number = await invoke("get_icnirp_limit", {
      frequencyMhz: source.frequency_mhz,
    });
    const dist: number = await invoke("calculate_safety_zone", {
      source,
      limitVM: limit,
    });
    setSafetyDistances((prev) => new Map(prev).set(source.id, dist));
  }, []);

  const addSource = useCallback(async (lat?: number, lng?: number) => {
    const source: RadiatingSource = await invoke("create_source", {
      name: `Source ${project.sources.length + 1}`,
      frequencyMhz: 900.0,
      powerW: 50.0,
      lat: lat ?? project.map_center[0],
      lng: lng ?? project.map_center[1],
    });

    await recalcSafetyZone(source);
    setProject((p) => ({ ...p, sources: [...p.sources, source] }));
  }, [project, recalcSafetyZone]);

  const removeSource = useCallback((id: string) => {
    setProject((p) => ({ ...p, sources: p.sources.filter((s) => s.id !== id) }));
    setSafetyDistances((prev) => {
      const next = new Map(prev);
      next.delete(id);
      return next;
    });
  }, []);

  const updateSource = useCallback(async (id: string, field: keyof RadiatingSource, value: number | string) => {
    setProject((p) => {
      const updated = p.sources.map((s) => (s.id === id ? { ...s, [field]: value } : s));
      const src = updated.find((s) => s.id === id);
      if (src) recalcSafetyZone(src);
      return { ...p, sources: updated };
    });
  }, [recalcSafetyZone]);

  const addControlPoint = useCallback(async (lat: number, lng: number) => {
    const cp: ControlPoint = await invoke("create_control_point", {
      name: `Point ${(project.control_points?.length ?? 0) + 1}`,
      lat,
      lng,
      heightM: 1.5,
    });
    const evaluated: ControlPoint = await invoke("evaluate_control_point", {
      point: cp,
      sources: project.sources,
    });
    setProject((p) => ({ ...p, control_points: [...(p.control_points || []), evaluated] }));
  }, [project]);

  const removeControlPoint = useCallback((id: string) => {
    setProject((p) => ({ ...p, control_points: (p.control_points || []).filter((cp) => cp.id !== id) }));
  }, []);

  const handleMapClick = useCallback((lat: number, lng: number) => {
    if (activeTab === "control_points") {
      addControlPoint(lat, lng);
    } else {
      addSource(lat, lng);
    }
  }, [activeTab, addSource, addControlPoint]);

  const handleSave = useCallback(async () => {
    const path = await save({
      filters: [{ name: "EMF Projet", extensions: ["emfp", "json"] }],
      defaultPath: `${project.name}.emfp`,
    });
    if (path) {
      const json = JSON.stringify(project, null, 2);
      await writeTextFile(path, json);
    }
  }, [project]);

  const handleOpen = useCallback(async () => {
    const path = await open({
      filters: [{ name: "EMF Projet", extensions: ["emfp", "json"] }],
      multiple: false,
    });
    if (path) {
      const content = await readTextFile(path as string);
      const data = JSON.parse(content) as ProjectData;
      setProject(data);
      for (const source of data.sources) {
        recalcSafetyZone(source);
      }
    }
  }, [recalcSafetyZone]);

  const handleExport = useCallback(async () => {
    const path = await save({
      filters: [{ name: "DXF", extensions: ["dxf"] }],
      defaultPath: `${project.name}.dxf`,
    });
    if (path) {
      await invoke("export_dxf", { path, data: project });
    }
  }, [project]);

  return (
    <div className="app">
      <Toolbar
        projectName={project.name}
        onSave={handleSave}
        onOpen={handleOpen}
        onExport={handleExport}
        onSettings={() => {}}
      />
      <div className="app-body">
        <aside className="sidebar">
          <div className="tab-bar">
            <button
              className={`tab-btn ${activeTab === "sources" ? "active" : ""}`}
              onClick={() => setActiveTab("sources")}
            >
              Sources
            </button>
            <button
              className={`tab-btn ${activeTab === "control_points" ? "active" : ""}`}
              onClick={() => setActiveTab("control_points")}
            >
              Points de contrôle
            </button>
          </div>

          {activeTab === "sources" ? (
            <SourcePanel
              sources={project.sources}
              onAdd={() => addSource()}
              onRemove={removeSource}
              onUpdate={updateSource}
            />
          ) : (
            <ControlPointPanel
              points={project.control_points || []}
              onRemove={removeControlPoint}
            />
          )}
        </aside>
        <main className="map-container">
          <MapView
            sources={project.sources}
            controlPoints={project.control_points || []}
            safetyDistances={safetyDistances}
            center={project.map_center}
            zoom={project.map_zoom}
            onMapClick={handleMapClick}
          />
        </main>
      </div>
    </div>
  );
}

export default App;
