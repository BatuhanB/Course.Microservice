using AutoMapper;
using Course.Order.Application.Contracts;
using Course.Order.Application.Dtos;
using Course.Shared.Messages;
using MassTransit;
using MediatR;

namespace Course.Order.Application.Features.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler(
    IWriteRepository<Domain.OrderAggregate.Order> writeRepository, 
    IMapper mapper,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateOrderCommand, Shared.Dtos.Response<CreatedOrderDto>>
{
    private readonly IWriteRepository<Domain.OrderAggregate.Order> _writeRepository = writeRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;


    public async Task<Shared.Dtos.Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var mappedOrder = _mapper.Map<Domain.OrderAggregate.Order>(request);

        var result = await _writeRepository.AddAsync(mappedOrder, cancellationToken);
        await _writeRepository.SaveChangesAsync(cancellationToken);
        var response = new CreatedOrderDto(result.Id);

        if (response.OrderId > 0)
        {
            foreach (var orderItem in request.OrderItems)
            {
                await _publishEndpoint
                .Publish<OrderCreatedNotificationEvent>(new OrderCreatedNotificationEvent()
                {
                    CourseBuyDate = mappedOrder.CreatedDate,
                    CourseName = orderItem.ProductName,
                    CourseOwnerId = orderItem.ProductOwnerId,
                }, cancellationToken);
            }
        }

        return Shared.Dtos.Response<CreatedOrderDto>.Success(response, 201);
    }
}