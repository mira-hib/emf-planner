import { useEffect, useRef, useState } from "react";
export interface Column<T> {
  key: keyof T;
  label: string;
  width?: number;
  kind?: "number" | "text" | "boolean" | "select";
  options?: string[];
  min?: number;
  max?: number;
  readOnly?: boolean;
  format?: (row: T) => string;
}
function Cell({
  value,
  column,
  onCommit,
}: {
  value: unknown;
  column: Column<{ [key: string]: unknown }>;
  onCommit: (value: unknown) => void;
}) {
  const formatted =
    typeof value === "number"
      ? String(
          Number(
            value.toFixed(column.key === "lat" || column.key === "lng" ? 7 : 4),
          ),
        )
      : String(value ?? "");
  const [text, setText] = useState(formatted),
    [invalid, setInvalid] = useState(false);
  const cancelled = useRef(false);
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
    if (column.kind === "number") {
      const v = Number(text.trim().replace(",", "."));
      if (
        !text.trim() ||
        !Number.isFinite(v) ||
        (column.min !== undefined && v < column.min) ||
        (column.max !== undefined && v > column.max)
      ) {
        setInvalid(true);
        return;
      }
      onCommit(v);
    } else onCommit(text);
  }
  if (column.kind === "boolean")
    return (
      <input
        aria-label={column.label}
        type="checkbox"
        checked={value === true}
        onChange={(e) => onCommit(e.target.checked)}
      />
    );
  if (column.kind === "select")
    return (
      <select
        aria-label={column.label}
        value={String(value ?? "")}
        onChange={(e) => onCommit(e.target.value)}
      >
        {column.options?.map((o) => (
          <option key={o}>{o}</option>
        ))}
      </select>
    );
  return (
    <input
      aria-label={column.label}
      aria-invalid={invalid}
      title={
        invalid ? "Valeur invalide. Vérifiez les bornes du champ." : undefined
      }
      inputMode={column.kind === "number" ? "decimal" : undefined}
      value={text}
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
          setInvalid(false);
          e.currentTarget.blur();
        }
      }}
    />
  );
}
export default function DataGrid<T extends { id: string }>({
  rows,
  columns,
  selected,
  onSelect,
  onUpdate,
  onAdd,
  label = "Tableau",
}: {
  rows: T[];
  columns: Column<T>[];
  selected?: string;
  onSelect?: (id: string) => void;
  onUpdate?: (id: string, key: keyof T, value: unknown) => void;
  onAdd?: () => void;
  label?: string;
}) {
  return (
    <div className="data-grid" role="region" aria-label={label} tabIndex={0}>
      <table>
        <colgroup>
          <col style={{ width: 30 }} />
          {columns.map((c) => (
            <col key={String(c.key)} style={{ width: c.width ?? 90 }} />
          ))}
        </colgroup>
        <thead>
          <tr>
            <th aria-label="Numéro" />
            {columns.map((c) => (
              <th key={String(c.key)}>{c.label}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr
              key={r.id}
              className={selected === r.id ? "selected" : ""}
              onClick={() => onSelect?.(r.id)}
              style={{
                background: (r as { color?: string }).color?.match(
                  /^#[0-9a-f]{6}$/i,
                )
                  ? `${(r as { color?: string }).color}35`
                  : undefined,
              }}
            >
              <th scope="row">{String(i + 1).padStart(3, "0")}</th>
              {columns.map((c) => (
                <td key={String(c.key)}>
                  {c.readOnly || !onUpdate ? (
                    <span>
                      {c.format ? c.format(r) : String(r[c.key] ?? "")}
                    </span>
                  ) : (
                    <Cell
                      value={r[c.key]}
                      column={
                        c as unknown as Column<{ [key: string]: unknown }>
                      }
                      onCommit={(value) => {
                        if (value !== r[c.key]) onUpdate(r.id, c.key, value);
                      }}
                    />
                  )}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
        {onAdd && (
          <tfoot>
            <tr onClick={onAdd}>
              <th>＊</th>
              <td colSpan={columns.length}>
                <button
                  className="new-row"
                  onClick={(e) => {
                    e.stopPropagation();
                    onAdd();
                  }}
                >
                  Ajouter une ligne
                </button>
              </td>
            </tr>
          </tfoot>
        )}
      </table>
      {!rows.length && !onAdd && (
        <div className="grid-empty">Aucune donnée</div>
      )}
    </div>
  );
}
