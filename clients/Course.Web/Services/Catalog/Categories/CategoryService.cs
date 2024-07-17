using Course.Shared.Dtos;
using Course.Web.Models.Catalog.Categories;
using Course.Web.Services.Interfaces.Catalog;

namespace Course.Web.Services.Catalog.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateAsync(CategoryCreateInput model)
    {
        var response = await _httpClient.PostAsJsonAsync("categories/create", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string categoryId)
    {
        var response = await _httpClient.PutAsJsonAsync("categories/delete", categoryId);
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("categories/getall");
        if (!response.IsSuccessStatusCode) { return []; }
        var courses = await response.Content.ReadFromJsonAsync<Response<IEnumerable<CategoryViewModel>>>();
        return courses!.Data!;
    }

    public async Task<CategoryViewModel> GetById(string categoryId)
    {
        var response = await _httpClient.GetAsync($"categories/getbyid/{categoryId}");
        if (!response.IsSuccessStatusCode) { return null; }
        var courses = await response.Content.ReadFromJsonAsync<Response<CategoryViewModel>>();
        return courses!.Data!;
    }

    public async Task<bool> UpdateAsync(CategoryUpdateInput model)
    {
        var response = await _httpClient.PostAsJsonAsync("categories/update", model);
        return response.IsSuccessStatusCode;
    }
}
