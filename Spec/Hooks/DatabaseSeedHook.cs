using System.Data.Common;
using Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Spec.Seeders;

namespace Spec.Hooks;

[Binding]
public sealed class DatabaseSeedHook
{
    private static DatabaseContext? _context;
    private static DbConnection? _sharedConnection;

    [BeforeFeature]
    public static void SeedDatabase(FeatureContext featureContext)
    {
        _sharedConnection ??= new SqliteConnection("Data Source=TestDatabase;Mode=Memory;Cache=Shared");
        _sharedConnection.Open();

        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(_sharedConnection)
            .Options;

        _context ??= new DatabaseContext(options);

        Log.Information("[SpecFlow.DatabaseSeedHook]: Connected to in-memory database");

        _context.Database.EnsureCreated();
        Log.Information("[SpecFlow.DatabaseSeedHook]: Database schema ensured.");

        if (featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Seed(_context, Log.Logger);
        }
    }

    [AfterFeature]
    public static void CleanupDatabase(FeatureContext featureContext)
    {
        if (_context is not null && featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Cleanup(_context, Log.Logger);
        }

        _context?.Dispose();
        _context = null;

        if (_sharedConnection is not null)
        {
            _sharedConnection.Close();
            _sharedConnection.Dispose();
            _sharedConnection = null;
        }

        Log.Information("[SpecFlow.DatabaseSeedHook]: In-memory SQLite database disposed.");
    }
}