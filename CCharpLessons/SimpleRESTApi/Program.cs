using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using SQLitePCL;
[assembly: InternalsVisibleTo("SimpleRESTApi.Tests")]

var builder = WebApplication.CreateSlimBuilder(args);

// Compute DB path early so we can register DbContext with the same SQLite file
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "simple_rest_api.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? builder.Environment.ContentRootPath);

// Register EF Core DbContext using the same SQLite file
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ILoggerFactory>(sp => LoggerFactory.Create(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Trace)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Initialize SQLite provider and ensure DB schema
Batteries_V2.Init();

// Create Params table (used by existing endpoints) if it doesn't exist.
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

// Ensure EF Core creates Users and Devices tables based on the model
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
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

// Additional test endpoint - moved to TestEndpoints class
app.MapGet("/test", TestEndpoints.Test)
   .WithName("TestEndpoint");

app.MapGet("/testcstr", TestEndpoints.TestCstr)
   .WithName("TestCstr");

app.MapGet("/testFnCall", TestEndpoints.TestFnCall)
   .WithName("TestFnCall");


// Initialize endpoints with db path
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
Endpoints.Init(dbPath, loggerFactory);

// Map the full-method endpoints
app.MapGet("/test-full", Endpoints.TestFull)
   .WithName("TestFull");

// Map POST endpoint that uses the full method
app.MapPost("/test-demo", Endpoints.PostDemo)
   .WithName("TestDemo");

// Map POST endpoint that accepts two string params
app.MapPost("/test-two-params", Endpoints.PostTwoParams)
   .WithName("TestTwoParams");

// User endpoints using EF Core DbContext
app.MapPost("/users", async (CreateUserRequest req, AppDbContext db) =>
{
    var user = new User { Username = req.Username, Email = req.Email, CreatedUtc = DateTime.UtcNow };
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
}).WithName("CreateUser");

app.MapGet("/users", async (AppDbContext db) => await db.Users.ToArrayAsync()).WithName("GetUsers");

app.MapGet("/users/{id}", async (int id, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(id);
    return user is null ? Results.NotFound() : Results.Ok(user);
}).WithName("GetUserById");

// Device endpoints
app.MapPost("/devices", async (CreateDeviceRequest req, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(req.UserId);
    if (user is null) return Results.BadRequest("User not found");
    var device = new Device { Name = req.Name, SerialNumber = req.SerialNumber, UserId = req.UserId, CreatedUtc = DateTime.UtcNow };
    db.Devices.Add(device);
    await db.SaveChangesAsync();
    return Results.Created($"/devices/{device.Id}", device);
}).WithName("CreateDevice");

app.MapGet("/devices", async (AppDbContext db) => await db.Devices.Include(d => d.User).ToArrayAsync()).WithName("GetDevices");

app.MapGet("/devices/{id}", async (int id, AppDbContext db) =>
{
    var device = await db.Devices.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
    return device is null ? Results.NotFound() : Results.Ok(device);
}).WithName("GetDeviceById");

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

public record DemoRequest(string Message);

public record TwoParamsRequest(string Param1, string Param2);

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(DemoRequest))]
[JsonSerializable(typeof(TwoParamsRequest))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(User[]))]
[JsonSerializable(typeof(Device))]
[JsonSerializable(typeof(Device[]))]
[JsonSerializable(typeof(DestinationMachine))]
[JsonSerializable(typeof(CreateUserRequest))]
[JsonSerializable(typeof(CreateDeviceRequest))]
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

