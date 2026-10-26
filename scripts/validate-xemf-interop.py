"""Validate self-generated .xemf files with the installed original's .NET data types.

Windows/.NET Framework only; never reads an arbitrary supplied project file.
The assembly argument selects the original recovered data type definitions.
"""
import argparse
import json
import os
from pathlib import Path
import struct
import subprocess
import zlib

root = Path(__file__).resolve().parents[1]
parser = argparse.ArgumentParser(description=__doc__)
parser.add_argument('--assembly', required=True)
parser.add_argument('--output', required=True)
args = parser.parse_args()
if os.name != 'nt':
    parser.error('The independent oracle requires Windows .NET Framework.')
output = Path(args.output).resolve()
output.mkdir(parents=True, exist_ok=True)
assembly = Path(args.assembly).resolve()
csc = Path(os.environ['WINDIR']) / 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
def run(name, command):
    result = subprocess.run([str(x) for x in command], cwd=root, capture_output=True, encoding='utf-8', errors='replace')
    (output / (name + '.log')).write_text(result.stdout + result.stderr, encoding='utf-8')
    if result.returncode:
        raise RuntimeError(name + ' failed; see ' + str(output / (name + '.log')))
    return result.stdout

def chunk(tag, data):
    return struct.pack('>I', len(data)) + tag + data + struct.pack('>I', zlib.crc32(tag + data))
png = b'\x89PNG\r\n\x1a\n'
png += chunk(b'IHDR', struct.pack('>IIBBBBB', 2, 1, 8, 6, 0, 0, 0))
png += chunk(b'IDAT', zlib.compress(b'\0\xff\0\0\xff\0\0\xff\xff')) + chunk(b'IEND', b'')
(output / 'fixture.png').write_bytes(png)
run('prepare', ['node', '--import', 'tsx', 'scripts/prepare-xemf-validation.mts', output, output / 'fixture.png'])
run('compile', [csc, '/nologo', '/r:System.Drawing.dll', '/r:System.Web.Extensions.dll', '/out:' + str(output / 'Validate.exe'), root / 'scripts/validate-xemf-native.cs'])
result = json.loads(run('native-reader', [output / 'Validate.exe', assembly, output / 'generated.xemf', output / 'native-resaved.xemf']))
assert result['sources'] == 1 and result['firstPower'] == 1000 and result['firstPattern'] == 360
assert result['firstColour'] == -15584170 and result['buildings'] == 2
assert (result['aliasRows'], result['aliasColumns']) == (2, 2)
assert result['firstAliasValue'] == 'A&B <é> 😀'
assert (result['imageWidth'], result['imageHeight'], result['imagePixel']) == (2, 1, -65536)
assert result['nativeBytes'] < result['fileBytes']
script = """
import { readFileSync } from 'node:fs';
import { readProject } from './src/lib/files.ts';
const p=readProject('native-resaved.xemf',new Uint8Array(readFileSync(process.argv[1])));
if(p.sources[0].name!=='Source — العربية 漢字'||p.sources[0].color!=='#123456'||!p.background_image||p.report_aliases.Client!=='A&B <é> 😀')throw new Error('Native re-save lost data');
console.log(JSON.stringify({sources:p.sources.length,image:true,aliases:p.report_aliases}));
"""
run('clone-reader-after-native-resave', ['node', '--import', 'tsx', '--input-type=module', '-e', script, output / 'native-resaved.xemf'])
result.update(status='passed', scope='Self-generated project read by recovered original data types and reserialized by .NET Framework; then reimported by the clone. This does not establish visual original UI execution.', assembly=str(assembly))
(output / 'result.json').write_text(json.dumps(result, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
print(json.dumps({'status': 'passed', 'result': str(output / 'result.json')}))
