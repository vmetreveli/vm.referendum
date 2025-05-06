using System.Text;
using Asp.Versioning;
using Framework.Abstractions.Repository;
using Framework.Infrastructure.Interceptors;
using Framework.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.Services;
using vm.referendum.Infrastructure.Authentication;
using vm.referendum.Infrastructure.Authentication.Authentication;
using vm.referendum.Infrastructure.Authentication.Cryptography;
using vm.referendum.Infrastructure.Authentication.Settings;
using vm.referendum.Infrastructure.Clock;
using vm.referendum.Infrastructure.Context;
using vm.referendum.Infrastructure.Cryptography;
using vm.referendum.Infrastructure.Data;
using vm.referendum.Infrastructure.Repositories;
using vm.referendum.Infrastructure.Services;

namespace vm.referendum.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services
            .AddDbContext<DbContext, DataContext>((sp, options) =>
                {
                    UpdateAuditableEntitiesInterceptor? auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>();
                    options.UseNpgsql(
                            configuration.GetConnectionString("DefaultConnection"))
                        .UseSnakeCaseNamingConvention()
                        .AddInterceptors(auditableInterceptor!);
                }
            );

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]!)),
                ClockSkew = TimeSpan.FromSeconds(5)
            });

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SETTINGS_KEY));
        services.Configure<EmailConfiguration>(configuration.GetSection(EmailConfiguration.SETTINGS_KEY));
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        services.AddScoped<IPasswordGenerator, PasswordGenerator>();

        services.AddScoped<IUnitOfWork, UnitOfWork<DataContext>>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddTransient<IEmailService, EmailService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        AddApiVersioning(services);
        // AddBackgroundJobs(services, configuration);


        return services;
    }


    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        //
        // services.AddQuartz();
        //
        // services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        //
        // services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }


    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}