import { mkdirSync, readFileSync, writeFileSync } from "node:fs";
import { exportXemf } from "../src/lib/export-xemf";
import { normalizeProject, newSource } from "../src/lib/project";
import { toGeo } from "../src/lib/geometry";
import { readProject } from "../src/lib/files";
import { readNrbf } from "../src/lib/nrbf";

const out = process.argv[2] ?? ".tools/xemf-validation";
mkdirSync(out, { recursive: true });
const p = normalizeProject(
  JSON.parse(readFileSync("public/examples/demonstration.emfp", "utf8")),
);
p.name = "Interop Unicode";
p.sources = [
  {
    ...newSource(p.origin, "Source — العربية 漢字"),
    power_w: 1000,
    gain_dbi: 0,
    height_m: 20,
    color: "#123456",
  },
];
p.report_aliases = { "{Client}": "A&B <é> 😀", "{Projet}": "Test natif" };
p.settings.buildings_mode = "screen";
p.lines.push({
  id: "label",
  name: "Étiquette",
  kind: "label",
  points: [[1, 2]],
  width: 1,
  color: "#222222",
  font_name: "Arial",
  font_size: 12,
});
p.background_image = {
  data_url:
    "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAIAAAABCAYAAAD0In+KAAAAEUlEQVR4nGP4z8Dwn4GBgQEADfoB/wtVMEUAAAAASUVORK5CYII=",
  west: p.origin[1],
  north: p.origin[0],
  east: toGeo(100, 0, p.origin)[1],
  south: toGeo(0, -50, p.origin)[0],
  opacity: 0.7,
};
// Real 2 x 1 PNG, produced independently with .NET below when validating Windows.
const pngPath = process.argv[3];
const png = pngPath
  ? new Uint8Array(readFileSync(pngPath))
  : Uint8Array.from(
      Buffer.from(p.background_image.data_url.split(",")[1], "base64"),
    );
p.background_image.data_url = `data:image/png;base64,${Buffer.from(png).toString("base64")}`;
const bytes = exportXemf(p, { bytes: png, width: 2, height: 1 });
writeFileSync(`${out}/generated.xemf`, bytes);
writeFileSync(`${out}/project.json`, JSON.stringify(p, null, 2));
const { root, deref } = readNrbf(bytes.buffer);
writeFileSync(
  `${out}/native-root.json`,
  JSON.stringify(root, (_, v) => deref(v), 2),
);
const roundtrip = readProject("generated.xemf", bytes);
if (JSON.stringify(roundtrip.sources) !== JSON.stringify(p.sources))
  throw new Error("Source precision lost");
console.log(
  JSON.stringify({
    bytes: bytes.length,
    root: root.__type,
    sources: p.sources.length,
    roundtrip: "passed",
  }),
);
