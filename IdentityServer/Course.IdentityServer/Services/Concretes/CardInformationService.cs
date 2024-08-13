using Course.IdentityServer.Data;
using Course.IdentityServer.Models;
using Course.IdentityServer.Services.Abstracts;

namespace Course.IdentityServer.Services.Concretes
{
    public class CardInformationService : GenericService<CardInformation>, ICardInformationService
    {
        public CardInformationService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
