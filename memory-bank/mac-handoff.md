# Passation Windows → Mac — 20 octobre 2026

## Démarrer le clone

```bash
git pull origin main
npm ci
npm run tauri dev
```

Prérequis : Node.js 24, Rust stable et outils de développement Xcode.
Le clone Tauri se compile sur le Mac ; l’exécutable fourni dans l’archive est
une compilation Windows. L’application originale doit être manipulée sous
Windows pour de nouveaux scénarios dynamiques.

## Tout ce qui est fourni

Dans Git :

- Clone React/Tauri, import .xemf, moteur, vues, exports et tests.
- `reference/original-decompiled/` : export ILSpy, 137 fichiers C# avec ressources
  et projet généré. Commencer par `EMFPlanner/CalcSZZ.cs`, `ClassDN.cs` et les `UC_*.cs`.
- `tests/fixtures/` : projet original et 15 résultats observés, disponibles
  immédiatement pour `npm test` et `npm run benchmark`.
- Memory bank, scripts de capture/reconstruction et scripts de passation.
- Icônes d’origine, 36 câbles, quatre modèles d’antennes de référence et nouveaux outils versionnés.

Dans [la release de passation](https://github.com/mira-hib/emf-planner/releases/tag/windows-handoff-2026-10-20) :

- Archive originale RAR, installateur et application extraite avec leurs données.
- Assemblages PE/CLI reconstruits avant et après les scénarios Windows.
- Export C#, ressources et résumés de reconstruction.
- Inventaires REA des membres et comparaison des 3 106 corps CIL.
- Projets de scénarios, sorties de référence, captures et exports de tests.
- Exécutable Windows du clone compilé en mode release.

Cette grande archive conserve la compilation antérieure à l’affinage. Le code
actuel se récupère avec `git pull`. Le nouvel exécutable est fourni séparément
sur cette même release ; voir `reference/interface-build.json` pour sa version
et son empreinte. Ne pas remplacer le code actuel par celui d’une ancienne archive.

[Nouvel exécutable Windows et captures](https://github.com/mira-hib/emf-planner/releases/download/windows-handoff-2026-10-20/emf-planner-windows-interface-6f979729.zip) :
3 690 894 octets, sources `6f979729839e8621f4879dbf1eec218b0c9338fa`.
SHA-256 : `16e354c4f2055d7a8de329367046485e0ef9120d63764286c2cd7ec723dae913`.
Le téléchargement public a été relu et son empreinte vérifiée.

Archive : **228 710 977 octets**, **389 fichiers**.
SHA-256 : `7ac5fccc81e45e3969225087292b60ecabbc2e160eacc08d403997efe2d31b6b`.
La copie de contrôle est dans `reference/handoff-summary.json`.
Publication vérifiée : archive accessible (HTTP 200) et empreinte SHA-256
retournée par GitHub identique à la copie locale. Les liens et cette preuve
sont conservés dans `reference/handoff-release.json`.

Téléchargement et restauration depuis la racine du dépôt :

```bash
curl -L --fail 'https://github.com/mira-hib/emf-planner/releases/download/windows-handoff-2026-10-20/emf-planner-windows-handoff-2026-10-20.zip' -o /tmp/emf-planner-handoff.zip
python3 scripts/restore_handoff.py /tmp/emf-planner-handoff.zip
```

Le script vérifie le SHA-256 de l’archive et de chacun des fichiers avant toute
extraction. `--verify-only` permet de contrôler sans restaurer. Les fichiers sont
remis aux chemins `extracted/` et `.tools/` utilisés dans les rapports Windows.
Le script n’exécute aucun binaire de l’archive.

Les dumps RAM bruts restent sur le PC : les images PE/CLI, métadonnées,
ressources et scripts nécessaires à la reprise sont inclus. Les outils Windows
installés et leurs caches ne sont pas transférés ; ils ne fonctionnent pas sur
Mac et peuvent être réinstallés avec les scripts fournis si besoin.

## État exact à reprendre

L’utilisateur demande la même interface et les mêmes fonctions que l’original,
avec les commandes non implémentées **visibles mais grisées**. Les 16 rubriques
existent. Les outils complémentaires sont centralisés dans
`src/components/FeatureTools.tsx` ; ne pas remplacer un bouton grisé par une
commande factice.

Lire [clone-interface.md](clone-interface.md) et [features.md](features.md) pour
le détail. Le moteur qui pilote ces écrans est `src/lib/engine.ts`, utilisé par
un Web Worker. Les anciens modules Rust ne pilotent plus cette interface.

Vérifications après affinage : build TypeScript/Vite, **21 tests métier**,
**14 parcours d’interface Edge**, compilation **Tauri Windows release**. Les
rapports Word/PDF/Excel ont été téléchargés et leur contenu contrôlé, y compris
la sélection « points seuls » / « sources et points ».

La comparaison des 15 références donne un écart maximal de champ
`9.945531114219364e-7 V/m`, sous `1.1e-6`. Ce seul scénario ne prouve pas la
concordance de toutes les branches et bibliothèques natives de l’original.

Sur Mac :

```bash
npm test
npm run benchmark
npx playwright install chromium
npm run test:ui
```

Les tests d’interface démarrent Vite si nécessaire et utilisent Chromium sur
Mac, Edge sur Windows. La validation d’exécution sur Mac reste à faire.

Les bibliothèques/éditeur d’antennes, les câbles, sommets, groupes, courbes de
niveau, intervalles de coupes et surface d’exposition 3D sont raccordés.
Lire [interface-refinement.md](interface-refinement.md). Poursuivre par les
outils gris : DEM/SRTM, génération avancée des coupes, rapports illustrés,
modèles Word, DWG et calibration par références.
Les écrans fonctionnels peuvent ensuite être comparés plus finement à
l’original pour améliorer la fidélité visuelle.

## Mise à jour du 22 octobre — menu par menu

La priorité utilisateur est maintenant la reproduction complète de chaque rubrique. Lire d’abord [menu-reproduction.md](menu-reproduction.md), puis [l’audit Sources](../reference/sources-native-audit-2026-10-22.md). Un premier lot essentiel de Sources est intégré ; la sélection de cellules/colonnes, le visualiseur complet et les sorties Word/Excel natives restent à terminer avant de déclarer cette rubrique achevée.

Validation actuelle : 63 tests unitaires, 21 parcours Edge et compilation TypeScript/Vite réussis. Utiliser `npm ci`, `npm test`, puis `npm run tauri dev` sur Mac. Ne pas confondre le tampon de sources JSON interne avec le format sérialisé natif `buffer.tmp`. Les nouveaux champs optionnels `frequency_end_mhz` et `feeder.include_connectors` doivent être conservés dans les sauvegardes et les copies.
