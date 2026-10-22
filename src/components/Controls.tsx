import { Play } from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
import OriginalIcon from "./OriginalIcon";
import { useEffect, useRef, useState } from "react";
import { formatCoordinate, parseCoordinate } from "../lib/coordinates";
export function Tool({
  label,
  children,
  onClick,
  disabled = false,
}: {
  label: string;
  children: React.ReactNode;
  onClick: () => void;
  disabled?: boolean;
}) {
  return (
    <button
      className="tool-button"
      title={label}
      aria-label={label}
      onClick={onClick}
      disabled={disabled}
    >
      <OriginalIcon label={label}>{children}</OriginalIcon>
    </button>
  );
}
export function NumberField({
  label,
  value,
  onChange,
  min,
  max,
  step = "any",
}: {
  label: string;
  value: number;
  onChange: (v: number) => void;
  min?: number;
  max?: number;
  step?: string;
}) {
  const { project } = usePlanner();
  const coordinate = /Latitude|Longitude/.test(label),
    latitude = /Latitude/.test(label);
  const formatted = coordinate
    ? formatCoordinate(
        value,
        project.settings.coordinate_format ?? "decimal",
        latitude,
      )
    : String(Number(value.toFixed(4)));
  const [text, setText] = useState(formatted),
    [invalid, setInvalid] = useState(false),
    cancelled = useRef(false);
  useEffect(() => {
    setText(formatted);
    setInvalid(false);
  }, [formatted]);
  function commit() {
    if (cancelled.current) {
      cancelled.current = false;
      return;
    }
    if (text === formatted) return;
    try {
      const n = coordinate
        ? parseCoordinate(text, latitude)
        : Number(text.trim().replace(",", "."));
      if (
        !text.trim() ||
        !Number.isFinite(n) ||
        (min !== undefined && n < min) ||
        (max !== undefined && n > max)
      )
        throw new Error("Valeur invalide");
      setInvalid(false);
      onChange(n);
    } catch {
      setInvalid(true);
    }
  }
  return (
    <label className="number-field">
      <span>{label}</span>
      <input
        aria-label={label}
        type="text"
        inputMode={
          coordinate && project.settings.coordinate_format !== "decimal"
            ? "text"
            : "decimal"
        }
        step={step}
        value={text}
        aria-invalid={invalid}
        min={min}
        max={max}
        onChange={(e) => {
          setText(e.target.value);
          setInvalid(false);
        }}
        onBlur={commit}
        onKeyDown={(e) => {
          if (e.key === "Enter") e.currentTarget.blur();
          if (e.key === "Escape") {
            cancelled.current = true;
            setText(formatted);
            e.currentTarget.blur();
          }
        }}
      />
    </label>
  );
}
export function LimitSelector({ multiple = false }: { multiple?: boolean }) {
  const { project, settings, commit } = usePlanner();
  return (
    <fieldset className="limit-selector">
      <legend>Limites utilisées pour les calculs</legend>
      {project.limits.map((l, i) => (
        <label key={l.id} title={l.name} style={{ color: l.color }}>
          <input
            type={multiple ? "checkbox" : "radio"}
            name={multiple ? `limit-${i}` : "limit-set"}
            checked={
              multiple ? l.active : project.settings.selected_limit === i
            }
            onChange={(e) =>
              multiple
                ? commit(
                    (p) => ({
                      ...p,
                      limits: p.limits.map((v, j) =>
                        i === j ? { ...v, active: e.target.checked } : v,
                      ),
                    }),
                    "Limites affichées modifiées",
                    false,
                  )
                : settings({ selected_limit: i })
            }
          />{" "}
          {i + 1}
        </label>
      ))}
    </fieldset>
  );
}
export function CalculateButton({
  kind,
}: {
  kind: "points" | "horizontal" | "vertical";
}) {
  const { calculate, busy } = usePlanner();
  return (
    <button
      className="primary-action"
      disabled={busy}
      onClick={() => calculate(kind)}
    >
      <Play size={14} /> Calculer{" "}
      {kind === "points" ? "les points" : "les coupes actives"}
    </button>
  );
}
