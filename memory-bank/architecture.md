# Architecture d'EMFPlanner 7.1 (Original)

## Type d'application
- **Installateur** : Inno Setup 6.0.0
- **Type** : Application hybride native C++ / .NET Framework 4.5.2
- **Langage principal** : VB.NET (côté métier) + C++ (moteur CAO ODA)
- **Pont natif↔.NET** : SWIG (Simplified Wrapper and Interface Generator)

## Binaire principal
- `EMFPlanner.exe` — PE32+ x86-64, 3.7 Mo
- **Protégé** par packing XOR 0x55 (code auto-décryptant)
- Ghidra ne retrouve que 2 fonctions (le stub de déchiffrement)

## Composants .NET (décompilés avec succès)
| DLL | Lignes C# | Rôle |
|---|---|---|
| TD_SwigCoreMgd.dll | 438 043 | Wrapper .NET du moteur graphique ODA (géométrie, rendu) |
| TD_SwigDbMgd.dll | 339 487 | Wrapper .NET de la base de données dessin ODA (entités DWG) |
| PRC_SwigDbMgd.dll | 152 753 | Wrapper .NET pour le format PRC (3D PDF) |
| TD_SwigPublishMgd.dll | 25 035 | Wrapper .NET pour l'export/publication |
| CopyModules.dll | ? | Utilitaire de copie de modules |
| Newtonsoft.Json.dll | — | Sérialisation JSON (lib tierce) |

## Bibliothèques natives ODA (non décompilées)
~40 DLLs `TD_*_21.4_11.dll` — moteur Open Design Alliance :
- `TD_DbCore` — base de données dessin
- `TD_Ge` — géométrie
- `TD_Gi` — interface graphique
- `TD_Gs` — système graphique
- `TD_Root` — fondations
- `TD_PDFToolkit` — génération PDF
- `OdBrepModeler` — modélisation B-Rep
- `DwfCore` / `DwfToolkit` — format DWF

## Ressources embarquées
- 31 templates JPG/PNG (patterns, rapport)
- Fichiers `.dot` — templates de rapports (graphiques, tableaux)
- Fichiers `.xlt` — templates Excel (sources, points de contrôle)
- `CustomBaseMaps.set` — URLs des fonds de carte (OSM, Google, Esri, Bing, Yandex, 2GIS)

## Fonds de carte supportés
OpenStreetMap, Carto, OSM Relief, Google Map, Google Satellite, Esri Satellite, HERE WeGo, Mapquest, OpenTopoMap, Bing Sat, 2GIS, Yandex Map, Yandex Sat
