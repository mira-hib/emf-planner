# Reproduction rubrique par rubrique

Décision utilisateur du 22 octobre 2026 : reprendre chaque élément du menu de l’original, interface et fonctionnalités, en commençant par ce qui permet de réaliser une étude EMF. Les rubriques existent déjà dans le clone ; leur présence ne signifie pas que la reproduction est terminée.

## Ordre de travail

| Ordre | Rubrique de l’original | Objectif de la passe |
|---|---|---|
| 1 | Radiating Sources | Saisie et organisation des émetteurs, diagrammes, câbles, puissances, facteurs et sorties propres aux sources |
| 2 | EMF Limits | Quatre jeux de limites, intervalles, facteurs, styles et préréglages du logiciel d’origine |
| 3 | Origin and Grid | Origine, coordonnées, emprise et deux grilles de calcul |
| 4 | Antenna Locations | Positions, coordonnées, déplacement et implantation sur la carte |
| 5 | Buildings | Empreintes, hauteurs, bases, édition et interaction avec les calculs |
| 6 | Terrain | Relief, courbes, triangulation et données d’altitude |
| 7 | Point Calculations | Groupes, hauteurs, contributions par source et résultats |
| 8 | Horizontal Cuts | Commandes de création, grilles, contours, styles et rapports |
| 9 | Vertical Cuts | Géométrie, coupes automatiques, maxima, cotes et rapports |
| 10 | Tables and Graphs | Valeurs calculées, sélections, graphiques et exports |
| 11 | Lines and Labels | Dessin, nœuds, annotations, symboles et styles |
| 12 | CAD | Mise en page, calques, échelles et sorties CAO |
| 13 | 3D | Vue, navigation, objets, calques et volumes de sécurité |
| 14 | Report by template | Modèles, alias, insertion des tableaux/graphiques et production Word |
| 15 | Calibrate Map Image | Repères, deux méthodes de calibration et pyramide de tuiles |
| 16 | Settings | Compléter les préférences transversales et leur conservation |

Les préférences nécessaires à une rubrique sont traitées avec elle. L’ordre suit les données nécessaires à une étude, puis les calculs et les livrables.

## Méthode de validation

Pour chaque rubrique, relever les commandes, champs, boîtes de dialogue et comportements dans les formulaires C# récupérés par REA. Comparer les données sauvegardées et les interactions du clone. Les observations dynamiques déjà conservées servent de référence visuelle ; ajouter de nouvelles observations de l’original lorsque le contrôle du bureau est disponible. Vérifier un projet vide et Castelfranco sans écraser le fichier original. Conserver les fonctions inachevées en gris lorsque leur commande ne réalise pas encore le travail annoncé.

## Rubrique 1 — lot essentiel intégré

- Sélection de plusieurs lignes par Ctrl/Maj, édition groupée d’une colonne, duplication après la dernière ligne sélectionnée, ordre stable, déplacements haut/bas/début/fin et suppression confirmée par Suppr.
- Copie et collage de sources complètes avec identifiants renouvelés, diagrammes et paramètres de câbles indépendants ; tampon persistant entre projets. Le tampon utilise du JSON validé, sans reprendre BinaryFormatter.
- Activation collective fondée sur la première source, palette native de douze couleurs appliquée à la sélection et conservation des couleurs importées.
- Bandes F/F2, service libre, état Existing/New et conservation des données de câble lors de l’import .xemf et de la sauvegarde .emfp. Bornes numériques des principaux champs alignées sur les plages natives ; N tronqué à l’entier.
- Calculateur EIRP indépendant de la grille, trois sélecteurs W/mW/dBW/dBm, calcul direct et deux calculs inverses. Précision Single et arrondis natifs vérifiés séparément.
- Facteurs α24/Ftdc/Fpr bornés à 0,1–1 et désactivés/vides suivant les indicateurs du jeu de limites.
- Calcul explicite des ouvertures H/V pour la source courante ou toutes les sources, croisements au niveau absolu de 3 dB.
- Enveloppes avec gains ignorés/utilisés et éditables, normalisation native et conservation du gain de la source. Câbles calculés au milieu de F/F2 ; connecteurs entièrement exclus lorsque leur case est décochée. MSI Windows-1251 et UTF-8.

