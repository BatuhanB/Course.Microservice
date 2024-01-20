using Course.Catalog.Service.Api.Dtos.Course;

namespace Course.Catalog.Service.Api.Dtos.Category;

public class CategoryWithCoursesDto : BaseDto
{
    public required string Name { get; set; }
    public List<CourseDto>? Courses { get; set; }
}