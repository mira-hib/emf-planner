# Fonctionnalités — état actuel du clone

Mise à jour : 20 octobre 2026. Ce tableau remplace le comptage historique
35/60 (58 %) qui concernait le premier prototype Mac.

| Domaine | Disponible | Encore grisé / partiel |
|---|---|---|
| Interface | 16 rubriques, icônes d’origine, grille compacte, panneaux redimensionnables, annuler/rétablir | Comparaison des autres formulaires à poursuivre |
| Projets | Import .xemf/.emfp/JSON, sauvegarde .emfp, démonstration, copie locale | Sauvegarde .xemf d’origine |
| Sources | Édition, copie, ordre, couleurs, activation, EIRP, facteurs, MSI, XLSX, bibliothèque/éditeur, enveloppes, génération symétrique | .xlt, catalogue fabricant complet |
| Câbles | 36 modèles, interpolation logarithmique, bretelles, connecteurs, pertes annexes, calcul inverse de puissance | Dossiers externes |
| Limites | Quatre jeux avec coefficients/exposants et activation | Préréglages nationaux |
| Carte | SVG hors ligne, Leaflet, calques, zoom/déplacement/règle, centrage, fonds personnalisés | Cache/proxy avancés |
| Bâtiments | Dessin, propriétés, copie/ordre, bases depuis terrain, écrans, sommets, translation, KML | OSM, équarrissage |
| Terrain | Points, courbes de niveau, fermeture/copie/sommets, interpolation, triangulation affichable | DEM/SRTM |
| Lignes | Routes, clôtures, textes, styles, copie, nœuds, scission, translation | Icônes, import OSM |
| Points | Gestion et ordre des groupes, hauteurs/mesures, calculs, matrice des sources, alignement sur azimuts, plages de hauteurs | Sélection multiple avancée |
| Coupes H | Hauteurs d’antennes, tri/couleurs/styles, épaisseur/opacité, intervalles, maxima V, deux grilles, quatre limites, coupe V par règle | Déplacement de la grille, rapports illustrés |
| Coupes V | Azimuts d’antennes, intervalles, dimensions, deux grilles, quatre limites, calcul sélectionné, ajustement aux résultats | Recherche automatique avancée, cotes, rapports illustrés |
| Tableaux | Champ/exposition selon distance et azimut, résultats points | Tables des coupes verticales et alias Word |
| Rapports | DOCX, XLSX, PDF, sources et/ou points, prévisualisation | Modèles Word et alias personnalisés |
| CAO | A4/A3/A2, échelle, calques, SVG/PDF/DXF avec contours | DWG et import DXF général |
| 3D | Terrain, bâtiments, antennes, contours H et surface d’exposition à 100 % calculée en Worker | Maillage approximatif réglable ; concordance avec le maillage natif non validée |
| Calibration | Image et emprise géographique éditable | Calibration par lignes/points de référence |
| Paramètres | Propriétaires, formats décimal/DM/DMS, fonds personnalisés, réglages de calcul | Dossiers externes, langue, cache/proxy |

Les commandes non implémentées restent visibles et désactivées dans leurs
rubriques. Leurs libellés et états sont centralisés dans
`src/components/FeatureTools.tsx`. Voir [l’affinage](interface-refinement.md) et
[clone-interface.md](clone-interface.md) pour les limites, le moteur réellement
utilisé et les vérifications numériques.

La concordance mesurée porte sur 15 combinaisons point/hauteur d’un seul projet,
pas sur toutes les fonctions de l’original.
