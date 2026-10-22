import type { RadiatingSource } from "../types";
import { uid } from "./project";

export const SOURCE_BUFFER_FORMAT = "emf-planner/source-rows";
export const SOURCE_BUFFER_VERSION = 1;
const MAX_BUFFER_LENGTH = 20_000_000;
const MAX_BUFFER_ROWS = 10_000;
type IdFactory = () => string;
export type SourceRowMove = "up" | "down" | "top" | "bottom";
export interface SourceRowsResult {
  sources: RadiatingSource[];
  selectedIds: string[];
  activeId: string;
  changed: boolean;
}

const STRING_FIELDS = [
  "id",
  "name",
  "service_type",
  "antenna_model",
  "location",
  "owner",
  "sector_id",
] as const;
const NUMBER_FIELDS = [
  "frequency_mhz",
  "power_w",
  "gain_dbi",
  "lat",
  "lng",
  "height_m",
  "azimuth_deg",
  "tilt_deg",
  "duty_cycle",
  "power_reduction",
  "alpha24",
  "ground_reflection_k",
  "num_tx",
  "total_attenuation_db",
  "antenna_size_m",
  "mechanical_downtilt_deg",
  "electrical_downtilt_deg",
  "beam_width_h_deg",
  "beam_width_v_deg",
] as const;
const OPTIONAL_FIELDS = [
  "active",
  "color",
  "is_existing",
  "pattern_h",
  "pattern_v",
  "limit_factors",
  "x_m",
  "y_m",
  "frequency_end_mhz",
  "feeder",
] as const;
const FEEDER_STRINGS = ["cable", "jumper", "jumper_lengths"] as const;
const FEEDER_NUMBERS = [
  "length_m",
  "connectors",
  "connector_loss",
  "other_loss",
] as const;

function invalid(reason: string): never {
  throw new Error(`Tampon de sources invalide : ${reason}.`);
}
function record(
  value: unknown,
  allowed: readonly string[],
  label: string,
): Record<string, unknown> {
  if (!value || typeof value !== "object" || Array.isArray(value))
    invalid(label);
  const prototype = Object.getPrototypeOf(value);
  if (prototype !== Object.prototype && prototype !== null) invalid(label);
  const descriptors = Object.getOwnPropertyDescriptors(value);
  for (const [key, descriptor] of Object.entries(descriptors)) {
    if (!allowed.includes(key) || descriptor.get || descriptor.set)
      invalid(`${label}, propriété ${key}`);
  }
  return value as Record<string, unknown>;
}
function string(value: unknown, label: string): string {
  if (typeof value !== "string") invalid(label);
  return value;
}
function number(value: unknown, label: string): number {
  if (typeof value !== "number" || !Number.isFinite(value)) invalid(label);
  return value;
}
function boolean(value: unknown, label: string): boolean {
  if (typeof value !== "boolean") invalid(label);
  return value;
}
function pattern(value: unknown, label: string): number[] {
  if (!Array.isArray(value) || (value.length !== 0 && value.length !== 360))
    invalid(label);
  return value.map((entry, i) => number(entry, `${label}[${i}]`));
}
function validateSource(value: unknown): RadiatingSource {
  const row = record(
      value,
      [...STRING_FIELDS, ...NUMBER_FIELDS, ...OPTIONAL_FIELDS],
      "source",
    ),
    result: Record<string, unknown> = {};
  for (const key of STRING_FIELDS) result[key] = string(row[key], key);
  if (!result.id) invalid("identifiant vide");
  for (const key of NUMBER_FIELDS) result[key] = number(row[key], key);
  for (const key of ["active", "is_existing"] as const)
    if (row[key] !== undefined) result[key] = boolean(row[key], key);
  if (row.color !== undefined) result.color = string(row.color, "couleur");
  for (const key of ["x_m", "y_m", "frequency_end_mhz"] as const)
    if (row[key] !== undefined) result[key] = number(row[key], key);
  for (const key of ["pattern_h", "pattern_v"] as const)
    if (row[key] !== undefined) result[key] = pattern(row[key], key);
  if (row.limit_factors !== undefined) {
    if (!Array.isArray(row.limit_factors) || row.limit_factors.length > 4)
      invalid("facteurs de limites");
    result.limit_factors = row.limit_factors.map((factors, i) => {
      if (!Array.isArray(factors) || factors.length !== 3)
        invalid(`facteurs de limite ${i + 1}`);
      return factors.map((factor, j) => number(factor, `facteurs[${i}][${j}]`));
    });
  }
  if (row.feeder !== undefined) {
    const input = record(
        row.feeder,
        [
          ...FEEDER_STRINGS,
          ...FEEDER_NUMBERS,
          "automatic_connectors",
          "include_connectors",
        ],
        "câble",
      ),
      feeder: Record<string, unknown> = {};
    for (const key of FEEDER_STRINGS)
      feeder[key] = string(input[key], `câble.${key}`);
    for (const key of FEEDER_NUMBERS)
      feeder[key] = number(input[key], `câble.${key}`);
    feeder.automatic_connectors = boolean(
      input.automatic_connectors,
      "câble.automatic_connectors",
    );
    if (input.include_connectors !== undefined)
      feeder.include_connectors = boolean(
        input.include_connectors,
        "câble.include_connectors",
      );
    result.feeder = feeder;
  }
  return result as unknown as RadiatingSource;
}

