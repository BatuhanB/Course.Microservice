using Course.Order.Service.Api.Dtos;
using Course.Shared.Dtos;

namespace Course.Order.Service.Api.Services.Contracts
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> Get(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basket);
        Task<Response<bool>> Delete(string userId);
    }
}
