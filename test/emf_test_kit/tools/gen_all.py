#!/usr/bin/env python3
"""Generates every test data file of the EMF Planner 7.1 test kit (stdlib + Pillow)."""
import math, os, random, struct, zipfile
from math import log10, sqrt, atan2, degrees, radians, cos, sin
ROOT = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "data")
def W(path, text, enc="utf-8", newline="\n"):
    p = os.path.join(ROOT, path); os.makedirs(os.path.dirname(p), exist_ok=True)
    with open(p, "w", encoding=enc, newline="") as f: f.write(text.replace("\n", newline))
def WB(path, data):
    p = os.path.join(ROOT, path); os.makedirs(os.path.dirname(p), exist_ok=True)
    with open(p, "wb") as f: f.write(data)
LAT0, LON0 = 45.485337, 9.198818          # origin used in the manual (p.25)
M_LAT = 111132.92                          # m per degree latitude (approx.)
def xy2ll(x, y, lat0=LAT0, lon0=LON0):
    return lat0 + y/M_LAT, lon0 + x/(111412.84*cos(radians(lat0)))

# ---------------- 1. limits ----------------
W("limits/limits_flat.tsv", "1000000\t6\t0\t20\t0\t4.5\t0\t10\t0\n")
W("limits/limits_icnirp.tsv",
  "30\t824\t-1\t1842\t-1\n300\t27.5\t0\t61.4\t0\n1500\t1.585\t0.5\t3.545\t0.5\n1000000\t61.4\t0\t137.3\t0\n")
def icn(f, occ=False):
    e = (1842, 61.4, 3.545, 137.3) if occ else (824, 27.5, 1.585, 61.4)
    if f <= 30: return e[0]/f
    if f <= 300: return e[1]
    if f <= 1500: return e[2]*sqrt(f)
    return e[3]
rows = ["f_MHz\tEL_N1_Vm(general)\tEL_N2_Vm(occupational)\tmeasured_for_100pct_N1\tmeasured_for_25pct_N1\tnote"]
for f, note in [(0.1,"100 kHz lower bound of sum"),(10,""),(29.999,"just below 30"),(30,"boundary (row 1)"),(30.001,"just above 30"),
                (100,""),(299.999,""),(300,"boundary (row 2)"),(300.001,""),(900,"GSM900"),(1499.999,""),(1500,"boundary (row 3)"),
                (1500.001,""),(1800,""),(2100,""),(2600,""),(23000,""),(300000,"300 GHz upper bound"),(1000000,"last table row"),
                (1000001,"beyond table: observe")]:
    e1, e2 = icn(f), icn(f, True)
    rows.append(f"{f}\t{e1:.4f}\t{e2:.4f}\t{e1:.4f}\t{e1/2:.4f}\t{note}")
W("limits/limit_check_icnirp.tsv", "\n".join(rows)+"\n")

# ---------------- 2. sources ----------------
HDR = "Radiation source name\tSector ID\tType of service\tFrequency, MHz\tTransmitter power, W\tNumber of TX\tTotal atten., dB\tAntenna model\tAntenna azimuth, deg\tAntenna gain, dBi\tAntenna height, m\tAntenna size, m"
S = [("TS01_900_man","A","GSM",900,12.05,1,0,"742265V02",60,16,29,1.9),
     ("TS02_2100_3tx","B","UMTS",2100,10.42,3,0,"742265V02",130,18.3,29,1.9),
     ("TS03_23GHz_PtP","C","PtP",23000,0.1995,1,0,"23G 0.6",125,40,27.2,0.6),
     ("TS04_18GHz_PtP","C","PtP",18000,0.2512,1,0,"18G 0.6",220,39.2,27.2,0.6),
     ("TS05_LTE_att","A","LTE",2600,20,1,0.37,"AMB4519",180,18,40,1.5),
     ("TS06_ISO_1W","-","Other",900,1,1,0,"isotropic",0,0,10,0.1),
     ("TS07_ISO_1kW","-","Other",900,1000,1,0,"isotropic",0,0,10,0.1)]
W("sources/sources_oracle.tsv", HDR+"\n"+"\n".join("\t".join(str(v) for v in s) for s in S)+"\n")
cols = list(zip(*S)); names = HDR.split("\t")
for i, n in enumerate(names):
    W(f"sources/columns/col{i+1:02d}_{n.split(',')[0].replace(' ','_').replace('.','')}.txt", "\n".join(str(v) for v in cols[i])+"\n")
