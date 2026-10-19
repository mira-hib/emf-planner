# Extraction de données — Dernière passe exhaustive

## Résultat : 0 code métier, mais données utiles complètes extraites

---

## 1. feeders.txt — Base de données complète câbles coaxiaux

**36 types de câbles** avec atténuation (dB/100m) par fréquence (MHz) :

### Andrew FSJ (câbles flexibles)
| Câble | Diamètre | Fréquences |
|-------|----------|------------|
| FSJ1-50A | 1/4" | 30–10000 MHz (7 points) |
| FSJ2-50 | 3/8" | 30–10000 MHz (7 points) |
| FSJ4-50B | 1/2" | 30–10000 MHz (7 points) |

### Andrew EFX
| Câble | Diamètre | Fréquences |
|-------|----------|------------|
| EFX2-50 | 3/8" | 30–10000 MHz (7 points) |

### Andrew LDF (câbles semi-rigides)
| Câble | Diamètre | Fréquences |
|-------|----------|------------|
| LDF1-50 | 1/4" | 0.5–15800 MHz (11 points) |
| LDF2-50 | 3/8" | 0.5–12000 MHz (11 points) |
| LDF4-50A | 1/2" | 0.5–8800 MHz (13 points) |
| LDF5-50A | 7/8" | 0.5–5000 MHz (10 points) |
| LDF6-50 | 1-1/4" | 0.5–3000 MHz (8 points) |
| LDF7-50A | 1-5/8" | 0.5–2700 MHz (9 points) |
| LDF12-50 | 2-1/4" | 0.5–2200 MHz (8 points) |

### Andrew LCF
| Câble | Diamètre | Fréquences |
|-------|----------|------------|
| LCF14-50J | 1/4" | 0.5–15800 MHz (8 points) |
| LCF38-50J | 3/8" | 0.5–13500 MHz (10 points) |
| LCF12-50J | 1/2" | 0.5–8800 MHz (12 points) |
| LCF78-50J | 7/8" | 0.5–5000 MHz (11 points) |
| LCF114-50J | 1-1/4" | 0.5–3800 MHz (10 points) |
| LCF158-50J | 1-5/8" | 0.5–2750 MHz (7 points) |

### RG (câbles standards)
| Câble | Fréquences |
|-------|------------|
| RG-6/U | 1–3000 MHz (9 points) |
| RG-11/U | 1–3000 MHz (8 points) |

### Elliot EW (guides d'ondes)
| Câble | Bande | Fréquences |
|-------|-------|------------|
| EW17 | 1.7–2.4 GHz | 8 points |
| EW20 | 1.9–2.7 GHz | 9 points |
| EW28 | 2.6–3.4 GHz | 9 points |
| EW34 | 3.1–4.2 GHz | 12 points |
| EW37 | 3.3–4.3 GHz | 11 points |
| EW43 | 4.4–5.0 GHz | 2 points |
| EW52 | 4.6–6.4 GHz | 12 points |
| EW63 | 5.85–7.1 GHz | 12 points |
| EW64 | 5.3–7.75 GHz | 16 points |
| EW77 | 6.1–8.5 GHz | 16 points |
| EW85 | 7.7–9.8 GHz | 12 points |
| EW90 | 8.3–11.7 GHz | 12 points |
| EW127 | 10–13.25 GHz | 13 points |
| EW132 | 11–15.35 GHz | 14 points |
| EW180 | 14–19.7 GHz | 19 points |
| EW220 | 21–23.6 GHz | 14 points |
| EW240 | 22–26.5 GHz | 16 points |

**Total : 36 câbles, ~370 points de données fréquence/atténuation**

---

## 2. CustomBaseMaps.set — 13 fournisseurs de cartes

```
OpenStreetMap    http://a.tile.openstreetmap.org/[Z]/[X]/[Y].png
Carto Basemap    https://cartodb-basemaps-c.global.ssl.fastly.net/light_nolabels/[Z]/[X]/[Y]@2x.png
OSM Relief       http://a.tile.thunderforest.com/cycle/[Z]/[X]/[Y].png
Google Map       http://mt2.google.com/vt/lyrs=m@169000000&hl=en&x=[X]&y=[Y]&zoom=[17-Z]&s=Galile
Google Satellite http://khms2.googleapis.com/kh?v=930&src=app&x=[X]&y=[Y]&z=[Z]&s=
Esri Satellite   https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/[Z]/[Y]/[X].jpg
HERE WeGo Sat    https://3.aerial.maps.api.here.com/maptile/2.1/maptile/newest/satellite.day/[Z]/[X]/[Y]/256/png8?app_id=...
Mapquest Sat     https://c.tiles.mapbox.com/v4/mapquest.satellitenolabels/[Z]/[X]/[Y]@2x.png?access_token=...
OpenTopoMap      http://a.tile.opentopomap.org/[Z]/[X]/[Y].png
Bing Sat         http://ecn.t0.tiles.virtualearth.net/tiles/a[RES].jpeg?g=0
2GIS             http://tile1.maps.2gis.com/tiles?v=1112&x=[X]&y=[Y]&z=[Z]
Yandex map       https://core-renderer-tiles.maps.yandex.net/tiles?l=map&x=[X]&y=[Y]&z=[Z]
Yandex Sat       http://sat01.maps.yandex.net/tiles?l=sat&x=[X]&y=[Y]&z=[Z]&g=...
```

Placeholders : `[X]`, `[Y]`, `[Z]` = tuile x/y/zoom. `[RES]` = quadkey Bing. `[17-Z]` = zoom inversé Google.

