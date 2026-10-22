# EMF Limits — audit statique du 22 octobre 2026

Evidence: sources C# déjà décompilées dans `reference/original-decompiled/EMFPlanner/`. Il s'agit des données et comportements historiques EMFPlanner 7.1; aucune conformité aux réglementations actuelles n'est affirmée. Aucun binaire exécuté ou redécompilé pour cet audit.

## Écran et commandes

- Le contrôle est `UC_Limits`, pas `UC_PDU`. Groupe `EMF Limits`, quatre descriptions multilignes (320 × 35), quatre couleurs cliquables (35 × 35) et trois facteurs par jeu (`α24`, `Ftdc`, `Fpr`). Sources: `UC_Limits.cs:1252`, `:1264`, `:1439`, `:1593`, `:1710`, `:1960`, `:2008`.
- Une table commune contient neuf colonnes: `Frequency to F, MHz`, `Limit E1, V/m`, `Param A1` ... E4/A4. Colonnes E largeur 55, A largeur 50, F remplit l'espace; alignement centré, pas de redimensionnement ni sélection multiple. Ajout natif = ligne entièrement vide; suppression de la ligne choisie. Sources: `UC_Limits.cs:1611-1686`, `:1916-1925`.
- Seize boutons avec icônes: ICNIRP, EU, USA, Switzerland, UK, Belgium, Italy, China, Russia, Poland, India, Lithuania, Slovenia, Greece, Croatia, Bulgaria. Chaque commande demande confirmation, remplace la table entière, remplit les descriptions et rafraîchit le tracé. Sources: `UC_Limits.cs:1350-1581`, `:2109-2283`.
- Les presets ne modifient ni couleurs ni cases des facteurs. Particularité historique: Greece ne réaffecte pas les descriptions, qui restent donc celles du projet. Sources: `Public2.cs:866-960`, en particulier `:901-905`.

## Validation, intervalles et facteurs

- F dans `(0, 1000000]` MHz; E dans `(0, 100000]`; A dans `[-3, 3]`. Les valeurs invalides deviennent des cellules vides et la sentinelle interne `-999`. Sources: `UC_Limits.cs:1876-1908`; affichage des sentinelles comme vide: `:1741-1757`.
- Le modèle natif est une liste commune `LimitPoint {Freq, E[4], A[4]}` (`Public2.cs:365-389`). Un seul F/E/A manquant dans un jeu invalide le jeu entier pour le tracé et conserve le PDU de repli pour les calculs (`CalcSZZ.cs:177-216`; `UC_Limits.cs:1810-1838`).
- L'ordre de saisie est conservé; aucun tri. Si le premier F dépasse 10, une borne artificielle à 10 est préfixée. Pour chaque segment, on sélectionne la première paire vérifiant `f > borne précédente && f <= borne suivante`, puis E = coefficient de la borne suivante × f^A. Il n'y a aucune extrapolation au-dessus de la dernière borne, ni en dessous/à 10. Si première borne <=10, elle sert de borne inférieure sans appliquer son coefficient avant elle. Sources: `CalcSZZ.cs:204-216`.
- Repli natif: PDU = 1000000 (densité historique, E équivalent = sqrt(3.77 × 1000000) V/m). C'est une valeur technique de repli historique, pas une limite réelle d'exposition. PDU normal = E²/3.77. La contribution est densité/PDU, les facteurs `α24 × Ftdc × Fpr` multipliant la puissance, chacun remplacé par 1 si sa case est décochée. Sources: `CalcSZZ.cs:177-181`, `:212-213`, `:265-284`, `:630-635`.
- Couleurs par défaut rouge, bleu, vert, cyan; les douze facteurs sont cochés. Les projets héritent des paramètres de l'application. Sources: `Public2.cs:839-851`, `:963-979`.

## Tracé

Graphique commun sur fond blanc, grille grise logarithmique sur les deux axes, traits de couleur épaisseur 3. Axe X fixe 10 MHz à 100 GHz (10, 100, 1000, 10000, 100000 MHz); axe Y fixe 1 à 1000 V/m. Les discontinuités restent séparées, chaque segment utilise sa propre formule; pas de raccord vertical. Rafraîchissement après édition, couleur, ajout, suppression, preset et redimensionnement. Sources: `UC_Limits.cs:1766-1865`, `:1911`, `:1968`, `:2104`.

## Presets exacts

L'ensemble des coefficients, fréquences et descriptions se trouve dans `Public2.cs:866-960`. Pour les presets à trois intervalles, bornes communes = 400, 2000, 1000000; jeu 2 = (61,0), (3,0.5), (137,0); jeux 3/4 vides sauf Italie.

| Preset                     | Jeu 1 (coefficient, exposant) par intervalle                       | Particularité                                                   |
| -------------------------- | ------------------------------------------------------------------ | --------------------------------------------------------------- |
| ICNIRP, EU, UK             | (28,0); (1.375,0.5); (61,0)                                        | lignes 871-874, 944-947                                         |
| Belgium                    | (14,0); (0.688,0.5); (30.5,0)                                      | 880-883                                                         |
| Bulgaria                   | (6,0); (6,0); (6,0)                                                | 886-889                                                         |
| Croatia                    | (11.2,0); (0.55,0.5); (24.4,0)                                     | 896-899                                                         |
| Greece                     | (21.56,0); (1.059,0.5); (46.97,0)                                  | conserve descriptions, 902-904                                  |
| India, Lithuania, Slovenia | (8.854,0); (0.435,0.5); (19.29,0)                                  | 907-910, 917-920, 932-935                                       |
| Switzerland                | (5,0); (5,0); (5,0)                                                | 938-941                                                         |
| China                      | borne 100000: (12,0)                                               | jeu 2 (137,0), 892-893                                          |
| Italy                      | borne 100000: (6,0)                                                | jeux 2/3/4 = (20,0)/(4.5,0)/(3,0), descriptions exactes 913-914 |
| Poland                     | borne 100000: (7,0)                                                | jeu 2 (60,0), 923-924                                           |
| Russia                     | bornes 300/100000: (3,0)/(6.1,0)                                   | jeux 2/3/4 vides, 927-929                                       |
| USA                        | bornes 30/300/1500/1000000: (824,-1)/(27.5,0)/(1.585,0.5)/(61.4,0) | jeu 2 (1842,-1)/(61.4,0)/(3.545,0.5)/(137.3,0), 950-954         |

## Écarts constatés avant correction

- `src/components/ProjectPages.tsx:20-159`: quatre tableaux indépendants, description sur une ligne, pas de graphique, ajout prérempli et suppression uniquement dernière ligne. Bornes F autorisent zéro sans maximum, E a un minimum arbitraire sans maximum, A sans bornes.
- `src/lib/engine.ts:21-25`: tri des lignes, extrapolation avant/après le domaine, aucune sentinelle ou invalidation du jeu, repli 28 arbitraire et clamp 1e-9. Les facteurs par jeu sont déjà appliqués correctement à la puissance (`:105-119`, `:148`).
- Ancien `src/lib/presets.ts`: un seul jeu créé par commande, coefficients et références réglementaires ajoutés sans correspondance au natif; `useExtraActions.ts` remplaçait le seul jeu sélectionné. Le port corrigé dans cette branche utilise les 4 jeux et des sentinelles explicites afin de conserver la table commune.

Validation ajoutée: `tests/limit-presets.test.ts` compare les 16 ensembles complets aux déclarations C# (y compris descriptions, jeux vides et fréquences), vérifie le remplacement de quatre jeux, la conservation des préférences et l'absence de mutation des données précédentes.
