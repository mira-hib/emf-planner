# EMF Planner — Clone

Application Windows Tauri + React reproduisant les principaux écrans
d’EMFPlanner 7.1 à partir de son interface observée et du code .NET récupéré en
mémoire. Les fonctions secondaires non implémentées sont visibles en gris avec
une explication au survol.

## Utilisation sous Windows

Lancer `src-tauri/target/release/emf-planner.exe`, ou :

```powershell
.\scripts\Start-EmfPlanner.ps1
```

Le menu **Fichier** ouvre les projets d’origine `.xemf`, les projets du clone
`.emfp`/JSON et une démonstration. L’enregistrement produit un `.emfp` ; il ne
réécrit pas le format binaire de l’original.

## Reprendre sur Mac

```bash
git pull origin main
npm ci
npm run tauri dev
```

Utiliser Node.js 24 et Rust stable avec les outils de développement Xcode.
Les tests métier et leurs références originales sont versionnés et ne
nécessitent pas les outils Windows : `npm test` et `npm run benchmark`.
Pour les tests d’interface : `npx playwright install chromium`, puis
`npm run test:ui` (Chromium sur Mac, Edge sur ce PC Windows).

Le code C# récupéré est dans `reference/original-decompiled/`. Voir
[la passation Mac](memory-bank/mac-handoff.md) pour les archives d’analyse,
les empreintes, les fonctions grisées et les prochaines étapes.

## Interface disponible

- 16 rubriques : sources, limites, origine, positions d’antennes, bâtiments,
  terrain, lignes, points de contrôle, coupes H/V, tableaux, rapports, CAO, 3D,
  calibration et paramètres.
- Tableaux éditables, copie/suppression, annuler/rétablir, raccourcis et copie de
  travail locale.
- Diagrammes d’antennes MSI, calcul EIRP et facteurs par jeu de limites.
- Icônes de l’original, grille compacte et panneaux redimensionnables.
- Bibliothèque/éditeur d’antennes, enveloppes, génération symétrique et calculateur de câbles avec bretelles/connecteurs.
- Calculs aux points avec plusieurs hauteurs et matrice de contributions.
- Coupes horizontales/verticales et contours des quatre jeux de limites actifs.
- Plan hors ligne, fonds de carte en ligne, bâtiments, relief et annotations.
- Édition des sommets, bâtiments KML, courbes de niveau, triangulation, gestion des groupes et coupes par intervalles.
- Rapports Word/Excel/PDF, plan CAO SVG/PDF/DXF et scène 3D interactive.
- Surface d’exposition à 100 % en 3D, avec zone et pas de maillage réglables.

Le [détail des fonctions disponibles et grisées](memory-bank/clone-interface.md)
décrit les limites actuelles. L’interface n’est pas encore une reproduction
pixel par pixel et l’ensemble du comportement de l’original n’est pas validé.

## Vérifications

```powershell
npm run build
npm test
npm run test:ui
npm run benchmark
```

Les tests couvrent les calculs, le chargement du projet original, l’édition,
l’enregistrement, les deux types de coupes, les vues CAO/3D et les exports.
Les 15 résultats point/hauteur capturés sur le projet livré concordent avec le
clone à moins de `1.1e-6 V/m` ; cette mesure concerne ce seul scénario.
Les tests du maillage 3D vérifient sa convergence vers une sphère analytique.
Voir [l’affinage de l’interface](memory-bank/interface-refinement.md).

## Développement

Prérequis : Node.js, Rust/MSVC, WebView2 et les dépendances Tauri 2.

```powershell
npm ci
npm run tauri dev
npm run tauri build -- --no-bundle
```

La chaîne portable de cette machine est conservée dans `.tools/` (hors Git).
`scripts/Start-EmfPlanner.ps1 -Development` la configure puis démarre Tauri.

| Emplacement | Rôle |
|---|---|
| `src/components/` | Écrans React, carte SVG/Leaflet, CAO, scène Three.js |
| `src/lib/engine.ts` | Moteur de calcul utilisé par cette interface |
| `src/lib/calculation.worker.ts` | Calculs annulables en arrière-plan |
| `src/lib/import-xemf.ts`, `nrbf.ts` | Lecture de données du format original |
| `src/lib/files.ts` | Fichiers natifs Tauri, Excel, Word, PDF et DXF |
| `src/components/FeatureTools.tsx` | Commandes complémentaires et états désactivés |
| `src-tauri/` | Application Windows ; anciens modules de calcul Rust conservés |
| `tests/` | Cas numériques et parcours d’interface Playwright |
| `memory-bank/` | Analyse, preuves, décisions et état du clone |

## Analyse de l’original

L’analyse statique sur Mac était bloquée par Enigma. L’exécution Windows et la
capture mémoire ont ensuite permis de récupérer 137 fichiers C#, dont les
calculs EMF et les diagrammes d’antennes. Les 3 106 corps CIL du module principal
sont restés identiques après les scénarios de calcul, CAO/3D et Office.

Voir le [memory bank](memory-bank/README.md), le
[rapport dynamique Windows](memory-bank/windows-dynamic-analysis.md) et les
[scénarios de référence](memory-bank/windows-scenario-validation.md).

Projet éducatif de recherche et de reconstruction logicielle.
