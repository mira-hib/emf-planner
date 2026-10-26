import type { PlannerProject, SavedCutResult } from "../types";
import { toGeo, rad, terrainHeight } from "./geometry";
import { patternAmplitude } from "./engine";
import { validateRasterDimensions } from "./raster-validation";
import { completeLimit, NATIVE_FALLBACK_LIMIT } from "./limits";
import { DEFAULT_REPORT_ALIASES } from "./report-content";
import { writeNativeRecords, type NativeRecord } from "./write-nrbf";

const colour = (hex = "#000000") => ({
  name: null,
  value: (0xff000000 | parseInt(hex.slice(1), 16)) >>> 0,
  knownColor: 0,
  state: 2,
});
const wm = ([lat, lng]: [number, number]) => ({
  Xwm: 6378137 * rad(lng),
  Ywm: 6378137 * Math.log(Math.tan(Math.PI / 4 + rad(lat) / 2)),
});
const metres = ([x, y]: [number, number]) => ({ Xm: x, Ym: y });
const four = <T>(fn: (i: number) => T) =>
  Array.from({ length: 4 }, (_, i) => fn(i));
const crcTable = Uint32Array.from({ length: 256 }, (_, n) => {
  for (let i = 0; i < 8; i++) n = n & 1 ? 0xedb88320 ^ (n >>> 1) : n >>> 1;
  return n >>> 0;
});
function crc32(bytes: Uint8Array) {
  let n = 0xffffffff;
  for (const b of bytes) n = crcTable[(n ^ b) & 255] ^ (n >>> 8);
  return (n ^ 0xffffffff) >>> 0;
}
const extensionMagic = new TextEncoder().encode("EMFP-XEMF-EXT01!");

// The native reader stops at MessageEnd. A checked trailing extension preserves
// clone-only fields and exact precision, without changing native type layouts.
// A project re-saved by the original loses this extension and is imported normally.
export function nativeProjectExtension(
  bytes: Uint8Array,
): PlannerProject | undefined {
  if (bytes.length < 32) return;
  const trailer = bytes.length - extensionMagic.length;
  if (!extensionMagic.every((b, i) => bytes[trailer + i] === b)) return;
  const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength);
  const length = view.getUint32(trailer - 12, true),
    nativeLength = trailer - 12 - length;
  if (
    nativeLength < 18 ||
    bytes.length > 64 * 1024 * 1024 ||
    bytes[nativeLength - 1] !== 11
  )
    throw new Error("Extension de projet .xemf invalide ou tronquée.");
  const native = bytes.subarray(0, nativeLength),
    json = bytes.subarray(nativeLength, trailer - 12);
  if (
    crc32(native) !== view.getUint32(trailer - 8, true) ||
    crc32(json) !== view.getUint32(trailer - 4, true)
  )
    throw new Error("Le projet .xemf est corrompu (contrôle d’intégrité).");
  return JSON.parse(new TextDecoder("utf-8", { fatal: true }).decode(json));
}

