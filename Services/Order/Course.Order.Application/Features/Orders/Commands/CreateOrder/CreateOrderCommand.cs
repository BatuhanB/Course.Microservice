using Course.Order.Application.Dtos;
using Course.Shared.Dtos;
using MediatR;

namespace Course.Order.Application.Features.Orders.Commands.CreateOrder;
public sealed record CreateOrderCommand(DateTime CreatedDate, AddressDto Address, string BuyerId, List<OrderItemDto> OrderItems) : IRequest<Response<CreatedOrderDto>>;