"""Prepare redistributable, licensed format converters for the current host.

Windows uses pinned upstream releases. Unix builds pinned upstream sources.
No converter is downloaded at application runtime.
"""
import hashlib
import json
import os
import pathlib
import platform
import shutil
import subprocess
import tarfile
import urllib.request
import zipfile

ROOT = pathlib.Path(__file__).resolve().parents[1]
CACHE = ROOT / '.tools' / 'native-exports'
OUT = ROOT / 'src-tauri' / 'native'
CACHE.mkdir(parents=True, exist_ok=True)
OUT.mkdir(parents=True, exist_ok=True)
LIBREDWG_SOURCE_SHA256 = '62ebb73b984f865960f20ed26619ea5f8789d5e3fd088fa40a2598384da81275'

def download(url, filename, expected=None):
    target = CACHE / filename
    if not target.exists():
        with urllib.request.urlopen(url, timeout=120) as response, target.open('wb') as stream:
            shutil.copyfileobj(response, stream)
    digest = hashlib.sha256(target.read_bytes()).hexdigest()
    if expected and digest != expected:
        raise RuntimeError(f'SHA256 mismatch: {filename}')
    return target, digest

def run(args, cwd):
    print(' '.join(map(str, args)), flush=True)
    subprocess.run(list(map(str, args)), cwd=cwd, check=True)

manifest = {'platform': platform.system(), 'architecture': platform.machine(), 'sources': []}
if os.name == 'nt':
    archive, digest = download('https://github.com/LibreDWG/libredwg/releases/download/0.14/libredwg-0.14-win64.zip',
        'libredwg-0.14-win64.zip', '1ad7e15344d20b3426c3435b078d82fb84b35062815946b2cca9c5fc9810fea8')
    destination = CACHE / 'libredwg'
    with zipfile.ZipFile(archive) as bundle:
        bundle.extractall(destination)
    for name in ['dxf2dwg.exe', 'dwg2dxf.exe', 'dwgread.exe', 'libredwg-0.dll', 'libiconv-2.dll', 'libpcre2-8-0.dll', 'libpcre2-16-0.dll']:
        shutil.copy2(destination / name, OUT / name)
    manifest['sources'].append({'project': 'GNU LibreDWG', 'version': '0.14', 'sha256': digest,
        'url': 'https://github.com/LibreDWG/libredwg/releases/tag/0.14', 'license': 'GPL-3.0-or-later'})
    archive, digest = download('https://github.com/johnyf/fig2u3d/releases/download/v1.0.1/binaries_needed.tar',
        'binaries_needed.tar', '092d368324f1b0ddbc09387f12c2176f4e855ff965d5119702d25c5dcabd764e')
    with tarfile.open(archive) as bundle:
        bundle.extractall(CACHE, filter='data')
    for name in ['IDTFConverter.exe', 'IFXCore.dll']:
        shutil.copy2(CACHE / 'bin' / 'w32' / name, OUT / name)
    shutil.copytree(CACHE / 'bin' / 'w32' / 'Plugins', OUT / 'Plugins', dirs_exist_ok=True)
    shutil.copy2(CACHE / 'bin' / 'License.txt', OUT / 'U3D-LICENSE.txt')
    manifest['sources'].append({'project': 'Intel U3D', 'version': 'fig2u3d-v1.0.1', 'sha256': digest,
        'url': 'https://github.com/johnyf/fig2u3d/releases/tag/v1.0.1', 'license': 'Apache-2.0'})
