# EMFPlanner — Memory Bank

Base de connaissances pour le projet de reverse engineering et reproduction d'EMFPlanner 7.1.

## Index

| Fichier | Description |
|---------|-------------|
| `full-kit-implementation.md` | Reprise en cours après coupure : nouveaux modules, preuves, vérifications et tâches restantes |
| `build-validation.md` | Builds desktop x64/ARM64 et résultats du test kit regroupés dans `test validation` |
| `map-providers.md` | Catalogue natif complet des cartes, Google/Bing/Yandex, configuration et disponibilité vérifiée |
| `architecture.md` | Architecture du logiciel original (PE, .NET, ODA, Enigma) |
| `features.md` | Fonctions disponibles, partielles et grisées du clone actuel |
| `menu-reproduction.md` | **Ordre des 16 rubriques ; Sources, EMF Limits, Origin and Grid et Antenna Locations repris, écarts et validations** |
| `castelfranco-fidelity.md` | Comparaison directe Windows de l’antenne, CAO et 3D ; corrections et limites vérifiées |
| `interface-refinement.md` | Affinage visuel, nouveaux outils, surface EMF 3D et validation |
| `clone-interface.md` | **Reprise du clone Windows : 16 rubriques, commandes secondaires, moteur, vérifications et lancement** |
| `mac-handoff.md` | **Reprise sur Mac : commandes, code C# versionné et archive complète des artefacts Windows** |
| `stack-decision.md` | Choix technologiques : Rust + Tauri + React |
| `progress.md` | Suivi d'avancement complet |
| `test-kit-validation.md` | **Campagne du kit : corrections calcul/interface, 1 800 références CalcPPE, couverture et limites** |
| `formulas.md` | Formules EMF extraites du manuel (ITU/ICNIRP) |
| `analysis-swigcore.md` | Analyse .NET SwigCoreMgd (1454 classes ODA) |
| `analysis-swigdb.md` | Analyse .NET SwigDbMgd (866 classes ODA) |
| `analysis-native-strings.md` | Strings natifs + données du manuel PDF |
| `rea-deep-analysis.md` | Analyse Ghidra/REA : Enigma Protector identifié |
| `rea-exhaustive-analysis.md` | 10 pistes épuisées, 3 découvertes de données |
| `rea-data-extraction.md` | Données extraites : 36 câbles, 13 cartes, templates |
| `guide-windows-unpacking.md` | Guide pas-à-pas : dump mémoire sur Windows (x64dbg, ExtremeDumper, dnSpy) |
| `windows-toolchain.md` | Installation Windows effective : REA, Ghidra, débogueur, outils .NET, commandes et vérifications |
| `windows-dynamic-analysis.md` | **Résultat actuel : payload .NET récupéré, 137 fichiers C# exportés, code métier lisible** |
| `windows-scenario-validation.md` | Calculs, coupes, CAO/3D et exports testés ; comparaison mémoire avant/après et 15 références numériques |

## Builds et validation du kit — 24 octobre

Tous les builds desktop Windows/Linux/macOS x64 et ARM64 et le web sont produits.
Les six variantes passent chacune **78 tests métier du kit et neuf parcours
navigateur** ; rapports et builds sont regroupés dans `test validation/2026-10-24/`.
La couverture du guide reste partielle (65/216 scénarios ; 151 non exécutés).
Voir [build-validation.md](build-validation.md) pour la provenance, les formats,
les commandes et l’archive de livraison. `main` et l’index initial sont conservés.

## Reprise du 22 octobre — Antenna Locations et cartes

Rubrique Antenna Locations reprise : sept colonnes, cinq commandes natives,
sélection multiple, déplacements limités à la sélection, conversion WM native,
accrochage et déplacement sur cartes locale/Leaflet. Lire
[menu-reproduction.md](menu-reproduction.md) et
[l’audit Antenna Locations](../reference/antenna-locations-native-audit-2026-10-22.md).
Les quatorze choix de cartes de la capture utilisateur sont désormais présents ;
voir [map-providers.md](map-providers.md). Prochaine rubrique : **Buildings**.
La sélection de cellules/colonnes et le réordonnancement sont raccordés. Les
interactions clavier WinForms et la comparaison visuelle native exhaustive
restent non vérifiées ; ne pas déclarer une reproduction pixel par pixel validée.

