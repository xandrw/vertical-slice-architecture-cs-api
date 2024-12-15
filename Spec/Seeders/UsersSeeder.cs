using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Serilog;

namespace Spec.Seeders;

public static class UsersSeeder
{
    public static void Seed(DatabaseContext context, ILogger logger)
    {
        try
        {
            var passwordHasher = new HmacSha512PasswordHasher();

            User[] users =
            [
                User.Create("test.admin@email.com", "Admin", "password", passwordHasher.HashPassword),
                User.Create("test.author@example.com", "Author", "password", passwordHasher.HashPassword)
            ];

            context.Users.AddRange(users);
            context.SaveChanges();

            foreach (var user in users)
            {
                logger.Information($"[SpecFlow.UsersSeeder]: User {user.Email} with {user.Role} role seeded.");
            }
        }
        catch (Exception e)
        {
            logger.Error(e, "[SpecFlow.UsersSeeder]: An error occurred while seeding users.");
            throw;
        }
    }

    public static void Cleanup(DatabaseContext context, ILogger logger)
    {
        try
        {
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();

            logger.Information("[SpecFlow.UsersSeeder]: All seeded users removed.");
        }
        catch (Exception e)
        {
            logger.Error(e, "[SpecFlow.UsersSeeder]: An error occurred while cleaning up users.");
            throw;
        }
    }
}