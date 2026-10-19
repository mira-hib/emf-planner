# Analyse Binaires Natifs + Manuel PDF

## Développeur
- **Center of Telecommunication Technologies LLC** (Russie)
- Anciennement "SanZone", 20+ ans de développement

## Formules clés (depuis le manuel, 76 pages)

### Puissance atténuée (eq.1)
```
PTXAM = P_TX × F_TDC × F_PP × α24
```
- `F_TDC` : facteur de cycle de service (1 pour FDD, ~0.75 pour TDD)
- `F_PP` : réduction statistique de puissance
- `α24` : atténuation moyenne 24h

### Limites d'exposition
```
Limit = E × F^A    (V/m)
```
- F = fréquence en MHz
- E et A varient par bande de fréquence et par standard

### Exposition simultanée
```
Σ (Ei / Limiti)² < 1
```
Somme pondérée par les limites fréquentielles — doit être < 1 pour conformité.

### Champ total
```
E_total = √(Σ Ei²)    (V/m)
```
Méthode source ponctuelle selon ITU-T K.70 (2020). Somme quadratique.

### Coefficient de réflexion
```
k = 1 à 1.6
```

### Volumes de sécurité
Parallélépipède : dimensions Lh, Lm1, Lm2, Lv

## Standards de référence
- **ITU-T K.70 (2020)** — méthode source ponctuelle
- **ITU-R F.699-8** — antennes à symétrie de révolution
- **ICNIRP** — limites d'exposition

## Modèle de données — Source Rayonnante (19 champs)
Name, Sector ID, Type of service, Frequency MHz, Transmitter power W, Number of TX, Total attenuation dB, Antenna model, Azimuth, Gain dBi, Height m, Antenna size m, Mech downtilt, Elec downtilt, BW H-plane, BW V-plane, Location (Tower/Roof/Facade), New/Existing, Owner

## Modèle de données — Point de Contrôle (13 champs)
Description, Lat/Lon, X/Y coords, Distance, Azimuth, Location, Height, Attenuation dB, Measured E-field V/m

## Templates de rapports
- `{RadiatingSources_1}` — tableau des sources
- `{AntennaPatterns}` — diagrammes polaires
- `{ControlPoints_1/2}` — points de contrôle
- `{EMF_Safety_Volumes1}` — volumes de sécurité
- `{HorizontalCutsA4_1/A3_1}` — coupes horizontales
- `{VerticalCuts_1}` — coupes verticales
- `{EFieldStrengthGraph_1}` — graphique champ E
- `{SimultaneousExposureGraph_1}` — exposition simultanée
- `{CardinalPointsChart}` — points cardinaux

## Templates visuels
- `Pattern.jpg` / `ReportPatternDB_DEG.png` : diagrammes polaires d'antenne (échelle dB 0-20, secteurs 30°)
- `0.jpg`–`27.jpg` : tuiles de gradient couleur 100×16px (palette heatmap EMF)

## Fonctionnalités identifiées (19 majeures)
### Phase 1 (MVP)
Map tiles, source CRUD, antenna patterns (MSI), calcul E basique, calculateur EIRP, convertisseur unités, origine+grille, placement antennes

### Phase 2 (Calculs)
Coupes V/H, calculs ponctuels, volumes de sécurité, exposition simultanée, multi-limites, facteurs d'atténuation puissance, calcul feeder

### Phase 3 (Environnement)
Bâtiments (OSM/KML/manuel, blindage/atténuation), terrain (DEM, contours, triangulation), coefficient de réflexion

### Phase 4 (Viz/Export)
Graphiques/tableaux, diagrammes polaires, export DXF, rapports Word avec aliases, visualisation 3D

### Phase 5 (Avancé)
Calibration carte, synthèse ITU-R F.699-8, enveloppe, multi-threading, save/load projet
