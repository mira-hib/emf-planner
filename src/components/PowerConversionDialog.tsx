import { useState } from "react";
import { POWER_UNITS, type PowerUnit } from "../lib/source-power";
import { convertedPowerFields } from "../lib/power-conversion";
import Modal from "./Modal";

export default function PowerConversionDialog({ value, onApply, onClose }: { value: number; onApply: (watts: number) => void; onClose: () => void }) {
  const [fields, setFields] = useState<Record<PowerUnit, string>>(() => value > 0 ? convertedPowerFields(String(value), "W") : { W: String(value), mW: "", dBW: "", dBm: "" });
  const [error, setError] = useState(value > 0 ? "" : "Saisissez une puissance strictement positive.");
  return <Modal title="Conversion de puissance" onClose={onClose}><div className="form-grid">
    {POWER_UNITS.map((unit) => <label key={unit}>Puissance, {unit}<input aria-label={`Puissance, ${unit}`} inputMode="decimal" value={fields[unit]} onChange={(e) => {
      try { setFields(convertedPowerFields(e.target.value, unit)); setError(""); }
      catch (reason) { setFields({ W: "", mW: "", dBW: "", dBm: "", [unit]: e.target.value }); setError(String((reason as Error).message)); }
    }} /></label>)}
  </div>{error && <p role="alert">{error}</p>}<div className="dialog-actions"><button disabled={!!error} onClick={() => { onApply(Number(Number(fields.W.replace(",", ".")).toFixed(4))); onClose(); }}>OK</button><button onClick={onClose}>Annuler</button></div></Modal>;
}
