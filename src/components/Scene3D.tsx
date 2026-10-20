import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";
import Delaunator from "delaunator";
import type { Contour, PlannerProject } from "../types";
import { terrainHeight, toLocal } from "../lib/geometry";
export default function Scene3D({
  project,
  contours,
}: {
  project: PlannerProject;
  contours: Contour[];
}) {
  const host = useRef<HTMLDivElement>(null),
    [error, setError] = useState("");
  useEffect(() => {
    const el = host.current;
    if (!el) return;
    let renderer: THREE.WebGLRenderer;
    try {
      renderer = new THREE.WebGLRenderer({ antialias: true });
    } catch {
      setError("Le rendu 3D nécessite WebGL. La vue CAO reste disponible.");
      return;
    }
    const scene = new THREE.Scene();
    scene.background = new THREE.Color("#a8c9dd");
    const radius = project.settings.radius_m;
    const camera = new THREE.PerspectiveCamera(40, 1, 0.1, 100000);
    camera.position.set(radius * 1.5, radius * 1.2, radius * 1.5);
    renderer.setPixelRatio(Math.min(devicePixelRatio, 2));
    el.append(renderer.domElement);
    const controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    scene.add(new THREE.AmbientLight(0xffffff, 2));
    const sun = new THREE.DirectionalLight(0xffffff, 2.5);
    sun.position.set(100, 300, 100);
    scene.add(sun);
    const grid = new THREE.GridHelper(radius * 2, 20, 0x8b999c, 0xc4cdcc);
    scene.add(grid);
    const originHeight = terrainHeight(project, 0, 0);
    if (project.terrain_points.length >= 3) {
      const points = project.terrain_points,
        del = Delaunator.from(
          points,
          (p) => p.x,
          (p) => p.y,
        ),
        geo = new THREE.BufferGeometry();
      geo.setAttribute(
        "position",
        new THREE.Float32BufferAttribute(
          points.flatMap((p) => [p.x, p.elevation - originHeight, -p.y]),
          3,
        ),
      );
      geo.setIndex(Array.from(del.triangles));
      geo.computeVertexNormals();
      scene.add(
        new THREE.Mesh(
          geo,
          new THREE.MeshStandardMaterial({
            color: 0xd9d7c5,
            side: THREE.DoubleSide,
            transparent: true,
            opacity: 0.8,
          }),
        ),
      );
    }
    for (const b of project.buildings) {
      if (b.points.length < 3) continue;
      const shape = new THREE.Shape(
          b.points.map((p) => new THREE.Vector2(p[0], p[1])),
        ),
        geometry = new THREE.ExtrudeGeometry(shape, {
          depth: b.height_m,
          bevelEnabled: false,
        });
      geometry.rotateX(-Math.PI / 2);
      geometry.translate(0, b.foundation_m - originHeight, 0);
      const mesh = new THREE.Mesh(
        geometry,
        new THREE.MeshStandardMaterial({ color: 0x9b9d9c, roughness: 0.9 }),
      );
      scene.add(mesh);
      scene.add(
        new THREE.LineSegments(
          new THREE.EdgesGeometry(geometry),
          new THREE.LineBasicMaterial({ color: 0x454a4a }),
        ),
      );
    }
    for (const s of project.sources.filter((s) => s.active !== false)) {
      const [x, y] = toLocal(s.lat, s.lng, project.origin),
        h = s.height_m;
      const tower = new THREE.Mesh(
        new THREE.CylinderGeometry(0.35, 1, h, 6),
        new THREE.MeshStandardMaterial({ color: 0x4e5e67 }),
      );
      tower.position.set(
        x,
        terrainHeight(project, x, y) - originHeight + h / 2,
        -y,
      );
      scene.add(tower);
      const antenna = new THREE.Mesh(
        new THREE.BoxGeometry(0.5, Math.max(0.5, s.antenna_size_m), 0.3),
        new THREE.MeshStandardMaterial({ color: 0xe8e8df }),
      );
      antenna.position.set(
        x + 0.6,
        terrainHeight(project, x, y) - originHeight + h,
        -y,
      );
      antenna.rotation.y = (-s.azimuth_deg * Math.PI) / 180;
      scene.add(antenna);
    }
    for (const c of contours) {
      const positions = c.segments.flatMap((s) =>
          s.flatMap((p) => [p[0], c.height_m, -p[1]]),
        ),
        geo = new THREE.BufferGeometry();
      geo.setAttribute(
        "position",
        new THREE.Float32BufferAttribute(positions, 3),
      );
      scene.add(
        new THREE.LineSegments(
          geo,
          new THREE.LineBasicMaterial({
            color: c.color,
            transparent: true,
            opacity: 0.8,
          }),
        ),
      );
    }
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
      });
      renderer.dispose();
      renderer.domElement.remove();
    };
  }, [project, contours]);
  return (
    <div className="scene-3d" ref={host}>
      {error && <p role="alert">{error}</p>}
      <div className="scene-help">
        Rotation : glisser · Zoom : molette · Déplacement : clic droit
      </div>
    </div>
  );
}
