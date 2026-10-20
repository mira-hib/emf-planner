# Guide : Unpacking dynamique d'EMFPlanner sur Windows

**Capture réussie sur ce PC :** voir [`windows-dynamic-analysis.md`](windows-dynamic-analysis.md).
La méthode effective utilise DbgHelp et une reconstruction hors ligne après
refus de lancement d'ExtremeDumper ; le payload métier .NET a été récupéré.

**Reprise Windows du 2026-10-19 : les outils sont installés.** Consulter
[`windows-toolchain.md`](windows-toolchain.md) pour les chemins réels, les lanceurs,
les tests effectués et la configuration Codex. Le guide ci-dessous décrit les
tentatives à effectuer ; leur réussite dépend du contenu réellement présent en mémoire.

## Objectif
Extraire le code métier EMF chiffré par Enigma Protector en dumpant la mémoire du process après déchiffrement à l'exécution.

---

## Prérequis à installer

| Outil | Téléchargement | Rôle |
|-------|----------------|------|
| **EMFPlanner 7.1 Trial** | `Setup_EMFPlanner_7.1trial_x64_230131.exe` | L'application à analyser |
| **x64dbg** | https://x64dbg.com | Débogueur x64 (inclut Scylla) |
| **dnSpy** | https://github.com/dnSpy/dnSpy/releases | Décompilateur .NET (pour VB.NET/C#) |
| **ExtremeDumper** | https://github.com/wwh1004/ExtremeDumper/releases | Dump spécialisé .NET (alternative à Scylla) |
| **PE-bear** | https://github.com/hasherezade/pe-bear/releases | Inspecteur de fichiers PE |
| **Detect It Easy (DiE)** | https://github.com/horsicq/Detect-It-Easy/releases | Identifier le packer/compiler |

---

## Méthode A : Dump avec ExtremeDumper (la plus simple pour .NET)

EMFPlanner est du VB.NET (.NET Framework 4.5.2). ExtremeDumper est conçu pour dumper les assemblies .NET protégés.

### Étapes

1. **Installer EMFPlanner normalement** via le setup trial

2. **Lancer EMFPlanner** normalement (pas via le debugger)
   - Attendre que la fenêtre principale apparaisse
   - Une partie du code peut maintenant être déchiffrée en mémoire ; la présence de l'interface ne garantit pas un dump complet

3. **Lancer ExtremeDumper en administrateur**
   - Clic droit → Exécuter en tant qu'administrateur

4. **Trouver EMFPlanner dans la liste des processus**
   - Il apparaît comme `EMFPlanner.exe`

5. **Dump**
   - Clic droit sur `EMFPlanner.exe` → **Dump**
   - Ou pour plus de contrôle : clic droit → **Dump Modules** → sélectionner le module principal
   - Enregistrer le fichier dumpé (ex: `EMFPlanner_dumped.exe`)

6. **Ouvrir le dump avec dnSpy**
   - `dnSpy.exe` → File → Open → `EMFPlanner_dumped.exe`
   - Si ça marche → tu as le code VB.NET complet avec noms de classes et méthodes
   - Naviguer dans l'arbre des namespaces
   - Chercher : EMF, Field, Calcul, Source, Antenna, Safety, Zone, Propagation

7. **Exporter le code source**
   - Dans dnSpy : File → Export to Project
   - Ça crée un projet Visual Studio complet avec tout le code décompilé

---

## Méthode B : Dump avec x64dbg + Scylla (si ExtremeDumper échoue)

### Étapes

1. **Ouvrir x64dbg** (version x64, pas x32)
   - File → Open → `C:\Program Files\EMFPlanner\EMFPlanner.exe`

2. **Configurer les breakpoints anti-Enigma**
   ```
   Onglet "Symbols" → chercher ntdll.dll
   Breakpoint sur : NtProtectVirtualMemory
   ```
   Ou plus simple :
   ```
   Onglet "Command" (en bas) → taper :
   bp VirtualProtect
   bp VirtualAlloc
   ```

3. **Lancer l'exécution**
   - Appuyer sur F9 (Run) plusieurs fois
   - Chaque breakpoint = Enigma qui déchiffre une section
   - Continuer (F9) jusqu'à ce que la **fenêtre EMFPlanner apparaisse**
   - Vérifier ensuite le contenu du dump : certaines parties peuvent rester chiffrées ou virtualisées

4. **Suspendre le process**
   - Cliquer sur le bouton Pause (||) dans x64dbg
   - Ou appuyer sur F12

5. **Dump avec Scylla**
   - Menu : Plugins → Scylla
   - Dans Scylla :
     - "IAT Autosearch" → cliquer
     - "Get Imports" → cliquer
     - Vérifier que des imports sont trouvés
     - "Dump" → enregistrer comme `EMFPlanner_dump.exe`
     - "Fix Dump" → sélectionner le fichier dumpé → produit `EMFPlanner_dump_SCY.exe`

