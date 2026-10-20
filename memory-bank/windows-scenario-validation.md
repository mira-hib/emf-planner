# Validation par utilisation de l’original sous Windows

## Résultat du 2026-10-19

L’original **EMF Planner 7.1 Trial** a été utilisé sur une copie du projet livré
`Castelfranco Emilia TAV2.xemf`. Les calculs ponctuels, les coupes, la CAO, la 3D
et les exports Word, Excel et DWG ont été exercés.

**Les 3 106 corps CIL de l’assemblage principal sont identiques entre la capture
au démarrage et la capture après ces scénarios.** Aucun corps de méthode de cet
assemblage n’a été ajouté, perdu ou modifié. Cela renforce la récupération du
code métier principal décrite dans `windows-dynamic-analysis.md`.

L’utilisation a aussi fait charger des dépendances supplémentaires. La capture
initiale seule ne couvrait donc pas tous les composants de l’application.
Ces observations portent sur les scénarios ci-dessous et ne prouvent pas une
couverture exhaustive de toutes les branches ou de tous les composants externes.

## Scénarios réellement exécutés

| Scénario | Résultat observé |
|---|---|
| Ouverture du projet fourni | 9 sources actives, bâtiments, relief, points et coupes chargés |
| Calcul de tous les points, limites 1 | Tableau de 15 combinaisons point/hauteur pour 8 points ; contributions des 9 sources |
| Coupes horizontales, grille 1 | 14 hauteurs, de 18 à 30 m avec 27,2 m ; quatre jeux de limites cochés ; contours recalculés en 7,451 s affichées |
| Coupes verticales, grille 1 | Six azimuts : 60°, 125°, 130°, 220°, 290°, 320° ; calcul achevé en 6,308 s affichées |
| Vue CAO | Plan A3 à 1:1000, calques, bâtiments et contours affichés |
| Export DWG | Fichier créé, 461 824 octets, en-tête `AC1021` ; pas de réouverture dans un logiciel CAO indépendant |
| Vue 3D | Bâtiments, pylône, plan de terrain et surfaces d’exposition affichés |
| Rapport Word `{ControlPoints_1}` | Document Word d’une page, 15 lignes de résultats numériques à six décimales |
| Export Excel des points | Classeur `ControlPointsEN1` ouvert et rempli avec les coordonnées et hauteurs ; affiché en lecture seule/mode de compatibilité |

Les fonds de carte contiennent des tuiles « API KEY REQUIRED ». La géométrie
locale et les résultats s’affichent néanmoins. Les commandes **Save / Save as du
projet sont désactivées dans cette version d’évaluation**. Aucun changement de
licence, de protection ou de réglage de sécurité n’a été réalisé.

Les documents Office générés ont été observés dans Word et Excel, sans sauvegarde
de fichiers Office. Les processus distincts `WINWORD` et `EXCEL` ont été vus
(PID historiques 19660 et 28656). Leurs mémoires ne font pas partie des captures
du seul processus EMFPlanner.

## Références numériques

Les valeurs suivantes proviennent du **rapport de l’original**, pas du clone.
Jeu de limites 1, projet fourni, neuf sources actives :

| Point | Hauteur au-dessus du sol, m | Champ total, V/m | Exposition simultanée, % |
|---|---:|---:|---:|
| 1.1.1 | 1,1 | 0,063538 | 0,001009 |
| 1.1.2 | 1,9 | 0,062489 | 0,000976 |
| 1.2.1 | 1,1 | 0,025225 | 0,000159 |
| 1.2.2 | 1,9 | 0,020072 | 0,000100 |
| 1.3.1 | 1,1 | 0,341606 | 0,029173 |
| 1.3.2 | 1,9 | 0,395011 | 0,039008 |
| 1.4.1 | 1,1 | 0,432815 | 0,046832 |
| 1.4.2 | 1,9 | 0,471055 | 0,055473 |
| 1.5.1 | 1,1 | 0,120968 | 0,003658 |
| 1.5.2 | 1,9 | 0,100006 | 0,002500 |
| 1.6.1 | 1,1 | 0,543768 | 0,073921 |
| 1.6.2 | 1,9 | 0,638786 | 0,102011 |
| 1.7.1 | 1,1 | 0,168588 | 0,007105 |
| 1.7.2 | 1,9 | 0,220221 | 0,012124 |
| 1.8 | 2 | 0,212670 | 0,011307 |

Les coordonnées, densités de puissance et paramètres des scénarios sont aussi
transcrits dans `.tools/emf-scenarios/observed-results.json`. Ces références
permettent de préparer une comparaison avec le clone ; **cette comparaison
numérique n’a pas encore été faite**. Elles ne constituent pas une validation
scientifique indépendante des calculs de l’original.

## Comparaison des captures

Après les scénarios, une seconde capture DbgHelp a été effectuée sur le PID
**9876**, dont le chemin d’exécutable a de nouveau été vérifié par le script.
Ce PID est historique et ne doit pas être réutilisé sans vérification.

| Mesure | Démarrage | Après scénarios |
|---|---:|---:|
| Taille du minidump, octets | 707 456 084 | 1 543 055 020 |
| Plages Memory64List | 1 816 | 3 775 |
| Candidats PE détectés | 124 | 296 |
| Images PE/CLI reconstruites | 14 | 30 |
| Modules du ModuleListStream | 110 | 265 |
| Types / méthodes / champs EMFPlanner | 223 / 3 530 / 2 556 | 223 / 3 530 / 2 556 |
| Corps CIL présents / absents déclarés | 3 106 / 424 | 3 106 / 424 |
| Anomalies ou instructions tronquées rapportées | 0 | 0 |

