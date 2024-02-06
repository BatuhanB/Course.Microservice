using Microsoft.AspNetCore.Http;

namespace Course.Shared.Services;

public class SharedIdentityService(IHttpContextAccessor contextAccessor) : ISharedIdentityService
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string GetUserId => _contextAccessor.HttpContext.User.FindFirst("sub").Value;
}