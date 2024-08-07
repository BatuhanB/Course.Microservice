using Course.Catalog.Service.Api.Services.Category;
using Course.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace Course.Catalog.Service.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await categoryService.GetAllAsync(cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoryWithCourses(CancellationToken cancellationToken)
    {
        var result = await categoryService.GetAllCategoryWithCourses(cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await categoryService.GetByIdAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Models.Category category, CancellationToken cancellationToken)
    {
        var result = await categoryService.CreateAsync(category, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Models.Category dto, CancellationToken cancellationToken)
    {
        var result = await categoryService.UpdateAsync(dto, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var result = await categoryService.DeleteAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }
}