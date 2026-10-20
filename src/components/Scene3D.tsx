import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import type { Contour, ExposureVolume, PlannerProject } from "../types";
import { terrainHeight, terrainMesh, toLocal } from "../lib/geometry";

const BUILDING_COLORS = [0x8e9a9d, 0x9b9d9c, 0xa5a8a7, 0x929796, 0x989c9b];
const ROOF_COLORS = [0x7a8688, 0x849092, 0x8a9496, 0x7e8a8c, 0x889294];

function makeTextSprite(text: string, color = "#445566", size = 3): THREE.Sprite {
  const canvas = document.createElement("canvas");
  const ctx = canvas.getContext("2d")!;
  const fontSize = 64;
  canvas.width = 256;
  canvas.height = 128;
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  ctx.font = `bold ${fontSize}px sans-serif`;
  ctx.fillStyle = color;
  ctx.textAlign = "center";
  ctx.textBaseline = "middle";
  ctx.fillText(text, canvas.width / 2, canvas.height / 2);
  const tex = new THREE.CanvasTexture(canvas);
  tex.needsUpdate = true;
  const mat = new THREE.SpriteMaterial({ map: tex, transparent: true, depthTest: false });
  const sprite = new THREE.Sprite(mat);
  sprite.scale.set(size * 2, size, 1);
  return sprite;
}

function createSkyDome(radius: number): THREE.Mesh {
  const geo = new THREE.SphereGeometry(radius, 32, 16);
  const vertexShader = `
    varying vec3 vWorldPosition;
    void main() {
      vec4 worldPosition = modelMatrix * vec4(position, 1.0);
      vWorldPosition = worldPosition.xyz;
      gl_Position = projectionMatrix * modelViewMatrix * vec4(position, 1.0);
    }
  `;
  const fragmentShader = `
    uniform vec3 topColor;
    uniform vec3 horizonColor;
    uniform float offset;
    uniform float exponent;
    varying vec3 vWorldPosition;
    void main() {
      float h = normalize(vWorldPosition + offset).y;
      gl_FragColor = vec4(mix(horizonColor, topColor, max(pow(max(h, 0.0), exponent), 0.0)), 1.0);
    }
  `;
  const mat = new THREE.ShaderMaterial({
    vertexShader,
    fragmentShader,
    uniforms: {
      topColor: { value: new THREE.Color(0x6baed6) },
      horizonColor: { value: new THREE.Color(0xe8eff5) },
      offset: { value: 10 },
      exponent: { value: 0.6 },
    },
    side: THREE.BackSide,
    depthWrite: false,
  });
  return new THREE.Mesh(geo, mat);
}

function createGroundPlane(radius: number): THREE.Group {
  const group = new THREE.Group();
  const planeGeo = new THREE.PlaneGeometry(radius * 2.5, radius * 2.5);
  const planeMat = new THREE.MeshStandardMaterial({
    color: 0xc8d4c0,
    roughness: 0.95,
    metalness: 0,
    transparent: true,
    opacity: 0.85,
  });
  const plane = new THREE.Mesh(planeGeo, planeMat);
  plane.rotation.x = -Math.PI / 2;
  plane.position.y = -0.05;
  plane.receiveShadow = true;
  group.add(plane);

  const grid = new THREE.GridHelper(radius * 2, 20, 0x8b999c, 0xc4cdcc);
  (grid.material as THREE.Material).transparent = true;
  (grid.material as THREE.Material).opacity = 0.45;
  group.add(grid);

  return group;
}

function createCompassLabels(radius: number): THREE.Group {
  const group = new THREE.Group();
  const labels = [
    { text: "N", pos: [0, 2, -radius - 8], color: "#cc3333" },
    { text: "S", pos: [0, 2, radius + 8], color: "#556677" },
    { text: "E", pos: [radius + 8, 2, 0], color: "#556677" },
    { text: "W", pos: [-radius - 8, 2, 0], color: "#556677" },
  ];
  for (const l of labels) {
    const sprite = makeTextSprite(l.text, l.color, 5);
    sprite.position.set(l.pos[0], l.pos[1], l.pos[2]);
    group.add(sprite);
  }

  const arrowDir = new THREE.Vector3(0, 0, -1);
  const arrowOrigin = new THREE.Vector3(0, 3, -radius - 2);
  const arrow = new THREE.ArrowHelper(arrowDir, arrowOrigin, 5, 0xcc3333, 2, 1.5);
  group.add(arrow);

  return group;
}

