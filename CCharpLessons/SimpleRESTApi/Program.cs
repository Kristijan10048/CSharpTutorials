using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.IO;
using SQLitePCL;
[assembly: InternalsVisibleTo("SimpleRESTApi.Tests")]

var builder = WebApplication.CreateSlimBuilder(args);

// Compute DB path early so we can register DbContext with the same SQLite file
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "simple_rest_api.db");
 Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? builder.Environment.ContentRootPath);

// Register EF Core DbContext using the same SQLite file.
// If a compiled model has been generated (see EF Core dbcontext optimize), try to register it.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Ensure migrations are looked up in the DbContext assembly by default
    options.UseSqlite($"Data Source={dbPath}", b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name));

    try
    {
        // Common generated model full type name used by dotnet ef dbcontext optimize.
        // The generated type is typically in a CompiledModels namespace and has a
        // public static Instance property exposing the IModel.
        var typeNames = new[] {
            "CompiledModels.AppDbContextModel, SimpleRESTApi",
            "CompiledModels.AppDbContextModel, SimpleRESTApi",
            // fallback without assembly, in case the generator used the project's root namespace
            "CompiledModels.AppDbContextModel"
        };

        foreach (var tn in typeNames)
        {
            var t = Type.GetType(tn, throwOnError: false, ignoreCase: true);
            if (t is null) continue;
            var instProp = t.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            if (instProp is null) continue;
            var model = instProp.GetValue(null) as IModel;
            if (model is null) continue;
            options.UseModel(model);
            break;
        }
    }
    catch
    {
        // Ignore any errors here and let EF Core try to build the model at runtime
        // (which will fail under NativeAOT). This fallback keeps behavior unchanged
        // when no compiled model is present.
    }
});

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
    var logger = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger("Startup");

    // Log migrations assembly and applied/pending migrations before attempting to migrate
    try
    {
        var migrationsAssembly = db.GetService<IMigrationsAssembly>();
        logger?.LogInformation("Migrations assembly: {Name}", migrationsAssembly?.Assembly?.GetName().Name ?? "(null)");

        if (migrationsAssembly?.Migrations != null && migrationsAssembly.Migrations.Count > 0)
        {
            logger?.LogInformation("Migrations found: {Migs}", string.Join(", ", migrationsAssembly.Migrations.Keys));
        }
        else
        {
            logger?.LogWarning("No migration types discovered in migrations assembly.");
        }

        var appliedBefore = db.Database.GetAppliedMigrations().ToArray();
        var pendingBefore = db.Database.GetPendingMigrations().ToArray();
        logger?.LogInformation("Applied migrations (before Migrate): {Applied}", string.Join(", ", appliedBefore));
        logger?.LogInformation("Pending migrations (before Migrate): {Pending}", string.Join(", ", pendingBefore));
    }
    catch (Exception ex)
    {
        logger?.LogWarning(ex, "Failed to retrieve migrations diagnostics before Migrate.");
    }

    try
    {
        // Apply EF Core migrations at startup so the DB schema matches the
        // migrations included in the project. This is preferable to EnsureCreated
        // when using migrations.
        db.Database.Migrate();
        logger?.LogInformation("Applied EF Core migrations to DB file: {DbPath}", dbPath);

        // Log applied/pending after migration
        try
        {
            var appliedAfter = db.Database.GetAppliedMigrations().ToArray();
            var pendingAfter = db.Database.GetPendingMigrations().ToArray();
            logger?.LogInformation("Applied migrations (after Migrate): {Applied}", string.Join(", ", appliedAfter));
            logger?.LogInformation("Pending migrations (after Migrate): {Pending}", string.Join(", ", pendingAfter));
        }
        catch (Exception ex)
        {
            logger?.LogWarning(ex, "Failed to retrieve applied/pending migrations after Migrate.");
        }
    }
    catch (InvalidOperationException ex) when (ex.Message?.Contains("NativeAOT") == true)
    {
        // When running as NativeAOT the runtime model building is not supported.
        // If a compiled model has not been generated, skip EnsureCreated and log a warning.
        if (logger is not null)
        {
            logger.LogWarning(ex, "Skipping EnsureCreated because runtime model building is not supported with NativeAOT. Generate a compiled model with 'dotnet ef dbcontext optimize'.");
        }
        else
        {
            Console.WriteLine("Warning: Skipping EnsureCreated because runtime model building is not supported with NativeAOT. Generate a compiled model with 'dotnet ef dbcontext optimize'.");
        }
    }
    catch (Exception ex)
    {
        // If migrations fail for other reasons, attempt a dev-friendly EnsureCreated fallback
        if (logger is not null)
        {
            logger.LogWarning(ex, "Migrate() failed, attempting EnsureCreated() fallback (dev only).");
        }
        else
        {
            Console.WriteLine($"Migrate() failed: {ex}. Attempting EnsureCreated() fallback (dev only).");
        }

        try
        {
            db.Database.EnsureCreated();
            logger?.LogInformation("EnsureCreated() succeeded for DB file: {DbPath}", dbPath);
        }
        catch (Exception ex2)
        {
            if (logger is not null)
                logger.LogError(ex2, "EnsureCreated() also failed - schema not present.");
            else
                Console.WriteLine($"EnsureCreated() also failed: {ex2}");

            throw; // rethrow so startup fails visibly
        }
    }

    // Log contents of __EFMigrationsHistory for diagnostics
    try
    {
        using var histConn = new SqliteConnection($"Data Source={dbPath}");
        histConn.Open();
        using var histCmd = histConn.CreateCommand();
        histCmd.CommandText = "SELECT MigrationId, ProductVersion FROM __EFMigrationsHistory ORDER BY MigrationId;";
        using var histReader = histCmd.ExecuteReader();
        var histRows = new System.Collections.Generic.List<string>();
        while (histReader.Read())
        {
            var mig = histReader.IsDBNull(0) ? "(null)" : histReader.GetString(0);
            var ver = histReader.IsDBNull(1) ? "(null)" : histReader.GetString(1);
            histRows.Add($"{mig} (EF Core {ver})");
        }

        if (histRows.Count == 0)
        {
            logger?.LogInformation("__EFMigrationsHistory is empty for DB file: {DbPath}", dbPath);
        }
        else
        {
            logger?.LogInformation("__EFMigrationsHistory rows: {Rows}", string.Join("; ", histRows));
        }
    }
    catch (Exception ex)
    {
        logger?.LogWarning(ex, "Failed to read __EFMigrationsHistory from DB file.");
    }

    // Log existing tables in the SQLite file to help diagnose schema issues
    try
    {
        using var listConn = new SqliteConnection($"Data Source={dbPath}");
        listConn.Open();
        using var listCmd = listConn.CreateCommand();
        listCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
        using var reader = listCmd.ExecuteReader();
        var tables = new System.Collections.Generic.List<string>();
        while (reader.Read())
        {
            tables.Add(reader.GetString(0));
        }

        foreach (var table in tables)
        {
            logger?.LogInformation("Table found: {Table}", table);
        }

        if (tables.Count == 0)
        {
            logger?.LogWarning("No tables found in SQLite database. Schema might not be initialized.");
        }
    }
    catch (Exception ex)
    {
        // Log diagnostics error but don't stop startup
        var scopedLoggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
        var diagLogger = scopedLoggerFactory is not null
            ? scopedLoggerFactory.CreateLogger("Startup")
            : null;

        if (diagLogger is not null)
            diagLogger.LogWarning(ex, "Failed to list SQLite tables for diagnostics");
        else
            Console.WriteLine($"Failed to list SQLite tables: {ex}");
    }
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

// Start the web application and listen for incoming requests
app.Run();







