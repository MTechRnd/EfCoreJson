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
        Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(int id);
        Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(int[] id);
        Task<OrderWithOrderDetailEntity> AggregateOperation();
        Task<IList<OrderWithOrderDetailEntity>> TotalOrdersOfCustomer(int id);
    }
}
