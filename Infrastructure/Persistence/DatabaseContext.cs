using Domain.Pages;
using Domain.Users;
using Infrastructure.Extensions;
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

    protected void ConfigureUsers(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email).IsUniqueWithPrefix();
    }
    
    protected void ConfigurePages(EntityTypeBuilder<Page> builder)
    {
        builder
            .HasMany(p => p.Sections)
            .WithOne()
            .HasForeignKey("PageId")
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(p => p.Title).IsUniqueWithPrefix();
    }
    
    protected void ConfigureSections(EntityTypeBuilder<Section> builder)
    {
        builder.Property<int>("PageId");

        builder.HasIndex("PageId", nameof(Section.Name)).IsUniqueWithPrefix();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var appDbPath = Path.Combine("Database", "app.db");

            if (File.Exists(appDbPath) == false) File.Create(appDbPath);
            
            optionsBuilder.UseSqlite($"Data Source={appDbPath}");
        }
    }
}