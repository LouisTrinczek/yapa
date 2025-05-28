using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.SwaggerFilters;

public class ResponseCodeFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var problemDetailsSchema = context.SchemaGenerator.GenerateSchema(
            typeof(ProblemDetails),
            context.SchemaRepository
        );

        var contentDictionary = new Dictionary<string, OpenApiMediaType>
        {
            ["application/json"] = new OpenApiMediaType { Schema = problemDetailsSchema }
        };

        var defaultResponses = new Dictionary<string, string>
        {
            ["400"] = "Bad Request",
            ["401"] = "Unauthorized",
            ["403"] = "Forbidden",
            ["404"] = "Not Found",
            ["500"] = "Internal Server Error"
        };

        foreach (var response in defaultResponses)
        {
            if (!operation.Responses.ContainsKey(response.Key))
            {
                operation.Responses.Add(response.Key, new OpenApiResponse
                {
                    Description = response.Value,
                    Content = contentDictionary
                });
            }
        }
    }
}