import {
  useMemo,
  useRef,
  useState,
  type ReactNode,
  type PointerEvent,
} from "react";
import { Marker, Tooltip } from "react-leaflet";
import { divIcon } from "leaflet";
import type { RadiatingSource } from "../types";

export function LeafletAntennaHandle({
  source,
  selected,
  onSelect,
  onMove,
}: {
  source: RadiatingSource;
  selected: boolean;
  onSelect: () => void;
  onMove: (lat: number, lng: number) => void;
}) {
  const position = useMemo<[number, number]>(
    () => [source.lat, source.lng],
    [source.lat, source.lng],
  );
  const icon = useMemo(
    () =>
      divIcon({
        className: "antenna-leaflet-handle",
        iconSize: [12, 12],
        iconAnchor: [6, 6],
        html: `<span style="display:block;width:10px;height:10px;border:2px solid ${selected ? "#0078d7" : "#333"};background:${source.active === false ? "#aaa" : "#7ecf78"};border-radius:50%"></span>`,
      }),
    [selected, source.active],
  );
  return (
    <Marker
      position={position}
      draggable
      bubblingMouseEvents={false}
      zIndexOffset={selected ? 1000 : 0}
      icon={icon}
      eventHandlers={{
        click: onSelect,
        dragstart: onSelect,
        dragend: (e) => {
          const pos = e.target.getLatLng();
          onMove(pos.lat, pos.lng);
        },
      }}
    >
      <Tooltip>
        {source.name} · {source.frequency_mhz} MHz
      </Tooltip>
    </Marker>
  );
}

export function SvgAntennaHandle({
  point,
  label,
  onSelect,
  onMove,
  children,
}: {
  point: [number, number];
  label: string;
  onSelect: () => void;
  onMove: (x: number, y: number) => void;
  children: ReactNode;
}) {
  const active = useRef(false),
    start = useRef(point);
  const [preview, setPreview] = useState<[number, number] | null>(null);
  const coordinate = (e: PointerEvent<SVGGElement>): [number, number] => {
    const matrix = e.currentTarget.ownerSVGElement?.getScreenCTM();
    if (!matrix) return point;
    const p = new DOMPoint(e.clientX, e.clientY).matrixTransform(
      matrix.inverse(),
    );
    return [p.x, -p.y];
  };
  return (
    <g
      role="button"
      aria-label={label}
      style={{ cursor: "move" }}
      transform={
        preview
          ? `translate(${preview[0] - point[0]} ${point[1] - preview[1]})`
          : undefined
      }
      onClick={(e) => {
        e.stopPropagation();
        onSelect();
      }}
      onPointerDown={(e) => {
        if (e.button !== 0) return;
        e.stopPropagation();
        active.current = true;
        start.current = coordinate(e);
        onSelect();
        e.currentTarget.setPointerCapture(e.pointerId);
      }}
      onPointerMove={(e) => {
        if (!active.current) return;
        e.stopPropagation();
        const p = coordinate(e);
        setPreview([
          point[0] + p[0] - start.current[0],
          point[1] + p[1] - start.current[1],
        ]);
      }}
      onPointerUp={(e) => {
        if (!active.current) return;
        e.stopPropagation();
        active.current = false;
        setPreview(null);
        const p = coordinate(e),
          dx = p[0] - start.current[0],
          dy = p[1] - start.current[1];
        if (Math.hypot(dx, dy) > 0.001) onMove(point[0] + dx, point[1] + dy);
      }}
      onPointerCancel={() => {
        active.current = false;
        setPreview(null);
      }}
    >
      {children}
    </g>
  );
}
