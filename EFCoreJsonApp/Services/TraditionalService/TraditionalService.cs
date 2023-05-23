using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.Order;
using EFCoreJsonApp.Models.OrderDetails;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreJsonApp.Services.TraditionalService
{
    public class TraditionalService : ITraditionalService
    {
        private DataContext _context;

        public TraditionalService(DataContext context)
        {
            _context = context;
        }
        public async Task<float> AverageOfPrice()
        {
            var res = await _context.Orders
                .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { id = o.Id, price = od.Price })
                .AverageAsync(p => p.price);
            return res;
        }

        public async Task<double> AverageOfQuantity()
        {
            var res = await _context.Orders
                .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { id = o.Id, quantity = od.Quantity })
                .AverageAsync(p => p.quantity);
            return res;
        }

        public async Task<int> SumOfAllQuantity()
        {
            var res = await _context.Orders
                .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { id = o.Id, quantity = od.Quantity })
                .SumAsync(p => p.quantity);
            return res;
        }

        public async Task<float> SumOfAllPrice()
        {
            var res = await _context.Orders
                .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { id = o.Id, price = od.Price })
                .SumAsync(p => p.price);
            return res;
        }

        public async Task<IList<OrderEntity>> GetAllData()
        {
            var res = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
            return res;
        }

        public async Task<IList<OrderEntity>> GetDataForMultipleCustomer(IList<Guid> orderIds)
        {
            var res = await _context.Orders.Where(o => orderIds.Contains(o.Id)).Include(o => o.OrderDetails).ToListAsync();
            return res;
        }

        public async Task<OrderEntity> GetDataForSingleCustomer(Guid id)
        {
            var res = await _context.Orders.Where(o => o.Id == id).Include(od => od.OrderDetails).FirstOrDefaultAsync();
            return res;
        }

        public async Task<int> TotalOrdersOfCustomer(Guid id)
        {
            var res = await _context.Orders
                .Where(o => o.Id == id)
                .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new {id = o.Id})
                .CountAsync();
            return res;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomers()
        {
            var res = await _context.Orders
                    .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { id = od.OrderId })
                    .GroupBy(orderId => orderId.id)
                    .Select(o => new OrderCount
                    {
                        Id = o.Key,
                        TotalOrder = o.Count()
                    }).ToListAsync();
            return res;
        }

        public async Task<int> GetMaxQuantityByOrderId(Guid id)
        {
            var res = await _context.Orders
                        .Where(o => o.Id == id)
                        .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { quantity = od.Quantity})
                        .MaxAsync(q => q.quantity);
            return res;
        }

        public async Task<int> GetMinQuantityByOrderId(Guid id)
        {
            var res = await _context.Orders
                        .Where(o => o.Id == id)
                        .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { quantity = od.Quantity })
                        .MinAsync(q => q.quantity);
            return res;
        }

        public async Task<float> GetTotalByOrderId(Guid id)
        {
            var res = await _context.Orders
                        .Where(od => od.Id == id)
                        .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { total = od.Total })
                        .SumAsync(od => od.total);
            return res;
        }

        public async Task<float> GetMaxPriceByOrderId(Guid id)
        {
            var res = await _context.Orders
                         .Where(o => o.Id == id)
                         .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { price = od.Price })
                         .MaxAsync(q => q.price);
            return res;
        }

        public async Task<float> GetMinPriceByOrderId(Guid id)
        {
            var res = await _context.Orders
                         .Where(o => o.Id == id)
                         .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { price = od.Price })
                         .MinAsync(q => q.price);
            return res;
        }
    }
}
