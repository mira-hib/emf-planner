import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync, readdirSync } from "node:fs";
import { createHash } from "node:crypto";
import {
  newProject,
  newPoint,
  newSource,
  normalizeProject,
} from "../src/lib/project.ts";
import {
  calculatePoints,
  calculatePointHeightRange,
  exposureAt,
  eirp,
  sourceField,
  verticalGrid,
  horizontalGrid,
  contours,
} from "../src/lib/engine.ts";
import { parseMsi, decodeMsi, writeMsi } from "../src/lib/antenna.ts";
import {
  sourcePower,
  powerFromWatts,
  powerToWatts,
} from "../src/lib/source-power.ts";
import { toGeo, terrainHeight } from "../src/lib/geometry.ts";
import { rebaseProject } from "../src/lib/origin-grid.ts";
import { copySourceRows, pasteSourceRows } from "../src/lib/source-rows.ts";
import { symmetricPattern } from "../src/lib/editing.ts";
import { formatCoordinate, parseCoordinate } from "../src/lib/coordinates.ts";
import type { PlannerProject, RadiatingSource } from "../src/types";

const root = "test/emf_test_kit/data/";
const text = (path: string) => readFileSync(root + path, "utf8");
const tsv = (path: string) =>
  text(path)
    .trim()
    .split(/\r?\n/)
    .slice(1)
    .map((l) => l.split("\t"));
function close(actual: number, expected: number, tolerance = 1e-10) {
  assert.ok(
    Number.isFinite(actual) && Math.abs(actual - expected) <= tolerance,
    `${actual} ≠ ${expected}, tolerance ${tolerance}`,
  );
}
function base(): PlannerProject {
  const p = newProject();
  p.settings.reflection = 1; // Kit BASE-0 explicitly specifies k=1.
  p.origin = [45.485337, 9.198818];
  p.settings.calculation_area_m = {
    x_min: -100,
    x_max: 100,
    y_min: -100,
    y_max: 100,
  };
  p.settings.horizontal_grid_divisions = undefined;
  p.settings.building_attenuation_high_db = undefined;
  p.limits = [6, 20, 4.5, 10].map((value, i) => ({
    ...p.limits[i],
    rows: [{ frequency: 1e6, value, exponent: 0 }],
    active: true,
    use_alpha24: false,
    use_ftdc: false,
    use_fpr: false,
  }));
  p.sources = [
    {
      ...newSource(p.origin, "TS06_ISO_1W"),
      power_w: 1,
      height_m: 10,
      gain_dbi: 0,
      pattern_h: Array(360).fill(0),
      pattern_v: Array(360).fill(0),
    },
  ];
  return p;
}
function point(p: PlannerProject, x = 100, y = 0, height = 1.5) {
  const c = newPoint(p.origin, "P1");
  const [lat, lng] = toGeo(x, y, p.origin);
  Object.assign(c, { x_m: x, y_m: y, lat, lng });
  c.heights![0].height_m = height;
  p.control_points = [c];
  return c;
}
const sourceKeys: (keyof RadiatingSource)[] = [
  "name",
  "sector_id",
  "service_type",
  "frequency_mhz",
  "power_w",
  "num_tx",
  "total_attenuation_db",
  "antenna_model",
  "azimuth_deg",
  "gain_dbi",
  "height_m",
  "antenna_size_m",
];
const oracleSources = () =>
  tsv("sources/sources_oracle.tsv").map((row) =>
    Object.assign(
      newSource(base().origin, row[0]),
      Object.fromEntries(
        row.map((v, i) => [sourceKeys[i], i < 3 || i === 7 ? v : Number(v)]),
      ),
    ),
  );

