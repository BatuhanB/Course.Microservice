using Course.Shared.Dtos;
using Course.Web.Models.Catalog.Courses;
using Course.Web.Services.Interfaces.Catalog;

namespace Course.Web.Services.Catalog.Courses;

public class CourseService : ICourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<bool> CreateAsync(CourseCreateInput model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CourseViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("courses/getallasync");
        if (!response.IsSuccessStatusCode) { return []; }
        var courses = await response.Content.ReadFromJsonAsync<Response<IEnumerable<CourseViewModel>>>();
        return courses!.Data!;
    }

    public Task<IEnumerable<CourseViewModel>> GetAllByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CourseViewModel> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(CourseUpdateInput model)
    {
        throw new NotImplementedException();
    }
}
