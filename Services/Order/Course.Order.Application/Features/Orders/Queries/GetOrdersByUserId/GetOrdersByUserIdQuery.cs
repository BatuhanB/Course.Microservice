using Course.Order.Application.Contracts.Request;
using Course.Order.Application.Features.Orders.Models;
using Course.Shared.Dtos;
using MediatR;

namespace Course.Order.Application.Features.Orders.Queries.GetOrdersByUserId;
public sealed record GetOrdersByUserIdQuery(string UserId,PageRequest PageRequest) : IRequest<Response<OrderListDto>>;