# Suivi d'avancement

## 2026-10-19 — Jour 1

### Phase 1 : Décompilation
- [x] Extraction du RAR (EMFPlanner.rar)
- [x] Identification : Inno Setup 6.0.0, Delphi/VB.NET, ODA SDK
- [x] Extraction des fichiers de l'installateur (innoextract) — 87 fichiers
- [x] Analyse PE : EMFPlanner.exe = PE32+ x86-64, protégé XOR 0x55
- [x] Décompilation .NET réussie (4 DLLs, 955k lignes C#)
- [x] Ghidra installé (11.3.2 + 12.1.3)
- [x] REA installé + Ghidra 12.1.3 configuré (8 checks verts)
- [x] Analyse du code décompilé (3 agents : SwigCoreMgd, SwigDbMgd, strings natifs)
- [x] Extraction des formules de calcul EMF (depuis le manuel PDF 76 pages)

### Phase 2 : Analyse REA/Ghidra (3 passes)
- [x] Passe 1 : Identification Enigma Protector (AES + XOR 0x55 stub)
- [x] 13 777 fonctions Ghidra = artefacts (bruit chiffré), 0 symboles, 0 chaînes EMF
- [x] Passe 2 : Analyse exhaustive 87 fichiers, XOR brute-force 0x00-0xFF = 0 résultat
- [x] 0 PE/.NET embarqué, 0 BSJB, overlay = signature Authenticode
- [x] Passe 3 : Extraction de données utiles
  - feeders.txt : 36 câbles, ~370 points atténuation
  - CustomBaseMaps.set : 13 fournisseurs de cartes
  - Templates Excel : 19 colonnes sources, 10 colonnes points de contrôle
  - Templates Word : 14 alias de rapport
  - Opérateurs russes prédéfinis, images template

### Conclusion décompilation
**0 formule EMF dans les 955k lignes .NET** — 100% infrastructure ODA/Teigha + devDept Eyeshot.
**Enigma Protector** — AES, entropie 8.0, 94.9% du binaire chiffré.
**Code métier inaccessible** sans exécution Windows + unpacking dynamique.
→ Reproduction basée sur **standards publics** (ITU-T K.70, ITU-R F.699-8, ICNIRP 2020) + **manuel PDF**.

### Phase 3 : Clone (appclone/)
- [x] Rust + Cargo installés
- [x] Projet Tauri v2 + React + TypeScript créé
- [x] Backend Rust (7 modules) : emf, antenna, propagation, project, export, feeder, control_point
- [x] Frontend React (4 composants) : MapView, SourcePanel, ControlPointPanel, Toolbar
- [x] 36 câbles feeders intégrés (données exactes de feeders.txt)
- [x] 26 champs RadiatingSource (tous les 19 Excel + 7 extras)
- [x] Points de contrôle : modèle, ajout par clic, évaluation auto
- [x] Limites ICNIRP 2020 par bande de fréquence
- [x] Exposition simultanée R = Σ(Ei/Limiti)²
- [x] Densité de puissance S = E²/377
- [x] PTXAM = P_TX × num_TX × attenuation × F_TDC × F_PP × α24
- [x] Grille de calcul (`calculate_field_grid`) — backend prêt pour heatmap
- [x] Coupes verticales (`calculate_vertical_cut`) — backend prêt
- [x] 7 fournisseurs de cartes avec layer switcher
- [x] Boutons Save/Open/Export connectés (dialog natif Tauri)
- [x] Onglets sidebar (Sources / Points de contrôle)
- [x] Compilation Rust OK (0 erreurs)
- [x] TypeScript OK (0 erreurs)
- [ ] Premier lancement (npm run tauri dev)
- [ ] Heatmap champ E sur la carte (commande backend prête)
- [ ] Graphique E vs distance
- [ ] Génération PDF des rapports

### Outils installés
- Node.js v26.7.0 + npm 11.19
- Rust 1.99.1
- Ghidra 11.3.2 + 12.1.3 (~/tools/)
- JDK Temurin 21 (~/tools/)
- .NET SDK 8.0.426 (~/tools/dotnet/)
- REA (rea-agents 6.7.0) + Ghidra 12.1.3 provider
- innoextract, unar, pefile, dnfile, unrar-cffi

### État : 35/60 fonctionnalités (58%)
