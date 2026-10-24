"""Replay the recovered CalcPPE method in an isolated .NET console, with numeric-only stubs.

Does not load or launch EMFPlanner, call CalcTmp, or alter an installed application.
Requires the existing local .tools/dotnet SDK. Regular TS tests use the saved fixture.
"""
import hashlib
import json
import math
from pathlib import Path
import subprocess

ROOT = Path(__file__).resolve().parents[1]
SOURCE = ROOT / "reference/original-decompiled/EMFPlanner/CalcSZZ.cs"
WORK = ROOT / ".tools/test-kit-native-oracle"
FIXTURE = ROOT / "tests/fixtures/test-kit-native-oracle.json"


def method(text, signature):
    start = text.index(signature)
    opening = text.index("{", start)
    depth = 0
    for i in range(opening, len(text)):
        depth += (text[i] == "{") - (text[i] == "}")
        if depth == 0:
            return text[start:i + 1]
    raise ValueError("Unclosed native method")


def pattern(name):
    path = ROOT / "test/emf_test_kit/data/msi" / name
    lines = path.read_text(encoding="utf-8-sig").splitlines()
    return {key: [float(line.split()[1]) for line in lines[lines.index(key.upper() + " 360") + 1:][:360]]
            for key in ["horizontal", "vertical"]}


WORK.mkdir(parents=True, exist_ok=True)
native = method(SOURCE.read_text(encoding="utf-8"), "public static float CalcPPE(")
program = r'''
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
public static class Public2 {
  public struct CoordMeters { public double Xm, Ym; }
  public struct Tx { public CoordMeters CoordM; }
  public struct Project { public Tx[] TX; }
  public static Project PRJ;
}
public static class NativeCalc {
  public struct TEMP { public float H, Azimut, Tilt, PROIZV; public float[] DnH, DnV; }
  public static TEMP[] TMP;
__METHOD__
}
public sealed class Pattern { public double[] horizontal {get;set;} public double[] vertical {get;set;} }
public sealed class Case {
  public string pattern {get;set;}
  public double x {get;set;} public double y {get;set;} public double z {get;set;}
  public double azimuth {get;set;} public double tilt {get;set;}
  public double power {get;set;} public int tx {get;set;} public double loss {get;set;} public double gain {get;set;}
  public double height {get;set;}
  public double native_field {get;set;}
}
public sealed class Input { public Dictionary<string,Pattern> patterns {get;set;} public Case[] cases {get;set;} }
public static class Program {
  public static void Main(string[] args) {
    var input = JsonSerializer.Deserialize<Input>(File.ReadAllText(args[0]));
    Public2.PRJ = new Public2.Project { TX = new Public2.Tx[1] };
    foreach(var c in input.cases) {
      var p = input.patterns[c.pattern];
      NativeCalc.TMP = new [] { new NativeCalc.TEMP {
        H = (float)c.height, Azimut = (float)(c.azimuth * Math.PI / 180), Tilt = (float)(-c.tilt * Math.PI / 180),
        PROIZV = (float)Math.Sqrt((double)(30f * (float)c.power * c.tx) * Math.Pow(10, (float)c.gain / 10f) * Math.Pow(10, -(float)c.loss / 10f)),
        DnH = p.horizontal.Select(v => (float)Math.Pow(10, -(float)v / 20f)).ToArray(),
        DnV = p.vertical.Select(v => (float)Math.Pow(10, -(float)v / 20f)).ToArray()
      }};
      c.native_field = (float)Math.Sqrt(NativeCalc.CalcPPE(0,(float)c.x,(float)c.y,(float)c.z) * 3.77);
    }
    File.WriteAllText(args[1], JsonSerializer.Serialize(input.cases));
  }
}
'''.replace("__METHOD__", native)
(WORK / "Program.cs").write_text(program, encoding="utf-8")
(WORK / "Oracle.csproj").write_text('''<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>
<OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework>
<ImplicitUsings>disable</ImplicitUsings><Nullable>disable</Nullable><EnableDefaultCompileItems>true</EnableDefaultCompileItems>
</PropertyGroup></Project>''', encoding="utf-8")

patterns = {name: pattern(name) for name in ["ok_isotropic.msi", "ok_sector65x7_17dBi.msi", "ok_backfire_180.msi"]}
cases = []
for name in patterns:
    for az in [0, 60, 130, 220, 359]:
        for tilt in [-30, -10, 0, 10, 30]:
            for direction in [0, 10, 60, 130, 180, 220, 270, 359]:
                for z in [1.5, 30, 60]:
                    cases.append(dict(pattern=name, x=100 * math.sin(math.radians(direction)),
                                      y=100 * math.cos(math.radians(direction)), z=z, azimuth=az, tilt=tilt,
                                      power=20, tx=2, loss=.37, gain=17, height=30))
(WORK / "input.json").write_text(json.dumps(dict(patterns=patterns, cases=cases)), encoding="utf-8")
dotnet = ROOT / ".tools/dotnet/dotnet.exe"
subprocess.run([str(dotnet), "build", "Oracle.csproj", "-c", "Release", "--nologo", "-v", "quiet"], cwd=WORK, check=True)
subprocess.run([str(dotnet), str(WORK / "bin/Release/net10.0/Oracle.dll"), str(WORK / "input.json"), str(WORK / "output.json")], check=True)
output = dict(
    provenance="Isolated execution of the recovered original CalcPPE method, unchanged, with numeric-only Public2/TMP stubs. Not the original UI or CalcTmp pipeline.",
    source=str(SOURCE.relative_to(ROOT)).replace("\\", "/"),
    source_sha256=hashlib.sha256(SOURCE.read_bytes()).hexdigest(),
    normalized_method_sha256=hashlib.sha256(native.encode()).hexdigest(),
    case_count=len(cases),
    cases=json.loads((WORK / "output.json").read_text()),
)
FIXTURE.write_text(json.dumps(output, indent=2) + "\n", encoding="utf-8")
print(f"Saved {len(cases)} original-method cases to {FIXTURE.relative_to(ROOT)}")
