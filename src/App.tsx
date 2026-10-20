import { lazy, Suspense } from "react";
import {
  Activity,
  Antenna,
  Building2,
  ChartNoAxesCombined,
  CircleHelp,
  Crosshair,
  FileText,
  FolderOpen,
  Grid2X2,
  Layers,
  LocateFixed,
  MapPin,
  Mountain,
  MousePointer2,
  RadioTower,
  Redo2,
  Ruler,
  Save,
  Scissors,
  Settings,
  Undo2,
  X,
} from "lucide-react";
import { PlannerContext, usePlannerState } from "./lib/usePlanner";
import { chooseFile, importDxf } from "./lib/files";
import { newProject, normalizeProject } from "./lib/project";
import type { Section } from "./types";
import SourcesPage from "./components/SourcesPage";
import MapWorkspace from "./components/MapWorkspace";
import VerticalPage from "./components/VerticalPage";
import {
  LimitsPage,
  ReportPage,
  SettingsPage,
  TablesPage,
} from "./components/ProjectPages";
import CadView from "./components/CadView";
import { Tool } from "./components/Controls";
import FeatureTools from "./components/FeatureTools";
import { ResultsDialog } from "./components/Results";
import { TILES } from "./components/PlannerMap";
import "./App.css";
const Scene3D = lazy(() => import("./components/Scene3D"));
const NAV: { id: Section; label: string; icon: typeof Activity }[] = [
  { id: "sources", label: "Sources rayonnantes", icon: RadioTower },
  { id: "limits", label: "Limites EMF", icon: Activity },
  { id: "origin", label: "Origine et grille", icon: LocateFixed },
  { id: "antennas", label: "Positions des antennes", icon: Antenna },
  { id: "buildings", label: "Bâtiments", icon: Building2 },
  { id: "terrain", label: "Terrain", icon: Mountain },
  { id: "lines", label: "Lignes et annotations", icon: Layers },
  { id: "points", label: "Calculs aux points", icon: MapPin },
  { id: "horizontal", label: "Coupes horizontales", icon: Grid2X2 },
  { id: "vertical", label: "Coupes verticales", icon: Scissors },
  { id: "tables", label: "Tableaux et graphiques", icon: ChartNoAxesCombined },
  { id: "report", label: "Rapport par modèle", icon: FileText },
  { id: "cad", label: "CAO", icon: Ruler },
  { id: "3d", label: "3D", icon: Layers },
  { id: "calibrate", label: "Calibrer le fond de plan", icon: Crosshair },
  { id: "settings", label: "Paramètres", icon: Settings },
];
const MAP_SECTIONS: Section[] = [
  "points",
  "horizontal",
  "origin",
  "antennas",
  "buildings",
  "terrain",
  "lines",
  "calibrate",
];
export default function App() {
  const a = usePlannerState(),
    { project: p, section, dirty, task } = a;
  const title = NAV.find((n) => n.id === section)?.label ?? "";
  return (
    <PlannerContext.Provider value={a}>
      <div className="app desktop-planner">
        <header className="main-toolbar">
          <details className="file-menu">
            <summary role="button" title="Fichier" aria-label="Fichier">
              <FolderOpen size={22} />
              <span>▾</span>
            </summary>
            <div
              className="menu-popup"
              onClick={(e) => {
                if ((e.target as HTMLElement).closest("button"))
                  (e.currentTarget.parentElement as HTMLDetailsElement).open =
                    false;
              }}
            >
              <button
                onClick={() => {
                  if (
                    dirty &&
                    !window.confirm(
                      "Créer un nouveau projet ? Les modifications non enregistrées seront remplacées.",
                    )
                  )
                    return;
                  a.load(newProject());
                }}
              >
                Nouveau
              </button>
              <button onClick={a.openProject}>
                Ouvrir… <kbd>Ctrl+O</kbd>
              </button>
              <button onClick={a.save}>
                Enregistrer… <kbd>Ctrl+S</kbd>
              </button>
              <span title="L’écriture au format binaire de l’original n’est pas encore implémentée. Utilisez .emfp.">
                <button disabled>Enregistrer au format original .xemf</button>
              </span>
              <hr />
              <button
                onClick={() =>
                  void task(async () => {
                    const f = await chooseFile(["dxf"]);
                    if (f)
                      a.commit(importDxf(p, new TextDecoder().decode(f.bytes)));
                    return f;
                  }, "Plan DXF importé")
                }
              >
                Importer un plan DXF…
              </button>
              <button
                onClick={() =>
                  void task(async () => {
                    const r = await fetch("/examples/demonstration.emfp");
                    if (!r.ok) throw new Error("Démonstration indisponible.");
                    if (
                      dirty &&
                      !window.confirm(
                        "Remplacer les modifications par le projet de démonstration ?",
                      )
                    )
                      return null;
                    a.load(normalizeProject(await r.json()));
                  }, "Démonstration ouverte")
                }
              >
                Projet de démonstration
              </button>
            </div>
          </details>
          <Tool label="Enregistrer le projet" onClick={a.save}>
            <Save />
          </Tool>
          <span className="toolbar-separator" />
          <Tool
            label="Annuler la dernière modification"
            disabled={!a.canUndo}
            onClick={() => a.restore()}
          >
            <Undo2 />
          </Tool>
          <Tool
            label="Rétablir"
            disabled={!a.canRedo}
            onClick={() => a.restore(true)}
          >
            <Redo2 />
          </Tool>
          <Tool label="Paramètres" onClick={() => a.setSection("settings")}>
            <Settings />
          </Tool>
          <Tool
            label="À propos et raccourcis"
            onClick={() =>
              a.setStatus(
                "EMF Planner · Ctrl+O : ouvrir · Ctrl+S : enregistrer · Ctrl+Z : annuler · Échap : quitter le dessin",
              )
            }
          >
            <CircleHelp />
          </Tool>
          <span className="project-caption">
            EMF Planner{" "}
            <span>
              — {p.name}
              {dirty ? " *" : ""}
            </span>
          </span>
          <span className="project-count">
            {p.sources.length} sources · {p.control_points.length} points
          </span>
        </header>
        {a.error && (
          <div className="error-banner" role="alert">
            <span>{a.error}</span>
            <button
              aria-label="Fermer le message"
              onClick={() => a.setError("")}
            >
              <X size={15} />
            </button>
          </div>
        )}
        <div className="app-body">
          <aside className="navigation-pane">
            <nav aria-label="Rubriques du projet">
              {NAV.map((n) => (
                <button
                  key={n.id}
                  className={section === n.id ? "active" : ""}
                  onClick={() => {
                    a.setSection(n.id);
                    a.setTool("pan");
                    a.setDraft([]);
                  }}
                >
                  <n.icon size={14} />
                  <span>{n.label}</span>
                </button>
              ))}
            </nav>
            <div className="display-options">
              <div className="pane-caption">Options d’affichage</div>
              <label>
                <input
                  type="checkbox"
                  checked={p.settings.show_basemap}
                  onChange={(e) =>
                    a.settings({ show_basemap: e.target.checked })
                  }
                />
                Fond de carte
              </label>
              {(Object.keys(a.display) as (keyof typeof a.display)[]).map(
                (key, i) => (
                  <label key={key}>
                    <input
                      type="checkbox"
                      checked={a.display[key]}
                      onChange={(e) =>
                        a.setDisplay((p) => ({ ...p, [key]: e.target.checked }))
                      }
                    />
                    {
                      [
                        "Grille",
                        "Bâtiments",
                        "Relief",
                        "Lignes et annotations",
                        "Antennes",
                        "Points de contrôle",
                        "Contours EMF",
                        "Libellés",
                        "Azimuts",
                      ][i]
                    }
                  </label>
                ),
              )}
              <select
                aria-label="Fond de carte"
                value={p.settings.map_provider}
                onChange={(e) => a.settings({ map_provider: e.target.value })}
              >
                {Object.keys(TILES).map((name) => (
                  <option key={name}>{name}</option>
                ))}
              </select>
            </div>
            <div className="navigation-bottom">
              <MousePointer2 size={12} /> Coordonnées locales en mètres
            </div>
          </aside>
          <main className="workspace" aria-label={title}>
            <FeatureTools section={section} />
            <div className="workspace-content">
              {section === "sources" && <SourcesPage />}
              {MAP_SECTIONS.includes(section) && (
                <MapWorkspace title={title} />
              )}{" "}
              {section === "vertical" && <VerticalPage />}
              {section === "limits" && <LimitsPage />}
              {section === "tables" && <TablesPage />}
              {section === "report" && <ReportPage />}
              {section === "cad" && <CadView />}
              {section === "settings" && <SettingsPage />}
              {section === "3d" && (
                <Suspense
                  fallback={
                    <div className="empty-workspace">
                      Chargement de la vue 3D…
                    </div>
                  }
                >
                  <Scene3D
                    project={p}
                    contours={a.cutResults.map((r) => r.contours)}
                  />
                </Suspense>
              )}
            </div>
          </main>
        </div>
        <footer className="statusbar">
          <span className="status-message" role="status">
            {a.status}
          </span>
          <span>
            X = {a.cursor[0].toFixed(1)} m · Y = {a.cursor[1].toFixed(1)} m
          </span>
          <span>{a.elapsed ? `${a.elapsed.toFixed(3)} s` : ""}</span>
          {a.busy ? (
            <>
              <progress value={a.progress} max={100} />
              <button onClick={a.cancel}>Annuler</button>
            </>
          ) : (
            <span className="status-ready">{dirty ? "Modifié" : "Prêt"}</span>
          )}
        </footer>
        {a.resultOpen && <ResultsDialog />}
      </div>
    </PlannerContext.Provider>
  );
}
