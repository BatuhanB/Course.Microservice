using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.Shared.BaseController;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController : Controller
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}