export function nativeProjectRecords(
  p: PlannerProject,
  raster?: { bytes: Uint8Array; width: number; height: number },
): NativeRecord {
  const s = p.settings,
    display = s.display_options ?? {},
    vertical = s.vertical_display ?? {};
  const coord = (xy: [number, number]) => wm(toGeo(xy[0], xy[1], p.origin));
  const area = s.calculation_area_m ?? {
    x_min: -s.radius_m,
    x_max: s.radius_m,
    y_min: -s.radius_m,
    y_max: s.radius_m,
  };
  const bounds: [number, number][] = [
    [area.x_min, area.y_min],
    [area.x_max, area.y_max],
  ];
  const firstRows = p.limits[0]?.rows ?? [];
  const shared = p.limits.every(
    (l) =>
      l.rows.length === firstRows.length &&
      l.rows.every((r, i) => r.frequency === firstRows[i].frequency),
  );
  const frequencies = shared
    ? firstRows.map((r) => r.frequency)
    : [
        ...new Set([
          10,
          ...p.limits.flatMap((l) => l.rows.map((r) => r.frequency)),
        ]),
      ].sort((a, b) => a - b);
  const limits = frequencies.map((Freq, rowIndex) => ({
    Freq,
    ...Object.fromEntries(
      ["E", "A"].map((field) => [
        field,
        four((i) => {
          const limit = p.limits[i],
            rows = limit?.rows;
          let row = shared ? rows?.[rowIndex] : undefined;
          if (!shared && limit) {
            if (!completeLimit(limit)) return -999;
            const lower = rows[0].frequency > 10 ? 10 : rows[0].frequency;
            const probe =
              (Freq +
                (rowIndex ? frequencies[rowIndex - 1] : Math.min(Freq, 10))) /
              2;
            let previous = lower;
            for (let j = rows[0].frequency > 10 ? 0 : 1; j < rows.length; j++) {
              if (probe > previous && probe <= rows[j].frequency) {
                row = rows[j];
                break;
              }
              previous = rows[j].frequency;
            }
            if (!row)
              row = {
                frequency: Freq,
                value: NATIVE_FALLBACK_LIMIT,
                exponent: 0,
              };
          }
          return row ? (field === "E" ? row.value : row.exponent) : -999;
        }),
      ]),
    ),
  }));
  const hResults = p.saved_cut_results?.horizontal ?? [],
    vResults = p.saved_cut_results?.vertical ?? [];
  const many = p.limits.filter((l) => l.active).length > 1;
  const paths = (results: SavedCutResult[]) =>
    results.flatMap((r) => r.contours.segments.map((path) => path.map(coord)));
  const groups = [
    ...new Set(p.control_points.map((c) => c.group ?? "Groupe 1")),
  ];
  if (!groups.length) groups.push("Groupe 1");
  const aliases = Object.entries(
    p.report_aliases || s.report_aliases
      ? { ...p.report_aliases, ...s.report_aliases }
      : DEFAULT_REPORT_ALIASES(),
  ).map(([key, value]) => [`{${key.replace(/^\{|\}$/g, "")}}`, value]);
  const centre = wm(p.map_center),
    zoom = Math.max(1, Math.min(22, Math.round(p.map_zoom))),
    pixels = 256 * 2 ** zoom;
  const tileX = (centre.Xwm / (2 * Math.PI * 6378137) + 0.5) * pixels;
  const tileY = (0.5 - centre.Ywm / (2 * Math.PI * 6378137)) * pixels;
  const originAltitude = terrainHeight(p, 0, 0);
  const root: NativeRecord = {
    SETT: {
      GUID: crc32(
        new TextEncoder().encode(
          JSON.stringify({
            name: p.name,
            origin: p.origin,
            background_image: p.background_image,
          }),
        ),
      ),
      NameTileServer: s.map_provider,
      ZOOM: zoom,
      TileX: Math.floor(tileX / 256),
      TileY: Math.floor(tileY / 256),
      PointX: -Math.round(tileX % 256),
      PointY: -Math.round(tileY % 256),
      ShowMap: s.show_basemap !== false,
      KoefOslabl: s.reflection,
      ConsiderRelief: s.consider_terrain,
      OriginCoordWM: wm(p.origin),
      OriginCoordWGS: { Lat: p.origin[0], Lon: p.origin[1] },
      PRTOCoordWGS: {
        Lat: (s.site_coordinates ?? p.origin)[0],
        Lon: (s.site_coordinates ?? p.origin)[1],
      },
      HorAreaCoordWM:
        s.calculation_area_wm?.map(([Xwm, Ywm]) => ({ Xwm, Ywm })) ??
        bounds.map(coord),
      HorAreaCoordM: bounds.map(metres),
      MaxHorSize: s.horizontal_grid_divisions ?? [600, 200],
      HorSize: s.horizontal_grid_divisions?.[0] ?? 600,
      CombineHorPlanes: s.project_lower_cuts !== false,
      CombineHorPlanesByColor: s.merge_same_color !== false,
      LengthAttractAntennas: s.antenna_capture_distance ?? 15,
      ShowAntennas: display.sources !== false,
      ShowAntennaLabels: display.labels !== false,
      ShowAntennaAzimuth: display.azimuths !== false,
      ShowControlPoints: display.points !== false,
      ShowControlPointGroups: s.control_point_labels?.groups !== false,
      ShowControlPointHeight: s.control_point_labels?.heights !== false,
      ControlPointPrefix: s.control_point_labels?.prefix ?? "",
      ShowOrigin: s.show_origin !== false,
      ShowHorizPlaneScale: s.show_calculation_area !== false,
      ShowConturZO: display.contours !== false,
      TransparFillZO: Math.round(255 * (s.contour_opacity ?? 0)),
      LineWidthZO: s.contour_width ?? 2,
      ShowLengthZO: vertical.length !== false,
      ShowHgtBottomZO: vertical.bottom !== false,
      ShowHgtMaxZO: vertical.maximum !== false,
      ShowHgtTopZO: vertical.top === true,
      ShowMinDistToZO: vertical.distance !== false,
      ShowManualPointsZO: vertical.dimensions !== false,
      ShowKPonVert: vertical.points !== false,
      ShowPlus2m: vertical.terrain_line === true,
      LineHightAboveTerrain: s.terrain_line_height_m ?? 2,
      LineWidthVertCut: 3,
      ShowAllBuildings: display.buildings !== false,
      ShowBuildingText: s.building_labels?.name !== false,
      ShowBuildingHeight: s.building_labels?.height === true,
      ShowBuildingFoundation: s.building_labels?.foundation === true,
      BuildingLineWidth: 2,
      BuildingTextSize: 9,
      BuildingColor: colour(),
      ShowRelief: display.terrain !== false,
      ShowRoads: display.lines !== false && s.line_visibility?.roads !== false,
      ShowFences:
        display.lines !== false && s.line_visibility?.fences !== false,
      ShowText: display.lines !== false && s.line_visibility?.labels !== false,
      CircleStep: 50,
      Tabl1LengthMax: 200,
      Tabl1StepL: 5,
      Tabl1HeightMax: 50,
      Tabl1StepH: 2,
      Tabl2LengthMax: 200,
      Tabl2StepL: 5,
      Tabl2Height: 50,
      Tabl3LengthMax: 200,
      Tabl3StepL: 5,
      Tabl3Height: [2, 10, 20],
      TablRadial1LengthMax: 200,
      TablRadial1StepL: 5,
      TablRadial1Angle: 10,
      TablRadial1Height: [2, 10],
      Graph1LengthMax: 200,
      Graph1Height: 50,
      TablHoriz1Angle: 5,
      RepFileName: "",
      ALS: [aliases.map(([key]) => key), aliases.map(([, value]) => value)],
      LMT: { _items: limits, _size: limits.length, _version: limits.length },
      LimitDescription: four((i) => p.limits[i]?.name ?? ""),
      LimitColor: four((i) => colour(p.limits[i]?.color)),
      LimitAlpha24: four((i) => p.limits[i]?.use_alpha24 ?? false),
      LimitFtdc: four((i) => p.limits[i]?.use_ftdc ?? false),
      LimitFpr: four((i) => p.limits[i]?.use_fpr ?? false),
      TableGraphLimitIndex: s.selected_limit,
      VertCutLimits: four((i) => p.limits[i]?.active ?? false),
      HorizCutLimits: four((i) => p.limits[i]?.active ?? false),
      FlagLastRezManyLimits: many,
      ControlPointLimitIndex: s.selected_limit,
      ControlPointMesureFreq: s.point_measurement_frequency_mhz ?? 1000,
      UseBuildings: ["ignore", "screen", "attenuate"].indexOf(s.buildings_mode),
      BuildAttenSubband1: s.building_attenuation_db,
      BuildAttenSubband2: s.building_attenuation_high_db ?? 6,
      BuildFreqBorder: s.building_frequency_boundary_mhz ?? 400,
    },
    TX: p.sources.map((t) => ({
      Active: t.active !== false,
      Name: t.name,
      TypeOfService: t.service_type,
      F: t.frequency_mhz,
      F2: t.frequency_end_mhz ?? 0,
      P: t.power_w,
      N: t.num_tx,
      Loss: t.total_attenuation_db,
      AntType: t.antenna_model,
      Az: t.azimuth_deg,
      G: t.gain_dbi,
      H: t.height_m,
      D: t.antenna_size_m,
      Ug: t.mechanical_downtilt_deg,
      ElectUg: t.electrical_downtilt_deg,
      UgH: String(t.beam_width_h_deg),
      UgV: String(t.beam_width_v_deg),
      Location: t.location,
      Exist: t.is_existing !== false ? "Existing" : "Planned",
      Owner: t.owner,
      Sector: t.sector_id,
      DnH: Array.from(
        { length: 360 },
        (_, i) => -20 * Math.log10(patternAmplitude(t.pattern_h, i, 65)),
      ),
      DnV: Array.from(
        { length: 360 },
        (_, i) => -20 * Math.log10(patternAmplitude(t.pattern_v, i, 10)),
      ),
      Alpha24: four((i) => t.limit_factors?.[i]?.[0] ?? t.alpha24),
      Ftdc: four((i) => t.limit_factors?.[i]?.[1] ?? t.duty_cycle),
      Fpr: four((i) => t.limit_factors?.[i]?.[2] ?? t.power_reduction),
      Cable: t.feeder?.cable ?? "",
      LengthCable: t.feeder?.length_m ?? 0,
      Jumper: t.feeder?.jumper ?? "",
      LengthJumper: t.feeder?.jumper_lengths ?? "0",
      ChkLossSocket: t.feeder?.include_connectors ?? false,
      LossSocketOne: t.feeder?.connector_loss ?? 0,
      ExtraLoss: t.feeder?.other_loss ?? 0,
      Color: colour(t.color ?? "#ffffff"),
      CoordWM: wm([t.lat, t.lng]),
      CoordM: metres([t.x_m ?? 0, t.y_m ?? 0]),
    })),
    CPG: groups.map((name) => ({
      Name: name,
      Active:
        p.control_points.find((c) => (c.group ?? "Groupe 1") === name)
          ?.group_active !== false,
      CP: p.control_points
        .filter((c) => (c.group ?? "Groupe 1") === name)
        .map((c) => ({
          CoordWM: wm([c.lat, c.lng]),
          CoordM: metres([c.x_m, c.y_m]),
          Name: c.description ?? c.name,
          Location: c.location ?? "Ground",
          H: (c.heights?.length
            ? c.heights
            : [
                {
                  height_m: c.height_m,
                  attenuation_db: c.attenuation_db,
                  measured: c.measured_e_field_v_m ?? 0,
                  description: "",
                },
              ]
          ).map((h) => ({
            H: h.height_m,
            L: h.attenuation_db,
            MeasuredE: h.measured,
            Name: h.description,
          })),
        })),
    })),
    BLD: p.buildings.map((b) => ({
      Show: b.visible !== false,
      H: b.height_m,
      Fundation: b.foundation_m,
      Text: b.name,
      CoordWM: b.points.map(coord),
      CoordM: b.points.map(metres),
      UseAttenuation: b.custom_attenuation !== false,
    })),
    ROAD: p.lines
      .filter((l) => l.kind === "road")
      .map((l) => ({
        CoordWM: l.points.map(coord),
        WidthM: l.width,
        WidthBorder: l.border_width ?? 1,
      })),
    FENCE: p.lines
      .filter((l) => l.kind === "barrier")
      .map((l) => ({
        CoordWM: l.points.map(coord),
        Width: l.width,
        Type: l.fence_type ?? 0,
      })),
    TXT: p.lines
      .filter((l) => l.kind === "label")
      .map((l) => ({
        CoordWM: coord(l.points[0] ?? [0, 0]),
        Text: l.name,
        FontName: l.font_name ?? "Arial",
        Size: l.font_size ?? 10,
        Angle: l.angle_deg ?? 0,
        Point: l.show_point ?? false,
        Icon: null,
      })),
    ElLN: (p.terrain_contours ?? []).map((l) => ({
      CoordWM: l.points.map(coord),
      H: l.elevation,
      CloseLine: l.closed,
    })),
    ElPT: p.terrain_points.map((t) => ({
      CoordWM: coord([t.x, t.y]),
      H: t.elevation,
    })),
    HP: p.horizontal_cuts.map((c) => ({
      Activ: c.active,
      Height: c.height_m,
      Color: colour(c.color),
      LineType: Math.max(
        0,
        ["solid", "dash", "dot", "dashdot"].indexOf(c.line_style ?? "solid"),
      ),
      PLNwm: paths(hResults.filter((r) => r.cut_id === c.id)),
      FlagCombine: false,
    })),
    HP_PLNwm: four((i) => paths(hResults.filter((r) => r.limit_index === i))),
    VP: p.vertical_cuts.map((c) => {
      const results = vResults.filter((r) => r.cut_id === c.id),
        size = results[0] ? Math.max(1, results[0].grid.width - 1) : 0;
      const below = c.min_height_m ?? 0;
      return {
        Activ: c.active,
        Name: c.name ?? "",
        Azimuth: c.azimuth_deg,
        CoordWM: coord([c.x, c.y]),
        CoordM: metres([c.x, c.y]),
        H1: c.max_height_m,
        H2: below,
        L1: c.forward_m,
        L2: c.backward_m,
        LastH1: c.max_height_m,
        LastH2: below,
        LastL1: c.forward_m,
        LastL2: c.backward_m,
        Coord1M: metres([
          c.x - c.backward_m * Math.sin(rad(c.azimuth_deg)),
          c.y - c.backward_m * Math.cos(rad(c.azimuth_deg)),
        ]),
        Coord2M: metres([
          c.x + c.forward_m * Math.sin(rad(c.azimuth_deg)),
          c.y + c.forward_m * Math.cos(rad(c.azimuth_deg)),
        ]),
        Size: size,
        Elev0: originAltitude,
        Elev: [],
        Bld: [],
        PLNs: four((i) =>
          results
            .filter((r) => (r.limit_index ?? s.selected_limit) === i)
            .flatMap((r) =>
              r.contours.segments.map((path) =>
                path.map(([x, y]) => ({
                  X: ((x + c.backward_m) / (c.forward_m + c.backward_m)) * size,
                  Y: ((y + below) / (c.max_height_m + below)) * size,
                })),
              ),
            ),
        ),
        ManualPointZO: [],
        DimensionZO: (c.dimensions ?? []).map((d) => ({
          R: d.x,
          H: d.y1,
          Hz: d.y2,
          Text: "",
        })),
      };
    }),
    RPL: {
      BTM: null,
      RasterScale: 1,
      Length: 50,
      PointRaster: [
        { Xpix: 0, Ypix: 0 },
        { Xpix: 0, Ypix: 0 },
        { Xpix: 1, Ypix: 1 },
      ],
      PointMap: [coord([0, 0]), coord([0, 0]), coord([1, 1])],
    },
  };
  if (raster && p.background_image) {
    const bg = p.background_image;
    root.RPL = {
      BTM: { Data: raster.bytes },
      RasterScale: 1,
      UseScalingLine: true,
      Length: 50,
      PointRaster: [
        { Xpix: 0, Ypix: 0 },
        { Xpix: 0, Ypix: 0 },
        { Xpix: raster.width, Ypix: 0 },
      ],
      PointMap: [
        wm([bg.north, bg.west]),
        wm([bg.north, bg.west]),
        wm([bg.north, bg.east]),
      ],
    };
  }
  return root;
}

