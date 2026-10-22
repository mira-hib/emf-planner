import test from "node:test";
import assert from "node:assert/strict";
import {
  nativeAttenuation,
  nativeEirpCalculator,
  nativePowerRound,
  nativeTransmitterPower,
  POWER_UNITS,
  powerFromWatts,
  powerToWatts,
  sourcePower,
  transmitterPowerFromTarget,
  type PowerUnit,
} from "../src/lib/source-power.ts";
import { eirp } from "../src/lib/engine.ts";
import { newProject, newSource } from "../src/lib/project.ts";

function close(actual: number, expected: number, tolerance = 1e-10) {
  assert.ok(
    Math.abs(actual - expected) <= tolerance * Math.max(1, Math.abs(expected)),
    `${actual} != ${expected}`,
  );
}
test("power units preserve physical power, permit negative dB levels, and reject invalid logarithms", () => {
  assert.equal(powerToWatts(30, "dBm"), 1);
  assert.equal(powerToWatts(0, "dBW"), 1);
  assert.equal(powerToWatts(-30, "dBW"), 0.001);
  assert.equal(powerToWatts(1000, "mW"), 1);
  for (const unit of POWER_UNITS)
    close(powerToWatts(powerFromWatts(1.23456789, unit), unit), 1.23456789);
  assert.equal(powerToWatts(0, "W"), 0);
  assert.equal(powerFromWatts(0, "mW"), 0);
  assert.throws(() => powerFromWatts(0, "dBW"), /nulle/);
  assert.throws(() => powerToWatts(-1, "W"), /négative/);
  assert.throws(() => powerToWatts(Infinity, "W"), /invalide/);
  assert.throws(() => powerToWatts(10_000, "dBm"), /invalide/);
  assert.throws(() => powerToWatts(1, "unknown" as PowerUnit), /inconnue/);
});
test("unrounded EIRP matches the engine and separates total port power from power per TX", () => {
  const s = {
    ...newSource(newProject().origin, "Test"),
    power_w: 2.5,
    num_tx: 6,
    total_attenuation_db: 10 * Math.log10(2),
    gain_dbi: 10,
  };
  const powers = sourcePower(s);
  close(powers.total_transmitter_w, 15);
  close(powers.antenna_w, 7.5);
  close(powers.eirp_w, 75);
  assert.equal(powers.eirp_w, eirp(s));
  assert.equal(sourcePower({ ...s, power_w: 0 }).eirp_w, 0);
  // Duty/limit factors and patterns apply to field calculations, not EIRP.
  assert.equal(
    sourcePower({ ...s, alpha24: 0.1, duty_cycle: 0.2, power_reduction: 0.3 })
      .eirp_w,
    powers.eirp_w,
  );
});
test("inverse targets account for feeder loss, antenna gain and every transmitter", () => {
  const parameters = { num_tx: 3, total_attenuation_db: 10, gain_dbi: 20 };
  for (const unit of POWER_UNITS) {
    close(
      transmitterPowerFromTarget(
        powerFromWatts(3, unit),
        unit,
        "antenna",
        parameters,
      ),
      10,
    );
    close(
      transmitterPowerFromTarget(
        powerFromWatts(300, unit),
        unit,
        "eirp",
        parameters,
      ),
      10,
    );
  }
  assert.throws(
    () => transmitterPowerFromTarget(0, "W", "antenna", parameters),
    /positive/,
  );
});
// Values evaluated with Windows .NET Math.Log10/Math.Pow/Math.Round after
// Single input casts, from UC_PRTO.cs:5578–5704. These are algorithm reference
// values, not screenshots or claims of additional native UI observations.
const nativeCases = [
  {
    power: 12.05,
    power_unit: "W" as const,
    num_tx: 2,
    total_attenuation_db: 1.6,
    gain_dbi: 17.5,
    transmitter_w: 12.050000190734863,
    antenna_w: 16.673126571526964,
    eirp_w: 937.5988091298893,
    antenna: [16.673, 16673.127, 12.22, 42.22],
    eirp: [937.6, 937598.809, 29.7, 59.7],
  },
  {
    power: 0.123456789,
    power_unit: "W" as const,
    num_tx: 3,
    total_attenuation_db: 2.3456,
    gain_dbi: -2.125,
    transmitter_w: 0.12345679104328156,
    antenna_w: 0.21581232826604815,
    eirp_w: 0.13230499786083932,
    antenna: [0.216, 215.812, -6.659, 23.341],
    eirp: [0.1, 132.305, -8.8, 21.2],
  },
  {
    power: 44.7,
    power_unit: "dBm" as const,
    num_tx: 2,
    total_attenuation_db: 1.8,
    gain_dbi: 17.1,
    transmitter_w: 29.51209745115099,
    antenna_w: 38.99689927404389,
    eirp_w: 2000.0005489791417,
    antenna: [38.997, 38996.899, 15.91, 45.91],
    eirp: [2000, 2000000.549, 33, 63],
  },
];
test("native calculator follows Single inputs and each native output unit precision", () => {
  for (const c of nativeCases)
    for (const [i, unit] of POWER_UNITS.entries()) {
      const actual = nativeEirpCalculator({
        ...c,
        antenna_unit: unit,
        eirp_unit: unit,
      });
      close(actual.transmitter_w, c.transmitter_w);
      close(actual.antenna_w, c.antenna_w);
      close(actual.eirp_w, c.eirp_w);
      assert.equal(actual.antenna_value, c.antenna[i]);
      assert.equal(actual.eirp_value, c.eirp[i]);
    }
});
test("native transmitter inverse uses total antenna power and rounds only its displayed result", () => {
  const parameters = { num_tx: 3, total_attenuation_db: 10, gain_dbi: 20 };
  for (const unit of POWER_UNITS) {
    const result = nativeTransmitterPower({
      ...parameters,
      antenna_power: 3000,
      antenna_unit: "mW",
      power_unit: unit,
    });
    close(result.transmitter_w, 10);
    assert.equal(
      result.power_value,
      nativePowerRound(powerFromWatts(10, unit), 3),
    );
  }
  // Gain is validated by the native routine, but does not affect port→TX.
  assert.equal(
    nativeTransmitterPower({
      ...parameters,
      gain_dbi: -20,
      antenna_power: 3,
      antenna_unit: "W",
      power_unit: "W",
    }).power_value,
    10,
  );
});
test("native attenuation inverse rejects a port output larger than the summed TX inputs", () => {
  assert.equal(
    nativeAttenuation({
      power: 10,
      power_unit: "W",
      antenna_power: 3,
      antenna_unit: "W",
      num_tx: 3,
      gain_dbi: 20,
    }),
    10,
  );
  assert.equal(
    nativeAttenuation({
      power: 10,
      power_unit: "W",
      antenna_power: 30,
      antenna_unit: "W",
      num_tx: 3,
      gain_dbi: 20,
    }),
    0,
  );
  assert.throws(
    () =>
      nativeAttenuation({
        power: 10,
        power_unit: "W",
        antenna_power: 31,
        antenna_unit: "W",
        num_tx: 3,
        gain_dbi: 20,
      }),
    /négative/,
  );
});
test("native display rounding uses ties to even with no decimal epsilon bias", () => {
  assert.equal(nativePowerRound(1.25, 1), 1.2);
  assert.equal(nativePowerRound(1.35, 1), 1.4);
  assert.equal(nativePowerRound(-1.25, 1), -1.2);
  assert.equal(nativePowerRound(-1.35, 1), -1.4);
  assert.equal(nativePowerRound(2.3455, 3), 2.346);
  assert.equal(nativePowerRound(2.3445, 3), 2.344);
  assert.equal(nativePowerRound(1.005, 2), 1);
  assert.equal(Object.is(nativePowerRound(-0.001, 1), -0), false);
});
test("invalid TX counts and nonfinite parameters never yield silently clamped inverse powers", () => {
  const valid = {
    power: 10,
    power_unit: "W" as const,
    num_tx: 1,
    total_attenuation_db: 0,
    gain_dbi: 0,
    antenna_unit: "W" as const,
    eirp_unit: "W" as const,
  };
  for (const num_tx of [0, -1, 0.5, 1.5, NaN, Infinity]) {
    assert.throws(
      () => nativeEirpCalculator({ ...valid, num_tx }),
      /Nombre de TX/,
    );
    assert.throws(
      () => transmitterPowerFromTarget(1, "W", "eirp", { ...valid, num_tx }),
      /Nombre de TX/,
    );
  }
  assert.throws(
    () => nativeEirpCalculator({ ...valid, total_attenuation_db: -1 }),
    /négative/,
  );
  assert.throws(
    () => nativeEirpCalculator({ ...valid, gain_dbi: NaN }),
    /Gain/,
  );
  assert.throws(() => nativeEirpCalculator({ ...valid, power: 0 }), /positive/);
  assert.throws(
    () => nativeEirpCalculator({ ...valid, gain_dbi: 1e50 }),
    /Gain/,
  );
});
