using AutoMapper;
using Course.Order.Application.Contracts;
using Course.Order.Application.Dtos;
using Course.Shared.Messages;
using MassTransit;

namespace Course.Order.Application.Consumers;
public class CreateOrderMessageCommandConsumer
    (IWriteRepository<Domain.OrderAggregate.Order> writeRepository, 
    IMapper mapper) 
    : IConsumer<CreateOrderMessageCommand>
{
    private readonly IWriteRepository<Domain.OrderAggregate.Order> _writeRepository = writeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var address = new AddressDto(
            context.Message.Address.Province,
            context.Message.Address.District,
            context.Message.Address.Street,
            context.Message.Address.ZipCode,
            context.Message.Address.Line);
        var mappedAddress = _mapper.Map<Domain.OrderAggregate.Address>(address);
        var order = new Domain.OrderAggregate.Order(context.Message.BuyerId,mappedAddress);
        foreach (var item in context.Message.OrderItems)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.ImageUrl);
        }
        
        var result = await _writeRepository.AddAsync(order);
        await _writeRepository.SaveChangesAsync();
    }
}
