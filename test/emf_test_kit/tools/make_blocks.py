#!/usr/bin/env python3
"""Builds guide/blocks.tex (copy-paste blocks) directly from data/ so that the PDF and the files never diverge."""
import os, csv
H = os.path.dirname(os.path.abspath(__file__)); D = os.path.join(H, "..", "data")
def rd(p): return open(os.path.join(D, p), encoding="utf-8").read().splitlines()
out = []
def esc(t): return t.replace("\\", "\\textbackslash{}").replace("_", "\\_").replace("&", "\\&").replace("%", "\\%").replace("#", "\\#").replace("{", "\\{").replace("}", "\\}").replace("$", "\\$")
def block(code, title, lines, width=None):
    out.append("\\paragraph*{Bloc \\code{%s} --- %s}\\vspace{-2pt}" % (code, title))
    out.append("\\begin{Verbatim}[frame=single,framesep=1.5mm,fontsize=\\footnotesize,rulecolor=\\color{accent}]")
    out.extend(lines); out.append("\\end{Verbatim}")
# A-1..A-12 : source columns, side by side by 3
tsv = [l.split("\t") for l in rd("sources/sources_oracle.tsv")]
hdr, rows = tsv[0], tsv[1:]
out.append("\\subsection{Sources de référence : une colonne par bloc (\\code{A-1} à \\code{A-12})}\\label{sec:blocsA}")
out.append("Sélectionner un bloc, copier, cliquer sur la \\textbf{première cellule} de la colonne correspondante dans \\ui{Radiating Sources}, puis \\code{Ctrl+V}. Les sept lignes sont \\code{TS01}\\ldots\\code{TS07}.")
for i in range(0, 12, 3):
    out.append("\\noindent")
    for j in range(i, i+3):
        out.append("\\begin{minipage}[t]{0.325\\textwidth}\\small\\textbf{A-%d} %s\\\\[-2pt]" % (j+1, esc(hdr[j])))
        out.append("\\begin{Verbatim}[frame=single,framesep=1mm,fontsize=\\footnotesize,rulecolor=\\color{accent}]")
        out.extend(r[j] for r in rows); out.append("\\end{Verbatim}"); out.append("\\end{minipage}\\hfill")
    out.append("\\par\\medskip")
out.append("\\begin{note}Pour \\code{TS07} (1~kW isotrope), l'oracle des contours circulaires (\\code{HCT-04}) suppose une \\textbf{hauteur d'antenne de 10~m} et le diagramme \\code{ok\\_isotropic.msi}. Pour \\code{BLD-11}, passez-la à 20~m.\\end{note}")
out.append("\\subsection{Limites, origine, formats}\\label{sec:blocsC}")
out.append("\\paragraph*{Bloc \\code{C-1} --- limites plates (9 valeurs, de gauche à droite dans la ligne 1 du tableau \\ui{EMF Limits})}")
out.append("\\begin{center}\\small\\begin{tabular}{@{}ccccccccc@{}}\\toprule \\textbf{Freq.\\ to F} & \\textbf{E1} & \\textbf{A1} & \\textbf{E2} & \\textbf{A2} & \\textbf{E3} & \\textbf{A3} & \\textbf{E4} & \\textbf{A4}\\\\\\midrule 1000000 & 6 & 0 & 20 & 0 & 4.5 & 0 & 10 & 0\\\\\\bottomrule\\end{tabular}\\end{center}")
out.append("Fichier à coller en une fois : \\code{data/limits/limits\\_flat.tsv}.")
block("C-2", "chaînes à coller pour l'origine (une ligne à la fois, \\code{ORI-02})", [l for l in rd("coords/paste_coordinates.txt") if l.strip()!=","])
out.append("\\paragraph*{Bloc \\code{C-3} --- table ICNIRP du manuel (p.21), 4 lignes}")
out.append("\\begin{center}\\small\\begin{tabular}{@{}rrrrr@{}}\\toprule \\textbf{Freq.\\ to F} & \\textbf{E1} & \\textbf{A1} & \\textbf{E2} & \\textbf{A2}\\\\\\midrule 30 & 824 & $-1$ & 1842 & $-1$\\\\ 300 & 27.5 & 0 & 61.4 & 0\\\\ 1500 & 1.585 & 0.5 & 3.545 & 0.5\\\\ 1000000 & 61.4 & 0 & 137.3 & 0\\\\\\bottomrule\\end{tabular}\\end{center}")
out.append("Fichier à coller : \\code{data/limits/limits\\_icnirp.tsv}.\\\\ Valeurs attendues de $E_L$ aux fréquences de test : \\code{data/limits/limit\\_check\\_icnirp.tsv}.")
g = [l.split("\t") for l in rd("coords/latlon_formats.tsv")]
out.append("\\paragraph*{Bloc \\code{C-4} --- aller-retour des formats de coordonnées (\\code{ORI-03})}")
out.append("\\begin{center}\\scriptsize\\renewcommand{\\arraystretch}{1.1}\\begin{tabular}{@{}rrp{4.6cm}p{5.2cm}@{}}\\toprule \\textbf{lat} & \\textbf{lon} & \\textbf{DMS attendu} & \\textbf{DM attendu}\\\\\\midrule")
for r in g[1:]:
    dms = r[3].replace("\"", "''"); dm = r[4]
    out.append("%s & %s & %s & %s\\\\" % (r[0], r[1], esc(dms), esc(dm)))
