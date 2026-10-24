import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import {
  Hand,
  Orbit,
  Scan,
  ZoomIn,
  ZoomOut,
  Layers,
  Home,
  FileUp,
} from "lucide-react";
import type { Contour, ExposureVolume, PlannerProject } from "../types";
import type { CutResult } from "../lib/usePlanner";
import { terrainHeight, terrainMesh, toGeo } from "../lib/geometry";
import {
  closedContourPolygons,
  contourPaths,
  horizontalPosition,
  mastDimensions,
  mastEdges,
  mastFoundation,
  scenePosition,
  verticalPosition,
} from "../lib/scene-geometry";
import { geographicTile, selectedMapProvider, tileUrl } from "../lib/map-providers";
import "./Scene3D.css";

type View = "home" | "top" | "bottom" | "front" | "back" | "left" | "right";
type Layer =
  "buildings" | "roads" | "masts" | "horizontal" | "vertical" | "volume";
const LAYERS: [Layer, string][] = [
  ["buildings", "Bâtiments"],
  ["roads", "Routes"],
  ["masts", "Mâts dessinés"],
  ["horizontal", "Coupes horizontales"],
  ["vertical", "Coupes verticales"],
  ["volume", "Volume calculé"],
];
const VIEWS: [View, string][] = [
  ["top", "Haut"],
  ["bottom", "Bas"],
  ["front", "Face"],
  ["back", "Arrière"],
  ["left", "Gauche"],
  ["right", "Droite"],
];

function makeBackground() {
  const canvas = document.createElement("canvas");
  canvas.width = 1;
  canvas.height = 256;
  const context = canvas.getContext("2d")!,
    gradient = context.createLinearGradient(0, 0, 0, 256);
  gradient.addColorStop(0, "#66a3d2");
  gradient.addColorStop(1, "#f5f5f5");
  context.fillStyle = gradient;
  context.fillRect(0, 0, 1, 256);
  const texture = new THREE.CanvasTexture(canvas);
  texture.colorSpace = THREE.SRGBColorSpace;
  return texture;
}

function lineObject(points: THREE.Vector3[], color: string | number) {
  return new THREE.Line(
    new THREE.BufferGeometry().setFromPoints(points),
    new THREE.LineBasicMaterial({ color }),
  );
}

function addHorizontal(group: THREE.Group, contour: Contour) {
  const paths = contourPaths(contour);
  for (const path of paths)
    group.add(
      lineObject(
        path.map(
          (point) =>
            new THREE.Vector3(...horizontalPosition(point, contour.height_m)),
        ),
        contour.color,
      ),
    );
  for (const { outer, holes } of closedContourPolygons(paths)) {
    const shape = new THREE.Shape(outer.map((p) => new THREE.Vector2(...p)));
    shape.holes = holes.map(
      (hole) => new THREE.Path(hole.map((p) => new THREE.Vector2(...p))),
    );
    const geometry = new THREE.ShapeGeometry(shape);
    geometry.rotateX(-Math.PI / 2);
    geometry.translate(0, contour.height_m, 0);
    group.add(
      new THREE.Mesh(
        geometry,
        new THREE.MeshBasicMaterial({
          color: contour.color,
          transparent: true,
          opacity: 70 / 255,
          side: THREE.DoubleSide,
          depthWrite: false,
        }),
      ),
    );
  }
}

function makeVolume(volume: ExposureVolume, originAltitude: number) {
  const positions = new Float32Array(volume.positions.length);
  for (let i = 0; i < positions.length; i += 3)
    positions.set(
      scenePosition(
        volume.positions[i],
        volume.positions[i + 1],
        volume.positions[i + 2],
        originAltitude,
      ),
      i,
    );
  const geometry = new THREE.BufferGeometry();
  geometry.setAttribute("position", new THREE.BufferAttribute(positions, 3));
  geometry.computeVertexNormals();
  return new THREE.Mesh(
    geometry,
    new THREE.MeshLambertMaterial({
      color: volume.color,
      side: THREE.DoubleSide,
      transparent: true,
      opacity: 0.4,
      depthWrite: false,
    }),
  );
}

