# Sources — preuve d’interface du 22 octobre 2026

`sources-castelfranco.png` : clone dans Edge, fenêtre 1480×960, projet `tests/fixtures/original-sample.xemf` importé sans modification. Le fichier original n’a pas été réécrit. La capture montre les neuf sources, leurs couleurs importées, le panneau H/V, le calculateur EIRP indépendant et les facteurs activés selon les limites.

Contrôles : 63 tests unitaires réussis, dont les 15 références numériques originales ; 21 parcours Edge réussis. Le parcours de redimensionnement a également passé trois répétitions après correction de la concurrence entre le raccourci Fin et le redimensionnement de la fenêtre. Compilation TypeScript/Vite réussie.

Le contrôle du bureau étant indisponible, cette capture ne constitue pas une nouvelle observation dynamique de l’original. L’audit [sources-native-audit-2026-10-22.md](../sources-native-audit-2026-10-22.md) cite le code C# récupéré et les comportements comparés. [Le suivi](../../memory-bank/menu-reproduction.md) distingue le lot livré des éléments encore absents ; la rubrique Sources reste partielle.
