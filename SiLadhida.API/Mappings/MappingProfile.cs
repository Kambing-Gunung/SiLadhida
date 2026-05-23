using AutoMapper;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pesanan, CreateOrderResponseDto>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.StatusSekarang.ToString())
            );
    }
}