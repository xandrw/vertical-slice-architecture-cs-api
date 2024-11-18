using Domain.Users;
using Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Serilog;

namespace Spec.Seeders;

public static class UsersSeeder
{
    public static void Seed(SqliteConnection connection, ILogger logger)
    {
        var passwordHasher = new HmacSha512PasswordHasher();

        User[] users =
        [
            User.Create("test.admin@email.com", "Admin", "password", passwordHasher.HashPassword),
            User.Create("test.author@example.com", "Author", "password", passwordHasher.HashPassword)
        ];

        string adminId = "137";
        string authorId = "175";
        foreach (var user in users)
        {
            var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = @"
                INSERT INTO Users (Id, Email, Role, PasswordHash, PasswordSalt)
                VALUES (@Id, @Email, @Role, @PasswordHash, @PasswordSalt);
            ";
            insertCommand.Parameters.AddWithValue("@Id", user.Role == Role.Admin ? adminId : authorId);
            insertCommand.Parameters.AddWithValue("@Email", user.Email);
            insertCommand.Parameters.AddWithValue("@Role", user.Role);
            insertCommand.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            insertCommand.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
            insertCommand.ExecuteNonQuery();
            logger.Information($"[SpecFlow.UsersSeeder]: User {user.Email} with {user.Role} role seeded.");
        }
    }

    public static void Cleanup(SqliteConnection connection, ILogger logger)
    {
        var cleanupCommand = connection.CreateCommand();
        cleanupCommand.CommandText = "DELETE FROM Users WHERE Email LIKE 'test.%';";
        cleanupCommand.ExecuteNonQuery();

        var resetSequenceCommand = connection.CreateCommand();
        resetSequenceCommand.CommandText = @"
            UPDATE sqlite_sequence
            SET seq = COALESCE((SELECT Id FROM Users ORDER BY Id DESC LIMIT 1), 0)
            WHERE name = 'Users';
        ";
        resetSequenceCommand.ExecuteNonQuery();

        logger.Information("[SpecFlow.UsersSeeder]: Cleaned up.");
    }
}