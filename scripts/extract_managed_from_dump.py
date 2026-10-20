"""Recover PE/CLI files from a Windows full-memory minidump, without executing them.

Preserves the minidump and records addresses, PE layout decisions, and omissions.
Recovered images are derived artifacts, not a claim that all original code survived.
"""
import argparse
import bisect
import hashlib
import json
import mmap
from pathlib import Path
import struct

import dnfile
import pefile


def u32(data, offset):
    return struct.unpack_from("<I", data, offset)[0]


class Dump:
    def __init__(self, data):
        self.data = data
        if data[:4] != b"MDMP":
            raise ValueError("Expected a Windows minidump")
        self.streams = {}
        directory = u32(data, 12)
        for index in range(u32(data, 8)):
            kind, size, rva = struct.unpack_from("<III", data, directory + index * 12)
            self.streams[kind] = (rva, size)
        rva, _ = self.streams[9]  # Memory64ListStream
        count, file_cursor = struct.unpack_from("<QQ", data, rva)
        self.ranges = []
        for index in range(count):
            address, size = struct.unpack_from("<QQ", data, rva + 16 + index * 16)
            if file_cursor + size > len(data):
                raise ValueError("Memory descriptor exceeds dump file")
            self.ranges.append((address, size, file_cursor))
            file_cursor += size
        self.ranges.sort()
        self.starts = [entry[0] for entry in self.ranges]

    def read(self, address, size):
        result = bytearray()
        while size:
            index = bisect.bisect_right(self.starts, address) - 1
            if index < 0:
                return None
            base, length, file_offset = self.ranges[index]
            delta = address - base
            if delta < 0 or delta >= length:
                return None
            count = min(length - delta, size)
            result.extend(self.data[file_offset + delta:file_offset + delta + count])
            address += count
            size -= count
        return bytes(result)


def metadata_location(dump, base, pe, cli_rva, layout):
    cli_offset = cli_rva if layout == "mapped" else pe.get_offset_from_rva(cli_rva)
    cli = dump.read(base + cli_offset, 24)
    if not cli or u32(cli, 0) != 72:
        return None
    metadata_rva, metadata_size = struct.unpack_from("<II", cli, 8)
    if not 16 <= metadata_size <= 64 * 1024 * 1024:
        return None
    metadata_offset = metadata_rva if layout == "mapped" else pe.get_offset_from_rva(metadata_rva)
    if dump.read(base + metadata_offset, 4) != b"BSJB":
        return None
    return {"cli_rva": hex(cli_rva), "metadata_rva": hex(metadata_rva), "metadata_size": metadata_size}


def rebuild(dump, base, pe, layout):
    size = max([pe.OPTIONAL_HEADER.SizeOfHeaders] + [s.PointerToRawData + s.SizeOfRawData for s in pe.sections])
    if size > 128 * 1024 * 1024:
        return None, ["Declared file size exceeds recovery limit"]
    if layout == "raw":
        result = dump.read(base, size)
        return result, [] if result else ["Original file-layout image is not fully captured"]
    headers = dump.read(base, pe.OPTIONAL_HEADER.SizeOfHeaders)
    if headers is None:
        return None, ["Missing PE headers"]
    result = bytearray(size)
    result[:len(headers)] = headers
    missing = []
    for section in pe.sections:
        if not section.SizeOfRawData:
            continue
        content = dump.read(base + section.VirtualAddress, section.SizeOfRawData)
        if content is None:
            missing.append({"section": section.Name.decode(errors="replace").rstrip("\0"),
                            "address": hex(base + section.VirtualAddress), "size": section.SizeOfRawData})
        else:
            result[section.PointerToRawData:section.PointerToRawData + len(content)] = content
    if missing:
        return None, missing  # Never silently synthesize unreadable section bytes.
    return bytes(result), []


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("dump", type=Path)
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    args.output.mkdir(parents=True, exist_ok=False)
    inventory = []
    recovered = []
    seen = set()
    with args.dump.open("rb") as source, mmap.mmap(source.fileno(), 0, access=mmap.ACCESS_READ) as data:
        dump = Dump(data)
        for range_base, range_size, range_offset in dump.ranges:
            cursor = range_offset
            end = range_offset + range_size
            while True:
                hit = data.find(b"MZ", cursor, end)
                if hit < 0:
                    break
                cursor = hit + 2
                base = range_base + hit - range_offset
                header = dump.read(base, 4096)
                if not header or not 0x40 <= u32(header, 0x3C) < 3500:
                    continue
                if header[u32(header, 0x3C):u32(header, 0x3C) + 4] != b"PE\0\0":
                    continue
                try:
                    pe = pefile.PE(data=header, fast_load=True)
                    if not 1 <= pe.FILE_HEADER.NumberOfSections <= 96:
                        continue
                    if pe.OPTIONAL_HEADER.SizeOfImage > 128 * 1024 * 1024:
                        continue
                    cli = pe.OPTIONAL_HEADER.DATA_DIRECTORY[14]
                    record = {"address": hex(base), "machine": hex(pe.FILE_HEADER.Machine),
                              "image_size": pe.OPTIONAL_HEADER.SizeOfImage, "cli_rva": hex(cli.VirtualAddress)}
                    inventory.append(record)
                    if not cli.VirtualAddress:
                        continue
                    for layout in ["mapped", "raw"]:
                        metadata = metadata_location(dump, base, pe, cli.VirtualAddress, layout)
                        if metadata is None:
                            continue
                        image, missing = rebuild(dump, base, pe, layout)
                        candidate = dict(record, layout=layout, metadata=metadata, missing=missing)
                        if image is None:
                            recovered.append(candidate)
                            continue
                        digest = hashlib.sha256(image).hexdigest()
                        if digest in seen:
                            continue
                        seen.add(digest)
                        name = None
                        try:
                            managed = dnfile.dnPE(data=image)
                            if managed.net and managed.net.mdtables.Assembly:
                                name = str(managed.net.mdtables.Assembly.rows[0].Name)
                            managed.close()
                        except Exception as error:
                            candidate["metadata_parser_error"] = str(error)
                        filename = f"image-{base:016x}-{layout}.dll"
                        destination = args.output / filename
                        destination.write_bytes(image)
                        candidate.update(path=str(destination.resolve()), sha256=digest,
                                         bytes=len(image), assembly_name=name)
                        recovered.append(candidate)
                        print(json.dumps({"assembly": name, "address": hex(base), "layout": layout,
                                          "bytes": len(image), "path": filename}), flush=True)
                except (pefile.PEFormatError, ValueError, IndexError, struct.error, OverflowError):
                    continue
        report = {"dump": str(args.dump.resolve()), "memory_ranges": len(dump.ranges),
                  "pe_candidates": inventory, "recovered": recovered,
                  "limitations": ["Header/signature scanning cannot recover erased PE headers.",
                                  "Reconstruction does not repair transformed or encrypted CIL bodies.",
                                  "Missing captured ranges cause recovery to be refused, not zero-filled."]}
        (args.output / "inventory.json").write_text(json.dumps(report, indent=2), encoding="utf-8")
        print(json.dumps({"memory_ranges": len(dump.ranges), "pe_candidates": len(inventory),
                          "recovered_files": len(seen), "report": str(args.output / "inventory.json")}), flush=True)


if __name__ == "__main__":
    main()
