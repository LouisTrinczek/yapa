using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ProjectDatabaseContext(DbContextOptions options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDatabaseContext).Assembly);
    }
}