exp = ["name\tP_port_W\tEIRP_W\tE_at_10m_Vm\tLm_6Vm_m\tLm_20Vm_m\tLm_4.5Vm_m\tLm_10Vm_m"]
for s in S:
    P=s[4]*s[5]*10**(-s[6]/10); eirp=P*10**(s[9]/10); k=sqrt(30*eirp)
    exp.append(f"{s[0]}\t{P:.4f}\t{eirp:.2f}\t{k/10:.4f}\t"+"\t".join(f"{k/e:.2f}" for e in (6,20,4.5,10)))
W("sources/expected_from_formulas.tsv", "\n".join(exp)+"\n")
random.seed(7)
big=[HDR]
for i in range(1000):
    big.append(f"STRESS_{i:04d}\tS{i%3}\tLTE\t{random.choice([700,800,900,1800,2100,2600,3500])}\t{random.choice([5,10,20,40,60])}\t{random.choice([1,2,4])}\t{random.choice([0,0.3,0.5,1])}\tgeneric\t{random.randrange(0,360,10)}\t{random.choice([15,17,18,18.5])}\t{random.choice([20,25,30,35,40])}\t{random.choice([1.3,1.5,2.0])}")
W("sources/sources_stress_1000.tsv", "\n".join(big)+"\n")

# ---------------- 3. units ----------------
u=["W\tmW\tdBW\tdBm\tcomment"]
for w,c in [(12.05,"manual p.13: 12050 mW, 10.8 dBW, 40.8 dBm"),(1,""),(0.001,""),(100,""),(0.1,"20 dBm"),(1e-12,"tiny"),(1e6,"huge"),(0.5,"")]:
    u.append(f"{w}\t{w*1000}\t{10*log10(w):.4f}\t{10*log10(w)+30:.4f}\t{c}")
u.append("0\t0\t-inf\t-inf\tedge: log of zero")
u.append("-5\t-5000\tundefined\tundefined\tedge: negative power")
W("units/unit_conversion_cases.tsv", "\n".join(u)+"\n")

# ---------------- 4. coordinates ----------------
c=["X_m\tY_m\tR_m\tazimuth_deg(clockwise from north)"]
for x,y in [(0.3,-5.1),(-28.2,-81.8),(74.3,-81.1),(111.3,33.5),(30.7,72),(-40.3,68.8),(0,100),(100,0),(0,-100),(-100,0),(0,0),(1e-9,1e-9),(70.71,70.71)]:
    R=sqrt(x*x+y*y); az=degrees(atan2(x,y))%360 if R>0 else float('nan')
    c.append(f"{x}\t{y}\t{R:.2f}\t{az:.2f}")
