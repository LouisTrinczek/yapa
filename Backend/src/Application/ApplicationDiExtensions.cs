using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDiExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // This is where the Application Services Dependency Injection are registered
        // services.AddScoped<IService, Service>();
        return services;
    }
}