for (const row of tsv("sources/expected_from_formulas.tsv")) {
  test(`SRC-17 SRC-18 HCT-11 [F] kit source ${row[0]}`, () => {
    const s = oracleSources().find((s) => s.name === row[0])!;
    close(sourcePower(s).antenna_w, Number(row[1]), 0.0001);
    close(eirp(s), Number(row[2]), 0.01);
    const p = base();
    p.sources = [
      {
        ...s,
        height_m: 0,
        azimuth_deg: 0,
        pattern_h: Array(360).fill(0),
        pattern_v: Array(360).fill(0),
      },
    ];
    close(exposureAt(p, 10, 0, 0).total_field, Number(row[3]), 0.0001);
    [6, 20, 4.5, 10].forEach((limit, i) =>
      close(Math.sqrt(30 * eirp(s)) / limit, Number(row[i + 4]), 0.011),
    );
  });
}
for (const row of tsv("units/unit_conversion_cases.tsv")) {
  test(`SRC-11 SRC-12 [F] kit conversion ${row[0]} W`, () => {
    const watts = Number(row[0]);
    if (watts < 0) {
      assert.throws(() => powerToWatts(watts, "W"));
      return;
    }
    close(
      powerFromWatts(watts, "mW"),
      Number(row[1]),
      Math.max(1e-12, watts * 1e-10),
    );
    for (const [unit, expected] of [
      ["dBW", row[2]],
      ["dBm", row[3]],
    ] as const) {
      if (!watts) assert.throws(() => powerFromWatts(watts, unit));
      else {
        close(powerFromWatts(watts, unit), Number(expected), 0.000051);
        close(
          powerToWatts(powerFromWatts(watts, unit), unit),
          watts,
          Math.max(1e-15, watts * 1e-12),
        );
      }
    }
  });
}
for (const row of tsv("control_points/iso_expected_1W_0dBi_h10_cp1.5.tsv")) {
  test(`PNT-03 PNT-05 PNT-06 [F] kit point ${row[0]}`, () => {
    const p = base();
    point(p, Number(row[1]));
    const r = calculatePoints(p)[0];
    close(r.total_field, Number(row[4]), 0.0001);
    close(r.total_field, Math.sqrt(30) / Math.hypot(Number(row[1]), 8.5));
    close(r.exposure_ratio, (r.total_field / 6) ** 2);
    close(r.total_power_density, r.total_field ** 2 / 377);
  });
}
const transformations: [string, (p: PlannerProject) => void, number][] = [
  [
    "MET-01 power",
    (p) => {
      p.sources[0].power_w = 4;
    },
    2,
  ],
  [
    "MET-02 loss",
    (p) => {
      p.sources[0].total_attenuation_db = 20 * Math.log10(2);
    },
    0.5,
  ],
  [
    "MET-03 TX",
    (p) => {
      p.sources[0].num_tx = 4;
    },
    2,
  ],
  [
    "MET-04 gain",
    (p) => {
      p.sources[0].gain_dbi = 20 * Math.log10(2);
    },
    2,
  ],
  [
    "MET-05 reflection 1.3",
    (p) => {
      p.settings.reflection = 1.3;
    },
    1.3,
  ],
  [
    "MET-05 reflection 1.6",
    (p) => {
      p.settings.reflection = 1.6;
    },
    1.6,
  ],
  [
    "MET-06 duplicate",
    (p) => {
      p.sources.push({ ...p.sources[0], id: "copy" });
    },
    Math.SQRT2,
  ],
  [
    "MET-07 frequencies",
    (p) => {
      p.sources.push({ ...p.sources[0], id: "other", frequency_mhz: 2100 });
    },
    Math.SQRT2,
  ],
  [
    "MET-08 dBm",
    (p) => {
      p.sources[0].power_w = powerToWatts(30, "dBm");
    },
    1,
  ],
  [
    "MET-09 inactive",
    (p) => {
      p.sources.push({ ...p.sources[0], id: "off", active: false });
    },
    1,
  ],
  [
    "MET-12 zero",
    (p) => {
      p.sources[0].power_w = 0;
    },
    0,
  ],
  [
    "MET-12 tiny",
    (p) => {
      p.sources[0].power_w = 1e-12;
    },
    1e-6,
  ],
  [
    "SRC-22 MET-13 Ftdc",
    (p) => {
      p.limits[0].use_ftdc = true;
      p.sources[0].duty_cycle = 0.75;
    },
    Math.sqrt(0.75),
  ],
  [
    "SRC-22 MET-13 Ftdc Fpr",
    (p) => {
      p.limits[0].use_ftdc = p.limits[0].use_fpr = true;
      p.sources[0].duty_cycle = p.sources[0].power_reduction = 0.5;
    },
    0.5,
  ],
  [
    "SRC-22 MET-13 alpha",
    (p) => {
      p.limits[0].use_alpha24 = true;
      p.sources[0].alpha24 = 0.25;
    },
    0.5,
  ],
  [
    "SRC-22 MET-13 alpha zero",
    (p) => {
      p.limits[0].use_alpha24 = true;
      p.sources[0].alpha24 = 0;
    },
    0,
  ],
  [
    "SRC-24 MET-14 report fields",
    (p) => {
      p.sources[0].electrical_downtilt_deg = 40;
      p.sources[0].beam_width_h_deg = 3;
      p.sources[0].beam_width_v_deg = 70;
    },
    1,
  ],
];
for (const [name, change, factor] of transformations)
  test(`${name} [R] kit transformation`, () => {
    const p = base();
    const expected = exposureAt(p, 100, 0, 1.5).total_field * factor;
    change(p);
    close(exposureAt(p, 100, 0, 1.5).total_field, expected);
  });
