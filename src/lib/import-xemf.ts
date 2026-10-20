import { readNrbf } from "./nrbf";
import { newPoint, newProject, newSource, uid } from "./project";
import { deg, toLocal } from "./geometry";
import type { PlannerProject } from "../types";
type Obj = Record<string, unknown>;
export function importXemf(buffer: ArrayBuffer, name: string): PlannerProject {
  const { root, deref } = readNrbf(buffer);
  const obj = (v: unknown) => (deref(v) ?? {}) as Obj;
  const arr = (v: unknown): unknown[] => {
    const r = deref(v);
    return Array.isArray(r) ? r.map(deref) : [];
  };
  const n = (o: Obj, k: string, f = 0) =>
    typeof o[k] === "number" ? (o[k] as number) : f;
  const s = (o: Obj, k: string, f = "") =>
    typeof deref(o[k]) === "string" ? (deref(o[k]) as string) : f;
  const bool = (o: Obj, k: string, f = true) =>
    typeof o[k] === "boolean" ? (o[k] as boolean) : f;
  const settings = obj(root.SETT),
    origin = obj(settings.OriginCoordWGS),
    p = newProject();
  p.name = name.replace(/\.[^.]+$/, "");
  p.description = "Importé depuis EMFPlanner (.xemf)";
  p.origin = [n(origin, "Lat"), n(origin, "Lon")];
  p.map_center = p.origin;
  p.map_zoom = n(settings, "ZOOM", 19);
  p.settings = {
    ...p.settings,
    reflection: n(settings, "KoefOslabl", 1),
    consider_terrain: bool(settings, "ConsiderRelief"),
    buildings_mode:
      (["ignore", "screen", "attenuate"] as const)[
        n(settings, "UseBuildings")
      ] ?? "ignore",
    building_attenuation_db: n(settings, "BuildAttenSubband1", 20),
    selected_limit: n(settings, "ControlPointLimitIndex"),
    radius_m: 150,
    terrain_interpolation: "original",
  };
  const geo = (wm: unknown): [number, number] => {
    const c = obj(wm);
    return [
      deg(2 * Math.atan(Math.exp(n(c, "Ywm") / 6378137))) - 90,
      deg(n(c, "Xwm") / 6378137),
    ];
  };
  const local = (wm: unknown): [number, number] => {
    const [lat, lng] = geo(wm);
    return toLocal(lat, lng, p.origin);
  };
  p.sources = arr(root.TX).map((r, i) => {
    const t = obj(r),
      source = newSource(p.origin, s(t, "Name", `Source ${i + 1}`)),
      [lat, lng] = geo(t.CoordWM),
      [x, y] = toLocal(lat, lng, p.origin);
    return {
      ...source,
      active: bool(t, "Active"),
      frequency_mhz: n(t, "F", 900),
      power_w: n(t, "P"),
      gain_dbi: n(t, "G"),
      lat,
      lng,
      x_m: x,
      y_m: y,
      height_m: n(t, "H"),
      azimuth_deg: n(t, "Az"),
      mechanical_downtilt_deg: n(t, "Ug"),
      electrical_downtilt_deg: n(t, "ElectUg"),
      num_tx: n(t, "N", 1),
      total_attenuation_db: n(t, "Loss"),
      antenna_model: s(t, "AntType"),
      antenna_size_m: n(t, "D", 1),
      beam_width_h_deg: Number(s(t, "UgH", "65").replace(",", ".")) || 65,
      beam_width_v_deg: Number(s(t, "UgV", "10").replace(",", ".")) || 10,
      service_type: s(t, "TypeOfService"),
      location: s(t, "Location", "Tower"),
      owner: s(t, "Owner"),
      sector_id: s(t, "Sector"),
      is_existing: s(t, "Exist") === "Existing",
      pattern_h: arr(t.DnH) as number[],
      pattern_v: arr(t.DnV) as number[],
      limit_factors: [0, 1, 2, 3].map(
        (j) =>
          [
            arr(t.Alpha24)[j] ?? 1,
            arr(t.Ftdc)[j] ?? 1,
            arr(t.Fpr)[j] ?? 1,
          ] as number[],
      ),
      color: i >= 6 ? "#a5eda2" : "#ffffff",
    };
  });
  p.control_points = arr(root.CPG).flatMap((r, gi) => {
    const g = obj(r);
    return arr(g.CP).map((cr, ci) => {
      const c = obj(cr),
        [lat, lng] = geo(c.CoordWM),
        [x, y] = toLocal(lat, lng, p.origin);
      const heights = arr(c.H).map((h) => {
        const v = obj(h);
        return {
          id: uid(),
          height_m: n(v, "H", 1.5),
          attenuation_db: n(v, "L"),
          measured: n(v, "MeasuredE"),
          description: s(v, "Name"),
        };
      });
      return {
        ...newPoint(p.origin, `${gi + 1}.${ci + 1}`),
        lat,
        lng,
        x_m: x,
        y_m: y,
        group: s(g, "Name", `Groupe ${gi + 1}`),
        description: s(c, "Name"),
        location: s(c, "Location", "Ground"),
        heights,
        height_m: heights[0]?.height_m ?? 1.5,
      };
    });
  });
  p.buildings = arr(root.BLD).map((r, i) => {
    const b = obj(r);
    return {
      id: uid(),
      name: s(b, "Text", `${i + 1}`),
      height_m: n(b, "H"),
      foundation_m: n(b, "Fundation"),
      attenuation_db: p.settings.building_attenuation_db,
      points: arr(b.CoordWM).map(local),
      active: bool(b, "UseAttenuation"),
    };
  });
  p.terrain_points = arr(root.ElPT).map((r) => {
    const t = obj(r),
      [x, y] = local(t.CoordWM),
      wm = obj(t.CoordWM);
    return {
      id: uid(),
      x,
      y,
      elevation: n(t, "H"),
      wm_x: n(wm, "Xwm"),
      wm_y: n(wm, "Ywm"),
    };
  });
  p.lines = [
    ...arr(root.ROAD).map((r, i) => {
      const l = obj(r);
      return {
        id: uid(),
        name: `Route ${i + 1}`,
        kind: "road" as const,
        points: arr(l.CoordWM).map(local),
        color: "#707070",
        width: n(l, "WidthM", 1),
      };
    }),
    ...arr(root.FENCE).map((r, i) => {
      const l = obj(r);
      return {
        id: uid(),
        name: `Clôture ${i + 1}`,
        kind: "barrier" as const,
        points: arr(l.CoordWM).map(local),
        color: "#333333",
        width: n(l, "Width", 1),
      };
    }),
    ...arr(root.TXT).map((r) => {
      const l = obj(r);
      return {
        id: uid(),
        name: s(l, "Text"),
        kind: "label" as const,
        points: [local(l.CoordWM)],
        color: "#333333",
        width: 1,
      };
    }),
  ];
  p.horizontal_cuts = arr(root.HP).map((r) => {
    const c = obj(r);
    return {
      id: uid(),
      active: bool(c, "Activ"),
      height_m: n(c, "Height"),
      color: "#d32246",
    };
  });
  p.vertical_cuts = arr(root.VP).map((r) => {
    const c = obj(r),
      coord = obj(c.CoordM);
    return {
      id: uid(),
      active: bool(c, "Activ"),
      azimuth_deg: n(c, "Azimuth"),
      x: n(coord, "Xm"),
      y: n(coord, "Ym"),
      backward_m: n(c, "L2", 100),
      forward_m: n(c, "L1", 100),
      max_height_m: n(c, "H1", 100),
    };
  });
  const limitList = obj(settings.LMT),
    limitRows = arr(limitList._items).slice(0, n(limitList, "_size"));
  const descriptions = arr(settings.LimitDescription);
  p.limits = p.limits.map((l, i) => ({
    ...l,
    name: String(descriptions[i] ?? l.name),
    use_alpha24: arr(settings.LimitAlpha24)[i] === true,
    use_ftdc: arr(settings.LimitFtdc)[i] === true,
    use_fpr: arr(settings.LimitFpr)[i] === true,
    rows: limitRows
      .map((r) => {
        const t = obj(r);
        return {
          frequency: n(t, "Freq"),
          value: Number(arr(t.E)[i]),
          exponent: Number(arr(t.A)[i]),
        };
      })
      .filter((r) => r.frequency >= 0 && r.value > 0),
  }));
  if (p.limits.some((l) => !l.rows.length))
    throw new Error(
      "Le projet utilise un tableau de limites non pris en charge.",
    );
  // The original rounds local coordinates of sources/control points to 0.1 m.
  const rounded = (lat: number, lng: number): [number, number] => {
    const a = (lat * Math.PI) / 180,
      b = (p.origin[0] * Math.PI) / 180,
      dl = ((lng - p.origin[1]) * Math.PI) / 180,
      dx = Math.cos(a) * Math.sin(dl),
      dy = Math.cos(b) * Math.sin(a) - Math.sin(b) * Math.cos(a) * Math.cos(dl),
      distance = Math.fround(
        6378137 *
          Math.atan2(
            Math.hypot(dx, dy),
            Math.sin(b) * Math.sin(a) +
              Math.cos(b) * Math.cos(a) * Math.cos(dl),
          ),
      );
    let az = Math.fround(Math.atan2(dx, dy));
    if (az < 0) az = Math.fround(Math.PI * 2 + az);
    return [
      Math.round(distance * Math.sin(az) * 10) / 10,
      Math.round(distance * Math.cos(az) * 10) / 10,
    ];
  };
  p.sources = p.sources.map((s) => {
    const [x_m, y_m] = rounded(s.lat, s.lng);
    return { ...s, x_m, y_m };
  });
  p.control_points = p.control_points.map((c) => {
    const [x_m, y_m] = rounded(c.lat, c.lng);
    return { ...c, x_m, y_m };
  });
  return p;
}
