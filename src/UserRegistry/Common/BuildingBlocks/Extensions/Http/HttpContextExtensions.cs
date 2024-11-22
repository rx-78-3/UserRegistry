using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Extensions.Http;
public static class HttpContextExtensions
{
    public static string? ExtractTokenFromContext(this HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            var bearerToken = authorizationHeader.ToString();
            if (bearerToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return bearerToken.Substring("Bearer ".Length).Trim();
            }
        }
        return null;
    }
}
