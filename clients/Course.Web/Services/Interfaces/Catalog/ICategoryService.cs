using Course.Web.Models.Catalog.Categories;

namespace Course.Web.Services.Interfaces.Catalog;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllAsync();
    Task<IEnumerable<CategoryViewModel>> GetAllByUserIdAsync(string userId);
    Task<CategoryViewModel> GetById(string categoryId);
    Task<bool> DeleteAsync(string categoryId);
    Task<bool> CreateAsync(CategoryCreateInput model);
    Task<bool> UpdateAsync(CategoryUpdateInput model);
}
