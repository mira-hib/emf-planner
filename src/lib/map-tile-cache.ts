import {isTauri,invoke} from "@tauri-apps/api/core";
import {networkFetch} from "./network";
const CACHE_NAME="emf-planner-map-tiles-v1";
const MAX_TILES=1500;
export async function mapCacheStats(){
  if(isTauri()) return invoke<{count:number;bytes:number;available:boolean;directory:string}>("native_cache_stats");
  if(!globalThis.caches)return {count:0,bytes:0,available:false};
  const cache=await caches.open(CACHE_NAME),keys=await cache.keys();let bytes=0;
  for(const key of keys){const response=await cache.match(key);bytes+=Number(response?.headers.get("x-emf-bytes")??0);}
  return {count:keys.length,bytes,available:true};
}
export async function clearMapCache(){if(isTauri())await invoke("clear_native_cache");if(globalThis.caches)await caches.delete(CACHE_NAME);}
/** The exact URL is the cache key, so provider/zoom and configured account never collide. */
export async function cachedTile(url:string):Promise<Blob> {
  const request=new Request(url),cache=globalThis.caches?await caches.open(CACHE_NAME):undefined;
  const stored=await cache?.match(request);
  if(stored)return stored.blob();
  const controller=new AbortController(),timer=setTimeout(()=>controller.abort(),20000);
  try{
    const response=await networkFetch(request,{signal:controller.signal},true);
    if(!response.ok)throw new Error(`Tuile HTTP ${response.status}`);
    const blob=await response.blob();
    if(!blob.type.startsWith("image/"))throw new Error("La réponse du serveur n’est pas une image.");
    if(cache){
      try{const headers=new Headers(response.headers);headers.set("x-emf-bytes",String(blob.size));await cache.put(request,new Response(blob,{headers}));const keys=await cache.keys();for(const old of keys.slice(0,Math.max(0,keys.length-MAX_TILES)))await cache.delete(old);}catch{/* A full browser cache must not prevent displaying the downloaded tile. */}
    }
    return blob;
  }finally{clearTimeout(timer);}
}