else:
    archive, digest = download('https://github.com/LibreDWG/libredwg/releases/download/0.14/libredwg-0.14.tar.xz', 'libredwg-0.14.tar.xz', LIBREDWG_SOURCE_SHA256)
    with tarfile.open(archive) as bundle:
        bundle.extractall(CACHE, filter='data')
    source = CACHE / 'libredwg-0.14'
    run(['./configure', '--disable-shared', '--disable-bindings', '--disable-docs'], source)
    run(['make', '-j', str(min(os.cpu_count() or 2, 4))], source)
    for name in ['dxf2dwg', 'dwg2dxf', 'dwgread']:
        shutil.copy2(source / 'programs' / name, OUT / name)
    manifest['sources'].append({'project': 'GNU LibreDWG', 'version': '0.14', 'sha256': digest,
        'url': 'https://github.com/LibreDWG/libredwg/releases/tag/0.14', 'license': 'GPL-3.0-or-later'})
    commit = '4d75fccf839d1aa180ec53e519e307272957b625'
    archive, digest = download(f'https://github.com/Korijn/u3d/archive/{commit}.tar.gz', f'u3d-{commit}.tar.gz',
        '2a7e2ebc0631d1a03923c8db9c99cfeafab7948dd40eb447c628d3bff898cf52')
    with tarfile.open(archive) as bundle:
        bundle.extractall(CACHE, filter='data')
    source = CACHE / f'u3d-{commit}'
    build = CACHE / 'u3d-build'
    # This 2006 dependency predates modern macOS SDK headers and C++17.
    # Keep the compatibility edits explicit with the packaged provenance.
    compatibility = {
        'RTL/Dependencies/png/pngpriv.h': [('#      include <fp.h>', '#      include <math.h>')],
        'RTL/Dependencies/zlib/zutil.h': [('#if defined(MACOS) || defined(TARGET_OS_MAC)', '#if (defined(MACOS) || defined(TARGET_OS_MAC)) && !defined(__APPLE__)')],
    }
    for relative, edits in compatibility.items():
        path = source / relative
        text = path.read_text()
        for old, new in edits:
            if old not in text:
                raise RuntimeError(f'Unexpected U3D source for compatibility patch: {relative}')
            text = text.replace(old, new)
        path.write_text(text)
    run(['cmake', '-S', source, '-B', build, '-DU3D_SHARED=OFF', '-DU3D_BUILD_SAMPLES=OFF',
         '-DCMAKE_C_STANDARD=99', '-DCMAKE_CXX_STANDARD=14', '-DCMAKE_CXX_STANDARD_REQUIRED=ON',
         '-DCMAKE_BUILD_TYPE=Release', '-DCMAKE_POLICY_VERSION_MINIMUM=3.5'], CACHE)
    run(['cmake', '--build', build, '--parallel', str(min(os.cpu_count() or 2, 4))], CACHE)
    candidates = [path for path in build.rglob('IDTFConverter') if path.is_file()]
    if len(candidates) != 1:
        raise RuntimeError(f'Expected one built U3D converter, found {candidates}')
    shutil.copy2(candidates[0], OUT / 'IDTFConverter')
    # IFXCoreStatic is a loader. The encoder still dynamically loads its core
    # and import/export/scheduling plug-ins, even with bundled image libraries.
    for path in build.rglob('*'):
        if path.is_file() and path.name.startswith('libIFX') and path.suffix in ['.so', '.dylib']:
            shutil.copy2(path, OUT / path.name)
    for path in source.iterdir():
        if path.is_file() and any(word in path.name.lower() for word in ['license', 'copying', 'notice']):
            shutil.copy2(path, OUT / ('U3D-' + path.name))
    (OUT / 'U3D-compatibility-patches.json').write_text(json.dumps(compatibility, indent=2) + '\n')
    manifest['sources'].append({'project': 'U3D', 'commit': commit, 'sha256': digest,
        'url': 'https://github.com/Korijn/u3d', 'license': 'Apache-2.0'})

# Include the exact corresponding LibreDWG source with the distributed executable.
archive, digest = download('https://github.com/LibreDWG/libredwg/releases/download/0.14/libredwg-0.14.tar.xz', 'libredwg-0.14.tar.xz', LIBREDWG_SOURCE_SHA256)
shutil.copy2(archive, OUT / archive.name)
with tarfile.open(archive) as bundle:
    copying = next(member for member in bundle.getmembers() if member.name.endswith('/COPYING'))
    (OUT / 'LibreDWG-COPYING.txt').write_bytes(bundle.extractfile(copying).read())
manifest['files'] = {path.relative_to(OUT).as_posix(): hashlib.sha256(path.read_bytes()).hexdigest()
    for path in sorted(OUT.rglob('*')) if path.is_file() and path.name != 'manifest.json'}
(OUT / 'manifest.json').write_text(json.dumps(manifest, indent=2) + '\n', encoding='utf8')
print(f'Prepared {len(manifest["files"])} files in {OUT}', flush=True)
