#!/usr/bin/env python3
"""Local synthetic tile server to test the base-map URL templates of EMF Planner (Settings > Base map settings).
Run:  python3 tile_server.py      then add base maps (Map Name / URL):
   LOCAL_XYZ   http://127.0.0.1:8000/t/[Z]/[X]/[Y].png
   LOCAL_QUERY http://127.0.0.1:8000/g?x=[X]&y=[Y]&zoom=[17-Z]
Each tile shows its own z/x/y and its lat/lon corner, and every request is logged with its parsed values."""
import io, math, sys, re
from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse, parse_qs
from PIL import Image, ImageDraw
def tile_nw(z, x, y):
    n = 2**z; lon = x/n*360-180; lat = math.degrees(math.atan(math.sinh(math.pi*(1-2*y/n)))); return lat, lon
def png(z, x, y, tag):
    im = Image.new("RGB", (256, 256), ((x*37) % 200+55, (y*53) % 200+55, (z*17) % 200+55)); d = ImageDraw.Draw(im)
    d.rectangle([0, 0, 255, 255], outline="black"); la, lo = tile_nw(z, x, y)
    d.text((8, 8), f"{tag} z={z}", fill="black"); d.text((8, 28), f"x={x}", fill="black"); d.text((8, 48), f"y={y}", fill="black")
    d.text((8, 80), f"NW lat {la:.5f}", fill="black"); d.text((8, 100), f"NW lon {lo:.5f}", fill="black")
    b = io.BytesIO(); im.save(b, "PNG"); return b.getvalue()
class H(BaseHTTPRequestHandler):
    def do_GET(s):
        u = urlparse(s.path); z = x = y = None; tag = "?"
        m = re.match(r"/t/(\d+)/(\d+)/(\d+)\.png", u.path)
        if m: z, x, y = map(int, m.groups()); tag = "XYZ"
        elif u.path == "/g":
            q = parse_qs(u.query); x, y, zoom = int(q["x"][0]), int(q["y"][0]), int(q["zoom"][0]); z = 17-zoom; tag = "QUERY(zoom=17-Z)"
        print(f"REQUEST {s.path}  -> z={z} x={x} y={y}", flush=True)
        if z is None: s.send_error(404); return
        data = png(z, x, y, tag); s.send_response(200); s.send_header("Content-Type", "image/png"); s.send_header("Content-Length", str(len(data))); s.end_headers(); s.wfile.write(data)
    def log_message(s, *a): pass
if __name__ == "__main__":
    lat, lon = 45.485337, 9.198818
    for z in (10, 14, 18, 19):
        n = 2**z; print(f"z={z}: tile containing the manual origin (45.485337, 9.198818) = x {int((lon+180)/360*n)}, y {int((1-math.asinh(math.tan(math.radians(lat)))/math.pi)/2*n)}")
    print("serving on http://127.0.0.1:8000  (Ctrl+C to stop)"); HTTPServer(("127.0.0.1", 8000), H).serve_forever()
