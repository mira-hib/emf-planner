import test from "node:test";
import assert from "node:assert/strict";
import {
  buildCadDrawing,
  cadAxisTicks,
  cadDrawingDxf,
  CAD_LAYERS,
} from "../src/lib/cad.ts";
import { importDxf, projectDxf } from "../src/lib/files.ts";
import { cutNumericSummary } from "../src/lib/cut-report.ts";
import { newProject, newPoint, newSource } from "../src/lib/project.ts";

test("native CAD paper dimensions, margins, title box and 30 scale choices", () => {
  const p = newProject();
  for (const [paper, width, height] of [
    ["A4", 210, 297],
    ["A3", 420, 297],
    ["A2", 594, 420],
  ] as const) {
    const drawing = buildCadDrawing(p, [], { paper, scale: 1000 });
    assert.equal(drawing.width, width);
    assert.equal(drawing.height, height);
    const frame = drawing.entities.find(
      (e) =>
        e.kind === "path" && e.layer === "frame" && e.width === 1 && e.closed,
    );
    assert.ok(frame?.kind === "path");
    assert.deepEqual(frame.points, [
      [20, 5],
      [width - 5, 5],
      [width - 5, height - 5],
      [20, height - 5],
    ]);
    const title = drawing.entities.find(
      (e) => e.kind === "path" && e.layer === "frame" && !e.closed,
    );
    if (paper === "A4") assert.equal(title, undefined);
    else {
      assert.ok(title?.kind === "path");
      assert.deepEqual(title.points, [
        [width - 190, 5],
        [width - 190, 60],
        [width - 5, 60],
      ]);
    }
  }
});
test("CAD centers on the actual rectangular calculation area and uses native axis ticks", () => {
  const p = newProject();
  p.settings.calculation_area_m = {
    x_min: 10,
    x_max: 110,
    y_min: 20,
    y_max: 80,
  };
  const drawing = buildCadDrawing(p),
    area = drawing.entities.find(
      (e) => e.layer === "area" && e.kind === "path" && e.closed,
    );
  assert.ok(area?.kind === "path");
  // UC_CAD rounds the two paper offsets to even whole millimetres.
  assert.deepEqual(area.points, [
    [168, 118],
    [268, 118],
    [268, 178],
    [168, 178],
  ]);
  assert.deepEqual(
    cadAxisTicks(10, 110),
    [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110],
  );
  assert.deepEqual(cadAxisTicks(0, 0), []);
  const half = buildCadDrawing(p, [], { scale: 2000 }),
    halfArea = half.entities.find(
      (e) => e.layer === "area" && e.kind === "path" && e.closed,
    );
  assert.ok(halfArea?.kind === "path");
  assert.equal(halfArea.points[1][0] - halfArea.points[0][0], 50);
});
test("CAD draws azimuth arrows in paper units and includes annotations, terrain, point heights and building text", () => {
  const p = newProject(),
    s = newSource(p.origin, "Test");
  s.azimuth_deg = 90;
  p.sources = [s, { ...s, id: "inactive", active: false }];
  p.control_points = [
    {
      ...newPoint(p.origin, "1.1"),
      heights: [
        {
          id: "h",
          height_m: 7,
          attenuation_db: 0,
          measured: 0,
          description: "",
        },
      ],
    },
  ];
  p.buildings = [
    {
      id: "b",
      name: "Bâtiment 1",
      points: [
        [0, 0],
        [20, 0],
        [20, 20],
        [0, 20],
      ],
      height_m: 12,
      foundation_m: 3,
      active: true,
      attenuation_db: 20,
    },
  ];
  p.lines = [
    {
      id: "label",
      name: "Via Emilia",
      kind: "label",
      points: [[10, 20]],
      width: 1,
      color: "#000000",
    },
  ];
  p.terrain_contours = [
    {
      id: "t",
      name: "Courbe",
      elevation: 45,
      closed: true,
      points: [
        [0, 0],
        [10, 0],
        [0, 10],
      ],
    },
  ];
  const drawing = buildCadDrawing(p),
    arrow = drawing.entities.find(
      (e) => e.layer === "sources" && e.kind === "path",
    );
  assert.ok(arrow?.kind === "path");
  assert.equal(arrow.points[1][0] - arrow.points[0][0], 80);
  assert.equal(
    drawing.entities.filter((e) => e.layer === "sources" && e.kind === "circle")
      .length,
    1,
  );
  const strings = drawing.entities
    .filter((e) => e.kind === "text")
    .map((e) => e.value);
  for (const value of [
    "№1(Az=90°)",
    "1.1(7m)",
    "Bâtiment 1",
    "12m",
    "Via Emilia",
    "45",
  ])
    assert.ok(strings.includes(value), value);
  p.settings.building_labels = { name: false, height: false, foundation: true };
  const labels = buildCadDrawing(p).entities.filter(
    (e) => e.kind === "text" && e.layer === "buildings",
  );
  assert.equal(labels.length, 1);
  assert.ok(labels[0].kind === "text");
  assert.equal(labels[0].value, "3m");
});
test("all ten native CAD layers can be hidden independently and saved paper settings apply", () => {
  const p = newProject();
  p.cad_options = {
    paper: "A4",
    scale: 2000,
    layers: Object.fromEntries(CAD_LAYERS.map(([key]) => [key, false])),
  };
  assert.equal(buildCadDrawing(p).entities.length, 0);
  assert.equal(buildCadDrawing(p).paper, "A4");
  assert.equal(buildCadDrawing(p).scale, 2000);
});
test("paper DXF contains the same scaled vertices, layer table, millimetres, text and native line styles", () => {
  const p = newProject();
  const drawing = buildCadDrawing(p, [
    {
      level: 1,
      color: "#ff0000",
      height_m: 20,
      line_style: "13",
      segments: [
        [
          [0, 0],
          [10, 0],
        ],
      ],
    },
  ]);
  const dxf = cadDrawingDxf(drawing);
  assert.match(dxf, /\$INSUNITS\r\n70\r\n4\r\n/);
  for (const [, , layer] of CAD_LAYERS)
    assert.ok(dxf.includes(`2\r\n${layer}\r\n`));
  assert.match(dxf, /100\r\nAcDbPolyline/);
  assert.match(dxf, /10\r\n218\r\n20\r\n148\r\n10\r\n228\r\n20\r\n148/);
  assert.match(dxf, /EMF_DASH_1/);
  assert.match(dxf, /M1:1000/);
  assert.equal(
    projectDxf(p, [], { paper: { paper: "A3", scale: 1000 } }),
    cadDrawingDxf(buildCadDrawing(p)),
  );
  // The public model-space export remains in metres for existing consumers.
  assert.match(projectDxf(p), /\$INSUNITS\r\n70\r\n6/);
});
test("DXF import rejects genuine DWG and binary DXF instead of interpreting binary as text", () => {
  assert.throws(() => importDxf(newProject(), "AC1032\0\0binary"), /DWG/);
  assert.throws(
    () => importDxf(newProject(), "AutoCAD Binary DXF\r\n\x1a\0"),
    /binaire/,
  );
  assert.throws(() => importDxf(newProject(), "not a DXF"), /section ENTITIES/);
});
test("cut reports leave contour-only numeric cells empty and accept large sampled grids", () => {
  assert.deepEqual(cutNumericSummary([{ fields: [], values: [] }]), {
    maxField: null,
    maxRatio: null,
    samples: null,
  });
  assert.deepEqual(cutNumericSummary([]), {
    maxField: null,
    maxRatio: null,
    samples: null,
  });
  assert.deepEqual(cutNumericSummary([{ fields: [0, 0], values: [0, 0] }]), {
    maxField: 0,
    maxRatio: 0,
    samples: 2,
  });
  const fields = Array.from({ length: 250_000 }, (_, i) => i / 1000),
    values = Array.from({ length: 250_000 }, (_, i) => i / 100000);
  assert.deepEqual(cutNumericSummary([{ fields, values }]), {
    maxField: 249.999,
    maxRatio: 2.49999,
    samples: 250_000,
  });
});
