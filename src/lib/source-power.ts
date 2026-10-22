export type PowerUnit = "W" | "mW" | "dBW" | "dBm";
export const POWER_UNITS: readonly PowerUnit[] = ["W", "mW", "dBW", "dBm"];

export interface SourcePowerParameters {
  num_tx: number;
  total_attenuation_db: number;
  gain_dbi: number;
}
export interface SourcePowerInput extends SourcePowerParameters {
  power_w: number;
}
function finite(value: number, label: string): number {
  if (!Number.isFinite(value))
    throw new Error(`${label} : valeur numérique invalide.`);
  return value;
}
function positive(value: number, label: string): number {
  finite(value, label);
  if (value <= 0) throw new Error(`${label} : la valeur doit être positive.`);
  return value;
}
function validateParameters(input: SourcePowerParameters) {
  // Public2.StructTX.N is an Int32. Do not reproduce the native scratchpad's
  // overwritten invalid-number sentinel and resulting division by zero.
  if (!Number.isSafeInteger(input.num_tx) || input.num_tx < 1)
    throw new Error(
      "Nombre de TX : saisissez un entier supérieur ou égal à 1.",
    );
  finite(input.total_attenuation_db, "Atténuation");
  if (input.total_attenuation_db < 0)
    throw new Error("L’atténuation ne peut pas être négative.");
  finite(input.gain_dbi, "Gain");
}
function unit(value: PowerUnit) {
  if (!POWER_UNITS.includes(value))
    throw new Error("Unité de puissance inconnue.");
  return value;
}
export function powerToWatts(value: number, powerUnit: PowerUnit): number {
  finite(value, "Puissance");
  unit(powerUnit);
  if ((powerUnit === "W" || powerUnit === "mW") && value < 0)
    throw new Error("La puissance ne peut pas être négative.");
  return finite(
    powerUnit === "W"
      ? value
      : powerUnit === "mW"
        ? value / 1000
        : powerUnit === "dBW"
          ? 10 ** (value / 10)
          : 10 ** ((value - 30) / 10),
    "Puissance convertie",
  );
}
export function powerFromWatts(watts: number, powerUnit: PowerUnit): number {
  finite(watts, "Puissance");
  unit(powerUnit);
  if (watts < 0) throw new Error("La puissance ne peut pas être négative.");
  if ((powerUnit === "dBW" || powerUnit === "dBm") && watts === 0)
    throw new Error(
      "Une puissance nulle n’a pas de valeur logarithmique finie.",
    );
  return finite(
    powerUnit === "W"
      ? watts
      : powerUnit === "mW"
        ? watts * 1000
        : 10 * Math.log10(watts) + (powerUnit === "dBm" ? 30 : 0),
    "Puissance convertie",
  );
}

/** Unrounded physical powers; no duty/limit/pattern factors enter boresight EIRP. */
export function sourcePower(input: SourcePowerInput) {
  validateParameters(input);
  const transmitter_w = powerToWatts(input.power_w, "W"),
    total_transmitter_w = finite(
      transmitter_w * input.num_tx,
      "Puissance totale TX",
    ),
    antenna_w = finite(
      total_transmitter_w * 10 ** (-input.total_attenuation_db / 10),
      "Puissance au port antenne",
    ),
    eirp_w =
      transmitter_w === 0
        ? 0
        : finite(
            total_transmitter_w *
              10 ** ((input.gain_dbi - input.total_attenuation_db) / 10),
            "EIRP",
          );
  return { transmitter_w, total_transmitter_w, antenna_w, eirp_w };
}

/** Returns watts per transmitter, from total antenna-port power or EIRP. */
export function transmitterPowerFromTarget(
  target: number,
  targetUnit: PowerUnit,
  targetKind: "antenna" | "eirp",
  parameters: SourcePowerParameters,
): number {
  validateParameters(parameters);
  if (targetKind !== "antenna" && targetKind !== "eirp")
    throw new Error("Type de puissance cible inconnu.");
  const watts = positive(powerToWatts(target, targetUnit), "Puissance cible"),
    gain = targetKind === "eirp" ? parameters.gain_dbi : 0;
  return positive(
    (watts * 10 ** ((parameters.total_attenuation_db - gain) / 10)) /
      parameters.num_tx,
    "Puissance TX calculée",
  );
}

