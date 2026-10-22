import type { PlannerProject, StoredAntenna } from "../types";
import { COLORS, uid } from "./project";
import { rad, toGeo, toLocal } from "./geometry";

export function rangeValues(
  start: number,
  end: number,
  step: number,
  maxCount = 1000,
): number[] {
  if (![start, end, step].every(Number.isFinite) || step <= 0 || end < start)
    throw new Error(
      "La borne finale doit être supérieure ou égale à la borne initiale, et le pas strictement positif.",
    );
  const count = Math.floor((end - start) / step + 1e-8) + 1;
  if (count > maxCount)
    throw new Error(
      `La plage est limitée à ${maxCount} valeurs. Augmentez le pas.`,
    );
  return [
    ...new Set(
      Array.from(
        { length: count },
        (_, i) => Math.round((start + step * i) * 10) / 10,
      ),
    ),
  ];
}
export function addHorizontalHeights(
  p: PlannerProject,
  heights: number[],
): PlannerProject {
  const cuts = [...p.horizontal_cuts];
  for (const h of heights) {
    const height_m = Math.round(h * 10) / 10;
    if (
      Number.isFinite(h) &&
      h >= 0 &&
      !cuts.some((c) => Math.abs(c.height_m - height_m) < 0.05)
    )
      cuts.push({
        id: uid(),
        active: true,
        height_m,
        color: COLORS[cuts.length % COLORS.length],
      });
  }
  return {
    ...p,
    horizontal_cuts: cuts.sort((a, b) => a.height_m - b.height_m),
  };
}
export function addVerticalAngles(
  p: PlannerProject,
  angles: number[],
): PlannerProject {
  const cuts = [...p.vertical_cuts];
  for (const angle of angles) {
    if (angle === 360) continue;
    const azimuth_deg = ((angle % 360) + 360) % 360;
    if (
      !cuts.some((c) => c.x === 0 && c.y === 0 && c.azimuth_deg === azimuth_deg)
    )
      cuts.push({
        id: uid(),
        active: true,
        azimuth_deg,
        x: 0,
        y: 0,
        backward_m: 0,
        forward_m: 150,
        max_height_m: 100,
      });
  }
  return {
    ...p,
    vertical_cuts: cuts.sort((a, b) => a.azimuth_deg - b.azimuth_deg),
  };
}
export function alignPointsToAzimuth(
  p: PlannerProject,
  tolerance: number,
  group?: string,
): PlannerProject {
  if (!Number.isFinite(tolerance) || tolerance <= 0)
    throw new Error("Distance maximale invalide.");
  return {
    ...p,
    control_points: p.control_points.map((point) => {
      if (group && point.group !== group) return point;
      let best = tolerance,
        target: [number, number] | undefined;
      for (const s of p.sources) {
        if (s.active === false) continue;
        const position = toLocal(s.lat, s.lng, p.origin),
          x = s.x_m ?? position[0],
          y = s.y_m ?? position[1];
        const dx = Math.sin(rad(s.azimuth_deg < 0 ? 0 : s.azimuth_deg)),
          dy = Math.cos(rad(s.azimuth_deg < 0 ? 0 : s.azimuth_deg));
        const along = (point.x_m - x) * dx + (point.y_m - y) * dy;
        const distance = Math.abs((point.x_m - x) * dy - (point.y_m - y) * dx);
        if (along > 0 && along < 1000 && distance < best) {
          best = distance;
          target = [x + along * dx, y + along * dy];
        }
      }
      if (!target) return point;
      const [lat, lng] = toGeo(...target, p.origin);
      return { ...point, x_m: target[0], y_m: target[1], lat, lng };
    }),
  };
}
export function reorderGroup(
  p: PlannerProject,
  group: string,
  direction: number,
): PlannerProject {
  const groups = [
      ...new Set(p.control_points.map((c) => c.group || "Groupe 1")),
    ],
    index = groups.indexOf(group),
    next = index + direction;
  if (index < 0 || next < 0 || next >= groups.length) return p;
  [groups[index], groups[next]] = [groups[next], groups[index]];
  return {
    ...p,
    control_points: [...p.control_points].sort(
      (a, b) =>
        groups.indexOf(a.group || "Groupe 1") -
        groups.indexOf(b.group || "Groupe 1"),
    ),
  };
}
export function symmetricPattern(
  frequency: number,
  gain: number,
  diameter: number,
): StoredAntenna {
  if (
    !Number.isFinite(frequency + gain + diameter) ||
    frequency < 1 ||
    frequency > 70000 ||
    gain < 0 ||
    gain > 60 ||
    diameter < 0.1 ||
    diameter > 20
  )
    throw new Error(
      "Fréquence : 1 à 70 000 MHz ; gain : 0 à 60 dBi ; diamètre : 0,1 à 20 m.",
    );
  // Port of frmParabola.Gq in the recovered original, rather than a current compliance profile.
  const lambda = 300 / frequency,
    ratio = diameter / lambda,
    g1 = 2 + 15 * Math.log10(ratio);
  if (gain < g1)
    throw new Error(
      "Le gain est incompatible avec ce diamètre et cette fréquence.",
    );
  const phiM = (20 * lambda * Math.sqrt(gain - g1)) / diameter,
    phiR = 15.85 * ratio ** -0.6;
  const values = Array.from({ length: 360 }, (_, index) => {
    const angle = Math.min(index, 360 - index);
    let g: number;
    if (angle < phiM) g = -0.0025 * (ratio * angle) ** 2;
    else if (ratio > 100)
      g =
        angle < phiR
          ? g1 - gain
          : angle < 48
            ? 32 - 25 * Math.log10(angle) - gain
            : -10 - gain;
    else
      g =
        angle < 100 / ratio
          ? g1 - gain
          : angle < 48
            ? 52 - 10 * Math.log10(ratio) - 25 * Math.log10(angle) - gain
            : 10 - 10 * Math.log10(ratio) - gain;
    return Math.round(Math.abs(g) * 10) / 10;
  });
  return {
    id: uid(),
    name: `${frequency / 1000} GHz ${diameter} m`,
    frequency,
    gain,
    horizontal: values,
    vertical: [...values],
  };
}
export function envelopePatterns(
  patterns: StoredAntenna[],
  name: string,
): StoredAntenna {
  if (!patterns.length) throw new Error("Sélectionnez au moins un diagramme.");
  const gain = Math.max(...patterns.map((p) => p.gain));
  const combine = (plane: "horizontal" | "vertical") =>
    Array.from(
      { length: 360 },
      (_, i) => gain - Math.max(...patterns.map((p) => p.gain - p[plane][i])),
    );
  return {
    id: uid(),
    name,
    gain,
    horizontal: combine("horizontal"),
    vertical: combine("vertical"),
  };
}
/** Native frmEnvelope: normalize each plane, optionally use gains, keep TX.G. */
export function envelopeForSource(
  patterns: StoredAntenna[],
  name: string,
  sourceGain: number,
  useGain = false,
): StoredAntenna {
  if (!patterns.length) throw new Error("Sélectionnez au moins un diagramme.");
  if (!Number.isFinite(sourceGain)) throw new Error("Gain de source invalide.");
  for (const pattern of patterns) {
    if (!Number.isFinite(pattern.gain))
      throw new Error("Gain de diagramme invalide.");
    for (const plane of [pattern.horizontal, pattern.vertical])
      if (
        plane.length !== 360 ||
        plane.some((value) => !Number.isFinite(value) || value < 0)
      )
        throw new Error(
          "Chaque plan doit contenir 360 atténuations positives ou nulles.",
        );
  }
  const combine = (plane: "horizontal" | "vertical") => {
    const normalized = patterns.map((pattern) => {
      const minimum = Math.min(...pattern[plane]);
      return pattern[plane].map(
        (loss) => (useGain ? pattern.gain : 0) - loss + minimum,
      );
    });
    // The native buffer starts at -60 dB before taking the directional maximum.
    const envelope = Array.from({ length: 360 }, (_, angle) =>
      Math.max(-60, ...normalized.map((values) => values[angle])),
    );
    const peak = Math.max(...envelope);
    return envelope.map((value) => peak - value);
  };
  return {
    id: uid(),
    name,
    gain: sourceGain,
    horizontal: combine("horizontal"),
    vertical: combine("vertical"),
  };
}
export function beamWidth(values: number[]): number {
  const peak = values.indexOf(Math.min(...values)),
    threshold = values[peak] + 3;
  const side = (sign: number) => {
    for (let i = 1; i <= 180; i++) {
      const next = values[(peak + sign * i + 720) % 360],
        prev = values[(peak + sign * (i - 1) + 720) % 360];
      if (next >= threshold) return i - 1 + (threshold - prev) / (next - prev);
    }
    return 180;
  };
  return Math.round((side(-1) + side(1)) * 10) / 10;
}
export function feederAttenuation(
  points: { frequency: number; loss: number }[],
  frequency: number,
): number {
  if (
    !Number.isFinite(frequency) ||
    !points.length ||
    frequency < points[0].frequency ||
    frequency > points[points.length - 1].frequency
  )
    throw new Error("Fréquence hors de la plage documentée pour ce câble.");
  const index = points.findIndex((p) => p.frequency >= frequency);
  if (!index) return points[0].loss;
  if (points[index].frequency === frequency) return points[index].loss;
  const a = points[index - 1],
    b = points[index];
  return (
    a.loss +
    ((b.loss - a.loss) * (Math.log10(frequency) - Math.log10(a.frequency))) /
      (Math.log10(b.frequency) - Math.log10(a.frequency))
  );
}
export function feederLoss(
  cablePoints: { frequency: number; loss: number }[],
  frequency: number,
  length: number,
  jumperPoints: { frequency: number; loss: number }[],
  jumpers: string,
  connectors: number,
  connectorLoss: number,
  otherLoss: number,
  automatic = true,
  includeConnectors = true,
) {
  if (
    [
      length,
      otherLoss,
      ...(includeConnectors ? [connectors, connectorLoss] : []),
    ].some((v) => !Number.isFinite(v) || v < 0)
  )
    throw new Error(
      "Les longueurs et les pertes doivent être positives ou nulles.",
    );
  const lengths = jumpers.split("+").map((s) => {
    if (!s.trim()) throw new Error("Longueurs des cavaliers invalides.");
    return Number(s.trim().replace(",", "."));
  });
  if (lengths.some((v) => !Number.isFinite(v) || v < 0))
    throw new Error("Saisissez les cavaliers sous la forme 3+3, en mètres.");
  const round = (v: number) => Math.round(v * 1000) / 1000;
  const cablePer100 = round(feederAttenuation(cablePoints, frequency)),
    jumperPer100 = round(feederAttenuation(jumperPoints, frequency));
  const cableLoss = round((cablePer100 * length) / 100),
    jumperLoss = round(
      (jumperPer100 * lengths.reduce((s, n) => s + n, 0)) / 100,
    );
  const count = !includeConnectors
    ? 0
    : automatic
      ? 2 + (lengths.length === 1 && lengths[0] === 0 ? 0 : lengths.length * 2)
      : connectors;
  return {
    cablePer100,
    jumperPer100,
    cableLoss,
    jumperLoss,
    connectors: count,
    total: round(
      cableLoss +
        jumperLoss +
        (includeConnectors ? count * connectorLoss : 0) +
        otherLoss,
    ),
  };
}
