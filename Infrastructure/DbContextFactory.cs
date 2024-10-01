using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure;

public class DbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION");
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new DatabaseContext(optionsBuilder.Options);
    }
}