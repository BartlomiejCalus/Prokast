using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Prokast.Server.Filters
{
    public class ExcludeEntitiesFilter: ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.Namespace != null && context.Type.Namespace.Contains("Entities"))
            {
                schema.Properties.Clear();
                schema.Type = null;
            }
        }
    }
}
