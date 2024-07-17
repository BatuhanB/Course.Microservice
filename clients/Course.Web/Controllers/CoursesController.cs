using Course.Shared.Services;
using Course.Web.Services.Interfaces.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Web.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public CoursesController(ICourseService courseService,
        ISharedIdentityService sharedIdentityService)
    {
        _courseService = courseService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _sharedIdentityService.GetUserId;
        var data = await _courseService.GetAllByUserIdAsync(userId);
        return View(data);
    }
}
