# Origin and Grid — audit statique du 22 octobre 2026

Evidence: sources C# déjà décompilées de `reference/original-decompiled/EMFPlanner/`, principalement `UC_Origin.cs`, `frmAreaSize.cs`, `CalcCoords.cs`, `Public2.cs`, `Form1.cs`. Cet audit décrit EMFPlanner 7.1; aucune nouvelle décompilation ou exécution du binaire.

## Champs et commandes natifs

- `UC_Origin.cs:326-446`: groupe **Origin and Grid**, sous-groupes **The origin point coordinates**, **Site coordinates for the report {Coordinates}**, **Elevation at the origin point**. L'origine latitude/longitude et son altitude sont des labels; seules les coordonnées du site pour le rapport sont des champs éditables. L'altitude vient de `Triangul.FindHeightTriang` au point d'origine Web Mercator et s'affiche au dixième de mètre (`:495-500`).
- Centrer la carte sur l'origine: `cmdPosition_Click`, appel `MapPositionWM` (`UC_Origin.cs:470-474`). Déplacer l'origine au centre de la carte: confirmation, origine = `GetScreenCenterWM`, normalisation X si supérieur à 20037508.343, rafraîchissement des coordonnées locales de la zone, sources, points de contrôle et plans verticaux (`:476-491`).
- Coordonnées de rapport PRTO distinctes de l'origine. Validation au blur et à Entrée, reformatage à partir des nombres enregistrés; erreur = message critique et restauration de la valeur enregistrée (`UC_Origin.cs:503-567`). Boutons centrer la carte sur PRTO (`:569-572`) et copier l'origine vers PRTO (`:574-579`). Le déplacement ultérieur de l'origine ne copie pas automatiquement PRTO.
- `CalcCoords.cs:26-194`: saisie décimale, degrés/minutes ou degrés/minutes/secondes; séparateurs point/virgule et caractères de ponctuation convertis en espaces. Signe négatif seulement si premier caractère S/s/- (latitude), W/w/- (longitude). Composante degrés latitude `[0,80]`, longitude `[0,179]`; minutes et secondes `[0,59.999999]`; magnitude totale strictement positive. Particularité: zéro refusé; 80°59' passe alors que 80.99° ne passe pas la vérification de composante degrés.
- `CalcCoords.cs:274-350`: affichage selon `AppSetup.CoordinatesFormat` (0 décimal, 1 DMS, 2 degrés/minutes). Les variantes N/E conservent les lettres d'hémisphère.

## Rectangle de calcul et valeurs par défaut

- Dimensions indépendantes à gauche/droite/haut/bas de l'origine. `UC_Origin.cs:581-587` initialise le dialogue à partir des valeurs absolues des coins locaux, arrondies à l'entier. Le dialogue réinjecte ces valeurs au chargement (`frmAreaSize.cs:390-397`); les `50` du designer ne sont donc pas des valeurs initiales de projet.
- Bornes exactes: gauche et bas `[0,5000]` m, droite et haut `[10,5000]` m, bornes incluses (`frmAreaSize.cs:399-436`). Les valeurs décimales sont acceptées, pas d'obligation entière. `AreaSizeOK=false` au chargement, true au clic OK, les modifications ne sont appliquées au projet que si ce drapeau est vrai (`:390-397`, `:439-443`; `UC_Origin.cs:589`).
- Application en Web Mercator: coin bas-gauche = origine moins largeur gauche/échelle et hauteur bas/échelle, puis **moins 0.1 unité WM** sur X/Y; coin haut-droit = origine plus largeur droite/échelle et hauteur haut/échelle, puis **plus 0.1 unité WM** sur X/Y. Les deux coins locaux sont ensuite recalculés (`UC_Origin.cs:591-596`). Cette marge n'est pas 0.1 m au sol.
- Défaut projet réel: origine WM `(1608744,6462657)`, PRTO copié depuis origine, rectangle WM ±100 unités autour de l'origine (`Public2.cs:720-731`). Les paramètres de l'application peuvent remplacer origine, PRTO et rectangle (`:737-743`). Origine et grille visibles par défaut (`:754-755`). Les valeurs sont sauvegardées comme préférences d'application (`Form1_Public.cs:183-187`).
- Dans la rubrique Origin, contour du rectangle toujours dessiné, indépendamment de la case Grid. Nom trompeur `PenMagenta2`: sa couleur réelle est **DarkBlue**, épaisseur 2 (`Form1.cs:1725-1726`, `:4200-4203`). Grille interne et labels X/Y en mètres DarkBlue épaisseur 1, graduation dynamique via `RazmetkaOSI(...,8,13,...)`, limitée au rectangle (`:4204-4241`; algorithme `Module1.cs:1186-1216`).
- Les côtés se déplacent à la souris; chaque mouvement met à jour le coin WM/local. Au relâchement, si les côtés se sont croisés, les valeurs min/max sont échangées (`Form1.cs:5407-5462`).

## Grille affichée et grilles de calcul 1/2

Ce sont deux concepts distincts dans le natif. La case **Grid** est `ShowHorizPlaneScale` (`Form1.cs:7750-7756`, handler `:7170-7175`). Elle contrôle les graduations dessinées dans la zone et ne choisit pas la résolution numérique du calcul.

