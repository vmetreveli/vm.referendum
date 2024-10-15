using Framework.Infrastructure.Exceptions;
using Serilog;
using vm.referendum.Api;
using vm.referendum.Api.Constants;
using vm.referendum.Api.Options;
using vm.referendum.Api.Policies;
using vm.referendum.Application;
using vm.referendum.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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
//builder.Services.AddFramework(builder.Configuration, typeof(Program).Assembly);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSerilogServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();


//builder.Services.AddTransient<ExceptionMiddleware>();
//builder.Services.AddTransient<RequestResponseLoggingMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.AddSerilog();
var app = builder.Build();
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
        foreach (var description in app.DescribeApiVersions())
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });

    // app.ApplyMigration();
    app.UseDeveloperExceptionPage();
}

// app.UseSwagger();
//
// app.UseSwaggerUI(swaggerUiOptions =>
//     swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Referendum API"));


app.UseMiddlewares();
// app.UseMiddleware<RequestResponseLoggingMiddleware>();

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