test("PNT-04 LIM-04 [M/F] measured field changes total, density and exposure independently of attenuation", () => {
  const p = base(),
    c = point(p);
  c.heights![0].measured = 0.07;
  c.heights![0].attenuation_db = 6;
  const r = calculatePoints(p)[0];
  const calculated = (Math.sqrt(30) / Math.hypot(100, 8.5)) * 10 ** (-6 / 20);
  close(r.calculated_field!, calculated);
  close(r.total_field, Math.hypot(calculated, 0.07));
  close(r.exposure_ratio, (calculated ** 2 + 0.07 ** 2) / 36);
  close(r.total_power_density, (calculated ** 2 + 0.07 ** 2) / 377);
  p.sources = [];
  c.heights![0].measured = 6;
  assert.equal(calculatePoints(p)[0].exposure_ratio, 1);
  c.heights![0].measured = 3;
  assert.equal(calculatePoints(p)[0].exposure_ratio, 0.25);
});
test("LIM-04 [F] measurement frequency selects its own limit and ignores TX reduction factors", () => {
  const p = base();
  p.sources = [];
  const c = point(p);
  c.heights![0].measured = 2;
  p.limits[0].rows = [
    { frequency: 400, value: 2, exponent: 0 },
    { frequency: 2000, value: 4, exponent: 0 },
  ];
  p.settings.point_measurement_frequency_mhz = 400;
  close(calculatePoints(p)[0].exposure_ratio, 1);
  p.settings.point_measurement_frequency_mhz = 401;
  close(calculatePoints(p)[0].exposure_ratio, 0.25);
});
test("PNT-01 [M] inactive groups are excluded from calculation and survive project round trip", () => {
  const p = base(),
    c = point(p);
  c.group_active = false;
  assert.deepEqual(
    calculatePoints(normalizeProject(JSON.parse(JSON.stringify(p)))),
    [],
  );
});
test("BLD-11 BLD-12 [F] shield wall and the 120 m grazing threshold", () => {
  const p = base();
  p.sources[0].height_m = 20;
  p.buildings = [
    {
      id: "wall",
      name: "WALL",
      height_m: 50,
      foundation_m: 0,
      attenuation_db: 6,
      active: true,
      points: [
        [30, -30],
        [50, -30],
        [50, 30],
        [30, 30],
      ],
    },
  ];
  assert.ok(exposureAt(p, 100, 0, 1.5).total_field > 0);
  p.settings.buildings_mode = "screen";
  for (const height of [1.5, 60, 100, 119])
    assert.equal(exposureAt(p, 100, 0, height).total_field, 0);
  assert.ok(exposureAt(p, 100, 0, 121).total_field > 0);
});
test("BLD-13 BLD-14 BLD-15 [M/R] Building height starts at foundation, penetration is frequency dependent and optional", () => {
  const p = base(),
    c = point(p);
  c.location = "Building";
  p.buildings = [
    {
      id: "inside",
      name: "INSIDE",
      height_m: 10,
      foundation_m: 100,
      attenuation_db: 0,
      active: true,
      points: [
        [90, -10],
        [110, -10],
        [110, 10],
        [90, 10],
      ],
    },
  ];
  const reference = calculatePoints(p)[0];
  close(reference.elevation, 100);
  p.settings.buildings_mode = "attenuate";
  p.settings.building_attenuation_db = 6;
  p.settings.building_attenuation_high_db = 3;
  p.settings.building_frequency_boundary_mhz = 500;
  for (const [freq, loss] of [
    [150, 6],
    [500, 6],
    [900, 3],
  ]) {
    p.sources[0].frequency_mhz = freq;
    close(
      calculatePoints(p)[0].total_field / reference.total_field,
      10 ** (-loss / 20),
    );
  }
  p.buildings[0].active = false;
  close(calculatePoints(p)[0].total_field, reference.total_field);
});
test("BLD-14 [R] explicit zero loss is retained, overlapping buildings do not multiply penetration losses", () => {
  const p = base();
  const b = {
    id: "b",
    name: "B",
    height_m: 30,
    foundation_m: 0,
    attenuation_db: 0,
    active: true,
    points: [
      [90, -10],
      [110, -10],
      [110, 10],
      [90, 10],
    ] as [number, number][],
  };
  p.buildings = [b, { ...b, id: "b2" }];
  const e = exposureAt(p, 100, 0, 1.5).total_field;
  p.settings.buildings_mode = "attenuate";
  close(exposureAt(p, 100, 0, 1.5).total_field, e);
  b.attenuation_db = 6;
  close(exposureAt(p, 100, 0, 1.5).total_field / e, 10 ** (-6 / 20));
});
test("TER-01 TER-04 MET-14 [F/R] terrain plane and altitude invariance", () => {
  const p = base();
  point(p, 25, 25);
  p.terrain_points = [
    [0, 0, 100],
    [100, 0, 110],
    [0, 100, 100],
  ].map(([x, y, elevation], i) => ({ id: String(i), x, y, elevation }));
  close(terrainHeight(p, 25, 25), 102.5);
  const before = calculatePoints(p)[0];
  const raised = {
    ...p,
    terrain_points: p.terrain_points.map((t) => ({
      ...t,
      elevation: t.elevation + 100,
    })),
  };
  close(calculatePoints(raised)[0].total_field, before.total_field);
});
test("SRC-07 [M] source paste across origins retains geographic position and rebases local coordinates", () => {
  const p = base(),
    buffer = copySourceRows(p.sources, [p.sources[0].id], p.origin)!;
  const origin = toGeo(100, 0, p.origin);
  const copy = pasteSourceRows([], "", buffer, undefined, origin).sources[0];
  close(copy.x_m!, -100, 0.1);
  close(copy.y_m!, 0, 0.1);
  assert.equal(copy.lat, p.sources[0].lat);
  assert.equal(copy.lng, p.sources[0].lng);
  assert.deepEqual(
    pasteSourceRows([], "", buffer, undefined, p.origin).sources[0].pattern_h,
    p.sources[0].pattern_h,
  );
});
test("ORI-04 MET-14 [R] eastward origin move retains the isotropic result at 0.5% tolerance", () => {
  const p = base();
  point(p);
  const e = calculatePoints(p)[0].total_field;
  const changed = rebaseProject(p, toGeo(100, 0, p.origin));
  close(calculatePoints(changed)[0].total_field, e, e * 0.005);
});
test("PNT-09 [F] coincident, sub-millimetre and distant points stay finite", () => {
  const p = base();
  for (const [x, z] of [
    [0, 10],
    [0.0001, 10],
    [100000, 1.5],
  ]) {
    const e = exposureAt(p, x, 0, z);
    assert.ok(Number.isFinite(e.total_field));
    assert.ok(e.total_field >= 0);
  }
});
test("VCT-06 VCT-07 [F/X] vertical grid and point agree at the kit coordinates", () => {
  const p = base();
  const g = verticalGrid(
    p,
    {
      id: "v",
      active: true,
      azimuth_deg: 90,
      x: 0,
      y: 0,
      forward_m: 100,
      backward_m: 0,
      max_height_m: 30,
    },
    1,
  );
  for (const [x, z] of [
    [5, 10],
    [10, 10],
    [50, 10],
    [100, 10],
    [50, 20],
    [100, 0],
    [20, 30],
  ]) {
    close(g.fields[z * g.width + x], Math.sqrt(30) / Math.hypot(x, z - 10));
    point(p, x, 0, z);
    close(calculatePoints(p)[0].total_field, g.fields[z * g.width + x]);
  }
});
test("HCT-01 VCT-05 [F] isotropic contour radii from the supplied kit", () => {
  const p = base();
  p.sources[0].power_w = 1000;
  const g = horizontalGrid(p, 10, 1);
  const c = contours(g, 1, "red");
  assert.ok(c.segments.length > 100);
  for (const s of c.segments)
    for (const [x, y] of s) close(Math.hypot(x, y), Math.sqrt(30000) / 6, 0.1);
});
for (const name of readdirSync(root + "msi").filter((n) =>
  n.startsWith("ok_"),
)) {
  test(`SRC-30 SRC-39 [F] kit MSI ${name}`, () => {
    const a = parseMsi(decodeMsi(readFileSync(root + "msi/" + name)));
    assert.equal(a.horizontal.length, 360);
    assert.equal(a.vertical.length, 360);
    assert.ok([...a.horizontal, ...a.vertical].every(Number.isFinite));
    const b = parseMsi(writeMsi(a.name, a.gain, a.horizontal, a.vertical));
    close(b.gain, a.gain);
    a.horizontal.forEach((v, i) => close(b.horizontal[i], v, 0.000051));
  });
}
test("SRC-25 MET-11 [F/R] negative mechanical tilt points down, rotation preserves the lobe", () => {
  const p = base(),
    a = parseMsi(text("msi/ok_sector65x7_17dBi.msi"));
  Object.assign(p.sources[0], {
    height_m: 30,
    azimuth_deg: 90,
    mechanical_downtilt_deg: -10,
    pattern_h: a.horizontal,
    pattern_v: a.vertical,
  });
  const down = exposureAt(p, 170, 0, 0).total_field;
  p.sources[0].mechanical_downtilt_deg = 10;
  // The supplied vertical pattern has a 20 dB floor: the residual amplitude is 0.1.
  close(exposureAt(p, 170, 0, 0).total_field / down, 0.1, 0.0001);
  p.sources[0].mechanical_downtilt_deg = -10;
  p.sources[0].azimuth_deg = 0;
  close(exposureAt(p, 0, 170, 0).total_field, down);
});
test("SRC-24 [M] electrical tilt is report metadata even on legacy synthetic patterns", () => {
  const p = base();
  delete p.sources[0].pattern_v;
  const e = sourceField(p, p.sources[0], 100, 0, 1.5).field;
  p.sources[0].electrical_downtilt_deg = 30;
  p.sources[0].beam_width_h_deg = 2;
  p.sources[0].beam_width_v_deg = 90;
  close(sourceField(p, p.sources[0], 100, 0, 1.5).field, e);
});
for (const name of readdirSync(root + "msi").filter((n) =>
  n.startsWith("bad_"),
)) {
  test(`SRC-31 [O] malformed kit MSI ${name}`, () => {
    const bytes = readFileSync(root + "msi/" + name);
    // Native ClassDN intentionally accepts signed losses by taking their magnitude.
    if (name === "bad_negative_attenuation.msi") {
      assert.ok(parseMsi(decodeMsi(bytes)).horizontal.every((v) => v >= 0));
    } else assert.throws(() => parseMsi(decodeMsi(bytes)), name);
  });
}
test("SRC-37 [native divergence] historical generator floor differs from the kit's recalled F.699 formula", () => {
  const a = symmetricPattern(40000, 50, 2);
  assert.equal(a.gain - a.horizontal[90], -10);
  const row = tsv("msi/f699_expected_40GHz_50dBi_2.0m.tsv").find(
    (r) => Number(r[0]) === 90,
  )!;
  assert.equal(Number(row[1]), -13);
  assert.match(
    readFileSync(
      "reference/original-decompiled/EMFPlanner/frmParabola.cs",
      "utf8",
    ),
    /return -10f - Gdn/,
  );
});
test("ORI-03 [F] DMS rounding carries into the next degree in both hemispheres", () => {
  for (const v of [10.999999, -10.999999]) {
    const formatted = formatCoordinate(v, "dms", true);
    assert.match(formatted, /11° 0′ 0\.00″/);
    close(parseCoordinate(formatted, true), Math.sign(v) * 11);
  }
});

