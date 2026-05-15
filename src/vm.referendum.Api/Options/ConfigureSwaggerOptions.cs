using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

namespace vm.referendum.Api.Options;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }

        const string schemeName = JwtBearerDefaults.AuthenticationScheme;

        options.AddSecurityDefinition(schemeName, GetJwtSecurityScheme());

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = schemeName
                    }
                },
                Array.Empty<string>()
            }
        });
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        return new OpenApiInfo
        {
            Title = $"Referendum v{description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = description.IsDeprecated
                ? "This API version has been deprecated"
                : null
        };
    }

    private static OpenApiSecurityScheme GetJwtSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "JWT Bearer token. Example: Bearer {token}",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };
    }
}