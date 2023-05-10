using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<OrderWithOrderDetailEntity>> GetAllData()
        {
            var result = await _context.OrderWithOrderDetails.ToListAsync();
            return result;
        }

        public Task<List<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(int[] id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderWithOrderDetailEntity>> TotalOrdersOfCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