test("PNT-07 PNT-08 DOC-08 [M/X] height range ignores manual loss and measured field without changing the project", () => {
  const p = base(),
    c = point(p);
  c.heights![0].attenuation_db = 6;
  c.heights![0].measured = 0.07;
  const saved = JSON.stringify(p);
  const rows = calculatePointHeightRange(
    p,
    Array.from({ length: 11 }, (_, i) => i),
  );
  assert.equal(rows.length, 11);
  assert.equal(JSON.stringify(p), saved);
  for (const r of rows) {
    close(r.total_field, Math.sqrt(30) / Math.hypot(100, r.height_m - 10));
    assert.equal(r.measured, 0);
  }
  assert.throws(() => calculatePointHeightRange(p, [NaN]));
  assert.throws(() => calculatePointHeightRange(p, Array(1001).fill(0)));
});
test("SET-01 SET-02 PRJ-02 ROB-02 [F] invalid calculation settings reject the entire imported project", () => {
  const p = base();
  point(p);
  assert.doesNotThrow(() => normalizeProject({ ...p, settings: { ...p.settings, reflection: Math.fround(1.6) } }));
  assert.throws(() => normalizeProject({ ...p, settings: { ...p.settings, building_attenuation_high_db: Infinity } }));
  for (const [key, values] of Object.entries({
    reflection: [0, 0.99, 1.61, NaN, Infinity],
    grid_step_m: [0, -1, NaN],
    fine_step_m: [0, -1],
    selected_limit: [-1, 4, 1.5],
    point_measurement_frequency_mhz: [0, 29, 300001],
  }))
    for (const value of values)
      assert.throws(
        () =>
          normalizeProject({ ...p, settings: { ...p.settings, [key]: value } }),
        key,
      );
  for (const value of [0, 49, 1001, NaN])
    assert.throws(() =>
      normalizeProject({
        ...p,
        settings: { ...p.settings, horizontal_grid_divisions: [value, 200] },
      }),
    );
  for (const [key, value] of [
    ["num_tx", 1.5],
    ["num_tx", NaN],
    ["azimuth_deg", Infinity],
    ["lat", 91],
    ["total_attenuation_db", -1],
  ] as const)
    assert.throws(
      () =>
        normalizeProject({
          ...p,
          sources: [{ ...p.sources[0], [key]: value }],
        }),
      key,
    );
  assert.throws(() =>
    normalizeProject({
      ...p,
      control_points: [{ ...p.control_points[0], x_m: NaN }],
    }),
  );
});
test("ORI-02 ROB-02 [F] ambiguous geographic text cannot silently become a different coordinate", () => {
  for (const s of ["1e3", "N45S", "-45 N", "91", "abc", "45 60 0", "45 0 60"])
    assert.throws(() => parseCoordinate(s, true), s);
});
test("PRJ-05 ROB-05 [F] 1000 kit sources survive save/reopen and give repeatable finite point results", () => {
  const p = base();
  point(p);
  p.sources = tsv("sources/sources_stress_1000.tsv").map((row) =>
    Object.assign(
      newSource(p.origin, row[0]),
      Object.fromEntries(
        row.map((v, i) => [sourceKeys[i], i < 3 || i === 7 ? v : Number(v)]),
      ),
    ),
  );
  const restored = normalizeProject(JSON.parse(JSON.stringify(p)));
  assert.equal(restored.sources.length, 1000);
  assert.deepEqual(restored.sources, p.sources);
  const first = calculatePoints(restored)[0];
  assert.ok(Number.isFinite(first.total_field) && first.total_field > 0);
  for (let i = 0; i < 5; i++)
    assert.deepEqual(calculatePoints(restored)[0], first);
});

