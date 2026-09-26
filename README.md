# C# Lessons & SimpleRESTApi

A collection of small, self-contained **C# tutorial projects** plus a lightweight **.NET 10 Web API demo**. Each lesson is an independent console application (or test project) that demonstrates one or more C# features with minimal code. The `SimpleRESTApi` folder is a modern web-service example built on top of the same lessons.

> Note: the source folders live under `CCharpLessons/` (the original author's spelling). Paths below use this exact name so commands work as written.

## Repository layout

```
CSharpTutorials/
├── README.md                       ← this file
└── CCharpLessons/
    ├── SimpleRESTApi/              ← .NET 10 Web API demo (see its own README)
    │   └── SimpleRESTApi.Tests/    ← xUnit tests for the API
    ├── Lesson1GettingStarted/      ← first steps, running a console app
    ├── Lesson2OperatorsTypesAndVariables/
    ├── Lesson3ControlStatementsSelection/
    ├── Lesson4ControlStatementsLoops/
    ├── Lesson5-1Methods/           ← methods (+ unit tests)
    ├── Lesson5-2Delegates/         ← delegates (void, value-returning, parameterized)
    ├── Lesson6Namespaces/
    ├── Lesson7IntroductionToClasses/
    ├── Lesson8ClassInheritance/
    ├── Lesson9Polymorphism/
    ├── Lesson10Properties/         ← properties (+ unit tests)
    ├── Lesson11Indexers/
    ├── Lesson12Structs/            ← structs (+ unit tests)
    ├── Lesson13Interfaces/
    ├── Lesson15IntroductionToExceptionHandling/  ← try/catch/finally (+ unit tests)
    ├── Lesson16UsingAttributes/
    ├── Lesson17Enums/
    ├── Lesson19HybridDictionary/   ← a dictionary-like class (+ unit tests)
    ├── Lesson20PartialClasses/
    └── ... and several focused demos:
        ├── AbsOrRelativePath/      ← absolute vs. relative paths
        ├── ArrayRemoveInsert/      ← removing and inserting array elements
        ├── Attributes/             ← attribute definitions
        ├── ClassInheritanceDemo/   ← inheritance walkthrough
        ├── ConstructorsAndInheritance/
        ├── EnumGreatherTest/       ← comparing enum values
        ├── ExtensionMethods/
        ├── HidingInheritedMembers/
        ├── Indexers/
        ├── LinqXml/                ← LINQ to XML
        ├── NamedParameters/        ← named arguments
        ├── ParamsKeyword/          ← the `params` keyword
        ├── Reflection/             ← reflection over assemblies/types
        ├── RegularExpressions/     ← regex matching/replacement
        ├── StackQueueAndDict/      ← stack, queue and dictionary
        └── XMLReadWrite/           ← reading and writing XML files
```

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

## Running the tests

Several lessons ship with xUnit test projects (folders named `*.Tests`). Run them individually:

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

- Getting started, variables/types, operators
- Control flow (selection and loops)
- Methods, delegates, namespaces
- Classes, constructors, inheritance, polymorphism, hiding members
- Properties, indexers, structs, interfaces, partial classes
- Enums, attributes, named arguments, the `params` keyword
- Exception handling
- Reflection, LINQ to XML, regular expressions
- Reading/writing XML files, stacks/queues/dictionaries

## Notes

- Build output under each project's `bin/` and `obj/` folders is currently committed to the repository. To keep history clean, add those paths to a `.gitignore` (for example: `**/bin/**`, `**/obj/**`) and commit the change.
- The `SimpleRESTApi` folder already ships its own detailed README; this file focuses on the lesson collection.
