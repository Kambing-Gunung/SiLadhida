using SiLadhida.Core.Entities;

namespace SiLadhida.API.Repositories.Interfaces;

public interface IPesananRepository
{
    Task<List<Pesanan>> GetAllAsync();

    Task<Pesanan?> GetByIdAsync(int id);

    Task AddAsync(Pesanan pesanan);

    Task SaveChangesAsync();
}