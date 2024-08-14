using Course.IdentityServer.Models;
using Course.IdentityServer.Models.Dtos;
using Course.IdentityServer.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.IdentityServer.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Dtos.Response<AddressDto> response;
            var address = await _addressService.GetByIdAsync(id);
            if (address.StatusCode == 200)
            {
                response = Dtos.Response<AddressDto>
                    .Success(
                    MapAdressToDto(address.Data),
                    address.StatusCode);
            }
            else
            {
                response = Dtos.Response<AddressDto>
                    .Fail(
                    "No Address Has Found",
                    404);
            }

            return CreateActionResultInstance(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(string userId)
        {
            Dtos.Response<List<AddressDto>> response;
            var addresses = await _addressService.GetByUserIdAsync(userId);
            if (addresses.StatusCode == 200)
            {
                response = Dtos.Response<List<AddressDto>>
                    .Success(
                    MapAdressToDtoList(addresses.Data),
                    addresses.StatusCode);
            }
            else
            {
                response = Dtos.Response<List<AddressDto>>
                    .Fail(
                    "No Address Has Found",
                    404);
            }
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto address)
        {
            var result = await _addressService.UpdateAsync(MapDtoToAdress(address));
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddressDto address)
        {
            var result = await _addressService.CreateAsync(MapDtoToAdress(address));
            return CreateActionResultInstance(result);
        }

        private static Address MapDtoToAdress(AddressDto address)
        {
            return new Address
            {
                Id = address.Id,
                District = address.District,
                Line = address.Line,
                Province = address.Province,
                Street = address.Street,
                UserId = address.UserId,
                ZipCode = address.ZipCode
            };
        }
        private static List<AddressDto> MapAdressToDtoList(List<Address> addresses)
        {
            var result = new List<AddressDto>();
            foreach (var address in addresses)
            {
                result.Add(new AddressDto
                {
                    Id = address.Id,
                    District = address.District,
                    Line = address.Line,
                    Province = address.Province,
                    Street = address.Street,
                    UserId = address.UserId,
                    ZipCode = address.ZipCode
                });
            }
            return result;
        }
        private static AddressDto MapAdressToDto(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
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
