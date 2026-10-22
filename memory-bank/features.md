# Fonctionnalités — état vérifié du clone

Mise à jour : 20 octobre 2026, après comparaison Windows de Castelfranco.

Le précédent état « implémentation complète » était trop affirmatif : plusieurs
commandes ouvraient une boîte ou annonçaient un succès sans réaliser la fonction.
Les commandes inachevées restent visibles et grisées. Le catalogue central est
`src/lib/capabilities.ts`.

| Domaine | Disponible | Partiel ou restant |
|---|---|---|
| Interface | 16 rubriques, icônes originales, grilles compactes, panneaux redimensionnables, annuler/rétablir | Comparaison visuelle complète de chaque écran |
| Projets | Import .xemf/.emfp/JSON, sauvegarde .emfp, copie locale | Écriture du format .xemf d’origine |
| Import .xemf | Sources, points, bâtiments, relief, lignes, coupes, limites ; zone rectangulaire, couleurs, réglages d’affichage et contours H/V enregistrés | Autres variantes de sérialisation et propriétés non encore mappées |
| Antennes | MSI, bibliothèque/éditeur, enveloppes, génération symétrique, câbles ; panneau 30 dB H/V rempli, six icônes, rotation native des échantillons avec ElectUg | Catalogue fabricant complet, lecture d’un dossier d’antennes |
| Calculs | Points, grilles H/V, quatre limites, terrain, bâtiments, volume 3D | Concordance globale non démontrée : 15 références point/hauteur sur un projet |
| Limites | Quatre tableaux éditables ; préréglages fournis dans le dépôt | Les préréglages ne constituent pas une validation réglementaire actuelle |
| Carte et dessin | Fonds Leaflet et plan local, zoom/déplacement/règle, calques, fonds personnalisés ; bâtiments, sommets, KML/OSM ; relief/triangulation ; lignes et groupes de points | Cache explicite et proxy gris ; fidélité des styles/annotations sur toutes les vues à poursuivre |
| Relief | Points et courbes, interpolation, import local HGT | Téléchargement automatique DEM/SRTM non implémenté |
| Coupes | Calculs H/V, intervalles, propriétés, tracés sauvegardés importés ; PDF du tableau des coupes | Rapports illustrés et Word des coupes ; les contours importés ne contiennent pas de champs numériques, un recalcul est nécessaire pour les tableaux |
| Rapports | DOCX/XLSX/PDF des sources et points, aperçu ; PDF des tableaux de coupes | Modèles Word, remplacement des alias et insertion des graphiques gris |
| CAO | A4 portrait/A3/A2, 30 échelles, dix calques persistés, zone réelle, grille cotée, bâtiments/relief/annotations, azimuts et points ; exports SVG/PDF/DXF papier | DWG natif grisé ; fond en ligne composé et images incorporées au DXF manquants |
| 3D | Orthographique, terrain blanc, bâtiments gris, seuls mâts TXT explicites, surfaces H remplies, coupes V, routes drapées, six vues, cadrage, zoom, déplacement, calques, volume calculé | Cube visuellement approximatif, tuiles dépendantes du réseau, PDF 3D grisé |
| Calibration | Image et emprise géographique éditable | Deux lignes, ligne+point, repères et pyramide de tuiles gris |
| Paramètres | Propriétaires, coordonnées DD/DM/DMS, fonds personnalisés, calculs | Dossier antennes, langues, cache et proxy gris |

Les résultats de Castelfranco sauvegardés par l’original sont des tracés, pas une
nouvelle simulation. Ils sont conservés avec cette distinction et invalidés dès
qu’une modification affecte les calculs. Les valeurs manquantes ne sont pas
présentées comme nulles.

Voir `castelfranco-fidelity.md` pour les écarts observés et les preuves de cette
session, et `reference/antenna-fidelity-audit.md` pour le détail des diagrammes.
