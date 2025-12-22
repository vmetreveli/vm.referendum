using System.Reflection;
namespace vm.referendum.Application.Contracts.Common.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic &&
                               !assembly.FullName.StartsWith("System") &&
                               !assembly.FullName.StartsWith("Microsoft"));

        foreach (Assembly assembly in assemblies)
        {
            List<Type> types = assembly.GetExportedTypes()
                .Where(type => Array.Exists(type.GetInterfaces(), tp => tp == typeof(IMap))).ToList();

            foreach (Type type in types)
            {
                MethodInfo? methodInfo = type.GetMethod(nameof(IMap.Mapping));
                methodInfo?.Invoke(Activator.CreateInstance(type), new object[]
                {
                    this
                });
            }
        }
    }
}