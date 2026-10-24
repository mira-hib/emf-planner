import { readFileSync, writeFileSync } from "node:fs";

// This is a coverage index, never a declaration that a referenced scenario passed in full.
const guide = ["tests_a.tex", "tests_b.tex", "tests_c.tex", "tests_d.tex"];
const automated = ["tests/test-kit.test.ts", "tests/browser/test-kit.spec.ts"];
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
    "not observed in this session: Windows Computer Use native pipe unavailable",
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
