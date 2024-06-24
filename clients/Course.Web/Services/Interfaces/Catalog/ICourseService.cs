using Course.Web.Models.Catalog.Courses;

namespace Course.Web.Services.Interfaces.Catalog;

public interface ICourseService
{
    Task<IEnumerable<CourseViewModel>> GetAllAsync();
    Task<IEnumerable<CourseViewModel>> GetAllByUserIdAsync(string userId);
    Task<CourseViewModel> GetById(string id);
    Task<bool> DeleteAsync(string id);
    Task<bool> CreateAsync(CourseCreateInput model);
    Task<bool> UpdateAsync(CourseUpdateInput model);
}
