using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Xunit;

namespace SimpleRESTApi.Tests
{
    public class PostTwoParamsTests
    {
        [Fact]
        public void PostTwoParams_InsertsRecordIntoDatabase()
        {
            var dbPath = Path.Combine(Path.GetTempPath(), $"testdb_{Guid.NewGuid():N}.db");
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? Path.GetTempPath());

                using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                {
                    conn.Open();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Params (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Param1 TEXT,
                        Param2 TEXT,
                        CreatedUtc TEXT
                    );";
                    cmd.ExecuteNonQuery();
                }

                // Create a mock logger factory for testing
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Trace));

                // Initialize the endpoints helper with the test DB path and logger factory
                Endpoints.Init(dbPath, loggerFactory);

                // Execute the method under test
                var result = Endpoints.PostTwoParams(new TwoParamsRequest("alpha", "beta"));
                Assert.NotNull(result);

                // Verify the row was inserted
                using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                {
                    conn.Open();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT Param1, Param2 FROM Params ORDER BY Id DESC LIMIT 1;";
                    using var reader = cmd.ExecuteReader();
                    Assert.True(reader.Read(), "No rows inserted.");
                    var p1 = reader.GetString(0);
                    var p2 = reader.GetString(1);
                    Assert.Equal("alpha", p1);
                    Assert.Equal("beta", p2);
                }
            }
            finally
            {
                if (File.Exists(dbPath)) File.Delete(dbPath);
            }
        }
    }
}