/** Project the same tile map onto the terrain; unavailable tiles leave a white surface. */
function applyMapTexture(
  project: PlannerProject,
  mesh: THREE.Mesh,
  points: { x: number; y: number }[],
) {
  if (!project.settings.show_basemap || !points.length) return () => {};
  const provider = selectedMapProvider(project);
  if (provider?.kind === "image") {
    const background = project.background_image;
    if (!background) return () => {};
    const coords = points.map((p) => geographicTile(...toGeo(p.x, p.y, project.origin), 0));
    const left = Math.min(...coords.map((p) => p[0])), right = Math.max(...coords.map((p) => p[0]));
    const top = Math.min(...coords.map((p) => p[1])), bottom = Math.max(...coords.map((p) => p[1]));
    if (right === left || bottom === top) return () => {};
    const canvas = document.createElement("canvas");
    canvas.width = canvas.height = 2048;
    const context = canvas.getContext("2d")!;
    context.fillStyle = "white";
    context.fillRect(0, 0, canvas.width, canvas.height);
    const texture = new THREE.CanvasTexture(canvas);
    texture.colorSpace = THREE.SRGBColorSpace;
    const uv = new Float32Array(points.length * 2);
    coords.forEach(([x, y], i) => { uv[2 * i] = (x - left) / (right - left); uv[2 * i + 1] = 1 - (y - top) / (bottom - top); });
    mesh.geometry.setAttribute("uv", new THREE.BufferAttribute(uv, 2));
    const material = mesh.material as THREE.MeshBasicMaterial;
    material.map = texture;
    material.needsUpdate = true;
    const image = new Image();
    image.onload = () => {
      const [x0, y0] = geographicTile(background.north, background.west, 0);
      const [x1, y1] = geographicTile(background.south, background.east, 0);
      context.globalAlpha = background.opacity;
      context.drawImage(image, (x0 - left) / (right - left) * canvas.width, (y0 - top) / (bottom - top) * canvas.height, (x1 - x0) / (right - left) * canvas.width, (y1 - y0) / (bottom - top) * canvas.height);
      texture.needsUpdate = true;
    };
    image.src = background.data_url;
    return () => { image.onload = null; image.src = ""; texture.dispose(); };
  }
  if (!provider || !provider.url || provider.configuration) return () => {};
  const west = Math.min(...points.map((p) => p.x)),
    east = Math.max(...points.map((p) => p.x));
  const south = Math.min(...points.map((p) => p.y)),
    north = Math.max(...points.map((p) => p.y));
  const [southLat, westLng] = toGeo(west, south, project.origin),
    [northLat, eastLng] = toGeo(east, north, project.origin);
  const tile = (lat: number, lng: number, zoom: number) => geographicTile(lat, lng, zoom, provider.projection);
  let zoom = Math.max(0, Math.min(provider.maxZoom ?? 19, Math.round(project.map_zoom))),
    nw = tile(northLat, westLng, zoom),
    se = tile(southLat, eastLng, zoom);
  while (
    zoom > 0 &&
    ((Math.ceil(se[0]) - Math.floor(nw[0])) *
      (Math.ceil(se[1]) - Math.floor(nw[1])) >
      128 ||
      Math.ceil(se[0]) - Math.floor(nw[0]) > 15 ||
      Math.ceil(se[1]) - Math.floor(nw[1]) > 15)
  ) {
    zoom--;
    nw = tile(northLat, westLng, zoom);
    se = tile(southLat, eastLng, zoom);
  }
  const left = Math.floor(nw[0]),
    top = Math.floor(nw[1]),
    right = Math.floor(se[0]),
    bottom = Math.floor(se[1]);
  const canvas = document.createElement("canvas");
  canvas.width = (right - left + 1) * 256;
  canvas.height = (bottom - top + 1) * 256;
  const context = canvas.getContext("2d")!;
  context.fillStyle = "white";
  context.fillRect(0, 0, canvas.width, canvas.height);
  const texture = new THREE.CanvasTexture(canvas);
  texture.colorSpace = THREE.SRGBColorSpace;
  const uv = new Float32Array(points.length * 2);
  for (let i = 0; i < points.length; i++) {
    const [lat, lng] = toGeo(points[i].x, points[i].y, project.origin),
      [tx, ty] = tile(lat, lng, zoom);
    uv[i * 2] = (tx - left) / (right - left + 1);
    uv[i * 2 + 1] = 1 - (ty - top) / (bottom - top + 1);
  }
  mesh.geometry.setAttribute("uv", new THREE.BufferAttribute(uv, 2));
  const material = mesh.material as THREE.MeshBasicMaterial;
  material.map = texture;
  material.needsUpdate = true;
  let disposed = false;
  const images: HTMLImageElement[] = [];
  for (let x = left; x <= right; x++)
    for (let y = top; y <= bottom; y++) {
      const image = new Image();
      image.crossOrigin = "anonymous";
      image.onload = () => {
        if (!disposed) {
          context.drawImage(image, (x - left) * 256, (y - top) * 256, 256, 256);
          texture.needsUpdate = true;
        }
      };
      image.src = tileUrl(provider.url, x, y, zoom);
      images.push(image);
    }
  return () => {
    disposed = true;
    images.forEach((image) => {
      image.onload = null;
      image.src = "";
    });
    texture.dispose();
  };
}

