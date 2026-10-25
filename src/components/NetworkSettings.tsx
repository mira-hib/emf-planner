import { useState } from "react";
import { isTauri } from "@tauri-apps/api/core";
import { usePlanner } from "../lib/usePlanner";
import { readPreferences, writePreferences, validateProxy } from "../lib/preferences";
import { configureNetwork } from "../lib/network";
import Modal from "./Modal";
export const NETWORK_COMMANDS = ["Paramètres proxy", "Dossier du cache"];
export default function NetworkSettings() {
  const a = usePlanner(), preferences = readPreferences();
  const [host, setHost] = useState(preferences.proxy?.host ?? ""), [port, setPort] = useState(String(preferences.proxy?.port ?? 8080));
  const [user, setUser] = useState(preferences.proxy?.user ?? ""), [password, setPassword] = useState("");
  const [directory, setDirectory] = useState(preferences.cache_directory ?? ""), [error, setError] = useState(""), [busy, setBusy] = useState(false);
  async function apply() {
    setError(""); setBusy(true);
    try {
      const proxy = validateProxy(host, Number(port.replace(",", ".")));
      const next = { ...preferences, proxy: proxy ? { ...proxy, user: user || undefined } : undefined, cache_directory: directory || undefined };
      await configureNetwork(next, password); writePreferences(next); a.setStatus("Configuration réseau enregistrée"); a.setCommand("");
    } catch (e) { setError(String(e instanceof Error ? e.message : e)); } finally { setBusy(false); }
  }
  return <Modal title={a.command} onClose={() => !busy && a.setCommand("")}>
    {!isTauri() && <p>Le navigateur utilise le proxy et le stockage gérés par son environnement. Le choix d’un dossier et du proxy est disponible dans l’application desktop.</p>}
    {a.command === "Paramètres proxy" ? <>
      <label>Serveur proxy<input aria-label="Serveur proxy" value={host} onChange={e => setHost(e.target.value)} placeholder="Adresse DNS, IPv4 ou IPv6"/></label>
      <label>Port<input aria-label="Port du proxy" inputMode="numeric" value={port} onChange={e => setPort(e.target.value)}/></label>
      <label>Utilisateur<input aria-label="Utilisateur du proxy" value={user} onChange={e => setUser(e.target.value)}/></label>
      <label>Mot de passe<input aria-label="Mot de passe du proxy" type="password" autoComplete="off" value={password} onChange={e => setPassword(e.target.value)}/></label>
      <p>Laisser le serveur vide désactive le proxy personnalisé. Le mot de passe est conservé uniquement pour la session en cours.</p>
    </> : <>
      <label>Dossier du cache<input aria-label="Dossier du cache" value={directory} onChange={e => setDirectory(e.target.value)} placeholder="Dossier automatique de l’application"/></label>
      <button disabled={!isTauri() || busy} onClick={() => void a.task(async () => {
        const { open } = await import("@tauri-apps/plugin-dialog"); const path = await open({ directory: true }); if (typeof path === "string") setDirectory(path);
      }, "Dossier sélectionné")}>Choisir le dossier…</button>
      <p>Le dossier est créé et son accès en écriture vérifié à l’application. Les fichiers existants sans rapport avec le cache restent conservés.</p>
    </>}
    {error && <p role="alert">{error}</p>}
    <footer><button disabled={busy} onClick={() => a.setCommand("")}>Annuler</button><button disabled={busy || !isTauri()} onClick={() => void apply()}>{busy ? "Vérification…" : "Appliquer"}</button></footer>
  </Modal>;
}
