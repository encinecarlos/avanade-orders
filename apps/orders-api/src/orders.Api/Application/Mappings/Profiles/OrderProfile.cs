using AutoMapper;
using orders.Api.Application.Dto;
using orders.Api.Domain.Entites;

namespace orders.Api.Application.Mappings.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingDate, opt => opt.Ignore())
                .ForMember(dest => dest.OrderStatus, opt => opt.Ignore());
        }
    }
}