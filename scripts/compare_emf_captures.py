"""Compare two EMFPlanner memory snapshots and their offline REA inventories.

Reads MINIDUMP_MODULE_LIST, recovered image inventories and REA member Evidence.
No target execution or memory modification is performed.
"""
import argparse
from collections import Counter
import json
import mmap
from pathlib import Path
import struct


def read_json(path):
    return json.loads(path.read_text(encoding="utf-8-sig"))


def dump_modules(path):
    with path.open("rb") as source, mmap.mmap(source.fileno(), 0, access=mmap.ACCESS_READ) as data:
        if data[:4] != b"MDMP":
            raise ValueError(f"Not a minidump: {path}")
        count, directory = struct.unpack_from("<II", data, 8)
        modules = []
        for stream in range(count):
            kind, size, rva = struct.unpack_from("<III", data, directory + stream * 12)
            if kind != 4:
                continue
            module_count = struct.unpack_from("<I", data, rva)[0]
            if size < 4 + 108 * module_count or rva + size > len(data):
                raise ValueError("Invalid ModuleListStream bounds")
            for index in range(module_count):
                offset = rva + 4 + index * 108
                base, image_size, checksum, timestamp, name_rva = struct.unpack_from("<QIIII", data, offset)
                name_size = struct.unpack_from("<I", data, name_rva)[0]
                if name_size % 2 or name_rva + 4 + name_size > len(data):
                    raise ValueError("Invalid minidump module name")
                name = data[name_rva + 4:name_rva + 4 + name_size].decode("utf-16le")
                modules.append({"path": name, "base": hex(base), "image_size": image_size,
                                "checksum": checksum, "timestamp": timestamp})
            return modules
        raise ValueError("No ModuleListStream")


def member_summary(evidence):
    result = evidence["normalized_result"]
    methods = result["methods"]
    return {"evidence_id": evidence["evidence_id"], "artifact": result["artifact"],
            "module": result["module"], "coverage": result["coverage"],
            "types": len(result["types"]), "fields": len(result["fields"]),
            "methods": len(methods),
            "body_status_counts": dict(Counter(m["body"]["status"] for m in methods)),
            "body_issues": [m["token"] for m in methods if m["body"].get("issue")
                            or m["body"].get("truncated_instructions")]}


def method_key(method):
    body = method["body"]
    return {"declaring_type": method["declaring_type"], "name": method["name"],
            "signature": method["signature"], "flags": method["flags"],
            "impl_flags": method["impl_flags"],
            "body": {key: body.get(key) for key in ("status", "il_size", "il_sha256",
                     "normalized_il_sha256", "max_stack", "init_locals", "local_var_sig_token",
                     "exception_regions", "truncated_instructions", "issue")}}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    for name in ("before-dump", "after-dump", "before-members", "after-members",
                 "before-inventory", "after-inventory", "output"):
        parser.add_argument("--" + name, required=True, type=Path)
    args = parser.parse_args()
    before_evidence, after_evidence = read_json(args.before_members), read_json(args.after_members)
    before, after = before_evidence["normalized_result"], after_evidence["normalized_result"]
    if before["module"]["mvid"] != after["module"]["mvid"]:
        raise ValueError("MVID differs; build-local token comparison is not valid")
    old = {m["token"]: m for m in before["methods"]}
    new = {m["token"]: m for m in after["methods"]}
    changed = [{"token": token, "type": old[token]["declaring_type"], "name": old[token]["name"]}
               for token in old.keys() & new.keys() if method_key(old[token]) != method_key(new[token])]
    old_modules, new_modules = dump_modules(args.before_dump), dump_modules(args.after_dump)
    old_paths = {m["path"].lower() for m in old_modules}
    new_paths = {m["path"].lower() for m in new_modules}
    old_images, new_images = read_json(args.before_inventory), read_json(args.after_inventory)
    old_assemblies = {i.get("assembly_name") for i in old_images["recovered"]}
    new_assemblies = {i.get("assembly_name") for i in new_images["recovered"]}
    result = {
        "before": member_summary(before_evidence), "after": member_summary(after_evidence),
        "comparison": {"same_mvid": True, "changed_methods": sorted(changed, key=lambda m: m["token"]),
                       "added_method_tokens": sorted(new.keys() - old.keys()),
                       "removed_method_tokens": sorted(old.keys() - new.keys()),
                       "same_type_inventory": before["types"] == after["types"],
                       "same_field_inventory": before["fields"] == after["fields"],
                       "same_metadata_table_counts": before["metadata"]["table_row_counts"] == after["metadata"]["table_row_counts"]},
        "loaded_modules": {"before_count": len(old_modules), "after_count": len(new_modules),
                           "added": [m for m in new_modules if m["path"].lower() not in old_paths],
                           "removed": [m for m in old_modules if m["path"].lower() not in new_paths],
                           "before": old_modules, "after": new_modules},
        "recovered_images": {"before_count": len(old_images["recovered"]),
                             "after_count": len(new_images["recovered"]),
                             "added_assembly_names": sorted(new_assemblies - old_assemblies, key=str),
                             "removed_assembly_names": sorted(old_assemblies - new_assemblies, key=str)},
        "limitations": ["Two point-in-time accessible-memory snapshots, not an execution trace.",
                        "ModuleListStream can omit images not registered with the Windows loader.",
                        "Reconstructed managed images are an additional memory scan, not a proof of exhaustive runtime coverage.",
                        "Same CIL does not verify native components, protected runtime values or numerical parity.",
                        "Office servers execute in separate processes and are outside these dumps."]}
    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("x", encoding="utf-8") as target:
        json.dump(result, target, ensure_ascii=False, indent=2)
    concise = {key: value for key, value in result.items() if key not in ("before", "after", "loaded_modules")}
    concise["loaded_modules"] = {
        "before_count": len(old_modules), "after_count": len(new_modules),
        "added_count": len(result["loaded_modules"]["added"]),
        "removed_count": len(result["loaded_modules"]["removed"])}
    print(json.dumps(concise, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
