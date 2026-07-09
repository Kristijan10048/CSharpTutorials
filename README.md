# SimpleRESTApi

Instructions to build, run, and test the API locally.

1. Build and run

```bash
dotnet build
dotnet run
```

By default Kestrel may listen on http://localhost:5000 and https://localhost:5001. Check the console output for the exact URL.

2. POST example using curl

Replace /your-route with your endpoint path and adjust JSON to match TwoParamsRequest properties.

```bash
curl -X POST http://localhost:5000/your-route \
  -H "Content-Type: application/json" \
  -d '{"prop1":"value","prop2":123}'
```

If your app uses HTTPS and a development certificate, skip certificate checks (not recommended for production):

```bash
curl -k -X POST https://localhost:5001/your-route \
  -H "Content-Type: application/json" \
  -d '{"prop1":"value","prop2":123}'
```

3. Notes

- After adding AppJsonSerializerContext.cs, rebuild the project so source-generated JsonTypeInfo is produced.
- If the request still fails with a NotSupportedException, provide Program.cs, the project file (.csproj), TwoParamsRequest.cs, and any existing JsonSerializerContext for further inspection.
