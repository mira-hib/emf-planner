import { useEffect } from "react";
import { TileLayer, type TileLayerOptions, type Coords, type DoneCallback } from "leaflet";
import { useMap } from "react-leaflet";
import { tileUrl, type BasemapProvider } from "../lib/map-providers";
import {cachedTile} from "../lib/map-tile-cache";

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
  createTile(coords:Coords,done:DoneCallback){
    const image=document.createElement("img");
    image.alt="";image.setAttribute("role","presentation");
    image.onload=()=>done(undefined,image);
    image.onerror=()=>done(new Error("La tuile n’a pas pu être chargée."),image);
    const url=this.getTileUrl(coords);
    void cachedTile(url).then(blob=>{const objectUrl=URL.createObjectURL(blob);image.onload=()=>{URL.revokeObjectURL(objectUrl);done(undefined,image);};image.onerror=()=>{URL.revokeObjectURL(objectUrl);done(new Error("Image de tuile invalide."),image);};image.src=objectUrl;}).catch(()=>{image.src=url;});
    // Keep the resolved endpoint inspectable even when pixels came from the persistent cache.
    image.dataset.tileUrl=url;
    return image;
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
