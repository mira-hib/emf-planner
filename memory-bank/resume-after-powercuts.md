# Reprise prioritaire après les coupures — 25 octobre 2026

La demande reste active : toutes les fonctionnalités du kit, comparaison REA,
builds Windows/Linux/macOS x64 et ARM64 + web, résultats dans `test validation`,
et lancement de la dernière version Windows. Ne pas remplacer cette demande
par une simple réponse de statut, ni assimiler des tests de code aux 216 scénarios.

## Résultats acquis

Mise à jour : **237 tests de code réussis** (`.tools/full-unit-final-237.log`),
65 parcours UI de régression + 1 parcours hors ligne réussi. ROB-01 teste les
28 chaînes du kit, jusqu'à 5 000 caractères, sans perte dans les fichiers/Word.
Index de couverture : 152 références partielles et 64 sans référence, toujours
pas une déclaration de réussite intégrale des 216 scénarios.

La charge UI réelle a terminé : 50 grilles calculées, conservées et sauvegardées,
navigation utilisable, sans erreur JS ; 1 014,61 s. Artefacts `stress-rob05-ui`.
Un relevé OS intermédiaire donne 1 793 196 032 octets privés pour les processus
du navigateur et du test ; ce n'est pas un pic global ni une limite imposée.

- **236/236 tests de code**, journal `.tools/full-unit-final.log`.
- **65/65 parcours UI**, journal `.tools/full-browser-final.log`.
- Les suites propres au kit comptent **143 tests de code et 28 parcours UI**.
  Windows x64, Windows ARM64 et Linux x64/ARM64 les ont passées en CI avec leurs
  builds et encodeurs natifs. Voir campagne intermédiaire 40237116754.
- Les deux projets natifs calculent leurs points existants et leur première
  coupe H/V à leur matrice importée : `native-samples-v2/results.json`.
- U3D, PDF3D et DWG R2000 réels ; géométrie 100x50mm relue par LibreDWG.
- Les 216 textes exacts sont catalogués. Index actuel : 149 références partielles,
  67 sans référence dans les suites. Ce ne sont pas 149 validations complètes.
- `reference/test-kit-full-audit-2026-10-25.md` détaille limites et écarts.

## Builds actuellement en cours

**Dernière campagne compacte :** branche `build-validation/20261025-compact`,
commit `e0a38164b52b30029a32397f78b96bf761c32341`, run
https://github.com/mira-hib/emf-planner/actions/runs/40241427422.
Les six cibles et ROB-05 ont **réussi en CI** : 144 tests du kit et 29 parcours
UI par cible. Les six archives ont été téléchargées et vérifiées (SHA256 global,
manifestes de fichiers). Les six contrôles d'architecture/paquets/convertisseurs
ont réussi. Les livrables sont dans `test validation/2026-10-25/builds` et les
rapports dans `test-kit`. L'archive globale est finalisée et vérifiée :
`test validation/emf-planner-builds-test-kit-2026-10-25.zip`.

Archive : 760 377 418 octets, SHA256
`75e5f555d590cd670e8d2c5ebac22644d10aea7dc85cae2e959b127b4f0e9ae8`.
2 489 fichiers au manifeste ; 2 491 entrées dans le ZIP, CRC vérifiés avant son
remplacement atomique. `reference/multiplatform-test-kit-builds-2026-10-25.json`
conserve le bilan de la livraison. Les campagnes précédentes sont préservées.
Elle ajoute les deux tests supplémentaires,
retire les symboles de debug des convertisseurs Unix et ne livre plus les
dossiers de préparation DEB/AppImage dupliqués. Tous les installateurs restent
produits. Les anciennes archives Linux dépassaient 600 Mo ; les téléchargements
de cette tentative ont été interrompus pour collecter les nouveaux livrables.
La campagne précédente ci-dessous est conservée pour diagnostic.

Le téléchargement utilise maintenant des blocs de 512 Kio, reprend les blocs
partiels et renouvelle les URL temporaires GitHub si elles expirent (401/403).
Le SHA256 de l'archive entière et le manifeste de chaque fichier extrait sont
vérifiés. `scripts/verify-validation-artifacts.py --root 'test validation/2026-10-25'`
vérifie ensuite architectures, signatures des paquets et convertisseurs.
N'archiver qu'après six vérifications réussies :
`python scripts/collect-validation.py --root 'test validation/2026-10-25' --archive`.

Branche isolée `build-validation/20261025-200009`, dernier commit
`ef431a6701791a3ddf6b7b7000d3a07620ed4c03`.
Run : https://github.com/mira-hib/emf-planner/actions/runs/40238835421

Six plateformes + une charge ROB-05 complète. Le run précédent a révélé deux
tests macOS utilisant Ctrl au lieu de Cmd. Les tests sont corrigés ; aucune
modification fonctionnelle de l'application ne vient de ce correctif.
Linux/macOS : le convertisseur ancien U3D compile avec C99/C++14, deux retouches
de compatibilité des en-têtes macOS, et ses vrais plug-ins dynamiques livrés.
Ces correctifs ont déjà permis les deux builds Linux. Attendre le nouveau bilan.

