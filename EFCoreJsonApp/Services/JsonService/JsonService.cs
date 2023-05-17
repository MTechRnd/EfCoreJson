using Azure.Core;
using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EFCoreJsonApp.Services.JsonService
{
    public class JsonService : IJsonService
    {
        private JsonDataContext _context;

        public JsonService(JsonDataContext context)
        {
            _context = context;
        }
        public decimal AggregateOperation()
        {
            var result = _context.OrderWithOrderDetails.AsEnumerable().Select(s => new { price = s.OrderDetailsJson.Sum(s => s.Price) });
            var totalSum = result.Sum(r => r.price);

            return (decimal)totalSum;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetAllData()
        {
            var result = await _context.OrderWithOrderDetails.ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(List<Guid> customerIds)
        {
            var result = await _context.OrderWithOrderDetails.Where(od => customerIds.Contains(od.Id)).ToListAsync();
            return result;
        }

        public async Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(Guid id)
        {
            var result = await _context.OrderWithOrderDetails.FirstOrDefaultAsync(od => od.Id == id);
            return result;
        }

        public int TotalOrdersOfCustomer(Guid id)
        {
            var result = _context.OrderWithOrderDetails.FirstOrDefault(od => od.Id == id);
            if (result != null)
                return result.OrderDetailsJson.Count();
            return -1;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomers()
        {
            var result1 = _context.OrderWithOrderDetails
                            .AsEnumerable()
                            .Select(s => new OrderCount { Id = s.Id, totalOrder = s.OrderDetailsJson.Count() }).ToList();
            return result1;
        }
    }
}
