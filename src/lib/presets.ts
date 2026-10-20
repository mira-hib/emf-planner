import type { LimitSet } from "../types";

let _seq = 0;
const uid = () =>
  typeof crypto !== "undefined" && crypto.randomUUID
    ? crypto.randomUUID()
    : `preset-${Date.now()}-${++_seq}`;

interface Preset {
  label: string;
  preset: Omit<LimitSet, "id">;
}

// ICNIRP 2020 general public reference levels (Table 7)
// E = value × f^exponent where f is in MHz
const ICNIRP_2020: Preset = {
  label: "ICNIRP",
  preset: {
    name: "ICNIRP 2020 — Public",
    color: "#e53935",
    rows: [
      // 0.1–10 MHz: 83 V/m occupational, 28 V/m public (constant)
      { frequency: 10, value: 28, exponent: 0 },
      // 10–400 MHz: E = 0.8842 × f^0.5 (≈28 V/m at 1000 MHz region start)
      { frequency: 400, value: 0.8842, exponent: 0.5 },
      // 400–2000 MHz: E = 1.375 × f^0.5
      { frequency: 2000, value: 1.375, exponent: 0.5 },
      // 2000–300000 MHz: 61 V/m constant
      { frequency: 300000, value: 61, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// EU Council Recommendation 1999/519/EC — identical to ICNIRP 1998 public
const EU: Preset = {
  label: "Union européenne",
  preset: {
    name: "UE 1999/519/CE — Public",
    color: "#1e88e5",
    rows: [
      { frequency: 10, value: 28, exponent: 0 },
      { frequency: 400, value: 0.8842, exponent: 0.5 },
      { frequency: 2000, value: 1.375, exponent: 0.5 },
      { frequency: 300000, value: 61, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// FCC OET-65, general population / uncontrolled exposure
// 30–300 MHz: 27.5 V/m; 300–1500 MHz: E = 1.0 × f^0.5 (from S = f/1500 mW/cm²)
// 1500–100000 MHz: ~61.4 V/m (from S = 1 mW/cm²)
const USA: Preset = {
  label: "États-Unis",
  preset: {
    name: "FCC OET-65 — Uncontrolled",
    color: "#3949ab",
    rows: [
      { frequency: 1.34, value: 614, exponent: 0 },
      { frequency: 30, value: 19.42, exponent: 0.5 },
      { frequency: 300, value: 27.5, exponent: 0 },
      { frequency: 1500, value: 1.585, exponent: 0.5 },
      { frequency: 100000, value: 61.4, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// Switzerland NISV (Verordnung über den Schutz vor nichtionisierender Strahlung)
// Annex 1: installation limit values (Anlagegrenzwerte) for sensitive locations
// 900 MHz: 4 V/m; 1800 MHz: 6 V/m; general: ~5 V/m zone
const SWITZERLAND: Preset = {
  label: "Suisse",
  preset: {
    name: "Suisse NISV — Lieux sensibles",
    color: "#d32f2f",
    rows: [
      { frequency: 900, value: 4, exponent: 0 },
      { frequency: 1800, value: 6, exponent: 0 },
      { frequency: 2600, value: 6, exponent: 0 },
      { frequency: 300000, value: 6, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// United Kingdom — PHE (now UKHSA) follows ICNIRP 2020
const UK: Preset = {
  label: "Royaume-Uni",
  preset: {
    name: "Royaume-Uni UKHSA — Public",
    color: "#0d47a1",
    rows: [
      { frequency: 10, value: 28, exponent: 0 },
      { frequency: 400, value: 0.8842, exponent: 0.5 },
      { frequency: 2000, value: 1.375, exponent: 0.5 },
      { frequency: 300000, value: 61, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// Belgium — Brussels Region Ordinance 2007 (modified 2014)
// Cumulative norm: 6 V/m per antenna at 900 MHz reference
// 900 MHz: 6 V/m, scales with sqrt(f/900) for other frequencies
const BELGIUM: Preset = {
  label: "Belgique",
  preset: {
    name: "Belgique Bruxelles — 6 V/m",
    color: "#f9a825",
    rows: [
      { frequency: 10, value: 6, exponent: 0 },
      { frequency: 300, value: 6, exponent: 0 },
      { frequency: 900, value: 6, exponent: 0 },
      { frequency: 300000, value: 0.2, exponent: 0.5 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// Italy — DPCM 8 July 2003: attention value and quality objective 6 V/m
// for buildings with occupancy > 4 hours
const ITALY: Preset = {
  label: "Italie",
  preset: {
    name: "Italie DPCM 2003 — 6 V/m",
    color: "#2e7d32",
    rows: [
      { frequency: 3000, value: 6, exponent: 0 },
      { frequency: 300000, value: 6, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// China — GB 8702-2014
// 30–3000 MHz: 12 V/m (public), derived from 0.4 W/m² power density
const CHINA: Preset = {
  label: "Chine",
  preset: {
    name: "Chine GB 8702-2014 — Public",
    color: "#c62828",
    rows: [
      { frequency: 30, value: 12, exponent: 0 },
      { frequency: 3000, value: 12, exponent: 0 },
      { frequency: 300000, value: 12, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// Russia — SanPiN 2.1.8/2.2.4.1383-03
// 300–2400 MHz: 10 V/m for residential, derived from 10 µW/cm² (= 0.1 W/m²)
// Stricter than ICNIRP
const RUSSIA: Preset = {
  label: "Russie",
  preset: {
    name: "Russie SanPiN — Résidentiel",
    color: "#b71c1c",
    rows: [
      { frequency: 30, value: 10, exponent: 0 },
      { frequency: 300, value: 3, exponent: 0 },
      { frequency: 2400, value: 6.14, exponent: 0 },
      { frequency: 300000, value: 6.14, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// Poland — follows EU/ICNIRP with installation limit 7 V/m
// Regulation of the Minister of Health 2019: 7 V/m for accessible areas
const POLAND: Preset = {
  label: "Pologne",
  preset: {
    name: "Pologne — 7 V/m zones accessibles",
    color: "#ad1457",
    rows: [
      { frequency: 300, value: 7, exponent: 0 },
      { frequency: 300000, value: 7, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// India — DoT 2012: 1/10th of ICNIRP power density → 1/√10 ≈ 0.316× E field
// Effective: E_india ≈ ICNIRP × 0.316
const INDIA: Preset = {
  label: "Inde",
  preset: {
    name: "Inde DoT 2012 — 1/10 ICNIRP",
    color: "#ff6f00",
    rows: [
      { frequency: 10, value: 8.854, exponent: 0 },
      { frequency: 400, value: 0.2797, exponent: 0.5 },
      { frequency: 2000, value: 0.4349, exponent: 0.5 },
      { frequency: 300000, value: 19.29, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// Lithuania — follows EU 1999/519/EC (ICNIRP 1998 public)
// HN 80:2015 hygiene norm
const LITHUANIA: Preset = {
  label: "Lituanie",
  preset: {
    name: "Lituanie HN 80:2015 — Public",
    color: "#00695c",
    rows: [
      { frequency: 10, value: 28, exponent: 0 },
      { frequency: 400, value: 0.8842, exponent: 0.5 },
      { frequency: 2000, value: 1.375, exponent: 0.5 },
      { frequency: 300000, value: 61, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// Slovenia — Decree on electromagnetic radiation in the natural and living
// environment (Ur. l. RS 70/96): stricter, ~10 V/m for sensitive areas
const SLOVENIA: Preset = {
  label: "Slovénie",
  preset: {
    name: "Slovénie — Zones sensibles",
    color: "#0277bd",
    rows: [
      { frequency: 400, value: 10, exponent: 0 },
      { frequency: 2000, value: 10, exponent: 0 },
      { frequency: 300000, value: 10, exponent: 0 },
    ],
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
    active: true,
  },
};

// Greece — EEAE regulations: 70% of ICNIRP around antenna installations,
// 60% near schools, hospitals
// Using 70% of ICNIRP: E_greece = 0.7 × ICNIRP
const GREECE: Preset = {
  label: "Grèce",
  preset: {
    name: "Grèce EEAE — 70 % ICNIRP",
    color: "#1565c0",
    rows: [
      { frequency: 10, value: 19.6, exponent: 0 },
      { frequency: 400, value: 0.6189, exponent: 0.5 },
      { frequency: 2000, value: 0.9625, exponent: 0.5 },
      { frequency: 300000, value: 42.7, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// Croatia — follows EU with Ordinance on protection from electromagnetic fields
// (NN 146/2014): ICNIRP-based with stricter installation limits in sensitive zones
const CROATIA: Preset = {
  label: "Croatie",
  preset: {
    name: "Croatie NN 146/2014 — Public",
    color: "#283593",
    rows: [
      { frequency: 10, value: 28, exponent: 0 },
      { frequency: 400, value: 0.8842, exponent: 0.5 },
      { frequency: 2000, value: 1.375, exponent: 0.5 },
      { frequency: 300000, value: 61, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

// Bulgaria — follows EU/ICNIRP per Ordinance No. 9 / 1991 (amended)
const BULGARIA: Preset = {
  label: "Bulgarie",
  preset: {
    name: "Bulgarie — ICNIRP Public",
    color: "#4527a0",
    rows: [
      { frequency: 10, value: 28, exponent: 0 },
      { frequency: 400, value: 0.8842, exponent: 0.5 },
      { frequency: 2000, value: 1.375, exponent: 0.5 },
      { frequency: 300000, value: 61, exponent: 0 },
    ],
    use_alpha24: true,
    use_ftdc: true,
    use_fpr: true,
    active: true,
  },
};

export const PRESETS: Preset[] = [
  ICNIRP_2020,
  EU,
  USA,
  SWITZERLAND,
  UK,
  BELGIUM,
  ITALY,
  CHINA,
  RUSSIA,
  POLAND,
  INDIA,
  LITHUANIA,
  SLOVENIA,
  GREECE,
  CROATIA,
  BULGARIA,
];

export function applyPreset(label: string): LimitSet | null {
  const entry = PRESETS.find((p) => p.label === label);
  if (!entry) return null;
  return { id: uid(), ...entry.preset };
}
