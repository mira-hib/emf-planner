"""Retain a desktop build, installers, test results, reports and logs together."""
from __future__ import annotations
import argparse
import hashlib
import json
import math
import os
import platform
from pathlib import Path
import shutil
import subprocess
import sys
import time
from datetime import datetime, timezone
import zipfile
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]
if hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8', errors='replace')
def utc():
    return datetime.now(timezone.utc).isoformat()
def digest(path):
    result = hashlib.sha256()
    with path.open('rb') as stream:
        for block in iter(lambda: stream.read(1024 * 1024), b''):
            result.update(block)
    return result.hexdigest()
def write_json(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
def zip_tree(source, destination):
    with zipfile.ZipFile(destination, 'w', zipfile.ZIP_DEFLATED, compresslevel=6) as archive:
        for path in sorted(source.rglob('*')):
            if path.is_file():
                archive.write(path, path.relative_to(source))

def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output-root', default='test validation')
    parser.add_argument('--label', required=True)
    parser.add_argument('--target', required=True)
    parser.add_argument('--bundles', required=True)
    parser.add_argument('--install', action='store_true')
    parser.add_argument('--prepare-native', action='store_true')
    parser.add_argument('--browser', choices=['chromium', 'msedge'], default='msedge' if os.name == 'nt' else 'chromium')
    args = parser.parse_args()
    output = Path(args.output_root).resolve() / args.label
    output.mkdir(parents=True, exist_ok=True)
    if (output / 'summary.json').exists():
        parser.error('Output already contains a run. Choose a new --output-root.')
    logs, reports, builds = [output / name for name in ['logs', 'test-kit', 'builds']]
    for path in [logs, reports, builds]:
        path.mkdir(parents=True, exist_ok=True)
    env = {**os.environ, 'CI': 'true', 'PLAYWRIGHT_HTML_OPEN': 'never', 'PYTHONIOENCODING': 'utf-8',
           'PLAYWRIGHT_CHANNEL': args.browser, 'APPIMAGE_EXTRACT_AND_RUN': '1'}
    summary = {'label': args.label, 'target': args.target, 'started_at': utc(), 'status': 'running',
        'host': {'system': platform.system(), 'release': platform.release(), 'machine': platform.machine()},
        'test_scope': 'test/emf_test_kit: tests/test-kit*.test.ts, tests/browser/test-kit*.spec.ts and actual native format encoders',
        'signature': 'Validation artifacts; no distribution signing, notarization, installation or publication.', 'steps': []}
    def persist():
        write_json(output / 'summary.json', summary)
    def run(name, command, extra_env=None):
        print(f'\n>>> {name}', flush=True)
        started = time.monotonic()
        entry = {'name': name, 'command': command, 'started_at': utc(), 'log': f'logs/{name}.log'}
        executable_command = command
        if os.name == 'nt' and command[0] in ['npm', 'npx']:
            executable_command = [os.environ.get('COMSPEC', 'cmd.exe'), '/d', '/c', subprocess.list2cmdline(command)]
        try:
            with (logs / f'{name}.log').open('w', encoding='utf-8') as log:
                process = subprocess.Popen(executable_command, cwd=ROOT, env={**env, **(extra_env or {})},
                    stdout=subprocess.PIPE, stderr=subprocess.STDOUT, encoding='utf-8', errors='replace', text=True)
                for line in process.stdout:
                    log.write(line)
                    log.flush()
                    print(line, end='', flush=True)
                entry['exit_code'] = process.wait()
        except OSError as error:
            entry.update(exit_code=127, error=str(error))
            (logs / f'{name}.log').write_text(str(error) + '\n', encoding='utf-8')
        entry.update(duration_seconds=round(time.monotonic() - started, 3), status='passed' if entry['exit_code'] == 0 else 'failed')
        summary['steps'].append(entry)
        persist()
        return entry['exit_code'] == 0
    persist()
    for name, command in [('node-version', ['node', '--version']), ('rust-version', ['rustc', '--version']), ('cargo-version', ['cargo', '--version'])]:
        run(name, command)
    if args.prepare_native:
        run('prepare-native', [sys.executable, 'scripts/prepare-native-exports.py'])
    if args.install and run('npm-ci', ['npm', 'ci']) and args.browser == 'chromium':
        command = ['npm', 'exec', '--', 'playwright', 'install']
        if platform.system() == 'Linux':
            command.append('--with-deps')
        run('browser-install', command + ['chromium'])
    tests = [path.relative_to(ROOT).as_posix() for path in sorted((ROOT / 'tests').glob('test-kit*.test.ts'))]
    run('unit-tests', ['node', '--import', 'tsx', '--test', '--test-reporter=spec', '--test-reporter-destination=stdout',
        '--test-reporter=junit', f'--test-reporter-destination={reports / "unit.junit.xml"}',
        '--test-reporter=tap', f'--test-reporter-destination={reports / "unit.tap"}', *tests])
    run('kit-inventory', ['node', '--import', 'tsx', 'scripts/test-kit-inventory.mts'])
    if (ROOT / 'reference/test-kit-coverage.json').exists():
        shutil.copy2(ROOT / 'reference/test-kit-coverage.json', reports / 'test-kit-coverage.json')
    rust_test_count = sum(path.read_text(encoding='utf-8').count('#[test]') for path in (ROOT / 'src-tauri/src').rglob('*.rs'))
    write_json(reports / 'rust-tests.json', {'declared_tests': rust_test_count,
        'scope': 'Legacy Rust modules are not the React/Worker engine. Native compilation is verified separately.'})
    if rust_test_count:
        run('rust-tests', ['cargo', 'test', '--locked', '--manifest-path', 'src-tauri/Cargo.toml', '--target', args.target, '--lib'])
    run('native-formats', ['node', '--import', 'tsx', 'scripts/validate-native-exports.mts', str(reports / 'native-exports')])
    browser_tests = [path.relative_to(ROOT).as_posix() for path in sorted((ROOT / 'tests/browser').glob('test-kit*.spec.ts'))]
    if run('frontend-build', ['npm', 'run', 'build']):
        shutil.copytree(ROOT / 'dist', builds / 'web', dirs_exist_ok=True)
        zip_tree(builds / 'web', builds / 'emf-planner-web.zip')
    run('browser-tests', ['node', 'node_modules/@playwright/test/cli.js', 'test', *browser_tests, '--output', str(reports / 'browser-artifacts'), '--reporter=list,html,json,junit'], {
        'PLAYWRIGHT_HTML_REPORT': str(reports / 'browser-html'), 'PLAYWRIGHT_JSON_OUTPUT_FILE': str(reports / 'browser.json'),
        'PLAYWRIGHT_JUNIT_OUTPUT_FILE': str(reports / 'browser.junit.xml'), 'PLAYWRIGHT_STATIC': '1',
        'PLAYWRIGHT_PORT': env.get('PLAYWRIGHT_PORT', '1432'), 'PLAYWRIGHT_DIST': str(builds / 'web')})
    for directory in (ROOT / 'reference').glob('test-kit-interface'):
        if directory.is_dir():
            shutil.copytree(directory, reports / 'screenshots' / directory.name, dirs_exist_ok=True)
    native_ok = run('native-build', ['node', 'node_modules/@tauri-apps/cli/tauri.js', 'build', '--ci', '--no-bundle', '--target', args.target])
    cargo_target = Path(env.get('CARGO_TARGET_DIR', str(ROOT / 'src-tauri/target')))
    if not cargo_target.is_absolute():
        cargo_target = ROOT / 'src-tauri' / cargo_target
    release = cargo_target / args.target / 'release'
    executable = release / ('emf-planner.exe' if platform.system() == 'Windows' else 'emf-planner')
    if native_ok and executable.is_file():
        shutil.copy2(executable, builds / executable.name)
        # Portable executables resolve these beside the executable on every host.
        shutil.copytree(ROOT / 'src-tauri/native', builds / 'native', dirs_exist_ok=True)
        for bundle in args.bundles.split(','):
            command = ['node', 'node_modules/@tauri-apps/cli/tauri.js', 'bundle', '--ci', '--target', args.target, '--bundles', bundle]
            if platform.system() == 'Darwin':
                command += ['--config', str(ROOT / 'src-tauri/tauri.validation.macos.json')]
            if run(f'bundle-{bundle}', command):
                folder = release / 'bundle' / ('macos' if bundle == 'app' else bundle)
                if folder.exists():
                    if bundle == 'app':
                        subprocess.run(['ditto', '-c', '-k', '--sequesterRsrc', '--keepParent', str(next(folder.glob('*.app'))), str(builds / 'emf-planner-macos-app.zip')], check=True)
                    else:
                        # Tauri leaves expanded DEB data and an entire AppDir
                        # beside installers. Retain final packages, not those
                        # duplicate staging trees.
                        extension = {'deb': '.deb', 'rpm': '.rpm', 'appimage': '.AppImage',
                                     'dmg': '.dmg', 'nsis': '.exe', 'msi': '.msi'}[bundle]
                        final = list(folder.glob('*' + extension))
                        if not final:
                            raise RuntimeError(f'No final {bundle} package was produced.')
                        (builds / folder.name).mkdir(parents=True, exist_ok=True)
                        for package in final:
                            shutil.copy2(package, builds / folder.name / package.name)
                else:
                    summary['steps'].append({'name': f'artifact-{bundle}', 'status': 'failed', 'exit_code': 1, 'error': 'No bundle directory was produced.'})
    run('diff-check', ['git', '-c', 'safe.directory=' + ROOT.as_posix(), 'diff', '--check'])
    if (reports / 'unit.junit.xml').exists():
        tree = ET.parse(reports / 'unit.junit.xml')
        summary['unit_tests'] = {'tests': len(tree.findall('.//testcase')), 'failures': len(tree.findall('.//failure')), 'errors': len(tree.findall('.//error'))}
    if (reports / 'browser.json').exists():
        summary['browser_tests'] = json.loads((reports / 'browser.json').read_text(encoding='utf-8')).get('stats', {})
    summary.update(finished_at=utc(), status='passed' if all(step['status'] == 'passed' for step in summary['steps']) else 'failed')
    persist()
    inventory = [{'path': path.relative_to(output).as_posix(), 'bytes': path.stat().st_size, 'sha256': digest(path)}
        for path in sorted(output.rglob('*')) if path.is_file() and not path.is_symlink() and path.name != 'manifest.json']
    write_json(output / 'manifest.json', {'created_at': utc(), 'files': inventory})
    (output / 'README.md').write_text(f'# {args.label}\n\nStatut : **{summary["status"]}**. Cible : `{args.target}`.\n\n'
        'Builds dans `builds/`, résultats du kit et captures dans `test-kit/`, journaux dans `logs/`.\n'
        'Résultats : `summary.json`. Empreintes SHA-256 : `manifest.json`.\n'
        'Builds de validation sans signature de distribution ni notarisation. Pas d’installation/exécution native automatique.\n', encoding='utf-8')
    print(json.dumps({'output': str(output), 'status': summary['status']}, ensure_ascii=False), flush=True)
    return 0 if summary['status'] == 'passed' else 1
if __name__ == '__main__':
    sys.exit(main())
