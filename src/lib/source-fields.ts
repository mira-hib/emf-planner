import type { RadiatingSource } from "../types";

/** Form1_Public.FreqSpanCheck: a single MHz value or an ordered pair. */
export function sourceFrequency(
  value: unknown,
): Pick<RadiatingSource, "frequency_mhz" | "frequency_end_mhz"> {
  const text = String(value).trim().replace(/,/g, ".");
  const match = text.match(
    /^([+]?\d+(?:\.\d+)?)\s*(?:[-–]\s*([+]?\d+(?:\.\d+)?))?$/,
  );
  if (!match)
    throw new Error(
      "Fréquence invalide : saisissez une valeur ou une bande, par exemple 900 ou 880-960 MHz.",
    );
  const first = Number(match[1]),
    last = match[2] ? Number(match[2]) : undefined;
  if (
    !Number.isFinite(first) ||
    first <= 0 ||
    first >= 1e8 ||
    (last !== undefined &&
      (!Number.isFinite(last) || Math.min(first, last) <= 30 || last >= 1e8))
  )
    throw new Error(
      "La fréquence doit être positive ; les bornes d’une bande doivent dépasser 30 MHz.",
    );
  return last === undefined
    ? { frequency_mhz: first, frequency_end_mhz: undefined }
    : {
        frequency_mhz: Math.min(first, last),
        frequency_end_mhz: Math.max(first, last),
      };
}
export function sourceFrequencyLabel(
  source: Pick<RadiatingSource, "frequency_mhz" | "frequency_end_mhz">,
) {
  return source.frequency_end_mhz
    ? `${source.frequency_mhz}-${source.frequency_end_mhz}`
    : String(source.frequency_mhz);
}

/** UC_PRTO.cmdFillWidthDN: crossings of the absolute 3 dB level, in Single precision. */
export function nativeBeamWidth(pattern: number[]): number {
  if (pattern.length !== 360 || pattern.some((v) => !Number.isFinite(v)))
    throw new Error("Le diagramme doit contenir 360 atténuations finies.");
  const p = pattern.map(Math.fround),
    f = Math.fround;
  const crossing = (i: number, next: number) =>
    f(i + f(f(3 - p[i]) / f(p[next] - p[i])));
  let a = 0,
    b = 0;
  if (p[359] <= 3 && p[0] >= 3) a = crossing(359, 0);
  else
    for (let i = 0; i < 359; i++) {
      if (p[i] <= 3 && p[i + 1] >= 3) {
        a = crossing(i, i + 1);
        break;
      }
    }
  if (p[0] <= 3 && p[359] >= 3) b = crossing(359, 0);
  else
    for (let i = 359; i > 0; i--) {
      if (p[i] <= 3 && p[i - 1] >= 3) {
        b = crossing(i - 1, i);
        break;
      }
    }
  const width = a < 180 && b > 180 ? f(a + f(360 - b)) : Math.abs(f(a - b));
  return width > 0 && width < 340 ? Number(width.toFixed(1)) : 360;
}

export function hasSourcePatterns(
  source?: RadiatingSource,
): source is RadiatingSource {
  return (
    !!source &&
    [source.pattern_h, source.pattern_v].every(
      (p) => p?.length === 360 && p.every(Number.isFinite),
    )
  );
}

export function fillSourceBeamWidths(source: RadiatingSource): RadiatingSource {
  return hasSourcePatterns(source)
    ? {
        ...source,
        beam_width_h_deg: nativeBeamWidth(source.pattern_h!),
        beam_width_v_deg: nativeBeamWidth(source.pattern_v!),
      }
    : source;
}
