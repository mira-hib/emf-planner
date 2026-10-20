import { useEffect, useMemo, useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import type { StoredAntenna } from "../types";
import { uid } from "../lib/project";
import { patternAmplitude } from "../lib/engine";
import {
  beamWidth,
  envelopePatterns,
  feederLoss,
  symmetricPattern,
} from "../lib/editing";
import { chooseFile, saveText } from "../lib/files";
import { parseMsi, writeMsi } from "../lib/antenna";
import feeders from "../data/feeders.json";
import DataGrid from "./DataGrid";
import AntennaPolar from "./AntennaPolar";
import Modal from "./Modal";

export const ANTENNA_COMMANDS = [
  "Bibliothèque d’antennes",
  "Éditeur de diagramme",
  "Enveloppe de diagrammes",
  "Antenne symétrique ITU-R F.699",
  "Bibliothèque de câbles",
  "Calcul inverse de puissance",
];
export default function AntennaTools() {
  const a = usePlanner(),
    { project, source, command, commit } = a;
  const [catalog, setCatalog] = useState<StoredAntenna[]>([]),
    [error, setError] = useState("");
  const initial = useMemo<StoredAntenna>(
    () => ({
      id: uid(),
      name: source?.antenna_model || "Nouveau diagramme",
      gain: source?.gain_dbi ?? 16,
      horizontal: Array.from(
        { length: 360 },
        (_, i) =>
          -20 *
          Math.log10(
            patternAmplitude(
              source?.pattern_h,
              i,
              source?.beam_width_h_deg ?? 65,
            ),
          ),
      ),
      vertical: Array.from(
        { length: 360 },
        (_, i) =>
          -20 *
          Math.log10(
            patternAmplitude(
              source?.pattern_v,
              i + (source?.electrical_downtilt_deg ?? 0),
              source?.beam_width_v_deg ?? 10,
            ),
          ),
      ),
    }),
    [source],
  );
  const [pattern, setPattern] = useState(initial),
    [selected, setSelected] = useState(""),
    [plane, setPlane] = useState<"horizontal" | "vertical">("horizontal");
  const [checked, setChecked] = useState<string[]>([]),
    [frequency, setFrequency] = useState(source?.frequency_mhz ?? 7000),
    [gain, setGain] = useState(source?.gain_dbi ?? 31),
    [diameter, setDiameter] = useState(source?.antenna_size_m ?? 1.2);
  const [cable, setCable] = useState(() => {
      const i = feeders.findIndex((f) => f.name === source?.feeder?.cable);
      return i < 0 ? 7 : i;
    }),
    [length, setLength] = useState(source?.feeder?.length_m ?? 0),
    [jumper, setJumper] = useState(() => {
      const i = feeders.findIndex((f) => f.name === source?.feeder?.jumper);
      return i < 0 ? 4 : i;
    }),
    [jumperLengths, setJumperLengths] = useState(
      source?.feeder?.jumper_lengths ?? "3+3",
    ),
    [automatic, setAutomatic] = useState(
      source?.feeder?.automatic_connectors ?? true,
    ),
    [connectors, setConnectors] = useState(source?.feeder?.connectors ?? 2),
    [connectorLoss, setConnectorLoss] = useState(
      source?.feeder?.connector_loss ?? 0.05,
    ),
    [otherLoss, setOtherLoss] = useState(source?.feeder?.other_loss ?? 0);
  const [target, setTarget] = useState(
      source
        ? source.power_w *
            source.num_tx *
            10 ** (-source.total_attenuation_db / 10)
        : 1,
    ),
    [unit, setUnit] = useState("W"),
    [targetKind, setTargetKind] = useState("antenna");
  useEffect(() => {
    fetch("/antennas/catalog.json")
      .then((r) => {
        if (!r.ok) throw new Error("Catalogue indisponible");
        return r.json();
      })
      .then(setCatalog)
      .catch((e) => setError(String(e)));
  }, []);
  const projectPatterns = project.sources
    .filter((s) => s.pattern_h && s.pattern_v)
    .map((s) => ({
      id: s.id,
      name: `${s.antenna_model} — ${s.frequency_mhz} MHz`,
      gain: s.gain_dbi,
      frequency: s.frequency_mhz,
      horizontal: s.pattern_h!,
      vertical: Array.from(
        { length: 360 },
        (_, i) =>
          -20 *
          Math.log10(
            patternAmplitude(
              s.pattern_v,
              i + s.electrical_downtilt_deg,
              s.beam_width_v_deg,
            ),
          ),
      ),
    }));
  const library = [
    ...(project.antenna_library ?? []),
    ...projectPatterns,
    ...catalog,
  ].filter(
    (value, index, all) => all.findIndex((v) => v.id === value.id) === index,
  );
  const chosen = library.find((p) => p.id === selected) ?? library[0];
  const close = () => a.setCommand("");
  function run(action: () => void) {
    try {
      action();
      setError("");
    } catch (e) {
      setError(e instanceof Error ? e.message : String(e));
    }
  }
  function apply(pat: StoredAntenna) {
    if (!source) return;
    commit((p) => ({
      ...p,
      antenna_library: [
        ...(p.antenna_library ?? []).filter((v) => v.id !== pat.id),
        pat,
      ],
      sources: p.sources.map((s) =>
        s.id === source.id
          ? {
              ...s,
              antenna_model: pat.name,
              gain_dbi: pat.gain,
              pattern_h: pat.horizontal,
              pattern_v: Array.from(
                { length: 360 },
                (_, i) =>
                  -20 *
                  Math.log10(
                    patternAmplitude(
                      pat.vertical,
                      i - s.electrical_downtilt_deg,
                      s.beam_width_v_deg,
                    ),
                  ),
              ),
              beam_width_h_deg: beamWidth(pat.horizontal),
              beam_width_v_deg: beamWidth(pat.vertical),
            }
          : s,
      ),
    }));
    close();
  }
  const preview = {
    ...source!,
    name: pattern.name,
    antenna_model: pattern.name,
    azimuth_deg: 0,
    pattern_h: pattern.horizontal,
    pattern_v: pattern.vertical,
  };
  let loss = 0,
    lossDetails: ReturnType<typeof feederLoss> | undefined,
    lossError = "";
  try {
    lossDetails = feederLoss(
      feeders[cable].points,
      source?.frequency_mhz ?? frequency,
      length,
      feeders[jumper].points,
      jumperLengths,
      connectors,
      connectorLoss,
      otherLoss,
      automatic,
    );
    loss = lossDetails.total;
  } catch (e) {
    lossError = String((e as Error).message);
  }
  const targetW =
    unit === "dBm"
      ? 10 ** ((target - 30) / 10)
      : unit === "dBW"
        ? 10 ** (target / 10)
        : unit === "mW"
          ? target / 1000
          : target;
  const powerW = source
    ? (targetW *
        10 **
          ((source.total_attenuation_db -
            (targetKind === "eirp" ? source.gain_dbi : 0)) /
            10)) /
      source.num_tx
    : 0;
  const number = (
    label: string,
    value: number,
    set: (n: number) => void,
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
  return (
    <Modal
      title={command}
      onClose={close}
      wide={
        command === "Éditeur de diagramme" ||
        command === "Bibliothèque d’antennes"
      }
    >
      {command === "Bibliothèque de câbles" ? (
        <>
          <fieldset>
            <legend>Câble coaxial — 36 références de l’original</legend>
            <label>
              Modèle
              <select
                aria-label="Modèle de câble"
                value={cable}
                onChange={(e) => setCable(Number(e.target.value))}
              >
                {feeders.map((f, i) => (
                  <option key={f.name} value={i}>
                    {f.name}
                  </option>
                ))}
              </select>
            </label>
            <label>
              Fréquence de la source, MHz
              <input
                aria-label="Fréquence de la source, MHz"
                value={source?.frequency_mhz ?? frequency}
                readOnly
              />
            </label>
            {number("Longueur du câble, m", length, setLength, 0)}
            <label>
              Modèle des bretelles
              <select
                aria-label="Modèle des bretelles"
                value={jumper}
                onChange={(e) => setJumper(Number(e.target.value))}
              >
                {feeders.map((f, i) => (
                  <option key={f.name} value={i}>
                    {f.name}
                  </option>
                ))}
              </select>
            </label>
            <label>
              Longueurs des bretelles, m
              <input
                aria-label="Longueurs des bretelles, m"
                value={jumperLengths}
                onChange={(e) => setJumperLengths(e.target.value)}
                placeholder="3+3"
              />
            </label>
            <label className="check">
              <input
                type="checkbox"
                checked={automatic}
                onChange={(e) => setAutomatic(e.target.checked)}
              />
              Nombre automatique de connecteurs
            </label>
            {automatic ? (
              <p>Connecteurs : {lossDetails?.connectors ?? "—"}</p>
            ) : (
              number("Nombre de connecteurs", connectors, setConnectors, 0)
            )}
            {number(
              "Perte par connecteur, dB",
              connectorLoss,
              setConnectorLoss,
              0,
            )}
            {number("Autres pertes, dB", otherLoss, setOtherLoss, 0)}
            <p className="hint">
              Plage documentée : {feeders[cable].points[0].frequency}–
              {
                feeders[cable].points[feeders[cable].points.length - 1]
                  .frequency
              }{" "}
              MHz.
            </p>
          </fieldset>
          {lossError ? (
            <p role="alert">{lossError}</p>
          ) : (
            <div className="dialog-result">
              <span>
                Câble : {lossDetails?.cableLoss.toFixed(3)} dB · bretelles :{" "}
                {lossDetails?.jumperLoss.toFixed(3)} dB
              </span>
              <span>
                Atténuation totale : <output>{loss.toFixed(3)} dB</output>
              </span>
            </div>
          )}
          <footer>
            <button onClick={close}>Annuler</button>
            <button
              disabled={
                !!lossError ||
                !source ||
                [length, connectors, connectorLoss, otherLoss].some(
                  (v) => !Number.isFinite(v) || v < 0,
                )
              }
              onClick={() => {
                commit((p) => ({
                  ...p,
                  sources: p.sources.map((s) =>
                    s.id === source!.id
                      ? {
                          ...s,
                          total_attenuation_db: loss,
                          feeder: {
                            cable: feeders[cable].name,
                            length_m: length,
                            jumper: feeders[jumper].name,
                            jumper_lengths: jumperLengths,
                            automatic_connectors: automatic,
                            connectors: lossDetails!.connectors,
                            connector_loss: connectorLoss,
                            other_loss: otherLoss,
                          },
                        }
                      : s,
                  ),
                }));
                close();
              }}
            >
              Appliquer à la source
            </button>
          </footer>
        </>
      ) : command === "Calcul inverse de puissance" ? (
        <>
          <label>
            Puissance cible
            <select
              value={targetKind}
              onChange={(e) => setTargetKind(e.target.value)}
            >
              <option value="antenna">Au port d’antenne</option>
              <option value="eirp">EIRP</option>
            </select>
          </label>
          {number("Valeur cible", target, setTarget)}
          <label>
            Unité
            <select value={unit} onChange={(e) => setUnit(e.target.value)}>
              {["W", "mW", "dBW", "dBm"].map((u) => (
                <option key={u}>{u}</option>
              ))}
            </select>
          </label>
          <p>
            Nombre de TX : {source?.num_tx} · pertes :{" "}
            {source?.total_attenuation_db} dB · gain : {source?.gain_dbi} dBi.
          </p>
          <div className="dialog-result">
            Puissance de chaque TX : <output>{powerW.toFixed(6)} W</output>
          </div>
          <footer>
            <button onClick={close}>Annuler</button>
            <button
              disabled={!Number.isFinite(powerW) || powerW <= 0 || !source}
              onClick={() => {
                a.updateSource(source!.id, "power_w", powerW);
                close();
              }}
            >
              Appliquer à la source
            </button>
          </footer>
        </>
      ) : command === "Antenne symétrique ITU-R F.699" ? (
        <>
          <p className="hint">
            Génération selon la fonction Gq de l’original : diagrammes H et V
            symétriques, 360 échantillons.
          </p>
          {number(
            "Fréquence du diagramme, MHz",
            frequency,
            setFrequency,
            1,
            70000,
          )}
          {number("Gain du diagramme, dBi", gain, setGain, 0, 60)}
          {number("Diamètre, m", diameter, setDiameter, 0.1, 20)}
          <footer>
            <button onClick={close}>Annuler</button>
            <button
              onClick={() =>
                run(() => apply(symmetricPattern(frequency, gain, diameter)))
              }
            >
              Créer et appliquer
            </button>
          </footer>
        </>
      ) : command === "Bibliothèque d’antennes" ? (
        <>
          <div className="library-layout">
            <div>
              <select
                className="antenna-list"
                aria-label="Diagrammes disponibles"
                size={14}
                value={chosen?.id ?? ""}
                onChange={(e) => setSelected(e.target.value)}
              >
                {library.map((p, i) => (
                  <option key={`${p.id}-${i}`} value={p.id}>
                    {p.name} {p.frequency ? `· ${p.frequency} MHz` : ""} ·{" "}
                    {p.gain} dBi
                  </option>
                ))}
              </select>
              <p className="hint">
                Catalogue livré, diagrammes du projet et modèles enregistrés.
              </p>
            </div>
            {chosen && (
              <div>
                <AntennaPolar
                  source={{
                    ...source!,
                    name: chosen.name,
                    azimuth_deg: 0,
                    pattern_h: chosen.horizontal,
                    pattern_v: chosen.vertical,
                  }}
                  both
                />
                <p>
                  {chosen.gain} dBi · H {beamWidth(chosen.horizontal)}° · V{" "}
                  {beamWidth(chosen.vertical)}°
                </p>
              </div>
            )}
          </div>
          <footer>
            <button
              onClick={() =>
                void a.task(async () => {
                  const f = await chooseFile(["msi", "txt"]);
                  if (!f) return null;
                  const p = parseMsi(new TextDecoder().decode(f.bytes));
                  const added = { ...p, id: uid() };
                  commit((v) => ({
                    ...v,
                    antenna_library: [...(v.antenna_library ?? []), added],
                  }));
                  setSelected(added.id);
                  return f;
                }, "Diagramme ajouté à la bibliothèque")
              }
            >
              Importer MSI…
            </button>
            <button onClick={close}>Fermer</button>
            <button
              disabled={!chosen || !source}
              onClick={() => chosen && run(() => apply(chosen))}
            >
              Appliquer à la source
            </button>
          </footer>
        </>
      ) : command === "Enveloppe de diagrammes" ? (
        <>
          <label>
            Nom
            <input
              value={pattern.name}
              onChange={(e) => setPattern({ ...pattern, name: e.target.value })}
            />
          </label>
          <div className="pattern-checks">
            {library.map((p, i) => (
              <label key={`${p.id}-${i}`}>
                <input
                  type="checkbox"
                  checked={checked.includes(p.id)}
                  onChange={(e) =>
                    setChecked((c) =>
                      e.target.checked
                        ? [...c, p.id]
                        : c.filter((v) => v !== p.id),
                    )
                  }
                />
                {p.name} · {p.gain} dBi
              </label>
            ))}
          </div>
          <p className="hint">
            L’enveloppe conserve le gain absolu maximal de chaque direction.
          </p>
          <footer>
            <button onClick={close}>Annuler</button>
            <button
              disabled={!checked.length}
              onClick={() =>
                run(() =>
                  apply(
                    envelopePatterns(
                      library.filter((p) => checked.includes(p.id)),
                      pattern.name || "Enveloppe",
                    ),
                  ),
                )
              }
            >
              Créer et appliquer
            </button>
          </footer>
        </>
      ) : (
        <>
          <div className="diagram-editor">
            <div>
              <label>
                Nom du diagramme
                <input
                  value={pattern.name}
                  onChange={(e) =>
                    setPattern({ ...pattern, name: e.target.value })
                  }
                />
              </label>
              {number("Gain, dBi", pattern.gain, (v) =>
                setPattern({ ...pattern, gain: v }),
              )}
              <div className="button-row">
                <button
                  className={plane === "horizontal" ? "pressed" : ""}
                  onClick={() => setPlane("horizontal")}
                >
                  Plan H
                </button>
                <button
                  className={plane === "vertical" ? "pressed" : ""}
                  onClick={() => setPlane("vertical")}
                >
                  Plan V
                </button>
              </div>
              <AntennaPolar source={preview} vertical={plane === "vertical"} />
              <button
                onClick={() =>
                  setPattern((p) => ({ ...p, [plane]: Array(360).fill(0) }))
                }
              >
                Diagramme omnidirectionnel
              </button>
              <button
                onClick={() =>
                  setPattern((p) => ({
                    ...p,
                    [plane]: [
                      ...p[plane === "horizontal" ? "vertical" : "horizontal"],
                    ],
                  }))
                }
              >
                Copier l’autre plan
              </button>
            </div>
            <DataGrid
              label="Échantillons du diagramme"
              rows={pattern[plane].map((loss, i) => ({
                id: String(i),
                angle: i,
                loss,
              }))}
              columns={[
                { key: "angle", label: "Angle, °", readOnly: true, width: 80 },
                {
                  key: "loss",
                  label: "Atténuation, dB",
                  kind: "number",
                  min: 0,
                  max: 200,
                  width: 140,
                },
              ]}
              onUpdate={(id, _k, v) =>
                setPattern((p) => ({
                  ...p,
                  [plane]: p[plane].map((n, i) =>
                    i === Number(id) ? Number(v) : n,
                  ),
                }))
              }
            />
          </div>
          <footer>
            <button
              onClick={() =>
                void a.task(
                  () =>
                    saveText(
                      `${pattern.name}.msi`,
                      writeMsi(
                        pattern.name,
                        pattern.gain,
                        pattern.horizontal,
                        pattern.vertical,
                      ),
                    ),
                  "Diagramme exporté",
                )
              }
            >
              Exporter MSI
            </button>
            <button onClick={close}>Annuler</button>
            <button onClick={() => run(() => apply(pattern))}>
              Appliquer le diagramme
            </button>
          </footer>
        </>
      )}
      {error && (
        <p className="dialog-error" role="alert">
          {error}
        </p>
      )}
    </Modal>
  );
}
