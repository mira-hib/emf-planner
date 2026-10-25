import {
  Cable,
  Copy,
  FileText,
  Globe,
  Grid2X2,
  Layers,
  Library,
  ListOrdered,
  MapPin,
  Maximize,
  Move,
  Palette,
  PenLine,
  Plus,
  RotateCw,
  Scissors,
  Settings,
  Trash2,
  Waves,
} from "lucide-react";
import type { Section } from "../types";
import { PENDING_COMMANDS } from "../lib/capabilities";
import { usePlanner } from "../lib/usePlanner";
import { COLORS, uid } from "../lib/project";
import { terrainHeight } from "../lib/geometry";
import { useExtraActions } from "../lib/useExtraActions";
import { useGeometryActions } from "../lib/useGeometryActions";
import OriginalIcon from "./OriginalIcon";
import { fillSourceBeamWidths, hasSourcePatterns } from "../lib/source-fields";

// Secondary commands identified in the recovered UC_*.cs forms.
// Commands without an action stay visible and disabled until implemented.
const PIXEL_MOVE_TOOLS = ["Déplacer l’objet d’un pixel à gauche", "Déplacer l’objet d’un pixel à droite", "Déplacer l’objet d’un pixel vers le haut", "Déplacer l’objet d’un pixel vers le bas"];
export const EXTRA_TOOLS: Record<Section, string[]> = {
  sources: [
    "Bibliothèque d’antennes",
    "Éditeur de diagramme",
    "Enveloppe de diagrammes",
    "Antenne symétrique ITU-R F.699",
    "Diagramme V : −1°",
    "Diagramme V : +1°",
    "Bibliothèque de câbles",
    "Calcul inverse de puissance",
    "Calculer les ouvertures de toutes les sources",
    "Calculer les ouvertures de la source sélectionnée",
    "Importer Excel .xlt",
  ],
  limits: [
    "ICNIRP",
    "Union européenne",
    "États-Unis",
    "Suisse",
    "Royaume-Uni",
    "Belgique",
    "Italie",
    "Chine",
    "Russie",
    "Pologne",
    "Inde",
    "Lituanie",
    "Slovénie",
    "Grèce",
    "Croatie",
    "Bulgarie",
  ].map((n) => `Préréglage : ${n}`),
  origin: [
    "Dimensions de la zone de calcul",
    "Origine au centre de l’écran",
    "Coordonnées du site",
    "Centrer sur le site",
  ],
  antennas: [
    "Centrer sur l’antenne",
    "Antennes sélectionnées au centre de l’écran",
    "Antennes sélectionnées sur l’antenne active",
    "Antennes sélectionnées à l’origine",
    "Origine sur l’antenne active",
  ],
  buildings: [
    ...PIXEL_MOVE_TOOLS,
    "Dupliquer le bâtiment",
    "Altitude des bases depuis le terrain",
    "Altitude de la base sélectionnée",
    "Équarrir les angles",
    "Ajouter un sommet",
    "Supprimer un sommet",
    "Placer à l’arrière",
    "Décaler les bâtiments",
    "Importer les bâtiments KML",
    "Bâtiments OpenStreetMap",
  ],
  terrain: [
    ...PIXEL_MOVE_TOOLS,
    "Courbe de niveau",
    "Fermer une courbe de niveau",
    "Ajouter un sommet de relief",
    "Supprimer un sommet de relief",
    "Dupliquer le relief sélectionné",
    "Afficher la triangulation",
    "Relief DEM / SRTM",
  ],
  lines: [
    ...PIXEL_MOVE_TOOLS,
    "Dupliquer l’objet",
    "Ajouter un nœud",
    "Supprimer un nœud",
    "Scinder une ligne",
    "Choisir une icône",
    "Décaler les objets",
    "Routes et clôtures OpenStreetMap",
  ],
  points: [
    "Renommer le groupe",
    "Supprimer le groupe",
    "Monter le groupe",
    "Descendre le groupe",
    "Points sur l’azimut des antennes",
    "Calcul d’un point par plage de hauteurs",
    "Points sélectionnés au centre de l’écran",
    "Centrer sur le point",
  ],
  horizontal: [
    "Trier par hauteur",
    "Couleurs distinctes",
    "Couleur unique",
    "Styles de ligne distincts",
    "Hauteurs par intervalle",
    "Hauteurs des maxima verticaux",
    "Maxima verticaux avec décalage",
    "Coupe verticale le long de la règle",
    "Déplacer la grille au centre de l’écran",
    "Rapport des coupes horizontales",
  ],
  vertical: [
    "Trier par azimut",
    "Coupes suivant le maximum du diagramme",
    "Coupes par intervalle d’angles",
    "Coupes automatiques supplémentaires",
    "Réduire la hauteur aux résultats",
    "Réduire la longueur aux résultats",
    "Calculer uniquement la coupe sélectionnée",
    "Ajouter une cote",
    "Supprimer toutes les cotes",
    "Rapport des coupes verticales",
  ],
  tables: [
    "Tableaux de champ des coupes verticales",
    "Tableaux d’exposition des coupes verticales",
    "Graphiques dans le rapport Word",
    "Alias des tableaux et graphiques",
  ],
  report: [
    "Choisir un modèle Word",
    "Alias personnalisés",
    "Copier les alias",
    "Coller les alias",
    "Restaurer les alias",
    "Liste des 14 alias standard",
    "Rapport Word depuis le modèle",
  ],
  cad: [
    "Exporter DWG",
    "Importer DWG",
    "Exporter DXF",
    "Importer DXF",
    "Mise en page CAO avancée",
  ],
  "3d": ["Volumes de sécurité EMF 3D"],
  calibrate: [
    "Calibrer avec deux lignes",
    "Calibrer avec une ligne et un point",
    "Repères de calibration au centre",
    "Actualiser la pyramide de tuiles",
  ],
  settings: [
    "Liste des propriétaires",
    "Dossier des diagrammes d’antennes",
    "Coordonnées en degrés / minutes / secondes",
    "Fonds de carte personnalisés",
    "Cache des cartes",
    "Paramètres proxy",
    "Langue de l’interface",
  ],
};

