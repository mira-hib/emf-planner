# Campagne du kit EMFPlanner — 22 octobre 2026

## Résultat et portée

Le kit contient **216 scénarios**, dont plusieurs essais d’installation, de licence,
de réseau et de fonctions encore grisées du clone. Cette campagne ajoute **78 tests
métier** et **9 parcours Edge** aux 81 tests et 28 parcours existants.
Les fichiers d’entrée du kit sont conservés sans modification.

Validation finale : **159/159 tests métier**, **37/37 parcours Edge** sur les
sources stabilisées, TypeScript/Vite et benchmark réussis. La suite Edge finale
est conservée localement dans `.tools/test-kit-final-ui/`. Une première exécution
avait été interrompue sur un parcours par le rechargement Vite pendant une édition ;
le parcours est ensuite passé seul, puis les 37 sont passés ensemble.

La release Windows finale est compilée avec la cible `x86_64-pc-windows-msvc`.
Chemin, taille et SHA-256 : [test-kit-build.json](test-kit-build.json).
Le nouvel exécutable n’a pas été lancé ; la vérification des parcours concerne Edge.

L’index [test-kit-coverage.json](test-kit-coverage.json) rattache **65 scénarios** à
des vérifications automatisées de cette campagne. Ce sont des couvertures partielles :
un identifiant dans un test ne signifie pas que toutes ses variantes ont été exécutées.
Les 151 autres scénarios ne sont pas déclarés validés par cette suite.

Trois niveaux de référence restent distincts :

1. **Application originale observée précédemment** : les 15 valeurs de Castelfranco
   dans `tests/fixtures/original-results.json`. Écart maximal inchangé :
   **9,945531114219364e-7 V/m**, tolérance 1,1e-6 V/m.
2. **Méthode originale récupérée et exécutée isolément** : 1 800 cas de `CalcPPE`,
   trois diagrammes du kit, cinq azimuts de source, cinq inclinaisons mécaniques,
   huit directions d’observation et trois altitudes, à 100 m horizontalement.
   Écart relatif maximal du champ : **2,5830227192795853e-6**, soit **0,0002583 %**.
   Tolérance du test : 1e-4 en relatif. Le corps C# est extrait sans modification ;
   les données `Public2` et `TMP` sont des structures minimales initialisées par le
   programme d’essai. Cette vérification ne couvre pas `CalcTmp`, ses appels protégés,
   les bâtiments, le terrain, la licence ou le pipeline complet de l’application.
3. **Formules indépendantes et relations du kit** : puissances, conversions, champ
   isotrope, contours, facteurs, blindage, terrain, mesures et croisements point/coupe.

Le contrôle Windows a échoué avec `native pipe is unavailable` (erreur Windows 2),
y compris après réinitialisation. **Aucune nouvelle manipulation ni capture de
l’interface originale n’a été effectuée.** Les captures de cette campagne montrent
le clone dans Edge. L’identité de l’image .NET récupérée a été revérifiée avec REA :
[Evidence statique](test-kit-native-evidence.json), SHA-256
`358188cc57c432319c0b687fd3e810b19a614355bc7ab59e475e8abf75a52e55`.

## Écarts corrigés

| Défaut constaté | Correction et vérification |
|---|---|
| Le champ mesuré était affiché mais ignoré dans les totaux | `sqrt(Ecalc² + Emes²)`, ajout de `Emes²/377` à la densité et de `(Emes/Elim)²` à l’exposition. Le champ calculé reste accessible séparément. UI, Worker, sauvegarde et Excel vérifiés. |
| Absence de fréquence de mesure | Réglage de projet et import de `ControlPointMesureFreq`, défaut 1000 MHz ; borne native 30–300000 MHz. Les projets ne contenant que des mesures peuvent être calculés. |
| Perte de l’activité des groupes à l’import | `CPG.Active` conservé ; case d’activation dans le panneau des points ; groupes inactifs exclus du calcul et des rapports. |
| Position `Building` sans prise en compte de la base | Hauteur comptée depuis `Fundation`, comme `RepWordAlias.CalcRKB`. Option `Building` disponible dans la grille ; ancien mode `Roof` conservé pour les fichiers du clone. |
| Atténuation intérieure unique et pertes nulles remplacées par la valeur globale | Import des deux bandes et de `BuildFreqBorder`, réglages éditables. Nouveaux projets : 3/6 dB et 400 MHz, valeurs natives. Les anciens `.emfp` sans bandes conservent les pertes de leurs bâtiments ; zéro reste zéro. Pas d’accumulation répétée pour des polygones superposés. |
| Le calcul par plage remplaçait les hauteurs enregistrées et pouvait réutiliser leurs pertes/mesures | Calcul temporaire sans mutation du projet, sans perte manuelle ni champ mesuré, avec atténuation des bâtiments conservée. Explication dans le dialogue. Vérifications PNT-07/08 et annulation des groupes. |
| Les ouvertures et l’inclinaison électrique pouvaient modifier un diagramme synthétique sans MSI | Champs réservés au rapport ; les échantillons mesurés continuent de piloter le calcul. Les anciens diagrammes absents utilisent le repli synthétique fixe 65°/10° du clone ; ce repli n’est pas une antenne native validée. Aperçu et export cohérents. |
| Azimut sentinelle négatif interprété comme une vraie rotation | Calcul à 0°, suivant `CalcTmp`. La saisie invalide de la grille reste rejetée ; la chaîne native `0÷360` n’est pas reproduite. |
| Copie entre projets gardant les anciens X/Y | Origine mémorisée dans le tampon ; collage dans une autre origine recalculé au dixième natif depuis les ancres géographiques. Lat/lon conservées. |
| Noms KML `hauteur;libellé` ignorés | 10/B01, 12,5/B02, 15/L-shape et 4/2 correctement séparés ; base initiale 0 conforme au chargeur natif. Décimale française et UTF-8/BOM/CRLF/UTF-16 testés. |
| Imports MSI ambigus acceptés silencieusement | Rejet explicite d’un gain absent, de points surnuméraires et de pertes supérieures à 300 dB ; import transactionnel conservant le diagramme précédent. Tous les 12 fichiers `bad_*` sont testés. |
| Réflexion 0,01–5 proposée alors que le natif borne 1–1,6 | Bornes UI corrigées ; import validé, y compris la représentation Single native de 1,6. Autres paramètres numériques non finis, TX non entiers et points corrompus rejetés sans remplacement du projet courant. |
| Coordonnées ambiguës acceptées (`1e3`, doubles hémisphères, signe contradictoire) | Rejet explicite ; arrondi DD/DMS avec retenue vers le degré suivant revérifié. |

