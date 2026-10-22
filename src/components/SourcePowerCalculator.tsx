import { useEffect, useState } from "react";
import type { RadiatingSource } from "../types";
import {
  nativeAttenuation,
  nativeEirpCalculator,
  nativePowerRound,
  nativeTransmitterPower,
  POWER_UNITS,
  powerFromWatts,
  type PowerUnit,
} from "../lib/source-power";
import "./SourcePowerCalculator.css";

type Scratch = {
  power: string;
  count: string;
  loss: string;
  gain: string;
  port: string;
  eirp: string;
};
const EMPTY: Scratch = {
  power: "",
  count: "",
  loss: "",
  gain: "",
  port: "",
  eirp: "",
};
function number(text: string, name: string) {
  const value = text.trim();
  if (!/^[+-]?(?:\d+(?:[.,]\d*)?|[.,]\d+)(?:e[+-]?\d+)?$/i.test(value))
    throw new Error(`${name} : saisissez une valeur numérique.`);
  const parsed = Number(value.replace(",", "."));
  if (!Number.isFinite(parsed))
    throw new Error(`${name} : valeur numérique invalide.`);
  return parsed;
}
function singleText(value: number) {
  return String(Number(Math.fround(value).toPrecision(7)));
}
/** Independent native scratchpad: its three actions never write to the project. */
export default function SourcePowerCalculator({
  source,
}: {
  source?: RadiatingSource;
}) {
  const [values, setValues] = useState<Scratch>(EMPTY),
    [powerUnit, setPowerUnit] = useState<PowerUnit>("W"),
    [antennaUnit, setAntennaUnit] = useState<PowerUnit>("W"),
    [eirpUnit, setEirpUnit] = useState<PowerUnit>("W"),
    [error, setError] = useState("");
  useEffect(() => {
    setError("");
    if (!source) {
      setValues(EMPTY);
      return;
    }
    const next: Scratch = {
      power: singleText(source.power_w),
      count: String(source.num_tx),
      loss: singleText(source.total_attenuation_db),
      gain: singleText(source.gain_dbi),
      port: "",
      eirp: "",
    };
    try {
      const converted = powerFromWatts(Math.fround(source.power_w), powerUnit);
      next.power =
        powerUnit === "dBW" || powerUnit === "dBm"
          ? String(nativePowerRound(converted, 3))
          : singleText(converted);
      const calculated = nativeEirpCalculator({
        power: number(next.power, "Puissance TX"),
        power_unit: powerUnit,
        num_tx: source.num_tx,
        total_attenuation_db: number(next.loss, "Atténuation"),
        gain_dbi: number(next.gain, "Gain"),
        antenna_unit: antennaUnit,
        eirp_unit: eirpUnit,
      });
      next.port = String(calculated.antenna_value);
      next.eirp = String(calculated.eirp_value);
    } catch (e) {
      setError((e as Error).message);
    }
    setValues(next);
    // Changing a unit label alone does not recalculate the native scratchpad.
    // A newly selected/edited source is loaded in the currently selected units.
  }, [source]);

  const change = (key: keyof Scratch, value: string) => {
      setValues((v) => ({ ...v, [key]: value }));
      setError("");
    },
    parameters = () => ({
      num_tx: number(values.count, "Nombre de TX"),
      total_attenuation_db: number(values.loss, "Atténuation"),
      gain_dbi: number(values.gain, "Gain"),
    }),
    run = (action: () => Partial<Scratch>) => {
      try {
        const next = action();
        setValues((v) => ({ ...v, ...next }));
        setError("");
      } catch (e) {
        setError((e as Error).message);
      }
    },
    forward = () =>
      run(() => {
        const result = nativeEirpCalculator({
          ...parameters(),
          power: number(values.power, "Puissance TX"),
          power_unit: powerUnit,
          antenna_unit: antennaUnit,
          eirp_unit: eirpUnit,
        });
        return {
          port: String(result.antenna_value),
          eirp: String(result.eirp_value),
        };
      }),
    inversePower = () =>
      run(() => {
        const result = nativeTransmitterPower({
          ...parameters(),
          antenna_power: number(values.port, "Puissance au port antenne"),
          antenna_unit: antennaUnit,
          power_unit: powerUnit,
        });
        return { power: String(result.power_value) };
      }),
    inverseLoss = () =>
      run(() => ({
        loss: String(
          nativeAttenuation({
            num_tx: number(values.count, "Nombre de TX"),
            gain_dbi: number(values.gain, "Gain"),
            power: number(values.power, "Puissance TX"),
            power_unit: powerUnit,
            antenna_power: number(values.port, "Puissance au port antenne"),
            antenna_unit: antennaUnit,
          }),
        ),
      })),
    field = (key: keyof Scratch, label: string, row: number) => (
      <label className="source-power-field">
        <span style={{ gridRow: row }}>
          {label === "Nombre de TX" ? "Nombre TX" : label}
        </span>
        <input
          style={{ gridRow: row }}
          aria-label={`Calculateur : ${label}`}
          inputMode="decimal"
          readOnly={key === "eirp"}
          value={values[key]}
          onChange={(e) => change(key, e.target.value)}
        />
      </label>
    ),
    units = (
      value: PowerUnit,
      set: (unit: PowerUnit) => void,
      label: string,
      row: number,
    ) => (
      <select
        style={{ gridRow: row, gridColumn: 3 }}
        aria-label={`Unité ${label} du calculateur`}
        value={value}
        onChange={(e) => {
          set(e.target.value as PowerUnit);
          setError("");
        }}
      >
        {POWER_UNITS.map((v) => (
          <option key={v}>{v}</option>
        ))}
      </select>
    );
  return (
    <fieldset
      className="source-power-calculator"
      disabled={!source}
      aria-label="Calculateur EIRP indépendant"
    >
      <legend>Calculateur EIRP</legend>
      <div className="source-power-grid">
        {field("power", "Puissance TX", 1)}
        {units(powerUnit, setPowerUnit, "puissance TX", 1)}
        {field("count", "Nombre de TX", 2)}
        {field("loss", "Atténuation", 3)}
        <span className="source-power-unit" style={{ gridRow: 3 }}>
          dB
        </span>
        {field("gain", "Gain antenne", 4)}
        <span className="source-power-unit" style={{ gridRow: 4 }}>
          dBi
        </span>
        {field("port", "Port antenne", 5)}
        {units(antennaUnit, setAntennaUnit, "port antenne", 5)}
        {field("eirp", "EIRP", 6)}
        {units(eirpUnit, setEirpUnit, "EIRP", 6)}
        <button
          className="source-power-action"
          style={{ gridRow: 1 }}
          aria-label="Calculer la puissance TX depuis le port antenne"
          title="Calculer la puissance de chaque TX"
          onClick={inversePower}
        >
          ▶
        </button>
        <button
          className="source-power-action"
          style={{ gridRow: 3 }}
          aria-label="Calculer l’atténuation depuis les puissances TX et antenne"
          title="Calculer l’atténuation totale"
          onClick={inverseLoss}
        >
          ▶
        </button>
        <button
          className="source-power-action source-power-forward"
          style={{ gridRow: "5 / 7" }}
          aria-label="Calculer la puissance au port antenne et l’EIRP"
          title="Calculer la puissance au port antenne et l’EIRP"
          onClick={forward}
        >
          ▶
        </button>
      </div>
      {error && (
        <p className="source-power-error" role="alert">
          {error}
        </p>
      )}
    </fieldset>
  );
}
