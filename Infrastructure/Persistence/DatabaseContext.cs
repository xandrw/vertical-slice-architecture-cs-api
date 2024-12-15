using Domain.Pages;
using Domain.Users;
using Infrastructure.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Section> Sections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(ConfigureUsers);
        modelBuilder.Entity<Page>(ConfigurePages);
        modelBuilder.Entity<Section>(ConfigureSections);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureUsers(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email).IsUniqueWithPrefix();
    }

    private void ConfigurePages(EntityTypeBuilder<Page> builder)
    {
        builder
            .HasMany(p => p.Sections)
            .WithOne()
            .HasForeignKey("PageId")
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(p => p.Title).IsUniqueWithPrefix();
    }

    private void ConfigureSections(EntityTypeBuilder<Section> builder)
    {
        builder.Property<int>("PageId");

        builder.HasIndex("PageId", nameof(Section.Name)).IsUniqueWithPrefix();
    }

    private static SqliteConnection? _sharedInMemoryConnection;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Test")
        {
            if (_sharedInMemoryConnection == null)
            {
                _sharedInMemoryConnection = new SqliteConnection(
                    "Data Source=TestDatabase;Mode=Memory;Cache=Shared"
                );
                _sharedInMemoryConnection.Open();
            }

            optionsBuilder.UseSqlite(_sharedInMemoryConnection);
        }
        else
        {
            optionsBuilder.UseSqlite($"Data Source={Path.Combine("Database", "app.db")}");
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        if (_sharedInMemoryConnection is null) return;

        _sharedInMemoryConnection.Close();
        _sharedInMemoryConnection.Dispose();
        _sharedInMemoryConnection = null;
    }
}