using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CodeReviewCsharpTest;

public class CorsPolicyConfig
{
    // Vulnerable CORS: Wildcard origin with credentials allowed
    public void Configure(CorsPolicyBuilder builder)
    {
        builder.AllowAnyOrigin().AllowCredentials();
    }
}
