using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Models.Paging;
using Course.Catalog.Service.Api.Services.Course;
using Course.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace Course.Catalog.Service.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CoursesController(ICourseService courseService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllAsync(cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithCategoryAsync([FromQuery] PageRequest request, CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllWithCategoryAsync(request,cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllByUserIdWithCategoryAsync(string userId,[FromQuery] PageRequest request, CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllByUserIdWithCategory(userId, request, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await courseService.GetByIdAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdWithCategoryAsync(string id, CancellationToken cancellationToken)
    {
        var result = await courseService.GetByIdWithCategory(id, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Models.Course course, CancellationToken cancellationToken)
    {
        var result = await courseService.CreateAsync(course, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Models.Course dto, CancellationToken cancellationToken)
    {
        var result = await courseService.UpdateAsync(dto, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var result = await courseService.DeleteAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }
}