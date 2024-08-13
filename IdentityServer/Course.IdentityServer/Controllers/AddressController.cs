using Course.IdentityServer.Models;
using Course.IdentityServer.Models.Dtos;
using Course.IdentityServer.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Course.IdentityServer.Controllers
{
    public class AddressController :  BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _addressService.GetByIdAsync(id);
            return CreateActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserAsync(string id)
        {
            var result = await _addressService.GetByUserIdAsync(id);
            return CreateActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto address)
        {
            var result = await _addressService.UpdateAsync(MapToAdress(address));
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddressDto address)
        {
            var result = await _addressService.CreateAsync(MapToAdress(address));
            return CreateActionResultInstance(result);
        }

        private static Address MapToAdress(AddressDto address)
        {
            return new Address
            {
                District = address.District,
                Line = address.Line,
                Province = address.Province,
                Street = address.Street,
                UserId = address.UserId,    
                ZipCode = address.ZipCode
            };
        }
    }
}