test("SRC-25 MET-11 [original method] 1800 native CalcPPE cases across rotations, tilts and heights", () => {
  const fixture = JSON.parse(
    readFileSync("tests/fixtures/test-kit-native-oracle.json", "utf8"),
  );
  const source = readFileSync(fixture.source, "utf8").replace(/\r\n/g, "\n");
  const method = source
    .slice(
      source.indexOf("public static float CalcPPE("),
      source.indexOf("public static float CalcPointKB("),
    )
    .trim();
  assert.equal(
    createHash("sha256").update(method).digest("hex"),
    fixture.normalized_method_sha256,
  );
  assert.equal(fixture.cases.length, 1800);
  const patterns = new Map(
    ["ok_isotropic.msi", "ok_sector65x7_17dBi.msi", "ok_backfire_180.msi"].map(
      (name) => [name, parseMsi(text("msi/" + name))],
    ),
  );
  let maxRelative = 0;
  for (const c of fixture.cases) {
    const p = base(),
      pattern = patterns.get(c.pattern)!;
    Object.assign(p.sources[0], {
      power_w: c.power,
      num_tx: c.tx,
      total_attenuation_db: c.loss,
      gain_dbi: c.gain,
      height_m: c.height,
      azimuth_deg: c.azimuth,
      mechanical_downtilt_deg: c.tilt,
      pattern_h: pattern.horizontal,
      pattern_v: pattern.vertical,
    });
    const actual = sourceField(p, p.sources[0], c.x, c.y, c.z).field;
    const relative =
      Math.abs(actual - c.native_field) / Math.max(c.native_field, 1e-12);
    maxRelative = Math.max(maxRelative, relative);
    assert.ok(relative < 0.0001, JSON.stringify({ ...c, actual, relative }));
  }
  console.log(
    `Native CalcPPE: 1800 cases, maximum relative field difference ${maxRelative}`,
  );
});
