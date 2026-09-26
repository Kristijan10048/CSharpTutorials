---
name: write-unit-tests
description: Write and improve C# unit tests for specified classes, methods, behavior changes, or bug regressions. Use when asked to add unit tests, cover edge cases, or improve unit test coverage. Follow the project's existing test framework and conventions; prefer xUnit in this repository.
---

# Write unit tests

Create focused, deterministic tests that verify observable behavior and catch plausible regressions. Implement and run the tests when the user requests them; provide examples only when examples are requested.

## Establish scope and conventions

1. Read applicable `CLAUDE.md` and repository instructions. Inspect the requested production code, its callers, and nearby tests before editing.
2. Use the class, method, file, or behavior specified by the user. If none is specified, use the current conversation or relevant changes to identify a bounded target. Ask for the target only when it cannot reasonably be inferred; do not generate tests for the entire repository by default.
3. Identify the owning test project through its project references, not just its directory name. Check its target framework, test framework, package versions, nullable settings, and test command or CI configuration.
4. Reuse existing test projects, assertion libraries, fixtures, and test doubles. Add a test project only when needed, with a compatible target and the minimum dependencies. Avoid framework upgrades, new mocking libraries, or unrelated production refactors just to add tests.
5. Run the relevant existing tests before editing when practical so existing failures can be distinguished from regressions.

## Choose meaningful cases

Derive expected results from requirements, public contracts, examples, and domain rules. Inspect the implementation to find branches, but do not reproduce its algorithm to calculate expected values.

Select cases that matter to the target:

- Representative valid input and its exact expected result.
- Boundary values, including just inside and outside a documented limit.
- Empty, null, malformed, or duplicate input when the contract makes these relevant.
- Expected exceptions, including parameter names or domain details when contractual.
- State transitions, repeated calls, and required side effects or absence of side effects.
- Dependency failures, cancellation, or asynchronous results where the target handles them.
- A minimal reproduction of a reported bug.

Do not invent validation rules or freeze an apparent defect into an expected result. If intended behavior is ambiguous and affects the assertions, ask a focused question while continuing unambiguous cases. When explicitly asked for characterization tests, document that the assertions describe existing behavior.

Prefer a small set of distinct behaviors over redundant cases or a coverage percentage alone. Add simple property tests only when their behavior is requested, nontrivial, or important to the lesson being taught.

## Implement readable tests

- Follow local naming conventions; otherwise use `Method_Scenario_ExpectedBehavior` and a `<ClassName>Tests` class.
- Keep Arrange, Act, and Assert visually distinct. Test one behavior per case; use multiple assertions when they describe the same outcome.
- Use `[Fact]` for individual scenarios and `[Theory]` with `[InlineData]` or `[MemberData]` for the same behavior across inputs when working with xUnit. Match APIs to the installed version.
- Use exact, meaningful assertions on return values, state, collection contents, and exceptions. A non-null assertion alone is insufficient when a more specific outcome is available.
- Exercise production code through its public behavior. Do not copy production logic into tests, test mock setup, or use reflection to reach private methods merely to increase coverage.
- Prefer real value objects and small in-memory fakes. Mock external boundaries only when needed, using the project's existing tools. Verify calls only when the interaction itself is part of the contract.
- Await asynchronous calls and exception assertions. Use `async Task` tests; avoid `async void`, `.Wait()`, and `.Result` for asynchronous test execution.
- Control time and randomness through existing seams. Avoid sleeps, wall-clock timing assertions, live network services, machine-specific paths, and order-dependent data.
- Give each test independent state. Dispose resources and restore any changed global state in cleanup even when assertions fail. For unavoidable shared state, follow the framework's scoped serialization mechanism rather than disabling parallel execution globally.
- Use concise fixtures or helpers only when they clarify repeated setup. Keep expected values visible in the test.

## Repository-specific considerations

- Existing test projects under `CCharpLessons` use xUnit. Folder suffixes vary between `.Tests` and `.tests`; preserve their spelling.
- The repository mixes SDK-style projects and older .NET Framework projects. Inspect both the test and referenced production project for compatibility. Do not retarget or migrate production projects automatically to make a test build.
- `CCharpLessons/SimpleRESTApi.Tests/PostTwoParamsTests.cs` exercises SQLite and the filesystem. Treat that pattern as an integration test; do not copy its database setup into isolated unit tests. If the requested behavior requires real storage, explain the integration boundary and follow the requested scope.
- Tutorial examples can intentionally demonstrate unusual behavior. Preserve the lesson's purpose and test its stated contract rather than imposing an unrelated design convention.

## Verify and resolve failures

1. Run the new or changed tests using the project's actual runner. For a compatible SDK-style project using the existing VSTest setup, adapt these templates to the discovered paths and class name:

   ```text
   dotnet test "path/to/Project.Tests.csproj" --filter "FullyQualifiedName~Namespace.ClassNameTests"
   dotnet test "path/to/Project.Tests.csproj"
   ```

   For legacy projects or a different runner, follow the repository's build and test instructions. Confirm tests were discovered and executed; a successful command with zero matching tests does not establish success.
2. Run the affected test project after the focused tests pass. Broaden to dependent projects or the solution when changes affect shared behavior or repository instructions require it.
3. For a bug fix within the user's requested scope, demonstrate that the regression test fails for the reported reason before the fix and passes afterward when feasible. Preserve unrelated working changes; never reset the workspace to perform this check.
4. Correct test setup or assertion mistakes without weakening valid expectations. If a tests-only request exposes a production defect, report it and retain the regression evidence; do not silently change production behavior or skip the failing test.
5. Distinguish assertion failures from restore, build, SDK, framework, or environment failures. Report the exact blocker when execution is unavailable. Do not describe unexecuted tests as passing.
6. Use existing coverage tooling when coverage is requested or helps identify a concrete gap. Do not add dependencies or set arbitrary coverage targets solely for this workflow.
7. Review the diff for unintended production changes, generated artifacts, duplicated cases, and unrelated formatting changes.

## Report the result

Summarize the test files added or changed, the behaviors covered, and the commands actually run with their outcomes. Include observed test counts when available and disclose any failures, skipped tests, or execution blockers. Clearly identify any production changes made within the requested scope.
