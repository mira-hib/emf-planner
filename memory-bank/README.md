# EMFPlanner — Memory Bank

Base de connaissances pour le projet de reverse engineering et reproduction d'EMFPlanner 7.1.

## Index

| Fichier | Description |
|---------|-------------|
| `architecture.md` | Architecture du logiciel original (PE, .NET, ODA, Enigma) |
| `features.md` | Liste des fonctionnalités — état du clone (58% fait) |
| `stack-decision.md` | Choix technologiques : Rust + Tauri + React |
| `progress.md` | Suivi d'avancement complet |
| `formulas.md` | Formules EMF extraites du manuel (ITU/ICNIRP) |
| `analysis-swigcore.md` | Analyse .NET SwigCoreMgd (1454 classes ODA) |
| `analysis-swigdb.md` | Analyse .NET SwigDbMgd (866 classes ODA) |
| `analysis-native-strings.md` | Strings natifs + données du manuel PDF |
| `rea-deep-analysis.md` | Analyse Ghidra/REA : Enigma Protector identifié |
| `rea-exhaustive-analysis.md` | 10 pistes épuisées, 3 découvertes de données |
| `rea-data-extraction.md` | Données extraites : 36 câbles, 13 cartes, templates |
| `guide-windows-unpacking.md` | Guide pas-à-pas : dump mémoire sur Windows (x64dbg, ExtremeDumper, dnSpy) |

## Verdict final

**Code métier EMF inaccessible** — protégé par Enigma Protector (AES, entropie 8.0).
**Reproduction fonctionnelle viable** — basée sur standards publics ITU/ICNIRP + manuel PDF 76 pages.
