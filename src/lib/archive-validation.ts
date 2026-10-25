/** Check ZIP central-directory sizes before any library inflates entry data. */
export function validateArchive(bytes: Uint8Array, maxTotal = 128 * 1024 * 1024) {
  if (bytes.length > 64 * 1024 * 1024 || bytes.length < 22) throw new Error("Archive invalide ou supérieure à 64 Mo.");
  const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength);
  let end = -1;
  for (let i = bytes.length - 22; i >= Math.max(0, bytes.length - 65557); i--)
    if (view.getUint32(i, true) === 0x06054b50 && i + 22 + view.getUint16(i + 20, true) === bytes.length) { end = i; break; }
  if (end < 0 || view.getUint16(end + 4, true) !== 0 || view.getUint16(end + 6, true) !== 0) throw new Error("Archive ZIP tronquée ou répartie sur plusieurs volumes.");
  const count = view.getUint16(end + 10, true), size = view.getUint32(end + 12, true), offset = view.getUint32(end + 16, true);
  if (count > 10000 || offset + size > end) throw new Error("Archive trop complexe ou répertoire invalide.");
  let cursor = offset, total = 0;
  for (let i = 0; i < count; i++) {
    if (cursor + 46 > end || view.getUint32(cursor, true) !== 0x02014b50) throw new Error("Répertoire ZIP invalide.");
    const compressed = view.getUint32(cursor + 20, true), uncompressed = view.getUint32(cursor + 24, true);
    const length = 46 + view.getUint16(cursor + 28, true) + view.getUint16(cursor + 30, true) + view.getUint16(cursor + 32, true);
    total += uncompressed;
    if ((view.getUint16(cursor + 8, true) & 1) || compressed === 0xffffffff || uncompressed > 32 * 1024 * 1024 || total > maxTotal)
      throw new Error("Archive chiffrée, ZIP64 ou trop volumineuse après décompression.");
    if (cursor + length > offset + size) throw new Error("Entrée ZIP tronquée.");
    cursor += length;
  }
  if (cursor !== offset + size) throw new Error("Répertoire ZIP incohérent.");
  return { entries: count, uncompressedBytes: total };
}
