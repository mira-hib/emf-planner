import { useState } from "react";
import { invoke, isTauri } from "@tauri-apps/api/core";
import { usePlanner } from "../lib/usePlanner";
import { chooseFile, processWordTemplate, exportAliasWord } from "../lib/files";
import { DEFAULT_REPORT_ALIASES, REPORT_ALIASES } from "../lib/report-content";

export const encodeTemplate = (name: string, bytes: Uint8Array) => JSON.stringify({ name, data: btoa(Array.from(bytes, (b) => String.fromCharCode(b)).join("")) });
export function decodeTemplate(stored?: string): { name: string; bytes: Uint8Array } | null { if (!stored) return null; try { const data = JSON.parse(stored); return { name: data.name, bytes: Uint8Array.from(atob(data.data), (c) => c.charCodeAt(0)) }; } catch { return null; } }
export async function chooseWordTemplate() {
  const file = await chooseFile(["dot", "dotx", "docx"]); if (!file) return null;
  if (file.bytes[0] === 0x50 && file.bytes[1] === 0x4b) return file;
  if (!isTauri() || !file.path) throw new Error("Le modèle .dot binaire nécessite la conversion Word dans l’application Windows. Enregistrez-le en .dotx ou .docx pour le navigateur.");
  return { ...file, bytes: new Uint8Array(await invoke<number[]>("convert_word_template", { path: file.path })) };
}
export async function exportStoredWordTemplate(project: Parameters<typeof processWordTemplate>[0]) { const file = decodeTemplate(project.word_template); if (!file) throw new Error("Choisissez un modèle Word .dot, .dotx ou .docx."); return processWordTemplate(project, file.bytes); }
export default function ReportTemplatePanel() {
  const { project, commit, task } = usePlanner(), stored = decodeTemplate(project.word_template);
  const [tsv, setTsv] = useState(""), [help, setHelp] = useState(false);
  const aliases: Record<string, string> = project.settings.report_aliases ?? project.report_aliases ?? DEFAULT_REPORT_ALIASES();
  const update = (next: Record<string, string>) => commit((p) => ({ ...p, report_aliases: next, settings: { ...p.settings, report_aliases: next } }));
  return <fieldset className="report-template-panel"><legend>Rapport par modèle Word</legend>
    <label>Modèle Word<input aria-label="Modèle Word" readOnly value={stored?.name ?? project.settings.word_template_name ?? ""}/></label>
    <button onClick={() => void task(async () => { const file = await chooseWordTemplate(); if (file) commit((p) => ({ ...p, word_template: encodeTemplate(file.name, file.bytes), settings: { ...p.settings, word_template_name: file.name } })); }, "Modèle chargé")}>Choisir le modèle Word…</button>
    <button disabled={!stored} onClick={() => void task(() => exportStoredWordTemplate(project), "Rapport Word généré")}>Générer depuis le modèle</button>
    <table aria-label="Alias personnalisés"><thead><tr><th>Alias</th><th>Valeur</th><th/></tr></thead><tbody>{Object.entries(aliases).map(([key, value]) => <tr key={key}><td>{`{${key}}`}</td><td><input aria-label={`Valeur ${key}`} value={value} onChange={(e) => update({ ...aliases, [key]: e.target.value })}/></td><td><button aria-label={`Supprimer ${key}`} onClick={() => { const next = { ...aliases }; delete next[key as keyof typeof next]; update(next); }}>×</button></td></tr>)}</tbody></table>
    <label>Alias et valeurs (TSV)<textarea aria-label="Alias et valeurs TSV" value={tsv} onChange={(e) => setTsv(e.target.value)} placeholder={'{alias}\tvaleur'}/></label>
    <button onClick={() => void task(async () => { const next = { ...aliases }; for (const row of tsv.split(/\r?\n/).filter(Boolean)) { const split = row.indexOf("\t"); if (split < 0) throw new Error("Chaque ligne doit contenir un alias, une tabulation, puis sa valeur (qui peut être vide)."); const key = row.slice(0, split).replace(/^\{|\}$/g, ""); if (!key || /[{}]/.test(key)) throw new Error("Nom d’alias invalide."); next[key as keyof typeof next] = row.slice(split + 1); } update(next); }, "Alias importés")}>Importer les alias TSV</button>
    <button onClick={() => void task(async () => navigator.clipboard.writeText(JSON.stringify(aliases)), "Alias copiés")}>Copier les alias</button>
    <button onClick={() => void task(async () => { const value = JSON.parse(await navigator.clipboard.readText()); if (!value || typeof value !== "object" || Object.values(value).some((v) => typeof v !== "string")) throw new Error("Presse-papiers d’alias invalide."); update(value); }, "Alias collés")}>Coller les alias</button>
    <button onClick={() => update(DEFAULT_REPORT_ALIASES())}>Restaurer les alias</button>
    <button onClick={() => setHelp(!help)}>Aide des alias</button>
    {help && <div role="region" aria-label="Aide des alias"><p>Placez chaque alias de contenu dans son propre paragraphe. Les alias de texte fonctionnent aussi dans les tableaux, zones de texte, en-têtes et pieds de page. Les valeurs sont insérées littéralement. En cas de doublon TSV, la dernière valeur est retenue. Les alias de contenu réservés ont priorité.</p>{REPORT_ALIASES.map((alias) => <div key={alias}><code>{`{${alias}}`}</code> <button onClick={() => void task(() => exportAliasWord(project, alias), "Rapport Word exporté")}>Exporter Word</button></div>)}</div>}
  </fieldset>;
}
