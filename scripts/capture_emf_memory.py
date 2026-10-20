"""Capture only the selected, path-verified EMFPlanner process with Windows DbgHelp.

No injection, process memory writes, security-setting changes or privilege changes.
The output is a full accessible-memory minidump; unreadable memory may be omitted.
"""
import argparse
import ctypes
from ctypes import wintypes
import hashlib
import json
import msvcrt
import os
from pathlib import Path


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--pid", type=int, required=True)
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    expected = Path(__file__).resolve().parents[1] / "extracted/original/app/EMFPlanner.exe"
    kernel = ctypes.WinDLL("kernel32", use_last_error=True)
    dbghelp = ctypes.WinDLL("dbghelp", use_last_error=True)
    kernel.OpenProcess.argtypes = [wintypes.DWORD, wintypes.BOOL, wintypes.DWORD]
    kernel.OpenProcess.restype = wintypes.HANDLE
    kernel.QueryFullProcessImageNameW.argtypes = [wintypes.HANDLE, wintypes.DWORD, wintypes.LPWSTR, ctypes.POINTER(wintypes.DWORD)]
    kernel.QueryFullProcessImageNameW.restype = wintypes.BOOL
    kernel.CloseHandle.argtypes = [wintypes.HANDLE]
    kernel.CloseHandle.restype = wintypes.BOOL
    dbghelp.MiniDumpWriteDump.argtypes = [wintypes.HANDLE, wintypes.DWORD, wintypes.HANDLE,
                                        wintypes.DWORD, ctypes.c_void_p, ctypes.c_void_p, ctypes.c_void_p]
    dbghelp.MiniDumpWriteDump.restype = wintypes.BOOL
    # PROCESS_QUERY_INFORMATION | PROCESS_VM_READ. No all-access or debug-privilege request.
    handle = kernel.OpenProcess(0x400 | 0x10, False, args.pid)
    if not handle:
        raise ctypes.WinError(ctypes.get_last_error())
    try:
        path_buffer = ctypes.create_unicode_buffer(32768)
        path_length = wintypes.DWORD(len(path_buffer))
        if not kernel.QueryFullProcessImageNameW(handle, 0, path_buffer, ctypes.byref(path_length)):
            raise ctypes.WinError(ctypes.get_last_error())
        if os.path.normcase(path_buffer.value) != os.path.normcase(str(expected)):
            raise RuntimeError(f"Refusing unexpected process image: {path_buffer.value}")
        args.output.parent.mkdir(parents=True, exist_ok=True)
        # FullMemory + FullMemoryInfo + ThreadInfo + IgnoreInaccessibleMemory.
        flags = 0x2 | 0x800 | 0x1000 | 0x20000
        print(f"Capturing verified EMFPlanner PID {args.pid} to {args.output}", flush=True)
        with args.output.open("xb") as output:
            success = dbghelp.MiniDumpWriteDump(handle, args.pid, msvcrt.get_osfhandle(output.fileno()),
                                               flags, None, None, None)
            if not success:
                raise ctypes.WinError(ctypes.get_last_error())
    finally:
        kernel.CloseHandle(handle)
    digest = hashlib.sha256()
    with args.output.open("rb") as dump:
        for block in iter(lambda: dump.read(1024 * 1024), b""):
            digest.update(block)
    record = {"pid": args.pid, "image": str(expected), "dump": str(args.output.resolve()),
              "bytes": args.output.stat().st_size, "sha256": digest.hexdigest(), "flags": hex(flags),
              "limitations": ["Accessible memory only; inaccessible regions may be omitted.",
                              "The process was running during collection; this is not an execution trace."]}
    args.output.with_suffix(".json").write_text(json.dumps(record, indent=2), encoding="utf-8")
    print(json.dumps(record, indent=2), flush=True)


if __name__ == "__main__":
    main()
