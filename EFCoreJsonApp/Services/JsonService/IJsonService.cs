using BenchmarkDotNet.Order;
using EFCoreJsonApp.Models.CsvDataReadModels;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using EFCoreJsonApp.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Services.JsonService
{
    public interface IJsonService
    {
        Task<IList<OrderWithOrderDetailEntity>> GetAllDataAsync();
        Task<OrderWithOrderDetailEntity> GetDataForSingleCustomerAsync(Guid id);
        Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomerAsync(IList<Guid> ids);
        Task<AverageOfPriceResult> AverageOfPriceAsync();
        Task<AverageOfQuantityResult> AverageOfQuantityAsync();
        Task<TotalQuantityResult> SumOfAllQuantityAsync();
        Task<TotalPriceResult> SumOfAllPriceAsync();
        Task<TotalOrderByCustomerResult> TotalOrdersOfCustomerAsync(Guid id);
        Task<IList<OrderCount>> TotalOrdersOfCustomersAsync();
        Task<MaxQuantityResult> GetMaxQuantityByOrderIdAsync(Guid id);
        Task<MinQuantityResult> GetMinQuantityByOrderIdAsync(Guid id);
        Task<TotalByOrderResult> GetTotalByOrderIdAsync(Guid id);
        Task<MaxPriceResult> GetMaxPriceByOrderIdAsync(Guid id);
        Task<MinPriceResult> GetMinPriceByOrderIdAsync(Guid id);
    }
}