W("coords/xy_to_polar.tsv","\n".join(c)+"\n")
def dms(v,pos,neg):
    h=pos if v>=0 else neg; t=round(abs(v)*3600,2); d=int(t//3600); t-=d*3600; m=int(t//60); sec=t-m*60
    return f"{d}°{m:02d}'{sec:05.2f}\" {h}"
def dm(v,pos,neg):
    h=pos if v>=0 else neg; t=round(abs(v)*60,4); d=int(t//60); mm=t-d*60
    return f"{d}°{mm:07.4f}' {h}"
g=["lat\tlon\tDD\tDMS\tDM"]
for lat,lon in [(LAT0,LON0),(0,0),(-33.8688,151.2093),(5.3600,-4.0083),(90,180),(-90,-180),(10.999999,10.999999),(44.345678,-134.567893)]:
    g.append(f"{lat}\t{lon}\t{lat:.6f}, {lon:.6f}\t{dms(lat,'N','S')} {dms(lon,'E','W')}\t{dm(lat,'N','S')} {dm(lon,'E','W')}")
W("coords/latlon_formats.tsv","\n".join(g)+"\n")
W("coords/paste_coordinates.txt", """45.485337, 9.198818
45.485337 9.198818
45,485337; 9,198818
N45.485337 E9.198818
45°29'07.2"N 9°11'55.7"E
45°29.1202'N 9°11.9291'E
   45.485337 ,   9.198818   
9.198818, 45.485337
91.0, 9.198818
45.485337, 181.0
abc, def
45.485337
,
""")

# ---------------- 5. buildings (KML) ----------------
def poly(pts):  # pts in metres (x,y) -> kml ring
    ring=[xy2ll(x,y) for x,y in pts]+[xy2ll(*pts[0])]
    return " ".join(f"{lo:.7f},{la:.7f},0" for la,lo in ring)
def placemark(name, pts, inner=None, raw=None):
    ib=f"<innerBoundaryIs><LinearRing><coordinates>{poly(inner)}</coordinates></LinearRing></innerBoundaryIs>" if inner else ""
    co=raw if raw is not None else poly(pts)
    return f"<Placemark><name>{name}</name><Polygon><outerBoundaryIs><LinearRing><coordinates>{co}</coordinates></LinearRing></outerBoundaryIs>{ib}</Polygon></Placemark>"
def kml(items, bom=False):
    return ("\ufeff" if bom else "")+'<?xml version="1.0" encoding="UTF-8"?>\n<kml xmlns="http://www.opengis.net/kml/2.2"><Document>\n'+"\n".join(items)+"\n</Document></kml>\n"
sq=lambda cx,cy,a:[(cx-a/2,cy-a/2),(cx+a/2,cy-a/2),(cx+a/2,cy+a/2),(cx-a/2,cy+a/2)]
ok=[placemark("10;B01",sq(60,0,20)),placemark("12.5;B02",[(-60,-10),(-30,-10),(-30,10),(-60,10)]),
    placemark("15;L-shape",[(0,40),(40,40),(40,55),(20,55),(20,80),(0,80)]),placemark("4;2",sq(-40,-40,8))]
W("buildings/buildings_ok.kml", kml(ok))
edge=[placemark("8",sq(100,100,10)),placemark(";NoHeight",sq(100,130,10)),placemark("abc;NotNumber",sq(100,160,10)),
 placemark("0;Zero",sq(130,100,10)),placemark("-5;Negative",sq(130,130,10)),placemark("10000;Huge",sq(130,160,10)),
 placemark("12,5;CommaDecimal",sq(160,100,10)),placemark("9;Église é ü 北京 ✓",sq(160,130,10)),
 placemark("9;a;b;c",sq(160,160,10)),placemark("9;Hole",sq(-100,100,30),inner=sq(-100,100,10)),
 placemark("9;Collinear",[(190,100),(200,100),(210,100)]),placemark("9;Bowtie",[(190,130),(200,140),(200,130),(190,140)]),
 placemark("10;B01",sq(60,0,20)),
 placemark("5;Tiny",sq(-130,-100,0.5)),placemark("20;Far10km",sq(10000,10000,30)),
 placemark("9;NoAltitude",None,raw=" ".join(f"{lo:.7f},{la:.7f}" for la,lo in [xy2ll(x,y) for x,y in sq(-160,100,10)+[sq(-160,100,10)[0]]])),
 placemark("9;Unclosed",None,raw=" ".join(f"{lo:.7f},{la:.7f},0" for la,lo in [xy2ll(x,y) for x,y in sq(-160,130,10)])),
 "<Placemark><name>7;PointNotPolygon</name><Point><coordinates>%.7f,%.7f,0</coordinates></Point></Placemark>"%(xy2ll(0,-150)[1],xy2ll(0,-150)[0]),
 "<Placemark><name>7;LineNotPolygon</name><LineString><coordinates>%.7f,%.7f,0 %.7f,%.7f,0</coordinates></LineString></Placemark>"%(xy2ll(0,-170)[1],xy2ll(0,-170)[0],xy2ll(20,-170)[1],xy2ll(20,-170)[0])]
W("buildings/buildings_edge.kml", kml(edge))
W("buildings/buildings_malformed_truncated.kml", kml(ok)[:-60])
W("buildings/buildings_empty.kml", kml([]))
W("buildings/buildings_utf8_bom.kml", kml(ok, bom=True))
W("buildings/buildings_crlf.kml", kml(ok), newline="\r\n")
W("buildings/buildings_utf16.kml", kml(ok), enc="utf-16")
many=[placemark(f"{10+i%20};B{i:04d}",sq((i%50)*25-600,(i//50)*25-250,15)) for i in range(2000)]
W("buildings/buildings_stress_2000.kml", kml(many))
# shielding scenario: source at origin, wall east, control point behind
W("buildings/shield_scenario.kml", kml([placemark("50;WALL",[(30,-30),(50,-30),(50,30),(30,30)]),
    placemark("20;INSIDE",[(95,-10),(115,-10),(115,10),(95,10)])]))

# ---------------- 6. MSI antenna patterns ----------------
def msi(name, gain, hfun, vfun, freq=900, unit="dBi", hn=360, vn=360, extra=""):
    L=[f"NAME {name}","MAKE EMF-test-kit",f"FREQUENCY {freq}","H_WIDTH 65","V_WIDTH 7",f"GAIN {gain} {unit}","TILT ELECTRICAL 0"]+([extra] if extra else [])
    L.append("HORIZONTAL 360"); L+= [f"{a} {hfun(a):.2f}" for a in range(hn)]
    L.append("VERTICAL 360");   L+= [f"{a} {vfun(a):.2f}" for a in range(vn)]
    return "\n".join(L)+"\n"
def wrap(a): return a-360 if a>180 else a
sector_h=lambda a: min(12*(wrap(a)/65)**2,25)
sector_v=lambda a: min(12*(wrap(a)/7)**2,20)
zero=lambda a:0
W("msi/ok_isotropic.msi", msi("iso_0dBi",0,zero,zero))
W("msi/ok_sector65x7_17dBi.msi", msi("sector65x7",17,sector_h,sector_v))
W("msi/gain_in_dBd.msi", msi("iso_0dBd",0,zero,zero,unit="dBd"))
W("msi/ok_lowercase_keywords.msi", msi("lower",17,sector_h,sector_v).replace("NAME","name").replace("GAIN","gain").replace("HORIZONTAL","horizontal").replace("VERTICAL","vertical"))
W("msi/ok_crlf.msi", msi("crlf",17,sector_h,sector_v), newline="\r\n")
W("msi/ok_long_unicode_name.msi", msi("é"*150+"_北京_"+"x"*150,17,sector_h,sector_v))
W("msi/bad_no_vertical.msi", msi("novert",17,sector_h,sector_v).split("VERTICAL")[0])
W("msi/bad_horizontal_359_lines.msi", msi("h359",17,sector_h,sector_v,hn=359))
W("msi/bad_horizontal_361_lines.msi", msi("h361",17,sector_h,sector_v,hn=361))
W("msi/bad_negative_attenuation.msi", msi("neg",17,lambda a:-3 if a==10 else sector_h(a),sector_v))
W("msi/bad_nonnumeric_value.msi", msi("nan",17,sector_h,sector_v).replace("10 ","10 abc#",1))
W("msi/bad_huge_attenuation.msi", msi("huge",17,lambda a:999 if a==180 else sector_h(a),sector_v))
W("msi/bad_missing_gain.msi", "\n".join(l for l in msi("nogain",17,sector_h,sector_v).split("\n") if not l.startswith("GAIN")))
W("msi/bad_comma_decimal.msi", msi("comma",17,sector_h,sector_v).replace("HORIZONTAL 360\n0 0.00","HORIZONTAL 360\n0 0,50"))
W("msi/bad_unsorted_duplicate_angles.msi", msi("dup",17,sector_h,sector_v).replace("\n5 ","\n3 ",1))
W("msi/bad_gain_text.msi", msi("gaintext",17,sector_h,sector_v).replace("GAIN 17 dBi","GAIN seventeen dBi"))
W("msi/bad_empty.msi","")
WB("msi/bad_binary.msi", bytes(random.Random(1).randrange(256) for _ in range(4096)))
W("msi/ok_utf8_bom.msi", msi("bom",17,sector_h,sector_v).join(["\ufeff",""]) if False else "\ufeff"+msi("bom",17,sector_h,sector_v))

# ---------------- 7. F.699-8 oracle ----------------
def f699(freq_mhz, gmax, d, phi):
    lam=299.792458/freq_mhz; dl=d/lam
    g1 = 2+15*log10(dl) if dl<=100 else -1+15*log10(dl)
    phim=20/dl*sqrt(max(gmax-g1,0))
    if phi<phim: return gmax-2.5e-3*(dl*phi)**2
    if dl<=100:
        if phi<100/dl: return g1
        if phi<48: return 52-10*log10(dl)-25*log10(phi)
        return 10-10*log10(dl)
    phir=15.85*dl**-0.6
    if phi<phir: return g1
    if phi<48: return 29-25*log10(phi)
    return -13
rows=["phi_deg\tG_dBi(f=7000,Gmax=31,D=0.6)\trelative_att_dB"]
for phi in [0,1,2,3,4,4.9,5,6,7,7.2,10,20,30,47.9,48,90,180]:
    g=f699(7000,31,0.6,phi); rows.append(f"{phi}\t{g:.2f}\t{31-g:.2f}")
W("msi/f699_expected_7GHz_31dBi_0.6m.tsv","\n".join(rows)+"\n")

# ---------------- 8. images ----------------
from PIL import Image, ImageDraw
im=Image.new("RGB",(1000,800),"white"); d=ImageDraw.Draw(im)
for i in range(0,1001,100):
    d.line([(i,0),(i,800)],fill=(180,180,180),width=1); d.text((i+3,3),str(i),fill="black")
for j in range(0,801,100):
    d.line([(0,j),(1000,j)],fill=(180,180,180),width=1); d.text((3,j+3),str(j),fill="black")
d.line([(100,100),(600,100)],fill="red",width=3); d.text((300,80),"REF A: 500 px = 100 m",fill="red")
d.line([(100,300),(400,300)],fill="blue",width=3); d.text((200,280),"CHECK B: 300 px -> expect 60 m",fill="blue")
d.line([(700,200),(700,700)],fill="green",width=3); d.text((710,400),"CHECK C: 500 px -> expect 100 m (vertical)",fill="green")
d.ellipse([(95,95),(105,105)],outline="black")
os.makedirs(ROOT+"/images",exist_ok=True)
im.save(ROOT+"/images/calib_grid.png"); im.save(ROOT+"/images/calib_grid.jpg",quality=92); im.save(ROOT+"/images/calib_grid.bmp")
im.save(ROOT+"/images/calib_grid.gif"); im.save(ROOT+"/images/calib_grid.tif")
im.rotate(30,expand=True,fillcolor="white").save(ROOT+"/images/calib_grid_rot30.png")
Image.new("RGB",(1,1),"red").save(ROOT+"/images/edge_1x1.png")
Image.new("RGB",(9000,9000),"white").save(ROOT+"/images/edge_9000x9000.png")
Image.new("RGBA",(500,500),(255,0,0,80)).save(ROOT+"/images/edge_transparent.png")
Image.new("L",(500,500),128).save(ROOT+"/images/edge_grayscale.png")
Image.new("CMYK",(500,500),(0,255,255,0)).save(ROOT+"/images/edge_cmyk.jpg")
WB("images/edge_corrupt.png", open(ROOT+"/images/calib_grid.png","rb").read()[:600])
WB("images/edge_empty.png", b"")
WB("images/edge_text_renamed.png", b"this is not an image\n")

# ---------------- 9. fuzz & aliases ----------------
strings=["", " ", "   leading and trailing   ", "A"*300, "B"*5000, "Ünïcödé éàçñ 日本語 中文 العربية עברית", "emoji 😀📡⚡", "RTL \u202eevil\u202c text",
 "quote ' and \" and `", "semi;colon;list", "back\\slash C:\\temp\\x", "../../etc/passwd", "<script>alert(1)</script>", "&amp; <b>bold</b> &lt;", "%s %d %n {0} {1}", "{SiteName}", "{RadiatingSources_1}",
 "{{nested}}", "'; DROP TABLE sources;--", "=cmd|' /C calc'!A0", "tab\there", "CON", "NUL.txt", "name with 	tab", "line1\\nline2 (literal)", "0÷360", "∞", "​zero-width​space"]
W("fuzz/strings_text.txt","\n".join(s for s in strings if "\\n" not in s or True)+"\n")
nums=["","0","-0","-1","+5","5.","  .5","1e3","1E-3","1,5","1.5","1 500","1'500","1.2.3","--5","1..2","0x10","12abc","abc","NaN","Infinity","-Infinity","∞","1e400","1e-400","2147483647","2147483648","4294967296","9223372036854775808","0.1+0.2","١٢٣","１２３","3,14159","3.14159265358979323846264338327950288"]
W("fuzz/numbers_text.txt","\n".join(nums)+"\n")
b=["field\tvalues_to_try"]
b+= ["frequency_MHz\t0 | 0.001 | 0.099999 | 0.1 | 300000 | 300000.001 | 1000000 | 1000000.001 | -900",
     "power_W\t0 | 1e-12 | 1e6 | 1e9 | -1",
     "number_of_TX\t0 | 1 | 1.5 | 1000 | -1",
     "total_atten_dB\t-10 | 0 | 100 | 1000",
     "gain_dBi\t-30 | 0 | 60 | 200",
     "height_m\t0 | -5 | 0.01 | 1000 | 100000",
     "azimuth_deg\t0 | 360 | 360.0001 | -1 | 720 | x (non numeric -> '0÷360' per manual p.16)",
     "mech_tilt_deg\t-90 | -91 | 0 | 90 | 91",
     "reflection_coef\t0.99 | 1 | 1.3 | 1.6 | 1.61 | 0 | -1 | 1,3",
     "matrix_size\t0 | 49 | 50 | 800 | 1000 | 1001 | 100000 | abc",
     "grid_dimension_m\t0 | -100 | 0.5 | 100 | 100000 | 1e9",
     "building_height_m\t0 | -5 | 3 | 12.5 | 10000"]
W("fuzz/boundaries.tsv","\n".join(b)+"\n")
W("aliases/custom_aliases.tsv","{SiteName}\tTEST SITE é/ü 😀\n{ProjectName}\tProject <A&B> \"quoted\"\n{SiteAddress}\t"+"Long address "*80+"\n{Customer}\tACME;Corp\n{Designer}\t\n{Date}\t2026-10-20\n{RadiatingSources_1}\tcollision with standard alias\n{Unused}\tnever used in template\n{SiteName}\tduplicate name\n{ with space }\tspace in name\n")

# ---------------- 10. control points (Excel import) ----------------
cp=["Latitude\tLongitude\tX_m\tY_m\tLocation\tDescription\tmeasured_E_Vm@h1.1"]
for i,(x,y,loc) in enumerate([(100,0,"Ground"),(200,0,"Ground"),(0,100,"Ground"),(70.71,70.71,"Ground"),(105,0,"Building"),(0,0,"Ground")]):
    la,lo=xy2ll(x,y); cp.append(f"{la:.6f}\t{lo:.6f}\t{x}\t{y}\t{loc}\tCP{i+1}\t0")
W("control_points/control_points_oracle.tsv","\n".join(cp)+"\n")
# isotropic oracle table: 1 W, 0 dBi, source at (0,0) height 10 m; points at 1.5 m
rows=["point\tdist_horiz_m\tdz_m\tr_3D_m\tE_expected_Vm(k=1)\tratio_to_previous_point"]
prev=None
for x in (50,100,200,400):
    r=sqrt(x*x+8.5**2); E=sqrt(30*1*1)/r; rows.append(f"({x},0)\t{x}\t8.5\t{r:.3f}\t{E:.5f}\t{'' if prev is None else f'{E/prev:.4f}'}"); prev=E
W("control_points/iso_expected_1W_0dBi_h10_cp1.5.tsv","\n".join(rows)+"\n")

# ---- extra MSI: envelope set + backfire ----
for i,(t,g) in enumerate([(0,17.0),(3,16.8),(6,16.5)]):
    W(f"msi/envelope/env_tilt{t}.msi", msi(f"env_tilt{t}",g,sector_h,lambda a,t=t: min(12*(wrap(a-t)/7)**2,20)))
W("msi/ok_backfire_180.msi", msi("backfire180",17,lambda a: min(12*(wrap(a-180)/65)**2,25),sector_v))
# ---- F.699 oracle, D/lambda > 100 branch ----
rows=["phi_deg\tG_dBi(f=40000,Gmax=50,D=2.0)\trelative_att_dB"]
for phi in [0,0.1,0.2,0.3,0.5,1,2,5,10,20,47.9,48,90,180]:
    g=f699(40000,50,2.0,phi); rows.append(f"{phi}\t{g:.2f}\t{50-g:.2f}")
W("msi/f699_expected_40GHz_50dBi_2.0m.tsv","\n".join(rows)+"\n")
# ---- isotropic 1 W vertical-cut oracle (antenna h=10 m) ----
rows=["R_m\tH_m\tr_3D_m\tE_expected_Vm"]
for R,H in [(5,10),(10,10),(50,10),(100,10),(50,20),(100,0),(20,30),(0,0)]:
    r=sqrt(R*R+(H-10)**2); rows.append(f"{R}\t{H}\t{r:.3f}\t{sqrt(30)/r:.5f}")
W("control_points/iso_expected_vertical_cut_1W.tsv","\n".join(rows)+"\n")
# ---- horizontal contour radii for TS07 (1 kW isotropic, h=10 m) ----
rows=["limit\tEL_Vm\tr_sphere_m\tR_at_dz0_m\tR_at_dz10_m(cut height 20 m)\tR_at_dz-10_m(cut height 0 m)"]
for n,e in (("N1",6),("N2",20),("N3",4.5),("N4",10)):
    r=sqrt(30*1000)/e; rows.append(f"{n}\t{e}\t{r:.2f}\t{r:.2f}\t{sqrt(max(r*r-100,0)):.2f}\t{sqrt(max(r*r-100,0)):.2f}")
W("sources/iso_1kW_contour_radii.tsv","\n".join(rows)+"\n")
print("kit data written under", os.path.abspath(ROOT))
