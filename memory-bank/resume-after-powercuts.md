# Reprise prioritaire après les coupures — 25 octobre 2026

La demande reste active : toutes les fonctionnalités du kit, comparaison REA,
builds Windows/Linux/macOS x64 et ARM64 + web, résultats dans `test validation`,
et lancement de la dernière version Windows. Ne pas remplacer cette demande
par une simple réponse de statut, ni assimiler des tests de code aux 216 scénarios.

## Résultats acquis

- **236/236 tests de code**, journal `.tools/full-unit-final.log`.
- **65/65 parcours UI**, journal `.tools/full-browser-final.log`.
- Les suites propres au kit comptent **143 tests de code et 28 parcours UI**.
  Windows x64, Windows ARM64 et Linux x64/ARM64 les ont passées en CI avec leurs
  builds et encodeurs natifs. Voir campagne intermédiaire 40237116754.
- Les deux projets natifs calculent leurs points existants et leur première
  coupe H/V à leur matrice importée : `native-samples-v2/results.json`.
- U3D, PDF3D et DWG R2000 réels ; géométrie 100x50mm relue par LibreDWG.
- Les 216 textes exacts sont catalogués. Index actuel : 149 références partielles,
  67 sans référence dans les suites. Ce ne sont pas 149 validations complètes.
- `reference/test-kit-full-audit-2026-10-25.md` détaille limites et écarts.

## Builds actuellement en cours

Branche isolée `build-validation/20261025-200009`, dernier commit
`ef431a6701791a3ddf6b7b7000d3a07620ed4c03`.
Run : https://github.com/mira-hib/emf-planner/actions/runs/40238835421

Six plateformes + une charge ROB-05 complète. Le run précédent a révélé deux
tests macOS utilisant Ctrl au lieu de Cmd. Les tests sont corrigés ; aucune
modification fonctionnelle de l'application ne vient de ce correctif.
Linux/macOS : le convertisseur ancien U3D compile avec C99/C++14, deux retouches
de compatibilité des en-têtes macOS, et ses vrais plug-ins dynamiques livrés.
Ces correctifs ont déjà permis les deux builds Linux. Attendre le nouveau bilan.

Commandes de suivi, sans afficher les identifiants Git :

```powershell
. ./scripts/Use-ReaTools.ps1
python .tools/sync-validation-ci.py --campaign 2026-10-25 --status-only
python .tools/sync-validation-ci.py --campaign 2026-10-25
```

Le téléchargement reprend par morceaux nommés avec l'ID d'artefact. Les archives
sont vérifiées par SHA256. Chaque plateforme garde `download-provenance.json`.
Les anciens fichiers sont déplacés dans `attempts`, jamais comptés comme récents.
Le collecteur daté `scripts/collect-validation.py` conserve aussi les charges,
exemples natifs et audits. Ne pas écraser la campagne du 24 octobre.

## Windows lancé

Build CI Windows x64 du commit `7a54ccc031415bd4558665de84d3a3be23c8ed15`.
Les 145 fichiers applicatifs ont été comparés au travail courant par leur blob
Git : aucune différence. Cette version est donc à jour fonctionnellement.

Lancement effectué depuis
`test validation/2026-10-25/launched/Windows-x64-7a54ccc/emf-planner.exe`, avec
son dossier `native` adjacent. PID observé 28096, processus actif après 3 s.
SHA256 : `75c4de7421f55ca2da8c71c544d6b5947c55609aa5bb741b7cec873537adf624`.
`launch-result.json` conserve la preuve. Le contrôle visuel n'est pas établi :
le dernier appel Windows Computer Use a perdu son pipe natif après l'interruption.
Les PIDs et anciens handles ne sont pas réutilisables après une nouvelle coupure.

## ROB-05

`stress-rob05-final` est une campagne fraîche ; elle reprend uniquement ses
propres reçus de même projet et même empreinte du code. Dernier état observé :
29/50 coupes, zéro échec. Relancer si ses processus ont disparu :

```powershell
. ./scripts/Use-ReaTools.ps1
python scripts/prepare-kit-stress.py
node --import tsx scripts/validate-kit-stress.mts 'test validation/2026-10-25/stress-rob05-final'
```

La charge est aussi lancée en CI pour survivre à une coupure locale. Elle utilise
1000 sources, 2000 bâtiments, 50 hauteurs 0–49m, matrice1000 et quatre processus.
Hypothèse explicite : le modèle TSV « generic » reçoit sector65x7 du kit, gains
individuels conservés ; première limite seule. Temps et pics mémoire sont par
coupe. L'interface, les exports et le Gestionnaire des tâches restent séparés.

## Autres reprises interrompues

Les validations locales `local-kit` puis `local-kit-static` ont été interrompues
par les coupures ; leurs `summary.json` peuvent encore dire running alors que
les processus n'existent plus. La seconde avait passé 143 tests et 25 des 28 UI.
Utiliser les résultats CI complets pour le bilan, sans prétendre que ces deux
tentatives locales sont achevées. Le port Vite 1420 était aussi bloqué ; le
driver valide maintenant un snapshot statique sur un port dédié 1432.

## REA et travaux restants

L'assemblage original conserve son SHA256
`c78fa27e6af274e3e5ba6129b59c91043525c630512e975719ecbd8deaa480e8`.
REA a réanalysé UC_PRTO après perte de ses références en mémoire : bundle complet
de 2 enregistrements, 3 849 241 octets, dans
`test validation/2026-10-25/rea-sources-after-powercut.json`. Session fermée ensuite.
Le C# est dans `reference/original-decompiled/EMFPlanner`. Défauts initiaux des
sources corrigés selon `DGV1_UserAddedRow` : nom vide, Other, 1000MHz, P0/G0/H0,
taille1, Existing. Les calculs multi-fréquences ont des comparaisons indépendantes
avec le chemin point par point, sur quatre limites et trois modes de bâtiments.

À terminer/vérifier : conversions Office binaires réelles (Word COM a bloqué),
fond raster DXF/DWG, lecteurs tiers DWG/PDF3D, écriture .xemf native, tous les
écrans au pixel près, tests d'installation et bilan complet des 216 scénarios.
La licence/activation/horloge du logiciel commercial n'est pas à simuler.
