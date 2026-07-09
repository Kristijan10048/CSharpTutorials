SimpleRESTApi
=============

Lightweight example minimal Web API using .NET 10 and SQLite. The project demonstrates:
- Minimal hosting with `CreateSlimBuilder` and mapped endpoints
- SQLite usage with `Microsoft.Data.Sqlite`, `SQLitePCLRaw`, and EF Core
- Simple JSON options with a source-generated context
- Structured logging with INFO, DEBUG, and TRACE levels

Prerequisites
- .NET 10 SDK installed: https://dotnet.microsoft.com
- Optional: Visual Studio 2026 or VS Code

Build
- From the repository or solution folder:

```powershell
dotnet build CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj
```

Run
- Start the API locally:

```powershell
dotnet run --project CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj
```

- When running, logging output appears in the console at Trace, Debug, and Info levels.

Endpoints
- `GET /todos` returns a sample in-memory todo list
- `GET /todos/{id}` returns a todo by id
- `GET /test` returns a simple test message
- `GET /test-full` calls a full-method endpoint and returns a string
- `POST /test-demo` accepts `DemoRequest { Message }` and echoes it back
- `POST /test-two-params` accepts `TwoParamsRequest { Param1, Param2 }` and stores values in SQLite

Logging
- Each endpoint in the `Endpoints` class logs activity when accessed.
- Log levels used:
  - INFO: high-level events, such as successful data insert or initialization
  - DEBUG: endpoint access and request parameters
  - TRACE: detailed internal steps, such as opening a database connection or executing a command
- Console logging is configured at the Trace minimum level, so all logs are visible when running locally.

Example log output when calling `/test-two-params`:

```text
dbug: Endpoints[0]
      PostTwoParams endpoint accessed with Param1: alpha, Param2: beta
trce: Endpoints[0]
      PostTwoParams: Opening database connection
trce: Endpoints[0]
      PostTwoParams: Creating insert command
trce: Endpoints[0]
      PostTwoParams: Executing insert command
trce: Endpoints[0]
      PostTwoParams: Retrieving last inserted row id
info: Endpoints[0]
      PostTwoParams: Successfully inserted record with Id: 1, Param1: alpha, Param2: beta
```

Tests
- Tests are located at `CCharpLessons/SimpleRESTApi.Tests`.
- The test project uses xUnit and references the main project.
- To run tests:

```powershell
dotnet test CCharpLessons/SimpleRESTApi.Tests
```

Notes about the PostTwoParams test
- The unit test creates a temporary SQLite database file in the OS temp folder.
- It creates the `Params` table, initializes the `Endpoints` helper with the DB path and a `LoggerFactory`, calls `Endpoints.PostTwoParams`, and verifies the row was inserted.
- The test deletes the temporary DB file after completion.

EF Core migrations with SQLite
------------------------------

This project uses EF Core migrations for the `Users` and `Devices` tables. The migrations are based on `AppDbContext`, and the design-time context is created by `AppDbContextFactory`.

Install the EF tool once:

```powershell
dotnet tool install --global dotnet-ef
```

If it is already installed, update it when needed:

```powershell
dotnet tool update --global dotnet-ef
```

Run migration commands from this folder:

```powershell
cd CCharpLessons/SimpleRESTApi
```

Create a migration after changing the model:

```powershell
dotnet ef migrations add AddUserPhoneNumber --project . --startup-project .
```

This creates files in the `Migrations` folder. Review the generated `Up()` method before applying it. `Up()` describes what will be applied to the database, and `Down()` describes how EF can roll it back.

Apply pending migrations to the SQLite database:

```powershell
dotnet ef database update --project . --startup-project .
```

The default database file is:

```text
simple_rest_api.db
```

You can target another SQLite file by setting `SIMPLE_REST_DB` before running the command:

```powershell
$env:SIMPLE_REST_DB = "scratch.db"
dotnet ef database update --project . --startup-project .
```

Convenience scripts:

```powershell
./add-migration.ps1 -Name AddUserPhoneNumber
./update-database.ps1
```

Reset everything during learning
--------------------------------

If you do not need the data and want to start migrations from scratch:

1. Stop the API if it is running.
2. Delete the SQLite database file:

```powershell
Remove-Item .\simple_rest_api.db
```

3. Delete the migration files:

```powershell
Remove-Item .\Migrations\*.cs
```

4. Create a new initial migration:

```powershell
dotnet ef migrations add InitialCreate --project . --startup-project .
```

5. Apply it:

```powershell
dotnet ef database update --project . --startup-project .
```

The app also calls `Database.Migrate()` during startup, so running the API can apply pending migrations automatically. For learning, it is still useful to run `dotnet ef database update` manually so you can see exactly when migrations are applied.
