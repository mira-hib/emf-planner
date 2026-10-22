import names from "../data/icon-index.json";
import type { Section } from "../types";
const available = new Set(names);
const exact: Record<string, string> = {
  Fichier: "Form1.cmdFile",
  "Enregistrer le projet": "Form1.cmdSAVE",
  Paramètres: "Form1.ToolStripDropDownButton1",
  "Bibliothèque d’antennes": "UC_PRTO.cmdLoadDN2",
  "Éditeur de diagramme": "UC_PRTO.cmdEditorDN",
  "Enveloppe de diagrammes": "UC_PRTO.cmdEnvelope",
  "Antenne symétrique ITU-R F.699": "UC_PRTO.cmdApert",
  "Diagramme V : −1°": "UC_PRTO.cmdVertPatternDown",
  "Diagramme V : +1°": "UC_PRTO.cmdVertPatternUp",
  "Calcul inverse de puissance": "UC_PRTO.cmdCalcP",
  "Calculer les ouvertures de toutes les sources": "UC_PRTO.cmdFillWidthDN",
  "Calculer les ouvertures de la source sélectionnée":
    "UC_PRTO.cmdFillWidthDnOne",
  "Dupliquer la source": "UC_PRTO.cmdCopySelectedTX",
  "Monter la source": "UC_PRTO.cmdUp",
  "Descendre la source": "UC_PRTO.cmdDown",
  "Placer les sources au début": "UC_PRTO.cmdMoveTop",
  "Placer les sources à la fin": "UC_PRTO.cmdMoveBottom",
  "Copier les sources sélectionnées": "UC_PRTO.cmdCopyToBuffer",
  "Coller les sources": "UC_PRTO.cmdPasteFromBuffer",
  "Activer ou désactiver toutes les sources": "UC_PRTO.cmdCheckUncheck",
  "Exporter les tableaux Excel": "UC_PRTO.cmdExcel",
  "Exporter le rapport Word": "UC_PRTO.cmdWord",
  "Importer les bâtiments KML": "UC_Buildings.cmdImportBuildingPolygons",
  "Altitude des bases depuis le terrain": "UC_Buildings.cmdAutoFundation",
  "Altitude de la base sélectionnée": "UC_Buildings.cmdAutoFundationOneBLD",
  "Équarrir les angles": "UC_Buildings.cmdSquareBLD",
  "Dupliquer le bâtiment": "UC_Buildings.cmdAddCopyBuilding",
  "Placer à l’arrière": "UC_Buildings.cmdLayerBackward",
  "Bâtiments OpenStreetMap": "UC_Buildings.cmdGetBldFromCloud",
  "Courbe de niveau": "UC_Relief.cmdAddLine",
  "Afficher la triangulation": "UC_Relief.cmdTriangle",
  "Relief DEM / SRTM": "UC_Relief.cmdSRTM",
  "Scinder une ligne": "UC_Lines.cmdSplitNode",
  "Dupliquer l’objet": "UC_Lines.cmdAddCopyObject",
  "Choisir une icône": "UC_Lines.cmdIcon",
  "Routes et clôtures OpenStreetMap": "UC_Lines.cmdGetLinesFromCloud",
  "Points sur l’azimut des antennes": "UC_ControlPoints.cmdMovePointsToAzimuth",
  "Monter le groupe": "UC_ControlPoints.cmdUp",
  "Descendre le groupe": "UC_ControlPoints.cmdDown",
  "Calcul d’un point par plage de hauteurs": "UC_ControlPoints.cmdCalcOnePoint",
  "Calculer tous les points": "UC_ControlPoints.cmdCalcAllPoints",
  "Ajouter un point au centre": "UC_ControlPoints.cmdAddControlPoint",
  "Trier par hauteur": "UC_HorizPlanes.cmdSORT",
  "Couleurs distinctes": "UC_HorizPlanes.cmdDifferentColors",
  "Couleur unique": "UC_HorizPlanes.cmdSameColor",
  "Styles de ligne distincts": "UC_HorizPlanes.cmdDifferentLineTypes",
  "Hauteurs par intervalle": "UC_HorizPlanes.cmdAutoFill3",
  "Hauteurs des maxima verticaux": "UC_HorizPlanes.cmdAutoFill2",
  "Maxima verticaux avec décalage": "UC_HorizPlanes.cmdAutoFill4",
  "Ajouter les hauteurs des antennes": "UC_HorizPlanes.cmdAutoFill1",
  "Coupe verticale le long de la règle": "UC_HorizPlanes.cmdAddVertPlane",
  "Calculer avec la grille 1": "UC_HorizPlanes.cmdStart0",
  "Calculer avec la grille 2": "UC_HorizPlanes.cmdStart1",
  "Rapport des coupes horizontales": "UC_HorizPlanes.cmdWord1",
  "Coupes selon les azimuts des antennes": "UC_VertPlanes.cmdAutoFill1",
  "Trier par azimut": "UC_VertPlanes.cmdSORT",
  "Coupes par intervalle d’angles": "UC_VertPlanes.cmdAddRangeAndStep",
  "Réduire la hauteur aux résultats": "UC_VertPlanes.cmdAdjustUp",
  "Réduire la longueur aux résultats": "UC_VertPlanes.cmdAdjustLeft",
  "Calculer uniquement la coupe sélectionnée": "UC_VertPlanes.cmdStartOne0",
  "Rapport des coupes verticales": "UC_VertPlanes.cmdWord",
  "Supprimer toutes les cotes": "UC_VertPlanes.cmdClearAllMesurePoints",
  "Calculer toutes les coupes verticales": "UC_VertPlanes.cmdStart0",
};
export default function OriginalIcon({
  label,
  section,
  children,
}: {
  label: string;
  section?: Section;
  children?: React.ReactNode;
}) {
  const prefix =
    section === "buildings"
      ? "UC_Buildings"
      : section === "terrain"
        ? "UC_Relief"
        : "UC_Lines";
  const command =
    exact[label] ??
    (/Ajouter un (sommet|nœud)/.test(label)
      ? `${prefix}.cmdAddNode`
      : /Supprimer un (sommet|nœud)/.test(label)
        ? `${prefix}.cmdDeleteNode`
        : label.startsWith("Préréglage : ")
          ? `UC_Limits.cmd${({ ICNIRP: "ICNIRP", "Union européenne": "EU", "États-Unis": "USA", Suisse: "Switzerland", "Royaume-Uni": "UK", Belgique: "Belgium", Italie: "Italy", Chine: "China", Russie: "Russia", Pologne: "Poland", Inde: "India", Lituanie: "Lithuania", Slovénie: "Slovenia", Grèce: "Greece", Croatie: "Croatia", Bulgarie: "Bulgaria" } as Record<string, string>)[label.slice(13)]}`
          : undefined);
  const file = command ? `${command}.png` : "";
  return available.has(file) ? (
    <img
      className="original-icon"
      src={`/original-icons/${file}`}
      alt=""
      draggable={false}
    />
  ) : (
    <>{children}</>
  );
}
