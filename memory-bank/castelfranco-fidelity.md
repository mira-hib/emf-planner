# Comparaison Castelfranco — 20 octobre 2026

## Contexte et méthode

L’utilisateur a signalé que la CAO, la 3D et le diagramme d’antenne restaient
éloignés de l’original après les ajouts Mac (`fa6b462`). L’application Windows
EMFPlanner 7.1 Trial et le clone ont été observés directement sur le projet
Castelfranco Emilia TAV2. Le code récupéré avec REA a servi de seconde preuve.
Les comparaisons concernent ce scénario ; aucune parité générale n’est annoncée.

## Écarts confirmés et corrections

| Zone | Avant | Correction et preuve originale |
|---|---|---|
| Diagramme | Échelle 40 dB, tracés non remplis, commandes dispersées | 30 dB et remplissage H bleu/V rouge ; `UC_PRTO.cs:1730`, `:5192`, `:5212` |
| Éditeur | Décalage électrique retiré puis réappliqué implicitement | Conservation des 360 échantillons, import/export exacts ; `UC_PRTO.cs:4338`, `:4481`, `:4913` |
| Rotation V | Sens opposé et ElectUg inchangé | Mutation atomique `new[i]=old[(i+delta+360)%360]`, ElectUg += delta ; `Module1.cs:1144`, `UC_PRTO.cs:3339` |
| Import | Zone remplacée par un carré ±150 m, couleurs et résultats perdus | Zone rectangulaire réelle, couleurs .NET, courbes de relief, masques et contours sauvegardés |
| CAO | Sept calques, formats/cartouche et centre approximatifs | Dix calques, A4 portrait, marges/cartouche natifs, centre de HorAreaCoordM ; `UC_CAD.cs:352`, `:378`, `:870` |
| CAO objets | Relief, textes et détails incomplets | Flèches 80 mm papier, bâtiments/hauteurs, annotations, points, origine et grille ; `UC_CAD.cs:587`, `:653`, `:713`, `:771` |
| Export CAO | Zoom/déplacement affectaient le dessin exporté ; DWG était un DXF | SVG/PDF gardent la feuille complète ; DXF papier commun au rendu ; vrais boutons DXF, DWG grisé |
| 3D | Perspective, terrain vert, brouillard, tours/panneaux inventés, H/V incomplets | Orthographique et cadrage, terrain blanc, bâtiments gris ; `UC_3D.cs:326`, `:447` |
| 3D contours | Courbes lissées et hauteurs H corrigées deux fois | Tracés exacts, régions fermées alpha 70/255 et coupes V ; `UC_3D.cs:334`, `:399` |
| 3D mâts/routes | Un pylône par source, routes flottantes | Uniquement annotations `Tower/...`, treillis dimensionné, routes rasterisées sur terrain ; `UC_3D.cs:270`, `:438`, `:582` |
| Commandes | Certains outils n’effectuaient que setStatus | Modèles Word, calibration par repères, cache/proxy/langue et DWG restent gris avec raison explicite |

Les chemins C# ci-dessus sont relatifs à
`reference/original-decompiled/EMFPlanner/` ; la copie de travail originale est
également dans `extracted/dumped/initial/decompiled/EMFPlanner/`.

## Données du scénario préservées

- Neuf sources, huit points de contrôle, quatorze définitions de coupe H et six V.
- Zone métrique X −137.252202172… à 153.618969825… ; Y −101.740810940… à 137.250395389….
- Quatre enveloppes H sauvegardées : 245, 193, 287 et 312 segments.
- Vingt-quatre contours V sauvegardés (six plans × quatre limites).
- Couleurs des limites : rouge, orange `#ff8000`, bleu et vert `#008000`.
- Les contours combinés H sont placés à la hauteur du premier plan (18 m),
  conformément à `FlagLastRezManyLimits` dans le code original.

Les contours importés portent `contours_only: true`. Leurs tableaux de champs
restent vides. La vue V l’indique, les tableaux numériques nécessitent un
recalcul, et les rapports PDF n’inventent pas un maximum égal à zéro.

## Limites et prochaines comparaisons

La navigation 3D et les dix calques CAO sont utilisables, mais cela ne valide
pas une identité pixel par pixel avec Eyeshot/ODA. Le cube 3D est une
approximation visuelle. La composition du fond en ligne en CAO et son insertion
en DXF ne sont pas réalisées. Les modèles Word et alias, le DWG natif, les
calibrations par repères et certains paramètres réseau restent inachevés.

Poursuivre par les résultats V (terrain/bâtiments/cotes et styles), les écrans
de cartes avec chaque masque d’annotation, puis les modèles et exports de
l’original. Ne pas réactiver une commande parce qu’elle ouvre seulement une
boîte de dialogue ou écrit un message de succès.

## Reprise du 22 octobre 2026

La session a été interrompue par la limite de session. Les fichiers modifiés
étaient toujours présents. La reprise corrige aussi le routage des dialogues
de propriétaires, coordonnées et fonds personnalisés, ainsi que la sélection
initiale de coupe V restaurée depuis la sauvegarde locale.

- `npm test` : 39 tests passent, dont les 15 références numériques originales.
- `npm run test:ui -- --output .tools/final-fidelity` : 18 parcours Edge passent.
- Le parcours d’import vérifie la conservation des quatre H et vingt-quatre V,
  leur réouverture, l’invalidation après changement de puissance et leur retour
  après annulation.
- Les exports CAO SVG/DXF/PDF et le PDF du tableau des coupes sont téléchargés
  et contrôlés par le parcours CAO. Les données numériques absentes restent vides.
- Captures du clone relues : `reference/castelfranco-interface/antenna.png`,
  `cad.png` et `scene-3d.png`.

Le projet ouvert avant reconstruction a été sauvegardé séparément dans
`extracted/scenario-tests/samples/Project Samples/Castelfranco Emilia TAV2.emfp`
(259 042 octets). Ses paramètres de source sont identiques à ceux du projet
original ; son fond sélectionné était Satellite Esri. Cette sauvegarde est
locale et n’écrase pas le fichier .xemf original.

Le contrôle du bureau via Computer Use est indisponible dans la nouvelle
session (`native pipe is unavailable`, erreur Windows 2). Les parcours visuels
ont donc été contrôlés dans Edge ; le lancement natif est vérifié séparément
par le processus de l’exécutable recompilé. Les archives de passation historiques
ne sont pas remplacées.
