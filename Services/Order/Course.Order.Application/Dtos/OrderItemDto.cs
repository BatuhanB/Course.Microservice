namespace Course.Order.Application.Dtos;
public sealed record OrderItemDto(string ProductId, string ProductName, string ImageUrl, decimal Price);