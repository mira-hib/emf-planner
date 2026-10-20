import { usePlanner } from "./usePlanner";
import { ANTENNA_COMMANDS } from "../components/AntennaTools";
import { DIALOG_COMMANDS } from "../components/ToolDialogs";
import { addHorizontalHeights, reorderGroup } from "./editing";
import { rad, terrainHeight, toGeo } from "./geometry";
import { uid } from "./project";
import { chooseFile } from "./files";
import { importKmlBuildings } from "./kml";
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
  return {
    ...dialogActions,
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
      run: () => source && a.centerView([source.x_m ?? 0, source.y_m ?? 0]),
      enabled: !!source,
    },
    "Centrer sur le site": { run: () => a.centerView([0, 0]), enabled: true },
    "Origine au centre de l’écran": {
      run: () => a.changeOrigin(toGeo(...a.viewCenter, p.origin)),
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
          commit(importKmlBuildings(p, new TextDecoder().decode(file.bytes)));
          return file;
        }, "Bâtiments KML importés"),
      enabled: true,
    },
  };
}
