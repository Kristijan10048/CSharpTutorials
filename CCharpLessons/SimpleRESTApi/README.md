SimpleRESTApi
================

Lightweight example minimal Web API using .NET 10 and SQLite. The project demonstrates:
- Minimal hosting (CreateSlimBuilder) and mapping endpoints
- Using SQLite (Microsoft.Data.Sqlite + SQLitePCLRaw bundle)
- Simple JSON options with source-generated context

Prerequisites
- .NET 10 SDK installed: https://dotnet.microsoft.com
- (Optional) Visual Studio 2026 or VS Code

Build
- From repository or solution folder:
  dotnet build CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj

Run
- Start the API locally:
  dotnet run --project CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj

Endpoints
- GET /todos           -> returns a sample in-memory todo list
- GET /todos/{id}      -> returns a todo by id (route param)
- GET /test            -> returns a simple test message
- GET /test-full       -> calls a full-method endpoint and returns a string
- POST /test-demo      -> accepts DemoRequest { Message } and echoes it back
- POST /test-two-params -> accepts TwoParamsRequest { Param1, Param2 } and stores values in SQLite (Params table)

Tests
- Tests are located at: CCharpLessons/SimpleRESTApi.Tests
- The test project uses xUnit and references the main project.
- To run tests:
  dotnet test CCharpLessons/SimpleRESTApi.Tests

Notes about the PostTwoParams test
- The unit test creates a temporary SQLite database file in the OS temp folder, creates the Params table, initializes the Endpoints helper with the DB path, calls Endpoints.PostTwoParams, and verifies the row was inserted.
- The test deletes the temporary DB file after completion.

Project dependencies (selected)
- Microsoft.AspNetCore.OpenApi (10.x)
- Microsoft.Data.Sqlite (7.x)
- SQLitePCLRaw.bundle_e_sqlite3

If you want a test that exercises the app end-to-end, I can add integration tests that start the WebApplicationFactory and call endpoints via HttpClient.
