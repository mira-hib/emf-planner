# Suivi d'avancement

État courant après affinage du 20 octobre : voir `interface-refinement.md` et
`features.md`. 21 tests métier, 14 parcours UI, compilation Windows release.
Les entrées ci-dessous conservent l’historique, dont des blocages ensuite résolus.

Affinage poussé sur `main` : `6f97972`. Compilation Windows finale de 6 680 576
octets, lancée avec succès. Archive supplémentaire de 3 690 894 octets publiée
sur la release de passation et vérifiée par téléchargement ; métadonnées dans
`reference/interface-build.json`. L’archive d’analyse antérieure est conservée.

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

### Outils installés sur Mac
- Node.js v26.7.0 + npm 11.19
- Rust 1.99.1
- Ghidra 11.3.2 + 12.1.3 (~/tools/)
- JDK Temurin 21 (~/tools/)
- .NET SDK 8.0.426 (~/tools/dotnet/)
- REA (rea-agents 6.7.0) + Ghidra 12.1.3 provider
- innoextract, unar, pefile, dnfile, unrar-cffi

### État : 35/60 fonctionnalités (58%)

## 2026-10-19 — Reprise Windows

- [x] Lecture complète du memory bank et identification du point de reprise : analyse dynamique Windows.
- [x] Installation portable des outils dans `.tools` (exclus de Git), voir `windows-toolchain.md`.
- [x] REA 6.7.0 + Node.js 24.22.0 + Ghidra 12.1.4 ; JDK 23.0.1 existant réutilisé.
- [x] x64dbg/Scylla, dnSpyEx, ExtremeDumper, PE-bear, Detect It Easy et innoextract.
- [x] SDK .NET 10.0.402, ILSpy CLI, environnement Python PE/.NET/RAR.
- [x] Serveur MCP et compétence REA configurés dans le profil Codex, avec sauvegardes.
- [x] Diagnostic final Codex/Ghidra/compétence : 12 contrôles réussis (`healthy: true`).
- [x] Serveur MCP testé séparément : 207 outils ; décompilation native Ghidra et .NET ILSpy réussies sur des DLLs de contrôle.
- [x] Lanceurs PowerShell et terminal dédié ajoutés dans `scripts/`.
- [ ] Redémarrer/reconnecter Codex pour charger REA dans la conversation suivante.
- [x] Archive `EMFPlanner.rar` apparue à la racine pendant l'installation ; inventaire UnRAR vérifié, installateur trial x64 230131 présent.
- [x] Extraire l'archive et l'installateur : 131 fichiers, dont les modules `.tx`/`.txv`.
- [x] Lancer l'original depuis le dossier extrait : fenêtre principale opérationnelle.
- [x] Capturer la mémoire du processus EMFPlanner avec DbgHelp après refus de lancement d'ExtremeDumper.
- [x] Reconstruire le payload PE/CLI `EMFPlanner` (17 416 192 octets).
- [x] Vérifier son identité avec REA : CIL .NET Framework, métadonnées complètes.
- [x] Exporter 137 fichiers C# avec ILSpy, dont les calculs `CalcSZZ` et les diagrammes `ClassDN`.
- [x] Inventorier 223 types, 3 530 méthodes, 2 556 champs ; 3 106 corps CIL présents, aucune anomalie de corps rapportée par REA.
- [x] Préserver l'original inchangé, sauvegarder les Evidence et créer `extracted/dumped/EMFPlanner-recovered.zip`.
- [x] Utiliser une copie du projet livré : calcul des 15 combinaisons point/hauteur, 14 coupes horizontales et six verticales.
- [x] Vérifier les vues CAO/3D et les exports Word, Excel et DWG ; consigner les résultats numériques de l'original.
- [x] Capturer à nouveau après scénarios : 30 images PE/CLI reconstruites, contre 14 au démarrage, avec les dépendances CAO/Office.
- [x] Comparer les tokens et corps CIL du module principal : 3 106 corps identiques, aucune méthode ajoutée/modifiée/perdue ; voir `windows-scenario-validation.md`.
- [x] Comparer des cas numériques de l'original et du clone avant de modifier les calculs : 15 références, voir `clone-interface.md`.

Le clone n'a pas été modifié ni recompilé pendant cette installation.

Le résultat Windows remplace le verdict général « code métier inaccessible »
des passes statiques Mac. Voir **`windows-dynamic-analysis.md`** pour reprendre.

## 2026-10-20 — Reconstruction de l’interface Windows

- [x] Remplacer le prototype à quatre composants par les 16 rubriques de l’original.
- [x] Ajouter les commandes complémentaires identifiées dans les formulaires décompilés ; griser les fonctions non implémentées, avec infobulles.
- [x] Lire les projets binaires .xemf sans exécuter leur contenu, puis sauvegarder en .emfp.
- [x] Porter le moteur commun React/Worker avec diagrammes H/V, limites et terrain ; comparer aux 15 références de l’original.
- [x] Ajouter les coupes H/V multi-limites, les résultats par source, les graphiques et les exports Office/PDF/DXF.
- [x] Ajouter le dessin de bâtiments/lignes, les points de terrain, la calibration d’image, la CAO et une scène 3D.
- [x] Installer Rust portable sur ce PC et compiler l’application native de développement.
- [x] Vérifier TypeScript/Vite, 10 tests métier et les parcours d’interface Edge.
- [x] Compiler l’exécutable Windows release autonome (6 586 880 octets).
- [x] Préparer la reprise Mac : export C# et références de test versionnés, configuration Playwright portable et archive de 389 artefacts avec manifeste SHA-256.

L’état précédent « 58 % » et les listes de blocages ci-dessus sont historiques.
Le moteur utilisé par la nouvelle interface est `src/lib/engine.ts` ; les
anciens modules Rust restent présents mais ne pilotent plus ces écrans.
Voir **`clone-interface.md`** et **`features.md`** pour l’état actuel et les
fonctions restant grisées.
