import { defineConfig } from "@playwright/test";
export default defineConfig({
  testDir: "./tests/browser",
  timeout: 60000,
  workers: 1,
  fullyParallel: false,
  use: {
    baseURL: "http://127.0.0.1:1420",
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
    command: "npm run dev -- --host 127.0.0.1",
    url: "http://127.0.0.1:1420",
    reuseExistingServer: true,
    timeout: 30000,
  },
});