out.append("\\bottomrule\\end{tabular}\\end{center}")
out.append("\\subsection{Textes et nombres pour la robustesse}\\label{sec:blocsF}")
latin = []
for l in rd("fuzz/strings_text.txt"):
    if l.strip() == "" or len(l) > 80: continue
    if any(ord(c) > 0x24F or ord(c) in range(0x200B, 0x2070) for c in l): continue
    if "\\n" in l or "\t" in l: continue
    latin.append(l)
block("F-1", "chaînes de texte (une par ligne ; les lignes vides, très longues et multi-scripts sont dans \\code{fuzz/strings\\_text.txt})", latin)
out.append("Chaînes longues à fabriquer au besoin : \\code{python3 -c \"print('A'*5000)\"} (5000 caractères). Pour l'Unicode multi-scripts, utilisez le fichier : les polices du PDF ne les contiennent pas.")
block("F-2", "nombres écrits de façon piégeuse (\\code{ROB-02})", [l for l in rd("fuzz/numbers_text.txt") if l.strip() != "" and not any(ord(c) > 0x24F for c in l)])
out.append("\\paragraph*{Bloc \\code{F-3} --- bornes à essayer par champ (\\code{fuzz/boundaries.tsv})}")
out.append("\\begin{center}\\scriptsize\\renewcommand{\\arraystretch}{1.1}\\begin{tabular}{@{}lp{12.2cm}@{}}\\toprule \\textbf{Champ} & \\textbf{Valeurs} \\\\\\midrule")
for l in rd("fuzz/boundaries.tsv")[1:]:
    a, b = l.split("\t"); out.append("%s & %s\\\\" % (esc(a), esc(b).replace("\\textbackslash{}", "\\textbackslash{}")))
out.append("\\bottomrule\\end{tabular}\\end{center}")
block("L-1", "étiquettes de tour (\\code{LBL-03}, \\code{LBL-04})", ["Tower/15/15/0.5/1.2","Tower/15/0/0.5/0.5","Tower/15/15/0.5","tower/15/15/0.5/1.2","Tower/-5/15/0.5/1.2","Tower/0/0/0/0","Tower/15/15/0,5/1,2","Tower / 15 / 15 / 0.5 / 1.2","Tower/abc/15/0.5/1.2","Tower/1e3/1e3/5/5","Tower////"])
al = [l.split("\t") for l in rd("aliases/custom_aliases.tsv")]
out.append("\\paragraph*{Bloc \\code{R-1} --- alias personnalisés (\\code{RPT-03}) ; fichier \\code{aliases/custom\\_aliases.tsv}}")
out.append("\\begin{center}\\scriptsize\\renewcommand{\\arraystretch}{1.1}\\begin{tabular}{@{}lp{11.3cm}@{}}\\toprule \\textbf{Alias name} & \\textbf{Alias text}\\\\\\midrule")
for a in al:
    t = a[1] if len(a) > 1 else ""
    if len(t) > 60: t = t[:30] + "\\ldots{} (80 répétitions de « Long address », voir fichier)"
    else: t = esc(t) if t else "\\emph{(vide)}"
    t = t.replace("é", "é").replace("😀", "[emoji]")
    out.append("%s & %s\\\\" % (esc(a[0]), t))
out.append("\\bottomrule\\end{tabular}\\end{center}")
block("S-1", "chaînes pour le champ d'activation (\\code{INS-05}) --- ne servent qu'à vérifier le refus", ["", "     ", "12345", "'\";--", "AAAA-AAAA-AAAA-AAAA", "0000-0000-0000-0000"])
block("W-1", "limiter les c\\oe urs de calcul (\\code{ROB-06}), invite de commandes Windows ; adaptez le chemin", ['start "" /affinity 1  "C:\\Chemin\\vers\\EMFPlanner.exe"', 'start "" /affinity 3  "C:\\Chemin\\vers\\EMFPlanner.exe"', 'start "" /affinity F  "C:\\Chemin\\vers\\EMFPlanner.exe"', 'start "" /affinity FF "C:\\Chemin\\vers\\EMFPlanner.exe"'])
block("P-1", "commandes de vérification (Python 3, depuis \\code{emf\\_test\\_kit/})", ["python3 tools/gen_all.py", "python3 tools/msi_check.py sortie.msi 7000 31 0.6", "python3 tools/msi_check.py sortie.msi 40000 50 2.0", "python3 tools/env_check.py enveloppe.msi data/msi/envelope/*.msi", "python3 tools/tile_server.py"])
open(os.path.join(H, "..", "guide", "blocks.tex"), "w", encoding="utf-8").write("\n".join(out) + "\n")
print("blocks.tex lines:", len(out))