function createTower(height: number, terrainY: number): THREE.Group {
  const group = new THREE.Group();

  const towerGeo = new THREE.CylinderGeometry(0.2, 0.8, height, 8);
  const towerMat = new THREE.MeshStandardMaterial({
    color: 0x4a5a63,
    roughness: 0.7,
    metalness: 0.3,
  });
  const tower = new THREE.Mesh(towerGeo, towerMat);
  tower.position.y = terrainY + height / 2;
  tower.castShadow = true;
  group.add(tower);

  const crossbarCount = Math.max(2, Math.floor(height / 5));
  for (let i = 1; i <= crossbarCount; i++) {
    const y = terrainY + (height * i) / (crossbarCount + 1);
    const barWidth = 0.8 - (0.5 * i) / (crossbarCount + 1);
    const barGeo = new THREE.BoxGeometry(barWidth * 2, 0.12, 0.12);
    const bar = new THREE.Mesh(barGeo, towerMat);
    bar.position.y = y;
    bar.castShadow = true;
    group.add(bar);
  }

  const lightGeo = new THREE.SphereGeometry(0.4, 8, 8);
  const lightMat = new THREE.MeshStandardMaterial({
    color: 0xff2222,
    emissive: 0xff0000,
    emissiveIntensity: 0.8,
    roughness: 0.3,
  });
  const light = new THREE.Mesh(lightGeo, lightMat);
  light.position.y = terrainY + height + 0.5;
  group.add(light);

  return group;
}

function createAntenna(
  size: number,
  azimuthDeg: number,
  x: number,
  baseY: number,
  z: number,
): THREE.Group {
  const group = new THREE.Group();
  const antennaH = Math.max(0.6, size);
  const boxGeo = new THREE.BoxGeometry(0.4, antennaH, 0.2);
  const antennaMat = new THREE.MeshStandardMaterial({
    color: 0xe8e8df,
    roughness: 0.6,
    metalness: 0.15,
  });
  const antenna = new THREE.Mesh(boxGeo, antennaMat);
  antenna.castShadow = true;
  group.add(antenna);

  const bracketGeo = new THREE.CylinderGeometry(0.06, 0.06, 1.2, 6);
  const bracketMat = new THREE.MeshStandardMaterial({ color: 0x555555, roughness: 0.8, metalness: 0.4 });
  const bracket = new THREE.Mesh(bracketGeo, bracketMat);
  bracket.rotation.z = Math.PI / 2;
  bracket.position.x = -0.6;
  group.add(bracket);

  group.position.set(x + 0.8, baseY, z);
  group.rotation.y = (-azimuthDeg * Math.PI) / 180;
  return group;
}

function createVolumeMesh(
  volume: ExposureVolume,
  originHeight: number,
  color: string,
): THREE.Mesh {
  const geometry = new THREE.BufferGeometry();
  const positions = new Float32Array(volume.positions.length);
  for (let i = 0; i < positions.length; i += 3) {
    positions[i] = volume.positions[i];
    positions[i + 1] = volume.positions[i + 2] - originHeight;
    positions[i + 2] = -volume.positions[i + 1];
  }
  geometry.setAttribute("position", new THREE.BufferAttribute(positions, 3));
  geometry.computeVertexNormals();

  const vertexShader = `
    varying vec3 vNormal;
    varying vec3 vViewDir;
    void main() {
      vNormal = normalize(normalMatrix * normal);
      vec4 mvPos = modelViewMatrix * vec4(position, 1.0);
      vViewDir = normalize(-mvPos.xyz);
      gl_Position = projectionMatrix * mvPos;
    }
  `;
  const fragmentShader = `
    uniform vec3 baseColor;
    uniform float opacity;
    varying vec3 vNormal;
    varying vec3 vViewDir;
    void main() {
      float fresnel = pow(1.0 - abs(dot(vNormal, vViewDir)), 2.5);
      vec3 rim = mix(baseColor, vec3(1.0), fresnel * 0.5);
      float diffuse = max(dot(vNormal, normalize(vec3(1.0, 2.0, 0.5))), 0.0);
      vec3 final = rim * (0.4 + 0.6 * diffuse);
      gl_FragColor = vec4(final, opacity + fresnel * 0.2);
    }
  `;

  const c = new THREE.Color(color);
  const mat = new THREE.ShaderMaterial({
    vertexShader,
    fragmentShader,
    uniforms: {
      baseColor: { value: c },
      opacity: { value: 0.42 },
    },
    transparent: true,
    side: THREE.DoubleSide,
    depthWrite: false,
  });

  return new THREE.Mesh(geometry, mat);
}

