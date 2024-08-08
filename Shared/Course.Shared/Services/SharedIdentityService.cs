using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Course.Shared.Services;

public class SharedIdentityService(IHttpContextAccessor contextAccessor) : ISharedIdentityService
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string GetUserId
    {
        get
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext == null)
            {
                Console.WriteLine("HttpContext is null.");
                throw new NullReferenceException("HttpContext is null.");
            }

            var user = httpContext.User;
            if (user == null)
            {
                Console.WriteLine("User is null.");
                throw new NullReferenceException("User is null.");
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("User ID is null.");
                throw new NullReferenceException("User ID cannot be null or empty.");
            }

            return userId;
        }
    }
}