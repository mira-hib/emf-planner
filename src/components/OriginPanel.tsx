import { useState } from "react";
import { Ruler } from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
import { formatCoordinate } from "../lib/coordinates";
import { terrainHeight } from "../lib/geometry";
import {
  areaExtents,
  areaFromExtents,
  calculationArea,
  horizontalDimensions,
  type AreaExtents,
} from "../lib/origin-grid";
import { NumberField } from "./Controls";
import Modal from "./Modal";

export function GridSettings() {
  const { project, settings } = usePlanner();
  const divisions = project.settings.horizontal_grid_divisions;
  return (
    <fieldset>
      <legend>Résolution des calculs horizontaux</legend>
      <label>
        Définition de la grille
        <select
          aria-label="Définition de la grille"
          value={divisions ? "divisions" : "step"}
          onChange={(e) =>
            settings({
              horizontal_grid_divisions:
                e.target.value === "divisions" ? [600, 200] : undefined,
            })
          }
        >
          <option value="divisions">Divisions du grand axe</option>
          <option value="step">Pas en mètres</option>
        </select>
      </label>
      {divisions ? (
        divisions.map((value, i) => (
          <div key={i}>
            <NumberField
              label={`Divisions grille ${i + 1}`}
              value={value}
              min={50}
              max={1000}
              step="1"
              onChange={(v) =>
                settings({
                  horizontal_grid_divisions: divisions.map((n, j) =>
                    i === j ? Math.round(v) : n,
                  ) as [number, number],
                })
              }
            />
            <p className="hint">
              {(() => {
                try {
                  const d = horizontalDimensions(project, value);
                  return `${d.width} × ${d.height} points · ${d.dx.toFixed(2)} × ${d.dy.toFixed(2)} m`;
                } catch {
                  return "Zone trop étroite pour cette résolution.";
                }
              })()}
            </p>
          </div>
        ))
      ) : (
        <>
          <NumberField
            label="Pas grille 1, m"
            value={project.settings.grid_step_m}
            min={0.2}
            onChange={(v) => settings({ grid_step_m: v })}
          />
          <NumberField
            label="Pas grille 2, m"
            value={project.settings.fine_step_m}
            min={0.2}
            onChange={(v) => settings({ fine_step_m: v })}
          />
        </>
      )}
    </fieldset>
  );
}

export function AreaSizeDialog({ close }: { close: () => void }) {
  const { project, settings } = usePlanner();
  const [extents, setExtents] = useState(areaExtents(project));
  const [error, setError] = useState("");
  const labels: Record<keyof AreaExtents, string> = {
    left: "À gauche, m",
    right: "À droite, m",
    top: "En haut, m",
    bottom: "En bas, m",
  };
  function apply() {
    try {
      settings(areaFromExtents(project.origin, extents));
      close();
    } catch (e) {
      setError(e instanceof Error ? e.message : String(e));
    }
  }
  return (
    <Modal title="Dimensions de la zone de calcul" onClose={close}>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          apply();
        }}
      >
        <p>Dimensions par rapport à l’origine des coordonnées locales.</p>
        <div className="area-size-fields">
          {(["top", "left", "right", "bottom"] as const).map((key) => (
            <label key={key} className={`area-size-${key}`}>
              {labels[key]}
              <input
                aria-label={labels[key]}
                inputMode="decimal"
                value={Number.isNaN(extents[key]) ? "" : extents[key]}
                onChange={(e) =>
                  setExtents((v) => ({
                    ...v,
                    [key]: e.target.value.trim()
                      ? Number(e.target.value.replace(",", "."))
                      : NaN,
                  }))
                }
              />
            </label>
          ))}
          <span className="area-size-origin">
            ＋<small>Origine</small>
          </span>
        </div>
        {error && (
          <p className="error" role="alert">
            {error}
          </p>
        )}
        <div className="dialog-actions">
          <button type="button" onClick={close}>
            Annuler
          </button>
          <button type="submit">Appliquer les dimensions</button>
        </div>
      </form>
    </Modal>
  );
}

