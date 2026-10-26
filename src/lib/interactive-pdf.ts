import * as THREE from "three";
export interface PdfMesh { positions: number[]; normals: number[]; faces: number[]; color: [number, number, number] }
export function sceneMeshes(root: THREE.Object3D): PdfMesh[] {
  root.updateMatrixWorld(true); const meshes: PdfMesh[] = [];
  root.traverseVisible((object) => {
    const append = (geometry: THREE.BufferGeometry, matrix: THREE.Matrix4, material: THREE.Material) => {
      const cloned = geometry.clone().applyMatrix4(matrix); if (!cloned.getAttribute("normal")) cloned.computeVertexNormals();
      const pos = cloned.getAttribute("position"), normal = cloned.getAttribute("normal"); if (!pos || pos.count < 3) { cloned.dispose(); return; }
      const positions = Array.from(pos.array), normals = Array.from(normal.array), faces = cloned.index ? Array.from(cloned.index.array) : Array.from({ length: pos.count }, (_, i) => i), color = "color" in material && material.color instanceof THREE.Color ? material.color : new THREE.Color("#999999");
      if (positions.some((n) => !Number.isFinite(n))) throw new Error("Coordonnées 3D non finies.");
      meshes.push({ positions, normals, faces, color: [color.r, color.g, color.b] }); cloned.dispose();
    };
    if (object instanceof THREE.Mesh) append(object.geometry, object.matrixWorld, Array.isArray(object.material) ? object.material[0] : object.material);
    else if (object instanceof THREE.Line) {
      const position = object.geometry.getAttribute("position"), index = object.geometry.index, count = index?.count ?? position.count, stride = object instanceof THREE.LineSegments ? 2 : 1;
      for (let i = 0; i < count - 1; i += stride) { const a = new THREE.Vector3().fromBufferAttribute(position, index?.getX(i) ?? i).applyMatrix4(object.matrixWorld), b = new THREE.Vector3().fromBufferAttribute(position, index?.getX(i + 1) ?? i + 1).applyMatrix4(object.matrixWorld), d = b.clone().sub(a); if (!d.length()) continue; const cylinder = new THREE.CylinderGeometry(.05, .05, d.length(), 4), matrix = new THREE.Matrix4().compose(a.clone().add(b).multiplyScalar(.5), new THREE.Quaternion().setFromUnitVectors(new THREE.Vector3(0, 1, 0), d.normalize()), new THREE.Vector3(1, 1, 1)); append(cylinder, matrix, Array.isArray(object.material) ? object.material[0] : object.material); cylinder.dispose(); }
    }
  });
  if (!meshes.length) throw new Error("Le modèle 3D ne contient aucune géométrie visible.");
  if (meshes.reduce((n, m) => n + m.faces.length / 3, 0) > 1_000_000) throw new Error("Le modèle dépasse un million de triangles. Réduisez sa complexité.");
  return meshes;
}
const triples = (data: number[]) => Array.from({ length: Math.floor(data.length / 3) }, (_, i) => data.slice(i * 3, i * 3 + 3).join(" ")).join("\n");
export function meshesIdtf(meshes: PdfMesh[]): string {
  const identity = "1 0 0 0\n0 1 0 0\n0 0 1 0\n0 0 0 1";
  return `FILE_FORMAT "IDTF"\nFORMAT_VERSION 100\n${meshes.map((_, i) => `NODE "MODEL" { NODE_NAME "Mesh${i}" PARENT_LIST { PARENT_COUNT 1 PARENT 0 { PARENT_NAME "<NULL>" PARENT_TM { ${identity} } } } RESOURCE_NAME "Mesh${i}" }`).join("\n")}
RESOURCE_LIST "MODEL" { RESOURCE_COUNT ${meshes.length}
${meshes.map((m, i) => `RESOURCE ${i} { RESOURCE_NAME "Mesh${i}" MODEL_TYPE "MESH" MESH { FACE_COUNT ${m.faces.length / 3} MODEL_POSITION_COUNT ${m.positions.length / 3} MODEL_NORMAL_COUNT ${m.normals.length / 3} MODEL_DIFFUSE_COLOR_COUNT 0 MODEL_SPECULAR_COLOR_COUNT 0 MODEL_TEXTURE_COORD_COUNT 0 MODEL_BONE_COUNT 0 MODEL_SHADING_COUNT 1 MODEL_SHADING_DESCRIPTION_LIST { SHADING_DESCRIPTION 0 { TEXTURE_LAYER_COUNT 0 SHADER_ID 0 } } MESH_FACE_POSITION_LIST { ${triples(m.faces)} } MESH_FACE_NORMAL_LIST { ${triples(m.faces)} } MESH_FACE_SHADING_LIST { ${Array.from({ length: m.faces.length / 3 }, () => "0").join("\n")} } MODEL_POSITION_LIST { ${triples(m.positions)} } MODEL_NORMAL_LIST { ${triples(m.normals)} } } }`).join("\n")}}
RESOURCE_LIST "SHADER" { RESOURCE_COUNT ${meshes.length} ${meshes.map((_, i) => `RESOURCE ${i} { RESOURCE_NAME "Shader${i}" SHADER_MATERIAL_NAME "Material${i}" SHADER_ACTIVE_TEXTURE_COUNT 0 }`).join("\n")} }
RESOURCE_LIST "MATERIAL" { RESOURCE_COUNT ${meshes.length} ${meshes.map((m, i) => `RESOURCE ${i} { RESOURCE_NAME "Material${i}" MATERIAL_AMBIENT 0.2 0.2 0.2 MATERIAL_DIFFUSE ${m.color.join(" ")} MATERIAL_SPECULAR 0.1 0.1 0.1 MATERIAL_EMISSIVE 0 0 0 MATERIAL_REFLECTIVITY 0 MATERIAL_OPACITY 1 }`).join("\n")} }
${meshes.map((_, i) => `MODIFIER "SHADING" { MODIFIER_NAME "Mesh${i}" PARAMETERS { SHADER_LIST_COUNT 1 SHADER_LIST_LIST { SHADER_LIST 0 { SHADER_COUNT 1 SHADER_NAME_LIST { SHADER 0 NAME: "Shader${i}" } } } } }`).join("\n")}`;
}
/** ISO 32000 3D annotation embeds a real U3D stream; no raster substitute. */
export function embedU3dPdf(u3d: Uint8Array, meshes: PdfMesh[]): Uint8Array {
  if (String.fromCharCode(...u3d.slice(0, 3)) !== "U3D") throw new Error("Le convertisseur n’a pas produit un flux U3D valide.");
  const bounds = new THREE.Box3(); for (const mesh of meshes) for (let i = 0; i < mesh.positions.length; i += 3) bounds.expandByPoint(new THREE.Vector3(...mesh.positions.slice(i, i + 3) as [number, number, number]));
  const center = bounds.getCenter(new THREE.Vector3()), radius = Math.max(1, bounds.getSize(new THREE.Vector3()).length());
  const encode = (s: string) => new TextEncoder().encode(s), chunks: Uint8Array[] = [], offsets = [0]; let length = 0;
  const add = (part: string | Uint8Array) => { const bytes = typeof part === "string" ? encode(part) : part; chunks.push(bytes); length += bytes.length; };
  const object = (id: number, text: string | Uint8Array[]) => { offsets[id] = length; add(`${id} 0 obj\n`); if (typeof text === "string") add(text); else text.forEach(add); add("\nendobj\n"); };
  add("%PDF-1.7\n%EMF3D\n");
  object(1, "<< /Type /Catalog /Pages 2 0 R >>"); object(2, "<< /Type /Pages /Kids [3 0 R] /Count 1 >>");
  object(3, "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 842 595] /Annots [4 0 R] /Resources << >> >>");
  object(4, "<< /Type /Annot /Subtype /3D /Rect [20 20 822 575] /Contents (Interactive EMF Planner model) /3DD 5 0 R /3DV 6 0 R /3DA << /A /PO /DIS /I /TB true /NP true >> /F 4 >>");
  object(5, [encode(`<< /Type /3D /Subtype /U3D /Length ${u3d.length} /VA [6 0 R] /DV 6 0 R >>\nstream\n`), u3d, encode("\nendstream")]);
  object(6, `<< /Type /3DView /XN (Home) /IN (Home) /MS /M /C2W [1 0 0 0 0.707107 0.707107 0 -0.707107 0.707107 ${center.x} ${center.y - radius} ${center.z + radius}] /CO ${radius * Math.SQRT2} /P << /Subtype /P /FOV 35 >> /LS << /Subtype /CAD >> /BG << /CS /DeviceRGB /C [1 1 1] >> >>`);
  const xref = length; add(`xref\n0 7\n0000000000 65535 f \n${offsets.slice(1).map((n) => `${String(n).padStart(10, "0")} 00000 n \n`).join("")}trailer\n<< /Size 7 /Root 1 0 R >>\nstartxref\n${xref}\n%%EOF\n`);
  const result = new Uint8Array(length); let at = 0; for (const chunk of chunks) { result.set(chunk, at); at += chunk.length; } return result;
}
