using Asp.Versioning.ApiExplorer;

namespace vm.referendum.Api.Options;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));

        var scheme = GetJwtSecurityScheme();
        options.AddSecurityDefinition(scheme.Reference.Id, scheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                scheme,
                Array.Empty<string>()
            }
        });
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = $"Referendum v{description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated) info.Description = "This API version has been deprecated";

        return info;
    }

    private static OpenApiSecurityScheme GetJwtSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            Name = "JWT Authentication",
            Description = "Provide a JWT Bearer",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
    }
}