export function exportXemf(
  p: PlannerProject,
  raster?: { bytes: Uint8Array; width: number; height: number },
): Uint8Array {
  if (p.background_image && !raster) {
    const match = p.background_image.data_url.match(
      /^data:image\/(png|jpe?g|gif|bmp);base64,([\s\S]+)$/i,
    );
    if (!match)
      throw new Error(
        "L’image doit être convertie en PNG avant la sauvegarde .xemf.",
      );
    const bytes = Uint8Array.from(atob(match[2]), (c) => c.charCodeAt(0)),
      dimensions = validateRasterDimensions(bytes);
    if (!dimensions) throw new Error("Image du projet .xemf illisible.");
    raster = { bytes, width: dimensions[0], height: dimensions[1] };
  }
  const native = writeNativeRecords(nativeProjectRecords(p, raster));
  const json = new TextEncoder().encode(
    JSON.stringify({ ...p, format_version: 2 }),
  );
  const total = native.length + json.length + 12 + extensionMagic.length;
  if (total > 64 * 1024 * 1024)
    throw new Error("Projet .xemf trop volumineux (64 Mo maximum).");
  const bytes = new Uint8Array(total),
    view = new DataView(bytes.buffer);
  bytes.set(native);
  bytes.set(json, native.length);
  const offset = native.length + json.length;
  view.setUint32(offset, json.length, true);
  view.setUint32(offset + 4, crc32(native), true);
  view.setUint32(offset + 8, crc32(json), true);
  bytes.set(extensionMagic, offset + 12);
  return bytes;
}
