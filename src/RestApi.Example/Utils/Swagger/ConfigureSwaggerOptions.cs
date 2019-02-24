using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace RestApi.Example.Utils.Swagger
{
    /// <inheritdoc />
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        private readonly SwaggerSettings settings;

        /// <inheritdoc />
        public ConfigureSwaggerOptions(IOptions<SwaggerSettings> settings)
        {
            this.settings = settings?.Value ?? new SwaggerSettings();
        }

        /// <inheritdoc />
        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = settings.RoutePrefixWithSlash + "{documentName}/swagger.json";
        }
    }
}
