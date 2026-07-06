using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

/// <summary>
/// Endpoints helper class
/// Contains full-method endpoint handlers used by MapGet/MapPost. These methods are regular static
/// methods (not lambdas) so they can be referenced directly when mapping routes.
/// </summary>
internal static class Endpoints
{
    // Path to the SQLite database file. Initialized via Init(...).
    private static string? _dbPath;
    private static ILogger? _logger;

    /// <summary>
    /// Initialize the helper with the database file path and logger factory.
    /// Call this once from the top-level code before mapping endpoints that use the DB.
    /// </summary>
    public static void Init(string dbPath, ILoggerFactory loggerFactory)
    {
        _dbPath = dbPath;
        _logger = loggerFactory.CreateLogger(nameof(Endpoints));
        _logger?.LogInformation("Endpoints initialized with database path: {DbPath}", dbPath);
    }

    /// <summary>
    /// A simple test endpoint that returns a static string.
    /// Mapped as: app.MapGet("/test-full", Endpoints.TestFull)
    /// Logs at DEBUG level when accessed.
    /// </summary>
    public static string TestFull()
    {
        _logger?.LogDebug("TestFull endpoint accessed");
        _logger?.LogTrace("TestFull: Returning test message");
        return "Test Message";
    }

    /// <summary>
    /// Demo POST endpoint that echoes the received DemoRequest.
    /// Mapped as: app.MapPost("/test-demo", Endpoints.PostDemo)
    /// Logs at DEBUG level when accessed.
    /// </summary>
    public static IResult PostDemo(DemoRequest request)
    {
        _logger?.LogDebug("PostDemo endpoint accessed with message: {Message}", request.Message);
        _logger?.LogTrace("PostDemo: Echoing request back to client");
        return TypedResults.Ok(new { Message = "Received", Received = request.Message });
    }

    /// <summary>
    /// POST endpoint that accepts two string parameters in the JSON body and stores them in SQLite.
    /// Returns the created row id and stored values. Requires Init(...) to have been called first.
    /// Mapped as: app.MapPost("/test-two-params", Endpoints.PostTwoParams)
    /// Logs at INFO level on successful insert, DEBUG on access, and TRACE for detailed steps.
    /// </summary>
    public static IResult PostTwoParams(TwoParamsRequest request)
    {
        _logger?.LogDebug("PostTwoParams endpoint accessed with Param1: {Param1}, Param2: {Param2}", request.Param1, request.Param2);

        if (string.IsNullOrEmpty(_dbPath))
        {
            _logger?.LogError("PostTwoParams: Database path not initialized");
            return TypedResults.Problem("Database not initialized");
        }

        _logger?.LogTrace("PostTwoParams: Opening database connection");
        using var conn = new SqliteConnection($"Data Source={_dbPath}");
        conn.Open();

        _logger?.LogTrace("PostTwoParams: Creating insert command");
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Params (Param1, Param2, CreatedUtc) VALUES ($p1, $p2, $ts);";
        cmd.Parameters.AddWithValue("$p1", request.Param1 ?? string.Empty);
        cmd.Parameters.AddWithValue("$p2", request.Param2 ?? string.Empty);
        var now = DateTime.UtcNow.ToString("o");
        cmd.Parameters.AddWithValue("$ts", now);

        _logger?.LogTrace("PostTwoParams: Executing insert command");
        cmd.ExecuteNonQuery();

        _logger?.LogTrace("PostTwoParams: Retrieving last inserted row id");
        using var idCmd = conn.CreateCommand();
        idCmd.CommandText = "SELECT last_insert_rowid();";
        var id = (long)idCmd.ExecuteScalar();

        _logger?.LogInformation("PostTwoParams: Successfully inserted record with Id: {Id}, Param1: {Param1}, Param2: {Param2}", id, request.Param1, request.Param2);
        return TypedResults.Ok(new { Id = id, Param1 = request.Param1, Param2 = request.Param2, CreatedUtc = now });
    }
}
