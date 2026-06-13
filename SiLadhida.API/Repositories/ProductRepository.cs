using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Interfaces;

namespace SiLadhida.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(
            AppDbContext context,
            ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Product
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetByIdsAsync(
            IEnumerable<int> ids)
        {
            return await _context.Product
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);
        }

        public Task DeleteAsync(Product product)
        {
            _context.Product.Remove(product);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}