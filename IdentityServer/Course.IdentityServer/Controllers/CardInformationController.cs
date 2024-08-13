using Course.IdentityServer.Models;
using Course.IdentityServer.Models.Dtos;
using Course.IdentityServer.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Course.IdentityServer.Controllers
{

    public class CardInformationController : BaseController
    {
        private readonly ICardInformationService _cardInformationService;

        public CardInformationController(ICardInformationService cardInformationService)
        {
            _cardInformationService = cardInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _cardInformationService.GetByIdAsync(id);
            return CreateActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserAsync(string id)
        {
            var result = await _cardInformationService.GetByUserIdAsync(id);
            return CreateActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CardInformationDto cardInformation)
        {
            var result = await _cardInformationService.UpdateAsync(MapToCardInformation(cardInformation));
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CardInformationDto cardInformation)
        {
            var result = await _cardInformationService.CreateAsync(MapToCardInformation(cardInformation));
            return CreateActionResultInstance(result);
        }

        private static CardInformation MapToCardInformation(CardInformationDto cardInformation)
        {
            return new CardInformation
            {
                CardName = cardInformation.CardName,
                CardNumber = cardInformation.CardNumber,
                Expiration = cardInformation.Expiration,
                TotalPrice = cardInformation.TotalPrice,
                UserId = cardInformation.UserId
            };
        }
    }
}
