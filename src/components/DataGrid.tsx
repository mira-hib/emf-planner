import { useEffect, useRef, useState } from "react";
import {
  formatCoordinate,
  parseCoordinate,
  type CoordinateFormat,
} from "../lib/coordinates";
export interface Column<T> {
  key: keyof T;
  label: string;
  width?: number;
  kind?: "number" | "text" | "boolean" | "select";
  options?: string[];
  min?: number;
  max?: number;
  minExclusive?: number;
  maxExclusive?: number;
  readOnly?: boolean;
  format?: (row: T) => string;
  editValue?: (row: T) => unknown;
  parse?: (value: unknown) => unknown;
}
function Cell({
  value,
  column,
  onCommit,
  coordinateFormat,
}: {
  value: unknown;
  column: Column<{ [key: string]: unknown }>;
  onCommit: (value: unknown) => void;
  coordinateFormat?: CoordinateFormat;
}) {
  const formatted =
    typeof value === "number"
      ? coordinateFormat && (column.key === "lat" || column.key === "lng")
        ? formatCoordinate(value, coordinateFormat, column.key === "lat")
        : String(
            Number(
              value.toFixed(
                column.key === "lat" || column.key === "lng" ? 7 : 4,
              ),
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
      let v: number;
      try {
        v =
          coordinateFormat && (column.key === "lat" || column.key === "lng")
            ? parseCoordinate(text, column.key === "lat")
            : Number(text.trim().replace(",", "."));
      } catch {
        setInvalid(true);
        return;
      }
      if (
        !text.trim() ||
        !Number.isFinite(v) ||
        (column.min !== undefined && v < column.min) ||
        (column.max !== undefined && v > column.max) ||
        (column.minExclusive !== undefined && v <= column.minExclusive) ||
        (column.maxExclusive !== undefined && v >= column.maxExclusive)
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
  coordinateFormat,
  selectedIds,
  onSelectionChange,
  onUpdateMany,
  onDeleteSelection,
  onCopySelection,
  onPasteSelection,
  selectCells = false,
  onReorderColumns,
}: {
  rows: T[];
  columns: Column<T>[];
  selected?: string;
  onSelect?: (id: string) => void;
  onUpdate?: (id: string, key: keyof T, value: unknown) => void;
  onAdd?: () => void;
  label?: string;
  coordinateFormat?: CoordinateFormat;
  selectedIds?: string[];
  onSelectionChange?: (ids: string[], activeId: string) => void;
  onUpdateMany?: (ids: string[], key: keyof T, value: unknown) => void;
  onDeleteSelection?: () => void;
  onCopySelection?: () => void;
  onPasteSelection?: () => void;
  selectCells?: boolean;
  onReorderColumns?: (keys: (keyof T)[]) => void;
}) {
  const anchor = useRef<string | null>(null);
  const cellAnchor = useRef<{ id: string; key: keyof T } | null>(null);
  const [cells, setCells] = useState<{ id: string; key: keyof T }[]>([]);
  const draggedColumn = useRef<keyof T | null>(null);
  function selectCell(id: string, key: keyof T, event: React.MouseEvent) {
    if (!selectCells) return;
    event.stopPropagation();
    if (
      !event.ctrlKey &&
      !event.metaKey &&
      !event.shiftKey &&
      selectedIds?.includes(id) &&
      selectedIds.length > 1
    )
      return;
    let next = [{ id, key }];
    if (event.shiftKey && cellAnchor.current) {
      const a = rows.findIndex((r) => r.id === cellAnchor.current!.id),
        b = rows.findIndex((r) => r.id === id);
      const c = columns.findIndex((v) => v.key === cellAnchor.current!.key),
        d = columns.findIndex((v) => v.key === key);
      next = rows
        .slice(Math.min(a, b), Math.max(a, b) + 1)
        .flatMap((r) =>
          columns
            .slice(Math.min(c, d), Math.max(c, d) + 1)
            .map((v) => ({ id: r.id, key: v.key })),
        );
    } else if (event.ctrlKey || event.metaKey) {
      next = cells.some((c) => c.id === id && c.key === key)
        ? cells.filter((c) => c.id !== id || c.key !== key)
        : [...cells, { id, key }];
      cellAnchor.current = { id, key };
    } else if (cells.some((c) => c.id === id && c.key === key)) next = cells;
    else cellAnchor.current = { id, key };
    setCells(next);
    onSelectionChange?.([], id);
  }
  function selectRow(id: string, event: React.MouseEvent) {
    if (selectCells) setCells([]);
    if (!onSelectionChange) {
      onSelect?.(id);
      return;
    }
    const current = selectedIds ?? [];
    let next: string[];
    if (event.shiftKey && anchor.current) {
      const a = rows.findIndex((row) => row.id === anchor.current),
        b = rows.findIndex((row) => row.id === id);
      next = rows
        .slice(Math.min(a < 0 ? b : a, b), Math.max(a, b) + 1)
        .map((row) => row.id);
      if (event.ctrlKey || event.metaKey)
        next = [...new Set([...current, ...next])];
    } else if (event.ctrlKey || event.metaKey) {
      next = current.includes(id)
        ? current.filter((value) => value !== id)
        : [...current, id];
      anchor.current = id;
    } else {
      next = current.includes(id) ? current : [id];
      anchor.current = id;
    }
    onSelectionChange(next, id);
  }
  return (
    <div
      className="data-grid"
      role="region"
      aria-label={label}
      tabIndex={0}
      onKeyDown={(event) => {
        if (
          !onSelectionChange ||
          event.target instanceof HTMLInputElement ||
          event.target instanceof HTMLSelectElement
        )
          return;
        if (
          (event.ctrlKey || event.metaKey) &&
          event.key.toLowerCase() === "a"
        ) {
          event.preventDefault();
          onSelectionChange(
            rows.map((row) => row.id),
            selected ?? rows[0]?.id ?? "",
          );
        }
        if (
          event.key === "Delete" &&
          selectedIds?.length &&
          onDeleteSelection
        ) {
          event.preventDefault();
          onDeleteSelection();
        }
        if (
          (event.ctrlKey || event.metaKey) &&
          event.key.toLowerCase() === "c" &&
          onCopySelection
        ) {
          event.preventDefault();
          onCopySelection();
        }
        if (
          (event.ctrlKey || event.metaKey) &&
          event.key.toLowerCase() === "v" &&
          onPasteSelection
        ) {
          event.preventDefault();
          onPasteSelection();
        }
      }}
    >
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
              <th
                key={String(c.key)}
                draggable={!!onReorderColumns}
                onDragStart={() => {
                  draggedColumn.current = c.key;
                }}
                onDragOver={(event) => {
                  if (onReorderColumns) event.preventDefault();
                }}
                onDrop={(event) => {
                  if (!onReorderColumns || draggedColumn.current === null)
                    return;
                  event.preventDefault();
                  const keys = columns.map((v) => v.key),
                    from = keys.indexOf(draggedColumn.current),
                    to = keys.indexOf(c.key);
                  keys.splice(to, 0, ...keys.splice(from, 1));
                  draggedColumn.current = null;
                  onReorderColumns(keys);
                }}
                onClick={() => {
                  if (!selectCells) return;
                  setCells(rows.map((r) => ({ id: r.id, key: c.key })));
                  if (rows.length)
                    onSelectionChange?.([], selected ?? rows[0].id);
                }}
              >
                {c.label}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr
              key={r.id}
              className={
                (selectedIds ? selectedIds.includes(r.id) : selected === r.id)
                  ? "selected"
                  : ""
              }
              aria-selected={
                selectedIds ? selectedIds.includes(r.id) : selected === r.id
              }
              onClick={(event) => selectRow(r.id, event)}
              style={{
                background: (r as { color?: string }).color?.match(
                  /^#[0-9a-f]{6}$/i,
                )
                  ? `${(r as { color?: string }).color}${label === "Sources rayonnantes" || label === "Positions des antennes" ? "" : "35"}`
                  : undefined,
              }}
            >
              <th
                scope="row"
                tabIndex={onSelectionChange ? 0 : undefined}
                aria-label={
                  onSelectionChange
                    ? `Sélectionner la source ${i + 1}`
                    : undefined
                }
                onClick={(event) => {
                  if (!onSelectionChange) return;
                  event.stopPropagation();
                  if (selectCells) setCells([]);
                  if (!event.ctrlKey && !event.metaKey && !event.shiftKey) {
                    anchor.current = r.id;
                    onSelectionChange([r.id], r.id);
                  } else selectRow(r.id, event);
                }}
              >
                {String(i + 1).padStart(3, "0")}
              </th>
              {columns.map((c) => (
                <td
                  key={String(c.key)}
                  aria-selected={
                    selectCells
                      ? cells.some(
                          (cell) => cell.id === r.id && cell.key === c.key,
                        )
                      : undefined
                  }
                  onClick={(event) => selectCell(r.id, c.key, event)}
                >
                  {c.readOnly || !onUpdate ? (
                    <span>
                      {c.format ? c.format(r) : String(r[c.key] ?? "")}
                    </span>
                  ) : (
                    <Cell
                      coordinateFormat={coordinateFormat}
                      value={c.editValue ? c.editValue(r) : r[c.key]}
                      column={
                        c as unknown as Column<{ [key: string]: unknown }>
                      }
                      onCommit={(value) => {
                        const parsed = c.parse ? c.parse(value) : value;
                        if (parsed !== r[c.key]) {
                          if (
                            selectCells &&
                            onUpdateMany &&
                            cells.some(
                              (cell) => cell.id === r.id && cell.key === c.key,
                            )
                          )
                            onUpdateMany(
                              cells
                                .filter((cell) => cell.key === c.key)
                                .map((cell) => cell.id),
                              c.key,
                              parsed,
                            );
                          else if (onUpdateMany && selectedIds?.includes(r.id))
                            onUpdateMany(selectedIds, c.key, parsed);
                          else onUpdate(r.id, c.key, parsed);
                        }
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
