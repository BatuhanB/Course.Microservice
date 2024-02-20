namespace Course.Order.Application.Dtos;
public sealed record OrderDto(int Id, DateTime CreatedDate, AddressDto Address, string BuyerId, List<OrderItemDto> OrderItems);