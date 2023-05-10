using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJsonApp.Services.JsonService
{
    public class JsonService : IJsonService
    {
        private JsonDataContext _context;

        public JsonService(JsonDataContext context)
        {
            _context = context;
        }
        public Task<OrderWithOrderDetailEntity> AggregateOperation()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetAllData()
        {
            var result = await _context.OrderWithOrderDetails.ToListAsync();
            return result;
        }

        public Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(int[] id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderWithOrderDetailEntity>> TotalOrdersOfCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
