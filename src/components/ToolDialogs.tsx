import { useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import {
  addHorizontalHeights,
  addVerticalAngles,
  alignPointsToAzimuth,
  rangeValues,
} from "../lib/editing";
import { buildStandardAliases } from "../lib/useExtraActions";
import { uid } from "../lib/project";
import { terrainHeight } from "../lib/geometry";
import { exportCutReport } from "../lib/files";
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
  "Choisir une icône",
  "Rapport des coupes horizontales",
  "Rapport des coupes verticales",
  "Tableaux de champ des coupes verticales",
  "Tableaux d’exposition des coupes verticales",
  "Alias des tableaux et graphiques",
  "Alias personnalisés",
  "Liste des 14 alias standard",
  "Mise en page CAO avancée",
  "Dossier des diagrammes d’antennes",
  "Cache des cartes",
  "Paramètres proxy",
  "Langue de l’interface",
  "Calcul inverse de puissance",
  "Coordonnées du site",
  "Relief DEM / SRTM",
  "Ajouter une cote",
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
  const selectedLine = p.lines.find((l) => l.id === a.selectedObject);
  const [selectedIcon, setSelectedIcon] = useState(selectedLine?.icon ?? "");
  const [customAliases, setCustomAliases] = useState<Record<string, string>>(
    p.settings.report_aliases ?? {},
  );
  // --- CAD layout state ---
  const [cadPaper, setCadPaper] = useState("A3");
  const [cadOrientation, setCadOrientation] = useState<
    "portrait" | "landscape"
  >("landscape");
  const [cadScale, setCadScale] = useState(1000);
  const [cadTitle, setCadTitle] = useState(p.name);
  const [cadLayers, setCadLayers] = useState({
    contours: true,
    buildings: true,
    lines: true,
    sources: true,
    points: true,
  });
  // --- Settings dialog state ---
  const [antennaFolder, setAntennaFolder] = useState(
    p.settings.antenna_folder ?? "",
  );
  const [proxyHost, setProxyHost] = useState(p.settings.proxy?.host ?? "");
  const [proxyPort, setProxyPort] = useState(p.settings.proxy?.port ?? 8080);
  const [proxyUser, setProxyUser] = useState(p.settings.proxy?.user ?? "");
  const [language, setLanguage] = useState(p.settings.language ?? "fr");
  // --- Inverse power calc ---
  const [targetField, setTargetField] = useState(28);
  const [targetDistance, setTargetDistance] = useState(100);
  // --- Site coordinates ---
  const [siteLat, setSiteLat] = useState(p.origin[0]);
  const [siteLng, setSiteLng] = useState(p.origin[1]);
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
  else if (command === "Choisir une icône")
    action = () => {
      if (!selectedLine) throw new Error("Sélectionnez une ligne.");
      commit((v) => ({
        ...v,
        lines: v.lines.map((l) =>
          l.id === selectedLine.id
            ? { ...l, icon: selectedIcon || undefined }
            : l,
        ),
      }));
    };
  else if (command === "Coordonnées en degrés / minutes / secondes")
    action = () => a.settings({ coordinate_format: format });
  else if (command === "Alias personnalisés")
    action = () => {
      // Remove empty keys
      const cleaned = Object.fromEntries(
        Object.entries(customAliases).filter(([k, v]) => k.trim() && v.trim()),
      );
      commit((v) => ({
        ...v,
        settings: {
          ...v.settings,
          report_aliases: Object.keys(cleaned).length ? cleaned : undefined,
        },
      }));
    };
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
  else if (command === "Mise en page CAO avancée")
    action = () => {
      a.setStatus(
        `Mise en page CAO : ${cadPaper} ${cadOrientation}, 1:${cadScale}, titre « ${cadTitle} »`,
      );
    };
  else if (command === "Dossier des diagrammes d’antennes")
    action = () => {
      commit((v) => ({
        ...v,
        settings: { ...v.settings, antenna_folder: antennaFolder || undefined },
      }));
      a.setStatus(
        antennaFolder
          ? `Dossier d’antennes : ${antennaFolder}`
          : "Dossier d’antennes réinitialisé.",
      );
    };
  else if (command === "Cache des cartes")
    action = () => {
      a.setStatus("Cache des cartes vidé.");
    };
  else if (command === "Paramètres proxy")
    action = () => {
      const proxy = proxyHost.trim()
        ? {
            host: proxyHost.trim(),
            port: proxyPort,
            ...(proxyUser.trim() ? { user: proxyUser.trim() } : {}),
          }
        : undefined;
      commit((v) => ({
        ...v,
        settings: { ...v.settings, proxy },
      }));
      a.setStatus(
        proxy ? `Proxy : ${proxy.host}:${proxy.port}` : "Proxy désactivé.",
      );
    };
  else if (command === "Langue de l’interface")
    action = () => {
      commit((v) => ({
        ...v,
        settings: { ...v.settings, language },
      }));
      const labels: Record<string, string> = {
        fr: "Francais",
        en: "English",
        ru: "Russkij",
        de: "Deutsch",
      };
      a.setStatus(`Langue : ${labels[language] ?? language}`);
    };
  else if (command === "Calcul inverse de puissance")
    action = () => {
      if (targetField <= 0 || targetDistance <= 0)
        throw new Error("Les valeurs doivent être strictement positives.");
      const requiredEirp = (targetField * targetDistance) ** 2 / 30;
      const source = p.sources.find((s) => s.id === a.selectedSource);
      if (!source) throw new Error("Sélectionnez une source.");
      const gain = 10 ** (source.gain_dbi / 10);
      const requiredPower = requiredEirp / gain;
      a.setStatus(
        `Pour E = ${targetField} V/m à ${targetDistance} m : PIRE = ${requiredEirp.toFixed(1)} W, ` +
          `puissance TX = ${requiredPower.toFixed(2)} W (gain ${source.gain_dbi} dBi).`,
      );
    };
  else if (command === "Coordonnées du site")
    action = () => {
      if (!Number.isFinite(siteLat) || !Number.isFinite(siteLng))
        throw new Error("Coordonnées invalides.");
      a.changeOrigin([siteLat, siteLng]);
    };
  else if (command === "Relief DEM / SRTM")
    action = () => {
      a.setStatus(
        "Import DEM/SRTM en cours — cette fonctionnalité nécessite un serveur d'élévation externe.",
      );
    };
  else if (command === "Ajouter une cote")
    action = () => {
      const cut = p.vertical_cuts.find((c) => c.id === a.selectedCut);
      if (!cut) throw new Error("Sélectionnez une coupe verticale.");
      const dim = {
        id: uid(),
        x: cut.forward_m / 2,
        y1: 0,
        y2: cut.max_height_m / 2,
      };
      commit((v) => ({
        ...v,
        vertical_cuts: v.vertical_cuts.map((c) =>
          c.id === cut.id
            ? { ...c, dimensions: [...(c.dimensions ?? []), dim] }
            : c,
        ),
      }));
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
      ) : command === "Choisir une icône" ? (
        <>
          <p className="hint">
            Sélectionnez une icône pour la ligne{" "}
            {selectedLine
              ? `« ${selectedLine.name} »`
              : "(aucune ligne sélectionnée)"}
            .
          </p>
          <div style={{ display: "flex", gap: 8, flexWrap: "wrap" }}>
            {[
              { value: "tower", label: "Tour" },
              { value: "tree", label: "Arbre" },
              { value: "pole", label: "Poteau" },
              { value: "sign", label: "Panneau" },
            ].map((ic) => (
              <label
                key={ic.value}
                style={{ display: "flex", alignItems: "center", gap: 4 }}
              >
                <input
                  type="radio"
                  name="icon"
                  checked={selectedIcon === ic.value}
                  onChange={() => setSelectedIcon(ic.value)}
                />
                {ic.label}
              </label>
            ))}
            <label style={{ display: "flex", alignItems: "center", gap: 4 }}>
              <input
                type="radio"
                name="icon"
                checked={selectedIcon === ""}
                onChange={() => setSelectedIcon("")}
              />
              Aucune
            </label>
          </div>
        </>
      ) : command === "Rapport des coupes horizontales" ? (
        <>
          <h3>Coupes horizontales calculées</h3>
          <table className="report-preview-table">
            <thead>
              <tr>
                <th>Coupe</th>
                <th>Hauteur (m)</th>
                <th>Segments</th>
              </tr>
            </thead>
            <tbody>
              {a.cutResults.map((r, i) => (
                <tr key={r.id}>
                  <td>H{i + 1}</td>
                  <td>{r.contours.height_m.toFixed(1)}</td>
                  <td>{r.contours.segments.length}</td>
                </tr>
              ))}
            </tbody>
          </table>
          <p className="hint">
            Export PDF du tableau des coupes. Les valeurs de champ nécessitent
            un recalcul si seuls les contours ont été importés.
          </p>
          <div className="button-row">
            <button
              onClick={() => {
                void a.task(
                  () => exportCutReport(p, "horizontal", a.cutResults),
                  "Rapport PDF des coupes exporté",
                );
                close();
              }}
            >
              Exporter PDF
            </button>
            <button
              disabled
              title="Rapport Word des coupes pas encore implémenté"
            >
              Exporter Word
            </button>
          </div>
        </>
      ) : command === "Rapport des coupes verticales" ? (
        <>
          <h3>Coupes verticales calculées</h3>
          <table className="report-preview-table">
            <thead>
              <tr>
                <th>Coupe</th>
                <th>Azimut (deg)</th>
                <th>Segments</th>
              </tr>
            </thead>
            <tbody>
              {a.verticalResults.map((r, i) => {
                const cut = p.vertical_cuts.find((c) => c.id === r.cut_id);
                return (
                  <tr key={r.id}>
                    <td>V{i + 1}</td>
                    <td>{cut?.azimuth_deg.toFixed(1) ?? "?"}</td>
                    <td>{r.contours.segments.length}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
          <p className="hint">
            Export PDF du tableau des coupes. Les valeurs de champ nécessitent
            un recalcul si seuls les contours ont été importés.
          </p>
          <div className="button-row">
            <button
              onClick={() => {
                void a.task(
                  () => exportCutReport(p, "vertical", a.verticalResults),
                  "Rapport PDF des coupes exporté",
                );
                close();
              }}
            >
              Exporter PDF
            </button>
            <button
              disabled
              title="Rapport Word des coupes pas encore implémenté"
            >
              Exporter Word
            </button>
          </div>
        </>
      ) : command === "Tableaux de champ des coupes verticales" ? (
        <>
          <h3>Champ electrique - Coupes verticales</h3>
          <div className="vertex-grid">
            <DataGrid
              label="Champ des coupes verticales"
              rows={a.verticalResults.flatMap((r) => {
                const cut = p.vertical_cuts.find((c) => c.id === r.cut_id);
                return r.grid.values.length
                  ? Array.from(
                      { length: Math.min(r.grid.height, 50) },
                      (_, row) => ({
                        id: `${r.id}-${row}`,
                        azimut: cut?.azimuth_deg.toFixed(1) ?? "?",
                        distance: (r.grid.x0 + row * r.grid.dx).toFixed(1),
                        champ: r.grid.fields[row]?.toFixed(4) ?? "-",
                      }),
                    )
                  : [];
              })}
              columns={[
                { key: "azimut", label: "Azimut, deg", width: 90 },
                { key: "distance", label: "Distance, m", width: 100 },
                { key: "champ", label: "E, V/m", width: 120 },
              ]}
            />
          </div>
        </>
      ) : command === "Tableaux d’exposition des coupes verticales" ? (
        <>
          <h3>Exposition - Coupes verticales</h3>
          <div className="vertex-grid">
            <DataGrid
              label="Exposition des coupes verticales"
              rows={a.verticalResults.flatMap((r) => {
                const cut = p.vertical_cuts.find((c) => c.id === r.cut_id);
                return r.grid.values.length
                  ? Array.from(
                      { length: Math.min(r.grid.height, 50) },
                      (_, row) => ({
                        id: `${r.id}-${row}`,
                        azimut: cut?.azimuth_deg.toFixed(1) ?? "?",
                        distance: (r.grid.x0 + row * r.grid.dx).toFixed(1),
                        exposition:
                          (r.grid.values[row] * 100)?.toFixed(2) ?? "-",
                      }),
                    )
                  : [];
              })}
              columns={[
                { key: "azimut", label: "Azimut, deg", width: 90 },
                { key: "distance", label: "Distance, m", width: 100 },
                { key: "exposition", label: "Exposition, %", width: 120 },
              ]}
            />
          </div>
        </>
      ) : command === "Alias des tableaux et graphiques" ||
        command === "Liste des 14 alias standard" ? (
        <>
          <h3>14 alias standard</h3>
          <table className="report-preview-table">
            <thead>
              <tr>
                <th>Alias</th>
                <th>Valeur actuelle</th>
              </tr>
            </thead>
            <tbody>
              {Object.entries(buildStandardAliases(p)).map(([key, value]) => (
                <tr key={key}>
                  <td>
                    <code>{key}</code>
                  </td>
                  <td>{value}</td>
                </tr>
              ))}
            </tbody>
          </table>
          <p className="hint">
            Liste indicative des alias. Leur remplacement dans les modèles Word
            reste à implémenter.
          </p>
        </>
      ) : command === "Mise en page CAO avancée" ? (
        <>
          <label>
            Format papier
            <select
              value={cadPaper}
              onChange={(e) => setCadPaper(e.target.value)}
            >
              {["A4", "A3", "A2", "A1", "A0"].map((s) => (
                <option key={s}>{s}</option>
              ))}
            </select>
          </label>
          <label>
            Orientation
            <select
              value={cadOrientation}
              onChange={(e) =>
                setCadOrientation(e.target.value as "portrait" | "landscape")
              }
            >
              <option value="landscape">Paysage</option>
              <option value="portrait">Portrait</option>
            </select>
          </label>
          {number("Échelle 1:", cadScale, setCadScale, 1)}
          <label>
            Titre du cartouche
            <input
              value={cadTitle}
              onChange={(e) => setCadTitle(e.target.value)}
            />
          </label>
          <fieldset>
            <legend>Calques visibles</legend>
            {(Object.keys(cadLayers) as (keyof typeof cadLayers)[]).map(
              (key) => (
                <label key={key}>
                  <input
                    type="checkbox"
                    checked={cadLayers[key]}
                    onChange={(e) =>
                      setCadLayers((prev) => ({
                        ...prev,
                        [key]: e.target.checked,
                      }))
                    }
                  />
                  {key.charAt(0).toUpperCase() + key.slice(1)}
                </label>
              ),
            )}
          </fieldset>
        </>
      ) : command === "Dossier des diagrammes d’antennes" ? (
        <label>
          Chemin du dossier d'antennes
          <input
            value={antennaFolder}
            onChange={(e) => setAntennaFolder(e.target.value)}
            placeholder="/chemin/vers/antennes"
          />
        </label>
      ) : command === "Cache des cartes" ? (
        <>
          <label>
            <input
              type="checkbox"
              checked={p.settings.cache_enabled ?? true}
              onChange={(e) => a.settings({ cache_enabled: e.target.checked })}
            />
            Activer le cache local des tuiles de carte
          </label>
          <p className="hint">
            Le cache accélère le chargement des fonds de carte déjà consultés.
          </p>
        </>
      ) : command === "Paramètres proxy" ? (
        <>
          <label>
            Serveur proxy
            <input
              value={proxyHost}
              onChange={(e) => setProxyHost(e.target.value)}
              placeholder="proxy.example.com"
            />
          </label>
          {number("Port", proxyPort, setProxyPort, 1, 65535)}
          <label>
            Utilisateur (optionnel)
            <input
              value={proxyUser}
              onChange={(e) => setProxyUser(e.target.value)}
            />
          </label>
        </>
      ) : command === "Langue de l’interface" ? (
        <>
          {(["fr", "en", "de", "es", "ru"] as const).map((lang) => (
            <label key={lang}>
              <input
                type="radio"
                checked={language === lang}
                onChange={() => setLanguage(lang)}
              />
              {
                {
                  fr: "Français",
                  en: "English",
                  de: "Deutsch",
                  es: "Español",
                  ru: "Русский",
                }[lang]
              }
            </label>
          ))}
        </>
      ) : command === "Calcul inverse de puissance" ? (
        <>
          <p className="hint">
            Calculez la puissance TX nécessaire pour atteindre un champ cible à
            une distance donnée.
          </p>
          {number("Champ cible E, V/m", targetField, setTargetField, 0.01)}
          {number("Distance, m", targetDistance, setTargetDistance, 0.1)}
          <p>
            Source active :{" "}
            {p.sources.find((s) => s.id === a.selectedSource)?.name ?? "—"}
          </p>
        </>
      ) : command === "Coordonnées du site" ? (
        <>
          <p className="hint">
            Modifiez les coordonnées géographiques de l'origine du projet.
          </p>
          {number("Latitude, °", siteLat, setSiteLat, -90, 90)}
          {number("Longitude, °", siteLng, setSiteLng, -180, 180)}
        </>
      ) : command === "Relief DEM / SRTM" ? (
        <>
          <p>
            L'import de relief DEM/SRTM télécharge les données d'élévation
            depuis un service externe.
          </p>
          <p className="hint">
            Origine : {p.origin[0].toFixed(6)}° N, {p.origin[1].toFixed(6)}° E —
            Rayon : {p.settings.radius_m} m.
          </p>
          <p>
            Cette fonctionnalité nécessite une connexion réseau et un serveur
            d'élévation compatible.
          </p>
        </>
      ) : command === "Ajouter une cote" ? (
        <>
          <p className="hint">
            Ajoute une cote de dimension à la coupe verticale sélectionnée.
          </p>
          <p>
            Coupe :{" "}
            {p.vertical_cuts
              .find((c) => c.id === a.selectedCut)
              ?.azimuth_deg.toFixed(1) ?? "—"}
            °
          </p>
          <p>
            La cote sera positionnée au milieu de la coupe. Vous pouvez la
            déplacer ensuite.
          </p>
        </>
      ) : command === "Alias personnalisés" ? (
        <>
          <h3>Alias personnalises</h3>
          <p className="hint">
            Ajoutez des paires cle-valeur. Elles seront fusionnees avec les 14
            alias standard lors de la generation du rapport.
          </p>
          <div className="vertex-grid">
            <DataGrid
              label="Alias personnalises"
              rows={Object.entries(customAliases).map(([key, value], i) => ({
                id: String(i),
                key,
                value,
              }))}
              columns={[
                { key: "key", label: "Cle", width: 180 },
                { key: "value", label: "Valeur", width: 280 },
              ]}
              onUpdate={(id, col, val) =>
                setCustomAliases((prev) => {
                  const entries = Object.entries(prev);
                  const idx = Number(id);
                  if (idx < entries.length) {
                    if (col === "key") {
                      const newEntries = [...entries];
                      newEntries[idx] = [String(val), entries[idx][1]];
                      return Object.fromEntries(newEntries);
                    }
                    const newEntries = [...entries];
                    newEntries[idx] = [entries[idx][0], String(val)];
                    return Object.fromEntries(newEntries);
                  }
                  return prev;
                })
              }
            />
          </div>
          <div className="button-row">
            <button
              onClick={() =>
                setCustomAliases((prev) => ({
                  ...prev,
                  [`{CUSTOM_${Object.keys(prev).length + 1}}`]: "",
                }))
              }
            >
              Ajouter un alias
            </button>
          </div>
        </>
      ) : command === "Mise en page CAO avancée" ? (
        <>
          <label>
            Format de papier
            <select
              value={cadPaper}
              onChange={(e) => setCadPaper(e.target.value)}
            >
              {["A4", "A3", "A2", "A1", "A0"].map((v) => (
                <option key={v}>{v}</option>
              ))}
            </select>
          </label>
          <label>
            Orientation
            <select
              value={cadOrientation}
              onChange={(e) =>
                setCadOrientation(e.target.value as "portrait" | "landscape")
              }
            >
              <option value="landscape">Paysage</option>
              <option value="portrait">Portrait</option>
            </select>
          </label>
          <label>
            Echelle
            <select
              value={cadScale}
              onChange={(e) => setCadScale(Number(e.target.value))}
            >
              {[100, 200, 500, 1000, 2000, 5000, 10000].map((v) => (
                <option key={v} value={v}>
                  1:{v}
                </option>
              ))}
            </select>
          </label>
          <label>
            Texte du cartouche
            <input
              aria-label="Texte du cartouche"
              value={cadTitle}
              onChange={(e) => setCadTitle(e.target.value)}
            />
          </label>
          <fieldset>
            <legend>Calques visibles</legend>
            {(Object.keys(cadLayers) as (keyof typeof cadLayers)[]).map(
              (key) => (
                <label key={key}>
                  <input
                    type="checkbox"
                    checked={cadLayers[key]}
                    onChange={(e) =>
                      setCadLayers((l) => ({ ...l, [key]: e.target.checked }))
                    }
                  />
                  {
                    (
                      {
                        contours: "Contours EMF",
                        buildings: "Batiments",
                        lines: "Lignes et annotations",
                        sources: "Antennes",
                        points: "Points de controle",
                      } as Record<string, string>
                    )[key]
                  }
                </label>
              ),
            )}
          </fieldset>
        </>
      ) : command === "Dossier des diagrammes d’antennes" ? (
        <>
          <p className="hint">
            Chemin du dossier contenant les fichiers de diagrammes d'antennes
            (.msi, .pla, .ant).
          </p>
          <label>
            Dossier
            <input
              aria-label="Dossier des diagrammes"
              value={antennaFolder}
              onChange={(e) => setAntennaFolder(e.target.value)}
              placeholder="/chemin/vers/diagrammes"
              style={{ width: "100%" }}
            />
          </label>
        </>
      ) : command === "Cache des cartes" ? (
        <>
          <p>
            Le cache des tuiles de carte permet d'accelerer l'affichage des
            fonds de carte deja telecharges.
          </p>
          <p className="hint">
            Taille estimee : inconnue (stockage navigateur).
          </p>
          <div className="button-row">
            <button
              onClick={() => {
                try {
                  caches.delete("map-tiles").catch(() => {});
                } catch {
                  /* caches API unavailable */
                }
                a.setStatus("Cache des cartes vide.");
              }}
            >
              Vider le cache
            </button>
          </div>
        </>
      ) : command === "Paramètres proxy" ? (
        <>
          <p className="hint">
            Configurez un proxy HTTP pour les reseaux d'entreprise. Laissez le
            champ hote vide pour desactiver le proxy.
          </p>
          <label>
            Hote
            <input
              aria-label="Hote du proxy"
              value={proxyHost}
              onChange={(e) => setProxyHost(e.target.value)}
              placeholder="proxy.example.com"
            />
          </label>
          {number("Port", proxyPort, setProxyPort, 1, 65535)}
          <label>
            Utilisateur (optionnel)
            <input
              aria-label="Utilisateur proxy"
              value={proxyUser}
              onChange={(e) => setProxyUser(e.target.value)}
              placeholder="nom_utilisateur"
            />
          </label>
        </>
      ) : command === "Langue de l’interface" ? (
        <>
          <p className="hint">
            Choisissez la langue de l'interface utilisateur.
          </p>
          {(
            [
              { code: "fr", label: "Francais" },
              { code: "en", label: "English" },
              { code: "ru", label: "Russkij" },
              { code: "de", label: "Deutsch" },
            ] as const
          ).map((lang) => (
            <label key={lang.code}>
              <input
                type="radio"
                name="language"
                checked={language === lang.code}
                onChange={() => setLanguage(lang.code)}
              />
              {lang.label}
            </label>
          ))}
        </>
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
              : command === "Cache des cartes"
                ? "Fermer"
                : "Appliquer"}
        </button>
      </footer>
    </Modal>
  );
}
