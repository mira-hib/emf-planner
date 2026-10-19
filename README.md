# EMF Planner Clone

Outil de planification de champs électromagnétiques (EMF) — clone cross-platform d'EMFPlanner 7.1, construit avec Rust + Tauri + React.

Projet réalisé dans le cadre d'un article sur la faisabilité de reproduire un logiciel par décompilation.

## Stack technique

| Couche | Technologie |
|--------|------------|
| Backend | Rust (calculs EMF, propagation, feeders) |
| Desktop | Tauri v2 (cross-platform Mac/Windows/Linux) |
| Frontend | React + TypeScript + Vite |
| Carte | Leaflet (OpenStreetMap, Esri, Carto, OpenTopo) |
| CAO Export | DXF via `dxf` crate |

## Fonctionnalités implémentées

### Calculs EMF
- Champ E en espace libre : `E = √(30·EIRP) / d` (ITU-T K.70)
- EIRP avec nombre de TX et atténuation feeder
- PTXAM = P_TX × num_TX × atténuation × F_TDC × F_PP × α24
- Limites ICNIRP 2020 par bande de fréquence
- Exposition simultanée : `R = Σ(Ei/Limiti)²` avec verdict conforme/non-conforme
- Densité de puissance : `S = E²/377` (W/m²)
- Superposition quadratique multi-sources
- Coefficient de réflexion sol (k = 1.0–1.6)
- Modèles de propagation : Free-space, COST-Hata, ITU-R P.1546
- Grille de calcul pour heatmap
- Coupes verticales par azimut

### Données
- 36 types de câbles coaxiaux (~370 points fréquence/atténuation)
- 26 champs par source rayonnante
- 14 champs par point de contrôle avec évaluation automatique
- 7 fournisseurs de cartes avec layer switcher

### Interface
- Carte interactive (clic pour ajouter source ou point de contrôle)
- Sidebar avec onglets Sources / Points de contrôle
- Formulaire complet par source (identification, antenne, feeder, facteurs)
- Zones de sécurité circulaires sur la carte
- Popups détaillées (E calculé, ratio, conformité)
- Save/Open projet (.emfp) + Export DXF
- Dark/Light mode automatique

## Installation

### Prérequis
- [Rust](https://rustup.rs/) >= 1.75
- [Node.js](https://nodejs.org/) >= 18
- Dépendances Tauri : voir [tauri.app/start/prerequisites](https://v2.tauri.app/start/prerequisites/)

### Lancer en dev
```bash
npm install
npm run tauri dev
```

### Build production
```bash
npm run tauri build
```

## Architecture

```
appclone/
├── src/                          # Frontend React
│   ├── components/
│   │   ├── MapView.tsx           # Carte Leaflet + layer switcher
│   │   ├── SourcePanel.tsx       # CRUD sources rayonnantes
│   │   ├── ControlPointPanel.tsx # Points de contrôle
│   │   └── Toolbar.tsx           # Barre d'outils
│   ├── types/index.ts            # Interfaces TypeScript
│   ├── App.tsx                   # App principale + state
│   └── App.css                   # Styles (CSS variables, dark mode)
├── src-tauri/                    # Backend Rust
│   └── src/
│       ├── emf/mod.rs            # Calculs EMF (EIRP, PTXAM, champ E, ICNIRP)
│       ├── antenna/mod.rs        # Diagrammes d'antenne + interpolation
│       ├── propagation/mod.rs    # Modèles (Free-space, COST-Hata, ITU-R P.1546)
│       ├── feeder/mod.rs         # 36 câbles coaxiaux (Andrew, Elliot, RG)
│       ├── control_point/mod.rs  # Points de contrôle + évaluation
│       ├── project/mod.rs        # Save/Load JSON
│       ├── export/mod.rs         # Export DXF
│       └── lib.rs                # Commandes Tauri (15 commandes)
└── memory-bank/                  # Documentation reverse engineering
```

## Contexte : reverse engineering

Ce projet est né d'un article explorant la question : **peut-on reproduire un logiciel en le décompilant ?**

### Résultats de l'analyse d'EMFPlanner 7.1 :
- **955 000 lignes** de code .NET décompilé = 100% infrastructure (ODA/Teigha CAO)
- **0 formule EMF** dans le code décompilé
- Binaire protégé par **Enigma Protector** (AES, entropie 8.0)
- 3 passes REA/Ghidra : toutes les pistes épuisées

### Conclusion :
> La décompilation du code métier est impossible avec Enigma Protector.
> Mais la reproduction fonctionnelle est viable car toute la logique EMF repose sur des **standards publics** (ITU-T K.70, ITU-R F.699-8, ICNIRP 2020), documentés dans le manuel PDF de 76 pages.

## Standards implémentés

- **ITU-T K.70** (2020) — Méthode de source ponctuelle pour le champ E
- **ITU-R F.699-8** — Diagrammes d'antenne de référence
- **ICNIRP 2020** — Limites d'exposition du public général
- **COST-Hata** — Modèle de propagation urbain
- **ITU-R P.1546** — Propagation point-à-zone (simplifié)

## Licence

Projet éducatif — à des fins de recherche et d'article uniquement.
