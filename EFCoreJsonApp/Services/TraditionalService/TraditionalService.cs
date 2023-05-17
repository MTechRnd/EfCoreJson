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
        public async Task<decimal> AggregateOperation()
        {
            var avgOfPrice = await _context.OrderDetails.AverageAsync(od => od.Price);
            var avgOfQuantity = await _context.OrderDetails.AverageAsync(od => od.Quantity);
            return (decimal)avgOfPrice;
        }

        public async Task<IList<OrderEntity>> GetAllData()
        {
            var res = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
            return res;
        }

        public async Task<IList<OrderEntity>> GetDataForMultipleCustomer(List<Guid> orderIds)
        {
            var res = await _context.Orders.Where(o => orderIds.Contains(o.Id)).Include(o => o.OrderDetails).ToListAsync();
            return res;
        }

        public async Task<OrderEntity> GetDataForSingleCustomer(Guid id)
        {
            var res = _context.Orders.FirstOrDefault(o => o.Id == id);
            return res;
        }

        public async Task<int> TotalOrdersOfCustomer(Guid id)
        {
            var res = await _context.Orders.Where(o => o.Id == id).CountAsync();
            return res;
        }

        public async Task<List<OrderCount>> TotalOrdersOfCustomers()
        {
            var res = await _context.OrderDetails.GroupBy(od => od.OrderId).Select(o => new OrderCount
            {
                Id = o.Key,
                totalOrder = o.Count()
            }).ToListAsync();
            return res;
        }
    }
}
