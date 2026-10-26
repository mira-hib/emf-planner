# Fonds cartographiques — observation native et reproduction

## Sources et limites de l’observation

Analyse statique des artefacts EMFPlanner 7.1 déjà récupérés et décompilés :

- `reference/original-decompiled/EMFPlanner/Form1.cs`, `LoadTScmbURL` (vers 3743) : ajoute d’abord `Calibrated map image`, puis les lignes de `CustomBaseMaps.set` dans leur ordre ; `TScmbURL_SelectedIndexChanged` conserve le centre géographique en changeant la projection.
- `extracted/original/app/CustomBaseMaps.set` : treize fonds, noms et modèles d’URL. Les chaînes d’identifiants HERE/Mapbox historiques présentes dans cet artefact ne sont pas recopiées dans le code du clone.
- `reference/original-decompiled/EMFPlanner/CalcTileWeb.cs`, `LoadTileServer`, `CalcNameTile`, `CoordToTileNew`, `TileToCoordNEW` : remplacement de paramètres, quadkey Bing, projection elliptique Yandex, cache de l’image calibrée.
- `reference/original-decompiled/EMFPlanner/UC_3D.cs` utilise `CalcNameTile` pour la texture du terrain.

Il s’agit d’une lecture de décompilation, pas d’une preuve d’exécution de chaque fournisseur dans l’application native. Aucune nouvelle session REA ni nouvelle extraction binaire n’a été nécessaire pour ce sous-système.

## Comportement reproduit

Le catalogue `src/lib/map-providers.ts` contient les quatorze choix dans l’ordre de la capture utilisateur :

| Choix | Résultat de la reproduction |
| --- | --- |
| Calibrated map image | Image calibrée locale ; aucune requête de tuiles réseau. Texture géoréférencée disponible en 3D. |
| OpenStreetMap | Serveur OSM, HTTPS. |
| Carto Basemap | Style natif `light_nolabels`, image @2x, domaine CARTO actuel. |
| OSM Relief | Serveur Thunderforest `cycle`, configuration de clé nécessaire. |
| Google Map | Modèle natif `mt2.google.com`, zoom inversé `[17-Z]`. |
| Google Satellite | Serveur Google actuel `mt2.google.com/vt/lyrs=s` : correction de l’ancienne version `khms2` 930 qui répond 404. |
| Esri Satellite | Service natif World_Imagery, ordre z/y/x. |
| HERE WeGo Satellite | Choix visible ; URL avec compte/clé utilisateur à configurer. |
| Mapquest Satellite | Choix visible ; URL historique et jeton valides à configurer, sans substitution de fournisseur. |
| OpenTopoMap | Serveur natif, HTTPS, surzoom au-delà du niveau 17. |
| Bing Sat | Quadkey natif `[RES]`, serveur Bing. |
| 2GIS | Serveur natif, HTTPS. |
| Yandex map | Serveur natif et projection Mercator elliptique. |
| Yandex Sat | Serveur natif, projection elliptique et préfixe Gagarin. |

Le résolveur commun 2D/3D prend en charge `[X]`, `[Y]`, `[Z]`, `[17-Z]`, `[Z+1]`, `[FX]`, `[FY]`, `[RES]`, `[RND(a,b,c)]`, `[Gagarin.Substring(0,RND(0,8))]`, ainsi que `{x}`, `{y}`, `{z}`, `{-y}`, `{s}`, `{r}`, `{quadkey}`. Les styles de coordonnées restent géographiques lorsque le fond change ; Leaflet utilise EPSG:3395 pour Yandex et EPSG:3857 pour les autres fonds.

L’import `.xemf` conserve désormais le nom précis du fond natif. Auparavant, tous les noms contenant « Satellite » étaient remplacés par Esri. Les anciens noms du clone `Carto clair` et `Satellite Esri` sont résolus vers `Carto Basemap` et `Esri Satellite`.

Un fournisseur inconnu ou inaccessible laisse le fond vide avec un message. Aucun autre service n’est choisi implicitement. Le dialogue **Fonds de carte personnalisés** accepte les marqueurs natifs et un nom identique à un fond intégré pour configurer ce fournisseur ; les doublons dans les personnalisations sont refusés. La configuration reste sauvegardée dans le projet.

