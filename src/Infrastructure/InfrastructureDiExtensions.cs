using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDiExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // This is where the Dependency Injection for Database Repositories are being registered
        // services.AddScoped<IRepository, Repository>();
        return services;
    }
}