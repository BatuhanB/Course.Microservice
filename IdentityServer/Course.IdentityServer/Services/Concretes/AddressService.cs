using Course.IdentityServer.Data;
using Course.IdentityServer.Models;
using Course.IdentityServer.Services.Abstracts;

namespace Course.IdentityServer.Services.Concretes
{
    public class AddressService : GenericService<Address>,IAddressService
    {
        public AddressService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
