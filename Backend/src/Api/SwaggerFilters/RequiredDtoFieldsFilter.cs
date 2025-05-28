using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.SwaggerFilters;

public class RequiredDtoFieldsFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties is not { Count: > 0 })
        {
            return;
        }

        var notNullableProperties = schema.Properties
            .Where(property => !property.Value.Nullable && !schema.Required.Contains(property.Key))
            .Select(property => property.Key)
            .ToList();

        schema.Required.UnionWith(notNullableProperties);
    }
}