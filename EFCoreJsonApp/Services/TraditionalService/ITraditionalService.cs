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
        Task<OrderEntity> GetDataForSingleCustomer(Guid id);
        Task<IList<OrderEntity>> GetDataForMultipleCustomer(List<Guid> id);
        Task<decimal> AggregateOperation();
        Task<int> TotalOrdersOfCustomer(Guid id);
        Task<List<OrderCount>> TotalOrdersOfCustomers();

    }
}
