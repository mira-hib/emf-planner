# Analyse dynamique Windows — code métier récupéré

> Suite effectuée : calculs, coupes, CAO/3D et exports testés sur un projet livré.
> Les 3 106 corps CIL du module principal sont identiques après ces scénarios ;
> 16 assemblages supplémentaires ont été récupérés. Voir
> [windows-scenario-validation.md](windows-scenario-validation.md).

## Résultat du 2026-10-19

**Le payload .NET d'EMFPlanner a été récupéré après lancement sous Windows.**
REA reconnaît l'assemblage `EMFPlanner`, et ILSpy a exporté ses méthodes de calcul
en C# lisible. Le constat antérieur « code métier inaccessible » concernait
l'analyse statique sur Mac du fichier protégé sur disque.

L'export contient **137 fichiers C#**, dont **62 dans le dossier `EMFPlanner`**.
L'inventaire REA compte **223 types, 3 530 méthodes et 2 556 champs** :
3 106 méthodes ont un corps CIL présent ; 424 sont sans corps CIL déclaré.
REA ne signale aucune anomalie de corps dans cet inventaire. Cette distinction
ne permet pas de décrire les 424 déclarations comme des méthodes perdues.

## Artefacts à conserver

Chemins relatifs à `D:\projet perso\emf-planner` :

| Artefact | Chemin |
|---|---|
| Original préservé | `extracted/original/app/EMFPlanner.exe` |
| Capture mémoire complète accessible | `extracted/dumped/initial/EMFPlanner-p9876.dmp` |
| Assemblage récupéré, à ouvrir dans dnSpy/ILSpy | `extracted/dumped/initial/EMFPlanner.recovered.dll` |
| Export C# et ressources | `extracted/dumped/initial/decompiled/` |
| Archive transportable, sans le gros minidump | `extracted/dumped/EMFPlanner-recovered.zip` |
| Inventaire des images trouvées dans le dump | `extracted/dumped/initial/managed/inventory.json` |
| Synthèse machine et Evidence | `.tools/emf/recovery-summary.json` |

L'archive transportable fait **7 769 334 octets** et contient le DLL récupéré,
l'export C#, les identités et les Evidence ciblées. Son intégrité ZIP a été
vérifiée. Aucune de ces données n'a été envoyée à un service externe.

`extracted/`, `.tools/` et l'archive originale sont exclus de Git. Le clone
React/Rust n'a pas été modifié pendant cette analyse.

## Identités et provenance

| Objet | Taille | SHA-256 |
|---|---:|---|
| EXE original protégé | 3 847 264 | `e8bf6a479810dae8dba7cc022702691ad49153222b46d84f1614b5f836806226` |
| Minidump | 707 456 084 | `34b1bfaf8d4e8b7452f245c4458ae7e72fb4a2d9016b0af617e2b58b367effdb` |
| Assemblage récupéré | 17 416 192 | `c78fa27e6af274e3e5ba6129b59c91043525c630512e975719ecbd8deaa480e8` |
| ZIP de récupération | 7 769 334 | `95658ac7a87d795e4b31b0bff1b75edbd50ba085226e5988e26dd8376865643e` |

L'empreinte de l'original a été revérifiée après l'analyse : elle est inchangée.
Les signatures Authenticode du setup et de l'EXE original ont été vérifiées par Windows.

Identité .NET observée :

- Assembly : `EMFPlanner`, version **7.0.0.0** ; l'interface affiche **7.1 Trial**.
- Module : `EMFPlanner.exe`, x64, CIL, cible `.NET Framework 4.5.2`.
- MVID : `1bed30fd-0970-42c5-b45b-a0f90a582022`.
- Adresse de l'image récupérée dans cette capture : `0x7ff6c6af0000`.

Evidence REA sauvegardées :

- Original sans CLI sur disque : `ev_c5b0648c3b383c33ccf4f7f11c5df51d95765aeb1efffcca97f0d66b4261e925`
  (`.tools/emf-original-managed.json`).
- Identité managée récupérée, couverture complète de cette inspection :
  `ev_6fa327a1706bf285440770efbdf6a2483f6c67e00d9b4e9e3a7504e013c3e48c`
  (`.tools/emf/rea-managed-emf.json`).
- Inventaire complet des membres :
  `ev_429bafc08b177328e56ebf17b7b819f6d28c11252069b202d6897febc6085856`
  (`.tools/emf/rea-all-members.json`).
- Membres ciblés `CalcSZZ` et `ClassDN`, 23 méthodes avec corps présent et sans
  anomalie rapportée : `ev_e443982238f1fac8e4bc126927bde41c75d1ffd19f7dd8410f9cd5f1dbb8f342`
  (`.tools/emf/rea-calculation-members.json`).

## Procédure effectivement réalisée

1. Extraction du RAR avec UnRAR, puis du setup Inno 6.0.0 avec innoextract.
   **131 fichiers** extraits, y compris les modules `.tx`/`.txv`.
2. Inspection de l'original : PE x64, pas de répertoire CLI, sections de forte
   entropie ; cohérent avec le blocage statique décrit sur Mac.
3. Lancement de `extracted/original/app/EMFPlanner.exe` avec son dossier comme
   répertoire de travail. La fenêtre principale s'est affichée sans installation
   système supplémentaire. PID de cette session : **9876** (historique, à ne pas réutiliser aveuglément).
