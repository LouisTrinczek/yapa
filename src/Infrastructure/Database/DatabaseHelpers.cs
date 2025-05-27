using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database;

public static class DatabaseHelpers
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<ProjectDatabaseContext>(x => x.UseNpgsql(configuration.GetConnectionString("Database")));
    } 
}