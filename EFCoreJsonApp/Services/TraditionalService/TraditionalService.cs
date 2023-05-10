using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.Order;
using EFCoreJsonApp.Models.OrderDetails;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJsonApp.Services.TraditionalService
{
    public class TraditionalService : ITraditionalService
    {
        private DataContext _context;

        public TraditionalService(DataContext context)
        {
            _context = context;
        }
        public Task<OrderDetailEntity> AggregateOperation()
        {

            throw new NotImplementedException();
        }

        public async Task<IList<OrderEntity>> GetAllData()
        {
            var data = await _context.Orders.ToListAsync();
            return data;
        }

        public Task<IList<OrderEntity>> GetDataForMultipleCustomer(int[] id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetDataForSingleCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderDetailEntity>> TotalOrdersOfCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
