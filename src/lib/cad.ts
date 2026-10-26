import type { Contour, PlannerProject } from "../types";
import { toLocal } from "./geometry";

// Paper geometry follows UC_CAD.SubDrawAll in the recovered 7.1 application.
// Coordinates are millimetres, with the origin at the lower left of the sheet.
export const CAD_LAYERS = [
  ["frame", "Cadre du dessin", "Drawing frame"],
  ["contours", "Exposition EMF", "EMF exposure area"],
  ["buildings", "Bâtiments", "Buildings"],
  ["terrain", "Terrain", "Terrain"],
  ["lines", "Lignes et annotations", "Lines and labels"],
  ["basemap", "Fond de carte", "Base Map"],
  ["sources", "Antennes", "Antennas"],
  ["points", "Points de contrôle", "Control points"],
  ["origin", "Origine", "Origin"],
  ["area", "Zone de calcul", "Calculation area"],
] as const;
export type CadLayer = (typeof CAD_LAYERS)[number][0];
export const CAD_DRAW_ORDER: CadLayer[] = [
  "basemap",
  "lines",
  "contours",
  "terrain",
  "buildings",
  "sources",
  "points",
  "origin",
  "area",
  "frame",
];
export type CadPaper = "A4" | "A3" | "A2";
export interface CadOptions {
  scale: number;
  paper: CadPaper;
  layers?: Record<string, boolean>;
}
export const CAD_SCALES = [
  250, 500, 750, 1000, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000, 3250,
  3500, 3750, 4000, 4250, 4500, 4750, 5000, 5500, 6000, 6500, 7000, 7500, 8000,
  8500, 9000, 9500, 10000,
];
type XY = [number, number];
interface BaseEntity {
  layer: CadLayer;
  color: string;
}
export type CadEntity = BaseEntity &
  (
    | {
        kind: "path";
        points: XY[];
        width: number;
        closed?: boolean;
        dash?: number[];
        fill?: string;
      }
    | {
        kind: "circle";
        center: XY;
        radius: number;
        width: number;
        fill?: string;
      }
    | {
        kind: "text";
        position: XY;
        value: string;
        height: number;
        align?: "left" | "center" | "right";
        baseline?: "middle" | "top" | "bottom";
      }
    | {
        kind: "image";
        position: XY;
        width: number;
        height: number;
        url: string;
        opacity: number;
      }
  );
