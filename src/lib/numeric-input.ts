export function decimalInput(value: unknown): number {
  const text = String(value).trim().replace(/−/g, "-");
  if (!/^[+-]?(?:\d+(?:[.,]\d*)?|[.,]\d+)(?:[eE][+-]?\d+)?$/.test(text)) throw new Error("Saisissez un nombre décimal (point ou virgule).");
  const number = Number(text.replace(",", "."));
  if (!Number.isFinite(number)) throw new Error("Le nombre doit être fini.");
  return number;
}
