import { defineConfig } from "@playwright/test";
const port = process.env.PLAYWRIGHT_PORT ?? (process.env.PLAYWRIGHT_STATIC ? "1431" : "1420");
const address = `http://127.0.0.1:${port}`;
export default defineConfig({
  testDir: "./tests/browser",
  timeout: 60000,
  workers: 1,
  fullyParallel: false,
  use: {
    baseURL: address,
    channel:
      process.env.PLAYWRIGHT_CHANNEL ||
      (process.platform === "win32" ? "msedge" : undefined),
    headless: true,
    viewport: { width: 1480, height: 960 },
    trace: "retain-on-failure",
    screenshot: "only-on-failure",
    launchOptions: { args: ["--use-angle=swiftshader"] },
  },
  outputDir: ".tools/test-results",
  reporter: [["list"]],
  webServer: {
    command: process.env.PLAYWRIGHT_STATIC
      ? `npm run preview -- --outDir "${process.env.PLAYWRIGHT_DIST ?? ".tools/kit-preview-dist"}" --host 127.0.0.1 --port ${port} --strictPort`
      : `npm run dev -- --host 127.0.0.1 --port ${port} --strictPort`,
    url: address,
    reuseExistingServer: false,
    timeout: 30000,
  },
});