## Reprise du 22 octobre — campagne du kit

Le kit est maintenant intégré dans `tests/test-kit.test.ts` et
`tests/browser/test-kit.spec.ts`. Les premiers écarts calcul/interface sont corrigés ;
voir [test-kit-validation.md](test-kit-validation.md) et le
[bilan détaillé](../reference/test-kit-audit-2026-10-22.md). Les 216 scénarios ne sont
pas tous validés. La nouvelle référence isolée de `CalcPPE` couvre 1 800 cas ;
les 15 valeurs précédemment observées dans l’application restent conservées.

## Reprise précédente — Origin and Grid

Origine affichée en lecture seule et déplaçable, coordonnées du site indépendantes
pour le rapport, quatre dimensions du rectangle de calcul et poignées sur la carte,
grille graduée, deux résolutions horizontales natives en divisions et règle à deux
extrémités. Les changements d’origine préservent les positions géographiques et les
coins Web Mercator de la zone. **81 tests métier et 28 parcours Edge validés** ;
voir `menu-reproduction.md` et
[`origin-grid-native-audit-2026-10-22.md`](../reference/origin-grid-native-audit-2026-10-22.md).

La release Windows a ensuite été lancée à la demande de l’utilisateur : fenêtre
« EMF Planner — Clone » présente et processus répondant. Métadonnées dans
[`origin-grid-build.json`](../reference/origin-grid-build.json). La prochaine
rubrique est **Antenna Locations**.

Le kit fourni dans `test/emf_test_kit/` et son archive sont conservés dans le
dépôt ; sa campagne automatisée est désormais décrite dans `test-kit-validation.md`.

## Reprise précédente — EMF Limits

La rubrique Limites EMF utilise désormais le tableau commun natif, les quatre
descriptions/couleurs/facteurs, le graphique logarithmique et les 16 préréglages
historiques exacts d’EMFPlanner 7.1. Voir `menu-reproduction.md` et
[`limits-native-audit-2026-10-22.md`](../reference/limits-native-audit-2026-10-22.md).
Le projet Castelfranco conserve ses valeurs et les 15 références numériques.

## État actuel — après exécution Windows

**Code métier récupéré en mémoire sous Windows et décompilé** — assemblage
`EMFPlanner` identifié par REA, 137 fichiers C# exportés. Les algorithmes `CalcSZZ`
et `ClassDN` sont lisibles ; voir `windows-dynamic-analysis.md` pour les artefacts,
les Evidence et les limites.

Les scénarios Windows ont ensuite confirmé que les 3 106 corps CIL du module
principal restent identiques après calculs, coupes, CAO/3D et exports Office/DWG.
Ils ont aussi chargé 16 assemblages supplémentaires, maintenant conservés dans
une seconde capture. Voir `windows-scenario-validation.md`.

Le blocage Enigma décrit dans les anciennes analyses concerne le fichier protégé
sur disque. Le clone a ensuite été repris : 16 rubriques, import des projets
originaux, points, coupes, CAO/3D et exports. Les commandes non implémentées sont
visibles en gris, selon la demande de l’utilisateur.

L’affinage ajoute les icônes d’origine, panneaux redimensionnables,
bibliothèques/éditeur et câbles, sommets/KML, courbes de niveau, groupes,
intervalles de coupes et surface EMF 3D. Voir `interface-refinement.md`.

La comparaison des 15 résultats de référence passe avec un écart de champ
maximal inférieur à 0,000001 V/m. Ce résultat concerne un seul projet et ne
valide pas toutes les branches. Voir **`clone-interface.md`** pour continuer.
# Reprise après coupures

Lire d'abord [resume-after-powercuts.md](resume-after-powercuts.md) pour le dernier
état des validations, builds, charge ROB-05 et lancement Windows.

