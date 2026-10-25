import { readFileSync, writeFileSync } from "node:fs";

function argument(text: string, cursor: number): [string, number] {
  while (/\s/.test(text[cursor] ?? "") && cursor < text.length) cursor++;
  if (text[cursor] !== "{") throw new Error(`Expected TeX argument at ${cursor}`);
  const start = ++cursor;
  let depth = 1;
  for (; cursor < text.length; cursor++) {
    if (text[cursor] === "\\") { cursor++; continue; }
    if (text[cursor] === "{") depth++;
    if (text[cursor] === "}" && --depth === 0) return [text.slice(start, cursor), cursor + 1];
  }
  throw new Error("Unclosed TeX argument");
}
const scenarios = ["tests_a.tex", "tests_b.tex", "tests_c.tex", "tests_d.tex"].flatMap(file => {
  const path = `test/emf_test_kit/guide/${file}`, text = readFileSync(path, "utf8"), rows = [];
  for (const match of text.matchAll(/\\T(?=\{)/g)) {
    let cursor = match.index! + 2;
    const values: string[] = [];
    for (let i = 0; i < 4; i++) { const [value, next] = argument(text, cursor); values.push(value); cursor = next; }
    rows.push({ id: values[0], priority: values[1], source: path, line: text.slice(0, match.index).split("\n").length,
      steps_tex: values[2], expected_tex: values[3] });
  }
  return rows;
});
if (scenarios.length !== 216 || new Set(scenarios.map(s => s.id)).size !== 216) throw new Error("The full guide must contain 216 distinct scenarios.");
writeFileSync("reference/test-kit-scenarios.json", JSON.stringify({ scope: "Full original guide text. No execution or success is inferred from this catalog.", total: scenarios.length, scenarios }, null, 2) + "\n");
console.log(JSON.stringify({ total: scenarios.length, groups: Object.fromEntries([...new Set(scenarios.map(s => s.id.split('-')[0]))].map(prefix => [prefix, scenarios.filter(s => s.id.startsWith(prefix + '-')).length])) }));
