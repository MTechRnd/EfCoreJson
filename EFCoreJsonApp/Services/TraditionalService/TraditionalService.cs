using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services.TraditionalService
{
    public class TraditionalService : ITraditionalService
    {
        private DataContext _context;

        public TraditionalService(DataContext context)
        {
            _context = context;
        }
        public Task<OrderDetails> AggregateOperation()
        {

            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllData()
        {
            var data = await _context.Orders.ToListAsync();
            return data;
        }

        public Task<List<Order>> GetDataForMultipleCustomer(int[] id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetDataForSingleCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDetails>> TotalOrdersOfCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