## Vérification réseau du 22 octobre 2026

Commande : `tsx scripts/probe-map-providers.mts`. Résultats complets conservés dans `reference/map-provider-network-check.json`, avec date UTC, URL sans secrets, statut HTTP, type, taille, signature de fichier et en-tête CORS.

Une tuile z=3/x=4/y=2 par service accessible a été demandée. OpenStreetMap, CARTO, Google Map, Google Satellite corrigé, Esri, OpenTopoMap, Bing, 2GIS, Yandex map et Yandex Sat ont renvoyé **HTTP 200 + type image + signature PNG/JPEG + Access-Control-Allow-Origin: \***. Cette mesure ne garantit pas tous les niveaux de zoom, régions ou accès futurs.

L’ancienne URL Google Satellite version 930 renvoie **404 JSON** ; la nouvelle URL renvoie **200 JPEG**. L’échantillon Thunderforest renvoie 200 PNG mais porte le filigrane **API Key Required**, vérifié visuellement dans `reference/thunderforest-tile-probe.png`. Il serait donc incorrect de qualifier le fond OSM Relief de pleinement utilisable sans clé sur la seule base du statut 200.

HERE et Mapquest n’ont pas été interrogés avec les identifiants tiers de l’artefact. Leur disponibilité avec un compte utilisateur reste à vérifier. HERE fournit aujourd’hui une API v3 nécessitant un compte HERE platform et une clé/OAuth ; ses anciens comptes ne fonctionnent pas pour v3 : [guide officiel de migration](https://docs.here.com/map-rendering/docs/migration-guide-raster-tile-api). Exemple de modèle personnalisable : `https://maps.hereapi.com/v3/base/mc/[Z]/[X]/[Y]/png?style=satellite.day&apiKey=VOTRE_CLE` ; [documentation officielle](https://docs.here.com/map-rendering/docs/quickstart-raster-tile-api).

Thunderforest documente la clé API : [documentation officielle](https://www.thunderforest.com/docs/). Mapbox documente le jeton requis pour ses tuiles raster : [documentation officielle](https://docs.mapbox.com/api/maps/raster-tiles/). Cela ne prouve pas que le tileset historique `mapquest.satellitenolabels` reste accessible avec un nouveau jeton. Aucun tileset Mapbox alternatif n’est attribué au nom Mapquest automatiquement.

Les URL Google reprises ici appartiennent au mécanisme historique de l’application et ont été vérifiées directement. Elles ne constituent pas une implémentation de l’API officielle Google Maps Tiles actuelle, laquelle demande une clé et une session : [documentation officielle](https://developers.google.com/maps/documentation/tile/satellite). Le dialogue permet de configurer un modèle d’URL utilisateur ; il ne crée pas automatiquement les sessions de cette API.

## Vérifications automatisées

- `tests/map-providers.test.ts` : **7/7 passants** ; liste native, paramètres, quadkeys, absence de substitution, configuration même nom, validation, projection Yandex comparée aux constantes natives et import original réel.
- `tests/browser/map-providers.spec.ts` : **4/4 passants** lors de la campagne isolée ; liste complète, sélection de chaque serveur, chargement d’images simulées déterministes, absence de paramètres non résolus, messages de clé/erreur, récupération après erreur, personnalisation du nom natif. L’assertion de sauvegarde a ensuite été ajoutée et est incluse dans la campagne unifiée.
- `tsc --noEmit` : passant après l’intégration de la texture calibrée 3D.

Validation finale commune : **171 tests métier et 46 parcours Edge passants**,
dont les quatre parcours cartographiques avec l’assertion de sauvegarde. Sortie
finale : `.tools/antenna-map-full-ui/`. Les sondes réelles restent distinctes.

Les tests navigateur utilisent des images interceptées : ils prouvent le raccordement et la gestion des échecs, pas l’accès réseau réel. Le contrôle HTTP ci-dessus constitue une vérification indépendante des services distants. La texture calibrée 3D est implémentée et compilée ; elle n’a pas encore de comparaison visuelle native pixel par pixel.