export interface CadDrawing {
  width: number;
  height: number;
  scale: number;
  paper: CadPaper;
  area: { x_min: number; x_max: number; y_min: number; y_max: number };
  entities: CadEntity[];
}
const PAPER = { A4: [210, 297], A3: [420, 297], A2: [594, 420] } as const;
const AXIS_STEPS = [
  0.01, 0.02, 0.025, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.1, 0.2, 0.25,
  0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6,
  6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65,
  70, 75, 80, 85, 90, 95, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600,
  650, 700, 750, 800, 850, 900, 950, 1000, 1500, 2000, 2500, 3000, 3500, 4000,
  4500, 5000, 5500, 6000, 6500, 7000, 7500, 8000, 8500, 9000, 9500, 10000,
];
/** Module1.RazmetkaOSI selects 8–13 intervals with the smallest unused span. */
export function cadAxisTicks(min: number, max: number): number[] {
  if (!Number.isFinite(min + max) || max <= min) return [];
  let ticks: number[] = [],
    unused = Infinity;
  for (const step of AXIS_STEPS) {
    const first = Math.ceil(min / step),
      last = Math.floor(max / step),
      n = last - first,
      rest = Math.abs(max - min - n * step);
    if (n >= 8 && n <= 13 && rest < unused) {
      unused = rest;
      ticks = Array.from({ length: n + 1 }, (_, i) =>
        Number(((first + i) * step).toFixed(8)),
      );
    }
  }
  return ticks;
}
function roundEven(value: number) {
  const floor = Math.floor(value),
    fraction = value - floor;
  return fraction === 0.5
    ? floor + (floor % 2 === 0 ? 0 : 1)
    : Math.round(value);
}
// Original project heights/azimuths are System.Single values. Printing their
// widened JavaScript doubles exposes storage noise (e.g. 1.899999976158142).
function cadNumber(value: number) {
  return Number(value.toPrecision(7)).toString();
}
/** Numeric styles 1–27 are the native CAD line types, in paper millimetres. */
export function cadDash(style?: string): number[] | undefined {
  const patterns = [
    [1, 1],
    [1, 3],
    [1, 5],
    [2, 1],
    [2, 3],
    [2, 5],
    [5, 1],
    [5, 3],
    [5, 5],
    [10, 1],
    [10, 3],
    [10, 5],
    [5, 1, 1, 1],
    [5, 3, 1, 3],
    [5, 10, 1, 10],
    [5, 1, 1, 1, 1, 1],
    [5, 3, 1, 1, 1, 3],
    [5, 5, 1, 3, 1, 5],
    [5, 5, 10, 5],
    [1, 5, 1, 10],
    [1, 5, 1, 10, 1, 10],
    [4, 5, 4, 5, 1, 5],
    [1, 5, 1, 5, 4, 5, 4, 5],
    [1, 5, 4, 5, 4, 5, 4, 5],
    [1, 5, 1, 5, 1, 5, 4, 5, 4, 5],
    [4, 5, 4, 5, 4, 5, 1, 5, 1, 5],
    [1, 5, 4, 5, 1, 5, 4, 5, 1, 5],
  ];
  if (style && /^\d+$/.test(style)) return patterns[Number(style) - 1];
  return (
    { dash: [5, 3], dot: [0.5, 2], dashdot: [5, 1, 1, 1] } as Record<
      string,
      number[]
    >
  )[style ?? ""];
}
/** Same widest horizontal interior chord used by Module1.CoordCenterSign1. */
function labelCenter(points: XY[]): XY {
  if (!points.length) return [0, 0];
  const y =
      (Math.min(...points.map((p) => p[1])) +
        Math.max(...points.map((p) => p[1]))) /
      2,
    xs: number[] = [];
  points.forEach((a, i) => {
    const b = points[(i + 1) % points.length];
    if ((a[1] >= y && b[1] < y) || (a[1] < y && b[1] >= y))
      xs.push(a[0] + ((b[0] - a[0]) * (y - a[1])) / (b[1] - a[1]));
  });
  xs.sort((a, b) => a - b);
  let x = points[0][0],
    longest = -1;
  for (let i = 0; i + 1 < xs.length; i += 2)
    if (xs[i + 1] - xs[i] > longest) {
      longest = xs[i + 1] - xs[i];
      x = (xs[i + 1] + xs[i]) / 2;
    }
  return [x, y];
}
export function buildCadDrawing(
  project: PlannerProject,
  contours: Contour[] = [],
  options?: Partial<CadOptions>,
): CadDrawing {
  const saved = project.cad_options,
    scale = options?.scale ?? saved?.scale ?? 1000,
    paper = options?.paper ?? saved?.paper ?? "A3",
    layers = options?.layers ?? saved?.layers ?? {},
    [width, height] = PAPER[paper],
    unit = 1000 / scale,
    radius = project.settings.radius_m,
    area = project.settings.calculation_area_m ?? {
      x_min: -radius,
      x_max: radius,
      y_min: -radius,
      y_max: radius,
    },
    centerX = paper === "A4" ? 112.5 : paper === "A2" ? 304.5 : 217.5,
    offsetX = roundEven(centerX - ((area.x_min + area.x_max) / 2) * unit),
    offsetY = roundEven(height / 2 - ((area.y_min + area.y_max) / 2) * unit),
    xy = (x: number, y: number): XY => [x * unit + offsetX, y * unit + offsetY],
    entities: CadEntity[] = [],
    visible = (layer: CadLayer) => layers[layer] !== false,
    display = project.settings.display_options ?? {},
    add = (entity: CadEntity) => {
      if (visible(entity.layer)) entities.push(entity);
    },
    path = (
      layer: CadLayer,
      points: XY[],
      lineWidth = 0.3,
      color = "#000000",
      closed = false,
      dash?: number[],
    ) => {
      if (points.length > 1)
        add({
          kind: "path",
          layer,
          color,
          points,
          width: lineWidth,
          closed,
          dash,
        });
    },
    text = (
      layer: CadLayer,
      position: XY,
      value: string,
      textHeight = 2.5,
      align: "left" | "center" | "right" = "left",
      baseline: "middle" | "top" | "bottom" = "bottom",
    ) => {
      if (value)
        add({
          kind: "text",
          layer,
          color: "#000000",
          position,
          value,
          height: textHeight,
          align,
          baseline,
        });
    },
    circle = (layer: CadLayer, center: XY, radius: number, fill = "#000000") =>
      add({
        kind: "circle",
        layer,
        center,
        radius,
        color: "#000000",
        width: 0.15,
        fill,
      });
  if (!(scale > 0) || !Number.isFinite(scale))
    throw new Error("Échelle CAO invalide.");
  if (project.background_image && project.settings.show_basemap) {
    const image = project.background_image,
      [left, bottom] = toLocal(image.south, image.west, project.origin),
      [right, top] = toLocal(image.north, image.east, project.origin);
    add({
      kind: "image",
      layer: "basemap",
      color: "#ffffff",
      position: xy(left, bottom),
      width: (right - left) * unit,
      height: (top - bottom) * unit,
      url: image.data_url,
      opacity: image.opacity,
    });
  }
  if (display.lines !== false)
    for (const line of project.lines) {
      const lineVisibility = project.settings.line_visibility;
      if (
        (line.kind === "road" && lineVisibility?.roads === false) ||
        (line.kind === "barrier" && lineVisibility?.fences === false) ||
        (line.kind === "label" && lineVisibility?.labels === false)
      )
        continue;
      const pts = line.points.map(([x, y]) => xy(x, y));
      if (line.kind === "label") {
        if (pts[0]) {
          text("lines", [pts[0][0] + 1, pts[0][1]], line.name);
          if (line.icon) circle("lines", pts[0], 0.25);
        }
      } else if (line.kind === "road") {
        path("lines", pts, Math.max(0.3, line.width * unit));
        path("lines", pts, Math.max(0.05, line.width * unit - 0.6), "#fefefe");
      } else
        path(
          "lines",
          pts,
          0.3,
          "#000000",
          false,
          cadDash((line as typeof line & { line_style?: string }).line_style),
        );
    }
  for (const c of contours)
    for (const points of c.segments)
      path(
        "contours",
        points.map(([x, y]) => xy(x, y)),
        0.5,
        c.color,
        false,
        cadDash(c.line_style),
      );
  if (display.terrain !== false) {
    for (const c of project.terrain_contours ?? []) {
      const pts = c.points.map(([x, y]) => xy(x, y));
      path("terrain", pts, 0.3, "#a52a2a", c.closed);
      if (pts[0])
        text("terrain", [pts[0][0] + 1, pts[0][1]], String(c.elevation));
    }
    for (const p of project.terrain_points) {
      const position = xy(p.x, p.y);
      circle("terrain", position, 0.25);
      text("terrain", [position[0] + 1, position[1]], String(p.elevation));
    }
  }
  for (const b of project.buildings) {
    path(
      "buildings",
      b.points.map(([x, y]) => xy(x, y)),
      0.3,
      "#000000",
      true,
    );
    {
      const labels = project.settings.building_labels ?? {
          name: true,
          height: true,
          foundation: false,
        },
        lines = [
          labels.name ? b.name : "",
          labels.height ? `${cadNumber(b.height_m)}m` : "",
          labels.foundation ? `${cadNumber(b.foundation_m)}m` : "",
        ].filter(Boolean),
        [cx, cy] = labelCenter(b.points),
        [x, y] = xy(cx, cy + (lines.length - 1) * 1.5);
      lines.forEach((value, i) =>
        text("buildings", [x, y - i * 3], value, 2.5, "center", "middle"),
      );
    }
  }
  if (display.sources !== false)
    project.sources.forEach((source, i) => {
      if (source.active === false) return;
      const [x, y] = xy(source.x_m ?? 0, source.y_m ?? 0),
        az = Math.max(0, source.azimuth_deg),
        r = (az * Math.PI) / 180,
        tip: XY = [x + 80 * Math.sin(r), y + 80 * Math.cos(r)];
      {
        path(
          "sources",
          [
            [x, y],
            tip,
            [
              tip[0] + 2 * Math.sin(r + (173 * Math.PI) / 180),
              tip[1] + 2 * Math.cos(r + (173 * Math.PI) / 180),
            ],
            [
              tip[0] + 2 * Math.sin(r + (187 * Math.PI) / 180),
              tip[1] + 2 * Math.cos(r + (187 * Math.PI) / 180),
            ],
            tip,
          ],
          0.25,
        );
        text(
          "sources",
          [x + 83 * Math.sin(r), y + 83 * Math.cos(r)],
          `№${i + 1}(Az=${cadNumber(az)}°)`,
          2.5,
          "center",
          "middle",
        );
      }
      circle("sources", [x, y], 1, source.color ?? "#ffffff");
    });
  if (display.points !== false)
    project.control_points.forEach((point) => {
      const [x, y] = xy(point.x_m, point.y_m);
      circle("points", [x, y], 1, "#323232");
      {
        const labels = project.settings.control_point_labels ?? {
            prefix: "",
            groups: true,
            heights: true,
          },
          name = labels.groups
            ? point.name
            : point.name.split(".").slice(-1)[0],
          heights = (
            point.heights?.map((h) => h.height_m) ?? [point.height_m]
          ).map(cadNumber);
        text(
          "points",
          [x + 1, y],
          `${labels.prefix}${name}${labels.heights ? `(${heights.join(";")}m)` : ""}`,
        );
      }
    });
  const [ox, oy] = xy(0, 0);
  path(
    "origin",
    [
      [ox - 3, oy],
      [ox + 3, oy],
    ],
    0.5,
  );
  path(
    "origin",
    [
      [ox, oy - 3],
      [ox, oy + 3],
    ],
    0.5,
  );
  const [left, bottom] = xy(area.x_min, area.y_min),
    [right, top] = xy(area.x_max, area.y_max);
  path(
    "area",
    [
      [left, bottom],
      [right, bottom],
      [right, top],
      [left, top],
    ],
    0.7,
    "#000000",
    true,
  );
  for (const tick of cadAxisTicks(area.x_min, area.x_max)) {
    const x = xy(tick, 0)[0];
    path(
      "area",
      [
        [x, bottom],
        [x, top],
      ],
      0.5,
      "#000000",
      false,
      [1, 1],
    );
    text("area", [x, bottom - 2], String(tick), 3, "center", "top");
  }
  text("area", [right + 2, bottom], "X, m", 3);
  for (const tick of cadAxisTicks(area.y_min, area.y_max)) {
    const y = xy(0, tick)[1];
    path(
      "area",
      [
        [left, y],
        [right, y],
      ],
      0.5,
      "#000000",
      false,
      [1, 1],
    );
    text("area", [left - 2, y], String(tick), 3, "right", "middle");
  }
  text("area", [left, top + 2], "Y, m", 3);
  if (paper !== "A4") {
    const titleLeft = width - 190;
    path(
      "frame",
      [
        [titleLeft, 5],
        [titleLeft, 60],
        [width - 5, 60],
      ],
      1,
    );
    text("frame", [width - 7, 61], `M1:${scale}`, 3, "right");
  } else text("frame", [203, 6], `M1:${scale}`, 3, "right");
  path(
    "frame",
    [
      [20, 5],
      [width - 5, 5],
      [width - 5, height - 5],
      [20, height - 5],
    ],
    1,
    "#000000",
    true,
  );
  path(
    "frame",
    [
      [0, 0],
      [width, 0],
      [width, height],
      [0, height],
    ],
    0.1,
    "#000000",
    true,
  );
  return { width, height, scale, paper, area, entities };
}

