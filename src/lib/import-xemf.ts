import { readNrbf } from "./nrbf";
import { newPoint, newProject, newSource, uid } from "./project";
import { deg, toLocal } from "./geometry";
import { terrainHeight } from "./geometry";
import { dotnetColor } from "./dotnet-color";
import { normalizeMapProvider } from "./map-providers";
import { validateRasterDimensions } from "./raster-validation";
import type { PlannerProject, SavedCutResult } from "../types";
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
  const area = arr(settings.HorAreaCoordM).map(obj);
  const calculationArea =
    area.length === 2
      ? {
          x_min: Math.min(n(area[0], "Xm"), n(area[1], "Xm")),
          x_max: Math.max(n(area[0], "Xm"), n(area[1], "Xm")),
          y_min: Math.min(n(area[0], "Ym"), n(area[1], "Ym")),
          y_max: Math.max(n(area[0], "Ym"), n(area[1], "Ym")),
        }
      : undefined;
  const provider = s(settings, "NameTileServer");
  const areaWM = arr(settings.HorAreaCoordWM).map(obj);
  const site = obj(settings.PRTOCoordWGS);
  const divisions = arr(settings.MaxHorSize).map(Number);
  p.settings = {
    ...p.settings,
    site_coordinates: [
      n(site, "Lat", p.origin[0]),
      n(site, "Lon", p.origin[1]),
    ],
    calculation_area_wm:
      areaWM.length === 2
        ? (areaWM.map((c) => [n(c, "Xwm"), n(c, "Ywm")]) as [
            [number, number],
            [number, number],
          ])
        : undefined,
    horizontal_grid_divisions:
      divisions.length === 2 ? [divisions[0], divisions[1]] : [600, 200],
    show_calculation_area: bool(settings, "ShowHorizPlaneScale"),
    show_origin: bool(settings, "ShowOrigin"),
    antenna_capture_distance: n(settings, "LengthAttractAntennas", 15),
    reflection: n(settings, "KoefOslabl", 1),
    consider_terrain: bool(settings, "ConsiderRelief"),
    buildings_mode:
      (["ignore", "screen", "attenuate"] as const)[
        n(settings, "UseBuildings")
      ] ?? "ignore",
    building_attenuation_db: n(settings, "BuildAttenSubband1", 20),
    building_attenuation_high_db: n(settings, "BuildAttenSubband2", 6),
    building_frequency_boundary_mhz: n(settings, "BuildFreqBorder", 400),
    point_measurement_frequency_mhz: n(
      settings,
      "ControlPointMesureFreq",
      1000,
    ),
    selected_limit: n(settings, "ControlPointLimitIndex"),
    radius_m: calculationArea
      ? Math.max(...Object.values(calculationArea).map(Math.abs))
      : 150,
    terrain_interpolation: "original",
    calculation_area_m: calculationArea,
    show_basemap: bool(settings, "ShowMap"),
    map_provider: normalizeMapProvider(provider || "Carto Basemap"),
    contour_width: n(settings, "LineWidthZO", 2),
    display_options: {
      grid: bool(settings, "ShowHorizPlaneScale", false),
      buildings: bool(settings, "ShowAllBuildings"),
      terrain: bool(settings, "ShowRelief", false),
      lines:
        bool(settings, "ShowRoads") ||
        bool(settings, "ShowFences") ||
        bool(settings, "ShowText"),
      sources: bool(settings, "ShowAntennas"),
      points: bool(settings, "ShowControlPoints"),
      contours: bool(settings, "ShowConturZO"),
      labels: bool(settings, "ShowAntennaLabels"),
      azimuths: bool(settings, "ShowAntennaAzimuth"),
    },
    building_labels: {
      name: bool(settings, "ShowBuildingText"),
      height: bool(settings, "ShowBuildingHeight"),
      foundation: bool(settings, "ShowBuildingFoundation", false),
    },
    line_visibility: {
      roads: bool(settings, "ShowRoads"),
      fences: bool(settings, "ShowFences"),
      labels: bool(settings, "ShowText"),
    },
    control_point_labels: {
      prefix: s(settings, "ControlPointPrefix"),
      groups: bool(settings, "ShowControlPointGroups"),
      heights: bool(settings, "ShowControlPointHeight"),
    },
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
      frequency_end_mhz: n(t, "F2") || undefined,
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
      color: dotnetColor(obj(t.Color), "#ffffff"),
      feeder:
        s(t, "Cable") || s(t, "Jumper")
          ? {
              cable: s(t, "Cable"),
              length_m: n(t, "LengthCable"),
              jumper: s(t, "Jumper"),
              jumper_lengths: s(t, "LengthJumper", "0"),
              automatic_connectors: true,
              include_connectors: bool(t, "ChkLossSocket", false),
              connectors: bool(t, "ChkLossSocket", false)
                ? 2 +
                  s(t, "LengthJumper", "0")
                    .split("+")
                    .filter((v) => Number(v.replace(",", ".")) > 0).length *
                    2
                : 0,
              connector_loss: n(t, "LossSocketOne"),
              other_loss: n(t, "ExtraLoss"),
            }
          : undefined,
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
        group_active: bool(g, "Active", true),
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
      active: true,
      custom_attenuation: bool(b, "UseAttenuation"),
      visible: bool(b, "Show"),
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
  p.terrain_contours = arr(root.ElLN).map((r, i) => {
    const t = obj(r);
    return {
      id: uid(),
      name: `Relief ${i + 1}`,
      elevation: n(t, "H"),
      closed: bool(t, "CloseLine", false),
      points: arr(t.CoordWM).map(local),
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
        border_width: n(l, "WidthBorder", 1),
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
        fence_type: n(l, "Type"),
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
        font_name: s(l, "FontName"),
        font_size: n(l, "Size", 10),
        angle_deg: n(l, "Angle"),
        show_point: bool(l, "Point", false),
      };
    }),
  ];
  p.horizontal_cuts = arr(root.HP).map((r) => {
    const c = obj(r);
    return {
      id: uid(),
      active: bool(c, "Activ"),
      height_m: n(c, "Height"),
      color: dotnetColor(obj(c.Color), "#d32246"),
      line_style:
        ["solid", "dash", "dot", "dashdot"][n(c, "LineType")] ?? "solid",
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
      geo_position: c.CoordWM ? geo(c.CoordWM) : undefined,
      backward_m: n(c, "L2", 100),
      forward_m: n(c, "L1", 100),
      max_height_m: n(c, "H1", 100),
      min_height_m: n(c, "H2", 0),
      name: s(c, "Name"),
      dimensions: arr(c.DimensionZO).map((v) => {
        const d = obj(v);
        return { id: uid(), x: n(d, "R"), y1: n(d, "H"), y2: n(d, "Hz") };
      }),
    };
  });
  const limitList = obj(settings.LMT),
    limitRows = arr(limitList._items).slice(0, n(limitList, "_size"));
  const descriptions = arr(settings.LimitDescription);
  p.limits = p.limits.map((l, i) => ({
    ...l,
    name: String(descriptions[i] ?? l.name),
    color: dotnetColor(obj(arr(settings.LimitColor)[i]), l.color),
    active: arr(settings.HorizCutLimits)[i] === true,
    use_alpha24: arr(settings.LimitAlpha24)[i] === true,
    use_ftdc: arr(settings.LimitFtdc)[i] === true,
    use_fpr: arr(settings.LimitFpr)[i] === true,
    rows: limitRows.map((r) => {
      const t = obj(r);
      return {
        frequency: n(t, "Freq"),
        value: Number(arr(t.E)[i]),
        exponent: Number(arr(t.A)[i]),
      };
    }),
  }));
  const aliases = arr(settings.ALS),
    aliasCount = aliases.length / 2;
  if (Number.isInteger(aliasCount)) {
    p.report_aliases = Object.fromEntries(
      aliases
        .slice(0, aliasCount)
        .map((key, i) => [
          String(key ?? ""),
          String(aliases[i + aliasCount] ?? ""),
        ]),
    );
  }
  const raster = obj(root.RPL),
    bitmap = obj(raster.BTM),
    rawImage = deref(bitmap.Data);
  const image = rawImage instanceof Uint8Array ? rawImage : arr(bitmap.Data);
  if (image.length) {
    const bytes =
        image instanceof Uint8Array
          ? image
          : Uint8Array.from(image as number[]),
      dimensions = validateRasterDimensions(bytes);
    if (dimensions) {
      const pixels = arr(raster.PointRaster).map(obj),
        maps = arr(raster.PointMap).map(obj);
      if (pixels.length >= 3 && maps.length >= 3) {
        const sorted = [pixels[1], pixels[2]].sort(
          (a, b) => n(a, "Xpix") - n(b, "Xpix"),
        );
        const span = Math.hypot(
          n(sorted[1], "Xpix") - n(sorted[0], "Xpix"),
          n(sorted[1], "Ypix") - n(sorted[0], "Ypix"),
        );
        const byMap = bool(raster, "UseScalingLine", false);
        const mapSorted = [maps[1], maps[2]].sort(
          (a, b) => n(a, "Xwm") - n(b, "Xwm"),
        );
        const anchor = byMap ? mapSorted[0] : maps[0],
          pixel = byMap ? sorted[0] : pixels[0];
        const mapSpan = Math.hypot(
          n(mapSorted[1], "Xwm") - n(mapSorted[0], "Xwm"),
          n(mapSorted[1], "Ywm") - n(mapSorted[0], "Ywm"),
        );
        const latitude = geo(anchor)[0];
        const scale = byMap
          ? mapSpan / span
          : n(raster, "Length") / (span * Math.cos((latitude * Math.PI) / 180));
        if (Number.isFinite(scale) && scale > 0) {
          const left = n(anchor, "Xwm") - n(pixel, "Xpix") * scale,
            top = n(anchor, "Ywm") + n(pixel, "Ypix") * scale;
          const [north, west] = geo({ Xwm: left, Ywm: top });
          const [south, east] = geo({
            Xwm: left + dimensions[0] * scale,
            Ywm: top - dimensions[1] * scale,
          });
          let binary = "";
          for (let i = 0; i < bytes.length; i += 8192)
            binary += String.fromCharCode(...bytes.subarray(i, i + 8192));
          const mime =
            bytes[0] === 0xff
              ? "image/jpeg"
              : bytes[0] === 0x42
                ? "image/bmp"
                : bytes[0] === 0x47
                  ? "image/gif"
                  : "image/png";
          p.background_image = {
            data_url: `data:${mime};base64,${btoa(binary)}`,
            west,
            south,
            east,
            north,
            opacity: 0.7,
          };
        }
      }
    }
  }
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
  const horizontal: SavedCutResult[] = [],
    vertical: SavedCutResult[] = [];
  const emptyGrid = (height: number) => ({
    width: 2,
    height: 2,
    x0: calculationArea?.x_min ?? -150,
    y0: calculationArea?.y_min ?? -150,
    dx: calculationArea ? calculationArea.x_max - calculationArea.x_min : 300,
    dy: calculationArea ? calculationArea.y_max - calculationArea.y_min : 300,
    values: [],
    fields: [],
    height_m: height,
  });
  const segments = (v: unknown, convert: (v: unknown) => [number, number]) =>
    arr(v).flatMap((poly) => {
      const path = arr(poly).map(convert);
      return path.slice(1).map((point, i) => [path[i], point]);
    });
  if (bool(settings, "FlagLastRezManyLimits", false)) {
    arr(root.HP_PLNwm).forEach((v, i) => {
      const cut = p.horizontal_cuts[0];
      const paths = segments(v, local);
      if (!cut || !paths.length) return;
      horizontal.push({
        id: uid(),
        cut_id: cut.id,
        limit_index: i,
        contours_only: true,
        grid: emptyGrid(cut.height_m),
        contours: {
          level: 1,
          color: p.limits[i]?.color ?? cut.color,
          height_m: cut.height_m,
          segments: paths,
        },
      });
    });
  } else
    arr(root.HP).forEach((v, i) => {
      const cut = p.horizontal_cuts[i],
        paths = segments(obj(v).PLNwm, local);
      if (!paths.length || !cut) return;
      horizontal.push({
        id: uid(),
        cut_id: cut.id,
        contours_only: true,
        grid: emptyGrid(cut.height_m),
        contours: {
          level: 1,
          color: cut.color,
          height_m: cut.height_m,
          segments: paths,
        },
      });
    });
  const originAltitude = terrainHeight(p, 0, 0);
  arr(root.VP).forEach((v, ci) => {
    const raw = obj(v),
      cut = p.vertical_cuts[ci],
      size = n(raw, "Size");
    if (!cut || size <= 0) return;
    const l1 = n(raw, "LastL1", cut.forward_m),
      l2 = n(raw, "LastL2", cut.backward_m),
      h1 = n(raw, "LastH1", cut.max_height_m),
      h2 = n(raw, "LastH2", 0);
    arr(raw.PLNs).forEach((v, li) => {
      const paths = segments(v, (v) => {
        const point = obj(v);
        return [
          -l2 + (n(point, "X") / size) * (l1 + l2),
          n(raw, "Elev0") -
            originAltitude -
            h2 +
            (n(point, "Y") / size) * (h1 + h2),
        ];
      });
      if (!paths.length) return;
      vertical.push({
        id: uid(),
        cut_id: cut.id,
        limit_index: li,
        contours_only: true,
        grid: {
          ...emptyGrid(0),
          width: size + 1,
          height: size + 1,
          x0: -l2,
          y0: n(raw, "Elev0") - originAltitude - h2,
          dx: (l1 + l2) / size,
          dy: (h1 + h2) / size,
        },
        contours: {
          level: 1,
          color: p.limits[li]?.color ?? "#ff0000",
          height_m: 0,
          segments: paths,
        },
      });
    });
  });
  if (horizontal.length || vertical.length)
    p.saved_cut_results = { horizontal, vertical };
  return p;
}