Le rejet des gains MSI absents, des pertes >300 dB, des hauteurs KML invalides et des
contours dégénérés est un choix de robustesse du clone. Il n’est pas présenté comme
une observation dynamique de l’original. Un KML contenant un élément refusé est
rejeté entièrement ; les bâtiments précédents restent intacts. Un import répété
ajoute des copies, comme le chargeur KML original. Les trous restent refusés.

## Essais de charge et d’interface

- Les 1 000 sources de `sources_stress_1000.tsv` survivent à la sérialisation et
  au rechargement ; cinq répétitions du calcul de point donnent les mêmes résultats.
- Les 2 000 bâtiments de `buildings_stress_2000.kml` sont importés par l’interface,
  enregistrés et rouverts sans perte. Mesure initiale import/sauvegarde/réouverture :
  environ **5,7 s** sur ce poste ; ce n’est pas un benchmark du calcul EMF avec bâtiments.
- Les 19 éléments du KML limite sont classés individuellement ; `12,5` reste 12,5,
  Point/LineString ne deviennent pas des bâtiments, trous/contours dégénérés refusés.
- Les sept MSI `ok_*` se chargent et se réexportent ; gains dBd et pertes signées
  gardent les conventions déjà établies. Le fichier négatif est accepté en valeur
  absolue, conformément à `ClassDN`, malgré son nom `bad_*`.
- Affichage et accès aux commandes des points vérifiés à **1366×768 et 1040×720**.
  Cela ne remplace pas des essais Windows DPI 125/150 % ni une vérification WebView2.

Captures relues : [champ mesuré](test-kit-interface/point-measured.png),
[import KML](test-kit-interface/kml-import.png),
[panneau à 1040 px](test-kit-interface/points-1040.png). Les classements KML
et la mesure initiale de charge sont conservés dans le même dossier.

## Différences du guide à ne pas transformer en erreurs du clone

- SRC-37 : le générateur récupéré utilise un plancher **−10 dBi**, tandis que le
  fichier attendu 40 GHz du kit utilise −13 dBi. Le guide précise que sa formule
  F.699 a été reconstituée de mémoire. Le comportement natif historique est conservé.
- SRC-21 : les gestionnaires `UC_Limits` activent les trois facteurs indépendamment,
  contrairement à l’interdiction annoncée par le guide. Aucun interverrouillage
  supposé n’a été ajouté au clone.
- SRC-22/23 : zéro est testé au niveau du moteur, mais les champs de facteur natifs
  de la grille restent bornés à 0,1–1. Le test ne prétend pas que zéro est saisissable.
- LIM-01/04 : les préréglages historiques 7.1 et leur repli hors domaine sont ceux
  du C#, pas une certification des valeurs actuelles ni la table alternative du manuel.
- BLD-13/DOC-05 : défauts natifs 3/6 dB et 400 MHz ; le scénario inverse 6/3 dB et
  500 MHz est testé avec des réglages explicites.
- DOC-10 : `Module1.SuperFormat` coupe une chaîne formatée à un nombre de caractères.
  Le clone garde ses affichages décimaux et ses valeurs de calcul non tronquées ;
  l’identité textuelle de tous les rapports natifs reste à traiter.

## Reproduire et poursuivre

```powershell
. ./scripts/Use-ReaTools.ps1
npm test
npm run test:ui
npm run benchmark
npm run test:kit:inventory
```

Les commandes ciblées sont `npm run test:kit` et `npm run test:kit:ui`.
Pour régénérer les 1 800 références avec le SDK .NET local existant :
`.tools/python/Scripts/python.exe -B scripts/native-test-kit-oracle.py` après le
chargement de l’environnement ci-dessus. La fixture versionnée permet aux tests
TypeScript de fonctionner sans ce SDK ; son empreinte de méthode est contrôlée.

Restent à traiter ou à observer : les variantes de collage natif, les points
`Window`, les imports/exports Excel natifs, les grilles V complètes et cotes,
les modèles/alias et graphiques Word, les calibrations par repères, le DWG,
le réseau/cache/proxy, les grandes simulations simultanées et la comparaison
visuelle directe de toutes les rubriques avec l’original.

Les fonctions grisées ne sont pas déclarées implémentées. Cette campagne renforce
les branches disponibles ; elle ne clôt pas la reproduction du logiciel entier.
