# Observations directes de l’original — 25 octobre 2026

Contrôle Windows `@oai/sky` à nouveau disponible après reprise. Exécutable
installé `C:\Program Files\EMFPlanner7\EMFPlanner.exe`, fenêtre
`EMF Planner 7.1 Trial`. Les observations ci-dessous proviennent de captures
réellement affichées et de l’arbre d’accessibilité ; elles ne prouvent pas tous
les calculs ou toutes les interactions du guide.

- Démarrage : dialogue annonçant quatre jours d’essai restants ; accès au
  logiciel après OK. Save et Save as désactivés en mode Trial. Aucun test de
  changement d’horloge ni d’activation n’a été effectué.
- Sources : écran initial vide avec ligne de saisie WinForms. Fréquence de cette
  ligne 1000 MHz, puissance 0, N=1, gain/hauteur 0, taille 1 m. Les trois panneaux
  Antenna Pattern, EIRP Calculator et Attenuation factors sont à droite. Il reste
  des différences de valeurs proposées et de mise en page dans le clone.
- Buildings : commandes de sélection, téléchargement, dessin, suppression,
  copie, annuler/rétablir, import, calcul bases, équarrissage, sommets, ordre et
  déplacements directionnels. Paramètres de calcul en bas : ignorer, blindage
  complet ou pertes internes. Pertes affichées 3/6 dB, frontière 400 MHz. Options
  d’affichage dédiées : épaisseur, taille du texte et couleur. La carte utilisait
  Google Map et affichait effectivement des tuiles lors de l’observation.
- Vertical Cuts : colonnes activité, azimut, Xo, Yo, L1, L2, H1, H2 et sources
  regroupées. Ligne proposée L1=150, L2=0, H1=100, H2=0. Sept cases d’affichage :
  longueur, hauteur basse/maximum/haute, distance minimale, cotes, points. Ligne
  optionnelle au-dessus du terrain à 2,0 m. Deux options pour ajouter Owner ou
  utiliser Antenna type dans les noms. Image à droite et tableau à gauche.
- Settings : paramètres projet séparés des paramètres application. Matrices
  600/200 et coefficient 1,3 observés. Le code `Public2.NewProject` confirme ces
  valeurs d’initialisation. Cache, dossier des antennes, propriétaires/couleurs,
  proxy, coordonnées et tableau des fonds sont des paramètres application.
  Le tableau contient treize fournisseurs en plus de l’image calibrée ; Google,
  Bing, HERE, Mapquest, 2GIS et Yandex sont présents. Les identifiants de service
  intégrés à l’original ne sont pas recopiés dans le clone ni dans cet audit.

REA : le C# récupéré de `UC_VertPlanes`, `UC_Setup`, `CalcSZZ` et
`RepWordAlias` confirme les commandes et conventions utilisées. La distance
minimale aux contours est une distance **verticale au terrain**, pas une
distance euclidienne à l’origine (`CalcSZZ`, lignes 1162–1169).

Les essais du clone et les observations de l’original restent des preuves
distinctes. La reproduction pixel par pixel et l’intégralité des 216 scénarios
ne sont pas déclarées validées par ce fichier.
