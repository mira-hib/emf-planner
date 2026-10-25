# Fonds cartographiques — reprise du 22 octobre 2026

Les quatorze entrées de la capture utilisateur sont intégrées dans leur ordre natif, depuis **Calibrated map image** jusqu’à **Yandex Sat**. Le catalogue et les paramètres sont issus de `CustomBaseMaps.set`, `Form1.LoadTScmbURL` et `CalcTileWeb.cs` décompilés.

Code principal : `src/lib/map-providers.ts`, `src/components/BasemapLayer.tsx`, intégration `PlannerMap.tsx` et `Scene3D.tsx`. Quadkeys Bing, marqueurs natifs et Leaflet, projection elliptique Yandex et messages de service/configuration sont raccordés. L’import `.xemf` conserve le vrai fournisseur, y compris Google Satellite, et les anciens noms français du clone restent compatibles.

Google Satellite version 930 répond désormais 404 ; l’URL Google satellite corrigée a renvoyé un JPEG HTTP200. Dix services renvoient des images à la vérification ponctuelle réseau. OSM Relief renvoie une image portant **API Key Required** : configuration de clé nécessaire. HERE/Mapquest restent visibles et configurables avec les identifiants de l’utilisateur, sans copier les identifiants tiers de l’artefact ni substituer Esri. Le dialogue accepte un nom intégré identique pour remplacer son URL et conserve le réglage dans le projet.

Les tests métier dédiés passent 7/7 ; les quatre parcours navigateur, y compris la sauvegarde de personnalisation, sont repassés ensemble dans la campagne finale de 46 parcours Edge (171 tests métier globaux). Les tuiles de ces parcours sont interceptées. La texture calibrée 3D est implémentée et compile ; comparaison visuelle native détaillée encore non faite.

Preuves, liens officiels et limites : [audit complet](../reference/map-providers-native-audit-2026-10-22.md), [mesure réseau](../reference/map-provider-network-check.json), [image Thunderforest avec filigrane](../reference/thunderforest-tile-probe.png).
