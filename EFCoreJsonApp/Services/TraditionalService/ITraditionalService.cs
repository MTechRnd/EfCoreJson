using EFCoreJsonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services
{
    public interface ITraditionalService
    {
        Task<List<Order>> GetAllData();
        Task<Order> GetDataForSingleCustomer(int id);
        Task<List<Order>> GetDataForMultipleCustomer(int[] id);
        Task<OrderDetails> AggregateOperation();
        Task<List<OrderDetails>> TotalOrdersOfCustomer(int id);

    }
}
