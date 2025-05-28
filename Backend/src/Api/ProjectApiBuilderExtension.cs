using Api.SwaggerFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api;

public static class ProjectApiBuilderExtension
{
    public static WebApplicationBuilder AddProjectApi(this WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment()) 
            return builder;

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SupportNonNullableReferenceTypes();
            opt.OperationFilter<ResponseCodeFilter>();
            opt.SchemaFilter<RequiredDtoFieldsFilter>();
        });

        return builder;
    }
}