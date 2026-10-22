# Reproduction rubrique par rubrique

Décision utilisateur du 22 octobre 2026 : reprendre chaque élément du menu de l’original, interface et fonctionnalités, en commençant par ce qui permet de réaliser une étude EMF. Les rubriques existent déjà dans le clone ; leur présence ne signifie pas que la reproduction est terminée.

## Ordre de travail

| Ordre | Rubrique de l’original | Objectif de la passe |
|---|---|---|
| 1 | Radiating Sources | Saisie et organisation des émetteurs, diagrammes, câbles, puissances, facteurs et sorties propres aux sources |
| 2 | EMF Limits | Quatre jeux de limites, intervalles, facteurs, styles et préréglages du logiciel d’origine |
| 3 | Origin and Grid | Origine, coordonnées, emprise et deux grilles de calcul |
| 4 | Antenna Locations | Positions, coordonnées, déplacement et implantation sur la carte |
| 5 | Buildings | Empreintes, hauteurs, bases, édition et interaction avec les calculs |
| 6 | Terrain | Relief, courbes, triangulation et données d’altitude |
| 7 | Point Calculations | Groupes, hauteurs, contributions par source et résultats |
| 8 | Horizontal Cuts | Commandes de création, grilles, contours, styles et rapports |
| 9 | Vertical Cuts | Géométrie, coupes automatiques, maxima, cotes et rapports |
| 10 | Tables and Graphs | Valeurs calculées, sélections, graphiques et exports |
| 11 | Lines and Labels | Dessin, nœuds, annotations, symboles et styles |
| 12 | CAD | Mise en page, calques, échelles et sorties CAO |
| 13 | 3D | Vue, navigation, objets, calques et volumes de sécurité |
| 14 | Report by template | Modèles, alias, insertion des tableaux/graphiques et production Word |
| 15 | Calibrate Map Image | Repères, deux méthodes de calibration et pyramide de tuiles |
| 16 | Settings | Compléter les préférences transversales et leur conservation |

Les préférences nécessaires à une rubrique sont traitées avec elle. L’ordre suit les données nécessaires à une étude, puis les calculs et les livrables.

## Méthode de validation

Pour chaque rubrique, relever les commandes, champs, boîtes de dialogue et comportements dans les formulaires C# récupérés par REA. Comparer les données sauvegardées et les interactions du clone. Les observations dynamiques déjà conservées servent de référence visuelle ; ajouter de nouvelles observations de l’original lorsque le contrôle du bureau est disponible. Vérifier un projet vide et Castelfranco sans écraser le fichier original. Conserver les fonctions inachevées en gris lorsque leur commande ne réalise pas encore le travail annoncé.

## Rubrique 1 — lot essentiel intégré

- Sélection de plusieurs lignes par Ctrl/Maj, édition groupée d’une colonne, duplication après la dernière ligne sélectionnée, ordre stable, déplacements haut/bas/début/fin et suppression confirmée par Suppr.
- Copie et collage de sources complètes avec identifiants renouvelés, diagrammes et paramètres de câbles indépendants ; tampon persistant entre projets. Le tampon utilise du JSON validé, sans reprendre BinaryFormatter.
- Activation collective fondée sur la première source, palette native de douze couleurs appliquée à la sélection et conservation des couleurs importées.
- Bandes F/F2, service libre, état Existing/New et conservation des données de câble lors de l’import .xemf et de la sauvegarde .emfp. Bornes numériques des principaux champs alignées sur les plages natives ; N tronqué à l’entier.
- Calculateur EIRP indépendant de la grille, trois sélecteurs W/mW/dBW/dBm, calcul direct et deux calculs inverses. Précision Single et arrondis natifs vérifiés séparément.
- Facteurs α24/Ftdc/Fpr bornés à 0,1–1 et désactivés/vides suivant les indicateurs du jeu de limites.
- Calcul explicite des ouvertures H/V pour la source courante ou toutes les sources, croisements au niveau absolu de 3 dB.
- Enveloppes avec gains ignorés/utilisés et éditables, normalisation native et conservation du gain de la source. Câbles calculés au milieu de F/F2 ; connecteurs entièrement exclus lorsque leur case est décochée. MSI Windows-1251 et UTF-8.

## Rubrique 1 — encore à reproduire

La rubrique Sources n’est pas déclarée entièrement terminée. Restent notamment :

- Sélection de cellules et de colonnes comme la grille native, ordre des colonnes mémorisé, interactions de double clic, formulaire de conversion de puissance synchronisé.
- Valeurs initiales natives d’une ligne vide ; ouvertures textuelles réservées aux rapports ; association propriétaire/couleur. Les bornes invalides de l’azimut sont rejetées dans le clone, sans reproduire la sentinelle native `0÷360`.
- Arbre de dossiers et fichiers d’antennes, quatre graphiques du visualiseur, inversions et rotations des deux plans ; panier d’enveloppe alimenté par dossiers.
- Les quatre sorties Word spécifiques aux sources, diagrammes, volumes et points cardinaux. Le rapport générique disponible ne les remplace pas.
- Modèle Excel natif à 19 colonnes, sélection des sources actives et import historique XLS/XLT ; l’import disponible lit les fichiers OOXML XLSX/XLTX selon le schéma du clone. L’entrée XLT historique est grisée.
- Métadonnées natives de l’export MSI et comparaison visuelle complète des dialogues.

Le détail des preuves C# et des différences initiales figure dans [l’audit Sources](../reference/sources-native-audit-2026-10-22.md). Continuer les écarts de cette rubrique avant de la déclarer achevée ; la rubrique suivante dans l’ordre est Limites EMF.

## Contrôles de cette passe

Les tests couvrent les opérations de lignes et les copies profondes, les bandes de fréquences, les ouvertures, les calculs de puissance et les enveloppes/câbles. Les parcours Edge supplémentaires vérifient les modifications groupées, les limites de N, le tampon après rechargement, le calculateur sans modification du projet, les fichiers MSI et la sauvegarde des options de câble. Les 15 références de calcul de l’original restent conservées.

Résultat final : **63 tests unitaires et 21 parcours Edge réussis**, compilation TypeScript/Vite et release Windows réussies. La nouvelle release mesure 6 705 664 octets ; son SHA-256 et le chemin exact sont conservés dans [source-menu-build.json](../reference/source-menu-build.json). Capture relue : [Sources Castelfranco](../reference/sources-interface/sources-castelfranco.png). Le nouvel exécutable n’a pas été lancé pendant cette passe.

La compilation Windows utilise la cible explicite `x86_64-pc-windows-msvc` pour produire un nouvel exécutable sans remplacer celui actuellement ouvert dans `src-tauri/target/release/`. Ne pas fermer une session native contenant des modifications sans les sauvegarder. Le contrôle du bureau est indisponible dans cette session ; la validation visuelle s’appuie sur Edge et les références déjà capturées, pas sur une nouvelle manipulation de l’original.

`scripts/Start-EmfPlanner.ps1` choisit le plus récent des deux exécutables release, sans fermer une application déjà ouverte.
