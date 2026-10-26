/** Read dimensions without asking an image decoder to allocate pixel storage. */
export function rasterDimensions(bytes: Uint8Array): [number, number] | null {
  const view = new DataView(bytes.buffer, bytes.byteOffset, bytes.byteLength);
  if (bytes.length >= 24 && view.getUint32(0) === 0x89504e47 && view.getUint32(12) === 0x49484452)
    return [view.getUint32(16), view.getUint32(20)];
  if (bytes.length >= 10 && String.fromCharCode(...bytes.slice(0, 3)) === "GIF") return [view.getUint16(6, true), view.getUint16(8, true)];
  if (bytes.length >= 26 && bytes[0] === 0x42 && bytes[1] === 0x4d) {
    if (view.getUint32(14, true) === 12) return [view.getUint16(18, true), view.getUint16(20, true)];
    return [Math.abs(view.getInt32(18, true)), Math.abs(view.getInt32(22, true))];
  }
  if (bytes[0] === 0xff && bytes[1] === 0xd8) {
    let cursor = 2;
    while (cursor + 4 <= bytes.length) {
      if (bytes[cursor] !== 0xff) throw new Error("En-tête JPEG invalide.");
      while (bytes[cursor] === 0xff) cursor++;
      const marker = bytes[cursor++];
      if (marker === 0xd9 || marker === 0xda) break;
      if (marker === 0x01 || marker >= 0xd0 && marker <= 0xd7) continue;
      if (cursor + 2 > bytes.length) break;
      const length = view.getUint16(cursor);
      if (length < 2 || cursor + length > bytes.length) throw new Error("En-tête JPEG tronqué.");
      if ([0xc0, 0xc1, 0xc2, 0xc3, 0xc5, 0xc6, 0xc7, 0xc9, 0xca, 0xcb, 0xcd, 0xce, 0xcf].includes(marker)) {
        if (length < 7) throw new Error("Dimensions JPEG absentes.");
        return [view.getUint16(cursor + 5), view.getUint16(cursor + 3)];
      }
      cursor += length;
    }
  }
  if (bytes.length >= 30 && String.fromCharCode(...bytes.slice(0, 4)) === "RIFF" && String.fromCharCode(...bytes.slice(12, 16)) === "VP8X")
    return [1 + bytes[24] + bytes[25] * 256 + bytes[26] * 65536, 1 + bytes[27] + bytes[28] * 256 + bytes[29] * 65536];
  return null;
}
export function validateRasterDimensions(bytes: Uint8Array) {
  const dimensions = rasterDimensions(bytes);
  if (dimensions && (dimensions[0] < 1 || dimensions[1] < 1 || dimensions[0] * dimensions[1] > 100_000_000))
    throw new Error("Dimensions d’image invalides ou supérieures à 100 millions de pixels.");
  return dimensions;
}
