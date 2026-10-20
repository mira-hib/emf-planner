# Affinage visuel et outils — 20 octobre 2026

L’utilisateur autorise la poursuite autonome, l’implémentation des outils
réalisables et l’envoi sur GitHub à la fin. Les fonctions restantes restent
visibles en gris. Voir `features.md` pour le tableau actuel.

## Interface

Comparaison avec les écrans Sources et Coupes H de l’original et les formulaires
C# : navigation de 158 px, lignes compactes, panneaux de propriétés de 350 px,
outils locaux, sélection bleue pleine, espace de grille gris. Les panneaux
carte/coupe se redimensionnent à la souris ou au clavier.

Les polaires ont 0° à droite, H en bleu, V en rouge, anneaux d’atténuation et
superposition H+V. Les libellés d’antennes coïncidentes sont regroupés.
La reproduction pixel par pixel de tous les formulaires reste à poursuivre.

Les 169 PNG et leur provenance/empreinte sont dans `public/original-icons/`.
`scripts/extract-interface-icons.py` les reconstruit depuis les RESX versionnés,
sans désérialiser d’objets .NET.

## Outils raccordés

- Antennes : bibliothèque de projet et quatre modèles du projet de référence,
  MSI, éditeur H/V de 360 échantillons, enveloppe des gains absolus et génération
  symétrique selon `frmParabola.Gq`. Les modèles sont normalisés pour éviter
  d’appliquer deux fois l’inclinaison électrique.
- Câbles : 36 références, interpolation selon le logarithme de la fréquence
  (`frmCableCalc`), arrondis à trois décimales, bretelles `3+3`, connecteurs
  automatiques/manuels et pertes annexes. Montage enregistré dans `.emfp`.
  Calcul inverse par TX pour une cible au port ou en EIRP.
- Bâtiments/lignes : coordonnées des sommets, insertion/suppression, scission,
  translation, sélection sur le plan local ou Leaflet. KML avec hauteur/étages.
  Les polygones KML à trous sont refusés car le modèle ne les représente pas.
- Terrain : courbes ouvertes/fermées, altitudes, sommets, copie/suppression et
  triangulation affichable. Calcul et scène 3D partagent les triangles. Les
  courbes sont interpolées par leurs sommets, sans triangulation contrainte.
- Points : renommage/suppression/ordre des groupes, centrage/déplacement,
  projection près des azimuts avant, calcul d’un point par plage de hauteurs.
- Coupes : intervalles de hauteurs/angles, styles, épaisseur/opacité, hauteurs
  des contours V, décalage de leurs limites basses, réduction aux résultats,
  calcul sélectionné, coupe V suivant les extrémités de la règle.
- Paramètres : propriétaires, décimal/DM/DMS en saisie et affichage, fournisseurs
  de tuiles HTTP(S) personnalisés.

Les réglages d’affichage conservent les résultats. Le chargement d’un projet
réinitialise sélections, groupe, dessin et règle. Aucun bouton grisé factice.

## Surface EMF 3D

Le bouton « Volumes de sécurité EMF 3D » définit une boîte XY, deux altitudes
absolues et un pas. Le Worker échantillonne `exposureAt`, puis extrait la
surface du rapport d’exposition 1 par six tétraèdres par cellule. Three.js
l’affiche en transparence avec le jeu de limites et le pas. La grille inclut
les coordonnées et altitudes des antennes actives.

Calcul annulable ; maximum 220 000 nœuds, pas minimal 0,2 m et garde sur la
complexité. Une intersection détectée avec les bords est signalée. Une absence
de surface est annoncée à la résolution utilisée. Les lobes fins nécessitent
un pas plus fin.

Test analytique : antenne isotrope, sphère théorique de 10 m. Erreur radiale
inférieure à 12 cm au pas 1 m et à 4 cm au pas 0,5 m, avec une réduction de
l’erreur supérieure à trois. Ce cas vérifie la convergence du maillage ; il
ne prouve pas l’identité avec l’algorithme de volume natif.

## Validation et reprise

- `npm test` : 21 tests, dont les 15 références originales, câbles, diagrammes,
  coordonnées, relief et surface 3D.
- `npm run test:ui` : 14 parcours Edge/Playwright ; nouveaux outils, sauvegarde
  des réglages, styles conservant les contours et panneau à 1 040 px.
- `npm run benchmark` : écart maximal inchangé de 9,945531114219364e-7 V/m.
- `npm run tauri build -- --no-bundle` : compilation Windows release.

Les avertissements concernent la taille de certains chunks Vite et le code
Rust historique non utilisé. Les captures locales sont dans `.tools/test-results/`.
Les exports Word/Excel/PDF sont réellement téléchargés et relus par les tests.

Sur Mac : `git pull origin main`, `npm ci`, `npm run tauri dev`. Données et
icônes sont versionnées ; aucune extraction Windows n’est requise. Le rendu
et le build macOS restent à vérifier sur Mac. L’ancienne archive de passation
contient le précédent exécutable ; voir `mac-handoff.md` pour les versions.
