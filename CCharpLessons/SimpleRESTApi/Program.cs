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



