# Analyse approfondie — EMFPlanner.exe (Ghidra + REA)

> Historique de l'analyse statique Mac. La reprise Windows a ensuite permis de
> récupérer et décompiler le payload métier .NET ; voir `windows-dynamic-analysis.md`.

## Verdict final

**EMFPlanner.exe est protégé par Enigma Protector**, un packer commercial anti-rétro-ingénierie. La décompilation complète du code métier est **impossible** sans exécution sur Windows (unpacking dynamique).

---

## Architecture du binaire

| Section | VA Start | Taille raw | Taille virtuelle | Entropie | Contenu |
|---------|----------|-----------|------------------|----------|---------|
| SECTION.0 | 0x2000 | 409 KB | 1.3 MB | 8.00 | Code chiffré (Enigma) |
| SECTION.1 | 0x152000 | 0 | 40 KB | — | Vide (BSS) |
| .rsrc | 0x15C000 | 38 KB | 40 KB | 6.58 | Manifeste XML (requestedExecutionLevel) |
| SECTION.3 | 0x166000 | 278 KB | 12.6 MB | 8.00 | Code chiffré + dispatch table |
| SECTION.4 | 0xDA6000 | 3.0 MB | 3.0 MB | 6.10 | Runtime Enigma + payload chiffré |

### Taille totale : 3.84 MB (PE32+ x64)

---

## Protection : Enigma Protector

### Identification
- **Signature** : "Enigma Protector" + certificats X.509 auto-signés (CA Enigma Protector)
- **Date** : 2023-01-31 (timestamp Symantec SHA256)
- **Mécanisme** : Chiffrement AES de toutes les sections de code + stub d'entrée XOR 0x55

### Stub d'entrée (point d'entrée réel)
```c
void entry(void) {
    byte *ptr = &DAT_141096d70;
    long count = 0x60B;
    do {
        *ptr = *ptr ^ 0x55;  // XOR déchiffrement
        ptr++;
        count--;
    } while (count != 0);
    // Saute vers le code déchiffré → loader Enigma
}
```

Ce stub ne déchiffre que **1547 octets** (0x60B) — le micro-loader Enigma qui ensuite déchiffre le reste en mémoire à l'exécution.

### Conséquences pour la rétro-ingénierie
1. **Pas de symboles** : tous les 13 777 noms de fonctions sont `FUN_XXXXXXXX`
2. **Pas de chaînes lisibles** : 0 chaîne de domaine EMF (fréquence, puissance, antenne, etc.)
3. **Pas de PE embarqué** : le payload est entièrement chiffré, pas de MZ/PE extractible
4. **Pas de métadonnées .NET** : malgré le `.exe.config` indiquant .NET 4.5.2/VB.NET, le PE n'a pas de CLI directory

---

## Résultats Ghidra (analyse statique)

### Fonctions
- **13 777 fonctions** identifiées
- 13 775 dans SECTION.3 (code chiffré → Ghidra interprète les octets chiffrés comme du code)
- 2 dans SECTION.4 (stub d'entrée + loader)
- Taille moyenne : 28.6 instructions
- Plus grande fonction : 157 instructions (dans le loader Enigma)

### Qualité de l'analyse
Les 13 775 fonctions dans SECTION.3 sont des **artefacts** — Ghidra interprète les données chiffrées comme des instructions x86. Ce ne sont PAS les vraies fonctions du programme EMFPlanner. La preuve :
- Entropie 8.0 (données aléatoires/chiffrées)
- Aucune chaîne de caractères référencée
- Aucun import API Windows référencé
- Taille uniforme (~28 instructions) → pattern typique du bruit décodé

---

## Ce qui est visible (non chiffré)

### 1. Manifeste Windows (.rsrc)
```xml
<assemblyIdentity version="1.0.0.0" name="MyApplication.app"/>
<requestedExecutionLevel level="asInvoker" uiAccess="false"/>
```
→ Application VB.NET standard, pas de droits admin requis.

### 2. DLLs d'import Enigma
- kernel32.dll, user32.dll, advapi32.dll, oleaut32.dll, gdi32.dll, shell32.dll, version.dll, ole32.dll
- Seuls `LoadLibraryA` et `GetProcAddress` sont importés directement
→ Résolution dynamique de toutes les API à l'exécution (anti-import table reconstruction)

### 3. Certificats Enigma
- CA auto-signé "Enigma Protector CA"
- Valide 2017-11-09 → 2027-11-07
- Timestamping Symantec SHA256 (2023-01-31)

---

## Stratégie de reproduction

Puisque la décompilation du code métier est bloquée par Enigma Protector, la reproduction se base sur :

### Sources disponibles
1. **Manuel PDF** (76 pages) → formules complètes extraites (voir `formulas.md`)
2. **Standards publics** : ITU-T K.70, ITU-R P.1546, ITU-R F.699-8, ICNIRP 2020
3. **Fichiers de configuration** : CustomBaseMaps.set (tile URLs), .exe.config
4. **DLLs .NET décompilées** : structure de données ODA/Teigha (formats DWG/DXF)
5. **Modèle de données** : 19 champs source rayonnante + 13 champs point de contrôle (depuis le manuel)

### Ce qui manque et ne peut être obtenu sans unpacking Windows
- Code exact des algorithmes EMF (boucles de calcul, optimisations)
- Paramétrage interne des modèles de propagation
- Logique de rendu des volumes de sécurité 3D
- Gestion des licences/trial
- Format propriétaire des fichiers projet (.emfp?)

---

## Conclusion pour l'article

> **Est-il possible de reproduire un logiciel en le décompilant ?**
>
> Dans le cas d'EMFPlanner 7.1, la réponse est **non pour le code métier** mais **oui pour la fonctionnalité**. Le binaire est protégé par Enigma Protector, rendant la décompilation du code propriétaire impossible sans exécution Windows. Cependant, 100% des formules EMF sont basées sur des standards publics internationaux (ITU, ICNIRP). Le manuel PDF de 76 pages documente suffisamment l'application pour la reproduire fonctionnellement, sans avoir besoin du code source. L'infrastructure CAO (40+ DLLs ODA/Teigha, 955k lignes) est remplaçable par des bibliothèques open-source (Leaflet, DXF crate Rust).
