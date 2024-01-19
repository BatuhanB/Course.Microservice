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
    public async Task<IActionResult> GetAllWithCategoryAsync(CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllWithCategoryAsync(cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAllWithCategoryAsync(Guid userId,CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllByUserIdWithCategory(userId,cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        var result = await courseService.GetByIdAsync(id,cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdWithCategoryAsync(Guid id,CancellationToken cancellationToken)
    {
        var result = await courseService.GetByIdWithCategory(id, cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Models.Course course,CancellationToken cancellationToken)
    {
        var result = await courseService.CreateAsync(course, cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Models.Course course,CancellationToken cancellationToken)
    {
        var result = await courseService.UpdateAsync(course, cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        var result = await courseService.DeleteAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }
}