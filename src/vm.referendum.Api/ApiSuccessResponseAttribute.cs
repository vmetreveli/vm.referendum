using Swashbuckle.AspNetCore.Annotations;

namespace vm.referendum.Api;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ApiSuccessResponseAttribute(int statusCode, string description = null, Type type = null)
    : SwaggerResponseAttribute(statusCode, $"<ul><li>{description}</li><ul>", type);