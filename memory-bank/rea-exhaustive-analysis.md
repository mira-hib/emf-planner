# Analyse exhaustive — Toutes les pistes explorées

## Verdict

**Aucune piste n'a permis d'extraire le code métier EMF.** L'analyse exhaustive de TOUS les 87 fichiers confirme que la logique EMF est inaccessible sans exécution Windows + unpacking dynamique d'Enigma Protector.

Cependant, **3 découvertes majeures** apportent des données utiles pour la reproduction.

---

## Découverte 1 : `feeders.txt` — Base de données câbles/feeders

Fichier texte en clair contenant les **pertes d'atténuation (dB/100m) par fréquence (MHz)** pour ~30 types de câbles coaxiaux :

| Catégorie | Exemples | Gamme fréq. |
|-----------|----------|-------------|
| Andrew FSJ | FSJ1-50A (1/4"), FSJ2-50 (3/8"), FSJ4-50B (1/2") | 30–10000 MHz |
| Andrew LDF | LDF1-50 → LDF12-50 (1/4" à 2-1/4") | 0.5–15800 MHz |
| Andrew LCF | LCF14 → LCF158 (1/4" à 1-5/8") | 0.5–15800 MHz |
| Andrew EFX | EFX2-50 (3/8") | 30–10000 MHz |
| Elliot EW | EW17 → EW90 (guides d'ondes) | 1700–13250 MHz |
| RG | RG-6/U, RG-11/U | 1–3000 MHz |

**Format :** `fréquence_MHz\tatténuation_dB_par_100m`, séparé par `-\nnom_câble diamètre`.

→ **Directement utilisable** pour calculer les pertes de feeder dans le clone.

---

## Découverte 2 : Templates Excel — Modèle de données exact

### RadiatingSourcesEN.xlt (19 colonnes)
1. Type of service
2. Frequency, MHz
3. Transmitter power, W
4. Number of TX
5. Total attenuation, dB
6. Transmitting antenna model
7. Antenna azimuth, deg
8. Antenna gain, dBi
9. Antenna height, m
10. Antenna size, m
11. Mechanical downtilt, deg
12. Electrical downtilt, deg
13. Antenna beam width H-plane, deg
14. Antenna beam width V-plane, deg
15. Location
16. New or existing
17. Owner
18. Sector ID
19. Radiating source name

### ControlPointsEN.xlt (10 colonnes)
1. Location
2. X (m)
3. Latitude (deg.)
4. Longitude (deg.)
5. Y (m)
6. Distance (m)
7. Azimuth (m→deg?)
8. Height (m)
9. Attenuation (dB)
10. Measured E-field strength, V/m

---

## Découverte 3 : Templates Word — Structure des rapports

### Aliases pour rapports personnalisés (12 templates .dot)
| Alias | Description |
|-------|-------------|
| `{CardinalPointsChart}` | Graphique des points cardinaux (flèche azimut + nom secteur) |
| `{ControlPoints_1}` | Tableau points de contrôle sans descriptions |
| `{ControlPoints_2}` | Tableau points de contrôle avec descriptions |
| `{EMF_Safety_Volumes1}` | Volumes de sécurité EMF |
| `{AntennaPatterns}` | Diagrammes d'antenne des sources actives (A4 portrait) |
| `{HorizontalCutsА3_1}` | Coupes horizontales A3 |
| `{HorizontalCutsА4_1}` | Coupes horizontales A4 |
| `{VerticalCuts_1}` | Coupes verticales |
| `{EFieldStrengthGraph_1}` | Graphique intensité champ E |
| `{EFieldStrengthVerticalCutTables_1}` | Tableaux champ E aux coupes verticales (A4 album) |
| `{SimultaneousExposureGraph_1}` | Graphique exposition simultanée |
| `{SimultaneousExposureVerticalCutTables_1}` | Tableaux exposition simultanée aux coupes verticales |
| `{RadiatingSources_1}` | Tableau des sources rayonnantes |

### Colonnes du rapport points de contrôle
Azimut (deg) | X, m | Y, m | R, m | Location | Height above ground level | Simultaneous exposure | Total E-field strength (V/m) | Total eq. plane-wave power density (W/m²)

---

## Données supplémentaires trouvées

### CustomBaseMaps.set — 13 fournisseurs de cartes
OSM, Carto, Thunderforest, Google Map, Google Satellite, Esri Satellite, HERE WeGo, Mapquest, OpenTopoMap, Bing Sat, 2GIS, Yandex map, Yandex Sat

### Принадлежность.txt — Opérateurs russes prédéfinis
ПАО "МТС", ПАО "Мегафон", ПАО "ВымпелКом", ООО "Т2 Мобайл", ФГУП "РТРС"

### template/ — Images de rapport
- `Pattern.jpg` (1000×1000 N&B) : fond de diagramme d'antenne
- `Pattern ОЕ.jpg` (1000×1000 couleur) : fond diagramme d'antenne (ОЕ = environnement occupationnel)
- `ReportPatternDB_DEG.png` (1000×1000) : grille polaire dB/deg pour rapports
- `0.jpg`–`27.jpg` (100×16) : icônes/légendes d'interface (PNG renommés en .jpg)

### CurrentVersion.txt
`230131` (version date: 2023-01-31)

### EMFPlanner.exe.config
- .NET Framework 4.5.2
- VB.NET (Microsoft.VisualBasic.Logging)
- 300 connexions HTTP max (pour tuiles de carte)

---

## Analyses négatives (pistes épuisées)

### 1. Toutes les DLLs sont de l'infrastructure
| DLL | Type | Contenu |
|-----|------|---------|
| TD_Swig*.dll, TD_*.dll | ODA/Teigha | Moteur CAO DWG/DXF |
| PRC_Swig*.dll | ODA/Teigha | Support 3D PDF/PRC |
| devDept.*.dll | Eyeshot v2020 | Viewport 3D WinForms (obfusqué) |
| CopyModules.dll | C++/CLI stub | Class1 vide, chargement modules ODA |
| Newtonsoft.Json.dll | JSON.NET v7 | Sérialisation JSON |
| DwfCore/Toolkit, W3dTk, WhipTk | Autodesk DWF | Lecture/écriture DWF |
| oless.dll | OLE streams | Fichiers OLE composés |
| pcre.dll | PCRE | Expressions régulières |

### 2. XOR brute-force 0x00–0xFF : 0 résultat
Aucune clé XOR unique ne révèle de chaînes EMF cohérentes. Les quelques "hits" (EMF, V/m, dBi) à des clés différentes sont des coïncidences statistiques dans des données chiffrées.

### 3. Pas de PE/.NET embarqué
- 0 signature BSJB dans le binaire (assemblage .NET entièrement chiffré)
- 0 en-tête PE embarqué valide
- Overlay = 9824 octets de signature Authenticode (PKCS#7)

### 4. Entropie
- 94.9% du binaire est chiffré (entropie > 6.5)
- 48 blocs de faible entropie = manifeste XML + runtime Enigma + certificats
- 0 données EMF dans les zones lisibles

### 5. Enigma Protector confirmé
- Certificats X.509 : "Enigma Protector CA" (2017-2027)
- Timestamping : Symantec SHA256, 2023-01-31
- Seuls imports directs : LoadLibraryA, GetProcAddress (résolution dynamique)
- Le .NET assembly est emballé à l'intérieur du payload chiffré AES

---

## Impact pour le clone

### Champs manquants dans notre RadiatingSource (à ajouter) :
- `service_type: String` (Type of service)
- `num_tx: u32` (Number of TX)
- `total_attenuation_db: f64` (Total attenuation, dB — inclut feeder loss)
- `antenna_model: String` (Transmitting antenna model)
- `antenna_size_m: f64` (Antenna size, m)
- `mechanical_downtilt_deg: f64` (Mechanical downtilt, deg)
- `electrical_downtilt_deg: f64` (Electrical downtilt, deg)
- `beam_width_h_deg: f64` (Antenna beam width H-plane, deg)
- `beam_width_v_deg: f64` (Antenna beam width V-plane, deg)
- `location: String` (Location description)
- `new_or_existing: String` (New or existing)
- `owner: String` (Owner / operator)
- `sector_id: String` (Sector ID)

### Données à intégrer :
1. **feeders.txt** → module `feeder/mod.rs` avec interpolation des pertes par fréquence
2. **CustomBaseMaps.set** → URLs des tuiles pour le layer switcher
3. **Templates Excel** → import/export Excel avec les bons en-têtes
4. **Opérateurs** → liste prédéfinie configurable
