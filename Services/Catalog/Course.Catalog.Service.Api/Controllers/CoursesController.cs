using Course.Catalog.Service.Api.Models.Paging;
using Course.Catalog.Service.Api.Services.Course;
using Course.Shared.BaseController;
using Course.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Course.Catalog.Service.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CoursesController(ICourseService courseService, IPublishEndpoint publishEndpoint) : BaseController
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllAsync(cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWithCategoryAsync([FromQuery] PageRequest request, CancellationToken cancellationToken)
    {
        var result = await courseService.GetAllWithCategoryAsync(request, cancellationToken);
        return CreateActionResultInstance(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllByUserIdWithCategoryAsync(string userId, [FromQuery] PageRequest request, CancellationToken cancellationToken)
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
        if (result.IsSuccessful)
        {
            await SendEvents(dto, cancellationToken);
        }
        return CreateActionResultInstance(result);
    }

    private async Task SendEvents(Models.Course dto, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish<CourseNameUpdatedEvent>(
        new CourseNameUpdatedEvent
        {
            CourseId = dto.Id,
            CourseName = dto.Name
        }, cancellationToken);

        await _publishEndpoint.Publish<BasketCourseNameUpdatedEvent>(
            new BasketCourseNameUpdatedEvent
            {
                UserId = dto.UserId,
                CourseId = dto.Id,
                CourseName = dto.Name
            }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var result = await courseService.DeleteAsync(id, cancellationToken);
        return CreateActionResultInstance(result);
    }
}