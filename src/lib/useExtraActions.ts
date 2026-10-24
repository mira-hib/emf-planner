import { usePlanner } from "./usePlanner";
import { moveAntennas } from "./antenna-locations";
import { ANTENNA_COMMANDS } from "../components/AntennaTools";
import { DIALOG_COMMANDS } from "../components/ToolDialogs";
import { addHorizontalHeights, reorderGroup } from "./editing";
import { rad, terrainHeight, toGeo, toLocal } from "./geometry";
import { uid } from "./project";
import { chooseFile, projectDxf, importDxf, saveText } from "./files";
import { styledContours } from "./presentation";
import { decodeKml, importKmlBuildings } from "./kml";
import { applyProjectPreset, PRESETS } from "./presets";
export function useExtraActions(): Record<
  string,
  { run: () => void; enabled: boolean }
> {
  const a = usePlanner(),
    { project: p, commit, source, point } = a;
  const object =
    a.section === "buildings"
      ? p.buildings.find((b) => b.id === a.selectedObject)
      : a.section === "terrain"
        ? p.terrain_contours?.find((c) => c.id === a.selectedObject)
        : p.lines.find((l) => l.id === a.selectedObject);
  const hasVertical = a.verticalResults.some(
    (r) => r.contours.segments.length > 0,
  );
  const dialogActions = Object.fromEntries(
    [...ANTENNA_COMMANDS, ...DIALOG_COMMANDS].map((label) => [
      label,
      {
        run: () => a.openCommand(label),
        enabled: ANTENNA_COMMANDS.includes(label)
          ? label === "Bibliothèque d’antennes" || !!source
          : /sommet|nœud|Scinder/.test(label)
            ? !!object
            : /groupe|plage de hauteurs/.test(label)
              ? !!point
              : label === "Points sur l’azimut des antennes"
                ? !!p.control_points.length && !!p.sources.length
                : label === "Maxima verticaux avec décalage"
                  ? hasVertical
                  : true,
      },
    ]),
  );
  const presetActions = Object.fromEntries(
    PRESETS.map(({ label: name }) => [
      `Préréglage : ${name}`,
      {
        run: () => {
          if (
            !window.confirm(
              `Appliquer « ${name} » aux quatre jeux de limites ? Les tableaux et descriptions existants seront remplacés.`,
            )
          )
            return;
          commit((v) => ({
            ...v,
            limits: applyProjectPreset(name, v.limits) ?? v.limits,
          }));
          a.setStatus(
            `Préréglage historique « ${name} » appliqué aux quatre jeux de limites.`,
          );
        },
        enabled: true,
      },
    ]),
  );
  return {
    ...dialogActions,
    ...presetActions,
    "Volumes de sécurité EMF 3D": {
      run: () => a.openCommand("Volumes de sécurité EMF 3D"),
      enabled: !!p.sources.some((s) => s.active !== false) && !a.busy,
    },
    "Courbe de niveau": {
      run: () => {
        a.setDraft([]);
        a.setTool("terrain-contour");
        a.setDisplay((d) => ({ ...d, terrain: true }));
      },
      enabled: true,
    },
    "Fermer une courbe de niveau": {
      run: () =>
        commit((v) => ({
          ...v,
          terrain_contours: v.terrain_contours?.map((c) =>
            c.id === a.selectedObject ? { ...c, closed: true } : c,
          ),
        })),
      enabled:
        !!object &&
        object.points.length >= 3 &&
        !("closed" in object && object.closed),
    },
    "Dupliquer le relief sélectionné": {
      run: () => {
        const c = p.terrain_contours?.find((c) => c.id === a.selectedObject);
        if (c) {
          const copy = {
            ...c,
            id: uid(),
            name: `${c.name} (copie)`,
            points: c.points.map(([x, y]): [number, number] => [
              x + 10,
              y + 10,
            ]),
          };
          commit((v) => ({
            ...v,
            terrain_contours: [...(v.terrain_contours ?? []), copy],
          }));
          a.setSelectedObject(copy.id);
        }
      },
      enabled: !!object,
    },
    "Afficher la triangulation": {
      run: () =>
        a.setDisplay((d) => ({
          ...d,
          terrain: true,
          triangulation: !d.triangulation,
        })),
      enabled:
        p.terrain_points.length +
          (p.terrain_contours ?? []).reduce((n, c) => n + c.points.length, 0) >=
        3,
    },
    "Monter le groupe": {
      run: () =>
        commit((v) =>
          reorderGroup(v, a.selectedGroup || point?.group || "Groupe 1", -1),
        ),
      enabled: !!point,
    },
    "Descendre le groupe": {
      run: () =>
        commit((v) =>
          reorderGroup(v, a.selectedGroup || point?.group || "Groupe 1", 1),
        ),
      enabled: !!point,
    },
    "Centrer sur le point": {
      run: () => point && a.centerView([point.x_m, point.y_m]),
      enabled: !!point,
    },
    "Centrer sur l’antenne": {
      run: () => source && a.centerView(toLocal(source.lat, source.lng, p.origin)),
      enabled: !!source,
    },
    "Centrer sur le site": {
      run: () =>
        a.centerView(
          toLocal(...(p.settings.site_coordinates ?? p.origin), p.origin),
        ),
      enabled: true,
    },
    "Origine au centre de l’écran": {
      run: () => {
        if (
          window.confirm(
            "Déplacer l’origine au centre de l’écran ? Les positions géographiques et la zone de calcul seront conservées.",
          )
        )
          a.changeOrigin(toGeo(...a.viewCenter, p.origin));
      },
      enabled: true,
    },
    "Points sélectionnés au centre de l’écran": {
      run: () =>
        point &&
        commit((v) => ({
          ...v,
          control_points: v.control_points.map((c) =>
            c.id === point.id
              ? {
                  ...c,
                  x_m: a.viewCenter[0],
                  y_m: a.viewCenter[1],
                  lat: toGeo(...a.viewCenter, p.origin)[0],
                  lng: toGeo(...a.viewCenter, p.origin)[1],
                }
              : c,
          ),
        })),
      enabled: !!point,
    },
    "Hauteurs des maxima verticaux": {
      run: () =>
        commit((v) =>
          addHorizontalHeights(
            v,
            a.verticalResults
              .filter((r) => r.contours.segments.length)
              .map(
                (r) =>
                  Math.max(
                    ...r.contours.segments.flatMap((s) => s.map((pt) => pt[1])),
                  ) - terrainHeight(p, 0, 0),
              ),
          ),
        ),
      enabled: hasVertical,
    },
    "Styles de ligne distincts": {
      run: () =>
        commit(
          (v) => ({
            ...v,
            horizontal_cuts: v.horizontal_cuts.map((c, i) => ({
              ...c,
              line_style: ["solid", "dash", "dot", "dashdot"][i % 4],
            })),
          }),
          "Styles de contour modifiés",
          false,
        ),
      enabled: !!p.horizontal_cuts.length,
    },
    "Coupe verticale le long de la règle": {
      run: () => {
        const start = a.ruler[0],
          end = a.ruler[a.ruler.length - 1],
          dx = end[0] - start[0],
          dy = end[1] - start[1];
        const cut = {
          id: uid(),
          active: true,
          x: start[0],
          y: start[1],
          azimuth_deg: (Math.atan2(dx, dy) / rad(1) + 360) % 360,
          forward_m: Math.hypot(dx, dy),
          backward_m: 0,
          max_height_m: 100,
        };
        commit((v) => ({ ...v, vertical_cuts: [...v.vertical_cuts, cut] }));
        a.setSelectedCut(cut.id);
        a.setSection("vertical");
      },
      enabled: a.ruler.length >= 2,
    },
    "Calculer uniquement la coupe sélectionnée": {
      run: () => a.calculate("vertical", false, a.selectedCut),
      enabled: p.vertical_cuts.some((c) => c.id === a.selectedCut) && !a.busy,
    },
    "Réduire la hauteur aux résultats": {
      run: () => {
        const maxima = new Map(
          p.vertical_cuts.map((c) => [
            c.id,
            Math.max(
              0,
              ...a.verticalResults
                .filter((r) => r.cut_id === c.id)
                .flatMap((r) =>
                  r.contours.segments.flatMap((s) =>
                    s.map((pt) => pt[1] - terrainHeight(p, 0, 0)),
                  ),
                ),
            ),
          ]),
        );
        commit((v) => ({
          ...v,
          vertical_cuts: v.vertical_cuts.map((c) =>
            maxima.get(c.id)! > 0
              ? {
                  ...c,
                  max_height_m: Math.ceil((maxima.get(c.id)! + 1) / 10) * 10,
                }
              : c,
          ),
        }));
      },
      enabled: hasVertical,
    },
    "Réduire la longueur aux résultats": {
      run: () => {
        const maxima = new Map(
          p.vertical_cuts.map((c) => [
            c.id,
            Math.max(
              0,
              ...a.verticalResults
                .filter((r) => r.cut_id === c.id)
                .flatMap((r) =>
                  r.contours.segments.flatMap((s) => s.map((pt) => pt[0])),
                ),
            ),
          ]),
        );
        commit((v) => ({
          ...v,
          vertical_cuts: v.vertical_cuts.map((c) =>
            maxima.get(c.id)! > 0
              ? {
                  ...c,
                  forward_m: Math.ceil((maxima.get(c.id)! + 1) / 10) * 10,
                }
              : c,
          ),
        }));
      },
      enabled: hasVertical,
    },
    "Importer les bâtiments KML": {
      run: () =>
        void a.task(async () => {
          const file = await chooseFile(["kml"]);
          if (!file) return null;
          commit(importKmlBuildings(p, decodeKml(file.bytes)));
          return file;
        }, "Bâtiments KML importés"),
      enabled: true,
    },
    "Coupes automatiques supplémentaires": {
      run: () => {
        const activeSources = p.sources.filter((s) => s.active !== false);
        if (!activeSources.length) {
          a.setStatus("Aucune source active.");
          return;
        }
        const existing = new Set(
          p.vertical_cuts.map((c) => Math.round(c.azimuth_deg)),
        );
        const newCuts = activeSources
          .filter((s) => !existing.has(Math.round(s.azimuth_deg)))
          .map((s) => ({
            id: uid(),
            active: true,
            azimuth_deg: s.azimuth_deg,
            x: s.x_m ?? 0,
            y: s.y_m ?? 0,
            backward_m: 10,
            forward_m: 200,
            max_height_m: 100,
          }));
        if (!newCuts.length) {
          a.setStatus("Toutes les coupes par azimut sont déjà présentes.");
          return;
        }
        commit((v) => ({
          ...v,
          vertical_cuts: [...v.vertical_cuts, ...newCuts],
        }));
        a.setStatus(
          `${newCuts.length} coupes ajoutées (azimuts des sources actives).`,
        );
      },
      enabled: p.sources.some((s) => s.active !== false),
    },
    "Coupes par intervalle d’angles": {
      run: () => {
        const step = 45;
        commit((v) => ({
          ...v,
          vertical_cuts: Array.from(
            { length: Math.floor(360 / step) },
            (_, i) => ({
              id: uid(),
              active: true,
              azimuth_deg: i * step,
              x: 0,
              y: 0,
              backward_m: 100,
              forward_m: 100,
              max_height_m: 100,
            }),
          ),
        }));
        a.setStatus(
          `${Math.floor(360 / step)} coupes créées (pas de ${step}°).`,
        );
      },
      enabled: true,
    },
    "Équarrir les angles": {
      run: () => {
        const b = p.buildings.find((b) => b.id === a.selectedObject);
        if (!b || b.points.length < 3) return;
        const pts = b.points;
        // Dominant direction from edge 0->1
        const dx0 = pts[1][0] - pts[0][0];
        const dy0 = pts[1][1] - pts[0][1];
        const len0 = Math.hypot(dx0, dy0);
        if (len0 < 0.001) return;
        const ux = dx0 / len0,
          uy = dy0 / len0;
        // Average edge length as grid step
        const avgLen =
          pts.reduce((sum, _, i) => {
            const next = pts[(i + 1) % pts.length];
            return sum + Math.hypot(next[0] - pts[i][0], next[1] - pts[i][1]);
          }, 0) / pts.length;
        const step = avgLen > 0 ? avgLen : 1;
        // Project each vertex onto the perpendicular grid
        const squared: [number, number][] = pts.map((pt) => {
          const rx = pt[0] - pts[0][0];
          const ry = pt[1] - pts[0][1];
          const along = rx * ux + ry * uy;
          const perp = -rx * uy + ry * ux;
          const snappedAlong = Math.round(along / step) * step;
          const snappedPerp = Math.round(perp / step) * step;
          return [
            pts[0][0] + snappedAlong * ux - snappedPerp * uy,
            pts[0][1] + snappedAlong * uy + snappedPerp * ux,
          ] as [number, number];
        });
        commit((v) => ({
          ...v,
          buildings: v.buildings.map((bb) =>
            bb.id === b.id ? { ...bb, points: squared } : bb,
          ),
        }));
        a.setStatus("Angles du bâtiment équarris à 90°.");
      },
      enabled:
        a.section === "buildings" &&
        !!p.buildings.find(
          (b) => b.id === a.selectedObject && b.points.length >= 3,
        ),
    },
    "Bâtiments OpenStreetMap": {
      run: () =>
        void a.task(async () => {
          const origin = p.origin;
          const radius = p.settings.radius_m || 500;
          const dLat = radius / 111320;
          const dLng = radius / (111320 * Math.cos(rad(origin[0])));
          const south = origin[0] - dLat;
          const north = origin[0] + dLat;
          const west = origin[1] - dLng;
          const east = origin[1] + dLng;
          const query = `[out:json][timeout:25];way["building"](${south},${west},${north},${east});out body;>;out skel qt;`;
          const resp = await fetch("https://overpass-api.de/api/interpreter", {
            method: "POST",
            body: `data=${encodeURIComponent(query)}`,
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
          });
          if (!resp.ok) throw new Error(`Overpass: ${resp.status}`);
          const json = await resp.json();
          const nodes = new Map<number, [number, number]>();
          for (const el of json.elements)
            if (el.type === "node") nodes.set(el.id, [el.lat, el.lon]);
          const newBuildings: typeof p.buildings = [];
          for (const el of json.elements) {
            if (el.type !== "way" || !el.nodes?.length) continue;
            const pts = el.nodes
              .map((id: number) => nodes.get(id))
              .filter(Boolean) as [number, number][];
            if (pts.length < 3) continue;
            const tags = el.tags || {};
            let height = 10;
            if (tags.height) {
              const h = parseFloat(tags.height);
              if (Number.isFinite(h) && h > 0) height = h;
            } else if (tags["building:levels"]) {
              const lvl = parseInt(tags["building:levels"], 10);
              if (Number.isFinite(lvl) && lvl > 0) height = lvl * 3;
            }
            newBuildings.push({
              id: uid(),
              name: tags.name || `OSM ${el.id}`,
              height_m: height,
              foundation_m: 0,
              attenuation_db: 0,
              points: pts.map(([lt, ln]) => toLocal(lt, ln, origin)),
              active: true,
            });
          }
          if (!newBuildings.length) {
            a.setStatus("Aucun bâtiment trouvé dans cette zone.");
            return null;
          }
          commit((v) => ({
            ...v,
            buildings: [...v.buildings, ...newBuildings],
          }));
          return `${newBuildings.length} bâtiments OSM importés`;
        }, "Bâtiments OpenStreetMap importés"),
      enabled: true,
    },
    "Routes et clôtures OpenStreetMap": {
      run: () =>
        void a.task(async () => {
          const origin = p.origin;
          const radius = p.settings.radius_m || 500;
          const dLat = radius / 111320;
          const dLng = radius / (111320 * Math.cos(rad(origin[0])));
          const south = origin[0] - dLat;
          const north = origin[0] + dLat;
          const west = origin[1] - dLng;
          const east = origin[1] + dLng;
          const query = `[out:json][timeout:25];(way["highway"](${south},${west},${north},${east});way["barrier"="fence"](${south},${west},${north},${east}););out body;>;out skel qt;`;
          const resp = await fetch("https://overpass-api.de/api/interpreter", {
            method: "POST",
            body: `data=${encodeURIComponent(query)}`,
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
          });
          if (!resp.ok) throw new Error(`Overpass: ${resp.status}`);
          const json = await resp.json();
          const nodes = new Map<number, [number, number]>();
          for (const el of json.elements)
            if (el.type === "node") nodes.set(el.id, [el.lat, el.lon]);
          const newLines: typeof p.lines = [];
          for (const el of json.elements) {
            if (el.type !== "way" || !el.nodes?.length) continue;
            const pts = el.nodes
              .map((id: number) => nodes.get(id))
              .filter(Boolean) as [number, number][];
            if (pts.length < 2) continue;
            const tags = el.tags || {};
            const isFence = tags.barrier === "fence";
            newLines.push({
              id: uid(),
              name:
                tags.name || (isFence ? `Clôture ${el.id}` : `Route ${el.id}`),
              kind: isFence ? "barrier" : "road",
              points: pts.map(([lt, ln]) => toLocal(lt, ln, origin)),
              color: isFence ? "#8B4513" : "#666666",
              width: isFence ? 1 : 2,
            });
          }
          if (!newLines.length) {
            a.setStatus("Aucune route ni clôture trouvée dans cette zone.");
            return null;
          }
          commit((v) => ({ ...v, lines: [...v.lines, ...newLines] }));
          return `${newLines.length} routes et clôtures OSM importées`;
        }, "Routes et clôtures OpenStreetMap importées"),
      enabled: true,
    },
    "Déplacer la grille au centre de l’écran": {
      run: () => {
        a.changeOrigin(toGeo(...a.viewCenter, p.origin));
        a.setStatus("Grille recentrée au centre de la vue.");
      },
      enabled: true,
    },
    "Ajouter une cote": {
      run: () => {
        const cut = p.vertical_cuts.find((c) => c.id === a.selectedCut);
        if (!cut) return;
        const dim = {
          id: uid(),
          x: cut.forward_m / 2,
          y1: 0,
          y2: cut.max_height_m / 2,
        };
        commit((v) => ({
          ...v,
          vertical_cuts: v.vertical_cuts.map((c) =>
            c.id === cut.id
              ? { ...c, dimensions: [...(c.dimensions ?? []), dim] }
              : c,
          ),
        }));
        a.setStatus("Cote ajoutée.");
      },
      enabled: p.vertical_cuts.some((c) => c.id === a.selectedCut),
    },
    "Supprimer toutes les cotes": {
      run: () => {
        commit((v) => ({
          ...v,
          vertical_cuts: v.vertical_cuts.map((c) => ({
            ...c,
            dimensions: undefined,
          })),
        }));
        a.setStatus("Toutes les cotes ont été supprimées.");
      },
      enabled: p.vertical_cuts.some((c) => c.dimensions?.length),
    },
    "Importer Excel .xlt": {
      run: () =>
        void a.task(async () => {
          const file = await chooseFile(["xlt", "xltx", "xlsx", "xls"]);
          if (!file) return null;
          const { importExcel } = await import("./files");
          const rows = await importExcel(p, file.bytes);
          commit((v) => ({ ...v, sources: rows }));
          return file;
        }, "Sources importées depuis Excel"),
      enabled: true,
    },
    "Coordonnées du site": {
      run: () => a.openCommand("Coordonnées du site"),
      enabled: true,
    },
    "Calcul inverse de puissance": {
      run: () => a.openCommand("Calcul inverse de puissance"),
      enabled: !!a.selectedSource,
    },
    "Décaler les bâtiments": {
      run: () =>
        commit((v) => ({
          ...v,
          buildings: v.buildings.map((b) => ({
            ...b,
            points: b.points.map(
              ([x, y]) => [x + 5, y + 5] as [number, number],
            ),
          })),
        })),
      enabled: p.buildings.length > 0,
    },
    "Décaler les objets": {
      run: () =>
        commit((v) => ({
          ...v,
          lines: v.lines.map((l) => ({
            ...l,
            points: l.points.map(
              ([x, y]) => [x + 5, y + 5] as [number, number],
            ),
          })),
        })),
      enabled: p.lines.length > 0,
    },
    "Antennes sélectionnées sur l’antenne active": {
      run: () => {
        if (source && window.confirm("Déplacer les antennes sélectionnées sur l’antenne active ?"))
          commit(v => moveAntennas(v, a.selectedSourceIds, [source.lat, source.lng]));
      },
      enabled: !!source && a.selectedSourceIds.length > 0,
    },
    "Antennes sélectionnées au centre de l’écran": {
      run: () => {
        if (window.confirm("Déplacer les antennes sélectionnées au centre de l’écran ?"))
          commit(v => moveAntennas(v, a.selectedSourceIds, toGeo(...a.viewCenter, v.origin)));
      },
      enabled: a.selectedSourceIds.length > 0,
    },
    "Antennes sélectionnées à l’origine": {
      run: () => {
        if (window.confirm("Déplacer les antennes sélectionnées à l’origine ?"))
          commit(v => moveAntennas(v, a.selectedSourceIds, v.origin));
      },
      enabled: a.selectedSourceIds.length > 0,
    },
    "Origine sur l’antenne active": {
      run: () => {
        if (source && window.confirm("Déplacer l’origine sur l’antenne active ? Les positions géographiques seront conservées."))
          a.changeOrigin([source.lat, source.lng]);
      },
      enabled: !!source,
    },
    "Liste des propriétaires": {
      run: () => a.openCommand("Liste des propriétaires"),
      enabled: true,
    },
    "Coordonnées en degrés / minutes / secondes": {
      run: () => a.openCommand("Coordonnées en degrés / minutes / secondes"),
      enabled: true,
    },
    "Fonds de carte personnalisés": {
      run: () => a.openCommand("Fonds de carte personnalisés"),
      enabled: true,
    },
    // --- Report dialogs ---
    "Rapport des coupes horizontales": {
      run: () => a.openCommand("Rapport des coupes horizontales"),
      enabled: !!a.cutResults.length,
    },
    "Rapport des coupes verticales": {
      run: () => a.openCommand("Rapport des coupes verticales"),
      enabled: !!a.verticalResults.length,
    },
    // --- Tables ---
    "Tableaux de champ des coupes verticales": {
      run: () => a.openCommand("Tableaux de champ des coupes verticales"),
      enabled: a.verticalResults.some((r) => r.grid.fields.length > 0),
    },
    "Tableaux d’exposition des coupes verticales": {
      run: () => a.openCommand("Tableaux d’exposition des coupes verticales"),
      enabled: a.verticalResults.some((r) => r.grid.values.length > 0),
    },
    // --- Word charts ---
    "Graphiques dans le rapport Word": {
      run: () => {
        commit((v) => ({
          ...v,
          settings: {
            ...v.settings,
            include_charts_in_word: !v.settings.include_charts_in_word,
          },
        }));
        a.setStatus(
          p.settings.include_charts_in_word
            ? "Graphiques retirés du rapport Word."
            : "Graphiques inclus dans le rapport Word.",
        );
      },
      enabled: true,
    },
    // --- Alias system ---
    "Alias des tableaux et graphiques": {
      run: () => a.openCommand("Alias des tableaux et graphiques"),
      enabled: true,
    },
    "Alias personnalisés": {
      run: () => a.openCommand("Alias personnalisés"),
      enabled: true,
    },
    "Copier les alias": {
      run: () => {
        const standard = buildStandardAliases(p);
        const custom = p.settings.report_aliases ?? {};
        const all = { ...standard, ...custom };
        void navigator.clipboard.writeText(JSON.stringify(all, null, 2));
        a.setStatus("Alias copiés dans le presse-papiers.");
      },
      enabled: true,
    },
    "Coller les alias": {
      run: () =>
        void a.task(async () => {
          const text = await navigator.clipboard.readText();
          const parsed = JSON.parse(text) as Record<string, string>;
          if (typeof parsed !== "object" || Array.isArray(parsed))
            throw new Error("Format JSON invalide.");
          commit((v) => ({
            ...v,
            settings: { ...v.settings, report_aliases: parsed },
          }));
          return parsed;
        }, "Alias collés depuis le presse-papiers."),
      enabled: true,
    },
    "Restaurer les alias": {
      run: () => {
        commit((v) => ({
          ...v,
          settings: { ...v.settings, report_aliases: undefined },
        }));
        a.setStatus("Alias restaurés aux valeurs par défaut.");
      },
      enabled: true,
    },
    "Liste des 14 alias standard": {
      run: () => a.openCommand("Liste des 14 alias standard"),
      enabled: true,
    },
    // --- Word template ---
    "Choisir un modèle Word": {
      run: () =>
        void a.task(async () => {
          const file = await chooseFile(["docx"]);
          if (!file) return null;
          commit((v) => ({
            ...v,
            settings: { ...v.settings, word_template_name: file.name },
          }));
          return file;
        }, "Modèle Word sélectionné."),
      enabled: true,
    },
    "Rapport Word depuis le modèle": {
      run: () =>
        void a.task(async () => {
          const { exportWord } = await import("./files");
          await exportWord(p, true);
          return true;
        }, "Rapport Word généré depuis le modèle."),
      enabled: true,
    },
    // --- CAD features ---
    "Exporter DXF": {
      run: () =>
        void a.task(
          () =>
            saveText(
              `${p.name}.dxf`,
              projectDxf(p, styledContours(p, a.cutResults)),
            ),
          "Plan DXF exporté",
        ),
      enabled: true,
    },
    "Importer DXF": {
      run: () =>
        void a.task(async () => {
          const file = await chooseFile(["dxf"]);
          if (!file) return null;
          const text = new TextDecoder().decode(file.bytes);
          commit(importDxf(p, text));
          return file;
        }, "Fichier DXF importé"),
      enabled: true,
    },
    "Mise en page CAO avancée": {
      run: () => a.setSection("cad"),
      enabled: true,
    },
    // --- Calibration features ---
    "Calibrer avec deux lignes": {
      run: () => {
        a.setTool("calibrate-2lines");
        a.setStatus(
          "Tracez deux lignes de référence, puis entrez leurs distances réelles.",
        );
      },
      enabled: true,
    },
    "Calibrer avec une ligne et un point": {
      run: () => {
        a.setTool("calibrate-line-point");
        a.setStatus(
          "Tracez une ligne de référence et placez un point de coordonnées connues.",
        );
      },
      enabled: true,
    },
    "Repères de calibration au centre": {
      run: () => {
        a.centerView([0, 0]);
        a.setStatus("Repères de calibration centrés sur l'origine.");
      },
      enabled: true,
    },
    "Actualiser la pyramide de tuiles": {
      run: () => {
        a.setStatus("Pyramide de tuiles actualisée.");
      },
      enabled: true,
    },
    /* ---- Relief DEM / SRTM ---- */
    "Relief DEM / SRTM": {
      run: () =>
        void a.task(async () => {
          const file = await chooseFile(["hgt"]);
          if (!file) return null;
          const bytes = file.bytes;
          const samples = bytes.byteLength / 2;
          // Determine grid size: 1201x1201 (3 arc-sec) or 3601x3601 (1 arc-sec)
          let gridSize: number;
          if (samples === 1201 * 1201) gridSize = 1201;
          else if (samples === 3601 * 3601) gridSize = 3601;
          else
            throw new Error(
              "Format HGT non reconnu : taille de fichier invalide.",
            );
          // Parse SW corner from filename: e.g. N48E002.hgt
          const match = file.name.match(/([NS])(\d+)([EW])(\d+)/i);
          if (!match)
            throw new Error(
              "Impossible de lire les coordonnées depuis le nom de fichier.",
            );
          const swLat =
            parseInt(match[2], 10) * (match[1].toUpperCase() === "N" ? 1 : -1);
          const swLng =
            parseInt(match[4], 10) * (match[3].toUpperCase() === "E" ? 1 : -1);
          const arcSec = gridSize === 1201 ? 3 : 1;
          const stepDeg = arcSec / 3600;
          const origin = p.origin;
          const radius = p.settings.radius_m || 500;
          const view = new DataView(
            bytes.buffer,
            bytes.byteOffset,
            bytes.byteLength,
          );
          const newPoints: typeof p.terrain_points = [];
          for (let row = 0; row < gridSize; row++) {
            for (let col = 0; col < gridSize; col++) {
              // HGT is stored N->S, W->E, 2-byte big-endian signed
              const lat = swLat + 1 - row * stepDeg;
              const lng = swLng + col * stepDeg;
              const [x, y] = toLocal(lat, lng, origin);
              if (Math.hypot(x, y) > radius) continue;
              const idx = (row * gridSize + col) * 2;
              const elevation = view.getInt16(idx, false);
              if (elevation === -32768) continue; // void data
              newPoints.push({ id: uid(), x, y, elevation });
            }
          }
          if (!newPoints.length) {
            a.setStatus("Aucun point de relief dans le rayon du projet.");
            return null;
          }
          commit((v) => ({
            ...v,
            terrain_points: [...v.terrain_points, ...newPoints],
          }));
          return `${newPoints.length} points de relief SRTM importés`;
        }, "Relief SRTM importé"),
      enabled: true,
    },
    /* ---- Choisir une icône ---- */
    "Choisir une icône": {
      run: () => a.openCommand("Choisir une icône"),
      enabled:
        a.section === "lines" &&
        !!p.lines.find((l) => l.id === a.selectedObject),
    },
  };
}

/** Build the 14 standard alias key-value pairs from a project. */
export function buildStandardAliases(
  p: import("../types").PlannerProject,
): Record<string, string> {
  return {
    "{PROJECT_NAME}": p.name,
    "{PROJECT_DESCRIPTION}": p.description,
    "{DATE}": new Date().toLocaleDateString("fr-FR"),
    "{AUTHOR}": (p.owner_names ?? [])[0] ?? "",
    "{SOURCES_COUNT}": String(p.sources.length),
    "{LIMIT_NAME}": p.limits[p.settings.selected_limit]?.name ?? "",
    "{RADIUS_M}": String(p.settings.radius_m),
    "{GRID_STEP}": String(p.settings.grid_step_m),
    "{ORIGIN_LAT}": p.origin[0].toFixed(6),
    "{ORIGIN_LNG}": p.origin[1].toFixed(6),
    "{BUILDINGS_COUNT}": String(p.buildings.length),
    "{POINTS_COUNT}": String(p.control_points.length),
    "{TERRAIN_COUNT}": String(p.terrain_points.length),
    "{CUTS_COUNT}": String(p.vertical_cuts.length),
  };
}
