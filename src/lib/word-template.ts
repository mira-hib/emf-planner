import JSZip from "jszip";
import type { PlannerProject } from "../types";
import { validateArchive } from "./archive-validation";
import { canonicalReportAlias, DEFAULT_REPORT_ALIASES, REPORT_ALIASES, reportAliasDocument } from "./report-content";

export const xmlEscape = (text: string) => text.replace(/[&<>\"]/g, (c) => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;" })[c]!);
const xmlDecode = (text: string) => text.replace(/&(#x[\da-f]+|#\d+|amp|lt|gt|quot|apos);/gi, (_, entity: string) => entity[0] === "#" ? String.fromCodePoint(entity[1] === "x" ? parseInt(entity.slice(2), 16) : Number(entity.slice(1))) : ({ amp: "&", lt: "<", gt: ">", quot: '"', apos: "'" })[entity]!);
export function reportTextAliases(project: PlannerProject): Record<string, string> {
  const defaults = DEFAULT_REPORT_ALIASES();
  const values: Record<string, string> = { ...defaults, PROJET: project.name, DESCRIPTION: project.description, NB_SOURCES: String(project.sources.length), NB_POINTS: String(project.control_points.length), DATE: defaults.Date };
  for (const [key, value] of Object.entries({ ...project.report_aliases, ...project.settings.report_aliases })) values[key.replace(/^\{|\}$/g, "")] = value;
  return values;
}
interface TextNode { start: number; end: number; text: string }
interface ParagraphSpan { start: number; end: number; nodes: TextNode[] }
/** Paragraph stack keeps textbox paragraphs separate from the containing drawing paragraph. */
function paragraphs(xml: string): ParagraphSpan[] {
  const stack: ParagraphSpan[] = [], done: ParagraphSpan[] = [];
  const tokens = /<w:p(?=[\s>])[^>]*>|<\/w:p\s*>|<w:t(?=[\s>])[^>]*>([\s\S]*?)<\/w:t\s*>/g;
  for (const match of xml.matchAll(tokens)) {
    if (match[0].startsWith("</w:p")) { const p = stack.pop(); if (p) { p.end = match.index! + match[0].length; done.push(p); } }
    else if (match[0].startsWith("<w:p")) stack.push({ start: match.index!, end: 0, nodes: [] });
    else if (stack.length) { const contentStart = match.index! + match[0].indexOf(">") + 1; stack[stack.length - 1].nodes.push({ start: contentStart, end: contentStart + match[1].length, text: xmlDecode(match[1]) }); }
  }
  return done;
}
export interface TemplateResult { bytes: Uint8Array; replaced: number; unknownAliases: string[] }
export async function renderWordTemplate(project: PlannerProject, bytes: Uint8Array): Promise<TemplateResult> {
  if (bytes[0] !== 0x50 || bytes[1] !== 0x4b) throw new Error("Modèle Word binaire .dot : utilisez la conversion Word de l’application Windows ou un modèle .dotx/.docx.");
  validateArchive(bytes);
  const zip = await JSZip.loadAsync(bytes, { checkCRC32: true });
  if (!zip.file("word/document.xml")) throw new Error("Le fichier ne contient pas de document Word OOXML.");
  const entries = Object.keys(zip.files); if (entries.length > 10000) throw new Error("Modèle Word trop volumineux.");
  const values = reportTextAliases(project), unknown = new Set<string>(); let replaced = 0, nextRel = 10000;
  for (const path of entries.filter((s) => /^word\/(document|header\d*|footer\d*)\.xml$/.test(s))) {
    let xml = await zip.file(path)!.async("string");
    if (xml.length > 20_000_000 || /<!DOCTYPE|<!ENTITY/i.test(xml)) throw new Error("Structure XML du modèle non prise en charge.");
    const edits: { start: number; end: number; text: string }[] = [];
    for (const p of paragraphs(xml)) {
      const text = p.nodes.map((n) => n.text).join("");
      const structural = [...text.matchAll(/\{([^{}]+)\}/g)].filter((m) => REPORT_ALIASES.includes(canonicalReportAlias(m[1]) as typeof REPORT_ALIASES[number]));
      if (structural.length) {
        if (structural.length !== 1 || text.trim() !== structural[0][0]) throw new Error(`L’alias {${structural[0][1]}} doit occuper son propre paragraphe (corps, tableau, en-tête ou pied de page).`);
        const generated = await JSZip.loadAsync(await reportAliasDocument(project, canonicalReportAlias(structural[0][1])));
        let fragment = (await generated.file("word/document.xml")!.async("string")).match(/<w:body>([\s\S]*?)<\/w:body>/)![1].replace(/<w:sectPr[\s\S]*?<\/w:sectPr>/g, "");
        const relPath = path.replace("word/", "word/_rels/") + ".rels";
        let rels = zip.file(relPath) ? await zip.file(relPath)!.async("string") : '<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"></Relationships>';
        const sourceRels = await generated.file("word/_rels/document.xml.rels")!.async("string");
        for (const m of sourceRels.matchAll(/<Relationship\b[^>]*\/>/g)) {
          const id = m[0].match(/\bId="([^"]+)"/)?.[1], target = m[0].match(/\bTarget="([^"]+)"/)?.[1];
          if (!id || !target || !/\/image"/.test(m[0])) continue;
          const name = `template_${nextRel}_${target.split("/").pop()}`, newId = `rIdReport${nextRel++}`;
          const media = generated.file(`word/${target}`); if (!media) throw new Error("Image de rapport manquante.");
          zip.file(`word/media/${name}`, await media.async("uint8array"));
          fragment = fragment.split(`r:embed="${id}"`).join(`r:embed="${newId}"`);
          rels = rels.replace("</Relationships>", m[0].replace(`Id="${id}"`, `Id="${newId}"`).replace(`Target="${target}"`, `Target="media/${name}"`) + "</Relationships>");
        }
        zip.file(relPath, rels);
        // The generated fragment carries namespaces used by SVG/PNG drawings.
        const root = (await generated.file("word/document.xml")!.async("string")).match(/<w:document\b([^>]*)>/)![1];
        // Attach declarations to each block, preserving offsets in the original XML.
        fragment = fragment.replace(/<(w:p|w:tbl)(?=[\s>])/g, `<$1 ${[...root.matchAll(/xmlns:[\w]+="[^"]+"/g)].map((m) => m[0]).join(" ")}`);
        edits.push({ start: p.start, end: p.end, text: fragment }); replaced++; continue;
      }
      const transformed = text.replace(/\{([^{}]+)\}/g, (whole, key: string) => { if (Object.prototype.hasOwnProperty.call(values, key)) { replaced++; return values[key]; } unknown.add(key); return whole; });
      if (transformed !== text && p.nodes.length) { edits.push({ start: p.nodes[0].start, end: p.nodes[0].end, text: xmlEscape(transformed) }); for (const node of p.nodes.slice(1)) edits.push({ start: node.start, end: node.end, text: "" }); }
    }
    for (const edit of edits.sort((a, b) => b.start - a.start)) xml = xml.slice(0, edit.start) + edit.text + xml.slice(edit.end);
    zip.file(path, xml);
  }
  let contentTypes = await zip.file("[Content_Types].xml")!.async("string");
  for (const [extension, mime] of [["svg", "image/svg+xml"], ["png", "image/png"]]) if (!contentTypes.includes(`Extension="${extension}"`)) contentTypes = contentTypes.replace("</Types>", `<Default Extension="${extension}" ContentType="${mime}"/></Types>`);
  contentTypes = contentTypes.replace("application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"); zip.file("[Content_Types].xml", contentTypes);
  return { bytes: await zip.generateAsync({ type: "uint8array", compression: "DEFLATE" }), replaced, unknownAliases: [...unknown] };
}