## Rubrique 1 — encore à reproduire

La rubrique Sources n’est pas déclarée entièrement terminée. Restent notamment :

- Sélection de cellules et de colonnes comme la grille native, ordre des colonnes mémorisé, interactions de double clic, formulaire de conversion de puissance synchronisé.
- Valeurs initiales natives d’une ligne vide ; ouvertures textuelles réservées aux rapports ; association propriétaire/couleur. Les bornes invalides de l’azimut sont rejetées dans le clone, sans reproduire la sentinelle native `0÷360`.
- Arbre de dossiers et fichiers d’antennes, quatre graphiques du visualiseur, inversions et rotations des deux plans ; panier d’enveloppe alimenté par dossiers.
- Les quatre sorties Word spécifiques aux sources, diagrammes, volumes et points cardinaux. Le rapport générique disponible ne les remplace pas.
- Modèle Excel natif à 19 colonnes, sélection des sources actives et import historique XLS/XLT ; l’import disponible lit les fichiers OOXML XLSX/XLTX selon le schéma du clone. L’entrée XLT historique est grisée.
- Métadonnées natives de l’export MSI et comparaison visuelle complète des dialogues.

Le détail des preuves C# et des différences initiales figure dans [l’audit Sources](../reference/sources-native-audit-2026-10-22.md). Continuer les écarts de cette rubrique avant de la déclarer achevée ; la rubrique suivante dans l’ordre est Limites EMF.

## Contrôles de cette passe

Les tests couvrent les opérations de lignes et les copies profondes, les bandes de fréquences, les ouvertures, les calculs de puissance et les enveloppes/câbles. Les parcours Edge supplémentaires vérifient les modifications groupées, les limites de N, le tampon après rechargement, le calculateur sans modification du projet, les fichiers MSI et la sauvegarde des options de câble. Les 15 références de calcul de l’original restent conservées.

Résultat final : **63 tests unitaires et 21 parcours Edge réussis**, compilation TypeScript/Vite et release Windows réussies. La nouvelle release mesure 6 705 664 octets ; son SHA-256 et le chemin exact sont conservés dans [source-menu-build.json](../reference/source-menu-build.json). Capture relue : [Sources Castelfranco](../reference/sources-interface/sources-castelfranco.png). Le nouvel exécutable n’a pas été lancé pendant cette passe.

La compilation Windows utilise la cible explicite `x86_64-pc-windows-msvc` pour produire un nouvel exécutable sans remplacer celui actuellement ouvert dans `src-tauri/target/release/`. Ne pas fermer une session native contenant des modifications sans les sauvegarder. Le contrôle du bureau est indisponible dans cette session ; la validation visuelle s’appuie sur Edge et les références déjà capturées, pas sur une nouvelle manipulation de l’original.

`scripts/Start-EmfPlanner.ps1` choisit le plus récent des deux exécutables release, sans fermer une application déjà ouverte.

## Rubrique 2 — EMF Limits, lot essentiel intégré le 22 octobre

