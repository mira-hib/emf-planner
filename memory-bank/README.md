# EMFPlanner — Memory Bank

Base de connaissances pour le projet de reverse engineering et reproduction d'EMFPlanner 7.1.

## Index

| Fichier | Description |
|---------|-------------|
| `architecture.md` | Architecture du logiciel original (PE, .NET, ODA, Enigma) |
| `features.md` | Fonctions disponibles, partielles et grisées du clone actuel |
| `menu-reproduction.md` | **Ordre des 16 rubriques ; lots essentiels Sources, EMF Limits et Origin and Grid intégrés, écarts et validations** |
| `castelfranco-fidelity.md` | Comparaison directe Windows de l’antenne, CAO et 3D ; corrections et limites vérifiées |
| `interface-refinement.md` | Affinage visuel, nouveaux outils, surface EMF 3D et validation |
| `clone-interface.md` | **Reprise du clone Windows : 16 rubriques, commandes secondaires, moteur, vérifications et lancement** |
| `mac-handoff.md` | **Reprise sur Mac : commandes, code C# versionné et archive complète des artefacts Windows** |
| `stack-decision.md` | Choix technologiques : Rust + Tauri + React |
| `progress.md` | Suivi d'avancement complet |
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

## Reprise du 22 octobre — Origin and Grid

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
dépôt ; ses scénarios ne sont pas encore exécutés ni intégrés à la suite automatisée.

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
