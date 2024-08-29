using AutoMapper;
using Course.Order.Application.Contracts;
using Course.Order.Application.Dtos;
using Course.Order.Application.Features.Orders.Commands.Adapters;
using Course.Order.Application.Features.Orders.Models;
using Course.Shared.Messages;
using MassTransit;
using MediatR;
using System.Text.Json;

namespace Course.Order.Application.Features.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler(
    IWriteRepository<Domain.OrderAggregate.Order> writeRepository,
    IMapper mapper,
    IPublishEndpoint publishEndpoint,
    ISendEndpointProvider sendEndpoint,
    IHttpClientFactory clientFactory)
    : IRequestHandler<CreateOrderCommand, Shared.Dtos.Response<CreatedOrderDto>>
{
    private readonly IWriteRepository<Domain.OrderAggregate.Order> _writeRepository = writeRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpoint = sendEndpoint;
    private readonly HttpClient _httpClient = clientFactory.CreateClient("CreateOrderCommand");


    public async Task<Shared.Dtos.Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var mappedOrder = _mapper.Map<Domain.OrderAggregate.Order>(request);

        var result = await _writeRepository.AddAsync(mappedOrder, cancellationToken);
        await _writeRepository.SaveChangesAsync(cancellationToken);
        var response = new CreatedOrderDto(result.Id);

        if (response.OrderId > 0)
        {
            await PublishOrderCreatedNotificationEventAsync(request, mappedOrder, cancellationToken);
            await SendCreateInvoiceCommandAsync(mappedOrder, cancellationToken);
        }

        return Shared.Dtos.Response<CreatedOrderDto>.Success(response, 201);
    }

    private async Task PublishOrderCreatedNotificationEventAsync(CreateOrderCommand request, Domain.OrderAggregate.Order mappedOrder, CancellationToken cancellationToken)
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

    private async Task SendCreateInvoiceCommandAsync(Domain.OrderAggregate.Order mappedOrder, CancellationToken cancellationToken)
    {
        var requestUrl = $"api/shared/getbyid/{mappedOrder.BuyerId}";
        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"API Error: {response.StatusCode}, Content: {errorContent}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var userResponse = System.Text.Json.JsonSerializer.Deserialize<GetUserByIdResponse>(responseContent, options);

        var sendEndpoint = await _sendEndpoint.GetSendEndpoint(new Uri("queue:create-invoiceq"));

        if (response == null)
        {
            throw new ArgumentNullException($"{mappedOrder.BuyerId} could not found!");
        }

        List<OrderItemForInvoice> orderItemsForInvoice =
                    mappedOrder
                    .OrderItems
                    .Select(item => new OrderItemForInvoice().Map(item))
                    .ToList();

        CreateInvoiceCommand command = new()
        {
            Customer = new CustomerForInvoice()
            {
                UserName = userResponse!.UserName,
                Address = new AddressForInvoice()
                {
                    District = mappedOrder.Address.District,
                    Province = mappedOrder.Address.Province,
                    Street = mappedOrder.Address.Street,
                    ZipCode = mappedOrder.Address.ZipCode,
                    Line = mappedOrder.Address.Line
                }
            },
            OrderInformation = new OrderInformationForInvoice()
            {
                BuyerId = mappedOrder.BuyerId,
                OrderDate = mappedOrder.CreatedDate,
                OrderItems = orderItemsForInvoice
            }
        };

        await sendEndpoint
            .Send(
            command
            , cancellationToken);
    }
}