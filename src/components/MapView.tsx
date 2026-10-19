import { MapContainer, TileLayer, Marker, Popup, Circle, useMapEvents, LayersControl } from "react-leaflet";
import { Icon } from "leaflet";
import type { RadiatingSource, ControlPoint } from "../types";

import "leaflet/dist/leaflet.css";

const antennaIcon = new Icon({
  iconUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon.png",
  iconRetinaUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon-2x.png",
  shadowUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-shadow.png",
  iconSize: [25, 41],
  iconAnchor: [12, 41],
});

const controlPointIcon = new Icon({
  iconUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon.png",
  iconRetinaUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon-2x.png",
  shadowUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-shadow.png",
  iconSize: [20, 33],
  iconAnchor: [10, 33],
  className: "control-point-marker",
});

const TILE_PROVIDERS = [
  { name: "OpenStreetMap", url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", attribution: "&copy; OSM", maxZoom: 19 },
  { name: "Carto Light", url: "https://cartodb-basemaps-{s}.global.ssl.fastly.net/light_all/{z}/{x}/{y}.png", attribution: "&copy; Carto", maxZoom: 19 },
  { name: "Carto Dark", url: "https://cartodb-basemaps-{s}.global.ssl.fastly.net/dark_all/{z}/{x}/{y}.png", attribution: "&copy; Carto", maxZoom: 19 },
  { name: "OpenTopoMap", url: "https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png", attribution: "&copy; OpenTopoMap", maxZoom: 17 },
  { name: "Esri Satellite", url: "https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}", attribution: "&copy; Esri", maxZoom: 18 },
  { name: "Esri Street", url: "https://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}", attribution: "&copy; Esri", maxZoom: 18 },
  { name: "Esri Topo", url: "https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}", attribution: "&copy; Esri", maxZoom: 18 },
];

interface Props {
  sources: RadiatingSource[];
  controlPoints: ControlPoint[];
  safetyDistances: Map<string, number>;
  center: [number, number];
  zoom: number;
  onMapClick: (lat: number, lng: number) => void;
}

function ClickHandler({ onMapClick }: { onMapClick: (lat: number, lng: number) => void }) {
  useMapEvents({
    click(e) {
      onMapClick(e.latlng.lat, e.latlng.lng);
    },
  });
  return null;
}

export default function MapView({ sources, controlPoints, safetyDistances, center, zoom, onMapClick }: Props) {
  return (
    <MapContainer center={center} zoom={zoom} style={{ height: "100%", width: "100%" }}>
      <LayersControl position="topright">
        {TILE_PROVIDERS.map((provider, i) => (
          <LayersControl.BaseLayer key={provider.name} checked={i === 0} name={provider.name}>
            <TileLayer
              attribution={provider.attribution}
              url={provider.url}
              maxZoom={provider.maxZoom}
            />
          </LayersControl.BaseLayer>
        ))}
      </LayersControl>

      <ClickHandler onMapClick={onMapClick} />

      {sources.map((source) => (
        <Marker key={source.id} position={[source.lat, source.lng]} icon={antennaIcon}>
          <Popup>
            <strong>{source.name}</strong><br />
            {source.frequency_mhz} MHz / {source.power_w} W<br />
            Gain: {source.gain_dbi} dBi | Hauteur: {source.height_m} m<br />
            {source.sector_id && <>Secteur: {source.sector_id}<br /></>}
            {source.owner && <>Opérateur: {source.owner}</>}
          </Popup>
        </Marker>
      ))}

      {sources.map((source) => {
        const dist = safetyDistances.get(source.id) || 0;
        if (dist <= 0) return null;
        return (
          <Circle
            key={`zone-${source.id}`}
            center={[source.lat, source.lng]}
            radius={dist}
            pathOptions={{ color: "#ef4444", fillColor: "#fca5a5", fillOpacity: 0.3 }}
          />
        );
      })}

      {controlPoints.map((cp) => (
        <Marker key={cp.id} position={[cp.lat, cp.lng]} icon={controlPointIcon}>
          <Popup>
            <strong>{cp.name}</strong><br />
            Hauteur: {cp.height_m} m<br />
            {cp.calculated_e_field_v_m != null && (
              <>E calculé: {cp.calculated_e_field_v_m.toFixed(3)} V/m<br /></>
            )}
            {cp.measured_e_field_v_m != null && (
              <>E mesuré: {cp.measured_e_field_v_m.toFixed(3)} V/m<br /></>
            )}
            {cp.exposure_ratio != null && (
              <>Ratio: {(cp.exposure_ratio * 100).toFixed(1)}%
              {cp.compliant != null && (
                <span style={{ color: cp.compliant ? "#16a34a" : "#dc2626", fontWeight: 600 }}>
                  {" "}{cp.compliant ? "Conforme" : "Non conforme"}
                </span>
              )}
              </>
            )}
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
}
