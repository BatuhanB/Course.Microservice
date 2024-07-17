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

    public async Task<bool> CreateAsync(CourseCreateInput model)
    {
        var response = await _httpClient.PostAsJsonAsync("courses/create", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var response = await _httpClient.PutAsJsonAsync("courses/delete", id);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<CourseViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("courses/getall");
        if (!response.IsSuccessStatusCode) { return []; }
        var courses = await response.Content.ReadFromJsonAsync<Response<IEnumerable<CourseViewModel>>>();
        return courses!.Data!;
    }

    public async Task<IEnumerable<CourseWithCategoryViewModel>> GetAllByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync("courses/getallbyuseridwithcategory");
        if (!response.IsSuccessStatusCode) { return []; }
        var courses = await response.Content.ReadFromJsonAsync<Response<IEnumerable<CourseWithCategoryViewModel>>>();
        return courses!.Data!;
    }

    public async Task<CourseViewModel> GetById(string id)
    {
        var response = await _httpClient.GetAsync("courses/getbyid");
        if (!response.IsSuccessStatusCode) { return null; }
        var courses = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
        return courses!.Data!;
    }

    public async Task<bool> UpdateAsync(CourseUpdateInput model)
    {
        var response = await _httpClient.PostAsJsonAsync("courses/update", model);
        return response.IsSuccessStatusCode;
    }
}
