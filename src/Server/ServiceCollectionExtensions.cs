using Api;
using Application;
using Infrastructure;
using Infrastructure.Database;

namespace Server;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddProject(this WebApplicationBuilder builder) =>
        builder.AddCommon()
            .AddInfrastructureLayer()
            .AddPresentationLayer()
            .AddApplicationLayer()
            .AddDomainLayer();

    private static WebApplicationBuilder AddCommon(this WebApplicationBuilder builder)
    {
        // TODO: Add Policy for Production
        builder.Services.AddCors(o => o.AddPolicy("DevelopmentPolicy", policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        builder.Configuration.AddEnvironmentVariables();
        return builder;
    }

    private static WebApplicationBuilder AddApplicationLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        return builder;
    }

    private static WebApplicationBuilder AddDomainLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddRepositories();
        return builder;
    }

    private static WebApplicationBuilder AddInfrastructureLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddDatabaseServices(builder.Configuration);
        return builder;
    }

    private static WebApplicationBuilder AddPresentationLayer(this WebApplicationBuilder builder)
    {
        builder.AddProjectApi();
        return builder;
    }
}