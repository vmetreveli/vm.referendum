using Asp.Versioning.ApiExplorer;
using Framework.Infrastructure;
using Serilog;
using vm.referendum.Api;
using vm.referendum.Api.Constants;
using vm.referendum.Api.Options;
using vm.referendum.Api.Policies;
using vm.referendum.Application;
using vm.referendum.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy(PolicyNames.AuthenticatedUserPolicy, AuthenticatedUserPolicy.Instance);
});

// Added cache policy
builder.Services.AddOutputCache(opts =>
{
    opts.AddPolicy(PolicyNames.TwentySecondsCachePolicy, policyBuilder =>
        policyBuilder.Expire(TimeSpan.FromSeconds(20)));
});

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSerilogServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.AddSerilog();
WebApplication app = builder.Build();
//app.MapHealthChecks("_health");
// Apply the CORS policy globally

app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in app.DescribeApiVersions())
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });


    app.ApplyMigration();
    app.UseDeveloperExceptionPage();
}

app.UseErrorHandling();

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Grouping routes
// app.MapGroup(ApiRoutes.BaseRoute)
//     .RequireRateLimiting(PolicyNames.AuthenticatedUserPolicy); // Requiring the AuthenticatedUserPolicy

app.UseEndpoints(endpoints => endpoints.MapControllers());


app.Run();