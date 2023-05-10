using EFCoreJsonApp.Models.Order;
using EFCoreJsonApp.Models.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services
{
    public interface ITraditionalService
    {
        Task<List<OrderEntity>> GetAllData();
        Task<OrderEntity> GetDataForSingleCustomer(int id);
        Task<List<OrderEntity>> GetDataForMultipleCustomer(int[] id);
        Task<OrderDetailEntity> AggregateOperation();
        Task<List<OrderDetailEntity>> TotalOrdersOfCustomer(int id);

    }
}
