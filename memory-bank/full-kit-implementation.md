# Reprise complète du kit — travail en cours

Demande : implémenter les fonctions restantes, vérifier les scénarios du kit,
comparer au logiciel original avec REA, conserver builds et résultats dans
`test validation`. Reprise après une coupure de courant le 25 octobre.

Ne pas confondre le nombre de tests unitaires avec les 216 scénarios du guide.
`reference/test-kit-scenarios.json` catalogue les 216 textes exacts, sans succès
inféré. Aucun bilan intégral n’est encore établi.

## Travaux enregistrés

- Sources : collage cellules/colonnes/TSV, Shift+Insert, sélection multiple,
  persistance ordre/largeur, convertisseur 4 unités, ZIP/dossier récursif MSI,
  propriétaires et couleurs, édition de limites transactionnelle.
- Géométrie : édition des sommets/objets, bases min/max/moyenne, téléchargement
  OSM avec options et déduplication, import HGT et MNT Terrarium, calibration
  deux lignes/ligne-point, rotation et références, décodage TIFF, cache de tuiles.
- Rapports : 12 familles d’alias et variante A3, modèles OOXML avec remplacement
  dans tableaux/en-têtes/pieds/zones de texte, bibliothèque de rapports, Excel
  natif 19 colonnes. Des commandes précédentes ont été raccordées aux nouveaux
  modules, mais tous les parcours n’ont pas encore été vérifiés.
- Calcul : grilles verticales H1/H2 à matrice native, workers réellement séparés,
  sommes indépendantes de l’ordre des sources, grilles Float64 transférables,
  préparation des constantes source, conservation des anciennes images de coupe,
  projection des coupes et sauvegarde de contours après calcul.
- Ponts Rust : conversion Word/Excel historiques par instance Office privée en
  lecture seule et macros désactivées ; DWG par LibreDWG ; U3D puis annotation
  PDF 3D réelle. Les scripts d’installation conservent licences/source LibreDWG.

## Vérifications effectuées

- `npm test` : 216 tests réussis, zéro échec, après les premières optimisations.
  Ce sont 216 tests de code, **pas 216 scénarios complets**. Des changements
  postérieurs nécessitent une nouvelle exécution.
- `cargo check` réussi avec les ponts natifs (avertissements préexistants).
- Vrai encodeur Intel U3D : fichier 720 octets obtenu. Vraie conversion
  DXF→DWG R2000→DXF et lecture JSON LibreDWG obtenues après correction DXF.
  Artefacts `test validation/2026-10-25/native-exports-v2/`. Avertissements de
  handles à traiter ; ouverture visuelle dans un lecteur tiers non faite.
- Un essai Playwright initial a seulement échoué à sélectionner un projet
  `chromium` inexistant. Utiliser le canal Edge configuré ou PLAYWRIGHT_CHANNEL,
  sans `--project`. Le parcours Sources est en cours de relance.

## Restant à terminer et vérifier

Transport proxy réel + cache dossier natif, propriétaires globaux et format
coordonnées global, import/export complet des points, affichage détaillé des
coupes/cotes/terrain/tours, perf du vrai scénario lourd, validation numérique
des projections et workers, audits par scénario, parcours UI de tous les nouveaux
écrans, conversion Office réelle, lecteurs DWG/PDF3D, builds six plateformes.
`PENDING_COMMANDS` doit garder les vrais manques ; ne pas activer de faux bouton.
Les agents ont conservé leurs changements mais ont atteint une limite de quota
avant la rédaction des audits et manifests. Aucun agent n’est encore actif.

## Références REA

Artefact SHA256 `c78fa27e6af274e3e5ba6129b59c91043525c630512e975719ecbd8deaa480e8`,
MVID `1bed30fd-0970-42c5-b45b-a0f90a582022`. C# dans
`reference/original-decompiled/EMFPlanner/`. La réinspection de RepWordAlias est
conservée dans `reference/rea-full-kit-safety-evidence.json`. Elle lie notamment
`CalcEmfSafetyVolumes` aux dimensions Lv/Lh/Lm1/Lm2 et aux facteurs par limite.
Ce sont des preuves statiques ; aucune observation runtime ne doit en être déduite.

Les cases de facteurs indépendantes, le plancher natif F.699 −10, le nom NAME du
MSI, les bornes natives du générateur et les valeurs par défaut divergent parfois
du guide : conserver ces écarts explicitement. Les tests de licence/activation
du logiciel commercial ne doivent pas être simulés ni annoncés réussis.
