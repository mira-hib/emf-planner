import type { CoordinateFormat } from "./coordinates";
export interface NetworkProxy { host: string; port: number; user?: string }
export interface AppPreferences {
  coordinate_format?: CoordinateFormat;
  owner_names?: string[];
  owner_colors?: Record<string, string>;
  proxy?: NetworkProxy;
  cache_directory?: string;
}
export function readPreferences(): AppPreferences {
  try { const value = JSON.parse(localStorage.getItem("emf-planner-preferences-v1") ?? "{}");
    return value && typeof value === "object" && !Array.isArray(value) ? value : {}; } catch { return {}; }
}
export function writePreferences(patch: Partial<AppPreferences>) {
  const next = { ...readPreferences(), ...patch };
  // Network passwords belong only to the native client's in-memory session.
  localStorage.setItem("emf-planner-preferences-v1", JSON.stringify(next));
  return next;
}
export function validateProxy(host: string, port: number) {
  if (!host.trim()) return undefined;
  if (!Number.isInteger(port) || port < 1 || port > 65535 || /[\s/@?#]/.test(host)) throw new Error("Adresse de proxy et port entier de 1 à 65535 requis.");
  const formatted = host.includes(":") && !host.startsWith("[") ? `[${host}]` : host;
  let url: URL; try { url = new URL(`http://${formatted}:${port}`); } catch { throw new Error("Adresse de proxy invalide (nom DNS, IPv4 ou IPv6 attendu)."); }
  if (!url.hostname || url.pathname !== "/") throw new Error("Adresse de proxy invalide.");
  return { host: host.trim(), port };
}
