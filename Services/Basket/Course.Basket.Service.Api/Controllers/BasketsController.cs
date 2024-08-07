using Course.Order.Service.Api.Dtos;
using Course.Order.Service.Api.Services.Contracts;
using Course.Shared.BaseController;
using Course.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Order.Service.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketsController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _identityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService identityService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            return CreateActionResultInstance(await _basketService.Get(userId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basket)
        {
            basket.UserId = _identityService.GetUserId;
            var response = await _basketService.SaveOrUpdate(basket);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance(await _basketService.Delete(_identityService.GetUserId));
        }
    }
}
