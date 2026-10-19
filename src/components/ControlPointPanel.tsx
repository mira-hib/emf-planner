import { MapPin, Trash2 } from "lucide-react";
import type { ControlPoint } from "../types";

interface Props {
  points: ControlPoint[];
  onRemove: (id: string) => void;
}

export default function ControlPointPanel({ points, onRemove }: Props) {
  return (
    <div className="source-panel">
      <div className="panel-header">
        <MapPin size={18} />
        <h2>Points de contrôle</h2>
      </div>

      <div className="source-list">
        {points.length === 0 && (
          <p className="empty-hint">Cliquez sur la carte pour ajouter un point de contrôle</p>
        )}

        {points.map((cp) => (
          <div key={cp.id} className="source-card">
            <div className="source-header">
              <span className="source-name">{cp.name}</span>
              {cp.compliant != null && (
                <span className={`exposure-badge ${cp.compliant ? "compliant" : "non-compliant"}`}>
                  {cp.compliant ? "OK" : "NC"}
                </span>
              )}
              <button className="btn-icon btn-danger" onClick={() => onRemove(cp.id)}>
                <Trash2 size={14} />
              </button>
            </div>
            <div className="cp-details">
              <div className="cp-row">
                <span>Lat/Lng</span>
                <span>{cp.lat.toFixed(5)}, {cp.lng.toFixed(5)}</span>
              </div>
              <div className="cp-row">
                <span>Hauteur</span>
                <span>{cp.height_m} m</span>
              </div>
              {cp.calculated_e_field_v_m != null && (
                <div className="cp-row">
                  <span>E calculé</span>
                  <span>{cp.calculated_e_field_v_m.toFixed(4)} V/m</span>
                </div>
              )}
              {cp.measured_e_field_v_m != null && (
                <div className="cp-row">
                  <span>E mesuré</span>
                  <span>{cp.measured_e_field_v_m.toFixed(4)} V/m</span>
                </div>
              )}
              {cp.exposure_ratio != null && (
                <div className="cp-row">
                  <span>Ratio exposition</span>
                  <span style={{ color: cp.exposure_ratio < 1 ? "var(--success)" : "var(--danger)", fontWeight: 600 }}>
                    {(cp.exposure_ratio * 100).toFixed(1)}%
                  </span>
                </div>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
