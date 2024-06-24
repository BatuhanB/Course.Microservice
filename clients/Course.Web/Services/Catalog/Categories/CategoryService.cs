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

    public Task<bool> CreateAsync(CategoryCreateInput model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("categories/getallasync");
        if (!response.IsSuccessStatusCode) { return []; }
        var courses = await response.Content.ReadFromJsonAsync<Response<IEnumerable<CategoryViewModel>>>();
        return courses!.Data!;
    }

    public Task<IEnumerable<CategoryViewModel>> GetAllByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryViewModel> GetById(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(CategoryUpdateInput model)
    {
        throw new NotImplementedException();
    }
}
