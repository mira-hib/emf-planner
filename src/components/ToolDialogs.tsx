import { useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import {
  addHorizontalHeights,
  addVerticalAngles,
  alignPointsToAzimuth,
  rangeValues,
} from "../lib/editing";
import { uid } from "../lib/project";
import { terrainHeight } from "../lib/geometry";
import { TILES } from "./PlannerMap";
import Modal from "./Modal";
import DataGrid from "./DataGrid";
import type { CustomMap } from "../types";
export const DIALOG_COMMANDS = [
  "Hauteurs par intervalle",
  "Coupes par intervalle d’angles",
  "Calcul d’un point par plage de hauteurs",
  "Renommer le groupe",
  "Supprimer le groupe",
  "Points sur l’azimut des antennes",
  "Ajouter un sommet",
  "Supprimer un sommet",
  "Ajouter un sommet de relief",
  "Supprimer un sommet de relief",
  "Ajouter un nœud",
  "Supprimer un nœud",
  "Scinder une ligne",
  "Décaler les bâtiments",
  "Décaler les objets",
  "Liste des propriétaires",
  "Fonds de carte personnalisés",
  "Coordonnées en degrés / minutes / secondes",
  "Maxima verticaux avec décalage",
];
export default function ToolDialogs() {
  const a = usePlanner(),
    { project: p, command, point, commit } = a;
  const group = a.selectedGroup || point?.group || "Groupe 1";
  const object =
    a.section === "buildings"
      ? p.buildings.find((b) => b.id === a.selectedObject)
      : a.section === "terrain"
        ? p.terrain_contours?.find((c) => c.id === a.selectedObject)
        : p.lines.find((l) => l.id === a.selectedObject);
  const [start, setStart] = useState(0),
    [end, setEnd] = useState(command.includes("angles") ? 350 : 30),
    [step, setStep] = useState(command.includes("angles") ? 10 : 1),
    [error, setError] = useState("");
  const [name, setName] = useState(group),
    [tolerance, setTolerance] = useState(2),
    [dx, setDx] = useState(1),
    [dy, setDy] = useState(0),
    [all, setAll] = useState(!object),
    [offset, setOffset] = useState(0.5);
  const [vertices, setVertices] = useState<[number, number][]>(
      object?.points.map((c) => [...c]) ?? [],
    ),
    [node, setNode] = useState("1");
  const [owners, setOwners] = useState(
    (
      p.owner_names ?? [
        ...new Set(p.sources.map((s) => s.owner).filter(Boolean)),
      ]
    ).join("\n"),
  );
  const [maps, setMaps] = useState<(CustomMap & { id: string })[]>(
    (p.custom_maps ?? []).map((m, i) => ({ ...m, id: String(i) })),
  );
  const [format, setFormat] = useState(
    p.settings.coordinate_format ?? "decimal",
  );
  const close = () => a.setCommand("");
  const accept = (action: () => void) => {
    try {
      action();
      close();
    } catch (e) {
      setError(e instanceof Error ? e.message : String(e));
    }
  };
  const number = (
    label: string,
    value: number,
    set: (v: number) => void,
    min?: number,
    max?: number,
  ) => (
    <label>
      {label}
      <input
        aria-label={label}
        type="number"
        step="any"
        value={value}
        min={min}
        max={max}
        onChange={(e) => set(Number(e.target.value))}
      />
    </label>
  );
  const isRange = [
    "Hauteurs par intervalle",
    "Coupes par intervalle d’angles",
    "Calcul d’un point par plage de hauteurs",
  ].includes(command);
  const isVertices = /sommet|nœud|Scinder/.test(command);
  let action = () => {};
  if (isRange)
    action = () => {
      if (start < 0 || end > (command.includes("angles") ? 360 : 10000))
        throw new Error("Les bornes de la plage sont invalides.");
      const values = rangeValues(start, end, step);
      if (command.includes("angles"))
        commit((v) => addVerticalAngles(v, values));
      else if (command.includes("point")) {
        if (!point) throw new Error("Sélectionnez un point.");
        commit((v) => ({
          ...v,
          control_points: v.control_points.map((c) =>
            c.id === point.id
              ? {
                  ...c,
                  heights: values.map(
                    (height_m) =>
                      c.heights?.find((h) => h.height_m === height_m) ?? {
                        id: uid(),
                        height_m,
                        attenuation_db: 0,
                        measured: 0,
                        description: "",
                      },
                  ),
                }
              : c,
          ),
        }));
        a.calculate("points", false, point.id);
      } else commit((v) => addHorizontalHeights(v, values));
    };
  else if (command === "Renommer le groupe")
    action = () => {
      if (!name.trim()) throw new Error("Saisissez un nom de groupe.");
      if (
        p.control_points.some(
          (c) => c.group === name.trim() && c.group !== group,
        )
      )
        throw new Error("Ce nom est déjà utilisé.");
      commit((v) => ({
        ...v,
        control_points: v.control_points.map((c) =>
          (c.group || "Groupe 1") === group ? { ...c, group: name.trim() } : c,
        ),
      }));
      a.setSelectedGroup(name.trim());
    };
  else if (command === "Supprimer le groupe")
    action = () => {
      commit((v) => ({
        ...v,
        control_points: v.control_points.filter(
          (c) => (c.group || "Groupe 1") !== group,
        ),
      }));
      a.setSelectedGroup("");
      a.setSelectedPoint("");
    };
  else if (command === "Points sur l’azimut des antennes")
    action = () =>
      commit((v) =>
        alignPointsToAzimuth(v, tolerance, a.selectedGroup || undefined),
      );
  else if (isVertices)
    action = () => {
      const building = a.section === "buildings";
      if (
        !object ||
        vertices.length <
          (building || (object && "closed" in object && object.closed)
            ? 3
            : 2) ||
        vertices.some((c) => !c.every(Number.isFinite))
      )
        throw new Error("Le contour ne contient pas assez de sommets valides.");
      if (command === "Scinder une ligne") {
        const index = Number(node);
        if (index <= 0 || index >= vertices.length - 1)
          throw new Error(
            "Choisissez un nœud intérieur pour scinder la ligne.",
          );
        commit((v) => ({
          ...v,
          lines: v.lines.flatMap((l) =>
            l.id === object.id
              ? [
                  { ...l, points: vertices.slice(0, index + 1) },
                  {
                    ...l,
                    id: uid(),
                    name: `${l.name} (suite)`,
                    points: vertices.slice(index),
                  },
                ]
              : [l],
          ),
        }));
      } else
        commit((v) => ({
          ...v,
          ...(building
            ? {
                buildings: v.buildings.map((b) =>
                  b.id === object.id ? { ...b, points: vertices } : b,
                ),
              }
            : a.section === "terrain"
              ? {
                  terrain_contours: v.terrain_contours?.map((c) =>
                    c.id === object.id ? { ...c, points: vertices } : c,
                  ),
                }
              : {
                  lines: v.lines.map((l) =>
                    l.id === object.id ? { ...l, points: vertices } : l,
                  ),
                }),
        }));
    };
  else if (command.startsWith("Décaler"))
    action = () => {
      if (!Number.isFinite(dx + dy)) throw new Error("Déplacement invalide.");
      const move = (points: [number, number][]) =>
        points.map(([x, y]) => [x + dx, y + dy] as [number, number]);
      commit((v) => ({
        ...v,
        ...(command.includes("bâtiments")
          ? {
              buildings: v.buildings.map((b) =>
                all || b.id === object?.id
                  ? { ...b, points: move(b.points) }
                  : b,
              ),
            }
          : {
              lines: v.lines.map((l) =>
                all || l.id === object?.id
                  ? { ...l, points: move(l.points) }
                  : l,
              ),
            }),
      }));
    };
  else if (command === "Liste des propriétaires")
    action = () =>
      commit((v) => ({
        ...v,
        owner_names: [
          ...new Set(
            owners
              .split(/\r?\n/)
              .map((n) => n.trim())
              .filter(Boolean),
          ),
        ],
      }));
  else if (command === "Fonds de carte personnalisés")
    action = () => {
      const names = new Set(Object.keys(TILES));
      for (const map of maps) {
        if (!map.name.trim() || names.has(map.name.trim()))
          throw new Error("Chaque fond doit avoir un nom distinct.");
        names.add(map.name.trim());
        if (!["{x}", "{y}", "{z}"].every((s) => map.url.includes(s)))
          throw new Error("L’URL doit contenir {x}, {y} et {z}.");
        const url = new URL(map.url.replace(/\{[xyz]\}/g, "0"));
        if (!["https:", "http:"].includes(url.protocol))
          throw new Error("Utilisez une URL HTTP ou HTTPS.");
      }
      commit((v) => ({
        ...v,
        custom_maps: maps.map(({ name, url, attribution }) => ({
          name: name.trim(),
          url,
          attribution,
        })),
        settings: {
          ...v.settings,
          map_provider: [
            ...Object.keys(TILES),
            ...maps.map((m) => m.name.trim()),
          ].includes(v.settings.map_provider)
            ? v.settings.map_provider
            : "Carto clair",
        },
      }));
    };
  else if (command === "Coordonnées en degrés / minutes / secondes")
    action = () => a.settings({ coordinate_format: format });
  else if (command === "Maxima verticaux avec décalage")
    action = () => {
      const heights = a.verticalResults.flatMap((r) =>
        r.contours.segments.flatMap((s) => s.map((v) => v[1])),
      );
      if (!heights.length)
        throw new Error(
          "Calculez d’abord des coupes verticales contenant un contour EMF.",
        );
      commit((v) =>
        addHorizontalHeights(
          v,
          a.verticalResults
            .filter((r) => r.contours.segments.length)
            .map(
              (r) =>
                Math.min(
                  ...r.contours.segments.flatMap((s) => s.map((pt) => pt[1])),
                ) -
                terrainHeight(p, 0, 0) +
                offset,
            ),
        ),
      );
    };
  return (
    <Modal
      title={command}
      onClose={close}
      wide={isVertices || command === "Fonds de carte personnalisés"}
    >
      {isRange ? (
        <>
          <p className="hint">
            Les valeurs sont arrondies au dixième. Les coupes déjà présentes
            sont conservées.
          </p>
          {number(
            command.includes("angles")
              ? "Azimut initial, °"
              : "Hauteur initiale, m",
            start,
            setStart,
            0,
          )}
          {number(
            command.includes("angles")
              ? "Azimut final, °"
              : "Hauteur finale, m",
            end,
            setEnd,
            0,
          )}
          {number(
            command.includes("angles")
              ? "Pas angulaire, °"
              : "Pas en hauteur, m",
            step,
            setStep,
            0.1,
          )}
          {command.includes("point") && (
            <p>
              Point {point?.name} : sa liste de hauteurs sera remplacée par la
              plage, puis calculée.
            </p>
          )}
        </>
      ) : command === "Renommer le groupe" ? (
        <label>
          Nom du groupe
          <input
            aria-label="Nom du groupe"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </label>
      ) : command === "Supprimer le groupe" ? (
        <p>
          Supprimer le groupe « {group} » et ses{" "}
          {
            p.control_points.filter((c) => (c.group || "Groupe 1") === group)
              .length
          }{" "}
          points ? Cette modification peut être annulée.
        </p>
      ) : command === "Points sur l’azimut des antennes" ? (
        <>
          <p>
            Projeter les points proches sur l’axe avant de l’antenne active la
            plus proche, jusqu’à 1 000 m.
          </p>
          {number(
            "Distance maximale de l’axe, m",
            tolerance,
            setTolerance,
            0.1,
          )}
          <p className="hint">
            Portée : {a.selectedGroup || "tous les groupes"}.
          </p>
        </>
      ) : isVertices ? (
        <>
          <p className="hint">
            Sélectionnez une ligne du tableau pour ajouter ou retirer un sommet.
            Les coordonnées sont en mètres.
          </p>
          <div className="vertex-grid">
            <DataGrid
              label="Sommets de l’objet"
              rows={vertices.map(([x, y], i) => ({ id: String(i), x, y }))}
              columns={[
                { key: "x", label: "X, m", kind: "number", width: 160 },
                { key: "y", label: "Y, m", kind: "number", width: 160 },
              ]}
              selected={node}
              onSelect={setNode}
              onUpdate={(id, key, value) =>
                setVertices((rows) =>
                  rows.map((v, i) =>
                    i === Number(id)
                      ? key === "x"
                        ? [Number(value), v[1]]
                        : [v[0], Number(value)]
                      : v,
                  ),
                )
              }
            />
          </div>
          <div className="button-row">
            <button
              onClick={() => {
                const i = Math.min(Number(node), vertices.length - 1),
                  v = vertices[i],
                  n = vertices[(i + 1) % vertices.length];
                setVertices((rows) => [
                  ...rows.slice(0, i + 1),
                  [(v[0] + n[0]) / 2, (v[1] + n[1]) / 2],
                  ...rows.slice(i + 1),
                ]);
                setNode(String(i + 1));
              }}
            >
              Insérer après le sommet
            </button>
            <button
              disabled={
                vertices.length <=
                (a.section === "buildings" ||
                (object && "closed" in object && object.closed)
                  ? 3
                  : 2)
              }
              onClick={() => {
                setVertices((v) => v.filter((_, i) => i !== Number(node)));
                setNode("0");
              }}
            >
              Retirer ce sommet
            </button>
          </div>
        </>
      ) : command.startsWith("Décaler") ? (
        <>
          {number("Déplacement X, m", dx, setDx)}
          {number("Déplacement Y, m", dy, setDy)}
          <label>
            <input
              type="checkbox"
              checked={all}
              disabled={!object}
              onChange={(e) => setAll(e.target.checked)}
            />
            Déplacer tous les objets de cette rubrique
          </label>
        </>
      ) : command === "Liste des propriétaires" ? (
        <>
          <p>Un propriétaire par ligne.</p>
          <textarea
            aria-label="Propriétaires"
            rows={12}
            value={owners}
            onChange={(e) => setOwners(e.target.value)}
          />
        </>
      ) : command === "Fonds de carte personnalisés" ? (
        <>
          <div className="vertex-grid">
            <DataGrid
              label="Fonds de carte personnalisés"
              rows={maps}
              selected={node}
              onSelect={setNode}
              columns={[
                { key: "name", label: "Nom", width: 130 },
                { key: "url", label: "URL des tuiles", width: 310 },
                { key: "attribution", label: "Attribution", width: 150 },
              ]}
              onUpdate={(id, key, value) =>
                setMaps((rows) =>
                  rows.map((m) =>
                    m.id === id ? { ...m, [key]: String(value) } : m,
                  ),
                )
              }
            />
          </div>
          <div className="button-row">
            <button
              onClick={() =>
                setMaps((rows) => [
                  ...rows,
                  {
                    id: uid(),
                    name: "Nouveau fond",
                    url: "https://exemple.org/{z}/{x}/{y}.png",
                    attribution: "",
                  },
                ])
              }
            >
              Ajouter un fond
            </button>
            <button
              disabled={!maps.some((m) => m.id === node)}
              onClick={() =>
                setMaps((rows) => rows.filter((m) => m.id !== node))
              }
            >
              Retirer ce fond
            </button>
          </div>
          <p className="hint">
            Les fonds enregistrés apparaissent dans le sélecteur de carte.
          </p>
        </>
      ) : command === "Coordonnées en degrés / minutes / secondes" ? (
        <>
          {(["decimal", "dm", "dms"] as const).map((v, i) => (
            <label key={v}>
              <input
                type="radio"
                checked={format === v}
                onChange={() => setFormat(v)}
              />
              {
                [
                  "Degrés décimaux : 5,345°",
                  "Degrés et minutes : 5° 20,700′",
                  "Degrés, minutes, secondes : 5° 20′ 42″",
                ][i]
              }
            </label>
          ))}
        </>
      ) : command === "Maxima verticaux avec décalage" ? (
        number("Ajout à la hauteur inférieure du contour, m", offset, setOffset)
      ) : null}
      {error && (
        <p className="dialog-error" role="alert">
          {error}
        </p>
      )}
      <footer>
        <button onClick={close}>Annuler</button>
        <button onClick={() => accept(action)}>
          {command === "Supprimer le groupe"
            ? "Supprimer le groupe"
            : command === "Scinder une ligne"
              ? "Scinder à ce sommet"
              : "Appliquer"}
        </button>
      </footer>
    </Modal>
  );
}
