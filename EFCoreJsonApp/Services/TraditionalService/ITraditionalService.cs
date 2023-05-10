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
        Task<IList<OrderEntity>> GetAllData();
        Task<OrderEntity> GetDataForSingleCustomer(int id);
        Task<IList<OrderEntity>> GetDataForMultipleCustomer(int[] id);
        Task<OrderDetailEntity> AggregateOperation();
        Task<IList<OrderDetailEntity>> TotalOrdersOfCustomer(int id);

    }
}
