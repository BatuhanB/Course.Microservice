using AutoMapper;
using Course.Order.Application.Contracts;
using Course.Order.Application.Dtos;
using Course.Order.Application.Features.Orders.Constants;
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
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            var errors = new List<string>();
            validationResult.Errors.ForEach(x =>
            {
                errors.Add(x.ErrorMessage);
            });
            return Response<CreatedOrderDto>.Fail(errors, 500);
        }

        if (!validationResult.IsValid)
        {
            return Response<CreatedOrderDto>.Fail(OrderConstants.CreatedOrderNotValid, 500);
        }

        Domain.OrderAggregate.Order mappedOrder = _mapper.Map<Domain.OrderAggregate.Order>(request);

        var result = await _writeRepository.AddAsync(mappedOrder, cancellationToken);
        await _writeRepository.SaveChangesAsync();
        var response = new CreatedOrderDto(result.Id);

        return Response<CreatedOrderDto>.Success(response, 201);
    }
}