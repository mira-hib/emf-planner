"""Publish the requested project evidence using this repository's Git credential.

Credentials stay in memory and are never printed or written to disk.
"""
from __future__ import annotations

import argparse
import json
import os
import shutil
import subprocess
from pathlib import Path
from urllib.parse import quote
from urllib.request import Request, urlopen

ROOT = Path(__file__).resolve().parents[1]
REPO = "mira-hib/emf-planner"
API = f"https://api.github.com/repos/{REPO}"


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--inspect", action="store_true")
    parser.add_argument("--status", action="store_true")
    args = parser.parse_args()
    env = {**os.environ, "GIT_TERMINAL_PROMPT": "0", "GCM_INTERACTIVE": "never"}
    result = subprocess.run(
        ["git", "-c", "credential.interactive=never", "credential", "fill"],
        input=f"protocol=https\nhost=github.com\npath={REPO}.git\n\n".encode(),
        capture_output=True, cwd=ROOT, env=env,
    )
    credential = dict(line.split("=", 1) for line in result.stdout.decode().splitlines() if "=" in line)
    token = credential.get("password")
    if result.returncode or not token:
        raise RuntimeError("No usable GitHub credential for this repository")
    headers = {"Authorization": f"Bearer {token}", "Accept": "application/vnd.github+json", "X-GitHub-Api-Version": "2022-11-28", "User-Agent": "emf-planner-handoff"}

    def api(url: str, method: str = "GET", body=None):
        payload = json.dumps(body).encode() if body is not None else None
        request = Request(url, data=payload, method=method, headers={**headers, "Content-Type": "application/json"})
        with urlopen(request, timeout=60) as response:
            return json.load(response)

    repo = api(API)
    print(json.dumps({"repository": repo["full_name"], "private": repo["private"], "can_push": repo.get("permissions", {}).get("push")}), flush=True)
    if args.inspect:
        return
    if args.status:
        releases = api(API + "/releases?per_page=100")
        print(json.dumps([{ "id": r["id"], "tag": r["tag_name"], "draft": r["draft"], "url": r["html_url"], "assets": [{"id": a["id"], "name": a["name"], "size": a["size"], "state": a["state"], "digest": a.get("digest")} for a in r["assets"]]} for r in releases], indent=2), flush=True)
        return
    summary_path = ROOT / "reference/handoff-summary.json"
    summary = json.loads(summary_path.read_text(encoding="utf-8"))
    archive = ROOT / ".tools/handoff" / summary["archive"]
    if archive.stat().st_size != summary["bytes"]:
        raise ValueError("Archive size does not match the handoff summary")
    # Reverify all data before it leaves the computer.
    subprocess.run([os.sys.executable, "-B", str(ROOT / "scripts/restore_handoff.py"), str(archive), "--verify-only"], check=True, cwd=ROOT)
    commit = subprocess.check_output(["git", "-c", f"safe.directory={ROOT.as_posix()}", "rev-parse", "HEAD"], cwd=ROOT).decode().strip()
    tag = "windows-handoff-2026-10-20"
    body = (
        "Passation Windows vers Mac pour continuer le clone et l’analyse de l’original.\n\n"
        f"Sources et memory bank : commit `{commit}`.\n\n"
        "Archive : original RAR/installateur/application, code et ressources décompilés, assemblages PE/CLI reconstruits des deux captures, inventaires REA, comparaisons, projets et résultats de référence, captures de tests et exécutable Windows compilé.\n\n"
        f"Taille : {summary['bytes']} octets. {summary['files']} fichiers.\n\n"
        f"SHA-256 : `{summary['sha256']}`.\n\n"
        "Les dumps RAM bruts et les outils installés ne sont pas nécessaires à la reprise : les images reconstruites, métadonnées et scripts sont fournis.\n\n"
        "Sur Mac : `git pull origin main`, `npm ci`, puis `npm run tauri dev`. "
        "Télécharger l’archive et lancer `python3 scripts/restore_handoff.py /chemin/vers/archive.zip` pour restaurer les artefacts."
    )
    # A draft's tag may not yet exist as a Git ref. List drafts as well so a
    # retry after an interrupted upload cannot create a duplicate release.
    releases = api(API + "/releases?per_page=100")
    release = next((r for r in releases if r["tag_name"] == tag), None)
    if release is None:
        release = api(API + "/releases", "POST", {"tag_name": tag, "target_commitish": commit, "name": "Passation Windows vers Mac — 20 octobre 2026", "body": body, "draft": True, "prerelease": True})
    matching = next((a for a in release.get("assets", []) if a["name"] == archive.name), None)
    if matching and matching["size"] != summary["bytes"]:
        raise ValueError("A different archive already exists under this release name")
    if not matching:
        upload_url = release["upload_url"].split("{")[0] + "?name=" + quote(archive.name)
        curl = shutil.which("curl")
        if not curl:
            raise RuntimeError("curl is required for the large archive upload")
        # Keep the credential off the process command line and filesystem.
        # curl streams the file with larger buffers than urllib's default 8 KiB.
        config = "\n".join(f'header = "{key}: {value}"' for key, value in headers.items()) + "\n"
        print(f"Uploading {summary['bytes']} bytes with curl", flush=True)
        transfer = subprocess.run([curl, "--fail", "--silent", "--show-error", "--connect-timeout", "30", "--max-time", "1800", "--request", "POST", "--url", upload_url, "--header", "Content-Type: application/zip", "--config", "-", "--upload-file", str(archive)], input=config.encode(), capture_output=True)
        if transfer.returncode:
            raise RuntimeError(f"Archive transfer failed (curl {transfer.returncode}): {transfer.stderr.decode(errors='replace')}")
        matching = json.loads(transfer.stdout)
    if matching.get("digest") and matching["digest"] != "sha256:" + summary["sha256"]:
        raise ValueError("GitHub asset digest does not match the local archive")
    if matching["size"] != summary["bytes"] or matching["state"] != "uploaded":
        raise ValueError("GitHub has not confirmed the complete asset")
    if release["draft"]:
        release = api(API + f"/releases/{release['id']}", "PATCH", {"draft": False})
    # Publishing replaces the temporary draft tag in asset download URLs.
    release = api(API + f"/releases/{release['id']}")
    matching = next(a for a in release["assets"] if a["id"] == matching["id"])
    published = {"release_url": release["html_url"], "asset_url": matching["browser_download_url"], "bytes": matching["size"], "digest": matching.get("digest"), "source_commit": commit}
    (ROOT / ".tools/handoff/published-release.json").write_text(json.dumps(published, indent=2), encoding="utf-8")
    print(json.dumps(published, indent=2), flush=True)


if __name__ == "__main__":
    main()
