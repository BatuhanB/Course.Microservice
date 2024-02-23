using AutoMapper;
using Course.Order.Application.Contracts.Paging;
using Course.Order.Application.Features.Orders.Commands.CreateOrder;
using Course.Order.Application.Features.Orders.Models;

namespace Course.Order.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.OrderAggregate.Order, Dtos.OrderDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.Order, CreateOrderCommand>().ReverseMap();
        CreateMap<Domain.OrderAggregate.Order, Dtos.CreatedOrderDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.Address, Dtos.AddressDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.OrderItem, Dtos.OrderItemDto>().ReverseMap();
        CreateMap<IPaginate<Domain.OrderAggregate.Order>, OrderListDto>().ReverseMap();
    }
}