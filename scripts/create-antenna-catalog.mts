import { readFileSync, mkdirSync, writeFileSync } from "node:fs";
import { importXemf } from "../src/lib/import-xemf.ts";
import { patternAmplitude } from "../src/lib/engine.ts";
const bytes = readFileSync("tests/fixtures/original-sample.xemf");
const project = importXemf(
  bytes.buffer.slice(bytes.byteOffset, bytes.byteOffset + bytes.byteLength),
  "reference",
);
const catalog = [
  ...new Map(
    project.sources.map((s) => [
      `${s.antenna_model}-${s.frequency_mhz}`,
      {
        id: `${s.antenna_model}-${s.frequency_mhz}`,
        name: s.antenna_model,
        frequency: s.frequency_mhz,
        gain: s.gain_dbi,
        horizontal: s.pattern_h,
        vertical: Array.from(
          { length: 360 },
          (_, i) =>
            -20 *
            Math.log10(
              patternAmplitude(
                s.pattern_v,
                i + s.electrical_downtilt_deg,
                s.beam_width_v_deg,
              ),
            ),
        ),
      },
    ]),
  ).values(),
];
mkdirSync("public/antennas", { recursive: true });
writeFileSync("public/antennas/catalog.json", JSON.stringify(catalog));
console.log(`${catalog.length} reference antenna patterns`);
