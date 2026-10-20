"""Package the current Windows build; --publish adds it to the handoff release.

Git credentials are kept in memory and supplied to curl through stdin only.
The historical analysis archive is never replaced or modified.
"""
from __future__ import annotations

import argparse
import hashlib
import json
import os
from pathlib import Path
import shutil
import subprocess
from urllib.parse import quote
from urllib.request import Request, urlopen
import zipfile

ROOT = Path(__file__).resolve().parents[1]
REPO = "mira-hib/emf-planner"
API = f"https://api.github.com/repos/{REPO}"
TAG = "windows-handoff-2026-10-20"


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--publish", action="store_true")
    args = parser.parse_args()
    git = ["git", "-c", f"safe.directory={ROOT.as_posix()}"]
    commit = subprocess.check_output(git + ["rev-parse", "HEAD"], cwd=ROOT).decode().strip()
    exe = ROOT / "src-tauri/target/release/emf-planner.exe"
    proof = json.loads((ROOT / "reference/interface-validation.json").read_text(encoding="utf-8"))
    exe_hash = hashlib.sha256(exe.read_bytes()).hexdigest()
    if exe_hash != proof["windows_release"]["sha256"]:
        raise ValueError("The executable differs from the validated build")
    archive = ROOT / f".tools/handoff/emf-planner-windows-interface-{commit[:8]}.zip"
    archive.parent.mkdir(parents=True, exist_ok=True)
    readme = (
        "EMF Planner — interface affinée, 20 octobre 2026\n\n"
        f"Sources : https://github.com/{REPO}/tree/{commit}\n"
        f"Exécutable Windows x64 : emf-planner.exe\nSHA-256 : {exe_hash}\n\n"
        "Extraire le ZIP puis lancer emf-planner.exe sur Windows avec WebView2.\n"
        "Sur Mac, compiler les sources : git pull origin main ; npm ci ; npm run tauri dev.\n"
        "Cette archive complète l’archive d’analyse historique, sans la remplacer.\n"
        "21 tests métier et 14 parcours d’interface réussis ; voir validation.json.\n"
        "Les outils non implémentés restent gris. Le volume 3D est un maillage approximatif réglable.\n"
    )
    files = {"emf-planner.exe": exe.read_bytes(), "README.txt": readme.encode(), "validation.json": (ROOT / "reference/interface-validation.json").read_bytes()}
    for name in ["refined-sources.png", "antenna-editor.png", "terrain-curves.png", "exposure-volume.png", "horizontal-refined.png"]:
        path = ROOT / ".tools/test-results" / name
        if path.exists():
            files[f"screenshots/{name}"] = path.read_bytes()
    with zipfile.ZipFile(archive, "w", compression=zipfile.ZIP_DEFLATED, compresslevel=9) as z:
        for name, data in files.items():
            info = zipfile.ZipInfo(name, (2026, 10, 20, 0, 0, 0))
            info.compress_type = zipfile.ZIP_DEFLATED
            z.writestr(info, data)
    summary = {"archive": archive.name, "bytes": archive.stat().st_size, "sha256": hashlib.sha256(archive.read_bytes()).hexdigest(), "source_commit": commit, "executable_sha256": exe_hash}
    if not args.publish:
        print(json.dumps(summary, indent=2))
        return
    status = subprocess.check_output(git + ["status", "--porcelain", "--untracked-files=no"], cwd=ROOT).decode()
    if any(line[3:] != "reference/interface-build.json" for line in status.splitlines()):
        raise RuntimeError("Commit tracked changes before publishing the build")
    env = {**os.environ, "GIT_TERMINAL_PROMPT": "0", "GCM_INTERACTIVE": "never"}
    credentials = subprocess.run(git + ["-c", "credential.interactive=never", "credential", "fill"], input=f"protocol=https\nhost=github.com\npath={REPO}.git\n\n".encode(), capture_output=True, cwd=ROOT, env=env)
    values = dict(line.split("=", 1) for line in credentials.stdout.decode().splitlines() if "=" in line)
    token = values.get("password")
    if credentials.returncode or not token:
        raise RuntimeError("No usable Git credential for this repository")
    headers = {"Authorization": f"Bearer {token}", "Accept": "application/vnd.github+json", "X-GitHub-Api-Version": "2022-11-28", "User-Agent": "emf-planner-interface-build"}

    def api(url, method="GET", body=None):
        request = Request(url, data=None if body is None else json.dumps(body).encode(), method=method, headers={**headers, "Content-Type": "application/json"})
        with urlopen(request, timeout=60) as response:
            return json.load(response)

    release = api(API + "/releases/tags/" + TAG)
    asset = next((a for a in release["assets"] if a["name"] == archive.name), None)
    if not asset:
        curl = shutil.which("curl")
        if not curl:
            raise RuntimeError("curl is required")
        config = "\n".join('header = "' + (key + ": " + value).replace("\\", "\\\\").replace('"', '\\"') + '"' for key, value in headers.items()) + "\n"
        url = release["upload_url"].split("{")[0] + "?name=" + quote(archive.name)
        transfer = subprocess.run([curl, "--fail", "--silent", "--show-error", "--connect-timeout", "30", "--max-time", "300", "--request", "POST", "--url", url, "--header", "Content-Type: application/zip", "--config", "-", "--upload-file", str(archive)], input=config.encode(), capture_output=True)
        if transfer.returncode:
            raise RuntimeError(f"Upload failed (curl {transfer.returncode}): {transfer.stderr.decode(errors='replace')}")
        asset = json.loads(transfer.stdout)
    if asset["state"] != "uploaded" or asset["size"] != summary["bytes"] or asset.get("digest") != "sha256:" + summary["sha256"]:
        raise ValueError("GitHub did not confirm the exact archive")
    public_hash = hashlib.sha256()
    with urlopen(asset["browser_download_url"], timeout=60) as response:
        for chunk in iter(lambda: response.read(1024 * 1024), b""):
            public_hash.update(chunk)
    if public_hash.hexdigest() != summary["sha256"]:
        raise ValueError("Public download verification failed")
    marker = "<!-- interface-build -->"
    body = (release.get("body") or "").split(marker)[0].rstrip() + (
        f"\n\n{marker}\n\nInterface affinée et nouveaux outils : commit `{commit}`.\n\n"
        f"Nouvel exécutable Windows : [{archive.name}]({asset['browser_download_url']}). "
        f"SHA-256 : `{summary['sha256']}`.\n\n"
        "Icônes et panneaux affinés ; bibliothèques/éditeur, câbles, sommets/KML, "
        "groupes, courbes de niveau, intervalles de coupes et surface EMF 3D. "
        "21 tests métier et 14 parcours UI réussis. Les outils absents restent gris.\n\n"
        "La grande archive d’analyse conserve l’ancien exécutable. Sur Mac : "
        "`git pull origin main`, `npm ci`, `npm run tauri dev`.\n"
    )
    api(API + f"/releases/{release['id']}", "PATCH", {"body": body})
    summary.update({"release_url": release["html_url"], "asset_url": asset["browser_download_url"], "digest": asset["digest"], "download_verified": True})
    (ROOT / "reference/interface-build.json").write_text(json.dumps(summary, indent=2) + "\n", encoding="utf-8")
    print(json.dumps(summary, indent=2))


if __name__ == "__main__":
    main()
