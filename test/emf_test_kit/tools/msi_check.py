#!/usr/bin/env python3
"""Usage: python3 msi_check.py exported.msi 7000 31 0.6   -> compares an MSI exported by EMF Planner (ITU-R F.699-8 generator)
with the independent oracle; prints max deviation (dB) in horizontal plane. Tolerance suggested: 0.3 dB (0.5 dB near phi_m)."""
import sys, math
from math import log10, sqrt
def f699(fmhz, gmax, d, phi):
    lam = 299.792458/fmhz; dl = d/lam; g1 = 2+15*log10(dl) if dl <= 100 else -1+15*log10(dl); phim = 20/dl*sqrt(max(gmax-g1, 0))
    if phi < phim: return gmax-2.5e-3*(dl*phi)**2
    if dl <= 100:
        if phi < 100/dl: return g1
        return 52-10*log10(dl)-25*log10(phi) if phi < 48 else 10-10*log10(dl)
    phir = 15.85*dl**-0.6
    if phi < phir: return g1
    return 29-25*log10(phi) if phi < 48 else -13
def read(path):
    sec = None; H = {}; V = {}
    for ln in open(path, encoding="utf-8-sig", errors="replace"):
        t = ln.split()
        if not t: continue
        if t[0].upper() == "HORIZONTAL": sec = H; continue
        if t[0].upper() == "VERTICAL": sec = V; continue
        if sec is not None:
            try: sec[float(t[0].replace(",", "."))] = float(t[1].replace(",", "."))
            except Exception: pass
    return H, V
if __name__ == "__main__":
    p, f, g, d = sys.argv[1], float(sys.argv[2]), float(sys.argv[3]), float(sys.argv[4]); H, V = read(p)
    print("points: H", len(H), "V", len(V)); worst = (0, None)
    for a, att in sorted(H.items()):
        phi = a if a <= 180 else 360-a; exp = g-f699(f, g, d, phi); dev = abs(att-exp)
        if dev > worst[0]: worst = (dev, (a, att, round(exp, 2)))
    print("max deviation dB:", round(worst[0], 3), "at (angle, file, oracle) =", worst[1])
