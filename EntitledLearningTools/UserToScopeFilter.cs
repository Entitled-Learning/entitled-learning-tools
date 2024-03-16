using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class UserToScopeFilter
{
    private const string UserIdKey = "UserId";
    private const string UserNameKey = "UserName";
    
    private readonly RequestDelegate _next;
    private readonly ILogger<UserToScopeFilter> _logger;

    public UserToScopeFilter(RequestDelegate next, ILogger<UserToScopeFilter> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var userId = "Anonymous";
        var userName = "Anonymous";

        // If the headers already contain an official test header, skip out.
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NoUserId";
            userName = context.User.Identity?.Name ?? "NoUserName";
        }

        var scopeState= new Dictionary<string, object>
        {
            { UserIdKey, userId },
            { UserNameKey, userName }
        };

        using (_logger.BeginScope(scopeState))
        {
            await _next(context);
        }
    }
}
