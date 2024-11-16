using Microsoft.Data.Sqlite;
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

        if (featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Seed(_connection);
        }
    }

    [AfterFeature]
    public static void CleanupDatabase(FeatureContext featureContext)
    {
        if (_connection is null) return;
        
        if (featureContext.FeatureInfo.Tags.Contains("SeedUsers"))
        {
            UsersSeeder.Cleanup(_connection);
        }

        if (_connection.State != System.Data.ConnectionState.Open) return;
        
        _connection.Close();
        _connection.Dispose();
    }
}