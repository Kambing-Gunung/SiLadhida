using AutoMapper;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product
        CreateMap<Product, ProductResponseDto>();

        // Order Item
        CreateMap<OrderItem, OrderItemResponseDto>();

        // Order
        CreateMap<Order, OrderResponseDto>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.StatusSekarang.ToString())
            )
            .ForMember(
                dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)
            );
    }
}