# Sauvegarde native .xemf — 26 octobre 2026

Le menu Fichier permet désormais d'enregistrer au format `.xemf`. Le fichier
contient les structures binaires NRBF de l'original : réglages, sources et
diagrammes, facteurs, limites, points, bâtiments, relief, lignes, alias, coupes
et image PNG calibrée. La sauvegarde `.emfp` reste également disponible.

L'application écrit et lit ces données sans charger de types .NET ni exécuter
le contenu sérialisé. L'image est convertie en PNG et son calibrage est adapté
au repère isotrope du format d'origine. La précision supplémentaire et les
champs propres à cette reproduction sont conservés dans une extension après
la fin du flux natif, avec contrôles CRC du flux et de l'extension. Le lecteur
.NET ignore cette extension ; un réenregistrement par l'original la supprime.
La réimportation utilise alors les données et la précision du format natif.

## Preuves et portée

- REA : assemblage récupéré SHA256
  `c78fa27e6af274e3e5ba6129b59c91043525c630512e975719ecbd8deaa480e8`,
  MVID `1bed30fd-0970-42c5-b45b-a0f90a582022`. Structures Project, PrjSetup,
  TxPRTO, RasterPlan ; sélection des noms courts `Project`, `PrjSetup`,
  `TxPRTO`, `RasterPlan`, preuve
  `ev_383c318e0f234ca2c3aa80f11b237a993d3a3c4a10274c9d110716229a31ac01`.
  Les enregistrements complets ciblés sont conservés dans
  `rea-xemf-write-evidence.json`, extraits sans modifier leurs identités du
  bundle exporté avant la coupure. Ce sont des observations statiques.
- Oracle indépendant Windows/.NET Framework : le fichier créé est désérialisé
  avec les véritables types de données de l'assemblage original récupéré, puis
  réenregistré par le sérialiseur .NET et réimporté dans la reproduction.
  Les assertions couvrent couleur ARGB, diagrammes 360 points, alias Unicode,
  image 2 × 1 pixels avec contrôle du pixel rouge, tables et réglages.
  Commande : `scripts/validate-xemf-interop.py --assembly <assemblage> --output <dossier>`.
  Le helper accepte uniquement les données générées par ce workflow ; il ne
  sert pas de lecteur de fichiers utilisateurs non fiables.
- Huit tests de code ciblés : précision et intégrité du fichier, structures
  natives, image et emprise, contours H/V de Castelfranco, bornes des types,
  intervalles de limites indépendants, tableaux d'image supérieurs à 2 Mo,
  valeurs effectives des alias et accolades uniques dans le format original.
- Deux parcours réels du navigateur : menu, téléchargement binaire,
  réouverture, contours et image calibrée avec alias.
- Régression de code complète : **245/245** réussie, journal
  `.tools/full-unit-native-245.log`.
- Régression navigateur complète : **68/68** réussie, journal
  `.tools/full-browser-native.log`. Une première annotation des tests image
  mentionnait CAL-10 ; la correspondance correcte est CAL-07. Le contrôle des
  limites indépendantes relève de PRJ-01, pas de MET-03. Ces libellés sont
  corrigés dans les sources et l'index. Le contrôle supplémentaire des alias
  vérifie aussi leur affichage réel dans le panneau du rapport.
- Castelfranco enregistré via le menu du navigateur, relu puis réenregistré
  avec les types .NET originaux : 9 sources, 13 bâtiments, 4 résultats H,
  24 résultats V. Les 15 champs calculés aux points diffèrent au maximum de
  **2,78 × 10⁻¹⁷ V/m** de l'import initial.

Cette validation ne prouve pas l'ouverture visuelle dans la fenêtre originale,
ni la reproduction exacte de tous ses écrans. L'accès Windows Computer Use a
perdu son pipe après les coupures. Le pont Office historique, les fonds raster
DXF/DWG et les installations sur les six systèmes restent des points distincts.
Les 216 scénarios du guide ne sont pas déclarés tous validés.
