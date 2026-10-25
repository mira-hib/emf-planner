# Campagne du kit — 22 octobre 2026

## Builds et reprise du 24 octobre

À la demande de l’utilisateur, **les seuls résultats du kit** sont regroupés
avec les builds dans `test validation/2026-10-24/`. Les six cibles desktop
Windows/Linux/macOS, x64 et ARM64, ont passé chacune 78 tests métier et neuf
parcours Chromium sur GitHub Actions ; une exécution locale Edge passe aussi.
Installateurs, exécutables, version web, rapports HTML/JUnit/TAP/JSON, captures,
fichiers exportés, journaux et SHA-256 sont conservés. Voir
[build-validation.md](build-validation.md). La couverture reste 65 scénarios
partiels sur 216 et 151 non exécutés ; ces nombres ne sont pas remplacés par
le nombre de tests automatisés réussis.

## Historique du 22 octobre

L’utilisateur demande d’exploiter `test/emf_test_kit/` pour comparer le clone à
l’original et corriger ensemble la logique et l’interface. Le kit est désormais
intégré ; ses fichiers d’entrée restent inchangés.

Le bilan détaillé, les preuves et les limites sont dans
[reference/test-kit-audit-2026-10-22.md](../reference/test-kit-audit-2026-10-22.md).
Validation finale : **159 tests métier et 37 parcours Edge réussis**,
TypeScript/Vite et benchmark réussis. Les 37 parcours sont passés ensemble sur
les sources stabilisées (`.tools/test-kit-final-ui/`). Les captures relues et
les observations KML sont dans `reference/test-kit-interface/`.
Release Windows finale compilée, cible `x86_64-pc-windows-msvc`, sans lancement.
Chemin et empreinte : [test-kit-build.json](../reference/test-kit-build.json).
La matrice [test-kit-coverage.json](../reference/test-kit-coverage.json) inventorie
216 scénarios : 65 rattachés à des vérifications partielles de la nouvelle suite,
151 non exécutés par cette suite. Ne pas déclarer le guide entièrement validé.

Corrections : champ mesuré et fréquence de mesure, distinction champ calculé/total
dans les résultats et exports, groupes actifs conservés, points `Building` depuis
la base, pénétration par deux bandes, pertes nulles et superpositions, plages de
hauteurs temporaires sans perte manuelle ni mesure, champs d’antenne réservés au
rapport, azimut sentinelle à zéro, copie de sources entre origines, hauteurs KML
et encodages, imports MSI/KML transactionnels et validations numériques.

Les nouveaux projets reprennent les paramètres natifs bâtiment 3/6 dB, frontière
400 MHz. Les anciens `.emfp` sans bandes gardent leurs pertes par bâtiment. Les
paramètres natifs inactifs à zéro sont préservés ; la borne de réflexion accepte
la représentation Single native de 1,6. Les 15 références Castelfranco restent stables.

La méthode C# `CalcPPE` récupérée a été extraite sans modification et exécutée
dans une console .NET isolée, avec des structures numériques minimales :
1 800 références versionnées, écart relatif maximal du champ 2,5830227192795853e-6.
Cela ne valide pas `CalcTmp`, le terrain/bâtiments natifs ni le pipeline complet.
Script de régénération : `scripts/native-test-kit-oracle.py` ; fixture et provenance
dans `tests/fixtures/test-kit-native-oracle.json`.

Le contrôle Windows reste indisponible (`native pipe`, erreur 2). L’identité .NET
récupérée a été revérifiée via REA, mais aucune nouvelle capture de l’interface
originale n’a été faite. Les nouveaux parcours et captures concernent Edge.

Commandes : charger `. ./scripts/Use-ReaTools.ps1`, puis `npm test`,
`npm run test:ui`, `npm run benchmark`. Commandes ciblées : `test:kit`,
`test:kit:ui`, `test:kit:inventory`. Node/tsx exige ici une exécution hors du bac
à sable à cause de `uv_os_get_passwd ENOMEM` avant lancement des tests.

Le générateur historique F.699 reste à −10 dBi au plancher, contrairement aux
−13 dBi de la formule reconstituée du kit. Les cases de facteurs restent
indépendantes conformément au C#, malgré le guide. Les écarts et les fonctions
encore grisées restent suivis ; ne pas les transformer en boutons factices.
