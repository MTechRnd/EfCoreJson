using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.Order;
using EFCoreJsonApp.Models.OrderDetails;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;

namespace EFCoreJsonApp.Services.TraditionalService
{
    public class TraditionalService : ITraditionalService
    {
        private DataContext _context;

        public TraditionalService(DataContext context)
        {
            _context = context;
        }
        public async Task<float> AverageOfPriceAsync()
        {
            var res = await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { price = od.Price })
                .AverageAsync(p => p.price);
            return res;
        }

        public async Task<double> AverageOfQuantityAsync()
        {
            var res = await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { quantity = od.Quantity })
                .AverageAsync(p => p.quantity);
            return res;
        }

        public async Task<int> SumOfAllQuantityAsync()
        {
            var res = await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { quantity = od.Quantity })
                .SumAsync(p => p.quantity);
            return res;
        }

        public async Task<float> SumOfAllPriceAsync()
        {
            var res = await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { price = od.Price })
                .SumAsync(p => p.price);
            return res;
        }

        public async Task<IList<OrderEntity>> GetAllDataAsync()
        {
            var res = await _context.Orders
                .Include(o => o.OrderDetails)
                .ToListAsync();
            return res;
        }

        public async Task<IList<OrderEntity>> GetDataForMultipleCustomerAsync(IList<Guid> orderIds)
        {
            var res = await _context.Orders
                .Where(o => orderIds.Contains(o.Id))
                .Include(o => o.OrderDetails)
                .ToListAsync();
            return res;
        }

        public async Task<OrderEntity> GetDataForSingleCustomerAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Include(od => od.OrderDetails)
                .FirstOrDefaultAsync();
            return res;
        }

        public async Task<int> TotalOrdersOfCustomerAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Include(od => od.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { id = o.Id})
                .CountAsync();
            return res;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomersAsync()
        {
            var res = await _context.Orders
                    .Include(od => od.OrderDetails)
                    .SelectMany(od => od.OrderDetails, (o, od) => new { id = o.Id })
                    .GroupBy(orderId => orderId.id)
                    .Select(o => new OrderCount
                    {
                        Id = o.Key,
                        TotalOrder = o.Count()
                    })
                    .ToListAsync();
            return res;
        }

        public async Task<int> GetMaxQuantityByOrderIdAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { quantity = od.Quantity })
                .MaxAsync(o => o.quantity);
            return res;
        }

        public async Task<int> GetMinQuantityByOrderIdAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { quantity = od.Quantity })
                .MinAsync(o => o.quantity);
            return res;
        }

        public async Task<float> GetTotalByOrderIdAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(od => od.Id == id)
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { total = od.Total })
                .SumAsync(od => od.total);
            return res;
        }

        public async Task<float> GetMaxPriceByOrderIdAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { price = od.Price })
                .MaxAsync(q => q.price);
            return res;
        }

        public async Task<float> GetMinPriceByOrderIdAsync(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .SelectMany(od => od.OrderDetails, (o, od) => new { price = od.Price })
                .MinAsync(q => q.price);
            return res;
        }
    }
}