---

## 3. Templates Excel — Modèle de données exact

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

**Auteur :** sorokin (développeur EMFPlanner)

### ControlPointsEN.xlt (10 colonnes)
1. Location
2. X (m)
3. Latitude (deg.)
4. Longitude (deg.)
5. Y (m)
6. Distance (m)
7. Azimuth (deg)
8. Height (m)
9. Attenuation (dB)
10. Measured E-field strength, V/m

---

## 4. Templates Word — Structure de rapport détaillée

### Colonnes du rapport Control Points (depuis Aliases.dot)
- Number point
- Azimuth (deg)
- X, m
- Y, m
- R, m
- Location
- Height above ground level
- **Measured E-field strength (V/m)**
- **Calculated E-field strength** (V/m)
- **Simultaneous exposure**
- Total E-field strength (V/m)
- Total eq. plane-wave power density (W/m²)

### Colonnes du rapport Radiating Sources (depuis {RadiatingSources_1}.dot)
- Radiating source name
- Type of service
- Frequency, MHz
- Transmitter power, W
- Number of TX

### Tableau E-field strength le long d'un azimut
Grille de calcul : distances R (m) vs hauteurs H (m)
- Hauteurs : 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 m
- Valeurs numériques présentes dans le template : 0.7151, 0.7915, 1.0188, 3.5687, etc.

### Coupes horizontales — Légende des zones
1. Inside buildings / space with people >4h (garden, terrace, balcony)
2. Space with people <4h (streets, open fields)
3. 75% of EMF Limits
4. Full safety value

### 14 alias de rapport disponibles
| Alias | Description |
|-------|-------------|
| `{CardinalPointsChart}` | Rose des vents avec azimut + nom secteur |
| `{ControlPoints_1}` | Tableau points de contrôle sans descriptions |
| `{ControlPoints_2}` | Tableau points de contrôle avec descriptions |
| `{EMF_Safety_Volumes1}` | Volumes de sécurité EMF |
| `{AntennaPatterns}` | Diagrammes d'antenne (A4 portrait) |
| `{HorizontalCutsА3_1}` | Coupe horizontale A3, avec échelle M1:1000 |
| `{HorizontalCutsА4_1}` | Coupe horizontale A4, sans échelle |
| `{VerticalCuts_1}` | Coupes verticales le long des azimuts |
| `{EFieldStrengthGraph_1}` | Graphique champ E le long des coupes verticales |
| `{EFieldStrengthVerticalCutTables_1}` | Tableaux champ E aux coupes verticales (A4 album) |
| `{SimultaneousExposureGraph_1}` | Graphique exposition simultanée |
| `{SimultaneousExposureVerticalCutTables_1}` | Tableaux exposition simultanée |
| `{RadiatingSources_1}` | Tableau sources rayonnantes |

---

## 5. Autres fichiers de données

### Принадлежность.txt — Opérateurs prédéfinis
```
ПАО "МТС"          (MTS)
ПАО "Мегафон"       (MegaFon)
ПАО "ВымпелКом"     (VimpelCom/Beeline)
ООО "Т2 Мобайл"     (T2 Mobile/Tele2)
ФГУП "РТРС"         (Russian TV & Radio Network)
```

### template/ — Images
- `Pattern.jpg` (1000×1000 N&B) : fond diagramme d'antenne (général)
- `Pattern ОЕ.jpg` (1000×1000 couleur) : fond diagramme d'antenne (occupational exposure)
- `ReportPatternDB_DEG.png` (1000×1000) : grille polaire dB/deg pour rapports
- `0.jpg`–`27.jpg` (100×16 chacune) : 28 icônes couleur de légende (PNG renommés .jpg)

### CurrentVersion.txt
`230131` — build date 2023-01-31

### EMFPlanner.exe.config
- .NET Framework 4.5.2, VB.NET (Microsoft.VisualBasic.Logging)
- 300 connexions HTTP simultanées max (tuiles de carte)
- Auteur du manifeste : `sorokin` (visible aussi dans les templates Excel)

---

## 6. Analyses binaires négatives (confirmé)

| Piste | Résultat |
|-------|----------|
| .NET BSJB dans l'exe | 0 occurrence — assembly chiffré dans le payload Enigma |
| PE embarqué dans l'exe | 0 occurrence |
| Overlay PE | 9824 octets = signature Authenticode PKCS#7 |
| AES S-box dans le binaire | Non trouvée (Enigma utilise une implémentation inline) |
| Strings EMF dans SECTION.4 | 0 — runtime Enigma uniquement |
| Low-entropy blocks | 38/939 blocs (4.0%) — tous contiennent des certificats, manifestes, ou runtime Enigma |
| Strings dans .NET décompilé | Tous les "field" sont FieldInfo (.NET reflection), "propagate" est un paramètre CAO |
| CopyModules.dll | C++/CLI stub vide (CRT init uniquement) |
| devDept.Eyeshot décompilé | 100% viewport 3D, 0% EMF |
| Inno Setup script | Aucun script embarqué exploitable |
| URLs dans le binaire | Que des certificats (Symantec, GlobalSign) |

### Signature numérique
- **Développeur** : "Center of Telecommunication Technologies LLC" (certificat GlobalSign EV Code Signing)
- **Auteur code** : sorokin (nom dans les métadonnées Excel/Word)
- **Protection** : Enigma Protector CA (certificat auto-signé, 2017–2027)
- **Build** : 2023-01-31
