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
            var avgOfPrice = await _context.OrderDetails.AverageAsync(od => od.Price);
            return avgOfPrice;
        }

        public async Task<double> AverageOfQuantity()
        {
            var avgOfQuantity = await _context.OrderDetails.AverageAsync(od => od.Quantity);
            return avgOfQuantity;
        }

        public async Task<int> SumOfAllQuantity()
        {
            var sumOfQuantity = await _context.OrderDetails.SumAsync(od => od.Quantity);
            return sumOfQuantity;
        }

        public async Task<float> SumOfAllPrice()
        {
            var sumOfQuantity = await _context.OrderDetails.SumAsync(od => od.Price);
            return sumOfQuantity;
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
            var res = _context.Orders.FirstOrDefault(o => o.Id == id);
            return res;
        }

        public async Task<int> TotalOrdersOfCustomer(Guid id)
        {
            var res = await _context.OrderDetails.Where(o => o.OrderId == id).CountAsync();
            return res;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomers()
        {
            var res = await _context.OrderDetails.GroupBy(od => od.OrderId).Select(o => new OrderCount
            {
                Id = o.Key,
                TotalOrder = o.Count()
            }).ToListAsync();
            return res;
        }
    }
}
