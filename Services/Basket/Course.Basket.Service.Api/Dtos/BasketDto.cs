namespace Course.Order.Service.Api.Dtos
{
    public class BasketDto
    {
        public required string UserId { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto>? BasketItems { get; set; }
        public decimal? TotalPriceWithDiscount => BasketItems.Sum(x => x.PriceWithDiscount * x.Quantity);
        public decimal? TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);
    }
}