/** .NET Framework Math.Round(double, digits): nearest, ties to even. */
export function nativePowerRound(value: number, digits: number): number {
  finite(value, "Valeur à arrondir");
  if (!Number.isInteger(digits) || digits < 0 || digits > 15)
    throw new Error("Précision d’arrondi invalide.");
  if (Math.abs(value) >= 1e16) return value;
  const factor = 10 ** digits,
    scaled = value * factor,
    floor = Math.floor(scaled),
    fraction = scaled - floor,
    rounded =
      (fraction === 0.5
        ? floor + (floor % 2 === 0 ? 0 : 1)
        : Math.round(scaled)) / factor;
  return rounded === 0 ? 0 : rounded;
}
function single(value: number, label: string) {
  return finite(Math.fround(finite(value, label)), label);
}
function nativeParameters(
  parameters: SourcePowerParameters,
): SourcePowerParameters {
  validateParameters(parameters);
  return {
    num_tx: single(parameters.num_tx, "Nombre de TX"),
    total_attenuation_db: single(
      parameters.total_attenuation_db,
      "Atténuation",
    ),
    gain_dbi: single(parameters.gain_dbi, "Gain"),
  };
}
function nativeOutput(
  watts: number,
  dbw: number,
  powerUnit: PowerUnit,
  eirp: boolean,
): number {
  unit(powerUnit);
  const value =
    powerUnit === "W"
      ? watts
      : powerUnit === "mW"
        ? watts * 1000
        : dbw + (powerUnit === "dBm" ? 30 : 0);
  // UC_PRTO.CalcEIM: port 3 decimals in every unit; EIRP W/dBW/dBm
  // 1 decimal, but EIRP mW 3 decimals. These are display-only roundings.
  return nativePowerRound(value, eirp && powerUnit !== "mW" ? 1 : 3);
}
export interface NativeEirpInput extends SourcePowerParameters {
  power: number;
  power_unit: PowerUnit;
  antenna_unit: PowerUnit;
  eirp_unit: PowerUnit;
}
/** UC_PRTO.CalcEIM, including its Single input parsing and output precisions. */
export function nativeEirpCalculator(input: NativeEirpInput) {
  const parameters = nativeParameters(input),
    transmitter_w = positive(
      powerToWatts(single(input.power, "Puissance TX"), input.power_unit),
      "Puissance TX",
    ),
    antenna_dbw =
      10 * Math.log10(transmitter_w * parameters.num_tx) -
      parameters.total_attenuation_db,
    eirp_dbw = antenna_dbw + parameters.gain_dbi,
    antenna_w = positive(10 ** (antenna_dbw / 10), "Puissance au port antenne"),
    eirp_w = positive(10 ** (eirp_dbw / 10), "EIRP");
  return {
    transmitter_w,
    antenna_w,
    eirp_w,
    antenna_value: nativeOutput(
      antenna_w,
      antenna_dbw,
      input.antenna_unit,
      false,
    ),
    eirp_value: nativeOutput(eirp_w, eirp_dbw, input.eirp_unit, true),
  };
}
export interface NativeTransmitterInput extends SourcePowerParameters {
  antenna_power: number;
  antenna_unit: PowerUnit;
  power_unit: PowerUnit;
}
/** UC_PRTO.cmdCalcP_Click; the input is total port power, not EIRP. */
export function nativeTransmitterPower(input: NativeTransmitterInput) {
  const parameters = nativeParameters(input),
    antenna_w = positive(
      powerToWatts(
        single(input.antenna_power, "Puissance au port antenne"),
        input.antenna_unit,
      ),
      "Puissance au port antenne",
    ),
    transmitter_w = positive(
      10 **
        ((10 * Math.log10(antenna_w) + parameters.total_attenuation_db) / 10) /
        parameters.num_tx,
      "Puissance TX calculée",
    );
  return {
    transmitter_w,
    power_value: nativeOutput(
      transmitter_w,
      10 * Math.log10(transmitter_w),
      input.power_unit,
      false,
    ),
  };
}
export interface NativeAttenuationInput {
  power: number;
  power_unit: PowerUnit;
  antenna_power: number;
  antenna_unit: PowerUnit;
  num_tx: number;
  gain_dbi: number;
}
/** UC_PRTO.cmdCalcNu_Click; negative loss is rejected, never silently clamped. */
export function nativeAttenuation(input: NativeAttenuationInput): number {
  const parameters = nativeParameters({ ...input, total_attenuation_db: 0 }),
    transmitter_w = positive(
      powerToWatts(single(input.power, "Puissance TX"), input.power_unit),
      "Puissance TX",
    ),
    antenna_w = positive(
      powerToWatts(
        single(input.antenna_power, "Puissance au port antenne"),
        input.antenna_unit,
      ),
      "Puissance au port antenne",
    ),
    loss =
      10 * Math.log10(transmitter_w * parameters.num_tx) -
      10 * Math.log10(antenna_w);
  if (loss < 0)
    throw new Error(
      "La puissance au port dépasse la puissance totale TX : atténuation négative.",
    );
  return nativePowerRound(loss, 3);
}
