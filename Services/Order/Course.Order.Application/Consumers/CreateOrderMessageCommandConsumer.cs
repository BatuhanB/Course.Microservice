using AutoMapper;
using Course.Order.Application.Dtos;
using Course.Order.Application.Features.Orders.Commands.CreateOrder;
using Course.Shared.Messages;
using MassTransit;
using MediatR;

namespace Course.Order.Application.Consumers;
public class CreateOrderMessageCommandConsumer
    (IMapper mapper,
    IMediator mediator)
    : IConsumer<CreateOrderMessageCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        try
        {
            var address = new AddressDto(
            context.Message.Address.Province,
            context.Message.Address.District,
            context.Message.Address.Street,
            context.Message.Address.ZipCode,
            context.Message.Address.Line);
            var mappedAddress = _mapper.Map<Domain.OrderAggregate.Address>(address);

            var order = new Domain.OrderAggregate.Order(context.Message.BuyerId, mappedAddress);

            foreach (var item in context.Message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.ImageUrl, item.ProductOwnerId);
            }
            var createCommand = new CreateOrderCommand(address, context.Message.BuyerId, _mapper.Map<List<OrderItemDto>>(context.Message.OrderItems));
            await _mediator.Send(createCommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
}