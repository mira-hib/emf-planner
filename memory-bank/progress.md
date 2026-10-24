# Suivi d'avancement

État courant du 22 octobre : campagne du kit ajoutée après les lots essentiels Sources,
EMF Limits et Origin and Grid ; voir `test-kit-validation.md`, `menu-reproduction.md`
et `features.md`. **159 tests métier et 37 parcours Edge validés**,
compilation TypeScript/Vite réussie. Les entrées antérieures
conservent les résultats et blocages historiques.

Campagne du kit : mesures ajoutées aux totaux, groupes inactifs, positions de points
dans les bâtiments et pertes par bandes, plages de hauteurs temporaires, KML/MSI,
copie entre origines et paramètres corrigés. **1 800 cas de la méthode originale
`CalcPPE` exécutée isolément**, écart relatif maximal 2,5830227192795853e-6 ;
les 15 références UI antérieures restent inchangées. Le guide contient 216 scénarios,
dont 65 partiellement rattachés à cette nouvelle suite ; aucune déclaration de
validation exhaustive. Voir `test-kit-validation.md` et
`reference/test-kit-audit-2026-10-22.md`. Compilation Windows finale réussie,
sans lancement ; empreinte et chemin dans `reference/test-kit-build.json`.
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

## 2026-10-22 — Reproduction rubrique par rubrique, premier lot Sources

L’utilisateur demande de traiter les éléments du menu un à un, en commençant par les fonctions essentielles. L’ordre des 16 rubriques et les écarts précis à poursuivre sont conservés dans **`menu-reproduction.md`**. La rubrique Sources n’est pas déclarée complète.

- Sélection et édition de plusieurs lignes, copies indépendantes, déplacements haut/bas/début/fin, tampon persistant et suppression confirmée.
- Bandes F/F2, service libre, bornes numériques natives principales, état Existing/New, couleurs et câbles importés conservés.
- Calculateur EIRP indépendant avec quatre unités et trois opérations ; facteurs conditionnels bornés à 0,1–1.
- Commandes de calcul des ouvertures ; enveloppe native, connecteurs exclus lorsque décochés, fréquence médiane des bandes et décodage MSI Windows-1251/UTF-8.
- XLT binaire historique grisé ; XLSX/XLTX disponible selon le schéma du clone.
- Correction d’un défaut de largeur du panneau après agrandissement/réduction rapide de la fenêtre.
- Vérifications : **63 tests unitaires**, **21 parcours Edge**, TypeScript/Vite réussis. Les 15 références originales restent stables. Capture relue dans `reference/sources-interface/`.

Les archives et exécutables historiques restent conservés. Ne pas fermer l’application native déjà ouverte sans sauvegarder son projet ; la nouvelle compilation utilise une cible de sortie distincte.

## 2026-10-22 — EMF Limits

- Lecture du memory bank et reprise explicite de la seconde rubrique selon l’ordre demandé.
- Audit des sources `UC_Limits`, `Public2.SetLimitsPRJ/SetLimitsAPP` et `CalcSZZ.CalcTmp`, conservé dans `reference/limits-native-audit-2026-10-22.md`.
- Table commune à neuf colonnes, quatre descriptions multilignes, couleurs/facteurs, graphique logarithmique et édition avec ajout/suppression/annuler/rétablir.
- 16 préréglages historiques exacts, remplacement confirmé des quatre jeux, préférences conservées ; comparaison exhaustive des valeurs et descriptions au C# par les tests.
- Bornes natives, cellules vides et jeux incomplets conservés à l’import/sauvegarde ; nouveaux projets alignés sur les valeurs initiales natives.
- Intervalles natifs sans tri ni extrapolation et repli historique pour jeux absents/incomplets ; les 15 références Castelfranco restent stables, écart maximal de champ `9.945531114219364e-7 V/m`.
- **72 tests unitaires réussis, 24 parcours Edge validés**, TypeScript/Vite réussis. La suite complète a donné 23/24 sous Node 22.16.0 ; le dernier parcours (import de la dépendance ExcelJS par le test de rapport) est passé seul sous Node portable 24.22.0, sans changement de code. Utiliser `. ./scripts/Use-ReaTools.ps1` avant les commandes de validation Windows.
- Captures Edge relues et versionnées dans `reference/limits-interface/` : Castelfranco, ICNIRP et fenêtre étroite. Aucune nouvelle observation dynamique de l’original pendant cette passe.
- Release Windows recompilée avec succès ; métadonnées et empreinte dans `reference/limits-menu-build.json`. Nouvel exécutable non lancé, session native existante conservée.

La portée exacte et les limites de fidélité sont dans `menu-reproduction.md`.
La prochaine rubrique prévue est **Origin and Grid**.

## 2026-10-22 — Origin and Grid

