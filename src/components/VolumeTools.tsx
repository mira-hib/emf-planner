import { useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import { defaultVolumeOptions } from "../lib/volume";
import type { VolumeOptions } from "../types";
import Modal from "./Modal";
export const VOLUME_COMMAND = "Volumes de sécurité EMF 3D";
export default function VolumeTools() {
  const a = usePlanner(),
    [options, setOptions] = useState(
      a.project.volume_options ?? defaultVolumeOptions(a.project),
    );
  const labels: Record<keyof VolumeOptions, string> = {
    center_x: "Centre X, m",
    center_y: "Centre Y, m",
    radius_m: "Demi-largeur du volume, m",
    bottom_m: "Altitude basse, m",
    top_m: "Altitude haute, m",
    step_m: "Pas du maillage, m",
  };
  return (
    <Modal title={VOLUME_COMMAND} onClose={() => a.setCommand("")}>
      <p>
        Surface à 100 % d’exposition cumulée —{" "}
        {a.project.limits[a.project.settings.selected_limit].name}.
      </p>
      {(Object.keys(labels) as (keyof VolumeOptions)[]).map((key) => (
        <label key={key}>
          {labels[key]}
          <input
            aria-label={labels[key]}
            type="number"
            step="any"
            value={options[key]}
            onChange={(e) =>
              setOptions((o) => ({ ...o, [key]: Number(e.target.value) }))
            }
          />
        </label>
      ))}
      <p className="hint">
        Les altitudes sont absolues. Le pas règle la précision et le temps de
        calcul. Une surface qui atteint les bords du volume nécessite une zone
        plus grande.
      </p>
      <footer>
        <button onClick={() => a.setCommand("")}>Annuler</button>
        <button
          disabled={
            !Object.values(options).every(Number.isFinite) ||
            options.step_m < 0.2 ||
            options.radius_m <= 0 ||
            options.top_m <= options.bottom_m ||
            a.busy
          }
          onClick={() => {
            a.commit(
              (p) => ({ ...p, volume_options: options }),
              "Paramètres du volume définis",
            );
            a.setCommand("");
            a.calculate("volume");
          }}
        >
          Calculer le volume
        </button>
      </footer>
    </Modal>
  );
}
