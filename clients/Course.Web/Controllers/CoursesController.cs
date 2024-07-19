using Course.Shared.Services;
using Course.Web.Models.Catalog.Courses;
using Course.Web.Services.Interfaces.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course.Web.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ICategoryService _categoryService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public CoursesController(ICourseService courseService,
        ISharedIdentityService sharedIdentityService,
        ICategoryService categoryService)
    {
        _courseService = courseService;
        _sharedIdentityService = sharedIdentityService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _sharedIdentityService.GetUserId;
        var data = await _courseService.GetAllByUserIdAsync(userId);
        return View(data);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateInput model)
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        if (!ModelState.IsValid)
        {
            return View();
        }

        model.UserId = _sharedIdentityService.GetUserId;
        var result = await _courseService.CreateAsync(model);

        if (result)
        {
            ViewBag.Result = "Course has been successfully added";
        }
        return RedirectToAction(nameof(Index));
    }
}
