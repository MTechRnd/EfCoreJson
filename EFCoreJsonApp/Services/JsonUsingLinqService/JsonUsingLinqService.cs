using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
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

        public async Task<float> AverageOfPrice()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { price = s.OrderDetailsJson.Sum(s => s.Price) })
                .Average(r => r.price);

            return result;
        }

        public async Task<double> AverageOfQuantity()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { quantity = s.OrderDetailsJson.Sum(s => s.Quantity) })
                .Average(r => r.quantity);

            return result;
        }

        public async Task<float> SumOfAllPrice()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { price = s.OrderDetailsJson.Sum(s => s.Price) })
                .Sum(r => r.price);

            return result;
        }

        public async Task<int> SumOfAllQuantity()
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable()
                .Select(s => new { quantity = s.OrderDetailsJson.Sum(s => s.Quantity) })
                .Sum(r => r.quantity);
            return result;
        }


        public async Task<IList<OrderWithOrderDetailEntity>> GetAllData()
        {
            var result = await _context.OrderWithOrderDetails.ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(IList<Guid> customerIds)
        {
            var result = await _context.OrderWithOrderDetails
                .Where(od => customerIds.Contains(od.Id))
                .ToListAsync();
            return result;
        }

        public async Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(Guid id)
        {
            var result = await _context.OrderWithOrderDetails
                .FirstOrDefaultAsync(od => od.Id == id);
            return result;
        }

        public async Task<int> TotalOrdersOfCustomer(Guid id)
        {
            var result =  _context.OrderWithOrderDetails
                .Where(od => od.Id == id)
                .AsEnumerable()
                .Select(od => od.OrderDetailsJson.Count())
                .FirstOrDefault();
            return result;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomers()
        {
            var result1 = _context.OrderWithOrderDetails
                            .AsEnumerable()
                            .Select(s => new OrderCount { Id = s.Id, TotalOrder = s.OrderDetailsJson.Count() }).ToList();
            return result1;
        }

        public async Task<int> GetMaxQuantityByOrderId(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Max(o => o.Quantity))
                .FirstOrDefault();
            return result;
        }

        public async Task<int> GetMinQuantityByOrderId(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Min(o => o.Quantity))
                .FirstOrDefault();
            return result;
        }

        public async Task<float> GetTotalByOrderId(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Sum(o => o.Total))
                .FirstOrDefault();
            return result;
        }

        public async Task<float> GetMaxPriceByOrderId(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Max(o => o.Price))
                .FirstOrDefault();
            return result;
        }

        public async Task<float> GetMinPriceByOrderId(Guid id)
        {
            var result = _context.OrderWithOrderDetails
                .AsEnumerable().Where(od => od.Id == id)
                .Select(o => o.OrderDetailsJson.Min(o => o.Price))
                .FirstOrDefault();
            return result;
        }
    }
}
