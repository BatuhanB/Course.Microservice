using Course.Shared.Dtos;

namespace Course.Discount.Service.Api.Service.Contracts;
public interface IDiscountService
{
    Task CreateDbIfNotExists();
    Task<Response<List<Models.Discount>>> GetAllAsync();
    Task<Response<Models.Discount>> GetByIdAsync(int id);
    Task<Response<NoContent>> CreateAsync(Models.Discount discount);
    Task<Response<NoContent>> UpdateAsync(Models.Discount discount);
    Task<Response<NoContent>> DeleteAsync(int id);
    Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code,string userId);
}