6. **Analyser le dump**
   - Essayer d'abord avec **dnSpy** (assembly .NET)
   - Si ça ne marche pas → ouvrir avec **Ghidra** (code natif)

---

## Méthode C : MegaDumper (alternative .NET)

1. Lancer EMFPlanner normalement
2. Lancer MegaDumper en admin
3. Trouver le process → clic droit → `.NET Dump`
4. Il extrait tous les modules .NET séparément

---

## Que chercher dans le code dumpé

### Dans dnSpy (si .NET) — chercher ces termes :

```
EMF, ElectricField, FieldStrength, FieldCalculation
SafetyZone, SafetyDistance, SafetyVolume
RadiatingSource, TransmitterSource, Antenna
EIRP, PTXAM, PowerDensity
Propagation, FreeSpace, CostHata, P1546
ICNIRP, ExposureLimit, ExposureRatio
SimultaneousExposure, TotalField
HorizontalCut, VerticalCut, Heatmap
ControlPoint, MeasuredField
FrequencyMHz, PowerW, GainDBi
```

### Structure attendue (basée sur le manuel)

Le code VB.NET devrait contenir :
- **Classe RadiatingSource** avec ~19 propriétés
- **Classe ControlPoint** avec ~10-13 propriétés
- **Module de calcul** avec les formules :
  - `PTXAM = P_TX * F_TDC * F_PP * alpha24`
  - `E = sqrt(30 * EIRP) / d`
  - `R = sum((Ei/Limiti)^2)` (exposition simultanée)
- **Module de propagation** (Free-space, COST-Hata, ITU-R P.1546)
- **Module de rendu** (coupes horizontales, verticales, volumes 3D)
- **Module de rapport** (génération Word avec les 14 alias de template)

---

## Après le dump : ramener sur Mac pour analyse REA

### Fichiers à copier (USB ou cloud)

1. `EMFPlanner_dumped.exe` (le dump principal)
2. Tout dossier exporté par dnSpy (si export projet réussi)
3. Tous les modules .NET dumpés séparément (si MegaDumper)

### Placer les fichiers ici :
```
emfPlanner/
  extracted/
    dumped/          ← créer ce dossier
      EMFPlanner_dumped.exe
      dnspy-export/  ← si export dnSpy
```

### Analyse REA sur Mac

```bash
# Vérifier si c'est du .NET
npx -y rea-agents@6.7.0 inspect-managed-artifact emfPlanner/extracted/dumped/EMFPlanner_dumped.exe --json

# Analyse Ghidra (si natif)
npx -y rea-agents@6.7.0 analyze-binary emfPlanner/extracted/dumped/EMFPlanner_dumped.exe --provider ghidra --json

# Chercher les fonctions EMF
npx -y rea-agents@6.7.0 search-functions --pattern "EMF|field|calcul|antenna|safety|propagat" --json

# Chercher les strings
npx -y rea-agents@6.7.0 search-strings --pattern "frequency|MHz|dBi|V/m|EIRP|ICNIRP" --json
```

---

## Problèmes courants

| Problème | Solution |
|----------|----------|
| ExtremeDumper crashe | Utiliser x64dbg + Scylla (méthode B) |
| dnSpy ne reconnaît pas le dump | Le payload est peut-être natif C++ et non .NET. Utiliser Ghidra |
| Scylla ne trouve pas d'imports | Continuer F9 plus longtemps avant de dumper — le code n'est pas encore totalement déchiffré |
| EMFPlanner détecte le debugger | Dans x64dbg : Options → Preferences → cocher "Hide Debugger (PEB)" et les anti-anti-debug plugins (ScyllaHide) |
| Le dump ne fonctionne pas | Essayer de dumper avec Process Hacker : clic droit sur le process → Properties → Memory → sauvegarder les régions RWX |
| License trial expirée | Pas besoin de licence — il suffit que l'exe se lance et déchiffre le code, même si l'UI affiche "trial expired" |

---

## Vérification rapide du dump

Pour savoir si le dump est exploitable :

```bash
# Sur Mac après avoir copié le fichier
file EMFPlanner_dumped.exe
# Devrait afficher "PE32+ executable" sans mention de packing

strings EMFPlanner_dumped.exe | grep -c "EMF\|field\|antenna\|frequency"
# Si > 0 → le code est déchiffré, succès !
# Si 0 → le dump n'a pas capturé le code déchiffré, réessayer
```

---

## Résumé en 5 minutes

1. Installer EMFPlanner + ExtremeDumper sur Windows
2. Lancer EMFPlanner, attendre que la fenêtre apparaisse
3. ExtremeDumper (admin) → trouver le process → Dump
4. dnSpy → ouvrir le dump → explorer le code → Export to Project
5. Copier sur Mac → relancer Claude Code → analyse REA
