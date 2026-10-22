import type { LimitSet } from "../types";

// Historical EMFPlanner 7.1 application data, not current regulatory guidance.
// Source: reference/original-decompiled/EMFPlanner/Public2.cs:866-960.
export const NATIVE_LIMIT_MISSING = -999;
type NativeRow = readonly [
  number,
  number,
  number,
  number,
  number,
  number,
  number,
  number,
  number,
];
export interface Preset {
  label: string;
  country: string;
  rows: readonly NativeRow[];
  // Greece intentionally leaves the existing descriptions unchanged.
  descriptions: readonly string[] | null;
}

const M = NATIVE_LIMIT_MISSING;
const PUBLIC_NAMES = [
  "Public exposure limits",
  "Occupational exposure limits",
  "",
  "",
];
const COLORS = ["#ff0000", "#0000ff", "#00ff00", "#00ffff"];
const standardRows = (
  low: number,
  slope: number,
  high: number,
): NativeRow[] => [
  [400, low, 0, 61, 0, M, M, M, M],
  [2000, slope, 0.5, 3, 0.5, M, M, M, M],
  [1000000, high, 0, 137, 0, M, M, M, M],
];

export const PRESETS: readonly Preset[] = [
  {
    label: "ICNIRP",
    country: "ICNIRP",
    rows: standardRows(28, 1.375, 61),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Union européenne",
    country: "EU",
    rows: standardRows(28, 1.375, 61),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "États-Unis",
    country: "USA",
    rows: [
      [30, 824, -1, 1842, -1, M, M, M, M],
      [300, 27.5, 0, 61.4, 0, M, M, M, M],
      [1500, 1.585, 0.5, 3.545, 0.5, M, M, M, M],
      [1000000, 61.4, 0, 137.3, 0, M, M, M, M],
    ],
    descriptions: [
      "Limits for General Population/ Uncontrolled Exposure",
      "Limits for Occupational/ Controlled Exposure",
      "",
      "",
    ],
  },
  {
    label: "Suisse",
    country: "Switzerland",
    rows: [
      [400, 5, 0, 61, 0, M, M, M, M],
      [2000, 5, 0, 3, 0.5, M, M, M, M],
      [1000000, 5, 0, 137, 0, M, M, M, M],
    ],
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Royaume-Uni",
    country: "UK",
    rows: standardRows(28, 1.375, 61),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Belgique",
    country: "Belgium",
    rows: standardRows(14, 0.688, 30.5),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Italie",
    country: "Italy",
    rows: [[100000, 6, 0, 20, 0, 4.5, 0, 3, 0]],
    descriptions: [
      "Inside buildings and/or every space with people might stay continuously more than 4 hours (garden, terrace, balcony, etc)",
      "Every space with people usually stay continuously less than 4 hours (streets, open fields, etc)",
      "75% of EMF Limits 2",
      "Full safety value",
    ],
  },
  {
    label: "Chine",
    country: "China",
    rows: [[100000, 12, 0, 137, 0, M, M, M, M]],
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Russie",
    country: "Russia",
    rows: [
      [300, 3, 0, M, M, M, M, M, M],
      [100000, 6.1, 0, M, M, M, M, M, M],
    ],
    descriptions: ["Public exposure limits", "", "", ""],
  },
  {
    label: "Pologne",
    country: "Poland",
    rows: [[100000, 7, 0, 60, 0, M, M, M, M]],
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Inde",
    country: "India",
    rows: standardRows(8.854, 0.435, 19.29),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Lituanie",
    country: "Lithuania",
    rows: standardRows(8.854, 0.435, 19.29),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Slovénie",
    country: "Slovenia",
    rows: standardRows(8.854, 0.435, 19.29),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Grèce",
    country: "Greece",
    rows: standardRows(21.56, 1.059, 46.97),
    descriptions: null,
  },
  {
    label: "Croatie",
    country: "Croatia",
    rows: standardRows(11.2, 0.55, 24.4),
    descriptions: PUBLIC_NAMES,
  },
  {
    label: "Bulgarie",
    country: "Bulgaria",
    rows: [
      [400, 6, 0, 61, 0, M, M, M, M],
      [2000, 6, 0, 3, 0.5, M, M, M, M],
      [1000000, 6, 0, 137, 0, M, M, M, M],
    ],
    descriptions: PUBLIC_NAMES,
  },
];

export function applyProjectPreset(
  label: string,
  existingLimits: readonly LimitSet[],
): LimitSet[] | null {
  const preset = PRESETS.find(
    (entry) => entry.label === label || entry.country === label,
  );
  if (!preset) return null;
  return Array.from({ length: 4 }, (_, index) => {
    const existing = existingLimits[index];
    return {
      id: existing?.id ?? crypto.randomUUID(),
      name:
        preset.descriptions?.[index] ?? existing?.name ?? PUBLIC_NAMES[index],
      color: existing?.color ?? COLORS[index],
      active: existing?.active ?? true,
      use_alpha24: existing?.use_alpha24 ?? true,
      use_ftdc: existing?.use_ftdc ?? true,
      use_fpr: existing?.use_fpr ?? true,
      rows: preset.rows.map((row) => ({
        frequency: row[0],
        value: row[index * 2 + 1],
        exponent: row[index * 2 + 2],
      })),
    };
  });
}