/** UC_3D rasterizes roads into the terrain surface, avoiding floating segments. */
function makeRoadOverlay(
  project: PlannerProject,
  geometry: THREE.BufferGeometry,
  points: { x: number; y: number }[],
) {
  const west = Math.min(...points.map((point) => point.x)),
    east = Math.max(...points.map((point) => point.x));
  const south = Math.min(...points.map((point) => point.y)),
    north = Math.max(...points.map((point) => point.y));
  const width = Math.max(1, east - west),
    height = Math.max(1, north - south);
  const scale = Math.min(2.5, 4096 / Math.max(width, height));
  const canvas = document.createElement("canvas");
  canvas.width = Math.max(1, Math.round(width * scale));
  canvas.height = Math.max(1, Math.round(height * scale));
  const context = canvas.getContext("2d")!;
  const roads = project.lines.filter(
    (line) => line.kind === "road" && line.points.length > 1,
  );
  for (const inner of [false, true])
    for (const road of roads) {
      const lineWidth =
        Math.max(0.1, road.width) * scale -
        (inner ? (road.border_width ?? 1) : 0);
      if (lineWidth <= 0) continue;
      context.beginPath();
      road.points.forEach(([x, y], index) => {
        const px = ((x - west) / width) * canvas.width,
          py = ((north - y) / height) * canvas.height;
        if (index) context.lineTo(px, py);
        else context.moveTo(px, py);
      });
      context.strokeStyle = inner ? "white" : "black";
      context.lineWidth = lineWidth;
      context.stroke();
    }
  const texture = new THREE.CanvasTexture(canvas);
  texture.colorSpace = THREE.SRGBColorSpace;
  const roadGeometry = geometry.clone(),
    uv = new Float32Array(points.length * 2);
  points.forEach((point, index) => {
    uv[index * 2] = (point.x - west) / width;
    uv[index * 2 + 1] = (point.y - south) / height;
  });
  roadGeometry.setAttribute("uv", new THREE.BufferAttribute(uv, 2));
  return {
    texture,
    mesh: new THREE.Mesh(
      roadGeometry,
      new THREE.MeshBasicMaterial({
        map: texture,
        transparent: true,
        side: THREE.DoubleSide,
        depthWrite: false,
        polygonOffset: true,
        polygonOffsetFactor: -2,
        polygonOffsetUnits: -2,
      }),
    ),
  };
}

