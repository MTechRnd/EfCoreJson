using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models;
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
        public Task<OrderWithOrderDetails> AggregateOperation()
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderWithOrderDetails>> GetAllData()
        {
            var result = await _context.OrderWithOrderDetails.ToListAsync();
            return result;
        }

        public Task<List<OrderWithOrderDetails>> GetDataForMultipleCustomer(int[] id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderWithOrderDetails> GetDataForSingleCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderWithOrderDetails>> TotalOrdersOfCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
