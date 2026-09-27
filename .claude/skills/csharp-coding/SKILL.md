---
name: csharp-coding
description: Implement, debug, refactor, and explain C# code in this repository's console lessons, .NET applications, ASP.NET Core API, and WPF demos. Use for C# coding tasks while preserving the owning project's framework, language version, conventions, and teaching purpose.
---

# C# coding

Complete the requested change and verify the affected behavior. For explanation-only requests, explain without editing. Keep tutorial examples small and focused on the concept being taught.

## Establish context

1. Read applicable repository instructions and inspect the working tree. Preserve existing user changes.
2. Locate the owning `.csproj`, callers, and tests. Inspect target framework, language version, nullable and implicit-using settings, platform configurations, package references, and applicable `global.json`, `Directory.Build.*`, or `.editorconfig` files.
3. Match syntax and APIs to the actual compiler and framework. An installed modern SDK does not authorize modern syntax in legacy projects. Preserve frameworks, dependencies, and project formats unless the task requires changes.
4. Identify the requested behavior and smallest coherent change. Preserve public contracts and the teaching purpose; intentional examples of reflection, inheritance, exceptions, or older syntax are not automatically defects. Ask only when missing requirements materially change implementation.

## Repository context

- Sources live under `CCharpLessons/`; preserve its spelling. Folder and project filenames can differ, so discover paths instead of constructing them from a naming pattern.
- Projects mix SDK-style .NET and legacy .NET Framework formats. Add new source files to explicit legacy `Compile` items when needed; avoid duplicate entries in SDK-style projects.
- Lessons use `CCharpLessons/CCharpLessons.sln`; WPF has `WPF/DemosWPF/DemosWPF.sln`. Validate the affected project rather than assuming one solution builds every demo.
- `CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj` currently targets .NET 10 with nullable references, implicit usings, and NativeAOT enabled. Read its README and actual hosting, storage, and serialization code for API work. Recheck project settings rather than treating this description as permanent.
- Tests use xUnit with both `.Tests` and `.tests` suffixes. Determine ownership through `ProjectReference` entries. API tests are a sibling at `CCharpLessons/SimpleRESTApi.Tests/`.
- For substantial unit-test authoring, read [write-unit-tests](../write-unit-tests/SKILL.md). For requested lesson renames, read [rename-lesson-projects](../rename-lesson-projects/SKILL.md). Ordinary coding does not require either workflow.

## Implement C# changes

- Follow surrounding naming, formatting, namespace, and declaration styles. Use supported language features when they clarify code; avoid unrelated modernization.
- Prefer straightforward types and methods. Add interfaces, abstractions, dependencies, or design patterns only when the requested behavior benefits. Keep a short lesson understandable without an application framework.
- Make null and error behavior explicit at relevant boundaries. Honor nullable analysis; do not suppress warnings with `!` without establishing why the value is non-null. Use `TryParse` or equivalent for expected invalid input when appropriate.
- Preserve exception meaning and stack traces. Use `throw;` when rethrowing. Catch where recovery or useful context is possible, while preserving deliberately demonstrated exception behavior.
- Dispose owned streams, connections, and disposable resources using constructs supported by the project. Do not dispose resources owned by callers or dependency injection.
- Await asynchronous work, propagate cancellation where supported, and avoid blocking with `.Result` or `.Wait()`. Reserve `async void` for required event handlers. Keep WPF UI updates on the UI thread and long operations off it as appropriate.
- Choose collections and LINQ for semantics and clarity. Check deferred execution, multiple enumeration, ordering, and mutation when they affect correctness; avoid speculative performance refactors.
- For parsing, XML, regex, and files, consider representative and malformed input. Make culture and path assumptions explicit when contractual; avoid machine-specific absolute paths in examples.
- For ASP.NET Core, follow existing endpoint and dependency-injection styles, preserve HTTP contracts, validate external input, and parameterize database queries. Respect service lifetimes and avoid concurrent operations on one EF Core context.
- For trimming or NativeAOT code, inspect reflection and serialization compatibility and existing source-generation patterns. Do not suppress warnings or disable AOT just to make a build pass.
- Use comments to explain the lesson, a non-obvious decision, or a constraint, rather than narrating every statement.

## Verify

1. Follow existing build instructions and CI. For SDK-style projects, adapt `dotnet build "path/to/Project.csproj"` and `dotnet test "path/to/Project.Tests.csproj"` to discovered paths and the actual test runner.
2. For legacy .NET Framework or WPF, use compatible Visual Studio/MSBuild tooling and the project's configuration and platform. Missing targeting packs are environment blockers, not reasons to migrate silently.
3. Add meaningful regression or behavior tests when warranted, using the existing framework. Reproduce a bug before fixing it when feasible without resetting user changes. A build and bounded example run may suffice for simple tutorial edits; do not add a test framework solely for a trivial change.
4. Confirm tests were discovered and executed. Distinguish behavior failures from restore, compiler, or environment failures. Broaden checks to dependent projects when shared behavior changes; avoid expanding scope to unrelated solution failures.
5. For changes affecting NativeAOT compatibility, perform the documented publish check when supported. A normal build alone does not verify AOT publishing; disclose unverified compatibility.
6. Review the final diff for unintended edits or generated output. Keep build artifacts out of the change.

## Report

Explain what changed and why, the validation actually performed and its outcome, and remaining blockers or unverified behavior. For tutorials, briefly explain the C# concept when useful.
