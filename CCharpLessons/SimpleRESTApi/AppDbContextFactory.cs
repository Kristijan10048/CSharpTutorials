using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var path = Environment.GetEnvironmentVariable("SIMPLE_REST_DB") ?? "simple_rest_api.db";
        optionsBuilder.UseSqlite($"Data Source={path}");
        return new AppDbContext(optionsBuilder.Options);
    }
}
