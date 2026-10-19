# Fonctionnalités EMFPlanner — État du clone

## Statut : 🔴 À faire | 🟡 Partiel | 🟢 Fait

---

### 1. Cartographie
- 🟢 Affichage carte OpenStreetMap via Leaflet
- 🟢 Support multi-fournisseurs (OSM, Carto Light/Dark, OpenTopo, Esri Sat/Street/Topo)
- 🟢 Basculer entre fonds de carte (LayersControl)
- 🔴 Import fond de carte personnalisé

### 2. Sources Rayonnantes
- 🟢 Modèle de données complet (19+ champs : freq, puissance, gain, position, azimut, tilt, duty_cycle, F_PP, α24, k, service_type, num_tx, atténuation, modèle antenne, taille, downtilt méca/élec, beam width H/V, location, owner, sector_id)
- 🟢 Ajout par clic sur la carte
- 🟢 Panel CRUD (créer, lire, modifier, supprimer)
- 🟢 Facteurs d'atténuation PTXAM dans l'UI
- 🟢 Nombre de TX et atténuation totale dans le calcul EIRP
- 🔴 Import depuis fichier Excel (.xlt)
- 🔴 Diagrammes d'antennes personnalisés (struct Rust existe, pas d'UI)
- 🔴 Bibliothèque d'antennes prédéfinies

### 3. Calculs EMF (Backend Rust)
- 🟢 EIRP = P × num_TX × attenuation × 10^(G/10)
- 🟢 PTXAM = P_TX × num_TX × attenuation × F_TDC × F_PP × α24
- 🟢 Champ E en espace libre : E = √(30·EIRP) / d (ITU-T K.70)
- 🟢 Superposition de champs (somme quadratique √ΣEi²)
- 🟢 Zone de sécurité (distance pour limite ICNIRP)
- 🟢 Limites ICNIRP 2020 par bande de fréquence
- 🟢 Exposition simultanée R = Σ(Ei/Limiti)² avec verdict conforme
- 🟢 Coefficient de réflexion sol (k = 1.0–1.6)
- 🟢 Distance haversine (géodésique)
- 🟢 Pertes feeders/câbles (6 types, interpolation par fréquence)
- 🟡 Modèles de propagation — code Rust existe (Free-space, COST-Hata, ITU-R P.1546) mais pas connecté aux calculs
- 🔴 Prise en compte du diagramme d'antenne dans le calcul
- 🔴 Coupes horizontales (grille de calcul → heatmap)
- 🔴 Coupes verticales
- 🔴 Volumes de sécurité EMF 3D

### 4. Points de contrôle
- 🟢 Modèle de données (14 champs)
- 🟢 Ajout par clic sur la carte (onglet dédié)
- 🟢 Évaluation automatique (E calculé, ratio exposition, verdict)
- 🟢 Panel avec affichage résultats
- 🟢 Marqueurs verts sur la carte avec popup résultats
- 🔴 Saisie valeur E mesurée (comparaison terrain)
- 🔴 Export tableau points de contrôle

### 5. Visualisation
- 🟢 Marqueurs sources sur la carte
- 🟢 Cercles zones de sécurité (rayon calculé par ICNIRP)
- 🟢 Popup info source au clic (freq, puissance, gain, secteur, opérateur)
- 🟢 Marqueurs points de contrôle avec popup résultats
- 🟢 Badge conformité (OK/NC) sur les points de contrôle
- 🔴 Carte de chaleur (heatmap) du champ E
- 🔴 Graphique champ E vs distance
- 🔴 Diagramme polaire d'antenne
- 🔴 Graphique d'exposition simultanée

### 6. Rapports
- 🔴 Génération PDF
- 🔴 Templates de rapport personnalisables (12 alias connus)
- 🔴 Tableaux récapitulatifs des sources
- 🔴 Export graphiques dans le rapport

### 7. Import/Export
- 🟢 Save projet (.emfp/JSON) — bouton connecté avec dialog natif
- 🟢 Open projet (.emfp/JSON) — bouton connecté avec dialog natif
- 🟢 Export DXF — bouton connecté avec dialog natif
- 🔴 Import DWG/DXF
- 🔴 Import/Export Excel (.xlt avec 19 colonnes)
- 🔴 Export PDF

### 8. Interface
- 🟢 Toolbar (save, open, export, settings)
- 🟢 Panel latéral avec onglets (Sources / Points de contrôle)
- 🟢 Dark/Light mode (auto via prefers-color-scheme)
- 🟢 Layer switcher carte (7 fournisseurs)
- 🔴 Undo/Redo
- 🔴 Raccourcis clavier
- 🔴 Localisation (FR/EN/RU)
- 🔴 Panneau de paramètres (modèle propagation, norme EMF)

---

## Résumé

| Catégorie | 🟢 Fait | 🟡 Partiel | 🔴 À faire | Total |
|-----------|---------|-----------|-----------|-------|
| Cartographie | 3 | 0 | 1 | 4 |
| Sources | 5 | 0 | 3 | 8 |
| Calculs EMF | 10 | 1 | 3 | 14 |
| Points de contrôle | 5 | 0 | 2 | 7 |
| Visualisation | 5 | 0 | 4 | 9 |
| Rapports | 0 | 0 | 4 | 4 |
| Import/Export | 3 | 0 | 3 | 6 |
| Interface | 4 | 0 | 4 | 8 |
| **TOTAL** | **35** | **1** | **24** | **60** |

**58% fait, 2% partiel, 40% à faire**
