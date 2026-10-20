# Fonctionnalités — état actuel du clone

Mise à jour : 20 octobre 2026. Ce tableau remplace le comptage historique
35/60 (58 %) qui concernait le premier prototype Mac.

| Domaine | Disponible | Encore grisé / partiel |
|---|---|---|
| Interface | 16 rubriques, tableaux, barres d’outils, paramètres, annuler/rétablir | Fidélité visuelle fine à poursuivre |
| Projets | Import .xemf/.emfp/JSON, sauvegarde .emfp, démonstration, copie locale | Sauvegarde .xemf d’origine |
| Sources | Édition, copie, ordre, couleurs, activation, EIRP, facteurs, diagrammes H/V, MSI, XLSX | Bibliothèques d’antennes/câbles, éditeur, enveloppes, .xlt |
| Limites | Quatre jeux avec coefficients/exposants et activation | Préréglages nationaux |
| Carte | SVG hors ligne, Leaflet, calques, zoom, déplacement, règle | Fonds personnalisés/cache/proxy avancés |
| Bâtiments | Dessin, propriétés, copie, ordre, bases depuis terrain, écrans | KML/OSM, édition des sommets et équarrissage |
| Terrain | Points, altitude, triangulation pour les calculs | Courbes de niveau, DEM/SRTM, affichage triangulé |
| Lignes | Routes, clôtures, textes, style et copie | Nœuds, scission, icônes, import OSM |
| Points | Groupes, hauteurs, mesures, atténuation, calculs, matrice des sources | Gestion avancée des groupes, alignement et plages automatiques |
| Coupes H | Hauteurs d’antennes, tri, couleurs, grilles 1/2, quatre limites | Intervalles, maxima, styles et rapports illustrés |
| Coupes V | Azimuts d’antennes, tri, dimensions, grilles 1/2, quatre limites | Intervalles, ajustement automatique, cotes et rapports illustrés |
| Tableaux | Champ/exposition selon distance et azimut, résultats points | Tables des coupes verticales et alias Word |
| Rapports | DOCX, XLSX, PDF, sources et/ou points, prévisualisation | Modèles Word et alias personnalisés |
| CAO | A4/A3/A2, échelle, calques, SVG/PDF/DXF avec contours | DWG et import DXF général |
| 3D | Terrain, bâtiments, antennes et contours empilés | Volumes de sécurité fermés |
| Calibration | Image et emprise géographique éditable | Calibration par lignes/points de référence |

Les commandes non implémentées restent visibles et désactivées dans leurs
rubriques. Leurs libellés et états sont centralisés dans
`src/components/FeatureTools.tsx`. Voir
[clone-interface.md](clone-interface.md) pour les limites, le moteur réellement
utilisé et les vérifications numériques.

La concordance mesurée porte sur 15 combinaisons point/hauteur d’un seul projet,
pas sur toutes les fonctions de l’original.