4. ExtremeDumper : lancement refusé par Windows avec « accès refusé », également
   hors du bac à sable. La cause précise n'a pas été établie. Aucun réglage de
   sécurité n'a été changé.
5. Capture avec `MiniDumpWriteDump` depuis un processus distinct, via
   `scripts/capture_emf_memory.py`. Le script vérifie le chemin de l'EXE avant
   de capturer le PID sélectionné et refuse d'écraser un fichier existant.
6. Analyse hors ligne du flux `Memory64ListStream` : **1 816 plages mémoire**,
   **124 candidats PE**, **14 images PE/CLI reconstruites**, dont `EMFPlanner`.
   Les autres images incluent essentiellement des bibliothèques du framework.
7. `scripts/extract_managed_from_dump.py` vérifie PE/CLI et la racine `BSJB`,
   distingue les dispositions mémoire/fichier puis reconstruit les sections.
   Une section non capturée provoque un refus de reconstruction, sans ajout
   silencieux de zéros. Aucune plage nécessaire au DLL EMFPlanner n'était manquante.
8. Inspection REA, décompilation ciblée `CalcSZZ`, export ILSpy du projet et
   inventaire REA de tous les membres. Export ILSpy terminé avec code de sortie
   0 et journal vide ; la recherche ciblée de marqueurs d'échec de décompilation
   n'a retourné aucun résultat dans les fichiers C# exportés.

Le serveur MCP n'était pas exposé dans cette conversation malgré son installation.
L'analyse a utilisé le **CLI REA installé**, avec les mêmes chemins Node/.NET
que dans `scripts/Use-ReaTools.ps1`. Aucune nouvelle configuration n'était nécessaire.

## Premiers points d'entrée utiles

Tous les chemins ci-dessous partent de `extracted/dumped/initial/decompiled/`.

| Fichier / méthode | Observation | Ligne indicative |
|---|---|---:|
| `EMFPlanner/CalcSZZ.cs`, `CalcTmp` | Préparation des sources, limites, diagrammes et facteurs | 84 |
| `CalcTmp` | Diagrammes en dB convertis en amplitude par `10^(-dB/20)`, 360 points | 245 |
| `CalcTmp` | Préfacteur de champ avec puissance, nombre de TX, gain et pertes | 251 |
| `CalcTmp` | Facteurs alpha24, Ftdc et Fpr activables séparément par limite | 265 |
| `CalcPPE` | Rotation par azimut et tilt, distance 3D, interpolation des diagrammes H/V, puis champ | 293 |
| `CalcPointKB` | Somme des contributions, atténuation locale et prise en compte des bâtiments | 516 |
| `CalcPointKB` | Ratio par source sur sa limite ; champ total par somme quadratique | 630 |
| `CalcVertPlaneEcran` | Coupes verticales | 829 |
| `CalcAllHorizPlane` / `CalcOneHorizPlane` | Grilles de coupes horizontales | 1309 / 1580 |
| `Vectorization` | Vectorisation des contours | 2105 |
| `EMFPlanner/ClassDN.cs` | Chargement MSI, interpolation et normalisation des diagrammes | 259, 483, 592 |
| `EMFPlanner/Public2.cs` | Structures de projet et paramètres | — |
| `EMFPlanner/Form1.cs` | Le filtre d'ouverture/enregistrement original utilise **`.xemf`** | 2257, 2331 |

Le calcul interne emploie le facteur **3.77** dans les conversions entre le
champ au carré et la variable `PPE`. L'interprétation comme une densité exprimée
en µW/cm² est cohérente avec ces conversions ; vérifier les libellés UI et des
cas numériques avant d'en faire une convention dans le clone.

## Limites et prochaine étape

- Il s'agit de **C# décompilé**, pas du source VB.NET original ni d'une
  recompilation vérifiée. Les noms de variables locaux sont en partie reconstruits.
- Le projet exporté n'a pas été compilé. Ses références `HintPath` générées à
  partir d'un chemin relatif devront être adaptées avant une tentative de build.
- Le code conserve des appels aux chaînes protégées Enigma. En particulier,
  `CalcSZZ.Thf73` initialise `Public2.BufSize` à l'aide d'un appel Enigma, et cette
  variable intervient dans le facteur de gain. Sa valeur n'a pas été établie ici ;
  ne pas la remplacer par une constante supposée.
- Le minidump contient toute la mémoire accessible avec les options choisies ;
  les régions inaccessibles peuvent être omises. Le processus tournait pendant
  la capture. La décompilation n'est pas une trace d'exécution des calculs.
- Les cas numériques d'EMFPlanner n'ont pas encore été comparés au clone.
- Aucune modification de licence, du trial ou des protections de l'EXE original
  n'a été effectuée. L'application a été laissée ouverte pour poursuivre les tests.

**Prochaine étape utile :** établir quelques cas de référence reproductibles
dans l'original, tracer `CalcTmp → CalcPPE → CalcPointKB`, puis comparer le clone
sur les diagrammes d'antenne, les facteurs par limite et les conventions d'unités.
Ne plus répéter les passes statiques sur le fichier chiffré original.

Documentation de l'API de capture :
[MiniDumpWriteDump](https://learn.microsoft.com/en-us/windows/win32/api/minidumpapiset/nf-minidumpapiset-minidumpwritedump),
[options MINIDUMP_TYPE](https://learn.microsoft.com/en-us/windows/win32/api/minidumpapiset/ne-minidumpapiset-minidump_type),
[Memory64List](https://learn.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_memory64_list).
