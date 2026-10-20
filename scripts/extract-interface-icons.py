"""Extract embedded PNGs from the recovered RESX data without deserializing .NET objects."""
import base64
import hashlib
import json
from pathlib import Path
import struct
import xml.etree.ElementTree as ET

root=Path(__file__).resolve().parents[1]
output=root/"public/original-icons"
output.mkdir(parents=True,exist_ok=True)
manifest=[]
signature=b"\x89PNG\r\n\x1a\n"
for resource in sorted((root/"reference/original-decompiled").glob("*.resx")):
    form=resource.stem.removeprefix("EMFPlanner.")
    for element in ET.parse(resource).findall("data"):
        name=element.attrib.get("name","")
        if not name.endswith(".Image"):continue
        data=base64.b64decode(element.findtext("value") or "")
        start=data.find(signature)
        if start<0:continue
        pos=start+8
        while pos+12<=len(data):
            length=struct.unpack_from(">I",data,pos)[0]
            kind=data[pos+4:pos+8]
            pos+=12+length
            if pos>len(data):raise ValueError(f"Invalid PNG in {resource}:{name}")
            if kind==b"IEND":break
        else:raise ValueError(f"Missing IEND in {resource}:{name}")
        png=data[start:pos]
        filename=f"{form}.{name.removesuffix('.Image')}.png"
        (output/filename).write_bytes(png)
        manifest.append({"file":filename,"source":resource.name,"resource":name,"sha256":hashlib.sha256(png).hexdigest()})
(output/"manifest.json").write_text(json.dumps(manifest,indent=2),encoding="utf-8")
(root/"src/data/icon-index.json").write_text(json.dumps([item["file"] for item in manifest]),encoding="utf-8")
print(f"{len(manifest)} original interface icons extracted")
