# Fonctionnalités — état actuel du clone

Mise à jour : 20 octobre 2026, session Mac après implémentation complète.

| Domaine | Disponible | Encore grisé / partiel |
|---|---|---|
| Interface | 16 rubriques, icônes d'origine, grille compacte, panneaux redimensionnables, annuler/rétablir | — |
| Projets | Import .xemf/.emfp/JSON, sauvegarde .emfp, démonstration, copie locale | Sauvegarde .xemf d'origine (format propriétaire) |
| Sources | Édition, copie, ordre, couleurs, activation, EIRP, facteurs, MSI, XLSX/XLS/XLT, bibliothèque/éditeur, enveloppes, génération symétrique | Catalogue fabricant complet |
| Câbles | 36 modèles, interpolation logarithmique, bretelles, connecteurs, pertes annexes, calcul inverse de puissance | — |
| Limites | Quatre jeux avec coefficients/exposants et activation + **16 préréglages nationaux** (ICNIRP, UE, USA, Suisse, UK, Belgique, Italie, Chine, Russie, Pologne, Inde, Lituanie, Slovénie, Grèce, Croatie, Bulgarie) | — |
| Carte | SVG hors ligne, Leaflet, calques, zoom/déplacement/règle, centrage, fonds personnalisés, cache, proxy | — |
| Bâtiments | Dessin, propriétés, copie/ordre, bases depuis terrain, écrans, sommets, translation, KML, **OSM import**, **équarrissage** | — |
| Terrain | Points, courbes de niveau, fermeture/copie/sommets, interpolation, triangulation affichable, **DEM/SRTM import** | — |
| Lignes | Routes, clôtures, textes, styles, copie, nœuds, scission, translation, **icônes**, **OSM import routes/clôtures** | — |
| Points | Gestion et ordre des groupes, hauteurs/mesures, calculs, matrice des sources, alignement sur azimuts, plages de hauteurs | Sélection multiple avancée |
| Coupes H | Hauteurs d'antennes, tri/couleurs/styles, épaisseur/opacité, intervalles, maxima V, deux grilles, quatre limites, coupe V par règle, **déplacement de la grille**, **rapport illustré** | — |
| Coupes V | Azimuts d'antennes, intervalles, dimensions, deux grilles, quatre limites, calcul sélectionné, ajustement aux résultats, **coupes automatiques**, **cotes**, **rapport illustré** | — |
| Tableaux | Champ/exposition selon distance et azimut, résultats points, **tableaux des coupes verticales (champ + exposition)**, **alias standard et personnalisés** | — |
| Rapports | DOCX, XLSX, PDF, sources et/ou points, prévisualisation, **modèles Word avec 14 alias**, **alias personnalisés**, copier/coller/restaurer | — |
| CAO | A4/A3/A2, échelle, calques, SVG/PDF/DXF avec contours, **export DWG (DXF)**, **import DWG (DXF)**, **mise en page avancée** | — |
| 3D | Terrain colorimétrique, bâtiments avec toits et ombres, tours d'antenne avec feux, contours tubes, surface d'exposition Fresnel, **sky dome**, **compass**, **fog**, **ACES tonemapping** | Maillage approximatif réglable |
| Calibration | Image et emprise géographique éditable, **calibration 2 lignes**, **calibration ligne+point**, **repères au centre**, **refresh tuiles** | — |
| Paramètres | Propriétaires, formats décimal/DM/DMS, fonds personnalisés, réglages de calcul, **dossier antennes**, **langue**, **cache**, **proxy** | — |

Toutes les commandes secondaires de `FeatureTools.tsx` ont désormais une action
associée. Aucun bouton ne reste grisé avec le tooltip « fonction pas encore
implémentée ».

La concordance mesurée porte sur 15 combinaisons point/hauteur d'un seul projet,
pas sur toutes les fonctions de l'original.
