# Builds multiplateformes et résultats du test kit — 24 octobre 2026

Demande utilisateur : construire les plateformes et regrouper les builds et les
résultats dans `test validation`. Précision explicite : **les tests concernés sont
ceux du kit `test/emf_test_kit`**, pas la suite générale de l’application.

## Organisation

- Racine de livraison locale : `test validation/2026-10-24/`.
- `builds/PLATEFORME/` : exécutable, installateurs et paquet web.
- `test-kit/PLATEFORME/` : TAP/JUnit, rapport navigateur HTML/JSON/JUnit,
  captures, sauvegardes et fichiers réellement téléchargés par les parcours.
- `logs/PLATEFORME/` : sorties complètes des commandes.
- `test-kit/SCENARIOS.md` et `test-kit/reference/` : guide, données du kit,
  matrice des 216 scénarios et référence CalcPPE isolée de 1 800 cas.
- `manifest.json`, `CHECKSUMS.sha256`, `artifact-verification.json` : provenance,
  résultats, empreintes et vérification statique des architectures/packages.
- Archives GitHub originales et journaux de la campagne dans `downloads/`.

## Exécution reproductible

`scripts/validate-build.py` exécute uniquement `tests/test-kit.test.ts` et
`tests/browser/test-kit.spec.ts`, construit le frontend puis Tauri, et crée chaque
format d’installateur séparément. Il conserve les erreurs sans masquer les autres
formats produits. Choisir une nouvelle racine de sortie pour chaque exécution.

Sur Windows, charger les outils portables et Rust comme indiqué dans
`scripts/Start-EmfPlanner.ps1`, puis :

```powershell
python -B scripts/validate-build.py --output-root "test validation/NOUVELLE-CAMPAGNE" --label Windows-x64 --target x86_64-pc-windows-msvc --bundles nsis,msi
```

`scripts/collect-validation.py` rassemble les artefacts téléchargés et les
résultats locaux, écrit le bilan du kit et peut produire l’archive globale
(`--archive`). Les scripts temporaires `.tools/sync-validation-ci.py` et
`.tools/verify-validation-artifacts.py` assurent la récupération et le contrôle
de la présente campagne.

## Plateformes

Le workflow `.github/workflows/build.yml` produit Windows x64/ARM64 (EXE, NSIS,
MSI), Linux x64/ARM64 (binaire, DEB, RPM, AppImage) et macOS x64/ARM64 (APP zip,
DMG). La version web est également conservée. Android/iOS ne sont pas initialisés
dans ce projet desktop. Les artefacts sont des builds de validation non signés
pour distribution ; macOS utilise une signature ad hoc et pas la notarisation.

Le runner macOS 13 retiré a été remplacé par `macos-15-intel`. Le workflow ne
crée plus de release implicite et ne dispose que de la lecture des contenus.
Les artefacts sont conservés 14 jours sur GitHub puis récupérés localement.

La branche isolée `build-validation/20261024-231920` contient l’état de travail
compilé ; `main` et l’index initial n’ont pas été modifiés. Campagne :
https://github.com/mira-hib/emf-planner/actions/runs/40142361384
Le commit exact et les statuts sont dans `github-run.json` / `github-status.json`
du dossier de livraison. Les essais de mise au point initiaux ont été déplacés
dans `.tools/validation-attempts-2026-10-24/` et ne font pas partie du bilan.

## Portée des résultats

La suite dédiée contient **78 tests métier et neuf parcours navigateur**.
Les neuf parcours Edge et les 78 tests passent localement sous Windows. La
campagne CI a également réussi sur **les six variantes** : 78 tests métier et
neuf parcours Chromium sur chacune. Les six archives ont été téléchargées et
leurs SHA-256 vérifiés face aux empreintes GitHub. Les binaires et tous les
formats demandés sont produits (NSIS/MSI, APP/DMG, DEB/RPM/AppImage), ainsi que
le web. Les vérifications statiques contrôlent PE/Mach-O/ELF et architectures,
les en-têtes de packages et les CRC des archives.

Archive globale : `test validation/emf-planner-builds-test-kit-2026-10-24.zip`,
accompagnée de son `.sha256`. Le détail des 1 800 comparaisons locales
attendu/obtenu/écart est également conservé dans
`test-kit/reference/native-comparison-windows-x64.json`.

Vérification finale de l’archive : **467 712 551 octets**, SHA-256
`0e2ebacb61b080f06ce1849149470ff16dacd953df85700dcb7e948e54012be8` ;
CRC global, 27 contrôles statiques et 2 732 fichiers archivés comparés aux
empreintes du manifeste : tous réussis. Reçu durable :
`reference/multiplatform-test-kit-builds-2026-10-24.json`.

Les 216 scénarios du guide ne sont pas intégralement automatisés : 65 sont liés
partiellement à la suite, 151 ne sont pas exécutés par elle. Ne pas qualifier les
216 de réussis. Le test de 1 800 cas utilise une méthode originale isolée, pas
l’interface ni le pipeline entier. Les parcours pilotent le clone dans le
navigateur ; les installateurs et applications natives ne sont pas exécutés.
