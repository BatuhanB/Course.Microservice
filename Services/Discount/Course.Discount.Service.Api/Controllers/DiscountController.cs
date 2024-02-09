using Course.Discount.Service.Api.Models;
using Course.Discount.Service.Api.Service.Contracts;
using Course.Shared.BaseController;
using Course.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Discount.Service.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController(ISharedIdentityService identityService,
        IDiscountService discountService)
        : BaseController
    {
        private readonly ISharedIdentityService _identityService = identityService;
        private readonly IDiscountService _discountService = discountService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResultInstance(await _discountService.GetByIdAsync(id));
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            return CreateActionResultInstance(await _discountService.GetByCodeAndUserIdAsync(code,_identityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.CreateAsync(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.UpdateAsync(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.DeleteAsync(id));
        }
    }
}
