"""Package reproducible Windows evidence for transfer to another computer."""
from __future__ import annotations

import hashlib
import json
from pathlib import Path
from zipfile import ZIP_DEFLATED, ZipFile

ROOT = Path(__file__).resolve().parents[1]
OUTPUT = ROOT / ".tools/handoff/emf-planner-windows-handoff-2026-10-20.zip"
INPUTS = [
    "EMFPlanner.rar",
    "extracted",
    ".tools/emf",
    ".tools/emf-scenarios",
    ".tools/clone-validation",
    ".tools/test-results",
    "src-tauri/target/release/emf-planner.exe",
]


def sha256(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as stream:
        for chunk in iter(lambda: stream.read(1024 * 1024), b""):
            digest.update(chunk)
    return digest.hexdigest()


def main() -> None:
    OUTPUT.parent.mkdir(parents=True, exist_ok=True)
    files: dict[str, Path] = {}
    for item in INPUTS:
        source = ROOT / item
        if not source.exists():
            raise FileNotFoundError(source)
        for path in (source.rglob("*") if source.is_dir() else [source]):
            if path.is_file() and path.suffix.lower() != ".dmp":
                files[path.relative_to(ROOT).as_posix()] = path
    manifest = {
        "purpose": "Mac continuation: recovered code, assemblies, originals, scenarios and evidence",
        "excluded": [
            "Raw .dmp memory captures; reconstructed PE/CLI images and capture metadata are included",
            "Installed Windows toolchain and dependency caches; installation scripts are in Git",
        ],
        "files": [],
    }
    temporary = OUTPUT.with_suffix(".zip.tmp")
    with ZipFile(temporary, "w", compression=ZIP_DEFLATED, compresslevel=6) as archive:
        for index, (name, path) in enumerate(sorted(files.items()), 1):
            manifest["files"].append({"path": name, "bytes": path.stat().st_size, "sha256": sha256(path)})
            archive.write(path, name)
            if index % 50 == 0:
                print(f"Packaged {index}/{len(files)} files", flush=True)
        archive.writestr("HANDOFF-MANIFEST.json", json.dumps(manifest, indent=2, ensure_ascii=False))
    temporary.replace(OUTPUT)
    manifest_path = OUTPUT.with_name("handoff-manifest.json")
    manifest_path.write_text(json.dumps(manifest, indent=2, ensure_ascii=False), encoding="utf-8")
    summary = {"archive": OUTPUT.name, "bytes": OUTPUT.stat().st_size, "sha256": sha256(OUTPUT), "files": len(files)}
    OUTPUT.with_name("handoff-summary.json").write_text(json.dumps(summary, indent=2), encoding="utf-8")
    print(json.dumps(summary, indent=2), flush=True)


if __name__ == "__main__":
    main()
