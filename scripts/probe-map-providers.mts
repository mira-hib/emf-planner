// Deliberately requests one tile per public endpoint; no packaged third-party credentials.
import { writeFile } from "node:fs/promises";
import { TILES, tileUrl } from "../src/lib/map-providers.ts";
const candidates = Object.entries(TILES).filter(([, p]) => p.url)
  .map(([name, p]) => ({ name, url: tileUrl(p.url, 4, 2, 3, () => 0.5) }));
candidates.push(
  { name: "Google Satellite packaged v930 (retired)", url: "https://khms2.googleapis.com/kh?v=930&src=app&x=4&y=2&z=3&s=" },
  { name: "Google Map unversioned candidate", url: "https://mt2.google.com/vt/lyrs=m&hl=en&x=4&y=2&z=3&s=Galile" },
  { name: "Google Satellite unversioned candidate", url: "https://mt2.google.com/vt/lyrs=s&x=4&y=2&z=3" },
  { name: "Bing host candidate", url: "https://t0.tiles.virtualearth.net/tiles/a120.jpeg?g=0" },
);
const checks = await Promise.all(candidates.map(async ({ name, url }) => {
  try {
    const response = await fetch(url, { signal: AbortSignal.timeout(20000) });
    const bytes = new Uint8Array(await response.arrayBuffer());
    return { name, url, status: response.status, type: response.headers.get("content-type"), bytes: bytes.length,
      signature: Array.from(bytes.slice(0, 8)).map((b) => b.toString(16).padStart(2, "0")).join(""),
      cors: response.headers.get("access-control-allow-origin") };
  } catch (error) { return { name, url, error: String(error), cause: String((error as { cause?: unknown }).cause ?? "") }; }
}));
const result = { checked_at: new Date().toISOString(), scope: "One zoom-3 tile per HTTPS endpoint from Node fetch. Not a browser rendering or coverage guarantee.", checks };
await writeFile("reference/map-provider-network-check.json", JSON.stringify(result, null, 2) + "\n");
console.log(JSON.stringify(result, null, 2));
