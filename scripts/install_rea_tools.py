"""Install the Windows EMF analysis toolchain into the ignored .tools directory.

Uses upstream release assets; verifies publisher digests where available.
Does not change system installations, PATH, security settings or agent config.
"""
import hashlib
import json
import os
from pathlib import Path
import re
import subprocess
import sys
import time
import urllib.request
import zipfile

ROOT = Path(__file__).resolve().parents[1]
TOOLS = ROOT / ".tools"
DOWNLOADS = TOOLS / "downloads"
MANIFEST = TOOLS / "manifest.json"
HEADERS = {"User-Agent": "emf-planner-local-toolchain"}


def fetch(url):
    with urllib.request.urlopen(urllib.request.Request(url, headers=HEADERS), timeout=90) as response:
        return response.read()


def get_json(url):
    return json.loads(fetch(url))


def asset(repo, pattern, tag=None):
    endpoint = "tags/" + tag if tag else "latest"
    release = get_json(f"https://api.github.com/repos/{repo}/releases/{endpoint}")
    matches = [item for item in release["assets"] if re.search(pattern, item["name"], re.I)]
    if len(matches) != 1:
        raise RuntimeError(f"Expected one asset for {repo}: {[a['name'] for a in matches]}")
    item = matches[0]
    return {"version": release["tag_name"], "url": item["browser_download_url"],
            "name": item["name"], "digest": item.get("digest"), "size": item["size"]}


def download(item):
    destination = DOWNLOADS / item["name"]
    DOWNLOADS.mkdir(parents=True, exist_ok=True)
    if not destination.exists():
        partial = destination.with_suffix(destination.suffix + ".partial")
        print(f"Download {item['name']} ({item.get('size', 0) / 1048576:.1f} MiB)", flush=True)
        for attempt in range(3):
            try:
                subprocess.run([
                    "curl.exe", "--fail", "--location", "--silent", "--show-error",
                    "--connect-timeout", "30", "--speed-limit", "1024", "--speed-time", "60",
                    "--output", str(partial), item["url"],
                ], check=True)
                break
            except Exception as error:
                print(f"Download attempt {attempt + 1} failed: {error}", flush=True)
                if attempt == 2:
                    raise
                time.sleep(2)
        partial.replace(destination)
    if item.get("size") and destination.stat().st_size != item["size"]:
        raise RuntimeError(f"Unexpected download size: {destination}")
    digest = item.get("digest")
    algorithm = digest.split(":", 1)[0] if digest else "sha256"
    hasher = hashlib.new(algorithm)
    with destination.open("rb") as source:
        for block in iter(lambda: source.read(1024 * 1024), b""):
            hasher.update(block)
    actual = hasher.hexdigest()
    if digest and actual.lower() != digest.split(":", 1)[1].lower():
        raise RuntimeError(f"Publisher checksum mismatch: {destination}")
    item["verified_digest"] = f"{algorithm}:{actual}"
    item["publisher_digest_available"] = bool(digest)
    return destination


def install_zip(key, item):
    archive = download(item)
    target = TOOLS / key
    stamp = target / ".installed.json"
    if stamp.exists() and json.loads(stamp.read_text())["verified_digest"] == item["verified_digest"]:
        print(f"Already installed: {key} {item['version']}", flush=True)
        return target
    target.mkdir(parents=True, exist_ok=True)
    # Validate every member before extracting into this fixed local destination.
    with zipfile.ZipFile(archive) as bundle:
        for member in bundle.infolist():
            resolved = (target / member.filename).resolve()
            if not resolved.is_relative_to(target.resolve()):
                raise RuntimeError(f"Unsafe archive path: {member.filename}")
        bundle.extractall(target)
    stamp.write_text(json.dumps(item, indent=2), encoding="utf-8")
    print(f"Installed: {key} {item['version']}", flush=True)
    return target


def run(args, env):
    print("Run: " + subprocess.list2cmdline([str(a) for a in args]), flush=True)
    subprocess.run([str(a) for a in args], cwd=ROOT, env=env, check=True)


