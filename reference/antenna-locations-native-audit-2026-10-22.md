# Antenna Locations — audit du 22 octobre 2026

## Référence et preuves

La rubrique suivante après Origin and Grid est `UC_Antennas`. Source récupérée :
`reference/original-decompiled/EMFPlanner/UC_Antennas.cs` (formulaire, propriétés et
gestionnaires) ; conversions dans `CalcCoords.cs:545–596`, déplacement à la souris
dans `Form1.cs:5099–5183`, réglage par défaut dans `Public2.cs:745`.

REA a réinspecté le type exact dans l’assemblage récupéré :
`ev_46208ed985c20ebe7be6ccb6c5b934a8df99d330558517de28c3102217e2ba95`,
vue `ev_ac3f0e3e48db8d8c9a59cc928e7520b99679b3c28fdfc371ca45f22d6951534f`.
Sélection trouvée, 20 champs, 48 méthodes, couverture complète de cette sélection,
sans problème de métadonnées. SHA-256 du DLL :
`c78fa27e6af274e3e5ba6129b59c91043525c630512e975719ecbd8deaa480e8`.
Le résultat est conservé dans `antenna-locations-native-evidence.json`.
L’inspection statique n’exécute pas le programme.

L’observation Windows a été tentée avec la compétence Computer Use : initialisation
`@oai/sky` puis `list_apps` ; échec « Computer Use native pipe is unavailable »,
erreur système 2. Aucune nouvelle capture ni action dans l’original n’est revendiquée.
Les captures de cette passe sont celles du clone dans Edge.

## Interface et comportement intégrés

- Sept colonnes dans l’ordre natif : Source name, Latitude, Longitude, X, Y, H,
  Azimuth. Nom accompagné du modèle d’antenne, coordonnées géographiques et
  paramètres techniques en lecture seule ; seules X/Y sont éditables.
- Cinq commandes dans l’ordre du formulaire, avec les icônes récupérées : centrer
  la carte, déplacer les antennes sélectionnées au centre, sur l’antenne courante,
  à l’origine, puis déplacer l’origine sur l’antenne courante. Les quatre commandes
  de modification demandent confirmation. Le bouton historique « toutes les
  antennes à l’origine » était incorrect : l’original ne déplace que la sélection.
- Sélection de lignes ou de cellules Ctrl/Maj, sélection de colonne par son en-tête,
  déplacement des colonnes par glisser-déposer, édition X/Y groupée limitée aux
  cellules de la même colonne, annuler/rétablir et sauvegarde
  `.emfp`. Les commandes préservent les sources non sélectionnées. Les propriétés
  de puissance, diagrammes, hauteur et azimut ne sont pas modifiées.
- Les éditions X/Y utilisent le facteur natif `CalcMashtWMtoMeters` pour établir
  les coordonnées WM. Les opérations de déplacement recalculent X/Y avec
  `nativeLocal` et l’arrondi au dixième. Le centrage utilise l’ancre géographique,
  pas les coordonnées calcul arrondies. Déplacer l’origine conserve les ancres.
- Maximum capture distance : curseur natif 1–30, défaut 15, import de
  `LengthAttractAntennas` et conservation dans le projet. Le nombre correspond
  à des dixièmes d’unité WM, pas à des mètres au sol.
- Déplacement direct d’une antenne sur le plan local et sur Leaflet. L’accrochage
  teste séparément `abs(dx) < Single(valeur/10)` et `abs(dy) < seuil` en WM,
  parcourt les sources dans leur ordre et prend la première autre antenne trouvée.
  Les antennes sélectionnées sont dessinées au-dessus aux positions superposées.
  Le réglage d’accrochage est une préférence d’affichage et ne supprime pas les
  contours calculés. Déplacer une antenne invalide les résultats.
- Panneau initial élargi à 510 pixels, hauteur de ligne 18 pixels et curseur en bas.
  Sa largeur redimensionnable est mémorisée indépendamment des autres rubriques.

## Validation et limites de fidélité

Tests dédiés : `tests/antenna-locations.test.ts` et
`tests/browser/antenna-locations.spec.ts`. Ils couvrent sélection, confirmations,
conversion native, bornes, accrochage, conservation des paramètres, import,
annulation, sauvegarde, glisser-déposer et ancrage géographique. Les parcours
utilisent un projet vide enrichi puis le véritable Castelfranco à neuf sources.
Les tuiles du parcours Leaflet sont interceptées pour rendre le geste reproductible.
La disponibilité réelle des fonds est vérifiée séparément dans l’audit des cartes.

La grille utilise les mêmes formats de coordonnées que les branches natives
`ConvLatToStr/ConvLonToStr`, y compris les préfixes S/W et leurs précisions DMS
différentes ; les valeurs Single de hauteur sont affichées avec sept chiffres
significatifs. Les cas limites d’arrondis décimaux .NET/JavaScript ne sont pas
exhaustivement comparés. Les interactions clavier complètes de WinForms ne sont
pas toutes reproduites. Le déplacement affiche un aperçu et applique
l’accrochage au relâchement, alors que le natif le fait pendant le geste. La
comparaison pixel par pixel avec l’écran original reste non vérifiée faute de
contrôle Windows. Cette passe n’est donc pas une déclaration de fidélité absolue.

Les totaux finaux des suites, la compilation et l’empreinte du binaire sont
consignés dans `antenna-locations-build.json` et `memory-bank/menu-reproduction.md`.