MVID identique : `1bed30fd-0970-42c5-b45b-a0f90a582022`. Les tokens, noms,
signatures, flags, empreintes CIL brutes et normalisées, en-têtes utiles et
régions d’exception de toutes les méthodes ont été comparés : **aucune différence**.
Les inventaires des types, champs et nombres de lignes des tables de métadonnées
sont également identiques.

Le SHA-256 global du PE reconstruit diffère entre les captures ; on ne s’en sert
donc pas comme comparaison du code. Le résultat ci-dessus repose sur les membres
et les corps CIL, avec le même MVID.

Les **16 noms d’assemblages supplémentaires** observés sont :

- CAO/3D : `devDept.Eyeshot.Control.Win.v2020`, son assemblage de ressources
  `devDept.Eyeshot.Control.Win.v2020.Resources.LMMJNEOP0FY`,
  `devDept.Eyeshot.Control.x64.Win.v2020`, `devDept.Geometry.v2020`,
  `devDept.Graphics.Win.v2020`, `devDept.Graphics.Shaders.v2020` ;
- ODA/interop : `TD_SwigCoreMgd`, `TD_SwigDbMgd`, `TD_SwigPublishMgd`, `PRC_SwigDbMgd` ;
- Office : `office`, `Microsoft.Office.Interop.Word`, `Microsoft.Office.Interop.Excel` ;
- framework : `System.Management`, `System.Web.Services` ;
- un assemblage nommé `License Authorization`, inventorié sans analyse de son
  fonctionnement ni attribution certaine à un composant.

Le ModuleListStream contient aussi des DLL natives et modules `.tx` ODA chargés
pendant les essais. L’inventaire complet est conservé dans le JSON de comparaison.
Aucun nouvel assemblage métier nommé EMFPlanner n’a été découvert dans cette
seconde capture. La qualification des dépendances ci-dessus repose sur leurs
noms et les modules livrés ; leur code n’a pas été entièrement audité.

## Artefacts et reproduction

Tous les chemins partent de la racine du dépôt et sont locaux :

- Capture : `extracted/dumped/after-scenarios/EMFPlanner-p9876.dmp` et son `.json`.
  SHA-256 : `972e534f33897f037169ca061c75f0bd0a8b44c9a736cf8a36f130a2339dc7d0`.
- Images reconstruites : `extracted/dumped/after-scenarios/managed/` ; inventaire
  `inventory.json`, dont l’image principale `image-00007ff6c6af0000-mapped.dll`.
  SHA-256 de cette image : `358188cc57c432319c0b687fd3e810b19a614355bc7ab59e475e8abf75a52e55`.
- Evidence REA après scénarios : `.tools/emf-scenarios/after-members.json`,
  `ev_43d2d41c1e517c11ae6fe7ec5ddf429a21b410dc3ab8d384700dc27ed3342053`.
- Comparaison : `.tools/emf-scenarios/capture-comparison.json`, produite par
  `scripts/compare_emf_captures.py`.
- Références UI : `.tools/emf-scenarios/observed-results.json`.
- Copie de projet : `extracted/scenario-tests/samples/Project Samples/Castelfranco Emilia TAV2.xemf`.
  SHA-256 : `bbc1b5dfe34eec33421e229f6080d79b5c067ad9055875b9b9b85d1b79ba79f6`.
- DWG : `extracted/scenario-tests/castelfranco-grid1.dwg`.
  SHA-256 : `7d52c2f34439e1a331cc440933664c194ac49b0b6222d04e04cacabebffcccfe`.

Le script de comparaison est hors ligne et refuse d’écraser sa sortie. Il lit
les deux minidumps, les inventaires de reconstruction et les deux Evidence REA.
Les erreurs `invalid compressed int` émises par dnfile pendant la reconstruction
de certaines images framework/NGen ne sont pas des anomalies de corps CIL du
module EMFPlanner : les inspections REA de ce dernier sont complètes et sans
anomalie rapportée.

## Audit statique complémentaire et limites restantes

L’audit des 137 fichiers C# et de l’inventaire REA répartit les 424 déclarations
sans corps en **93 déclarations natives, 327 membres COM Word et quatre méthodes
de délégué**. Aucun corps CIL métier déclaré manquant n’a été identifié.

La recherche bornée au code exporté n’a pas trouvé de `Assembly.Load`,
`LoadFrom`, `LoadFile`, `AssemblyResolve`, `LoadLibrary` ou `GetProcAddress`.
Cela n’exclut pas des chargements internes aux bibliothèques tierces ou à la
partie native protégée.

Points encore ouverts :

- `CalcTmp` appelle bien `Thf73` ; `Public2.BufSize`, utilisé dans le calcul de
  gain, dépend d’une lecture de chaîne protégée. Sa valeur n’a pas été vérifiée
  indépendamment à l’exécution.
- L’éditeur de diagrammes appelé depuis `UC_PRTO` est un exécutable externe ;
  ses éventuels algorithmes ne font pas partie des 137 fichiers récupérés.
- Import MSI, import Excel, autres modèles de rapport, autres projets, grille 2
  et branches de paramètres non choisies restent à tester selon les besoins.
- Les deux captures sont des instantanés de mémoire accessible, pas une trace
  exhaustive des appels. Une image déchargée entre les captures peut leur échapper.
- Le clone React/Rust n’a pas été modifié. La prochaine étape utile est de
  comparer ses calculs aux 15 résultats de référence, puis d’adapter le moteur
  à partir du code métier récupéré et des écarts observés.