def main():
    if os.name != "nt":
        raise SystemExit("This installer targets Windows x64.")
    TOOLS.mkdir(exist_ok=True)
    nodes = get_json("https://nodejs.org/dist/index.json")
    node_version = next(v["version"] for v in nodes if v["version"].startswith("v24.") and v["lts"])
    node_name = f"node-{node_version}-win-x64.zip"
    sums = fetch(f"https://nodejs.org/dist/{node_version}/SHASUMS256.txt").decode()
    node_hash = next(line.split()[0] for line in sums.splitlines() if line.split()[-1] == node_name)
    sources = {
        "node": {"version": node_version, "url": f"https://nodejs.org/dist/{node_version}/{node_name}",
                 "name": node_name, "digest": "sha256:" + node_hash},
        "ghidra": asset("NationalSecurityAgency/ghidra", r"PUBLIC.*\.zip$", "Ghidra_12.1.4_build"),
        "x64dbg": asset("x64dbg/x64dbg", r"^snapshot_.*\.zip$"),
        "dnspy": asset("dnSpyEx/dnSpy", r"^dnSpy-net-win64\.zip$"),
        "extremedumper": asset("wwh1004/ExtremeDumper", r"^ExtremeDumper.*\.zip$"),
        "pe-bear": asset("hasherezade/pe-bear", r"^PE-bear_.*_qt6_x64_win_vs22\.zip$"),
        "die": asset("horsicq/DIE-engine", r"^die_win64_portable_.*\.zip$"),
        "innoextract": {"version": "1.9", "name": "innoextract-1.9-windows.zip",
                        "url": "https://constexpr.org/innoextract/files/innoextract-1.9-windows.zip",
                        "digest": "md5:72d0d0dd874b6236eaa44411f4470ee1"},
    }
    sdk_releases = get_json("https://builds.dotnet.microsoft.com/dotnet/release-metadata/10.0/releases.json")
    sdk_version = sdk_releases["latest-sdk"]
    sdk = next(release["sdk"] for release in sdk_releases["releases"] if release.get("sdk", {}).get("version") == sdk_version)
    sdk_file = next(f for f in sdk["files"] if f["rid"] == "win-x64" and f["name"].endswith(".zip"))
    sources["dotnet"] = {"version": sdk_version, "name": f"dotnet-sdk-{sdk_version}-win-x64.zip",
                         "url": sdk_file["url"], "digest": "sha512:" + sdk_file["hash"]}
    manifest = {"sources": sources, "java_home": r"C:\Program Files\Java\jdk-23"}
    MANIFEST.write_text(json.dumps(manifest, indent=2), encoding="utf-8")
    print(json.dumps({key: value["version"] for key, value in sources.items()}), flush=True)
    for key, value in sources.items():
        install_zip(key, value)
        MANIFEST.write_text(json.dumps(manifest, indent=2), encoding="utf-8")
    node_dir = next((TOOLS / "node").glob("node-v*-win-x64"))
    ghidra_dir = next((TOOLS / "ghidra").glob("ghidra_*_PUBLIC"))
    env = os.environ.copy()
    env.update({"JAVA_HOME": manifest["java_home"], "GHIDRA_INSTALL_DIR": str(ghidra_dir),
                "DOTNET_ROOT": str(TOOLS / "dotnet"), "DOTNET_CLI_HOME": str(TOOLS / "state" / "dotnet"),
                "DOTNET_SKIP_FIRST_TIME_EXPERIENCE": "1", "DOTNET_CLI_TELEMETRY_OPTOUT": "1",
                "DOTNET_GENERATE_ASPNET_CERTIFICATE": "false",
                "NUGET_PACKAGES": str(TOOLS / "cache" / "nuget")})
    env["PATH"] = os.pathsep.join([str(node_dir), str(TOOLS / "dotnet"), str(TOOLS / "dotnet-tools"),
                                 str(Path(manifest["java_home"]) / "bin"), env["PATH"]])
    run([node_dir / "npm.cmd", "install", "--prefix", TOOLS / "rea", "--cache", TOOLS / "cache" / "npm",
         "--ignore-scripts", "--no-audit", "--no-fund", "rea-agents@6.7.0"], env)
    ilspy = TOOLS / "dotnet-tools" / "ilspycmd.exe"
    if not ilspy.exists():
        run([TOOLS / "dotnet" / "dotnet.exe", "tool", "install", "ilspycmd", "--tool-path", ilspy.parent], env)
    python_dir = TOOLS / "python"
    if not (python_dir / "Scripts" / "python.exe").exists():
        run([sys.executable, "-m", "venv", python_dir], env)
    run([python_dir / "Scripts" / "python.exe", "-m", "pip", "install", "--disable-pip-version-check",
         "--cache-dir", TOOLS / "cache" / "pip", "pefile", "dnfile", "rarfile"], env)
    manifest["paths"] = {"node": str(node_dir), "ghidra": str(ghidra_dir), "java": manifest["java_home"],
                         "dotnet": str(TOOLS / "dotnet"), "python": str(python_dir / "Scripts" / "python.exe"),
                         "rea_entry": str(TOOLS / "rea" / "node_modules" / "rea-agents" / "scripts" / "rea.mjs")}
    MANIFEST.write_text(json.dumps(manifest, indent=2), encoding="utf-8")
    print("Portable toolchain installed. See .tools/manifest.json.", flush=True)


if __name__ == "__main__":
    main()
