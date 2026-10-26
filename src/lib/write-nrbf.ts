// Data-only NRBF writer. The schema is limited to EMFPlanner's project records;
// the application never invokes BinaryFormatter or instantiates serialized types.
import schema from "./xemf-schema.json" with { type: "json" };

export type NativeRecord = Record<string, unknown>;
const nativeAssembly =
  "EMFPlanner, Version=7.0.0.0, Culture=neutral, PublicKeyToken=null";
const drawingAssembly =
  "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
const listName = `System.Collections.Generic.List\`1[[EMFPlanner.Public2+LimitPoint, ${nativeAssembly}]]`;
const primitives: Record<string, number> = {
  bool: 1,
  byte: 2,
  double: 6,
  short: 7,
  int: 8,
  long: 9,
  float: 11,
};
const layouts: Record<string, string[][]> = {
  ...schema,
  Color: [
    ["string", "name"],
    ["long", "value"],
    ["short", "knownColor"],
    ["short", "state"],
  ],
  Bitmap: [["byte[]", "Data"]],
  "List<LimitPoint>": [
    ["LimitPoint[]", "_items"],
    ["int", "_size"],
    ["int", "_version"],
  ],
};

export function writeNativeRecords(root: NativeRecord): Uint8Array {
  let data = new Uint8Array(65536),
    pos = 0,
    nextId = 1;
  const encoder = new TextEncoder(),
    metadata = new Map<string, number>();
  const pending: { type: string; value: unknown; id: number }[] = [];
  const ensure = (n: number) => {
    if (pos + n > 64 * 1024 * 1024)
      throw new Error("Projet .xemf trop volumineux (64 Mo maximum).");
    if (pos + n <= data.length) return;
    const grown = new Uint8Array(
      Math.max(pos + n, Math.min(64 * 1024 * 1024, data.length * 2)),
    );
    grown.set(data);
    data = grown;
  };
  const u8 = (n: number) => {
    ensure(1);
    data[pos++] = n;
  };
  const i32 = (n: number) => {
    ensure(4);
    new DataView(data.buffer).setInt32(pos, n, true);
    pos += 4;
  };
  const string = (s: string) => {
    const bytes = encoder.encode(s);
    let n = bytes.length;
    while (n >= 128) {
      u8((n & 127) | 128);
      n >>>= 7;
    }
    u8(n);
    ensure(bytes.length);
    data.set(bytes, pos);
    pos += bytes.length;
  };
  function primitive(type: string, value: unknown) {
    const n = Number(value ?? 0);
    if (!Number.isFinite(n))
      throw new Error(`Valeur .xemf invalide : ${type}.`);
    const size = {
      bool: 1,
      byte: 1,
      short: 2,
      int: 4,
      float: 4,
      double: 8,
      long: 8,
    }[type]!;
    if (["byte", "short", "int"].includes(type)) {
      const [min, max] = {
        byte: [0, 255],
        short: [-32768, 32767],
        int: [-2147483648, 2147483647],
      }[type]!;
      if (!Number.isInteger(n) || n < min || n > max)
        throw new Error(`Valeur hors limites .xemf (${type}) : ${n}.`);
    }
    ensure(size);
    const view = new DataView(data.buffer);
    if (type === "bool") view.setUint8(pos, value ? 1 : 0);
    if (type === "byte") view.setUint8(pos, n);
    if (type === "short") view.setInt16(pos, n, true);
    if (type === "int") view.setInt32(pos, n, true);
    if (type === "float") view.setFloat32(pos, n, true);
    if (type === "double") view.setFloat64(pos, n, true);
    if (type === "long") view.setBigInt64(pos, BigInt(Math.trunc(n)), true);
    pos += size;
  }
  const fullName = (t: string): string => {
    if (t.endsWith("[]")) return fullName(t.slice(0, -2)) + "[]";
    return t === "Color" || t === "Bitmap"
      ? `System.Drawing.${t}`
      : t === "List<LimitPoint>"
        ? listName
        : `EMFPlanner.Public2+${t}`;
  };
  const binaryType = (t: string) =>
    primitives[t]
      ? 0
      : t === "string"
        ? 1
        : t.endsWith("[]") && primitives[t.slice(0, -2)]
          ? 7
          : t === "string[]"
            ? 6
            : t === "List<LimitPoint>"
              ? 3
              : 4;
  function additional(t: string) {
    const code = binaryType(t);
    if (code === 0) u8(primitives[t]);
    if (code === 7) u8(primitives[t.slice(0, -2)]);
    if (code === 3 || code === 4) string(fullName(t));
    if (code === 4)
      i32(t.replace(/\[.*$/, "") === "Color" || t === "Bitmap" ? 2 : 1);
  }
  function value(type: string, v: unknown, referencedId?: number) {
    if (primitives[type]) {
      primitive(type, v);
      return;
    }
    if (v === null) {
      u8(10);
      return;
    }
    if (type === "string") {
      u8(6);
      i32(nextId++);
      string(String(v ?? ""));
      return;
    }
    if (
      referencedId === undefined &&
      (type.endsWith("[]") ||
        type.endsWith("[,]") ||
        type === "List<LimitPoint>" ||
        type === "Bitmap")
    ) {
      const id = nextId++;
      u8(9);
      i32(id);
      pending.push({ type, value: v, id });
      return;
    }
    if (type.endsWith("[]") || type.endsWith("[,]")) {
      const element = type.slice(0, type.endsWith("[,]") ? -3 : -2);
      const array = Array.isArray(v) || v instanceof Uint8Array ? v : [];
      if (type.endsWith("[,]")) {
        const rows = array as unknown[][],
          width = rows[0]?.length ?? 2;
        u8(7);
        i32(referencedId!);
        u8(2);
        i32(2);
        i32(rows.length);
        i32(width);
        u8(binaryType(element));
        additional(element);
        for (const row of rows)
          for (let i = 0; i < width; i++) value(element, row[i]);
      } else if (primitives[element]) {
        u8(15);
        i32(referencedId!);
        i32(array.length);
        u8(primitives[element]);
        if (element === "byte" && array instanceof Uint8Array) {
          ensure(array.length);
          data.set(array, pos);
          pos += array.length;
        } else for (const item of array) primitive(element, item);
      } else {
        u8(7);
        i32(referencedId!);
        u8(element.endsWith("[]") ? 1 : 0);
        i32(1);
        i32(array.length);
        u8(binaryType(element));
        additional(element);
        for (const item of array) value(element, item);
      }
      return;
    }
    const fields = layouts[type];
    if (!fields) throw new Error(`Structure .xemf inconnue : ${type}`);
    const id = referencedId ?? (type === "Project" ? nextId++ : -nextId++),
      meta = metadata.get(type),
      record = (v ?? {}) as NativeRecord;
    if (meta !== undefined) {
      u8(1);
      i32(id);
      i32(meta);
    } else {
      metadata.set(type, id);
      u8(type === "List<LimitPoint>" ? 4 : 5);
      i32(id);
      string(fullName(type));
      i32(fields.length);
      for (const [, name] of fields) string(name);
      for (const [t] of fields) u8(t.endsWith("[,]") ? 4 : binaryType(t));
      for (const [t] of fields) {
        if (t.endsWith("[,]")) {
          string("System.String[,]");
          i32(3);
        } else additional(t);
      }
      if (type !== "List<LimitPoint>")
        i32(type === "Color" || type === "Bitmap" ? 2 : 1);
    }
    for (const [t, name] of fields) value(t, record[name]);
  }
  u8(0);
  i32(1);
  i32(-1);
  i32(1);
  i32(0);
  for (const [id, name] of [
    [1, nativeAssembly],
    [2, drawingAssembly],
    [
      3,
      "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
    ],
  ] as const) {
    u8(12);
    i32(id);
    string(name);
  }
  value("Project", root);
  for (let i = 0; i < pending.length; i++) {
    const item = pending[i];
    value(item.type, item.value, item.id);
  }
  u8(11);
  return data.slice(0, pos);
}
