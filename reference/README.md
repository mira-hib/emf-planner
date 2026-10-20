# Références récupérées sous Windows

`original-decompiled/` contient l’export ILSpy du module principal récupéré en
mémoire : 137 fichiers C#, ressources, manifeste et projet généré. Ce dossier
sert à l’analyse ; il n’est pas compilé par Tauri et n’est pas le code source
d’origine. L’export porte les limites habituelles de la décompilation.

Les assemblages PE/CLI reconstruits, les deux inventaires de capture, les
installateurs et les preuves supplémentaires sont conservés dans l’archive de
passation décrite dans `memory-bank/mac-handoff.md`.

Les dumps RAM complets et les outils installés sur le PC sont hors de cette
passation : les assemblages, métadonnées, ressources et scripts nécessaires à
la poursuite de l’analyse sont fournis séparément.
