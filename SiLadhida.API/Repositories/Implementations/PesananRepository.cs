using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.API.Repositories.Interfaces;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Repositories.Implementations;

public class PesananRepository : IPesananRepository
{
    private readonly AppDbContext _context;

    public PesananRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pesanan>> GetAllAsync()
    {
        return await _context.Pesanan
            .Include(p => p.Items)
            .ThenInclude(i => i.Produk)
            .ToListAsync();
    }

    public async Task<Pesanan?> GetByIdAsync(int id)
    {
        return await _context.Pesanan
            .Include(p => p.Items)
            .ThenInclude(i => i.Produk)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Pesanan pesanan)
    {
        await _context.Pesanan.AddAsync(pesanan);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}