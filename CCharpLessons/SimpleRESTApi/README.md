SimpleRESTApi
================

Lightweight example minimal Web API using .NET 10 and SQLite. The project demonstrates:
- Minimal hosting (CreateSlimBuilder) and mapping endpoints
- Using SQLite (Microsoft.Data.Sqlite + SQLitePCLRaw bundle)
- Simple JSON options with source-generated context
- Structured logging with INFO, DEBUG, and TRACE levels

Prerequisites
- .NET 10 SDK installed: https://dotnet.microsoft.com
- (Optional) Visual Studio 2026 or VS Code

Build
- From repository or solution folder:
  dotnet build CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj

Run
- Start the API locally:
  dotnet run --project CCharpLessons/SimpleRESTApi/SimpleRESTApi.csproj
- When running, logging output will appear in the console at Trace, Debug, and Info levels.

Endpoints
- GET /todos           -> returns a sample in-memory todo list
- GET /todos/{id}      -> returns a todo by id (route param)
- GET /test            -> returns a simple test message
- GET /test-full       -> calls a full-method endpoint and returns a string (logs at DEBUG level)
- POST /test-demo      -> accepts DemoRequest { Message } and echoes it back (logs at DEBUG level)
- POST /test-two-params -> accepts TwoParamsRequest { Param1, Param2 } and stores values in SQLite (logs at INFO, DEBUG, and TRACE levels)

Logging
- Each endpoint in the Endpoints class logs activity when accessed.
- Log levels used:
  - **INFO**: High-level events (e.g., successful data insert, initialization)
  - **DEBUG**: Endpoint access and request parameters
  - **TRACE**: Detailed internal steps (e.g., database connection open, command execution)
- Console logging is configured at the Trace minimum level, so all logs will be visible when running locally.
- Example log output when calling /test-two-params:
  ```
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
- Tests are located at: CCharpLessons/SimpleRESTApi.Tests
- The test project uses xUnit and references the main project.
- To run tests:
  dotnet test CCharpLessons/SimpleRESTApi.Tests

Notes about the PostTwoParams test
- The unit test creates a temporary SQLite database file in the OS temp folder, creates the Params table, initializes the Endpoints helper with the DB path and a LoggerFactory, calls Endpoints.PostTwoParams, and verifies the row was inserted.
- The test deletes the temporary DB file after completion.
- A LoggerFactory configured with console logging is passed to Endpoints.Init() for logging during the test.

Project dependencies (selected)
- Microsoft.AspNetCore.OpenApi (10.x)
- Microsoft.Data.Sqlite (7.x)
- Microsoft.Extensions.Logging (for structured logging)
- SQLitePCLRaw.bundle_e_sqlite3

If you want integration tests (HttpClient/WebApplicationFactory) instead of the current unit test, I can add them.
