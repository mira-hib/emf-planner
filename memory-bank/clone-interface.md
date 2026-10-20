# Clone Windows — interface et fonctions, 20 octobre 2026

## Reprise

L’utilisateur demande de reproduire l’interface et les fonctions de l’original,
et autorise explicitement les commandes grisées pour les fonctions encore non
implémentées. L’ancienne estimation « 58 % » ne décrit plus cette version.

L’interface React a été reconstruite avec les 16 rubriques des formulaires
`UC_*.cs` récupérés : sources, limites, origine, positions des antennes,
bâtiments, terrain, lignes, points, coupes horizontales, coupes verticales,
tableaux, rapport, CAO, 3D, calibration et paramètres.

**Mise à jour après affinage :** lire [interface-refinement.md](interface-refinement.md)
et [features.md](features.md). Bibliothèques/éditeur, câbles, sommets, groupes,
courbes de niveau et surface 3D sont désormais raccordés. Les icônes d’origine,
la densité des tableaux et les panneaux ont été affinés.

Les commandes secondaires sont centralisées dans
`src/components/FeatureTools.tsx`. Une commande non raccordée est désactivée,
grisée et porte l’infobulle « fonction pas encore implémentée ». Une commande
implémentée peut aussi être désactivée faute de sélection ou de données ; son
infobulle explique cette condition. Ne pas activer un bouton factice.

## Fonctions opérationnelles

| Rubrique | Comportement disponible |
|---|---|
| Fichiers | Nouveau, lecture JSON/.emfp, import binaire .xemf, sauvegarde .emfp, démonstration, copie de travail locale |
| Édition | Annuler/rétablir, raccourcis Ctrl+O/S/Z/Y, abandon du dessin, validation des cellules |
| Sources | Grille, copie/ordre, activation/couleurs/facteurs, EIRP, MSI/XLSX, bibliothèque/éditeur, enveloppes, génération symétrique, câbles/bretelles/connecteurs, puissance inverse |
| Limites | Quatre jeux éditables, intervalles de fréquences avec coefficient/exposant, couleurs et facteurs |
| Plan | Plan SVG hors ligne, déplacement/zoom/règle, fonds Leaflet, calques, import image avec emprise géographique |
| Origine/antennes | Coordonnées locales/géographiques, déplacement de l’origine avec recalage des objets, antennes à l’origine |
| Bâtiments | Dessin, propriétés, copie/ordre, base depuis terrain, écrans, sommets, translation, import KML |
| Terrain | Points et courbes de niveau, altitudes/sommets, copie/fermeture, interpolation et triangulation affichable |
| Lignes | Routes, clôtures, annotations, style, copie/suppression, nœuds, scission et translation |
| Points | Gestion/ordre des groupes, hauteurs/mesures, calcul/matrice, centrage, alignement sur azimuts et plage de hauteurs |
| Coupes H | Hauteurs d’antennes/intervalles/maxima, tri/couleurs/styles, deux grilles, quatre limites, coupe V par règle |
| Coupes V | Azimuts d’antennes/intervalles, dimensions, tri, deux grilles, limites, calcul sélectionné et ajustement aux résultats |
| Tableaux | Courbes champ/exposition selon distance et azimut, résultats des points |
| Rapports | Vrais DOCX/XLSX/PDF ; contenu sources+points ou points seuls ; prévisualisation |
| CAO | Papier A4/A3/A2, échelle, calques, export SVG/PDF et DXF incluant les contours calculés |
| 3D | Terrain/bâtiments/antennes, contours H, surface d’exposition à 100 %, navigation OrbitControls |
| Paramètres | Calculs, propriétaires, formats décimal/DM/DMS et fonds de carte personnalisés |

## Fonctions visibles mais non implémentées

Restent notamment gris : import .xlt, préréglages nationaux, équarrissage,
imports OSM, DEM/SRTM, icônes de lignes, génération automatique avancée des
coupes, cotes, rapports illustrés, alias/modèles Word, DWG, calibration par
références, proxy/cache et dossiers externes.

La sauvegarde binaire .xemf est grisée dans Fichier. Le clone lit le projet
original puis sauvegarde en .emfp. L’import DXF reconnaît LWPOLYLINE et TEXT ;
il ne représente pas un importeur CAO général. La surface EMF 3D utilise un
maillage approximatif réglable ; voir sa validation dans `interface-refinement.md`.
Les préréglages réglementaires désactivés ne sont pas des
normes actuelles validées.

## Moteur utilisé et concordance

Le moteur de cette interface est `src/lib/engine.ts`, partagé avec un Web Worker
annulable. Les anciens modules Rust et quatre anciens composants restent dans
le dépôt à titre de code antérieur ; ils ne pilotent plus cette interface.

Le port tient compte des diagrammes H/V à 360 échantillons, rotations,
inclinaisons, puissances et facteurs, limites par bande, hauteurs du terrain et
somme quadratique. L’import du projet original conserve les coordonnées et la
triangulation d’origine nécessaires au cas de référence.

Sur les **15 combinaisons point/hauteur d’un seul projet livré**, l’écart maximal
de champ avec l’affichage original à six décimales vaut
`9.945531114219364e-7 V/m`, sous la tolérance `1.1e-6`. Les pourcentages
d’exposition respectent la même tolérance absolue. Cela ne prouve pas la
concordance de toutes les branches, projets ou bibliothèques natives.

Références : `.tools/emf-scenarios/observed-results.json`,
`.tools/clone-validation/numeric-parity.json`,
`scripts/benchmark-clone.mts`, `tests/engine.test.ts`.

## Vérification et lancement

- `npm run build` : TypeScript et production Vite.
- `npm test` : 21 tests métier, dont import original, références numériques et maillage 3D.
- `npm run test:ui` : parcours Edge/Playwright, fichiers réellement téléchargés,
  calculs en Worker, navigation des 16 rubriques, CAO/3D, copie/annulation,
  commandes grisées et sélection du contenu des rapports.
- `npm run benchmark` : comparaison détaillée des 15 références.
- Exécutable : `src-tauri/target/release/emf-planner.exe`.
- Lanceur : `scripts/Start-EmfPlanner.ps1` ; option `-Development` pour Tauri dev.

Les tests utilisent maintenant le projet et les références versionnés dans
`tests/fixtures/`, afin de fonctionner aussi sur Mac après `git pull`.
Le code C# récupéré est versionné dans `reference/original-decompiled/`.
Voir `mac-handoff.md` pour l’archive complète des artefacts Windows.
Les captures de l’interface et exports sont dans `.tools/test-results/` ; ces
répertoires ne sont pas versionnés. Pour le premier build sous Windows,
dot-sourcer `scripts/Use-ReaTools.ps1`, définir RUSTUP_HOME/CARGO_HOME sur les
répertoires `.tools/rustup` et `.tools/cargo`, ajouter `.tools/cargo/bin` au PATH,
puis `npm run tauri build -- --no-bundle`.
