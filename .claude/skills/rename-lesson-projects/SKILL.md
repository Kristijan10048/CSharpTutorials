---
name: rename-lesson-projects
description: Rename C# lesson projects to Lesson followed by a lesson number and a PascalCase topic name, such as Lesson10Properties. Use when asked to standardize lesson project names, correct Lession spelling, or rename lesson folders and project files while updating solution entries and dependent references.
---

# Rename lesson projects

Apply the naming convention `Lesson<Number><Name>` to the projects requested by the user. Example: `Lesson10Properties`. The angle brackets mark placeholders; they are not literal characters or directory separators.

When asked to perform a rename, carry it through to working project references and validation. When asked only for a plan or audit, provide the proposed mapping without modifying files.

## Naming rules

- Use the exact prefix `Lesson`, a positive integer lesson number without leading zeros, and a descriptive PascalCase topic name: `Lesson18VirtualFunctions`.
- Align the project directory's final component, project filename without its extension, and solution display name. Keep the existing parent directory unless relocation is explicitly requested.
- Preserve established lesson numbers and topic meaning. Correct `Lession` to `Lesson` and remove separators between the number and topic when the mapping is unambiguous.
- Follow a user-supplied mapping or numbering order. For projects without a number, use an explicit curriculum or documented mapping if one exists. Otherwise ask for the missing numbering or an ordering rule before renaming those projects. Do not infer curriculum order from alphabetic order, filesystem order, or a gap in existing numbers.
- Treat sublesson identifiers such as `5-1`, `5-2`, and `3.5` as ambiguous under the integer convention. Ask how to map them or follow the user's explicit extension of the convention. Never silently turn `5-1` into `51` or discard the subdivision.
- Keep companion test projects attached to their production project's base name and preserve the existing test suffix, such as `.Tests` or `.tests`. Example: `Lesson10-Properties.Tests` becomes `Lesson10Properties.Tests`. Determine ownership from project references rather than suffix alone; shared test projects may need an explicit mapping.
- Leave already compliant projects unchanged. Do not rename unrelated C++, tooling, WPF, or infrastructure projects merely because they share a solution; include them only when the requested scope covers them.

## Inspect and establish the mapping

1. Read applicable `CLAUDE.md` and repository instructions. Inspect the working tree and preserve existing staged, unstaged, and untracked work. Do not stage, commit, reset, or discard changes as part of a rename unless requested.
2. Inventory the scoped project files, their parent directories, all solution entries that reference them, and dependent projects. Account for both SDK-style and legacy C# project formats.
3. Build an old-to-new mapping for directories, project files, solution display names, and companion tests. Show the concise mapping before applying it. Proceed without another confirmation when the requested mapping is unambiguous; ask only for missing decisions that affect names or scope.
4. Check for collisions with existing files, directories, project names, and other proposed targets, including case-insensitive collisions on Windows. Do not merge directories, overwrite files, or assign a replacement lesson number to resolve a conflict silently.
5. Search the repository for exact old paths and names, including slash variants and relevant hidden configuration. Exclude `.git`, `.vs`, `bin`, `obj`, and other generated output. Inspect each match's meaning before editing it.
6. When practical, build the affected projects or run their tests before changes to identify existing failures. Missing legacy tooling does not justify migrating project formats or frameworks.

## Apply a consistent rename

- Move each project folder and rename its project file according to the mapping. Before moving a directory, verify the resolved absolute source and destination stay inside the intended repository. Use literal paths and one filesystem API or shell throughout the operation.
- Use a unique intermediate name for case-only renames on a case-insensitive filesystem. For swaps or rename cycles, use a checked, collision-free intermediate mapping. Never overwrite an occupied destination.
- Update every affected `.sln` or `.slnx` entry's path and display name while preserving project GUIDs, configurations, platform mappings, nesting, and dependencies. Do not remove and recreate project entries if doing so would lose their metadata.
- Update incoming and outgoing `ProjectReference` paths relative to the referencing project's new location. Update legacy reference display metadata where present, preserving referenced project GUIDs.
- Check relative `Compile`, `Content`, `None`, resource, linked-file, `Import`, `HintPath`, and build-event paths affected by the move. Update only references whose resolved location changes.
- Update build scripts, CI, launch configurations, active documentation links, and other maintained consumers of changed project paths. Avoid blind global replacements in unrelated text, historical records, data files, or examples that intentionally mention an old name.
- Keep the task focused on project identity and paths. Preserve source namespaces and runtime assembly identity by default. A project-file rename can change implicit `AssemblyName` and `RootNamespace` values: inspect their effective old values and set them explicitly when needed to preserve behavior. Preserve existing explicit values unless changing them is requested.
- If the user also requests namespace or assembly renames, update declarations and consumers consistently, including `using` directives, fully qualified type names, `InternalsVisibleTo`, reflection strings, XAML namespace mappings and `x:Class`, resource/designer configuration, and assembly-qualified configuration entries where applicable. Report compatibility implications for consumers outside the repository.
- Preserve source behavior, package versions, target frameworks, encoding, and unrelated formatting. Do not hand-edit generated build artifacts; let the normal build regenerate them.

## Examples in this repository

These illustrate the convention; they do not authorize renaming these projects when another target is requested.

| Existing project or folder | Target base name | Reason |
| --- | --- | --- |
| `Lesson10-Properties` project in `Lesson10Properties` | `Lesson10Properties` | Align project filename and solution name with its folder. |
| `Lession18VirtualFunctions` folder | `Lesson18VirtualFunctions` | Correct the folder prefix and update its consumers. |
| `Lession21StaticClass` folder | `Lesson21StaticClass` | Correct the folder prefix and update its consumers. |
| `ArrayRemoveInsert` | Requires a lesson number | Preserve the topic; obtain the intended curriculum number. |
| `Lesson5-1Methods` | Requires a sublesson mapping | Preserve the distinction from lesson 5 until the numbering is specified. |

## Validate and report

1. Confirm each mapped project exists at its target path and its former path is gone, accounting for case-only changes. Verify that affected solution entries, project references, and moved relative file references resolve to the intended files.
2. Search again for old paths and names. Classify remaining matches: intentionally preserved namespaces or assembly names, historical text, generated output, or missed active references. Correct missed active references without erasing intentional matches.
3. Build affected projects and their consumers using the repository's tooling. Run affected existing tests and verify tests were actually discovered and executed. Use solution-level validation when the scope or repository instructions require it. Do not add unit tests solely for a filesystem rename.
4. Distinguish pre-existing failures and missing SDK or framework tooling from rename-related failures. Fix failures caused by the rename within scope; disclose blockers rather than claiming an unexecuted build passed.
5. Review the final diff and file status for accidental deletes, unrelated edits, lost metadata, or generated files. Leave staging and commits to the user's requested workflow.
6. Report the old-to-new mapping, updated references, validation commands and results, and any projects waiting on a numbering decision. Mention explicitly when namespaces or assembly identities were preserved or changed.
