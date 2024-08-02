using AutoMapper;
using Course.FakePayment.Service.Api.Models;
using Course.Shared.Messages;

namespace Course.FakePayment.Service.Api.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<PaymentDto, CreateOrderMessageCommand>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Order.Address))
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Order.BuyerId))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Order.OrderItems))
            .ReverseMap();

        CreateMap<Shared.Messages.Address, Models.Address>().ReverseMap();
        CreateMap<Shared.Messages.OrderItem, Models.OrderItem>().ReverseMap();
    }
}