/** Exports the same paper entities as the preview; raster images need SVG/PDF. */
export function cadDrawingDxf(drawing: CadDrawing): string {
  let nextHandle = 1;
  const pairs: (number | string)[] = [],
    add = (...values: (number | string)[]) => {
      for (let i = 0; i < values.length; i += 2) {
        pairs.push(values[i], values[i + 1]);
        if (
          values[i] === 0 &&
          !["SECTION", "ENDSEC", "ENDTAB", "EOF"].includes(
            String(values[i + 1]),
          )
        )
          pairs.push(5, (nextHandle++).toString(16).toUpperCase());
      }
    },
    escape = (value: string) =>
      value
        .replace(/[\r\n]/g, " ")
        .replace(
          /[^\x20-\x7e]/g,
          (c) =>
            `\\U+${c.charCodeAt(0).toString(16).toUpperCase().padStart(4, "0")}`,
        ),
    layerName = (layer: CadLayer) => CAD_LAYERS.find((l) => l[0] === layer)![2],
    dashes = new Map<string, string>();
  for (const e of drawing.entities)
    if (e.kind === "path" && e.dash) {
      const key = e.dash.join(",");
      if (!dashes.has(key)) dashes.set(key, `EMF_DASH_${dashes.size + 1}`);
    }
  add(
    0,
    "SECTION",
    2,
    "HEADER",
    9,
    "$ACADVER",
    1,
    "AC1015",
    9,
    "$INSUNITS",
    70,
    4,
    9,
    "$MEASUREMENT",
    70,
    1,
    9,
    "$EXTMIN",
    10,
    0,
    20,
    0,
    30,
    0,
    9,
    "$EXTMAX",
    10,
    drawing.width,
    20,
    drawing.height,
    30,
    0,
    0,
    "ENDSEC",
    0,
    "SECTION",
    2,
    "TABLES",
  );
  add(
    0,
    "TABLE",
    2,
    "LTYPE",
    100,
    "AcDbSymbolTable",
    70,
    dashes.size + 1,
    0,
    "LTYPE",
    100, "AcDbSymbolTableRecord", 100, "AcDbLinetypeTableRecord",
    2,
    "CONTINUOUS",
    70,
    0,
    3,
    "Solid line",
    72,
    65,
    73,
    0,
    40,
    0,
  );
  for (const [key, name] of dashes) {
    const pattern = key.split(",").map(Number);
    add(
      0,
      "LTYPE",
      100, "AcDbSymbolTableRecord", 100, "AcDbLinetypeTableRecord",
      2,
      name,
      70,
      0,
      3,
      "EMF line style",
      72,
      65,
      73,
      pattern.length,
      40,
      pattern.reduce((sum, n) => sum + n, 0),
    );
    pattern.forEach((n, i) => add(49, i % 2 === 0 ? n : -n, 74, 0));
  }
  add(
    0,
    "ENDTAB",
    0,
    "TABLE",
    2,
    "LAYER",
    100,
    "AcDbSymbolTable",
    70,
    CAD_LAYERS.length,
  );
  CAD_LAYERS.forEach(([, , name]) =>
    add(0, "LAYER", 100, "AcDbSymbolTableRecord", 100, "AcDbLayerTableRecord", 2, name, 70, 0, 62, 7, 6, "CONTINUOUS"),
  );
  add(
    0,
    "ENDTAB",
    0,
    "TABLE",
    2,
    "STYLE",
    100,
    "AcDbSymbolTable",
    70,
    1,
    0,
    "STYLE",
    100, "AcDbSymbolTableRecord", 100, "AcDbTextStyleTableRecord",
    2,
    "STANDARD",
    70,
    0,
    40,
    0,
    41,
    1,
    50,
    0,
    71,
    0,
    42,
    2.5,
    3,
    "Arial",
    4,
    "",
    0,
    "ENDTAB",
  );
  // Explicit model/paper space records are required by DWG encoders and CAD readers.
  const blockTable = (nextHandle++).toString(16).toUpperCase();
  const modelSpace = (nextHandle++).toString(16).toUpperCase();
  const paperSpace = (nextHandle++).toString(16).toUpperCase();
  pairs.push(0, "TABLE", 5, blockTable, 330, "0", 100, "AcDbSymbolTable", 2, "BLOCK_RECORD", 70, 2);
  for (const [handle, name] of [[modelSpace, "*Model_Space"], [paperSpace, "*Paper_Space"]])
    pairs.push(0, "BLOCK_RECORD", 5, handle, 330, blockTable, 100, "AcDbSymbolTableRecord", 100, "AcDbBlockTableRecord", 2, name, 70, 0);
  add(0, "ENDTAB",
    0,
    "ENDSEC",
    0, "SECTION", 2, "BLOCKS",
  );
  for (const [handle, name] of [[modelSpace, "*Model_Space"], [paperSpace, "*Paper_Space"]]) {
    add(0, "BLOCK", 330, handle, 100, "AcDbEntity", 8, "0", 100, "AcDbBlockBegin", 2, name, 70, 0, 10, 0, 20, 0, 30, 0, 3, name, 1, "");
    add(0, "ENDBLK", 330, handle, 100, "AcDbEntity", 8, "0", 100, "AcDbBlockEnd");
  }
  add(0, "ENDSEC",
    0,
    "SECTION",
    2,
    "ENTITIES",
  );
  const properties = (e: CadEntity) => {
    add(
      330, modelSpace,
      100,
      "AcDbEntity",
      8,
      layerName(e.layer),
      420,
      parseInt(e.color.replace("#", ""), 16) || 0,
    );
  };
  for (const e of drawing.entities) {
    if (e.kind === "image") continue;
    if (e.kind === "path") {
      add(0, "LWPOLYLINE");
      properties(e);
      if (e.dash) add(6, dashes.get(e.dash.join(","))!);
      add(
        100,
        "AcDbPolyline",
        90,
        e.points.length,
        70,
        e.closed ? 1 : 0,
        43,
        e.width,
      );
      e.points.forEach(([x, y]) => add(10, x, 20, y));
    } else if (e.kind === "circle") {
      if (e.fill && e.fill !== "none") {
        add(0, "HATCH");
        properties({ ...e, color: e.fill });
        add(
          100,
          "AcDbHatch",
          10,
          0,
          20,
          0,
          30,
          0,
          210,
          0,
          220,
          0,
          230,
          1,
          2,
          "SOLID",
          70,
          1,
          71,
          0,
          91,
          1,
          92,
          1,
          93,
          1,
          72,
          2,
          10,
          e.center[0],
          20,
          e.center[1],
          40,
          e.radius,
          50,
          0,
          51,
          360,
          73,
          1,
          97,
          0,
          75,
          0,
          76,
          1,
          98,
          0,
        );
      }
      add(0, "CIRCLE");
      properties(e);
      add(
        100,
        "AcDbCircle",
        10,
        e.center[0],
        20,
        e.center[1],
        30,
        0,
        40,
        e.radius,
      );
    } else {
      add(0, "TEXT");
      properties(e);
      add(
        100,
        "AcDbText",
        10,
        e.position[0],
        20,
        e.position[1],
        30,
        0,
        40,
        e.height,
        1,
        escape(e.value),
        7,
        "STANDARD",
        72,
        e.align === "center" ? 1 : e.align === "right" ? 2 : 0,
        11,
        e.position[0],
        21,
        e.position[1],
        31,
        0,
        100,
        "AcDbText",
        73,
        e.baseline === "middle" ? 2 : e.baseline === "top" ? 3 : 1,
      );
    }
  }
  add(0, "ENDSEC", 0, "EOF");
  return pairs.join("\r\n") + "\r\n";
}