/** Selected real rows, in table order, excluding stale IDs and duplicate selection entries. */
export function sourceSelection(
  sources: readonly RadiatingSource[],
  selectedIds: readonly string[],
): RadiatingSource[] {
  const selected = new Set(selectedIds);
  return sources.filter((source) => selected.has(source.id));
}
function unchanged(
  sources: readonly RadiatingSource[],
  selectedIds: readonly string[],
): SourceRowsResult {
  const selection = sourceSelection(sources, selectedIds);
  return {
    sources: [...sources],
    selectedIds: selection.map((source) => source.id),
    activeId: selection[0]?.id ?? "",
    changed: false,
  };
}
function cloneRows(
  rows: readonly RadiatingSource[],
  existing: readonly RadiatingSource[],
  createId: IdFactory,
): RadiatingSource[] {
  const occupied = new Set([...existing, ...rows].map((source) => source.id));
  return rows.map((source) => {
    const copy = validateSource(source);
    for (let attempt = 0; attempt < 100; attempt++) {
      const id = createId();
      if (typeof id !== "string" || !id)
        throw new Error("L’identifiant de source généré est invalide.");
      if (!occupied.has(id)) {
        occupied.add(id);
        return { ...copy, id };
      }
    }
    throw new Error("Impossible de créer un identifiant de source unique.");
  });
}

/** UC_PRTO.cmdCopySelectedTX_Click: ordered deep copies after the last selected row. */
export function duplicateSourceRows(
  sources: readonly RadiatingSource[],
  selectedIds: readonly string[],
  createId: IdFactory = uid,
): SourceRowsResult {
  const selected = sourceSelection(sources, selectedIds);
  if (!selected.length) return unchanged(sources, selectedIds);
  const copies = cloneRows(selected, sources, createId).map((source) => ({
    ...source,
    name: `${source.name} - Copy`,
  }));
  const insertion =
    sources.findIndex(
      (source) => source.id === selected[selected.length - 1].id,
    ) + 1;
  return {
    sources: [
      ...sources.slice(0, insertion),
      ...copies,
      ...sources.slice(insertion),
    ],
    selectedIds: copies.map((source) => source.id),
    activeId: copies[0].id,
    changed: true,
  };
}

