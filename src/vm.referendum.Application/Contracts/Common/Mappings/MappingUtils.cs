using System.Reflection;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;

namespace vm.referendum.Application.Contracts.Common.Mappings;

public static class MappingUtils
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        return services
            .AddAutoMapper(cfg =>
                    cfg.AddExpressionMapping(),
                Assembly.GetExecutingAssembly());
    }
}