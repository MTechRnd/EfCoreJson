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
        Task<List<OrderWithOrderDetailEntity>> GetAllData();
        Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(int id);
        Task<List<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(int[] id);
        Task<OrderWithOrderDetailEntity> AggregateOperation();
        Task<List<OrderWithOrderDetailEntity>> TotalOrdersOfCustomer(int id);
    }
}
