using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJsonApp.Services.JsonUsingLinqService
{
    public class JsonUsingLinqService : IJsonUsingLinqService
    {
        private JsonDataContext _context;

        public JsonUsingLinqService(JsonDataContext context)
        {
            _context = context;
        }

        public async Task<float> AverageOfPriceAsync()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { price = s.OrderDetailsJson.Sum(s => s.Price) })
                .Average(r => r.price);

            return result;
        }

        public async Task<double> AverageOfQuantityAsync()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { quantity = s.OrderDetailsJson.Sum(s => s.Quantity) })
                .Average(r => r.quantity);

            return result;
        }

        public async Task<float> SumOfAllPriceAsync()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { price = s.OrderDetailsJson.Sum(s => s.Price) })
                .Sum(r => r.price);

            return result;
        }

        public async Task<int> SumOfAllQuantityAsync()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { quantity = s.OrderDetailsJson.Sum(s => s.Quantity) })
                .Sum(r => r.quantity);
            return result;
        }


        public async Task<IList<OrderWithOrderDetailEntity>> GetAllDataAsync()
        {
            var result = await _context.OrderWithOrderDetails.ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomerAsync(IList<Guid> customerIds)
        {
            var result = await _context.OrderWithOrderDetails
                .Where(od => customerIds.Contains(od.Id))
                .ToListAsync();
            return result;
        }

        public async Task<OrderWithOrderDetailEntity> GetDataForSingleCustomerAsync(Guid id)
        {
            var result = await _context.OrderWithOrderDetails
                .FirstOrDefaultAsync(od => od.Id == id);
            return result;
        }

        public async Task<int> TotalOrdersOfCustomerAsync(Guid id)
        {
            var result =  _context.OrderWithOrderDetails
                .Where(od => od.Id == id)
                .AsEnumerable()
                .Select(od => od.OrderDetailsJson.Count())
                .FirstOrDefault();
            return result;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomersAsync()
        {
            var result1 = _context.OrderWithOrderDetails
                            .AsEnumerable()
                            .Select(s => new OrderCount { Id = s.Id, TotalOrder = s.OrderDetailsJson.Count() }).ToList();
            return result1;
        }

        public async Task<int> GetMaxQuantityByOrderIdAsync(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Max(o => o.Quantity))
                .FirstOrDefault();
            return result;
        }

        public async Task<int> GetMinQuantityByOrderIdAsync(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Min(o => o.Quantity))
                .FirstOrDefault();
            return result;
        }

        public async Task<float> GetTotalByOrderIdAsync(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Sum(o => o.Total))
                .FirstOrDefault();
            return result;
        }

        public async Task<float> GetMaxPriceByOrderIdAsync(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Max(o => o.Price))
                .FirstOrDefault();
            return result;
        }

        public async Task<float> GetMinPriceByOrderIdAsync(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Min(o => o.Price))
                .FirstOrDefault();
            return result;
        }
    }
}
