import { invoke, isTauri } from "@tauri-apps/api/core";
import { readPreferences, type AppPreferences } from "./preferences";
let configured: Promise<void> | undefined;
export function configureNetwork(preferences: AppPreferences, password?: string) {
  if (!isTauri()) {
    if (preferences.proxy) return Promise.reject(new Error("Le proxy personnalisé nécessite l’application desktop. Le navigateur utilise son propre proxy système."));
    return Promise.resolve();
  }
  configured = invoke<void>("configure_network", {
    proxy: preferences.proxy ? { ...preferences.proxy, password: password ?? null } : null,
    directory: preferences.cache_directory ?? null,
  });
  return configured;
}
export async function networkFetch(input: RequestInfo | URL, options: RequestInit = {}, cache = false): Promise<Response> {
  if (!isTauri()) return fetch(input, options);
  await (configured ?? configureNetwork(readPreferences()));
  const url = input instanceof Request ? input.url : String(input);
  const headers = Object.fromEntries(new Headers(options.headers ?? (input instanceof Request ? input.headers : undefined)).entries());
  if (options.signal?.aborted) throw new DOMException("Requête annulée", "AbortError");
  const result = await invoke<{ status: number; content_type: string; bytes: number[] }>("network_request", {
    request: { url, method: options.method ?? "GET", headers, body: typeof options.body === "string" ? options.body : null, cache },
  });
  if (options.signal?.aborted) throw new DOMException("Requête annulée", "AbortError");
  return new Response(new Uint8Array(result.bytes), { status: result.status, headers: { "Content-Type": result.content_type } });
}
