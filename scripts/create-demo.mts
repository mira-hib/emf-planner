import { mkdirSync, writeFileSync } from "node:fs";
import { newPoint, newProject, newSource } from "../src/lib/project.ts";
import { toGeo } from "../src/lib/geometry.ts";
const p = newProject();
p.name = "Site de démonstration";
p.description =
  "Projet de démonstration synthétique : trois secteurs, bâtiments et points de contrôle.";
p.origin = [5.345, -4.025];
p.map_center = p.origin;
p.sources = [0, 120, 240].map((angle, i) => ({
  ...newSource(p.origin, `900_${angle}`),
  azimuth_deg: angle,
  sector_id: `Secteur ${i + 1}`,
  power_w: 12.05,
  gain_dbi: 16,
  height_m: 29,
  electrical_downtilt_deg: 6,
}));
p.limits = p.limits.map((l, i) => ({
  ...l,
  name: `Limite ${[20, 6, 4.5, 3][i]} V/m`,
  rows: [{ frequency: 100000, value: [20, 6, 4.5, 3][i], exponent: 0 }],
}));
p.control_points = [
  [30, 45],
  [-45, 30],
  [60, -30],
  [-30, -70],
].map(([x, y], i) => {
  const [lat, lng] = toGeo(x, y, p.origin);
  return { ...newPoint(p.origin, `1.${i + 1}`), lat, lng, x_m: x, y_m: y };
});
p.buildings = [
  {
    id: crypto.randomUUID(),
    name: "1",
    height_m: 12,
    foundation_m: 0,
    attenuation_db: 20,
    active: true,
    points: [
      [-65, 25],
      [-30, 25],
      [-30, 60],
      [-65, 60],
    ],
  },
  {
    id: crypto.randomUUID(),
    name: "2",
    height_m: 8,
    foundation_m: 0,
    attenuation_db: 20,
    active: true,
    points: [
      [45, 50],
      [75, 50],
      [75, 75],
      [45, 75],
    ],
  },
];
p.horizontal_cuts = [18, 24, 29].map((height_m, i) => ({
  id: crypto.randomUUID(),
  active: true,
  height_m,
  color: ["#d32246", "#2855dd", "#18813d"][i],
}));
p.vertical_cuts = [0, 120, 240].map((azimuth_deg) => ({
  id: crypto.randomUUID(),
  active: true,
  azimuth_deg,
  x: 0,
  y: 0,
  backward_m: 100,
  forward_m: 100,
  max_height_m: 60,
}));
mkdirSync("public/examples", { recursive: true });
writeFileSync("public/examples/demonstration.emfp", JSON.stringify(p, null, 2));
