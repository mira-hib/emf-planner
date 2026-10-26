// Data-only reader for the NRBF records used by .xemf. Never instantiates .NET
// types, invokes constructors or executes serialized code (unlike BinaryFormatter).
type Obj = Record<string, unknown>;
type Meta = { name: string; names: string[]; types: number[]; info: unknown[] };
type Ref = { $ref: number };
const SKIP = Symbol("skip"),
  END = Symbol("end");
export function readNrbf(buffer: ArrayBuffer) {
  if (buffer.byteLength > 64 * 1024 * 1024)
    throw new Error("Projet .xemf trop volumineux (64 Mo maximum).");
  const view = new DataView(buffer),
    bytes = new Uint8Array(buffer),
    decoder = new TextDecoder(),
    objects = new Map<number, unknown>(),
    metadata = new Map<number, Meta>();
  let pos = 0,
    rootId = 0,
    records = 0;
  const need = (n: number) => {
    if (n < 0 || pos + n > view.byteLength)
      throw new Error(`Fichier .xemf tronqué à l’octet ${pos}.`);
  };
  const u8 = () => {
    need(1);
    return view.getUint8(pos++);
  };
  const i32 = () => {
    need(4);
    const v = view.getInt32(pos, true);
    pos += 4;
    return v;
  };
  const f64 = () => {
    need(8);
    const v = view.getFloat64(pos, true);
    pos += 8;
    return v;
  };
  const count = (n: number) => {
    if (n < 0 || n > 2_000_000)
      throw new Error("Taille d’objet .xemf invalide.");
    return n;
  };
  function string() {
    let n = 0,
      shift = 0,
      b;
    do {
      b = u8();
      n += (b & 127) * 2 ** shift;
      shift += 7;
      if (shift > 35) throw new Error("Chaîne .xemf invalide.");
    } while (b & 128);
    need(n);
    const s = decoder.decode(bytes.subarray(pos, pos + n));
    pos += n;
    return s;
  }
  function primitive(type: number): unknown {
    switch (type) {
      case 1:
        return u8() !== 0;
      case 2:
        return u8();
      case 3: {
        const start = pos,
          b = u8(),
          n = b < 128 ? 1 : b < 224 ? 2 : b < 240 ? 3 : 4;
        need(n - 1);
        pos += n - 1;
        return decoder.decode(bytes.subarray(start, pos));
      }
      case 5:
        return Number(string());
      case 6:
        return f64();
      case 7:
        need(2);
        {
          const v = view.getInt16(pos, true);
          pos += 2;
          return v;
        }
      case 8:
        return i32();
      case 9:
      case 12:
      case 13:
        need(8);
        {
          const v = Number(view.getBigInt64(pos, true));
          pos += 8;
          return v;
        }
      case 10:
        need(1);
        return view.getInt8(pos++);
      case 11:
        need(4);
        {
          const v = view.getFloat32(pos, true);
          pos += 4;
          return v;
        }
      case 14:
        need(2);
        {
          const v = view.getUint16(pos, true);
          pos += 2;
          return v;
        }
      case 15:
        return i32() >>> 0;
      case 16:
        need(8);
        {
          const v = Number(view.getBigUint64(pos, true));
          pos += 8;
          return v;
        }
      case 17:
        return null;
      case 18:
        return string();
      default:
        throw new Error(`Type primitif .xemf non pris en charge : ${type}`);
    }
  }
  function additional(type: number): unknown {
    if (type === 0 || type === 7) return u8();
    if (type === 3) return string();
    if (type === 4) {
      const name = string();
      i32();
      return name;
    }
    return null;
  }
  function value(): unknown {
    let v;
    do {
      v = record();
    } while (v === SKIP);
    if (v === END) throw new Error("Fin .xemf inattendue.");
    return v;
  }
  function values(length: number, type: number, info: unknown) {
    const result: unknown[] = [];
    for (let i = 0; i < length; i++) {
      if (type === 0) result.push(primitive(info as number));
      else {
        const v = value();
        if (v && typeof v === "object" && "$nulls" in v) {
          const n = count((v as { $nulls: number }).$nulls);
          if (i + n > length) throw new Error("Séquence nulle .xemf invalide.");
          for (let j = 0; j < n; j++) result.push(null);
          i += n - 1;
        } else result.push(v);
      }
    }
    return result;
  }
  function members(id: number, meta: Meta) {
    const result: Obj = { __type: meta.name };
    objects.set(id, result);
    for (let i = 0; i < meta.names.length; i++)
      result[meta.names[i]] =
        meta.types[i] === 0 ? primitive(meta.info[i] as number) : value();
    return result;
  }
  function record(): unknown {
    if (++records > 3_000_000)
      throw new Error("Trop d’objets dans le fichier .xemf.");
    const type = u8();
    switch (type) {
      case 0:
        rootId = i32();
        i32();
        {
          const major = i32(),
            minor = i32();
          if (major !== 1 || minor !== 0)
            throw new Error("Version .xemf inconnue.");
        }
        return SKIP;
      case 1: {
        const id = i32(),
          meta = metadata.get(i32());
        if (!meta) throw new Error("Métadonnées .xemf manquantes.");
        return members(id, meta);
      }
      case 2:
      case 3:
      case 4:
      case 5: {
        const id = i32(),
          name = string(),
          n = count(i32()),
          names = Array.from({ length: n }, string);
        const types =
          type >= 4 ? Array.from({ length: n }, u8) : Array(n).fill(2);
        const info = types.map(additional);
        if (type === 3 || type === 5) i32();
        const meta = { name, names, types, info };
        metadata.set(id, meta);
        return members(id, meta);
      }
      case 6: {
        const id = i32(),
          s = string();
        objects.set(id, s);
        return s;
      }
      case 7: {
        const id = i32(),
          kind = u8(),
          rank = count(i32());
        if (rank > 4) throw new Error("Rang de tableau .xemf invalide.");
        const lengths = Array.from({ length: rank }, () => count(i32()));
        if (kind >= 3) for (let i = 0; i < rank; i++) i32();
        const t = u8(),
          info = additional(t),
          n = count(lengths.reduce((a, b) => a * b, 1));
        const array: unknown[] = values(n, t, info);
        objects.set(id, array);
        return array;
      }
      case 8:
        return primitive(u8());
      case 9:
        return { $ref: i32() };
      case 10:
        return null;
      case 11:
        return END;
      case 12:
        i32();
        string();
        return SKIP;
      case 13:
        return { $nulls: u8() };
      case 14:
        return { $nulls: i32() };
      case 15:
      case 16:
      case 17: {
        const id = i32(),
          rawLength = i32(),
          t = type === 15 ? u8() : null;
        if(t === 2) {
          need(rawLength);
          const a=bytes.slice(pos,pos+rawLength);pos+=rawLength;
          objects.set(id,a);return a;
        }
        const n=count(rawLength);
        const a = values(n, t === null ? 2 : 0, t);
        objects.set(id, a);
        return a;
      }
      default:
        throw new Error(
          `Enregistrement .xemf non pris en charge : ${type}, octet ${pos - 1}.`,
        );
    }
  }
  if (bytes[0] !== 0)
    throw new Error("Ce fichier n’est pas un projet .xemf binaire.");
  while (pos < buffer.byteLength) {
    if (record() === END) break;
  }
  const deref = (v: unknown): unknown => {
    let n = 0;
    while (v && typeof v === "object" && "$ref" in v) {
      if (++n > 100) throw new Error("Référence .xemf cyclique.");
      v = objects.get((v as Ref).$ref);
    }
    return v;
  };
  const root = deref(objects.get(rootId));
  if (
    !root ||
    typeof root !== "object" ||
    (root as Obj).__type !== "EMFPlanner.Public2+Project"
  )
    throw new Error("Le fichier ne contient pas un projet EMFPlanner reconnu.");
  return { root: root as Obj, deref, objects };
}