export default function Scene3D({
  project,
  contours,
  verticalResults = [],
  volume,
}: {
  project: PlannerProject;
  contours: Contour[];
  verticalResults?: CutResult[];
  volume?: ExposureVolume | null;
}) {
  const host = useRef<HTMLDivElement>(null),
    axesHost = useRef<SVGSVGElement>(null),
    [error, setError] = useState("");
  const commands = useRef<{
    view: (view: View) => void;
    fit: () => void;
    zoom: (factor: number) => void;
    mode: (mode: "pan" | "rotate") => void;
    layer: (name: Layer, show: boolean) => void;
  } | null>(null);
  const [mode, setMode] = useState<"pan" | "rotate">("rotate"),
    [layersOpen, setLayersOpen] = useState(false);
  const [layers, setLayers] = useState<Record<Layer, boolean>>({
    buildings: true,
    roads: project.settings.display_options?.lines !== false,
    masts: true,
    horizontal: true,
    vertical: true,
    volume: true,
  });
  const layersRef = useRef(layers),
    modeRef = useRef(mode);
  layersRef.current = layers;
  modeRef.current = mode;

  useEffect(() => {
    const element = host.current;
    if (!element) return;
    let renderer: THREE.WebGLRenderer;
    try {
      renderer = new THREE.WebGLRenderer({ antialias: true, alpha: false });
    } catch {
      setError("Le rendu 3D nécessite WebGL. La vue CAO reste disponible.");
      return;
    }
    setError("");
    renderer.setPixelRatio(Math.min(devicePixelRatio, 2));
    renderer.outputColorSpace = THREE.SRGBColorSpace;
    renderer.setClearColor(0xf5f5f5);
    element.prepend(renderer.domElement);
    renderer.domElement.setAttribute("aria-label", "Modèle 3D du projet");
    const scene = new THREE.Scene(),
      background = makeBackground();
    scene.background = background;
    const content = new THREE.Group();
    scene.add(content);
    const groups = Object.fromEntries(
      LAYERS.map(([name]) => [name, new THREE.Group()]),
    ) as Record<Layer, THREE.Group>;
    for (const [name] of LAYERS) {
      groups[name].visible = layersRef.current[name];
      content.add(groups[name]);
    }
    scene.add(new THREE.AmbientLight(0xffffff, 0.8));
    const light = new THREE.DirectionalLight(0xffffff, 1);
    light.position.set(-200, 400, 300);
    scene.add(light);
    const originAltitude = terrainHeight(project, 0, 0),
      terrain = terrainMesh(project);
    const area = project.settings.calculation_area_m ?? {
      x_min: -project.settings.radius_m,
      x_max: project.settings.radius_m,
      y_min: -project.settings.radius_m,
      y_max: project.settings.radius_m,
    };
    const terrainPoints =
      terrain.points.length >= 3
        ? terrain.points
        : [
            { x: area.x_min, y: area.y_min, elevation: 0 },
            { x: area.x_max, y: area.y_min, elevation: 0 },
            { x: area.x_max, y: area.y_max, elevation: 0 },
            { x: area.x_min, y: area.y_max, elevation: 0 },
          ];
    const terrainGeometry = new THREE.BufferGeometry();
    terrainGeometry.setAttribute(
      "position",
      new THREE.Float32BufferAttribute(
        terrainPoints.flatMap((point) =>
          scenePosition(point.x, point.y, point.elevation, originAltitude),
        ),
        3,
      ),
    );
    terrainGeometry.setIndex(
      terrain.points.length >= 3
        ? Array.from(terrain.triangles)
        : [0, 1, 2, 0, 2, 3],
    );
    terrainGeometry.computeVertexNormals();
    const ground = new THREE.Mesh(
      terrainGeometry,
      new THREE.MeshBasicMaterial({
        color: 0xffffff,
        side: THREE.DoubleSide,
      }),
    );
    content.add(ground);
    const releaseMap = applyMapTexture(project, ground, terrainPoints);
    const roadOverlay = makeRoadOverlay(
      project,
      terrainGeometry,
      terrainPoints,
    );
    groups.roads.add(roadOverlay.mesh);

    for (const building of project.buildings) {
      if (building.points.length < 3) continue;
      const shape = new THREE.Shape(
        building.points.map((point) => new THREE.Vector2(...point)),
      );
      const geometry = new THREE.ExtrudeGeometry(shape, {
        depth: building.height_m,
        bevelEnabled: false,
      });
      geometry.rotateX(-Math.PI / 2);
      geometry.translate(0, building.foundation_m - originAltitude, 0);
      groups.buildings.add(
        new THREE.Mesh(
          geometry,
          new THREE.MeshLambertMaterial({
            color: 0x808080,
            side: THREE.DoubleSide,
          }),
        ),
      );
    }

    for (const annotation of project.lines.filter(
      (line) => line.kind === "label",
    )) {
      const dimensions = mastDimensions(annotation.name),
        point = annotation.points[0];
      if (!dimensions || !point) continue;
      const edges = mastEdges(
        dimensions,
        point[0],
        point[1],
        mastFoundation(project, ...point),
        originAltitude,
      );
      const geometry = new THREE.BufferGeometry().setFromPoints(
        edges.flatMap((edge) => edge.map((p) => new THREE.Vector3(...p))),
      );
      groups.masts.add(
        new THREE.LineSegments(
          geometry,
          new THREE.LineBasicMaterial({ color: 0x000000 }),
        ),
      );
    }
    for (const contour of contours) addHorizontal(groups.horizontal, contour);
    for (const result of verticalResults) {
      const cut = project.vertical_cuts.find(
        (cut) => cut.id === (result.cut_id ?? result.id),
      );
      if (!cut?.active) continue;
      for (const path of contourPaths(result.contours))
        groups.vertical.add(
          lineObject(
            path.map(
              (point) => new THREE.Vector3(...verticalPosition(point, cut)),
            ),
            result.contours.color,
          ),
        );
    }
    if (volume?.positions.length)
      groups.volume.add(makeVolume(volume, originAltitude));

    const camera = new THREE.OrthographicCamera(
        -100,
        100,
        100,
        -100,
        0.1,
        100000,
      ),
      controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.dampingFactor = 0.12;
    controls.minZoom = 0.02;
    controls.maxZoom = 500;
    controls.mouseButtons.MIDDLE = THREE.MOUSE.ROTATE;
    controls.mouseButtons.RIGHT = THREE.MOUSE.PAN;
    const setMode = (mode: "pan" | "rotate") => {
      controls.mouseButtons.LEFT =
        mode === "pan" ? THREE.MOUSE.PAN : THREE.MOUSE.ROTATE;
    };
    setMode(modeRef.current);
    const boundingBox = () => {
      const box = new THREE.Box3();
      content.traverseVisible((object) => {
        if (object instanceof THREE.Mesh || object instanceof THREE.Line) {
          object.geometry.computeBoundingBox();
          if (object.geometry.boundingBox)
            box.union(
              object.geometry.boundingBox
                .clone()
                .applyMatrix4(object.matrixWorld),
            );
        }
      });
      return box.isEmpty()
        ? new THREE.Box3(
            new THREE.Vector3(-10, 0, -10),
            new THREE.Vector3(10, 10, 10),
          )
        : box;
    };
    let frustumHeight = 200;
    const resize = () => {
      const width = element.clientWidth,
        height = Math.max(1, element.clientHeight),
        aspect = width / height;
      renderer.setSize(width, height);
      camera.left = (-frustumHeight * aspect) / 2;
      camera.right = -camera.left;
      camera.top = frustumHeight / 2;
      camera.bottom = -camera.top;
      camera.updateProjectionMatrix();
    };
    const fit = () => {
      content.updateMatrixWorld(true);
      const box = boundingBox(),
        center = box.getCenter(new THREE.Vector3()),
        size = box.getSize(new THREE.Vector3());
      const direction = camera.position
        .clone()
        .sub(controls.target)
        .normalize();
      if (!direction.length()) direction.set(0, 0.6, 1).normalize();
      const distance = Math.max(20, size.length() * 2);
      camera.position.copy(center).addScaledVector(direction, distance);
      controls.target.copy(center);
      camera.lookAt(center);
      camera.updateMatrixWorld(true);
      const projected = new THREE.Box3();
      for (const x of [box.min.x, box.max.x])
        for (const y of [box.min.y, box.max.y])
          for (const z of [box.min.z, box.max.z])
            projected.expandByPoint(
              new THREE.Vector3(x, y, z).applyMatrix4(
                camera.matrixWorldInverse,
              ),
            );
      const dimensions = projected.getSize(new THREE.Vector3()),
        aspect = element.clientWidth / Math.max(1, element.clientHeight);
      frustumHeight =
        Math.max(5, dimensions.y, dimensions.x / Math.max(0.1, aspect)) * 1.65;
      camera.zoom = 1;
      camera.near = 0.1;
      camera.far = Math.max(1000, distance * 5);
      resize();
      controls.update();
    };
    const view = (view: View) => {
      const directions: Record<View, [number, number, number]> = {
        home: [0, Math.tan((50 * Math.PI) / 180), 1],
        top: [0, 1, 0],
        bottom: [0, -1, 0],
        front: [0, 0, 1],
        back: [0, 0, -1],
        left: [-1, 0, 0],
        right: [1, 0, 0],
      };
      camera.up.set(0, 1, 0);
      if (view === "top") camera.up.set(0, 0, -1);
      if (view === "bottom") camera.up.set(0, 0, 1);
      camera.position
        .copy(controls.target)
        .add(new THREE.Vector3(...directions[view]).multiplyScalar(500));
      controls.update();
      fit();
    };
    commands.current = {
      view,
      fit,
      zoom: (factor) => {
        camera.zoom = THREE.MathUtils.clamp(camera.zoom * factor, 0.02, 500);
        camera.updateProjectionMatrix();
      },
      mode: setMode,
      layer: (name, show) => {
        groups[name].visible = show;
      },
    };
    view("home");
    const observer = new ResizeObserver(resize);
    observer.observe(element);
    let frame = 0;
    const animate = () => {
      frame = requestAnimationFrame(animate);
      controls.update();
      renderer.render(scene, camera);
      const axes = axesHost.current;
      if (axes) {
        const inverse = camera.quaternion.clone().invert();
        [
          [1, 0, 0],
          [0, 0, -1],
          [0, 1, 0],
        ].forEach((direction, index) => {
          const vector = new THREE.Vector3(
            direction[0],
            direction[1],
            direction[2],
          ).applyQuaternion(inverse);
          const x = 35 + vector.x * 25,
            y = 35 - vector.y * 25;
          const axis = axes.children[index],
            line = axis?.children[0],
            label = axis?.children[1];
          line?.setAttribute("x2", String(x));
          line?.setAttribute("y2", String(y));
          label?.setAttribute("x", String(x + 3));
          label?.setAttribute("y", String(y - 3));
        });
      }
    };
    animate();
    return () => {
      commands.current = null;
      cancelAnimationFrame(frame);
      observer.disconnect();
      controls.dispose();
      releaseMap();
      roadOverlay.texture.dispose();
      background.dispose();
      content.traverse((object) => {
        if (object instanceof THREE.Mesh || object instanceof THREE.Line) {
          object.geometry.dispose();
          (Array.isArray(object.material)
            ? object.material
            : [object.material]
          ).forEach((material) => material.dispose());
        }
      });
      renderer.dispose();
      renderer.domElement.remove();
    };
  }, [project, contours, verticalResults, volume]);

  return (
    <div className="scene-3d" ref={host}>
      {error && <p role="alert">{error}</p>}
      <div
        className="scene-navigation"
        role="toolbar"
        aria-label="Navigation 3D"
      >
        <button
          title="Exporter PDF 3D — non encore disponible"
          aria-label="Exporter PDF 3D"
          disabled
        >
          <FileUp size={18} />
        </button>
        <button
          title="Vue initiale"
          aria-label="Vue initiale"
          onClick={() => commands.current?.view("home")}
        >
          <Home size={18} />
        </button>
        <button
          title="Déplacement"
          aria-label="Déplacement 3D"
          aria-pressed={mode === "pan"}
          onClick={() => {
            setMode("pan");
            commands.current?.mode("pan");
          }}
        >
          <Hand size={18} />
        </button>
        <button
          title="Rotation"
          aria-label="Rotation 3D"
          aria-pressed={mode === "rotate"}
          onClick={() => {
            setMode("rotate");
            commands.current?.mode("rotate");
          }}
        >
          <Orbit size={18} />
        </button>
        <button
          title="Zoom sur l’ensemble"
          aria-label="Zoom sur l’ensemble"
          onClick={() => commands.current?.fit()}
        >
          <Scan size={18} />
        </button>
        <button
          title="Zoom avant"
          aria-label="Zoom avant 3D"
          onClick={() => commands.current?.zoom(1.25)}
        >
          <ZoomIn size={18} />
        </button>
        <button
          title="Zoom arrière"
          aria-label="Zoom arrière 3D"
          onClick={() => commands.current?.zoom(0.8)}
        >
          <ZoomOut size={18} />
        </button>
      </div>
      <div className="scene-view-cube" role="group" aria-label="Vues du modèle">
        <div className="scene-cube-faces">
          <button
            onClick={() => commands.current?.view("top")}
            aria-label="Vue de dessus"
          >
            TOP
          </button>
          <button
            onClick={() => commands.current?.view("front")}
            aria-label="Vue de face"
          >
            FRONT
          </button>
          <button
            onClick={() => commands.current?.view("right")}
            aria-label="Vue de droite"
          >
            RIGHT
          </button>
        </div>
        <select
          aria-label="Orientation 3D"
          defaultValue="home"
          onChange={(event) =>
            commands.current?.view(event.target.value as View)
          }
        >
          <option value="home">Vue initiale</option>
          {VIEWS.map(([value, label]) => (
            <option key={value} value={value}>
              {label}
            </option>
          ))}
        </select>
      </div>
      <div className="scene-layer-control">
        <button
          aria-expanded={layersOpen}
          onClick={() => setLayersOpen(!layersOpen)}
        >
          <Layers size={15} /> Affichage 3D
        </button>
        {layersOpen && (
          <fieldset>
            <legend>Objets visibles</legend>
            {LAYERS.map(([name, label]) => (
              <label key={name}>
                <input
                  type="checkbox"
                  checked={layers[name]}
                  onChange={(event) => {
                    const show = event.target.checked;
                    setLayers({ ...layers, [name]: show });
                    commands.current?.layer(name, show);
                  }}
                />
                {label}
              </label>
            ))}
          </fieldset>
        )}
      </div>
      {volume && (
        <div
          className="volume-summary"
          role="status"
          data-triangles={volume.triangles}
        >
          <strong>
            Exposition cumulée à 100 % —{" "}
            {project.limits[volume.limit_index]?.name}
          </strong>
          <span>
            {volume.triangles.toLocaleString("fr-FR")} triangles · pas{" "}
            {volume.step_m} m
          </span>
          {volume.clipped && (
            <span>
              La surface atteint les bords du volume. Agrandissez la zone pour
              la compléter.
            </span>
          )}
          {!volume.triangles && (
            <span>Aucune surface à 100 % détectée à cette résolution.</span>
          )}
        </div>
      )}
      <svg
        className="scene-coordinate-axes"
        ref={axesHost}
        viewBox="0 0 75 75"
        role="img"
        aria-label="Axes du modèle X est, Y nord, Z altitude"
      >
        {["X", "Y", "Z"].map((name, index) => (
          <g
            key={name}
            fill={["#d73725", "#177638", "#255faa"][index]}
            stroke={["#d73725", "#177638", "#255faa"][index]}
          >
            <line x1="35" y1="35" x2="35" y2="35" strokeWidth="1.5" />
            <text x="35" y="35" stroke="none" fontSize="10">
              {name}
            </text>
          </g>
        ))}
      </svg>
      <div className="scene-help">
        Rotation : bouton central · Déplacement : clic droit · Zoom : molette
      </div>
      {project.settings.show_basemap && (
        <span className="scene-map-attribution">
          {selectedMapProvider(project)?.attribution.replace(/<[^>]*>/g, "") ?? ""}
        </span>
      )}
    </div>
  );
}
