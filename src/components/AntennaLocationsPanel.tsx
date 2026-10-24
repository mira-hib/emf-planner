import { useState } from "react";
import { usePlanner } from "../lib/usePlanner";
import {
  antennaCaptureDistance,
  editAntennaCoordinate,
  formatAntennaCoordinate,
} from "../lib/antenna-locations";
import { wrap } from "../lib/geometry";
import DataGrid, { type Column } from "./DataGrid";
import type { RadiatingSource } from "../types";
import "./AntennaLocationsPanel.css";

export default function AntennaLocationsPanel() {
  const a = usePlanner(),
    p = a.project;
  const [order, setOrder] = useState<(keyof RadiatingSource)[]>([]);
  const selected = a.selectedSourceIds.filter((id) =>
    p.sources.some((s) => s.id === id),
  );
  const update = (
    ids: string[],
    key: keyof RadiatingSource,
    value: unknown,
  ) => {
    if (key !== "x_m" && key !== "y_m") return;
    try {
      a.commit((p) => editAntennaCoordinate(p, ids, key, Number(value)));
    } catch (e) {
      a.fail(e);
    }
  };
  const columns: Column<RadiatingSource>[] = [
    {
      key: "name",
      label: "Source name",
      readOnly: true,
      width: 130,
      format: (s) => `${s.name} (${s.antenna_model})`,
    },
    {
      key: "lat",
      label: "Latitude, deg",
      readOnly: true,
      width: 65,
      format: (s) =>
        formatAntennaCoordinate(
          s.lat,
          p.settings.coordinate_format ?? "decimal",
          true,
        ),
    },
    {
      key: "lng",
      label: "Longitude, deg.",
      readOnly: true,
      width: 65,
      format: (s) =>
        formatAntennaCoordinate(
          s.lng,
          p.settings.coordinate_format ?? "decimal",
          false,
        ),
    },
    {
      key: "x_m",
      label: "X, m",
      kind: "number",
      width: 45,
      editValue: (s) => (s.x_m ?? 0).toFixed(1),
    },
    {
      key: "y_m",
      label: "Y, m",
      kind: "number",
      width: 45,
      editValue: (s) => (s.y_m ?? 0).toFixed(1),
    },
    {
      key: "height_m",
      label: "H, m",
      readOnly: true,
      width: 45,
      format: (s) => String(Number(s.height_m.toPrecision(7))),
    },
    {
      key: "azimuth_deg",
      label: "Azimuth, deg.",
      readOnly: true,
      width: 45,
      format: (s) => String(wrap(s.azimuth_deg)),
    },
  ];
  if (order.length)
    columns.sort((a, b) => order.indexOf(a.key) - order.indexOf(b.key));
  return (
    <>
      <div className="antenna-location-grid">
        <DataGrid<RadiatingSource>
          label="Positions des antennes"
          rows={p.sources}
          columns={columns}
          selectCells
          onReorderColumns={setOrder}
          selected={a.selectedSource}
          selectedIds={selected}
          onSelectionChange={a.setSourceSelection}
          onSelect={a.setSelectedSource}
          onUpdate={(id, key, value) => update([id], key, value)}
          onUpdateMany={update}
        />
      </div>
      <label className="antenna-capture">
        <span>Maximum capture distance</span>
        <input
          aria-label="Maximum capture distance"
          type="range"
          min={1}
          max={30}
          step={1}
          value={antennaCaptureDistance(p)}
          title={`${antennaCaptureDistance(p) / 10} unités Web Mercator`}
          onChange={(e) =>
            a.settings({ antenna_capture_distance: Number(e.target.value) })
          }
        />
      </label>
    </>
  );
}
