# Reprise complète du kit — travail en cours

> État de reprise vérifié : le 25 octobre, **236/236 tests de code** et
> **65/65 parcours UI** passent. Les résultats du kit seuls sont 143 tests de
> code et 28 parcours UI dans la campagne locale en cours. Ces nombres ne
> signifient pas que les 216 scénarios du guide sont entièrement validés.
> Le scénario de charge repart intégralement dans `stress-rob05-final` ; les
> reçus précédents sont conservés séparément. Voir les précisions en fin de fichier.

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


## Checkpoint suppl?mentaire apr?s la seconde coupure ? 25 octobre, 20 h UTC

- 235/235 tests complets r?ussis avant la derni?re optimisation ; suite cibl?e
  calcul 20/20 apr?s agr?gation multi-fr?quences et cache des rayons identiques.
  Nouvelle suite compl?te attendue : 236 tests, r?sultat en cours.
- Deux campagnes UI statiques ont permis de corriger les anciennes attentes de
  boutons d?sactiv?s, les URL de test fig?es sur le port Vite, les s?lecteurs de
  contours qui comptaient les tours, puis un vrai d?faut Word : Packer.toBuffer
  n'est pas disponible dans le navigateur. Remplac? par toArrayBuffer.
  Ne pas annoncer toutes les UI r?ussies : dernier bilan 59/64 ; relance des
  parcours modifi?s + nouveau test Word des coupes en cours.
- Valeurs natives des nouvelles sources confirm?es UC_PRTO.UserAddedRow : nom
  vide, Other, F1000, P0, N1, G0, H0, taille1, mod?le vide, Existing. Appliqu?es.
- Tableaux verticaux : les commandes ouvrent d?sormais la table native calcul?e
  depuis les param?tres courants ; aucune grille charg?e n'est n?cessaire.
- Proxy natif/cache disque, points XLSX, pr?f?rences globales, profils verticaux,
  mesures terrain/tours, H2 et conservation des contours impl?ment?s depuis le
  pr?c?dent checkpoint. Le contr?le Windows de l'original fonctionne ? nouveau.
- Les deux projets originaux calculent une coupe H et V ? leur matrice import?e
  (big test 600, Castelfranco 800), sans valeur non finie. Fichiers d?taill?s dans
  test validation/2026-10-25/native-samples-v2. Big test n'a aucun point de contr?le.
- Encodeurs natifs r?els revalid?s dans native-exports-v3 : U3D + PDF3D, DWG R2000,
  relecture LibreDWG avec g?om?trie 100x50mm. Ce n'est pas un lecteur tiers.
- ROB-05 a produit 50 re?us sur 50 pour la charge exacte, mais plusieurs reprises
  ont partag? des re?us. Une ex?cution int?grale neuve est lanc?e, sans r?utiliser
  aucun re?u, dans stress-rob05-final. Elle utilise 4 processus sous priorit?
  normale, 1000 sources et 2000 b?timents, 50 hauteurs 0?49m et matrice1000.
  Le motif generic est explicitement remplac? par sector65x7 du kit ; gains TSV
  conserv?s. Ne pas confondre cette charge moteur avec les ?tapes UI/export/3D.
- Cargo check r?ussi apr?s ressources portables. Converters copi?s avec tous les
  ex?cutables portables, chemin adjacent accept? ; installateurs ont ressources.
- Nouvelle campagne six plateformes isol?e : branche
  build-validation/20261025-200009, premier commit
  413a91040a47b89df3ad1efae1072dcac4cb0644, run GitHub 40228691628.
  Ce premier snapshot pr?c?de les derniers correctifs : un nouveau commit sera
  n?cessaire. Les campagnes du 24 sont pr?serv?es. Les helpers launch/sync de
  .tools acceptent --campaign 2026-10-25 ; collect-validation est maintenant dat?
  dynamiquement. Ne pas pr?senter ces premiers builds comme les derniers.
- Toujours manquants/non valid?s : Office binaire r?el (Word COM a bloqu?), fond
  raster dans DXF/DWG, lecteurs tiers DWG/PDF3D, ?criture .xemf native, exactitude
  pixel de tous les ?crans, bilan r?el des 216 sc?narios. Langue est encore un
  bouton additionnel d?sactiv? sans ?quivalent confirm? dans UC_Setup.
