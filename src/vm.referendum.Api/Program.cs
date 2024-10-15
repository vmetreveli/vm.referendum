using Asp.Versioning;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Framework.Infrastructure;
using vm.referendum.Api.Middleware;
using vm.referendum.Api.Swagger;
using vm.referendum.Api;
using vm.referendum.AsynchronousAdapter;
using vm.referendum.Application;
using vm.referendum.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCatalogIntegration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSerilogServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(2, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddSingleton<ExceptionHandlingMiddleware>();

builder.Services.AddEndpointsApiExplorer();


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
    //app.ApplyMigration();
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var descriptions = app.DescribeApiVersions();

    // Build a swagger endpoint for each discovered API version
    foreach (var description in descriptions)
    {
        var url = $"/swagger/{description.GroupName}/swagger.json";
        var name = description.GroupName.ToUpperInvariant();
        options.SwaggerEndpoint(url, name);
    }
});

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseErrorHandling();


app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});


app.Run();