**Grid 1/2** se configurent dans Setup, groupe **Maximum matrix size for cut calculation** (`UC_Setup.cs:1052-1082`). Ce sont des nombres entiers de divisions du plus grand axe, `[50,1000]`, pas des pas en mètres. Les nombres 800/400 du designer sont remplacés au chargement (`:1115-1116`); vrais défauts projet **600/200** (`Public2.cs:732`). Lors du calcul le second axe est proportionnel aux dimensions de la zone, puis arrondi; la matrice contient divisions+1 points par axe (`CalcSZZ.cs:1342-1355`, `:1938-1960`). Choix des commandes de calcul: `UC_HorizPlanes.cs:1884-1904`, `UC_VertPlanes.cs:3504-3553`.

## Changement d'origine et système de coordonnées

- Les positions géographiques WM restent la référence immobile. Changer l'origine recalcule le rectangle local, les sources, les points de contrôle et les plans verticaux. L'origine se déplace aussi par glisser-déposer (zone de capture 16 pixels) ou commande contextuelle **Set Origin**; ces actions ne recentrent pas la vue (`Form1.cs:3952-3968`, `:4926-4962`). Pendant le glissement, seuls origine/zone sont recalculés, puis TX/CP/VP au relâchement.
- WM ↔ WGS: Mercator sphérique, rayon 6378137 m (`CalcCoords.cs:533-543`). Coordonnées locales WM → mètres: conversion du point en WGS, inverse sphérique `OGZ` depuis l'origine; X = distance × sin(azimut), Y = distance × cos(azimut) (`:545-587`). `OGZ` stocke distance et azimut dans des **Single**, donc leur précision est arrondie avant projection. Sources/points/plans sont arrondis au dixième (arrondi .NET ties-to-even), zone non arrondie (`:567-587`, `:598-649`).
- Conversion inverse locale → WM dans le natif est une approximation à échelle locale: origine WM + `(X,Y)/CalcMashtWMtoMeters` (`CalcCoords.cs:589-596`). L'échelle est X local d'un point situé à origine WM +1 sur X (`:652-657`). Ce n'est donc pas exactement l'inverse de la conversion sphérique précédente.

## Règle et sélection du centre

- La règle est un bouton bascule. À l'activation elle crée immédiatement deux extrémités situées à ±25 m horizontalement autour du centre courant de la carte (`Form1.cs:2747-2766`), soit une règle initiale d'environ 50 m. Deux points WM en mémoire de session, pas une polyligne persistante du projet (`Form1_Public.cs:42-44`).
- Distance = norme du vecteur WM × échelle à l'origine; angle = atan2(deltaX,deltaY), normalisé `[0,360)`, affichage distance et angle au dixième. Tracé noir 2 px sur fond blanc 4 px, anneau sur la première extrémité, flèche sur la seconde, cartouche blanc au milieu (`Form1.cs:4776-4812`).
- Chaque extrémité est déplaçable à la souris; capture 4 pixels (`Form1.cs:6791-6827`). Désactiver masque la règle; la réactivation réinitialise ses extrémités autour du nouveau centre.

## Écarts du clone observés avant correction de cette rubrique

- `MapWorkspace.tsx:426-486`: édition directe latitude/longitude origine (-85..85/-180..180), altitude à deux décimales, aucun groupe PRTO/rapport, aucun bouton copier origine/centrer rapport/déplacer origine au centre. Zone exprimée par demi-largeur symétrique `[5,20000]`, sans quatre dimensions natives.
- Les champs **Pas grille 1/2, m** (`MapWorkspace.tsx:458-469`) ne correspondent pas aux tailles de matrice natives et appartiennent à une autre rubrique native. Case d'affichage unique, mais le fond `PlannerMap.tsx:420-443` est une grille grise fixe 50 m sur toute la vue: pas de contour/labels locaux dynamiques dans la zone.
- `usePlanner.tsx:619-653` recalcule bien les objets au changement d'origine, mais via Mercator à échelle locale (`geometry.ts:8-44`) et recentre la vue. Avant correction, la zone `calculation_area_m` n'était pas reprojetée et restait donc attachée aux anciennes coordonnées locales. Les points locaux n'avaient pas l'arrondi Single puis 0.1 m du natif.
- Import: `import-xemf.ts:63` associait `display_options.grid` à `ShowCircles` (cercles concentriques), au lieu de `ShowHorizPlaneScale`.
- Règle: `usePlanner.tsx:514-531` conservait une polyligne issue de clics et donnait seulement sa longueur cumulée; manque activation immédiate 50 m, deux poignées, angle et bascule.

## Helper de parité ajouté dans cette session

`src/lib/native-coordinates.ts` expose `nativeLocal`, `nativeScale`, `mercatorPoint`, `mercatorGeo` sans dépendance cyclique ni modification de la géométrie générale. Les itérations de `OGZ` sont réduites à leur formule sphérique (flattening natif = 0), puis distance et azimut sont arrondis en Single. Cette simplification évite les cas dégénérés de type 0×NaN du code décompilé à l'équateur; elle n'affirme pas une reproduction des NaN accidentels historiques.

`tests/native-coordinates.test.ts`: quatre tests passent, notamment égalité exacte de toutes coordonnées TX arrondies enregistrées dans `original-sample.xemf` à partir des latitudes/longitudes importées, conversions Mercator, échelle WM +1 et cas midpoint 0.25→0.2 / 0.75→0.8. Aucun changement d'importeur ou de géométrie globale dans ce sous-travail.
