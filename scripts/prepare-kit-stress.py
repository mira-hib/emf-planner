"""Read the kit KML with a standard XML parser for the engine workload fixture.

This prepares geographic polygons. Actual application KML import is separately
exercised by BLD-06/PRJ-05 in the browser suite.
"""
import json
from pathlib import Path
import xml.etree.ElementTree as ET

root = Path(__file__).resolve().parents[1]
document = ET.parse(root / 'test/emf_test_kit/data/buildings/buildings_stress_2000.kml').getroot()
ns = {'k': 'http://www.opengis.net/kml/2.2'}
rows = []
for placemark in document.findall('.//k:Placemark', ns):
    height, label = placemark.findtext('k:name', namespaces=ns).split(';', 1)
    points = [list(map(float, value.split(',')[:2])) for value in placemark.find('.//k:coordinates', ns).text.split()]
    if points[0] == points[-1]:
        points.pop()
    rows.append({'name': label, 'height': float(height), 'geo': points})
if len(rows) != 2000:
    raise RuntimeError(f'Expected 2000 kit polygons; found {len(rows)}')
destination = root / '.tools/kit-stress-buildings.json'
destination.parent.mkdir(exist_ok=True)
destination.write_text(json.dumps(rows), encoding='utf-8')
print(f'Prepared {len(rows)} geographic polygons')
