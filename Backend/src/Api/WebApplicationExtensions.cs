using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class WebApplicationExtensions
{
    public static void UseProjectApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("DevelopmentPolicy");
        app.UseHttpsRedirection();
        app.UseExceptionHandler(ConfigureExceptionHandler);
        app.UseEndpoints();
    }

    private static void ConfigureExceptionHandler(IApplicationBuilder errorApp)
    {
        errorApp.Run(async context =>
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Something went wrong.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "A fatal error occurred. Please try again later, or see an employee."
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        });
    }

    public static void UseEndpoints(this WebApplication app)
    {
        // This is where the Endpoints are being mapped via extension methods
        // app.MapXYZEndpoints();
    }
}