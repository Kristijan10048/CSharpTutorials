# C# Lessons & SimpleRESTApi

A collection of small, self-contained **C# tutorial projects** plus a lightweight **.NET 10 Web API demo**. Each lesson is an independent console application (or test project) that demonstrates one or more C# features with minimal code. The `SimpleRESTApi` folder is a modern web-service example built on top of the same lessons.

> Note: the source folders live under `CCharpLessons/` (the original author's spelling). Paths below use this exact name so commands work as written.

## Repository layout

All lessons live under `CCharpLessons/` (the original author's spelling). They are numbered roughly in learning order; the `SimpleRESTApi` folder is a separate web-service demo with its own README.

### Core lessons

| # | Lesson | Notes |
|---|--------|-------|
| 1 | Getting started | first steps, running a console app |
| 2 | Operators, types and variables | |
| 3 | Control flow: selection | |
| 3.5 | Question mark (`?`) operator | |
| 4 | Control flow: loops | |
| 5-1 | Methods | (+ unit tests) |
| 5-2 | Delegates | void, value-returning, parameterized |
| 6 | Namespaces | |
| 7 | Introduction to classes | |
| 8 | Class inheritance | |
| 9 | Polymorphism | |
| 10 | Properties | (+ unit tests) |
| 11 | Indexers | |
| 12 | Structs | (+ unit tests) |
| 13 | Interfaces | |
| 15 | Exception handling | try/catch/finally (+ unit tests) |
| 16 | Attributes | |
| 17 | Enums | |
| 18 | Virtual functions | *(folder `Lession18VirtualFunctions`)* |
| 19 | HybridDictionary | a dictionary-like class (+ unit tests) |
| 20 | Partial classes | |
| 21 | Static classes | *(folder `Lession21StaticClass`)* |
| 22 | Named parameters | |
| 23 | The `params` keyword | |
| 24 | Extension methods | |
| 25 | Reflection | over assemblies/types |
| 26 | Regular expressions | matching/replacement |
| 27 | Reading and writing XML files | |
| 28 | LINQ to XML | |
| 29 | Stack, queue and dictionary | |
| 30 | Absolute vs relative paths | |
| 31 | Attribute definitions | |
| 32 | Class inheritance | walkthrough |
| 33 | Class inheritance demo | |
| 34 | Constructors and inheritance | |
| 35 | Hiding inherited members | |
| 36 | Enum comparison | |
| 37 | Removing and inserting array elements | |

> A few folders don't fit the clean numbering — `HelloWorld/` and `Lesson5Methods/` are small standalone experiments, and `Lession18VirtualFunctions` / `Lession21StaticClass` carry a historical typo in their names.

### Other projects

- `SimpleRESTApi/` — .NET 10 Web API demo (see its own README at `CCharpLessons/SimpleRESTApi/README.md`)

## Prerequisites

- **.NET SDK** installed (the API demo targets .NET 10; the lesson projects are console apps). Install from https://dotnet.microsoft.com.
- Optional: **Visual Studio 2026**, **VS Code**, or any editor with C# support.

> The lesson projects use a mix of project formats — most are legacy-style `.csproj` files, while newer ones (e.g. `Lesson5-2Delegates`, `SimpleRESTApi`) use modern SDK-style `<Project Sdk="Microsoft.NET.Sdk">`. Build every project individually with the commands below; there is no single solution restore that covers all of them cleanly.

## Building and running a lesson

Each lesson has its own `.csproj` under `CCharpLessons/`. From any folder:

```powershell
# Restore, build, and run one lesson (replace <Lesson> with the folder name)
dotnet run --project CCharpLessons/<Lesson>/<Lesson>.csproj
```

For example:

```powershell
dotnet run --project CCharpLessons/Lesson5-2Delegates/Lesson5-2Delegates.csproj
```

> Most folders share their name with the `.csproj` inside them, but a few don't — e.g. `Lesson10Properties/` contains `Lesson10-Properties.csproj`. Adjust the path accordingly when it differs.

## Running the tests

Several lessons ship with xUnit test projects (folders named `*.Tests`, though some use lowercase `.tests`). Run them individually:

```powershell
dotnet test CCharpLessons/Lesson10-Properties.Tests/Lesson10-Properties.Tests.csproj
```

The API demo has its own test project at `CCharpLessons/SimpleRESTApi.Tests`.

## SimpleRESTApi

`SimpleRESTApi/` is a minimal .NET 10 Web API using SQLite and EF Core. It demonstrates slim hosting, structured logging, source-generated JSON options, and AOT publishing. For build/run/test instructions, endpoints, migrations, and example log output, see its dedicated README:

```
CCharpLessons/SimpleRESTApi/README.md
```

## Topics covered

The lessons walk through the core of C#, roughly in order:

- Getting started, variables/types, operators (including the `?` operator)
- Control flow (selection and loops)
- Methods, delegates, namespaces
- Classes, constructors, inheritance, polymorphism, virtual functions, hiding members
- Properties, indexers, structs, interfaces, partial classes, static classes
- Enums, enum comparison, attributes, named arguments, the `params` keyword
- Extension methods
- Exception handling (try/catch/finally)
- Reflection, LINQ to XML, regular expressions
- Reading/writing XML files, stacks/queues/dictionaries, array manipulation

## Notes

- Build output under each project's `bin/` and `obj/` folders is currently committed to the repository. To keep history clean, add those paths to a `.gitignore` (for example: `**/bin/**`, `**/obj/**`) and commit the change.
- The `SimpleRESTApi` folder already ships its own detailed README; this file focuses on the lesson collection.
