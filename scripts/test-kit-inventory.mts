import { readFileSync, readdirSync, writeFileSync } from "node:fs";

// This is a coverage index, never a declaration that a referenced scenario passed in full.
const guide = ["tests_a.tex", "tests_b.tex", "tests_c.tex", "tests_d.tex"];
const automated = [
  ...readdirSync("tests").filter(name=>/^test-kit.*\.test\.ts$/.test(name)).map(name=>`tests/${name}`),
  ...readdirSync("tests/browser").filter(name=>/^test-kit.*\.spec\.ts$/.test(name)).map(name=>`tests/browser/${name}`),
];
const contents = automated.map((file) => ({
  file,
  text: readFileSync(file, "utf8"),
}));
const cases = guide.flatMap((file) =>
  [
    ...readFileSync(`test/emf_test_kit/guide/${file}`, "utf8").matchAll(
      /\\T\{([A-Z]+-\d+)\}\{(P[123])\}/g,
    ),
  ].map((m) => {
    const files = contents
      .filter((entry) => entry.text.includes(m[1]))
      .map((entry) => entry.file);
    return {
      id: m[1],
      priority: m[2],
      guide: file,
      status: files.length
        ? "partial-automated-coverage"
        : "not-run-by-kit-suite",
      tests: files,
    };
  }),
);
const output = {
  scope:
    "Coverage references only. A test may cover a subset or record a native/kit divergence; consult the audit and actual test output.",
  original_ui:
    "Original 7.1 Trial observed on 2026-10-25 with Windows Computer Use: startup, Sources, Buildings, Vertical Cuts and Settings. See reference/full-kit-runtime-observations-2026-10-25.md. These observations are not complete kit execution.",
  total_scenarios: cases.length,
  partially_covered_scenarios: cases.filter((c) => c.tests.length).length,
  cases,
};
writeFileSync(
  "reference/test-kit-coverage.json",
  JSON.stringify(output, null, 2) + "\n",
);
console.log(
  JSON.stringify({
    total: output.total_scenarios,
    partial: output.partially_covered_scenarios,
    unexecuted: cases.filter((c) => !c.tests.length).length,
  }),
);
