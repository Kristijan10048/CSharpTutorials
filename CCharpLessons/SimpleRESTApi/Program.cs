using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;

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

Todo[] sampleTodos =
[
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
];

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos)
        .WithName("GetTodos");

todosApi.MapGet("/{id}", Results<Ok<Todo>, NotFound> (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? TypedResults.Ok(todo)
        : TypedResults.NotFound())
    .WithName("GetTodoById");

// Additional test endpoint
app.MapGet("/test", () => "Test Message")
   .WithName("TestEndpoint");

// Full method endpoint defined as a regular static method in a helper class
static class Endpoints
{
    public static string TestFull()
    {
        return "Test Message";
    }

    // Demo POST endpoint method - accepts a JSON body and returns a simple response
    public static IResult PostDemo(DemoRequest request)
    {
        // Echo back a simple object
        return TypedResults.Ok(new { Message = "Received", Received = request.Message });
    }

    // POST endpoint that accepts two string parameters in the JSON body
    public static IResult PostTwoParams(TwoParamsRequest request)
    {
        // Return the received parameters in the response
        return TypedResults.Ok(new { Param1 = request.Param1, Param2 = request.Param2 });
    }
}

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
