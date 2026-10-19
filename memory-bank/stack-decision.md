# Stack Technique — EMF Planner Clone

## Décision : Rust + React + Tauri

### Backend (Rust via Tauri)
- **Framework** : Tauri v2
- **Calculs EMF** : nalgebra, ndarray
- **Export CAO** : crate `dxf` (format DXF)
- **Sérialisation** : serde + serde_json
- **IDs** : uuid v4

### Frontend (React + TypeScript)
- **UI** : React 19 + TypeScript
- **Styles** : Tailwind CSS + CSS custom properties (light/dark)
- **Carte** : Leaflet via react-leaflet
- **Graphiques** : Recharts
- **Icônes** : Lucide React
- **Build** : Vite

### Pourquoi ce stack
1. **Performance** : Rust pour les calculs intensifs de propagation EMF
2. **Cross-platform** : Tauri compile nativement Mac/Win/Linux + mobile
3. **Taille légère** : ~10-30 Mo vs ~300 Mo si Python/Electron
4. **UI moderne** : React + Leaflet = cartographie web-native fluide
5. **Sécurité mémoire** : garantie par le compilateur Rust

### Structure du projet
```
appclone/
├── src/                     # Frontend React
│   ├── components/          # MapView, SourcePanel, Toolbar
│   ├── types/               # Interfaces TypeScript
│   ├── hooks/               # React hooks custom
│   └── utils/               # Utilitaires frontend
├── src-tauri/src/           # Backend Rust
│   ├── emf/                 # Calculs champ électrique
│   ├── antenna/             # Diagrammes d'antennes
│   ├── propagation/         # Modèles (Free-space, COST-Hata, ITU-R)
│   ├── project/             # Save/load projet JSON
│   └── export/              # Export DXF
```
