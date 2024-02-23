using AutoMapper;
using Course.Order.Application.Contracts;
using Course.Order.Application.Features.Orders.Models;
using Course.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Course.Order.Application.Features.Orders.Queries.GetOrdersByUserId;
public class GetOrdersByUserIdQueryHandler(IReadRepository<Domain.OrderAggregate.Order> readRepository, IMapper mapper) :
    IRequestHandler<GetOrdersByUserIdQuery, Response<OrderListDto>>
{
    private readonly IReadRepository<Domain.OrderAggregate.Order> _readRepository = readRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<OrderListDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _readRepository.GetListAsync(
            predicate: x => x.BuyerId == request.UserId,
            orderBy: x => x.OrderBy(y => y.Id),
            include: x => x.Include(y => y.OrderItems),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken);

        if (!orders.Items.Any())
        {
            return Response<OrderListDto>.Success(new OrderListDto(), 200);
        }

        var mappedOrders = _mapper.Map<OrderListDto>(orders);
        return Response<OrderListDto>.Success(mappedOrders, 200);
    }
}