- Audit statique de `UC_Origin`, `frmAreaSize`, `CalcCoords`, des préférences et de la grille dessinée dans `Form1`, conservé dans `reference/origin-grid-native-audit-2026-10-22.md`.
- Origine en lecture seule avec altitude au dixième, déplacement par poignée ou centre de l’écran avec confirmation ; vue maintenue, coordonnées locales sources/points/coupes recalculées avec arrondi natif 0,1 m, sans déplacement des ancres géographiques.
- Coordonnées du site de rapport séparées de l’origine, copier origine, centrer site, conservation dans `.emfp`/import `PRTOCoordWGS` ; texte utilisé en aperçu/Word/PDF, métadonnée Excel et alias `{Coordinates}`.
- Quatre dimensions natives asymétriques avec bornes et marge WM de ±0,1 ; dialogue annulable, rectangle et poignées de redimensionnement, graduations dynamiques, emprise WM préservée après changement d’origine.
- Grilles horizontales natives de 50 à 1000 divisions, défaut 600/200, aspect de la matrice conservé et arrondi du petit axe ; mode métrique préservé pour les anciens `.emfp`. Les branches verticales restent à traiter avec la rubrique Coupes verticales.
- Règle initiale 50 m, deux extrémités déplaçables, distance au dixième et angle, désactivation par bascule.
- **81 tests unitaires et 28 parcours Edge validés sous Node 24.22.0**, TypeScript/Vite réussis. Les quatre nouveaux parcours ont ensuite été revérifiés après correction du helper d’ouverture : il accepte explicitement la confirmation et vérifie les neuf sources de Castelfranco, afin d’éviter un test passant sur le projet vide.
- Les 15 références numériques originales sont inchangées, écart maximal `9.945531114219364e-7 V/m`. L’égalité des coordonnées TX locales arrondies de la fixture est aussi testée.
- Captures effectivement importées puis relues dans `reference/origin-grid-interface/`. Aucune nouvelle observation dynamique du logiciel original. Détails et écarts dans `menu-reproduction.md`.
- Release Windows réussie : 6 711 808 octets, cible `x86_64-pc-windows-msvc`. Chemin et SHA-256 dans `reference/origin-grid-build.json` ; ancien chemin release par défaut conservé.

La prochaine rubrique prévue est **Antenna Locations**.

## 2026-10-22 — Lancement et synchronisation du dépôt

- Application lancée à la demande de l’utilisateur via `scripts/Start-EmfPlanner.ps1` : dernière release `src-tauri/target/x86_64-pc-windows-msvc/release/emf-planner.exe`, PID observé **35952**, fenêtre **EMF Planner — Clone**, `Responding=True`. Ce contrôle confirme le démarrage et la réponse de la fenêtre ; les 28 scénarios fonctionnels restent ceux vérifiés dans Edge.
- Empreinte de l’exécutable revérifiée avant synchronisation : `02C57E29A0E492F756EA9C61934300EDBD96046A3DC11F5EB34291EAEE4CAA5A`.
- Kit de tests présent dans `test/emf_test_kit/`, avec archive `test/emf_test_kit.zip`, guide, données et outils : ajouté au suivi Git conformément à la demande de pousser tous les changements. Le README du kit précise que ses valeurs attendues proviennent du manuel ou de formules indépendantes ; ses scénarios ne sont pas comptés parmi les 81 tests et 28 parcours validés.
- `.gitattributes` préserve les fichiers du kit à l’identique (`-text -whitespace`) : BOM, variantes CRLF et espaces intentionnels font partie des cas d’essai et ne doivent pas être normalisés par Git.
- Reprise : continuer **Antenna Locations** ; conserver les écarts des rubriques précédentes et ne pas fermer une session native contenant des modifications non sauvegardées.

## 2026-10-22 — Antenna Locations et catalogue cartographique

- Lecture du memory bank et reprise de la quatrième rubrique, selon l’ordre
  convenu. Réinspection REA de `UC_Antennas` (type trouvé, couverture complète de
  la sélection) ; formulaire C#, conversions et gestionnaires de glissement lus.
- Sept colonnes, lecture seule selon le natif, formats géographiques natifs,
  hauteur Single lisible, couleurs, cinq icônes/commandes, cellules/lignes/colonnes
  Ctrl/Maj et colonnes réordonnables. Éditions groupées limitées à la colonne.
- Correction des commandes « sélectionnées » qui déplaçaient toutes les sources ;
  confirmations, centrage par ancre géographique, conversion WM native et
  conservation des sources hors sélection. Curseur d’accrochage 1–30 importé et
  sauvegardé, premier voisin dans le rectangle WM natif. Déplacement SVG/Leaflet
  et priorité à l’antenne sélectionnée aux positions superposées. Icône Leaflet
  mémorisée pour conserver le geste pendant la sélection.
- Quatorze fonds dans l’ordre de la capture, dont Google Map/Satellite, Esri,
  Bing, 2GIS et Yandex ; marqueurs d’URL natifs, quadkeys et projection elliptique.
  Import conservant le fournisseur exact et configuration personnalisée même
  nom. Résolveur partagé 2D/3D, image calibrée sélectionnable et messages d’échec.
- Dix services ont renvoyé une image lors des sondes réseau. Google Satellite
  version 930 répond 404 : URL du même fournisseur corrigée et vérifiée. OSM
  Relief affiche un filigrane exigeant une clé ; HERE/Mapquest nécessitent une
  configuration utilisateur. Identifiants tiers de l’artefact non recopiés.
- **171 tests métier et 46 parcours Edge passants ensemble** sur les sources
  finales, TypeScript/Vite, benchmark et `git diff --check` réussis. Les 15
  références de champ restent à un écart maximal de `9.945531114219364e-7 V/m`.
  Cinq parcours Antenna Locations incluent le vrai Castelfranco à neuf sources et
  le geste Leaflet ; quatre parcours cartes complètent les sondes réseau.
- Release Windows finale compilée `x86_64-pc-windows-msvc`, sans lancement ni
  fermeture d’une application existante. Chemin, taille et SHA-256 conservés dans
  `reference/antenna-locations-build.json`. Captures Edge réellement relues dans
  `reference/antenna-locations-interface/`.
- Limites : Windows Computer Use échoue avec `native pipe`, erreur 2 ; pas de
  nouvelle capture originale. Raccourcis WinForms et arrondis limites non tous
  comparés, accrochage appliqué au relâchement. Audit détaillé dans
  `reference/antenna-locations-native-audit-2026-10-22.md`. Reprise suivante :
  **Buildings**, sans déclarer les écarts précédents achevés.
