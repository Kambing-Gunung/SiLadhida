using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Services.Interfaces;

public interface IOrderService
{
    Task<List<Pesanan>> GetAllAsync();

    Task<CreateOrderResponseDto> CreateAsync(CreateOrderDto dto);

    Task<Pesanan?> UpdateStatusAsync(int id, UpdateStatusDto dto);
}