Commandes de suivi, sans afficher les identifiants Git :

```powershell
. ./scripts/Use-ReaTools.ps1
python .tools/sync-validation-ci.py --campaign 2026-10-25 --status-only
python .tools/sync-validation-ci.py --campaign 2026-10-25
```

Le téléchargement reprend par morceaux nommés avec l'ID d'artefact. Les archives
sont vérifiées par SHA256. Chaque plateforme garde `download-provenance.json`.
Les anciens fichiers sont déplacés dans `attempts`, jamais comptés comme récents.
Le collecteur daté `scripts/collect-validation.py` conserve aussi les charges,
exemples natifs et audits. Ne pas écraser la campagne du 24 octobre.

## Windows lancé

Build CI Windows x64 du commit `e0a38164b52b30029a32397f78b96bf761c32341`.

Lancement effectué depuis
`test validation/2026-10-25/launched/Windows-x64-e0a3816/emf-planner.exe`, avec
son dossier `native` adjacent. PID 33496 encore actif à la reprise, fenêtre native
présente (handle 461884 lors de cette seconde vérification).
SHA256 : `f9535893849cebd2a0312092e082784b236cea02b523ccbe1b0c9285ca3b94f3`.
`launch-result.json` conserve la preuve. Le contrôle visuel n'est pas établi :
le dernier appel Windows Computer Use a perdu son pipe natif après l'interruption.
Les PIDs et anciens handles ne sont pas réutilisables après une nouvelle coupure.

## Travail supplémentaire après la livraison e0a3816

26 octobre : sauvegarde native `.xemf` implémentée. Cinq tests ciblés et deux
parcours navigateur passent ; régression complète **242/242** réussie.
Interopérabilité vérifiée avec les types de données originaux sous .NET
Framework, y compris Bitmap/alias/couleurs, réenregistrement .NET et réimport.
Résultat `test validation/2026-10-26/xemf-interop/result.json`.
Fichier Fichier → Enregistrer au format original .xemf maintenant activé.
Source/méthode/limites : `reference/native-project-save-2026-10-26.md`.
Preuve conservée dans `reference/rea-xemf-write-evidence.json`.
Lancer la campagne six cibles du 26 octobre et collecter ses résultats ; ne pas
présenter cette implémentation comme déjà livrée dans les builds e0a3816 du 25.

## ROB-05

`stress-rob05-final` est une campagne fraîche ; elle reprend uniquement ses
propres reçus de même projet et même empreinte du code. Dernier état observé :
**50/50 coupes, zéro échec, calcul moteur achevé**. La commande ci-dessous reste
disponible pour vérifier/reprendre les reçus après une interruption :

```powershell
. ./scripts/Use-ReaTools.ps1
python scripts/prepare-kit-stress.py
node --import tsx scripts/validate-kit-stress.mts 'test validation/2026-10-25/stress-rob05-final'
```

La charge est aussi lancée en CI pour survivre à une coupure locale. Elle utilise
1000 sources, 2000 bâtiments, 50 hauteurs 0–49m, matrice1000 et quatre processus.
Hypothèse explicite : le modèle TSV « generic » reçoit sector65x7 du kit, gains
individuels conservés ; première limite seule. Temps et pics mémoire sont par
coupe. L'interface, les exports et le Gestionnaire des tâches restent séparés.

## Autres reprises interrompues

Les validations locales `local-kit` puis `local-kit-static` ont été interrompues
par les coupures ; leurs `summary.json` peuvent encore dire running alors que
les processus n'existent plus. La seconde avait passé 143 tests et 25 des 28 UI.
Utiliser les résultats CI complets pour le bilan, sans prétendre que ces deux
tentatives locales sont achevées. Le port Vite 1420 était aussi bloqué ; le
driver valide maintenant un snapshot statique sur un port dédié 1432.

## REA et travaux restants

L'assemblage original conserve son SHA256
`c78fa27e6af274e3e5ba6129b59c91043525c630512e975719ecbd8deaa480e8`.
REA a réanalysé UC_PRTO après perte de ses références en mémoire : bundle complet
de 2 enregistrements, 3 849 241 octets, dans
`test validation/2026-10-25/rea-sources-after-powercut.json`. Session fermée ensuite.
Le C# est dans `reference/original-decompiled/EMFPlanner`. Défauts initiaux des
sources corrigés selon `DGV1_UserAddedRow` : nom vide, Other, 1000MHz, P0/G0/H0,
taille1, Existing. Les calculs multi-fréquences ont des comparaisons indépendantes
avec le chemin point par point, sur quatre limites et trois modes de bâtiments.

À terminer/vérifier : conversions Office binaires réelles (Word COM a bloqué),
fond raster DXF/DWG, lecteurs tiers DWG/PDF3D, écriture .xemf native, tous les
écrans au pixel près, tests d'installation et bilan complet des 216 scénarios.
La licence/activation/horloge du logiciel commercial n'est pas à simuler.
