# Environnement d'analyse Windows — 2026-10-19

Installation portable dans `D:\projet perso\emf-planner\.tools`, exclue de Git.
Le Node.js système reste en place. Les commandes du projet utilisent leur propre
Node.js et leur propre SDK .NET ; le PATH du compte Windows n'a pas été modifié.

## Outils disponibles

| Outil | Version | Emplacement sous `.tools` |
|---|---|---|
| REA | 6.7.0 | `rea/node_modules/rea-agents` |
| Node.js LTS | 24.22.0 | `node/node-v24.22.0-win-x64` |
| Ghidra | 12.1.4 | `ghidra/ghidra_12.1.4_PUBLIC` |
| x64dbg + Scylla x64/x86 | 2026.05.27 | `x64dbg/release` |
| dnSpyEx | 6.6.0 | `dnspy` |
| ExtremeDumper | 4.0.0.1 | `extremedumper/ExtremeDumper.exe` (x64), `ExtremeDumper-x86.exe` |
| PE-bear | 0.7.2, Qt6 x64 | `pe-bear` |
| Detect It Easy | 3.21 | `die/die` |
| innoextract | 1.9 | `innoextract` |
| .NET SDK | 10.0.402 | `dotnet` |
| ILSpy CLI | 11.1.0.9782 | `dotnet-tools/ilspycmd.exe` |
| Python venv | Python du PC | `python/Scripts/python.exe` |
| Modules Python | pefile 2024.8.26, dnfile 0.18.0, rarfile 4.5 | `python` |

Réutilisés : JDK 23.0.1 x64 dans `C:\Program Files\Java\jdk-23`, .NET Framework
4.8 et UnRAR dans `C:\Program Files\WinRAR`. Le package Mac `unrar-cffi` n'a pas
pu être construit sous Windows ; `rarfile` et UnRAR le remplacent.

Les URLs, tailles et empreintes sont consignées dans `.tools/manifest.json`.
Les empreintes publiées de Node, Ghidra, x64dbg, dnSpyEx, PE-bear, DiE et .NET ont
été vérifiées. innoextract ne publie qu'une empreinte MD5 ; ExtremeDumper ne
publie pas d'empreinte sur cette release. Ces deux archives proviennent également
des sites/dépôts officiels ; le manifeste conserve l'empreinte calculée.

## Utilisation depuis PowerShell

Depuis la racine du projet :

```powershell
# REA avec tous les chemins nécessaires
.\scripts\rea.ps1 --version
.\scripts\rea.ps1 doctor --client codex --provider ghidra --skill --json

# Charger les outils dans le terminal actuel
. .\scripts\Use-ReaTools.ps1
ilspycmd --version
innoextract --version

# Ouvrir les interfaces graphiques, selon le besoin
.\scripts\Open-ReaTool.ps1 x64dbg
.\scripts\Open-ReaTool.ps1 extremedumper
.\scripts\Open-ReaTool.ps1 dnspy
.\scripts\Open-ReaTool.ps1 ghidra
.\scripts\Open-ReaTool.ps1 pe-bear
.\scripts\Open-ReaTool.ps1 die
```

Un double-clic sur `scripts/Start-ReaShell.cmd` ouvre un terminal avec ces outils.
Les scripts n'activent pas automatiquement les droits administrateur.

L'installateur reproductible est `scripts/install_rea_tools.py`. Il télécharge
les distributions officielles dans `.tools/downloads` puis les extrait localement.
Il ne configure pas les clients MCP ; l'enregistrement Codex a été fait par
`rea setup --client codex --yes` après inspection de son plan.

## Intégration Codex

- Configuration : `C:\Users\ibson\.codex\config.toml`, serveur `mcp_servers.rea`.
- Commande absolue vers le Node portable et `scripts/rea.mjs mcp`.
- Variables : Ghidra, Java, .NET local et `REA_ILSPY_CMD_PATH`.
- Sauvegardes : `config.toml.rea.backup` et `config.toml.rea-dotnet.backup`.
- Compétence : `C:\Users\ibson\.agents\skills\reverse-engineer-anything`.
- Les autres réglages Codex ont été conservés.

