# Reprise des 216 scénarios du test kit

Le catalogue conserve le texte exact de chacun des 216 scénarios dans
`reference/test-kit-scenarios.json`. L'index `test-kit-coverage.json` relie
149 scénarios à des assertions partielles ; 67 n'ont pas encore de référence
dans les suites du kit. Une référence ne constitue pas une validation complète.

## Résultats établis

- Régression complète locale : **236/236** tests de code et **65/65** parcours
  d'interface réussis. Les journaux de cette régression restent dans `.tools`.
- Suites propres au kit sur Windows x64 CI : **143/143** tests de code et
  **28/28** parcours d'interface, sans échec ni parcours ignoré. Le build natif,
  les deux installateurs et les encodeurs U3D/DWG passent aussi.
- Référence originale : 15 valeurs de champ de Castelfranco comparées avec une
  tolérance de 0,0000011 V/m ; 1 800 cas de la méthode CalcPPE conservés. La
  méthode isolée ne valide pas tous les calculs du logiciel.
- Deux projets exemples natifs importés ; tous leurs points existants et une
  coupe horizontale/verticale à la première résolution importée sont calculés.
  Le projet « big test » ne contient aucun point de contrôle.
- Les exports de coupes Word sont désormais vérifiés dans le navigateur par
  ouverture réelle de leur archive OOXML et contrôle des images et titres.
- Les encodeurs produisent un véritable DWG R2000 et un flux U3D intégré au PDF.
  La relecture LibreDWG contrôle la géométrie de 100 × 50 mm à l'échelle 1:1000.

## Charge ROB-05

`scripts/validate-kit-stress.mts` calcule réellement 50 hauteurs, 1 000 sources,
2 000 bâtiments en blindage complet, avec 1 000 divisions sur le grand axe.
Le rectangle comprend tous les bâtiments du fichier du kit. Les hauteurs sont
0–49 m ; la première limite est seule active. Le modèle « generic » sans
diagramme fourni dans le TSV utilise explicitement le diagramme sector65x7 du
kit, avec le gain propre à chaque source. Ces choix restent dans le résultat.

Chaque coupe garde son empreinte de grille, sa durée, le pic mémoire du
processus, l'empreinte du projet et celle du code. La reprise ignore tout reçu
incompatible. Le fichier `result.json` fait foi pour l'avancement. Une campagne
fraîche est conservée dans `test validation/2026-10-25/stress-rob05-final` ; les
anciennes tentatives sont séparées. La même charge tourne maintenant en CI pour
survivre aux coupures locales. Cela ne valide pas la fluidité de l'interface,
le Gestionnaire des tâches ni tous les exports du scénario.

## Écarts et vérifications encore nécessaires

- Les facteurs indépendants, le plancher F.699, certaines bornes et plusieurs
  valeurs par défaut observées dans le binaire diffèrent du texte du guide.
  Les tests marqués « native divergence » consignent ces écarts.
- Les API qui exigent une clé propre au fournisseur restent configurables avec
  les identifiants de l'utilisateur. Aucun secret intégré à l'original n'est copié.
- Les conversions historiques `.dot`/`.xlt` disposent d'un pont Office ; leur
  validation COM réelle reste incomplète après un blocage de Word. Les formats
  OOXML fonctionnent directement et sont testés.
- Fond raster dans DXF/DWG, ouverture dans des lecteurs tiers DWG/PDF 3D,
  écriture du format natif `.xemf`, reproduction visuelle exhaustive et essais
  d'installation restent à terminer ou vérifier.
- Les essais de licence, d'activation, de changement d'horloge et de politique
  de sécurité du logiciel commercial ne sont pas déclarés réussis par le clone.

Les builds et rapports des six plateformes sont collectés dans la campagne
datée. Une compilation réussie sur un runner ne prouve pas l'exécution de son
installateur sur une machine utilisateur.