export default function FeatureTools({ section }: { section: Section }) {
  const a = usePlanner(),
    { project: p, source, commit } = a;
  const building = p.buildings.find((b) => b.id === a.selectedObject);
  const line = p.lines.find((l) => l.id === a.selectedObject);
  const extraActions = useExtraActions();
  const geometryActions = useGeometryActions();
  const rotate = (delta: number) => {
    a.rotateSourcePattern(delta);
  };
  const actions: Record<string, { run: () => void; enabled: boolean }> = {
    ...extraActions,
    "Calculer les ouvertures de toutes les sources": {
      enabled: p.sources.some(hasSourcePatterns),
      run: () =>
        commit(
          (v) => ({ ...v, sources: v.sources.map(fillSourceBeamWidths) }),
          "Ouvertures H/V recalculées",
          false,
        ),
    },
    "Calculer les ouvertures de la source sélectionnée": {
      enabled: hasSourcePatterns(source),
      run: () =>
        commit(
          (v) => ({
            ...v,
            sources: v.sources.map((s) =>
              s.id === source?.id ? fillSourceBeamWidths(s) : s,
            ),
          }),
          "Ouvertures H/V de la source recalculées",
          false,
        ),
    },
    "Diagramme V : −1°": {
      run: () => rotate(-1),
      enabled: !!source?.pattern_v,
    },
    "Diagramme V : +1°": { run: () => rotate(1), enabled: !!source?.pattern_v },
    "Dupliquer le bâtiment": {
      run: () => {
        if (!building) return;
        const copy = {
          ...building,
          id: uid(),
          name: `${building.name} (copie)`,
          points: building.points.map(
            ([x, y]) => [x + 5, y + 5] as [number, number],
          ),
        };
        commit((p) => ({ ...p, buildings: [...p.buildings, copy] }));
        a.setSelectedObject(copy.id);
      },
      enabled: !!building,
    },
    "Altitude des bases depuis le terrain": {
      run: () =>
        commit((p) => ({
          ...p,
          buildings: p.buildings.map((b) => ({
            ...b,
            foundation_m: b.points.length
              ? terrainHeight(p, b.points[0][0], b.points[0][1])
              : 0,
          })),
        })),
      enabled: p.buildings.length > 0 && p.terrain_points.length > 0,
    },
    "Altitude de la base sélectionnée": {
      run: () =>
        commit((p) => ({
          ...p,
          buildings: p.buildings.map((b) =>
            b.id === building?.id
              ? {
                  ...b,
                  foundation_m: terrainHeight(
                    p,
                    b.points[0][0],
                    b.points[0][1],
                  ),
                }
              : b,
          ),
        })),
      enabled: !!building?.points.length && p.terrain_points.length > 0,
    },
    "Placer à l’arrière": {
      run: () =>
        building &&
        commit((p) => ({
          ...p,
          buildings: [
            building,
            ...p.buildings.filter((b) => b.id !== building.id),
          ],
        })),
      enabled: !!building,
    },
    "Dupliquer l’objet": {
      run: () => {
        if (!line) return;
        const copy = {
          ...line,
          id: uid(),
          name: `${line.name} (copie)`,
          points: line.points.map(
            ([x, y]) => [x + 5, y + 5] as [number, number],
          ),
        };
        commit((p) => ({ ...p, lines: [...p.lines, copy] }));
        a.setSelectedObject(copy.id);
      },
      enabled: !!line,
    },
    "Trier par hauteur": {
      run: () =>
        commit((p) => ({
          ...p,
          horizontal_cuts: [...p.horizontal_cuts].sort(
            (a, b) => a.height_m - b.height_m,
          ),
        })),
      enabled: p.horizontal_cuts.length > 1,
    },
    "Couleurs distinctes": {
      run: () =>
        commit(
          (p) => ({
            ...p,
            horizontal_cuts: p.horizontal_cuts.map((c, i) => ({
              ...c,
              color: COLORS[i % COLORS.length],
            })),
          }),
          "Couleurs de contour modifiées",
          false,
        ),
      enabled: p.horizontal_cuts.length > 0,
    },
    "Couleur unique": {
      run: () =>
        commit(
          (p) => ({
            ...p,
            horizontal_cuts: p.horizontal_cuts.map((c) => ({
              ...c,
              color: p.horizontal_cuts[0].color,
            })),
          }),
          "Couleurs de contour modifiées",
          false,
        ),
      enabled: p.horizontal_cuts.length > 0,
    },
    "Trier par azimut": {
      run: () =>
        commit((p) => ({
          ...p,
          vertical_cuts: [...p.vertical_cuts].sort(
            (a, b) => a.azimuth_deg - b.azimuth_deg,
          ),
        })),
      enabled: p.vertical_cuts.length > 1,
    },
    ...geometryActions,
  };
  return (
    <div
      className="feature-tools"
      role="toolbar"
      aria-label="Outils complémentaires"
    >
      <div className="feature-tools-buttons">
        {EXTRA_TOOLS[section].map((label) => {
          const action = PENDING_COMMANDS[label] ? undefined : actions[label];
          const reason = PENDING_COMMANDS[label]
            ? `${label} — ${PENDING_COMMANDS[label]}`
            : action
              ? action.enabled
                ? label
                : `${label} — sélectionnez les données nécessaires.`
              : `${label} — fonction pas encore implémentée.`;
          return (
            <span
              key={label}
              title={reason}
              className={action ? "" : "pending-command"}
            >
              <button
                disabled={!action?.enabled}
                onClick={action?.run}
                title={reason}
                aria-label={label}
              >
                <OriginalIcon label={label} section={section}>
                  {(() => {
                    const Icon = iconFor(label);
                    return <Icon size={17} />;
                  })()}
                </OriginalIcon>
              </button>
            </span>
          );
        })}
      </div>
    </div>
  );
}
function iconFor(label: string) {
  if (/Bibliothèque/.test(label)) return /câbles/.test(label) ? Cable : Library;
  if (/Copier|Dupliquer/.test(label)) return Copy;
  if (/Supprimer/.test(label)) return Trash2;
  if (/Ajouter|intervalle|plage/.test(label)) return Plus;
  if (/Trier|Monter|Descendre/.test(label)) return ListOrdered;
  if (/Couleur/.test(label)) return Palette;
  if (/Diagramme V/.test(label)) return RotateCw;
  if (/Scinder|Coupe verticale/.test(label)) return Scissors;
  if (/Décaler|au centre|Origine sur/.test(label)) return Move;
  if (/Centrer|Coordonnées/.test(label)) return MapPin;
  if (/Rapport|Word/.test(label)) return FileText;
  if (/nœud|sommet|Éditeur|Équarrir/.test(label)) return PenLine;
  if (/Enveloppe|symétrique/.test(label)) return Waves;
  if (/maxima|hauteur|longueur/.test(label)) return Maximize;
  if (/Préréglage|OpenStreetMap|carte/.test(label)) return Globe;
  if (/terrain|relief|Relief|Courbe|Altitude/.test(label)) return Layers;
  if (/Tableaux|triangulation|grille/.test(label)) return Grid2X2;
  return Settings;
}
