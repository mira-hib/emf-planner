"""Verify and restore the Windows evidence archive; never executes its contents."""
from __future__ import annotations

import argparse
import hashlib
import json
import shutil
from pathlib import Path, PurePosixPath
from zipfile import ZipFile


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("archive", type=Path)
    parser.add_argument("--verify-only", action="store_true")
    args = parser.parse_args()
    root = Path(__file__).resolve().parents[1]
    summary = json.loads((root / "reference/handoff-summary.json").read_text(encoding="utf-8"))
    digest = hashlib.sha256()
    with args.archive.open("rb") as stream:
        for chunk in iter(lambda: stream.read(1024 * 1024), b""):
            digest.update(chunk)
    if digest.hexdigest() != summary["sha256"]:
        raise ValueError("Archive SHA-256 does not match the versioned handoff summary")
    with ZipFile(args.archive) as archive:
        manifest = json.loads(archive.read("HANDOFF-MANIFEST.json"))
        targets = []
        expected = {"HANDOFF-MANIFEST.json"}
        for item in manifest["files"]:
            name = item["path"]
            relative = PurePosixPath(name)
            if relative.is_absolute() or ".." in relative.parts or "\\" in name or ":" in name:
                raise ValueError(f"Unsafe archive entry: {name}")
            if not (name == "EMFPlanner.rar" or name.startswith(("extracted/", ".tools/emf/", ".tools/emf-scenarios/", ".tools/clone-validation/", ".tools/test-results/")) or name == "src-tauri/target/release/emf-planner.exe"):
                raise ValueError(f"Unexpected archive location: {name}")
            expected.add(name)
            target = root.joinpath(*relative.parts).resolve()
            if root.resolve() not in target.parents:
                raise ValueError(f"Entry leaves project root: {name}")
            info = archive.getinfo(name)
            if info.file_size != item["bytes"]:
                raise ValueError(f"Incorrect entry length: {name}")
            checksum = hashlib.sha256()
            with archive.open(info) as source:
                for chunk in iter(lambda: source.read(1024 * 1024), b""):
                    checksum.update(chunk)
            if checksum.hexdigest() != item["sha256"]:
                raise ValueError(f"Incorrect entry SHA-256: {name}")
            targets.append((info, target))
        if set(archive.namelist()) != expected:
            raise ValueError("Archive entries do not match the manifest")
        print(f"Verified archive and {len(targets)} individual file checksums", flush=True)
        if not args.verify_only:
            for info, target in targets:
                target.parent.mkdir(parents=True, exist_ok=True)
                with archive.open(info) as source, target.open("wb") as destination:
                    shutil.copyfileobj(source, destination)
            print(f"Restored evidence under {root}", flush=True)


if __name__ == "__main__":
    main()
