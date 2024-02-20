using AutoMapper;

namespace Course.Order.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.OrderAggregate.Order,Dtos.OrderDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.Order,Dtos.CreatedOrderDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.Address,Dtos.AddressDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.OrderItem,Dtos.OrderItemDto>().ReverseMap();
    }
}