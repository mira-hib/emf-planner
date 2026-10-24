# Référence de l’application originale

`original-sample.xemf` est la copie du projet livré « Castelfranco Emilia TAV2 »
utilisée pour les scénarios Windows. SHA-256 :
`bbc1b5dfe34eec33421e229f6080d79b5c067ad9055875b9b9b85d1b79ba79f6`.

`original-results.json` conserve les 15 valeurs relevées dans l’application
originale avant comparaison au clone ; sa provenance décrit cette capture.
Il ne contient pas des valeurs calculées par le clone.

Les tests et le benchmark lisent ces fichiers versionnés sur Windows et Mac.

`test-kit-native-oracle.json` ajoute 1 800 sorties de la méthode C# `CalcPPE`
récupérée, exécutée sans modification dans une console .NET avec des structures
numériques minimales. Sa provenance et ses empreintes sont intégrées à la fixture.
Elle se régénère avec `scripts/native-test-kit-oracle.py` et le SDK local existant.
Elle ne constitue pas une nouvelle observation de l’interface ou du pipeline
complet original. Voir `reference/test-kit-audit-2026-10-22.md`.