export function OriginPanel() {
  const a = usePlanner(),
    { project: p, settings } = a;
  const site = p.settings.site_coordinates ?? p.origin;
  const format = p.settings.coordinate_format ?? "decimal";
  const area = calculationArea(p);
  return (
    <div className="origin-panel">
      <fieldset>
        <legend>Origine des coordonnées locales</legend>
        <dl className="origin-coordinates">
          <dt>Latitude</dt>
          <dd>
            <output aria-label="Latitude de l’origine">
              {formatCoordinate(p.origin[0], format, true)}
            </output>
          </dd>
          <dt>Longitude</dt>
          <dd>
            <output aria-label="Longitude de l’origine">
              {formatCoordinate(p.origin[1], format, false)}
            </output>
          </dd>
        </dl>
        <div className="calculated-value">
          <span>Altitude du terrain à l’origine</span>
          <output>{terrainHeight(p, 0, 0).toFixed(1)} m</output>
        </div>
        <button onClick={() => a.centerView([0, 0])}>
          Centrer sur l’origine
        </button>
      </fieldset>
      <fieldset>
        <legend>Coordonnées du site pour le rapport</legend>
        <NumberField
          label="Latitude du site, °"
          value={site[0]}
          min={-85}
          max={85}
          onChange={(v) => settings({ site_coordinates: [v, site[1]] })}
        />
        <NumberField
          label="Longitude du site, °"
          value={site[1]}
          min={-180}
          max={180}
          onChange={(v) => settings({ site_coordinates: [site[0], v] })}
        />
        <button onClick={() => settings({ site_coordinates: [...p.origin] })}>
          Copier l’origine vers le site
        </button>
      </fieldset>
      <fieldset>
        <legend>Zone de calcul horizontale</legend>
        <dl className="origin-coordinates">
          <dt>X, m</dt>
          <dd>
            {area.x_min.toFixed(1)} à {area.x_max.toFixed(1)}
          </dd>
          <dt>Y, m</dt>
          <dd>
            {area.y_min.toFixed(1)} à {area.y_max.toFixed(1)}
          </dd>
        </dl>
        <button
          onClick={() => a.openCommand("Dimensions de la zone de calcul")}
        >
          Dimensions de la zone de calcul
        </button>
        <label>
          <input
            type="checkbox"
            checked={p.settings.show_calculation_area !== false}
            onChange={(e) =>
              settings({ show_calculation_area: e.target.checked })
            }
          />{" "}
          Afficher la zone de calcul
        </label>
        <label>
          <input
            type="checkbox"
            checked={a.display.grid}
            onChange={(e) =>
              a.setDisplay((d) => ({ ...d, grid: e.target.checked }))
            }
          />{" "}
          Afficher la grille
        </label>
        <label>
          <input
            type="checkbox"
            checked={p.settings.show_origin !== false}
            onChange={(e) => settings({ show_origin: e.target.checked })}
          />{" "}
          Afficher l’origine
        </label>
      </fieldset>
      <GridSettings />
      <button aria-pressed={a.ruler.length === 2} onClick={a.toggleRuler}>
        <Ruler size={13} /> Mesurer sur le plan
      </button>
      {a.ruler.length === 2 && (
        <p className="ruler-reading" aria-label="Mesure de la règle">
          {Math.hypot(
            a.ruler[1][0] - a.ruler[0][0],
            a.ruler[1][1] - a.ruler[0][1],
          ).toFixed(1)}{" "}
          m ·{" "}
          {(
            ((Math.atan2(
              a.ruler[1][0] - a.ruler[0][0],
              a.ruler[1][1] - a.ruler[0][1],
            ) *
              180) /
              Math.PI +
              360) %
            360
          ).toFixed(1)}
          °
        </p>
      )}
    </div>
  );
}