export default function Scene3D({
  project,
  contours,
  volume,
}: {
  project: PlannerProject;
  contours: Contour[];
  volume?: ExposureVolume | null;
}) {
  const host = useRef<HTMLDivElement>(null),
    [error, setError] = useState("");
  useEffect(() => {
    const el = host.current;
    if (!el) return;
    let renderer: THREE.WebGLRenderer;
    try {
      renderer = new THREE.WebGLRenderer({ antialias: true, alpha: false });
    } catch {
      setError("Le rendu 3D nécessite WebGL. La vue CAO reste disponible.");
      return;
    }

    renderer.shadowMap.enabled = true;
    renderer.shadowMap.type = THREE.PCFSoftShadowMap;
    renderer.setPixelRatio(Math.min(devicePixelRatio, 2));
    renderer.toneMapping = THREE.ACESFilmicToneMapping;
    renderer.toneMappingExposure = 1.1;
    el.append(renderer.domElement);

    const scene = new THREE.Scene();
    const radius = project.settings.radius_m;
    const originHeight = terrainHeight(project, 0, 0);

    scene.add(createSkyDome(radius * 5));
    scene.fog = new THREE.FogExp2(0xe0e8ef, 0.0004);

    const camera = new THREE.PerspectiveCamera(35, 1, 0.1, radius * 12);
    camera.position.set(radius * 1.3, radius * 0.9, radius * 1.6);

    const controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.dampingFactor = 0.08;
    controls.maxPolarAngle = Math.PI / 2.05;
    controls.minDistance = 2;
    controls.maxDistance = radius * 5;

    const hemiLight = new THREE.HemisphereLight(0xb1cce0, 0x8a9b6b, 1.2);
    scene.add(hemiLight);

    const sun = new THREE.DirectionalLight(0xfff5e8, 2.8);
    sun.position.set(radius * 0.8, radius * 1.5, radius * 0.5);
    sun.castShadow = true;
    sun.shadow.mapSize.width = 2048;
    sun.shadow.mapSize.height = 2048;
    const shadowSize = Math.min(radius * 1.5, 500);
    sun.shadow.camera.left = -shadowSize;
    sun.shadow.camera.right = shadowSize;
    sun.shadow.camera.top = shadowSize;
    sun.shadow.camera.bottom = -shadowSize;
    sun.shadow.camera.far = radius * 4;
    sun.shadow.bias = -0.0003;
    scene.add(sun);

    const fill = new THREE.DirectionalLight(0xb0c4de, 0.6);
    fill.position.set(-100, 50, -100);
    scene.add(fill);

    scene.add(createGroundPlane(radius));
    scene.add(createCompassLabels(radius));

    // Terrain mesh with height-based coloring
    const terrain = terrainMesh(project);
    if (terrain.points.length >= 3) {
      const points = terrain.points;
      const geo = new THREE.BufferGeometry();
      const posArr = points.flatMap((p) => [p.x, p.elevation - originHeight, -p.y]);
      geo.setAttribute("position", new THREE.Float32BufferAttribute(posArr, 3));
      geo.setIndex(Array.from(terrain.triangles));
      geo.computeVertexNormals();

      const elevations = points.map((p) => p.elevation - originHeight);
      const minElev = Math.min(...elevations);
      const maxElev = Math.max(...elevations);
      const range = maxElev - minElev || 1;
      const colors = new Float32Array(points.length * 3);
      const lowColor = new THREE.Color(0x7daa68);
      const highColor = new THREE.Color(0xa89068);
      for (let i = 0; i < points.length; i++) {
        const t = (elevations[i] - minElev) / range;
        const c = lowColor.clone().lerp(highColor, t);
        colors[i * 3] = c.r;
        colors[i * 3 + 1] = c.g;
        colors[i * 3 + 2] = c.b;
      }
      geo.setAttribute("color", new THREE.Float32BufferAttribute(colors, 3));

      const terrainMesh3d = new THREE.Mesh(
        geo,
        new THREE.MeshStandardMaterial({
          vertexColors: true,
          side: THREE.DoubleSide,
          transparent: true,
          opacity: 0.85,
          roughness: 0.9,
          metalness: 0,
        }),
      );
      terrainMesh3d.receiveShadow = true;
      scene.add(terrainMesh3d);
    }

    // Buildings with variation and shadows
    for (const [bi, b] of project.buildings.entries()) {
      if (b.points.length < 3) continue;
      const shape = new THREE.Shape(
        b.points.map((p) => new THREE.Vector2(p[0], p[1])),
      );
      const geometry = new THREE.ExtrudeGeometry(shape, {
        depth: b.height_m,
        bevelEnabled: false,
      });
      geometry.rotateX(-Math.PI / 2);
      geometry.translate(0, b.foundation_m - originHeight, 0);

      const wallColor = BUILDING_COLORS[bi % BUILDING_COLORS.length];
      const mesh = new THREE.Mesh(
        geometry,
        new THREE.MeshStandardMaterial({
          color: wallColor,
          roughness: 0.85,
          metalness: 0.05,
        }),
      );
      mesh.castShadow = true;
      mesh.receiveShadow = true;
      scene.add(mesh);

      // Rooftop
      const roofShape = new THREE.Shape(
        b.points.map((p) => new THREE.Vector2(p[0], p[1])),
      );
      const roofGeo = new THREE.ShapeGeometry(roofShape);
      roofGeo.rotateX(-Math.PI / 2);
      const roofY = b.foundation_m - originHeight + b.height_m;
      roofGeo.translate(0, roofY + 0.01, 0);
      const roofMesh = new THREE.Mesh(
        roofGeo,
        new THREE.MeshStandardMaterial({
          color: ROOF_COLORS[bi % ROOF_COLORS.length],
          roughness: 0.75,
          metalness: 0.1,
        }),
      );
      roofMesh.receiveShadow = true;
      scene.add(roofMesh);

      // Edges
      scene.add(
        new THREE.LineSegments(
          new THREE.EdgesGeometry(geometry),
          new THREE.LineBasicMaterial({ color: 0x3a4040, linewidth: 1 }),
        ),
      );
    }

    // Antenna towers and panels
    const sourcesGrouped = new Map<string, typeof project.sources>();
    for (const s of project.sources.filter((s) => s.active !== false)) {
      const key = `${s.lat.toFixed(8)},${s.lng.toFixed(8)}`;
      if (!sourcesGrouped.has(key)) sourcesGrouped.set(key, []);
      sourcesGrouped.get(key)!.push(s);
    }
    for (const [, sources] of sourcesGrouped) {
      const s0 = sources[0];
      const [x, y] = toLocal(s0.lat, s0.lng, project.origin);
      const maxH = Math.max(...sources.map((s) => s.height_m));
      const terrainY = terrainHeight(project, x, y) - originHeight;

      const towerGroup = createTower(maxH, terrainY);
      towerGroup.position.x = x;
      towerGroup.position.z = -y;
      scene.add(towerGroup);

      for (const s of sources) {
        const antennaGroup = createAntenna(
          s.antenna_size_m,
          s.azimuth_deg,
          x,
          terrainY + s.height_m,
          -y,
        );
        scene.add(antennaGroup);
      }
    }

    // Contour lines with tube geometry for thickness
    for (const c of contours) {
      for (const seg of c.segments) {
        if (seg.length < 2) continue;
        const pts = seg.map(
          (p) => new THREE.Vector3(p[0], c.height_m - originHeight, -p[1]),
        );
        const curve = new THREE.CatmullRomCurve3(pts, false);
        const tubeGeo = new THREE.TubeGeometry(curve, Math.max(2, pts.length), 0.25, 4, false);
        const tubeMat = new THREE.MeshStandardMaterial({
          color: c.color,
          roughness: 0.5,
          metalness: 0.1,
          transparent: true,
          opacity: 0.85,
        });
        scene.add(new THREE.Mesh(tubeGeo, tubeMat));
      }

      // Height label at the first segment
      if (c.segments.length > 0 && c.segments[0].length > 0) {
        const p = c.segments[0][0];
        const label = makeTextSprite(
          `${c.height_m.toFixed(0)}m`,
          c.color,
          2.5,
        );
        label.position.set(p[0], c.height_m - originHeight + 1.5, -p[1]);
        scene.add(label);
      }
    }

    // Volume (exposure surface) with fresnel shader
    if (volume?.positions.length) {
      scene.add(createVolumeMesh(volume, originHeight, volume.color));
    }

    // Axes helper in corner (small)
    const axesHelper = new THREE.AxesHelper(Math.min(radius * 0.08, 10));
    axesHelper.position.set(-radius, 0.1, radius);
    scene.add(axesHelper);

    // Resize observer
    const observer = new ResizeObserver(() => {
      renderer.setSize(el.clientWidth, el.clientHeight);
      camera.aspect = el.clientWidth / Math.max(1, el.clientHeight);
      camera.updateProjectionMatrix();
    });
    observer.observe(el);

    let frame = 0;
    const animate = () => {
      frame = requestAnimationFrame(animate);
      controls.update();
      renderer.render(scene, camera);
    };
    animate();

    return () => {
      cancelAnimationFrame(frame);
      observer.disconnect();
      controls.dispose();
      scene.traverse((o) => {
        if (o instanceof THREE.Mesh || o instanceof THREE.LineSegments) {
          o.geometry.dispose();
          const materials = Array.isArray(o.material)
            ? o.material
            : [o.material];
          materials.forEach((m) => m.dispose());
        }
        if (o instanceof THREE.Sprite) {
          o.material.map?.dispose();
          o.material.dispose();
        }
      });
      renderer.dispose();
      renderer.domElement.remove();
    };
  }, [project, contours, volume]);
  return (
    <div className="scene-3d" ref={host}>
      {error && <p role="alert">{error}</p>}
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
      <div className="scene-help">
        Rotation : glisser · Zoom : molette · Déplacement : clic droit
      </div>
    </div>
  );
}