**Redémarrer Codex ou reconnecter son serveur MCP pour charger REA dans une
nouvelle session.** Le serveur a été testé séparément ; cela ne signifie pas que
la conversation déjà ouverte a rechargé son catalogue.

## Vérifications réalisées

- Diagnostic final : `healthy: true` pour Codex + Ghidra + compétence, 12 contrôles
  réussis. Rapport : `.tools/doctor-final.json`.
- Initialisation MCP et `tools/list` : serveur `rea` 6.7.0, **207 outils**.
- `binary_session` sans cible : succès.
- Décompilation native via MCP : compilation d'une petite DLL x64 de contrôle,
  `open_binary` avec Ghidra, puis `analyze_function("rea_add")`. Le pseudo-code
  restitue `left + right + 7`. Session fermée avec `close_binary`.
- Evidence native :
  `ev_ed22a747d5a3afa5a54c7125479f5639ceafef621730224b0fb765ff13a0ded7`,
  conservée dans `.tools/smoke/analyze_function.json`.
- Décompilation .NET : compilation d'une DLL .NET Framework de contrôle et
  récupération de sa méthode `Add` par ILSpy CLI.
- Imports Python `pefile`, `dnfile`, `rarfile` : succès.
- Versions CLI et syntaxe des lanceurs PowerShell : vérifiées.

Les interfaces graphiques ont été extraites et leurs exécutables localisés ;
elles n'ont pas été ouvertes pour cette installation. Les fonctionnalités
optionnelles REA pour d'autres plateformes (Hopper, IDA, Android, firmware)
ne sont pas nécessaires à cette reprise EMFPlanner. Leur absence peut rendre
`environment_healthy` faux lors d'un audit global ; le périmètre ci-dessus passe.

Les contrôles natifs REA ont nécessité une exécution hors du bac à sable Codex :
l'accès au chemin temporaire sous `C:\Users\ibson` était refusé dans le bac à
sable. Les commandes autorisées ont ensuite réussi ; les protections Windows
et les règles Codex n'ont pas été désactivées.

## Suite de l'analyse EMFPlanner

L'archive **`EMFPlanner.rar` est maintenant présente à la racine du projet**
(39 050 551 octets). Son inventaire a été vérifié avec UnRAR ; elle contient :

- `EMFPlanner/Setup_EMFPlanner_7.1trial_x64_230131.exe` ;
- le manuel, `Miscellaneous Files.zip` et `Project Samples.zip`.

L'archive a ensuite été extraite et l'application originale lancée. Son payload
.NET a été récupéré et décompilé : voir **[`windows-dynamic-analysis.md`](windows-dynamic-analysis.md)**.
Les vérifications d'installation présentées plus haut restent celles des DLLs
de contrôle ; le nouveau rapport distingue les résultats sur EMFPlanner.

Pour une nouvelle tentative, consulter le rapport effectif avant
[le guide Windows](guide-windows-unpacking.md). L'affichage de
l'interface de l'application ne garantit pas un déchiffrement complet, et des
chaînes lisibles ne suffisent pas à prouver qu'un dump est exploitable.

## Sources des distributions

[REA](https://github.com/morluto/rea),
[Ghidra](https://github.com/NationalSecurityAgency/ghidra/releases),
[Node.js](https://nodejs.org/dist/),
[x64dbg](https://github.com/x64dbg/x64dbg/releases),
[dnSpyEx](https://github.com/dnSpyEx/dnSpy/releases),
[ExtremeDumper](https://github.com/wwh1004/ExtremeDumper/releases),
[PE-bear](https://github.com/hasherezade/pe-bear/releases),
[Detect It Easy](https://github.com/horsicq/DIE-engine/releases),
[innoextract](https://constexpr.org/innoextract/),
[.NET](https://builds.dotnet.microsoft.com/dotnet/release-metadata/10.0/releases.json),
[configuration MCP Codex](https://developers.openai.com/codex/mcp/).
