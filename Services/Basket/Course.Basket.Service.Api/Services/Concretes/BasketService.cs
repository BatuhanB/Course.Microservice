using Course.Order.Service.Api.Dtos;
using Course.Order.Service.Api.Services.Contracts;
using Course.Shared.Dtos;
using System.Text.Json;

namespace Course.Order.Service.Api.Services.Concretes;
public class BasketService(RedisService redisService) : IBasketService
{
    private readonly RedisService _redisService = redisService;

    public async Task<Response<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
    }

    public async Task<Response<BasketDto>> Get(string userId)
    {
        var basket = await _redisService.GetDb().StringGetAsync(userId);
        if(String.IsNullOrEmpty(basket))
        {
            return Response<BasketDto>.Fail("Basket not Found!", 404);
        }
        return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket), 200);
    }

    public async Task<Response<bool>> SaveOrUpdate(BasketDto basket)
    {
        var status = await _redisService.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
    }
}