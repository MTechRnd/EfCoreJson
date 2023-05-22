using BenchmarkDotNet.Order;
using EFCoreJsonApp.Models.AggregateOperations;
using EFCoreJsonApp.Models.CsvDataReadModels;
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
        Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(IList<Guid> ids);
        Task<AverageOfPriceResult> AverageOfPrice();
        Task<AverageOfQuantityResult> AverageOfQuantity();
        Task<TotalQuantityResult> SumOfAllQuantity();
        Task<TotalPriceResult> SumOfAllPrice();
        Task<int> TotalOrdersOfCustomer(Guid id);
        Task<IList<OrderCount>> TotalOrdersOfCustomers();
        Task<MaxQuantityResult> GetMaxQuantityByOrderId(Guid id);
        Task<MinQuantityResult> GetMinQuantityByOrderId(Guid id);
        Task<TotalByOrderResult> GetTotalByOrderId(Guid id);
        Task<MaxPriceResult> GetMaxPriceByOrderId(Guid id);
        Task<MinPriceResult> GetMinPriceByOrderId(Guid id);
    }
}
