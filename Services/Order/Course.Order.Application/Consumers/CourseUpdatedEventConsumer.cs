using MassTransit;
using Course.Shared.Messages;
using Course.Order.Application.Contracts;
using AutoMapper;
using System.Linq.Expressions;

namespace Course.Order.Application.Consumers;
public class CourseUpdatedEventConsumer(
    IWriteRepository<Domain.OrderAggregate.OrderItem> writeRepository,
    IReadRepository<Domain.OrderAggregate.OrderItem> readRepository,
    IMapper mapper)
    : IConsumer<CourseNameUpdatedEvent>
{
    private readonly IWriteRepository<Domain.OrderAggregate.OrderItem> _writeRepository = writeRepository;
    private readonly IReadRepository<Domain.OrderAggregate.OrderItem> _readRepository = readRepository;
    private readonly IMapper _mapper = mapper;
    public async Task Consume(ConsumeContext<CourseNameUpdatedEvent> context)
    {
        Expression<Func<Domain.OrderAggregate.OrderItem, bool>> predicate = x => x.ProductId == context.Message.CourseId; 
        var orderItems = await _readRepository.GetListAsync(
        predicate: predicate,
        orderBy: null,
        include: null,
        index: 0,
        size: 10,
        enableTracking: true,
        cancellationToken: CancellationToken.None);

        foreach (var item in orderItems.Items)
        {
            item.UpdateOrderItem(context.Message.CourseName,item.ImageUrl,item.Price);
        }

        await _writeRepository.SaveChangesAsync();
    }
}
