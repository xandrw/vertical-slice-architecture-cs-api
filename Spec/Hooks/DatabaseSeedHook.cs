using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Serilog;
using Spec.Seeders;

namespace Spec.Hooks;

[Binding]
public sealed class DatabaseSeedHook
{
    private const string DatabasePath = "../../../../Infrastructure/Database/app.db";
    private static SqliteConnection? _connection;

    [BeforeFeature]
    public static void SeedDatabase(FeatureContext featureContext)
    {
        _connection ??= new SqliteConnection($"Data Source={DatabasePath}");

        if (_connection.State != System.Data.ConnectionState.Open) _connection.Open();

        Log.Information("[SpecFlow.DatabaseSeedHook]: Connected to database");

        if (Debugger.IsAttached)
        {
            Log.Warning("[SpecFlow.DatabaseSeedHook]: Debugger attached, cleanup might not run if process is exited.");
        }

        if (featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Seed(_connection, Log.Logger);
        }
    }

    [AfterFeature]
    public static void CleanupDatabase(FeatureContext featureContext)
    {
        if (_connection is null) return;

        UsersSeeder.Cleanup(_connection, Log.Logger);

        if (_connection.State == System.Data.ConnectionState.Open)
        {
            _connection.Close();
        }

        _connection.Close();
        _connection.Dispose();
        _connection = null;
    }
}