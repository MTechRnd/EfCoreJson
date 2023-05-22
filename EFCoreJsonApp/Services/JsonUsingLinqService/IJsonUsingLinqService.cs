using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services.JsonUsingLinqService
{
    public interface IJsonUsingLinqService
    {
        Task<IList<OrderWithOrderDetailEntity>> GetAllData();
        Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(Guid id);
        Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(IList<Guid> ids);
        Task<float> AverageOfPrice();
        Task<double> AverageOfQuantity();
        Task<int> SumOfAllQuantity();
        Task<float> SumOfAllPrice();
        Task<int> TotalOrdersOfCustomer(Guid id);
        Task<IList<OrderCount>> TotalOrdersOfCustomers();
        Task<int> GetMaxQuantityByOrderId(Guid id);
        Task<int> GetMinQuantityByOrderId(Guid id);
        Task<float> GetTotalByOrderId(Guid id);
        Task<float> GetMaxPriceByOrderId(Guid id);
        Task<float> GetMinPriceByOrderId(Guid id);
    }
}