/** Native boundary rules abort the whole selection when it touches the destination edge. */
export function moveSourceRows(
  sources: readonly RadiatingSource[],
  selectedIds: readonly string[],
  direction: SourceRowMove,
): SourceRowsResult {
  const selected = sourceSelection(sources, selectedIds),
    selectedSet = new Set(selected.map((source) => source.id));
  if (!selected.length) return unchanged(sources, selectedIds);
  const indices = sources.flatMap((source, index) =>
    selectedSet.has(source.id) ? [index] : [],
  );
  const first = indices[0],
    last = indices[indices.length - 1];
  if (
    direction === "up" || direction === "top"
      ? first === 0
      : last === sources.length - 1
  )
    return unchanged(sources, selectedIds);
  let result = [...sources];
  if (direction === "top" || direction === "bottom") {
    const others = sources.filter((source) => !selectedSet.has(source.id));
    result =
      direction === "top" ? [...selected, ...others] : [...others, ...selected];
  } else {
    for (const index of direction === "up" ? indices : [...indices].reverse()) {
      const adjacent = index + (direction === "up" ? -1 : 1);
      [result[index], result[adjacent]] = [result[adjacent], result[index]];
    }
  }
  return {
    sources: result,
    selectedIds: selected.map((source) => source.id),
    activeId:
      direction === "top" ? selected[0].id : selected[selected.length - 1].id,
    changed: true,
  };
}

/** Internal, versioned data-only clipboard. A missing selection preserves the previous buffer. */
export function copySourceRows(
  sources: readonly RadiatingSource[],
  selectedIds: readonly string[],
): string | null {
  const selected = sourceSelection(sources, selectedIds);
  if (!selected.length) return null;
  if (selected.length > MAX_BUFFER_ROWS) invalid("trop de sources");
  const text = JSON.stringify({
    format: SOURCE_BUFFER_FORMAT,
    version: SOURCE_BUFFER_VERSION,
    sources: selected.map(validateSource),
  });
  if (text.length > MAX_BUFFER_LENGTH) invalid("tampon trop volumineux");
  return text;
}
export function parseSourceBuffer(text: string): RadiatingSource[] {
  if (typeof text !== "string" || text.length > MAX_BUFFER_LENGTH)
    invalid("taille du tampon");
  let value: unknown;
  try {
    value = JSON.parse(text);
  } catch {
    invalid("JSON illisible");
  }
  const buffer = record(
    value,
    ["format", "version", "sources"],
    "format du tampon",
  );
  if (
    buffer.format !== SOURCE_BUFFER_FORMAT ||
    buffer.version !== SOURCE_BUFFER_VERSION
  )
    invalid("format ou version non pris en charge");
  if (
    !Array.isArray(buffer.sources) ||
    !buffer.sources.length ||
    buffer.sources.length > MAX_BUFFER_ROWS
  )
    invalid("liste de sources");
  return buffer.sources.map(validateSource);
}

/** Insert after the active real row, at the end for the new-row placeholder, or at index zero when empty. */
export function pasteSourceRows(
  sources: readonly RadiatingSource[],
  activeId: string | null | undefined,
  text: string,
  createId: IdFactory = uid,
): SourceRowsResult {
  const copies = cloneRows(parseSourceBuffer(text), sources, createId);
  const current = sources.findIndex((source) => source.id === activeId),
    insertion = current < 0 ? sources.length : current + 1;
  return {
    sources: [
      ...sources.slice(0, insertion),
      ...copies,
      ...sources.slice(insertion),
    ],
    selectedIds: copies.map((source) => source.id),
    activeId: copies[0].id,
    changed: true,
  };
}

/** UC_PRTO.cmdColorAll_Click changes every selected real row. Selection clearing belongs to the UI. */
export function colorSourceRows(
  sources: readonly RadiatingSource[],
  selectedIds: readonly string[],
  color: string,
): RadiatingSource[] {
  if (!/^#(?:[0-9a-f]{3}|[0-9a-f]{6}|[0-9a-f]{8})$/i.test(color))
    throw new Error("Couleur de source invalide.");
  const selected = new Set(selectedIds);
  return sources.map((source) =>
    selected.has(source.id) ? { ...source, color } : source,
  );
}

/** UC_PRTO.cmdCheckUncheck_Click: the FIRST source decides the action, not any/all. */
export function toggleAllSourceRows(
  sources: readonly RadiatingSource[],
): RadiatingSource[] {
  const active = sources[0]?.active === false;
  return sources.map((source) => ({ ...source, active }));
}
