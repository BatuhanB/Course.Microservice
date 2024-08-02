using AutoMapper;
using Course.Order.Application.Contracts;
using Course.Order.Application.Dtos;
using Course.Shared.Dtos;
using MediatR;

namespace Course.Order.Application.Features.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler(IWriteRepository<Domain.OrderAggregate.Order> writeRepository, IMapper mapper)
    : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly IWriteRepository<Domain.OrderAggregate.Order> _writeRepository = writeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var mappedOrder = _mapper.Map<Domain.OrderAggregate.Order>(request);

        var result = await _writeRepository.AddAsync(mappedOrder, cancellationToken);
        await _writeRepository.SaveChangesAsync(cancellationToken);
        var response = new CreatedOrderDto(result.Id);

        return Response<CreatedOrderDto>.Success(response, 201);
    }
}