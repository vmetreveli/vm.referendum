using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace vm.referendum.Api.Policies;

public class AuthenticatedUserPolicy : IRateLimiterPolicy<string>
{
    public static readonly AuthenticatedUserPolicy Instance = new();

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        const int nonAuthPermitLimit = 40; // 40 requests per minute
        const int authenticatedPermitLimit = 400; // 400 requests per minute
        TimeSpan window = TimeSpan.FromMinutes(1);

        bool isAuthenticated = httpContext.User.Identity?.IsAuthenticated == true;

        // Authenticated requests
        if (isAuthenticated)
        {
            string identityName = httpContext.User.Identity?.Name!;

            return RateLimitPartition.GetFixedWindowLimiter(
                identityName!,
                partition => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = authenticatedPermitLimit,
                    Window = window
                }
            );
        }

        // Non-authenticated requests
        return RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Request.Headers.Host.ToString(),
            partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = nonAuthPermitLimit,
                Window = window
            }
        );
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected =>
        (context, lease) =>
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            return new ValueTask();
        };
}