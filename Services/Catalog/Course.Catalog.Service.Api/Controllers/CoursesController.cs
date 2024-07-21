using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Services.Course;
using Course.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllByUserIdWithCategoryAsync(string userId, CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllByUserIdWithCategory(userId, cancellationToken);
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
    public async Task<IActionResult> CreateAsync([FromBody] Models.Course course,CancellationToken cancellationToken)
    {
        var result = await courseService.CreateAsync(course, cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] Models.Course dto,CancellationToken cancellationToken)
    {
        if (Request.Form.TryGetValue("feature", out var featureJson))
        {
            dto.Feature = JsonConvert.DeserializeObject<Feature>(featureJson);
        }
        var result = await courseService.UpdateAsync(dto, cancellationToken);
        return CreateActionResultInstance(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id,CancellationToken cancellationToken)
    {
        var result = await courseService.DeleteAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }
}