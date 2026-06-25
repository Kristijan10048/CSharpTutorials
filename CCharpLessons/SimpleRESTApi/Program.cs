using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System.IO;
using SQLitePCL;
[assembly: InternalsVisibleTo("SimpleRESTApi.Tests")]

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Initialize SQLite database
// Ensure SQLitePCL provider is initialized
Batteries_V2.Init();
var dbPath = Path.Combine(app.Environment.ContentRootPath, "simple_rest_api.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? app.Environment.ContentRootPath);
using (var initConn = new SqliteConnection($"Data Source={dbPath}"))
{
    initConn.Open();
    using var cmd = initConn.CreateCommand();
    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Params (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Param1 TEXT,
        Param2 TEXT,
        CreatedUtc TEXT
    );";
    cmd.ExecuteNonQuery();
}

var sampleTodos = new Todo[]
{
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos)
        .WithName("GetTodos");

todosApi.MapGet("/{id}", (HttpContext ctx) =>
{
    var idStr = ctx.Request.RouteValues["id"]?.ToString();
    if (!int.TryParse(idStr, out var id))
        return TypedResults.BadRequest();

    var todo = sampleTodos.FirstOrDefault(a => a.Id == id);
    IResult result = todo is not null ? TypedResults.Ok(todo) : TypedResults.NotFound();
    return result;
})
.WithName("GetTodoById");

// Additional test endpoint
app.MapGet("/test", () => "Test Message")
   .WithName("TestEndpoint");


// Initialize endpoints with db path// Initialize endpoints with db path
Endpoints.Init(dbPath);

// Map the full-method endpoints
app.MapGet("/test-full", Endpoints.TestFull)
   .WithName("TestFull");

// Map POST endpoint that uses the full method
app.MapPost("/test-demo", Endpoints.PostDemo)
   .WithName("TestDemo");

// Map POST endpoint that accepts two string params
app.MapPost("/test-two-params", Endpoints.PostTwoParams)
   .WithName("TestTwoParams");

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

public record DemoRequest(string Message);

public record TwoParamsRequest(string Param1, string Param2);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}

/// <summary>
/// Endpoints helper class
/// Contains full-method endpoint handlers used by MapGet/MapPost above. These methods are regular static
/// methods (not lambdas) so they can be referenced directly when mapping routes. Keep top-level statements
/// before this type declaration (top-level statements must appear first in the file).
/// </summary>
static class Endpoints
{
    // Path to the SQLite database file. Initialized via Init(...).
    private static string? _dbPath;

    /// <summary>
    /// Initialize the helper with the database file path.
    /// Call this once from the top-level code before mapping endpoints that use the DB.
    /// </summary>
    public static void Init(string dbPath) => _dbPath = dbPath;

    /// <summary>
    /// A simple test endpoint that returns a static string.
    /// Mapped as: app.MapGet("/test-full", Endpoints.TestFull)
    /// </summary>
    public static string TestFull()
    {
        return "Test Message";
    }

    /// <summary>
    /// Demo POST endpoint that echoes the received DemoRequest.
    /// Mapped as: app.MapPost("/test-demo", Endpoints.PostDemo)
    /// </summary>
    public static IResult PostDemo(DemoRequest request)
    {
        // Echo back a simple object
        return TypedResults.Ok(new { Message = "Received", Received = request.Message });
    }

    /// <summary>
    /// POST endpoint that accepts two string parameters in the JSON body and stores them in SQLite.
    /// Returns the created row id and stored values. Requires Init(...) to have been called first.
    /// Mapped as: app.MapPost("/test-two-params", Endpoints.PostTwoParams)
    /// </summary>
    public static IResult PostTwoParams(TwoParamsRequest request)
    {
        if (string.IsNullOrEmpty(_dbPath))
            return TypedResults.Problem("Database not initialized");

        using var conn = new SqliteConnection($"Data Source={_dbPath}");
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Params (Param1, Param2, CreatedUtc) VALUES ($p1, $p2, $ts);";
        cmd.Parameters.AddWithValue("$p1", request.Param1 ?? string.Empty);
        cmd.Parameters.AddWithValue("$p2", request.Param2 ?? string.Empty);
        var now = DateTime.UtcNow.ToString("o");
        cmd.Parameters.AddWithValue("$ts", now);
        cmd.ExecuteNonQuery();

        using var idCmd = conn.CreateCommand();
        idCmd.CommandText = "SELECT last_insert_rowid();";
        var id = (long)idCmd.ExecuteScalar();

        return TypedResults.Ok(new { Id = id, Param1 = request.Param1, Param2 = request.Param2, CreatedUtc = now });
    }
}

