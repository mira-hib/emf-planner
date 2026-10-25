import JSZip from "jszip";
import type { StoredAntenna } from "../types";
import { decodeMsi, parseMsi } from "./antenna";
import { uid } from "./project";
import { validateArchive } from "./archive-validation";

export interface AntennaLibraryFile { name: string; bytes: () => Promise<Uint8Array> }
export async function readAntennaLibrary(files: AntennaLibraryFile[], progress?: (done: number, total: number) => void) {
  const candidates = files.filter((f) => /\.msi$/i.test(f.name));
  if (candidates.length > 10000) throw new Error("Le dossier dépasse 10 000 fichiers MSI.");
  const patterns: (StoredAntenna & { file_name: string })[] = [], rejected: string[] = [];
  let totalBytes = 0;
  for (let i = 0; i < candidates.length; i++) {
    const file = candidates[i];
    try {
      const bytes = await file.bytes(); totalBytes += bytes.length;
      if (bytes.length > 20 * 1024 * 1024 || totalBytes > 256 * 1024 * 1024) throw new Error("Bibliothèque trop volumineuse.");
      patterns.push({ ...parseMsi(decodeMsi(bytes)), id: uid(), file_name: file.name.split(/[\\/]/).pop()! });
    } catch (error) { rejected.push(`${file.name} : ${(error as Error).message}`); }
    if (i % 50 === 0 || i === candidates.length - 1) { progress?.(i + 1, candidates.length); await new Promise((resolve) => setTimeout(resolve, 0)); }
  }
  return { patterns, rejected, total: candidates.length };
}
export async function readAntennaZip(bytes: Uint8Array, progress?: (done: number, total: number) => void) {
  validateArchive(bytes, 256 * 1024 * 1024);
  const zip = await JSZip.loadAsync(bytes);
  return readAntennaLibrary(Object.values(zip.files).filter((entry) => !entry.dir).map((entry) => ({ name: entry.name, bytes: () => entry.async("uint8array") })), progress);
}
export async function chooseAntennaDirectory(): Promise<AntennaLibraryFile[] | null> {
  const { isTauri } = await import("@tauri-apps/api/core");
  if (isTauri()) {
    const { open } = await import("@tauri-apps/plugin-dialog");
    const { readDir, readFile } = await import("@tauri-apps/plugin-fs");
    const path = await open({ directory: true });
    if (typeof path !== "string") return null;
    const files: AntennaLibraryFile[] = [];
    async function visit(directory: string) {
      for (const entry of await readDir(directory)) {
        const child = `${directory}/${entry.name}`;
        if (entry.isDirectory) await visit(child);
        else if (entry.isFile && /\.msi$/i.test(entry.name)) files.push({ name: child, bytes: () => readFile(child) });
        if (files.length > 10000) throw new Error("Le dossier dépasse 10 000 fichiers MSI.");
      }
    }
    await visit(path); return files;
  }
  return new Promise((resolve) => {
    const input = document.createElement("input"); input.type = "file"; input.multiple = true; input.setAttribute("webkitdirectory", "");
    input.oncancel = () => resolve(null);
    input.onchange = () => resolve(Array.from(input.files ?? []).map((file) => ({ name: file.webkitRelativePath || file.name, bytes: async () => new Uint8Array(await file.arrayBuffer()) })));
    input.click();
  });
}
