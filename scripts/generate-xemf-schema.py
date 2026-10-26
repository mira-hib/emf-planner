"""Extract data field layouts from the retained native reconstruction (no execution)."""
import json
import pathlib
import re

root = pathlib.Path(__file__).resolve().parent.parent
source = (root / 'reference/original-decompiled/EMFPlanner/Public2.cs').read_text(encoding='utf-8-sig')
schemas = {}
for match in re.finditer(r'public struct (\w+)\s*\{', source):
    start = match.end()
    depth, end = 1, start
    while depth:
        depth += (source[end] == '{') - (source[end] == '}')
        end += 1
    fields = re.findall(r'^\t\tpublic ([\w<>\[\],]+) (\w+);', source[start:end], re.M)
    schemas[match[1]] = fields
destination = root / 'src/lib/xemf-schema.json'
destination.write_text(json.dumps(schemas, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
print(json.dumps({'types': len(schemas), 'fields': sum(map(len, schemas.values()))}))
