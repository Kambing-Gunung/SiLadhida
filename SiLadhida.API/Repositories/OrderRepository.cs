using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Interfaces;

namespace SiLadhida.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(
            AppDbContext context,
            ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Order
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Order
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            _logger.LogInformation("Adding new order to database");

            await _context.Order.AddAsync(order);
        }

        public async Task SaveChangesAsync()
        {
            _logger.LogInformation("Saving changes to database");

            await _context.SaveChangesAsync();
        }
    }
}