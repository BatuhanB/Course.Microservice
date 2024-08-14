namespace Course.IdentityServer.Models.Dtos
{
    public class CardInformationDto
    {
        public string Id { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string UserId { get; set; }
    }
}
