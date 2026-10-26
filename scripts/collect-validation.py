"""Assemble downloaded platform artifacts and test-kit evidence into one folder."""
from __future__ import annotations
import argparse, hashlib, json, re, shutil, zipfile
from pathlib import Path
from datetime import datetime, timezone
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]
LABELS = ['Windows-x64', 'Windows-arm64', 'Linux-x64', 'Linux-arm64', 'macOS-x64', 'macOS-arm64']
def digest(path):
    h = hashlib.sha256()
    with path.open('rb') as stream:
        for block in iter(lambda: stream.read(1024 * 1024), b''):
            h.update(block)
    return h.hexdigest()
def read(path):
    return json.loads(path.read_text(encoding='utf-8-sig'))
def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', default='test validation/' + datetime.now(timezone.utc).date().isoformat())
    parser.add_argument('--archive', action='store_true')
    args = parser.parse_args()
    output = Path(args.root).resolve()
    output.mkdir(parents=True, exist_ok=True)
    status = read(output / 'github-status.json') if (output / 'github-status.json').exists() else {}
    source = read(output / 'github-run.json') if (output / 'github-run.json').exists() else {}
    rows, actual = [], []
    for label in LABELS:
        candidates = [output / 'remote' / label]
        if label == 'Windows-x64':
            candidates += [output / 'local-kit' / label]
        def current_finished(path):
            if not (path / 'summary.json').exists() or read(path / 'summary.json')['status'] == 'running':
                return False
            if path.parent.name == 'remote' and source.get('commit'):
                receipt = path / 'download-provenance.json'
                return receipt.exists() and read(receipt).get('commit') == source['commit']
            return True
        chosen = next((p for p in candidates if current_finished(p)), None)
        if chosen:
            summary = read(chosen / 'summary.json')
            for subdir in ['builds', 'test-kit', 'logs']:
                if (chosen / subdir).exists():
                    shutil.copytree(chosen / subdir, output / subdir / label, dirs_exist_ok=True)
            for filename in ['summary.json', 'download-provenance.json']:
                if (chosen / filename).exists():
                    shutil.copy2(chosen / filename, output / 'test-kit' / label / filename)
            unit = summary.get('unit_tests', {})
            browser = summary.get('browser_tests', {})
            failed = [step['name'] for step in summary['steps'] if step['status'] != 'passed']
            rows.append({'platform': label, 'status': summary['status'], 'unit_tests': unit,
                         'browser_tests': browser, 'failed_steps': failed,
                         'artifact_source': chosen.relative_to(output).as_posix()})
            actual.append(label)
        else:
            job = next((j for j in status.get('jobs', []) if label in j['name']), {})
            rows.append({'platform': label, 'status': job.get('conclusion') or job.get('status', 'pending'),
                         'failed_steps': [s['name'] for s in job.get('steps', []) if s.get('conclusion') == 'failure']})
    # Keep the independent local Edge validation in addition to the CI Chromium report.
    if args.archive:
        verification_path = output / 'artifact-verification.json'
        verification = read(verification_path) if verification_path.exists() else {}
        if len(actual) != len(LABELS) or verification.get('commit') != source.get('commit') or not verification.get('all_file_checks_passed'):
            parser.error('All six current artifacts must be downloaded and pass verify-validation-artifacts.py before archiving.')
    local = output / 'local-kit/Windows-x64'
    if (local / 'summary.json').exists() and read(local / 'summary.json')['status'] != 'running':
        for subdir in ['test-kit', 'logs']:
            if (local / subdir).exists():
                shutil.copytree(local / subdir, output / subdir / 'Windows-x64-local-Edge', dirs_exist_ok=True)
    reference = output / 'test-kit/reference'
    reference.mkdir(parents=True, exist_ok=True)
    for file in ['reference/test-kit-coverage.json', 'reference/test-kit-scenarios.json',
                 'reference/native-project-save-2026-10-26.md', 'reference/rea-xemf-write-evidence.json',
                 'reference/full-kit-runtime-observations-2026-10-25.md', 'reference/test-kit-full-audit-2026-10-25.md',
                 'reference/rea-full-kit-safety-evidence.json', 'reference/rea-source-defaults-reprise.json',
                 'memory-bank/full-kit-implementation.md', 'reference/test-kit-audit-2026-10-22.md',
                 'memory-bank/resume-after-powercuts.md',
                 'reference/test-kit-native-evidence.json', 'tests/fixtures/test-kit-native-oracle.json',
                 'memory-bank/test-kit-validation.md']:
        if (ROOT / file).exists():
            shutil.copy2(ROOT / file, reference / Path(file).name)
    shutil.copytree(ROOT / 'test/emf_test_kit/guide', reference / 'guide', dirs_exist_ok=True)
    shutil.copytree(ROOT / 'test/emf_test_kit/data', reference / 'data', dirs_exist_ok=True)
    if (output / 'rea-sources-after-powercut.json').exists():
        shutil.copy2(output / 'rea-sources-after-powercut.json', reference / 'rea-sources-after-powercut.json')
    for evidence_dir, name in [(output / 'remote/ROB-05/test-kit/stress', 'ROB-05-CI'),
                         (output / 'stress-rob05-final', 'ROB-05-Windows-local'),
                         (output / 'stress-rob05-ui', 'ROB-05-browser-local'),
                         (output / 'native-samples-v2', 'original-project-samples')]:
        if evidence_dir.exists():
            shutil.copytree(evidence_dir, output / 'test-kit' / name, dirs_exist_ok=True)
    for evidence_dir in ['xemf-interop', 'native-project-ui']:
        if (output / evidence_dir).exists():
            shutil.copytree(output / evidence_dir, output / 'test-kit' / evidence_dir, dirs_exist_ok=True)
    web_source = output / 'builds/Windows-x64/web'
    if web_source.exists():
        shutil.copytree(web_source, output / 'builds/Web/dist', dirs_exist_ok=True)
        web_zip = output / 'builds/Windows-x64/emf-planner-web.zip'
        if web_zip.exists():
            shutil.copy2(web_zip, output / 'builds/Web/emf-planner-web.zip')
    inventory = read(ROOT / 'reference/test-kit-coverage.json')
    scenario_text = '# Résultats du test kit par scénario\n\n'
    scenario_text += f"{inventory['total_scenarios']} scénarios inventoriés ; {inventory['partially_covered_scenarios']} référencés partiellement par les suites automatisées. Les autres ne sont pas exécutés par ces suites.\n\n"
    scenario_text += 'Un test automatisé réussi ne valide pas toutes les étapes du scénario du guide. Les résultats détaillés (TAP, JUnit, HTML, fichiers exportés, captures) sont conservés par plateforme.\n\n'
    scenario_text += '| Scénario | Couverture | Tests concernés |\n|---|---|---|\n'
    for case in inventory['cases']:
        state = 'Partielle' if case['tests'] else 'Non exécuté'
        scenario_text += f"| {case['id']} | {state} | {', '.join(case['tests'])} |\n"
    (output / 'test-kit/SCENARIOS.md').write_text(scenario_text, encoding='utf-8')
    manifest = {'created_at': datetime.now(timezone.utc).isoformat(), 'source': source,
                'workflow': {k: status.get(k) for k in ['html_url', 'status', 'conclusion']},
                'test_scope': 'test/emf_test_kit; tests/test-kit*.test.ts, tests/browser/test-kit*.spec.ts and native format checks',
                'scenario_inventory': {'total': inventory['total_scenarios'], 'partial': inventory['partially_covered_scenarios'],
                    'not_run': inventory['total_scenarios'] - inventory['partially_covered_scenarios']}, 'platforms': rows}
    table = '| Plateforme | Statut | Tests métier du kit | Parcours du kit | Étapes en échec |\n|---|---|---:|---:|---|\n'
    for row in rows:
        unit, browser = row.get('unit_tests', {}), row.get('browser_tests', {})
        table += f"| {row['platform']} | {row['status']} | {unit.get('tests', '—')} | {browser.get('expected', '—')} | {', '.join(row['failed_steps'])} |\n"
    text = '# Builds et validation du test kit\n\n'
    text += 'Ce dossier regroupe les builds desktop et **uniquement les résultats des suites du test kit**, selon la précision utilisateur du 24 octobre 2026.\n\n' + table
    text += '\nLes ZIP de téléchargement originaux restent dans `downloads/` sur la machine de travail. L’archive de livraison contient directement les builds et résultats pour éviter de les dupliquer.\n'
    text += '\n- `builds/` : exécutables, installateurs et version web, par plateforme.\n- `test-kit/` : rapports HTML/JUnit/TAP/JSON, captures, téléchargements et matrice des 216 scénarios.\n- `logs/` : journaux complets des tests, compilations et installateurs.\n- `downloads/` : archives originales téléchargées depuis GitHub Actions.\n- `manifest.json` et `CHECKSUMS.sha256` : provenance, statuts et empreintes des fichiers livrés.\n\n'
    text += f"Les {inventory['partially_covered_scenarios']} scénarios liés aux suites ne sont couverts que partiellement ; les {inventory['total_scenarios'] - inventory['partially_covered_scenarios']} autres ne sont pas déclarés validés. Les 1 800 références CalcPPE concernent une méthode isolée de l’original et non son pipeline complet.\n\n"
    text += 'Builds de validation : les installateurs Windows ne sont pas signés ; les applications macOS ne sont pas notariées. Les tests d’interface pilotent la version navigateur du clone, pas les installateurs natifs. Aucun installateur n’est exécuté automatiquement. Android/iOS ne sont pas configurés dans ce projet desktop.\n\n'
    text += 'Sur macOS, utiliser le DMG ou le ZIP APP produit sur le runner, qui conserve les permissions. Sur Linux, rendre le binaire/AppImage exécutable si nécessaire (`chmod +x fichier.AppImage`). Le web se sert avec un serveur HTTP, pas en ouvrant directement le fichier HTML.\n\n'
    text += 'La charge ROB-05 complète est conservée dans `test-kit/ROB-05-CI/`, sa reprise locale dans `test-kit/ROB-05-Windows-local/` et le parcours réel dans l’application dans `test-kit/ROB-05-browser-local/`. Ce dernier a calculé puis sauvegardé ses 50 grilles.\n\n'
    text += 'L’audit des écarts et vérifications restantes est dans `test-kit/reference/test-kit-full-audit-2026-10-25.md`. Le succès des suites ne signifie pas que les 216 scénarios du guide sont intégralement validés.\n\n'
    if status.get('html_url'):
        text += f"[Campagne GitHub Actions]({status['html_url']}) — branche `{source.get('branch')}`, commit `{source.get('commit')}`. `main` et l’index de travail initial sont conservés.\n"
    (output / 'README.md').write_text(text, encoding='utf-8')
    paths = [path for subdir in ['builds', 'test-kit', 'logs'] for path in sorted((output / subdir).rglob('*')) if path.is_file()]
    paths += [output / name for name in ['README.md', 'github-status.json', 'github-run.json', 'artifact-verification.json'] if (output / name).is_file()]
    manifest['files'] = [{'path': path.relative_to(output).as_posix(), 'bytes': path.stat().st_size, 'sha256': digest(path)} for path in paths]
    (output / 'manifest.json').write_text(json.dumps(manifest, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
    (output / 'CHECKSUMS.sha256').write_text(''.join(f"{item['sha256']}  {item['path']}\n" for item in manifest['files']), encoding='utf-8')
    (output.parent / 'README.md').write_text(f'# Test validation\n\nDernière campagne : [{output.name}]({output.name}/README.md).\n\nLes builds et les résultats du test kit sont regroupés dans ce dossier. Les campagnes antérieures sont conservées dans leurs dossiers datés.\n', encoding='utf-8')
    if args.archive:
        archive = output.parent / f'emf-planner-builds-test-kit-{output.name}.zip'
        temporary = archive.with_suffix('.zip.partial')
        with zipfile.ZipFile(temporary, 'w', zipfile.ZIP_DEFLATED, compresslevel=6) as zipped:
            for path in sorted(output.rglob('*')):
                relative = path.relative_to(output)
                if path.is_file() and relative.parts[0] in ['builds', 'test-kit', 'logs', 'README.md', 'manifest.json', 'CHECKSUMS.sha256', 'github-status.json', 'github-run.json', 'artifact-verification.json']:
                    zipped.write(path, relative)
        with zipfile.ZipFile(temporary) as zipped:
            damaged = zipped.testzip()
            if damaged:
                raise RuntimeError('Archive integrity check failed: ' + damaged)
        temporary.replace(archive)
        (archive.with_suffix('.zip.sha256')).write_text(digest(archive) + '  ' + archive.name + '\n', encoding='utf-8')
    print(json.dumps({'root': str(output), 'platforms': rows, 'files': len(paths)}, ensure_ascii=False))
if __name__ == '__main__':
    main()
