"""Check downloaded validation artifact identity, architecture and packaged tools.

These are file checks, not an assertion that installers were executed.
"""
import argparse
import hashlib
import json
import struct
from pathlib import Path

EXPECTED = {'Windows-x64':'x64','Windows-arm64':'arm64','Linux-x64':'x64',
            'Linux-arm64':'arm64','macOS-x64':'x64','macOS-arm64':'arm64'}

def sha256(path):
    result=hashlib.sha256()
    with path.open('rb') as stream:
        for chunk in iter(lambda:stream.read(1024*1024),b''):
            result.update(chunk)
    return result.hexdigest()

def architecture(path):
    with path.open('rb') as stream:
        header=stream.read(64)
        if header[:2]==b'MZ':
            stream.seek(struct.unpack_from('<I',header,60)[0])
            pe=stream.read(6)
            if pe[:4]!=b'PE\0\0': raise ValueError('Invalid PE signature')
            return {0x8664:'x64',0xaa64:'arm64',0x14c:'x86'}.get(struct.unpack_from('<H',pe,4)[0],'unknown')
        if header[:4]==b'\x7fELF':
            endian='<' if header[5]==1 else '>'
            return {62:'x64',183:'arm64',3:'x86'}.get(struct.unpack_from(endian+'H',header,18)[0],'unknown')
        if header[:4] in [b'\xcf\xfa\xed\xfe',b'\xfe\xed\xfa\xcf']:
            endian='<' if header[:4]==b'\xcf\xfa\xed\xfe' else '>'
            return {0x01000007:'x64',0x0100000c:'arm64'}.get(struct.unpack_from(endian+'I',header,4)[0],'unknown')
    raise ValueError('Unsupported executable format')

def check_platform(root,label,commit):
    folder=root/'remote'/label
    if not (folder/'summary.json').exists():return {'platform':label,'status':'missing'}
    receipt=json.loads((folder/'download-provenance.json').read_text()) if (folder/'download-provenance.json').exists() else {}
    if receipt.get('commit')!=commit:return {'platform':label,'status':'stale-artifact','commit':receipt.get('commit')}
    summary=json.loads((folder/'summary.json').read_text())
    builds=folder/'builds'
    executable=builds/('emf-planner.exe' if label.startswith('Windows') else 'emf-planner')
    actual=architecture(executable)
    if actual!=EXPECTED[label]:raise ValueError(f'{label}: wrong application architecture: {actual}')
    native=builds/'native'
    manifest=json.loads((native/'manifest.json').read_text())
    for name,digest in manifest['files'].items():
        path=native/name
        if not path.is_file() or sha256(path)!=digest:raise ValueError(f'{label}: converter manifest mismatch: {name}')
    suffix='.exe' if label.startswith('Windows') else ''
    for name in ['IDTFConverter','dxf2dwg','dwg2dxf','dwgread']:
        if not (native/(name+suffix)).is_file():raise ValueError(f'{label}: missing {name}')
    if not label.startswith('Windows'):
        for name in ['libIFXCore.so','libIFXImporting.so','libIFXExporting.so','libIFXScheduling.so']:
            if not (native/name).is_file():raise ValueError(f'{label}: missing dynamic U3D plug-in {name}')
    formats={'.msi':b'\xd0\xcf\x11\xe0\xa1\xb1\x1a\xe1','.deb':b'!<arch>\n','.rpm':b'\xed\xab\xee\xdb'}
    packages=[]
    for path in sorted(builds.rglob('*')):
        if not path.is_file() or native in path.parents:continue
        suffix=path.suffix.lower()
        if suffix in formats:
            with path.open('rb') as stream:
                if not stream.read(8).startswith(formats[suffix]):raise ValueError(f'Invalid package signature: {path}')
            packages.append(path.relative_to(builds).as_posix())
        elif suffix=='.appimage':
            if architecture(path)!=EXPECTED[label]:raise ValueError('Wrong AppImage architecture')
            packages.append(path.relative_to(builds).as_posix())
        elif suffix=='.dmg':
            with path.open('rb') as stream:
                stream.seek(-512,2)
                if stream.read(4)!=b'koly':raise ValueError('Invalid DMG trailer')
            packages.append(path.relative_to(builds).as_posix())
        elif suffix=='.exe' and path!=executable:
            architecture(path);packages.append(path.relative_to(builds).as_posix())
    if not packages:raise ValueError(f'{label}: no installer package')
    if not (builds/'web/index.html').is_file():raise ValueError(f'{label}: missing web build')
    return {'platform':label,'status':'passed-file-checks','validation_status':summary['status'],
        'commit':commit,'application_architecture':actual,'application_sha256':sha256(executable),
        'converter_files_verified':len(manifest['files']),'packages':packages,
        'unit_tests':summary.get('unit_tests'),'browser_tests':summary.get('browser_tests'),
        'scope':'Main executable architecture, installer signatures, raw converter manifest, U3D plug-ins and web presence. No installer or foreign executable was run. Windows ARM64 helpers can use x86/x64 emulation; their architecture is distinct from the main application.'}

def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root',default='test validation/2026-10-25')
    args=parser.parse_args();root=Path(args.root).resolve()
    commit=json.loads((root/'github-run.json').read_text())['commit']
    rows=[]
    for label in EXPECTED:
        try:rows.append(check_platform(root,label,commit))
        except Exception as error:rows.append({'platform':label,'status':'failed','error':str(error)})
    report={'commit':commit,'platforms':rows,'all_file_checks_passed':all(row['status']=='passed-file-checks' for row in rows)}
    (root/'artifact-verification.json').write_text(json.dumps(report,ensure_ascii=False,indent=2)+'\n',encoding='utf-8')
    print(json.dumps(report))
    return 0 if report['all_file_checks_passed'] else 1

if __name__=='__main__':raise SystemExit(main())
