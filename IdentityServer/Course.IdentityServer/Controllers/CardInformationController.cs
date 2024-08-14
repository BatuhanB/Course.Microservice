using Course.IdentityServer.Models;
using Course.IdentityServer.Models.Dtos;
using Course.IdentityServer.Services.Abstracts;
using Course.IdentityServer.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Dtos.Response<CardInformationDto> response;
            var cardInformation = await _cardInformationService.GetByIdAsync(id);
            if (cardInformation.StatusCode == 200)
            {
                response = Dtos.Response<CardInformationDto>
                    .Success(
                    MapCardInformationToDto(cardInformation.Data),
                    cardInformation.StatusCode);
            }
            else
            {
                response = Dtos.Response<CardInformationDto>
                    .Fail(
                    "No Card Information Has Found",
                    404);
            }

            return CreateActionResultInstance(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(string userId)
        {
            Dtos.Response<List<CardInformationDto>> response;
            var cardInformations = await _cardInformationService.GetByUserIdAsync(userId);
            if (cardInformations.StatusCode == 200)
            {
                response = Dtos.Response<List<CardInformationDto>>
                    .Success(
                    MapCardInformationToListDto(cardInformations.Data),
                    cardInformations.StatusCode);
            }
            else
            {
                response = Dtos.Response<List<CardInformationDto>>
                    .Fail(
                    "No Card Informations Has Found",
                    404);
            }
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CardInformationDto cardInformation)
        {
            var result = await _cardInformationService.UpdateAsync(MapDtoToCardInformation(cardInformation));
            return CreateActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CardInformationDto cardInformation)
        {
            var result = await _cardInformationService.CreateAsync(MapDtoToCardInformation(cardInformation));
            return CreateActionResultInstance(result);
        }

        private static CardInformation MapDtoToCardInformation(CardInformationDto cardInformation)
        {
            return new CardInformation
            {
                Id = cardInformation.Id,
                CardName = cardInformation.CardName,
                CardNumber = cardInformation.CardNumber,
                Expiration = cardInformation.Expiration,
                UserId = cardInformation.UserId
            };
        }

        private static CardInformationDto MapCardInformationToDto(CardInformation cardInformation)
        {
            return new CardInformationDto
            {
                Id = cardInformation.Id,
                CardName = cardInformation.CardName,
                CardNumber = cardInformation.CardNumber,
                Expiration = cardInformation.Expiration,
                UserId = cardInformation.UserId
            };
        }

        private static List<CardInformationDto> MapCardInformationToListDto(List<CardInformation> cardInformations)
        {
            var result = new List<CardInformationDto>();
            foreach (var cardInformation in cardInformations)
            {
                result.Add(new CardInformationDto
                {
                    Id = cardInformation.Id,
                    CardName = cardInformation.CardName,
                    CardNumber = cardInformation.CardNumber,
                    Expiration = cardInformation.Expiration,
                    UserId = cardInformation.UserId
                });
            }
            return result;
        }
    }
}
