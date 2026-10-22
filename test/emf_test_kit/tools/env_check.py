#!/usr/bin/env python3
"""Usage: python3 env_check.py envelope_out.msi env1.msi env2.msi ...
Property: with 'Use antenna gain' ticked, absolute envelope gain(angle) = max_i (G_i - att_i(angle)) in both planes."""
import sys
def load(p):
    g = 0.0; sec = None; H = {}; V = {}
    for ln in open(p, encoding="utf-8-sig", errors="replace"):
        t = ln.split()
        if not t: continue
        k = t[0].upper()
        if k == "GAIN": g = float(t[1].replace(",", ".")); continue
        if k == "HORIZONTAL": sec = H; continue
        if k == "VERTICAL": sec = V; continue
        if sec is not None:
            try: sec[int(float(t[0]))] = float(t[1])
            except ValueError: pass
    return g, H, V
out = load(sys.argv[1]); ins = [load(p) for p in sys.argv[2:]]
for name, idx in (("H", 1), ("V", 2)):
    worst = 0.0
    for a in range(360):
        exp = max(i[0]-i[idx].get(a, 0) for i in ins); got = out[0]-out[idx].get(a, 0); worst = max(worst, abs(exp-got))
    print(name, "max deviation (dB):", round(worst, 3))
