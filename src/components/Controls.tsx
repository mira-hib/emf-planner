import { Play } from "lucide-react";
import { usePlanner } from "../lib/usePlanner";
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
      {children}
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
  return (
    <label className="number-field">
      <span>{label}</span>
      <input
        aria-label={label}
        type="number"
        step={step}
        value={value}
        min={min}
        max={max}
        onChange={(e) => {
          const n = Number(e.target.value);
          if (
            Number.isFinite(n) &&
            (min === undefined || n >= min) &&
            (max === undefined || n <= max)
          )
            onChange(n);
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
                ? commit((p) => ({
                    ...p,
                    limits: p.limits.map((v, j) =>
                      i === j ? { ...v, active: e.target.checked } : v,
                    ),
                  }))
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
