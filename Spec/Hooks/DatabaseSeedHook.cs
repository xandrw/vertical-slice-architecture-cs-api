using Microsoft.Data.Sqlite;
using Serilog;
using Spec.Seeders;

namespace Spec.Hooks;

[Binding]
public sealed class DatabaseSeedHook
{
    private const string DatabasePath = "../../../../Infrastructure/Database/app.db";
    private static SqliteConnection? _connection;
    private static ILogger _logger = LoggerHooks.GetLogger();

    [BeforeFeature]
    public static void SeedDatabase(FeatureContext featureContext)
    {
        _connection ??= new SqliteConnection($"Data Source={DatabasePath}");

        if (_connection.State != System.Data.ConnectionState.Open) _connection.Open();
        
        _logger.Information("[SpecFlow.DatabaseSeedHook]: Connected to database");

        if (featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Seed(_connection, _logger);
        }
    }

    [AfterFeature]
    public static void CleanupDatabase(FeatureContext featureContext)
    {
        if (_connection is null) return;
        
        if (featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Cleanup(_connection, _logger);
        }

        if (_connection.State != System.Data.ConnectionState.Open) return;
        
        _connection.Close();
        _connection.Dispose();
    }
}