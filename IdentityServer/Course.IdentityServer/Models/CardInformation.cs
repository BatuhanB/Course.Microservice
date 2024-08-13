using System;

namespace Course.IdentityServer.Models
{
    public class CardInformation : BaseEntity
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public decimal TotalPrice { get; set; }
        public ApplicationUser User { get; set; }
    }
}
