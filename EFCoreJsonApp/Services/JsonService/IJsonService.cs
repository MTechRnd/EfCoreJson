using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services.JsonService
{
    public interface IJsonService
    {
        Task<IList<OrderWithOrderDetailEntity>> GetAllData();
        Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(Guid id);
        Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(List<Guid> ids);
        Task<decimal> AggregateOperation();
        Task<int> TotalOrdersOfCustomer(Guid id);
        Task<IList<OrderCount>> TotalOrdersOfCustomers();
    }
}
