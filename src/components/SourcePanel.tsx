import { useState } from "react";
import { Radio, Trash2, Plus } from "lucide-react";
import type { RadiatingSource } from "../types";

interface Props {
  sources: RadiatingSource[];
  onAdd: () => void;
  onRemove: (id: string) => void;
  onUpdate: (id: string, field: keyof RadiatingSource, value: number | string) => void;
}

export default function SourcePanel({ sources, onAdd, onRemove, onUpdate }: Props) {
  const [expanded, setExpanded] = useState<string | null>(null);

  return (
    <div className="source-panel">
      <div className="panel-header">
        <Radio size={18} />
        <h2>Sources rayonnantes</h2>
        <button className="btn-icon" onClick={() => onAdd()}>
          <Plus size={18} />
        </button>
      </div>

      <div className="source-list">
        {sources.length === 0 && (
          <p className="empty-hint">Cliquez sur la carte pour ajouter une source</p>
        )}

        {sources.map((s) => (
          <div key={s.id} className="source-card">
            <div className="source-header" onClick={() => setExpanded(expanded === s.id ? null : s.id)}>
              <span className="source-name">{s.name}</span>
              <span className="source-info">{s.frequency_mhz} MHz</span>
              <button className="btn-icon btn-danger" onClick={(e) => { e.stopPropagation(); onRemove(s.id); }}>
                <Trash2 size={14} />
              </button>
            </div>

            {expanded === s.id && (
              <div className="source-details">
                <label>
                  Nom
                  <input value={s.name} onChange={(e) => onUpdate(s.id, "name", e.target.value)} />
                </label>
                <label>
                  Fréquence (MHz)
                  <input type="number" value={s.frequency_mhz} onChange={(e) => onUpdate(s.id, "frequency_mhz", +e.target.value)} />
                </label>
                <label>
                  Puissance (W)
                  <input type="number" value={s.power_w} onChange={(e) => onUpdate(s.id, "power_w", +e.target.value)} />
                </label>
                <label>
                  Gain (dBi)
                  <input type="number" value={s.gain_dbi} onChange={(e) => onUpdate(s.id, "gain_dbi", +e.target.value)} />
                </label>
                <label>
                  Hauteur (m)
                  <input type="number" value={s.height_m} onChange={(e) => onUpdate(s.id, "height_m", +e.target.value)} />
                </label>
                <label>
                  Azimut (°)
                  <input type="number" value={s.azimuth_deg} onChange={(e) => onUpdate(s.id, "azimuth_deg", +e.target.value)} />
                </label>
                <label>
                  Tilt (°)
                  <input type="number" value={s.tilt_deg} onChange={(e) => onUpdate(s.id, "tilt_deg", +e.target.value)} />
                </label>
                <div className="source-section-title">Identification</div>
                <label>
                  Type de service
                  <input value={s.service_type} onChange={(e) => onUpdate(s.id, "service_type", e.target.value)} placeholder="GSM 900, UMTS, LTE..." />
                </label>
                <label>
                  Secteur ID
                  <input value={s.sector_id} onChange={(e) => onUpdate(s.id, "sector_id", e.target.value)} placeholder="S1, S2, S3..." />
                </label>
                <label>
                  Opérateur
                  <input value={s.owner} onChange={(e) => onUpdate(s.id, "owner", e.target.value)} />
                </label>
                <label>
                  Lieu
                  <input value={s.location} onChange={(e) => onUpdate(s.id, "location", e.target.value)} />
                </label>
                <div className="source-section-title">Antenne</div>
                <label>
                  Modèle d'antenne
                  <input value={s.antenna_model} onChange={(e) => onUpdate(s.id, "antenna_model", e.target.value)} placeholder="Kathrein 742215..." />
                </label>
                <label>
                  Nombre de TX
                  <input type="number" min="1" value={s.num_tx} onChange={(e) => onUpdate(s.id, "num_tx", +e.target.value)} />
                </label>
                <label>
                  Taille antenne (m)
                  <input type="number" step="0.1" value={s.antenna_size_m} onChange={(e) => onUpdate(s.id, "antenna_size_m", +e.target.value)} />
                </label>
                <label>
                  Downtilt mécanique (°)
                  <input type="number" step="0.5" value={s.mechanical_downtilt_deg} onChange={(e) => onUpdate(s.id, "mechanical_downtilt_deg", +e.target.value)} />
                </label>
                <label>
                  Downtilt électrique (°)
                  <input type="number" step="0.5" value={s.electrical_downtilt_deg} onChange={(e) => onUpdate(s.id, "electrical_downtilt_deg", +e.target.value)} />
                </label>
                <label>
                  Ouverture H (°)
                  <input type="number" step="1" value={s.beam_width_h_deg} onChange={(e) => onUpdate(s.id, "beam_width_h_deg", +e.target.value)} />
                </label>
                <label>
                  Ouverture V (°)
                  <input type="number" step="1" value={s.beam_width_v_deg} onChange={(e) => onUpdate(s.id, "beam_width_v_deg", +e.target.value)} />
                </label>
                <div className="source-section-title">Atténuation feeder</div>
                <label>
                  Atténuation totale (dB)
                  <input type="number" step="0.1" value={s.total_attenuation_db} onChange={(e) => onUpdate(s.id, "total_attenuation_db", +e.target.value)} />
                </label>
                <div className="source-section-title">Facteurs de réduction</div>
                <label>
                  Cycle de service (F_TDC)
                  <input type="number" step="0.01" min="0" max="1" value={s.duty_cycle} onChange={(e) => onUpdate(s.id, "duty_cycle", +e.target.value)} />
                </label>
                <label>
                  Réduction puissance (F_PP)
                  <input type="number" step="0.01" min="0" max="1" value={s.power_reduction} onChange={(e) => onUpdate(s.id, "power_reduction", +e.target.value)} />
                </label>
                <label>
                  Atténuation 24h (α24)
                  <input type="number" step="0.01" min="0" max="1" value={s.alpha24} onChange={(e) => onUpdate(s.id, "alpha24", +e.target.value)} />
                </label>
                <label>
                  Réflexion sol (k)
                  <input type="number" step="0.1" min="1" max="1.6" value={s.ground_reflection_k} onChange={(e) => onUpdate(s.id, "ground_reflection_k", +e.target.value)} />
                </label>
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
}
