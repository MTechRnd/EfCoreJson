using EFCoreJsonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services.JsonService
{
    public interface IJsonService
    {
        Task<List<OrderWithOrderDetails>> GetAllData();
        Task<OrderWithOrderDetails> GetDataForSingleCustomer(int id);
        Task<List<OrderWithOrderDetails>> GetDataForMultipleCustomer(int[] id);
        Task<OrderWithOrderDetails> AggregateOperation();
        Task<List<OrderWithOrderDetails>> TotalOrdersOfCustomer(int id);
    }
}
