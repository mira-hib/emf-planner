import { POWER_UNITS, nativePowerRound, powerFromWatts, powerToWatts, type PowerUnit } from "./source-power";

/** frmDBW: changing W keeps its input; changing another unit rounds W to 4 decimals. */
export function convertedPowerFields(raw: string, unit: PowerUnit): Record<PowerUnit, string> {
  if (!raw.trim()) throw new Error("Saisissez une puissance positive.");
  const watts = powerToWatts(Number(raw.replace(",", ".")), unit);
  if (watts <= 0) throw new Error("Saisissez une puissance strictement positive.");
  return Object.fromEntries(POWER_UNITS.map((u) => [u, u === unit ? raw : String(nativePowerRound(powerFromWatts(watts, u), u === "W" ? 4 : 1))])) as Record<PowerUnit, string>;
}
