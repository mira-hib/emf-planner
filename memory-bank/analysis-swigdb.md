# Analyse TD_SwigDbMgd.cs (339 487 lignes)

## Verdict : AUCUNE logique EMF — 100% base de données DWG/DXF ODA

## Namespace
- `Teigha.TD` — 866 classes

## Classes clés

### Entités de dessin
- `OdDbLine`, `OdDbCircle`, `OdDbArc`, `OdDbPolyline`
- `OdDb3dSolid`, `OdDbSpline`, `OdDbHatch`
- `OdDbText`, `OdDbMText` — texte
- `OdDbBlockTableRecord`, `OdDbBlockReference` — blocs/symboles

### Couches et structure
- `OdDbLayerTable`, `OdDbLayerTableRecord`
- `OdDbLayout` (hérite `OdDbPlotSettings`)

### Géo-référencement
- `OdDbGeoData` — système de coordonnées géographiques
  - Types : Local / Grid / Geographic
  - Scale estimation, correction niveau mer
  - Maillage de points, direction nord, projection

### Persistance
- `OdDbDatabase.readFile(filename)` / `writeFile(filename, type, version)`
- Tout via P/Invoke natif vers `TD_SwigDb_21.4_11.dll`

## Conclusion
Wrapper .NET standard du SDK ODA pour la manipulation de fichiers DWG/DXF. Aucun calcul EMF. Utile pour comprendre comment EMFPlanner dessine les résultats sur la carte CAO, mais pas pour reproduire la logique métier.
