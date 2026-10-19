# Analyse TD_SwigCoreMgd.cs (438 043 lignes)

## Verdict : AUCUNE logique EMF — 100% infrastructure ODA/Teigha

## Namespace
- `Teigha.Core` — 1 454 classes

## Classes clés

### Géométrie
- `OdGePoint3d` (x, y, z), `OdGeVector3d`, `OdGeMatrix3d`
- `OdGeBoundBlock3d` — boîte englobante AABB
- `OdGeCircArc3d`, `OdGeLineSeg3d`, `OdGeNurbCurve3d`
- Constantes : `OdaPI`, `OdaPI2`, `OdaPI4`, `Oda2PI`

### Rendu
- `OdGiDrawable` — base des objets dessinables
- `OdGiMapper` — mapping textures
- `ShellData`, `MeshData` — données de maillage

### Utilitaires
- `Helpers` — factory d'objets via réflexion + SWIG P/Invoke
- `ObjectExtensions.CastTo<T>()` — cast cross-boundary native↔managed

## Composant découvert : devDept Eyeshot v2020
- **407 722 lignes** de code décompilé (obfusqué)
- Namespace : `devDept.Eyeshot`
- Rôle : viewport CAD 3D/2D (WinForms)
- Classes : `Model`, `ViewportLayout`, `Camera`, `Grid`, `Legend`
- Formats supportés : DWG, DXF, 3DS, OBJ, STL, STEP, IGES, LAS
- Libs bundlées : ClipperLib, TriangleNet, MIConvexHull, GeometryGym
- Protection : DeployLX.Licensing.v5

## Conclusion
Aucune formule EMF, fréquence, puissance, limite de sécurité ou constante de propagation dans les 438k lignes. C'est du pur moteur graphique CAO.
