import { useEffect, useRef, useState } from "react";
import type { Contour, FieldGrid } from "../types";
import { contours } from "../lib/engine";
import { usePlanner } from "../lib/usePlanner";
import { cutCoordinates, cutExposureBounds, cutWorld, verticalProfile } from "../lib/vertical-profile";
import { newPoint, uid } from "../lib/project";
import { terrainHeight, toGeo } from "../lib/geometry";
export default function FieldPlot({
  grid,
  label = "Coupe verticale",
  overlays,
  legend,
}: {
  grid?: FieldGrid;
  label?: string;
  overlays?: Contour[];
  legend?: { name: string; color: string }[];
}) {
  const canvas = useRef<HTMLCanvasElement>(null);
  const a = usePlanner(), project = a.project;
  const cut = project.vertical_cuts.find(c => c.id === a.selectedCut) ?? project.vertical_cuts[0];
  const [cursor, setCursor] = useState<[number, number] | null>(null), [menu, setMenu] = useState(false);
  const options = { length: true, bottom: true, maximum: true, top: false, distance: true, dimensions: true, points: true, terrain_line: false, ...project.settings.vertical_display };
  const profile = grid && cut ? verticalProfile(project, cut, grid.x0, grid.x0 + grid.dx * (grid.width - 1)) : null;
  const locate = (event: React.MouseEvent<HTMLDivElement>) => {
    if (!grid) return;
    const area = event.currentTarget.getBoundingClientRect();
    setCursor([grid.x0 + (event.clientX - area.left) / area.width * grid.dx * (grid.width - 1),
      grid.y0 + (1 - (event.clientY - area.top) / area.height) * grid.dy * (grid.height - 1)]);
  };
  const addDimension = () => {
    if (!cut || !cursor) return;
    const dimension = { id: uid(), x: cursor[0], y1: terrainHeight(project, ...cutWorld(cut, cursor[0])) - terrainHeight(project, 0, 0), y2: cursor[1] };
    a.commit(p => ({ ...p, vertical_cuts: p.vertical_cuts.map(c => c.id === cut.id ? { ...c, dimensions: [...c.dimensions ?? [], dimension] } : c) }), "Cote ajoutée", false); setMenu(false);
  };
  const addControlPoint = () => {
    if (!cut || !cursor) return;
    const xy = cutWorld(cut, cursor[0]), geo = toGeo(...xy, project.origin);
    const point = newPoint(project.origin, String(project.control_points.length + 1));
    const height = Math.max(0, cursor[1] + terrainHeight(project, 0, 0) - terrainHeight(project, ...xy));
    Object.assign(point, { x_m: xy[0], y_m: xy[1], lat: geo[0], lng: geo[1], height_m: height, heights: [{ id: uid(), height_m: height, attenuation_db: 0, measured: 0, description: "" }] });
    a.commit(p => ({ ...p, control_points: [...p.control_points, point] }), "Point créé sur la coupe", "stale-cuts"); a.setSelectedPoint(point.id); setMenu(false);
  };
  useEffect(() => {
    if (!grid || !grid.values.length || !canvas.current) return;
    const context = canvas.current.getContext("2d");
    if (!context) return;
    const image = context.createImageData(grid.width, grid.height);
    grid.values.forEach((v, i) => {
      const idx =
          ((grid.height - 1 - Math.floor(i / grid.width)) * grid.width +
            (i % grid.width)) *
          4,
        t = Math.min(1, Math.max(0, v)),
        color =
          v >= 1
            ? [218, 42, 70]
            : t > 0.5
              ? [244, 173, 67]
              : t > 0.1
                ? [99, 162, 218]
                : [224, 240, 246];
      image.data[idx] = color[0];
      image.data[idx + 1] = color[1];
      image.data[idx + 2] = color[2];
      image.data[idx + 3] = v < 0.01 ? 0 : Math.round(30 + t * 170);
    });
    context.putImageData(image, 0, 0);
  }, [grid]);
  const lines = overlays?.length
    ? overlays
    : grid
      ? [
          { level: 1, color: "#c02148" },
          { level: 0.5, color: "#dd973c" },
          { level: 0.1, color: "#275bd4" },
        ].map((l) => contours(grid, l.level, l.color))
      : [];
  return (
    <div className="field-plot">
      <div className="plot-title">{label}</div>
      <details><summary>Options d’affichage de la coupe</summary><div className="cut-display-options">
        {Object.entries({ length: "Longueur de la zone", bottom: "Hauteur basse", maximum: "Hauteur du maximum", top: "Hauteur haute", distance: "Distance minimale", dimensions: "Cotes supplémentaires", points: "Points de contrôle", terrain_line: "Ligne au-dessus du terrain" }).map(([key, text]) =>
          <label key={key}><input type="checkbox" checked={options[key as keyof typeof options]} onChange={event => a.settings({ vertical_display: { ...options, [key]: event.target.checked } })}/>{text}</label>)}
        <label>Hauteur au-dessus du terrain, m<input aria-label="Hauteur au-dessus du terrain, m" type="number" min="0" max="1000" step="0.1" value={project.settings.terrain_line_height_m ?? 2} onChange={event => {
          const value = Number(event.target.value); if (Number.isFinite(value) && value >= 0 && value <= 1000) a.settings({ terrain_line_height_m: value });
        }}/></label>
      </div></details>
      {grid ? (
        <>
          <div className="plot-area" onMouseMove={locate} onContextMenu={event => { event.preventDefault(); locate(event); setMenu(true); }}>
            <canvas
              ref={canvas}
              width={grid.width}
              height={grid.height}
              aria-label={label}
            />
            <svg
              viewBox={`${grid.x0} ${-grid.y0 - grid.dy * (grid.height - 1)} ${grid.dx * (grid.width - 1)} ${grid.dy * (grid.height - 1)}`}
              preserveAspectRatio="none"
            >
              {profile && <>
                <polyline points={profile.terrain.map(([x,y])=>`${x},${-y}`).join(" ")} fill="none" stroke="#555" strokeWidth="1" vectorEffect="non-scaling-stroke"/>
                {options.terrain_line && <polyline aria-label="Ligne au-dessus du terrain" points={profile.terrain.map(([x,y])=>`${x},${-y-(project.settings.terrain_line_height_m ?? 2)}`).join(" ")} fill="none" stroke="#888" strokeDasharray="5 3" strokeWidth="1" vectorEffect="non-scaling-stroke"/>}
                {profile.buildings.map(b=><g key={b.id}><rect x={b.left} y={-b.top} width={b.right-b.left} height={b.top-b.bottom} fill="#ddd" stroke="#444" strokeWidth="1" vectorEffect="non-scaling-stroke"/><title>{b.name} : {(b.top-b.bottom).toFixed(1)} m</title></g>)}
                {profile.towers.map(t=><path key={t.id} d={`M${t.distance-t.width/2} ${-t.foundation}L${t.distance} ${-t.top}L${t.distance+t.width/2} ${-t.foundation}`} fill="none" stroke="#555" strokeWidth="1" vectorEffect="non-scaling-stroke"/>)}
              </>}
              {lines.map((c, i) => (
                <path
                  key={i}
                  data-field-contour=""
                  d={c.segments
                    .map(
                      (s) => `M${s[0][0]} ${-s[0][1]}L${s[1][0]} ${-s[1][1]}`,
                    )
                    .join(" ")}
                  fill="none"
                  stroke={c.color}
                  strokeWidth="1.3"
                  vectorEffect="non-scaling-stroke"
                />
              ))}
              {cut && options.dimensions && cut.dimensions?.map(d=><g key={d.id} stroke="red" fill="red"><path d={`M${d.x} ${-d.y1}V${-d.y2}M${d.x-1} ${-d.y1}h2M${d.x-1} ${-d.y2}h2`} fill="none" strokeWidth="1" vectorEffect="non-scaling-stroke"/><text x={d.x+1} y={-(d.y1+d.y2)/2} stroke="none" fontSize={grid.dy*(grid.height-1)/40}>{Math.abs(d.y2-d.y1).toFixed(1)} m</text></g>)}
              {cut && options.points && project.control_points.filter(p=>p.group_active!==false).map(point=> {
                const position=cutCoordinates(cut,point.x_m,point.y_m); if(Math.abs(position.offset)>2)return null;
                return (point.heights ?? [{height_m:point.height_m}]).map((height,i)=><g key={`${point.id}:${i}`}><circle cx={position.distance} cy={-(height.height_m+terrainHeight(project,point.x_m,point.y_m)-terrainHeight(project,0,0))} r={grid.dy*(grid.height-1)/150} fill="#275bd4"/><title>{point.name} : {height.height_m} m</title></g>);
              })}
            </svg>
            <div className="plot-axis-y">H, m</div>
            <div className="plot-axis-x">Distance, m</div>
            <span className="plot-top">
              {(grid.y0 + grid.dy * (grid.height - 1)).toFixed(0)}
            </span>
            <span className="plot-bottom">{grid.y0.toFixed(0)}</span>
            <span className="plot-left">{grid.x0.toFixed(0)}</span>
            <span className="plot-right">
              {(grid.x0 + grid.dx * (grid.width - 1)).toFixed(0)}
            </span>
          </div>
          {menu && <div role="menu"><button role="menuitem" onClick={addDimension}>Ajouter une cote ici</button><button role="menuitem" onClick={addControlPoint}>Ajouter un point de mesure</button><button role="menuitem" onClick={()=>setMenu(false)}>Fermer le menu</button></div>}
          {cursor && <output>Distance {cursor[0].toFixed(1)} m · Hauteur {cursor[1].toFixed(1)} m</output>}
          {(() => { const bounds = cutExposureBounds(lines, d => cut ? terrainHeight(project, ...cutWorld(cut, d)) - terrainHeight(project, 0, 0) : 0); return bounds && <div className="cut-dimensions" style={{color:"#bb1111"}}>
            {options.length && <span>Longueur {bounds.right.toFixed(1)} m </span>}{options.bottom && <span>Bas {bounds.bottom.toFixed(1)} m </span>}{options.maximum && <span>Maximum {bounds.maxHeight.toFixed(1)} m </span>}{options.top && <span>Haut {bounds.top.toFixed(1)} m </span>}{options.distance && <span>Distance minimale {bounds.minDistance.toFixed(1)} m</span>}
          </div>; })()}
          {!grid.values.length && (
            <p className="hint">
              Contours enregistrés dans le projet d’origine. Recalculez la coupe
              pour obtenir les valeurs de champ.
            </p>
          )}
          <div className="plot-legend">
            {(
              legend ?? [
                { name: "100 %", color: "#c02148" },
                { name: "50 %", color: "#dd973c" },
                { name: "10 %", color: "#275bd4" },
              ]
            ).map((l, i) => (
              <span key={i} style={{ color: l.color }}>
                ― {l.name}
              </span>
            ))}
          </div>
        </>
      ) : (
        <div className="empty-workspace">
          <span>╱╲</span>
          <p>Sélectionnez une coupe et lancez le calcul.</p>
        </div>
      )}
    </div>
  );
}
