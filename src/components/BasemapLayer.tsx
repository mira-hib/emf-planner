import { useEffect } from "react";
import { TileLayer, type TileLayerOptions } from "leaflet";
import { useMap } from "react-leaflet";
import { tileUrl, type BasemapProvider } from "../lib/map-providers";

class NativeTileLayer extends TileLayer {
  constructor(
    private template: string,
    options: TileLayerOptions,
  ) {
    super(template, options);
  }
  getTileUrl(coords: { x: number; y: number; z: number }) {
    return tileUrl(this.template, coords.x, coords.y, coords.z);
  }
}

export default function BasemapLayer({
  provider,
  onStatus,
}: {
  provider?: BasemapProvider;
  onStatus: (message: string) => void;
}) {
  const map = useMap();
  useEffect(() => {
    if (!provider) {
      onStatus(
        "Fond de carte inconnu. Choisissez un fond ou configurez son URL.",
      );
      return;
    }
    onStatus(provider.configuration ?? "");
    if (!provider.url || provider.kind === "image" || provider.configuration)
      return;
    const layer = new NativeTileLayer(provider.url, {
      attribution: provider.attribution,
      maxZoom: 25,
      maxNativeZoom: provider.maxZoom ?? 19,
    });
    let loaded = false;
    layer.on("tileload", () => {
      loaded = true;
      onStatus("");
    });
    layer.on("tileerror", () => {
      if (!loaded)
        onStatus(
          "Tuiles indisponibles : vérifiez la connexion et l’URL ou la clé du fournisseur dans Fonds de carte personnalisés.",
        );
    });
    layer.addTo(map);
    return () => {
      layer.off();
      layer.remove();
    };
  }, [
    map,
    provider?.url,
    provider?.attribution,
    provider?.kind,
    provider?.configuration,
  ]);
  return null;
}