Reprise explicitement demandée par l’utilisateur après lecture du memory bank.
Audit statique : [commandes et preuves C#](../reference/limits-native-audit-2026-10-22.md).

- Remplacement des quatre tableaux séparés par la table native à neuf colonnes : fréquence commune F, puis E/A pour chacun des quatre jeux. Descriptions multilignes, couleurs, douze cases α24/Ftdc/Fpr.
- Ajout d’une ligne entièrement vide, suppression de la ligne choisie (sélection multiple Ctrl/Maj ajoutée au clone), Suppr, annuler/rétablir et sauvegarde/réouverture `.emfp`.
- Bornes natives : `0 < F ≤ 1000000`, `0 < E ≤ 100000`, `-3 ≤ A ≤ 3`. Virgule décimale, conversion Single et effacement des valeurs invalides. Sentinelle `-999` conservée pour les cellules vides, y compris à l’import `.xemf`.
- Graphique à deux axes logarithmiques, X de 10 MHz à 100 GHz, Y de 1 à 1000 V/m, quatre couleurs, segments discontinus séparés. Une cellule manquante supprime la courbe du jeu entier, comme dans l’original.
- 16 préréglages historiques portés intégralement de `Public2.SetLimitsPRJ`, tuples et descriptions comparés automatiquement au C#. Confirmation avant remplacement des quatre jeux ; conservation des identifiants, couleurs, activité et facteurs. Particularité native Grèce : descriptions conservées.
- Nouveaux projets alignés sur `SetLimitsAPP` : jeux Public/Occupational, jeux 3/4 vides, rouge/bleu/vert/cyan et facteurs cochés.
- Moteur : ordre saisi conservé, intervalles `(borne précédente, F]`, pas de tri ni d’extrapolation. Table vide/incomplète ou fréquence hors domaine : repli natif PDU=1000000, E équivalent `sqrt(3.77e6)`. C’est un comportement technique historique, pas une limite réelle d’exposition. Les 15 résultats de Castelfranco sont inchangés.
- Compatibilité des anciens `.emfp` du clone : leurs tableaux indépendants sont affichés sur une union de fréquences ; la table commune est enregistrée lors de l’édition. Aucune modification silencieuse à la simple ouverture.

L’interface et les parcours sont vérifiés dans Edge, sur projet vide et Castelfranco,
avec captures relues dans [reference/limits-interface](../reference/limits-interface/).
Aucune nouvelle observation dynamique du logiciel original ; la fidélité repose
sur les sources C# récupérées et les références déjà conservées. Le calcul ne
reproduit pas encore tous les arrondis intermédiaires Single de `CalcTmp`.
Les préréglages représentent la version historique 7.1, sans affirmation de
conformité aux réglementations actuelles.

Code : `src/components/LimitsPage.tsx`, `src/lib/limits.ts`, `src/lib/presets.ts`.
Validation : **72 tests unitaires, 24 parcours Edge**, TypeScript/Vite et 15
références numériques conservées. Suite UI : 23 parcours réussis sous Node 22,
puis le parcours de rapport ExcelJS réussi sous Node portable 24.22.0 après une
erreur de cache de module dans le runtime Node 22 (sans changement de code).
Release Windows recompilée avec la cible `x86_64-pc-windows-msvc`, sans fermer
la session native existante et sans lancer le nouvel exécutable. Chemin, taille
et SHA-256 dans [limits-menu-build.json](../reference/limits-menu-build.json).
La prochaine rubrique dans l’ordre est **Origin and Grid** ; les écarts Sources
restent suivis séparément et ne sont pas déclarés achevés.

## Rubrique 3 — Origin and Grid, lot essentiel intégré le 22 octobre

Audit statique : [champs, commandes et preuves C#](../reference/origin-grid-native-audit-2026-10-22.md).

- Origine latitude/longitude en lecture seule, altitude du terrain au dixième ; centrage sur l’origine et déplacement au centre de l’écran après confirmation. La poignée de l’origine se déplace aussi à la souris.
- `settings.site_coordinates` conserve les coordonnées PRTO indépendantes pour le rapport. Édition décimale/DM/DMS, copier origine, centrer site ; déplacer l’origine laisse PRTO inchangé. L’ancien dialogue « Coordonnées du site » modifiait l’origine : il est corrigé. Aperçu, Word, PDF, sujet Excel et alias `{Coordinates}` utilisent les coordonnées du site.
- Zone rectangulaire avec gauche/droite/haut/bas indépendants, dialogue annulable, bornes natives gauche/bas `[0,5000]`, droite/haut `[10,5000]`, marge de ±0,1 unité Web Mercator. Le rectangle est redimensionnable sur les cartes locale et Leaflet ; les bords croisés sont réordonnés au relâchement.
- Coordonnées WM des deux coins conservées à l’import/sauvegarde et lors du changement d’origine, puis nouvelles coordonnées locales calculées. Les autres objets conservent leurs positions géographiques. Les sources, points et plans verticaux utilisent `nativeLocal` avec Single distance/azimut et arrondi 0,1 m ; les coupes gardent leur ancre géographique. Annuler/rétablir restent disponibles.
- Grille dessinée DarkBlue dans le rectangle avec graduations dynamiques 8–13 intervalles (algorithme déjà utilisé en CAO). Correction de l’import : l’affichage de grille correspond à `ShowHorizPlaneScale`, et non à `ShowCircles`.
- Résolution horizontale native en nombre de divisions du grand axe, de 50 à 1000 ; défauts 600/200, deuxième axe proportionnel arrondi au pair et nœuds inclusifs. Le Worker utilise effectivement les deux résolutions. Les anciens projets métriques conservent leur mode ; un sélecteur permet de choisir divisions ou pas en mètres.
- Nouveaux projets : rectangle WM ±100 et deux grilles natives. Le lieu initial Abidjan du clone est conservé. Les préférences d’application de l’original ne sont pas importées.
- Règle 50 m au centre, deux extrémités ajustables, distance au dixième, angle et bascule d’affichage. La mesure reste disponible pour la création d’une coupe verticale le long de la règle.

Validation : **81 tests métier**, **28 parcours Edge** sous Node portable 24.22.0,
TypeScript/Vite, 15 références originales inchangées. Le helper d’ouverture des
nouveaux tests accepte explicitement la confirmation d’abandon et attend « Projet
ouvert » ; deux parcours vérifient les neuf sources importées. Captures relues :
[zone asymétrique](../reference/origin-grid-interface/origin-asymmetric-grid.png),
[Castelfranco après déplacement d’origine](../reference/origin-grid-interface/origin-rebased-castelfranco.png).

### Écarts encore suivis

- Pas de nouvelle comparaison dynamique de l’écran original. Les matrices verticales, dont le natif utilise une grille carrée et des hauteurs basse/haute distinctes, restent traitées par l’ancien pas métrique du clone jusqu’à la rubrique Coupes verticales. La nouvelle résolution est explicitement libellée horizontale.
- Le rendu de carte et les objets surfaciques restent dans le cadre Mercator à échelle locale du clone, distinct du repère sphérique natif des sources/points. La concordance générale des calculs après un déplacement arbitraire d’origine n’est pas démontrée ; seules les ancres et les conversions ciblées sont vérifiées.
- Le parseur de coordonnées du clone accepte zéro, les suffixes d’hémisphère et la plage latitude ±85/longitude ±180. Il ne reproduit pas les particularités natives de rejet de zéro et des composantes degrés limitées à 80/179.
- Poignées de coins au lieu des seuls bords natifs ; déplacement visuel de la poignée pendant le geste, recalcul au relâchement. La saisie géographique exacte de l’origine passe par le déplacement/centrage, comme dans le natif.
- Les paramètres ajoutés au panneau Origine pour la résolution et la visibilité de la zone sont des commodités du clone ; leur emplacement n’est pas identique au natif (Setup pour les matrices).

Code : `OriginPanel.tsx`, `OriginMapLayer.tsx`, `lib/origin-grid.ts`,
`lib/native-coordinates.ts`. La prochaine rubrique est **Antenna Locations**.

Release Windows recompilée, 6 711 808 octets ; chemin et SHA-256 dans
[origin-grid-build.json](../reference/origin-grid-build.json). Le nouvel exécutable
a ensuite été lancé à la demande de l’utilisateur via `Start-EmfPlanner.ps1` :
fenêtre « EMF Planner — Clone », PID observé 35952, processus répondant.
Aucune session native ouverte n’a été fermée par l’agent. Ce contrôle de lancement
ne remplace pas une validation des parcours dans l’application native.

## Rubrique 4 — Antenna Locations et fonds de carte, 22 octobre

Audit : [champs, commandes et preuves REA/C#](../reference/antenna-locations-native-audit-2026-10-22.md).
Code : `AntennaLocationsPanel.tsx`, `AntennaMapHandle.tsx`, `lib/antenna-locations.ts`.

- Grille à sept colonnes dans l’ordre natif, nom avec modèle, latitude/longitude,
  hauteur et azimut en lecture seule, X/Y éditables au dixième affiché. Formats
  géographiques des branches `ConvLatToStr/ConvLonToStr`, y compris les différences
  de précision suivant l’hémisphère. Hauteurs Single affichées sans bruit binaire.
- Sélection de lignes Ctrl/Maj, cellules disjointes Ctrl, rectangle Maj et colonnes
  par en-tête ; édition groupée limitée aux cellules de la colonne éditée.
  Réordonnancement des colonnes par glisser-déposer. Les commandes de déplacement
  demandent des lignes sélectionnées ; une sélection de cellules ne déplace pas
  les antennes. Les couleurs natives des lignes et les cinq icônes sont reprises.
- Cinq commandes natives, dans leur ordre : centrer sur la courante, sélection au
  centre de l’écran, sur la courante, à l’origine, puis origine sur la courante.
  Les commandes modifiant les positions sont confirmées. Correction des anciennes
  commandes qui déplaçaient toutes les sources malgré leur libellé « sélectionnées ».
- Conversions WM avec facteur natif pour X/Y, coordonnées natives arrondies lors
  des déplacements, conservation des propriétés techniques et des sources hors
  sélection, annuler/rétablir et sauvegarde `.emfp`. Le centrage utilise l’ancre
  géographique et le changement d’origine conserve les positions géographiques.
- Curseur Maximum capture distance 1–30, défaut 15, import `LengthAttractAntennas`,
  conservation dans le projet et absence d’invalidation des calculs pour ce seul
  réglage. Seuil rectangulaire natif en dixièmes d’unité WM, première autre antenne
  correspondante. Glisser-déposer local SVG et Leaflet, accrochage au relâchement,
  priorité visuelle aux sources sélectionnées superposées. L’icône Leaflet reste
  stable au changement de sélection, pour ne pas interrompre le glissement.
- Panneau initial 510 px avec sept colonnes visibles, lignes 18 px et curseur en
  bas ; largeur redimensionnable mémorisée séparément des autres rubriques.

La capture utilisateur des cartes est traitée dans la même passe : **14 entrées
dans l’ordre natif**, résolveur de tuiles commun 2D/3D, quadkeys Bing, projection
elliptique Yandex, vrai fournisseur conservé à l’import, personnalisation de son
URL et messages d’indisponibilité sans remplacement silencieux. Google Satellite
utilise une URL du même fournisseur ayant renvoyé une image après constat du
404 sur l’ancienne version. Dix services ont renvoyé une image lors de la sonde
réseau ; OSM Relief porte un filigrane exigeant une clé. HERE/Mapquest sont
configurables avec les accès de l’utilisateur. Voir [map-providers.md](map-providers.md).

Les raccourcis WinForms complets et les cas limites d’arrondis intermédiaires ne
sont pas tous reproduits. L’aperçu de déplacement applique l’accrochage à la fin
du geste ; le natif l’applique pendant le geste. Pas de nouvelle capture originale
Windows : `@oai/sky.list_apps` échoue avec `native pipe`, erreur système 2.
Les captures conservées concernent Edge sur projet neuf et Castelfranco réellement
importé. La rubrique n’est pas déclarée identique pixel par pixel.

Prochaine rubrique selon l’ordre : **Buildings**. Les écarts précédents Sources,
Limites et Origine restent suivis, sans déclaration d’achèvement général.

Validation finale : **171 tests métier, 46 parcours Edge réunis sur les sources
stabilisées**, TypeScript/Vite et benchmark réussis. Les cinq parcours Antenna
Locations incluent Castelfranco importé, glissement SVG/Leaflet, cellules/colonnes,
réordonnancement, confirmations et conservation des sources hors sélection. Les
quatre parcours des cartes utilisent des tuiles interceptées ; l’accès réel à dix
services est vérifié séparément. Les 15 résultats originaux gardent un écart
maximal de champ de `9.945531114219364e-7 V/m`. Captures effectivement relues dans
`reference/antenna-locations-interface/`. Résultats Edge dans
`.tools/antenna-map-full-ui/`. Les essais isolés initiaux et leurs erreurs
d’environnement/locateurs ne sont pas comptés comme réussis.

Release Windows finale compilée avec succès pour `x86_64-pc-windows-msvc`.
Chemin, taille, SHA-256 et validations dans
[antenna-locations-build.json](../reference/antenna-locations-build.json).
Exécutable non lancé ; aucune application existante fermée.
