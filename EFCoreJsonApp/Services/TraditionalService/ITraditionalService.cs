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
        Task<IList<OrderEntity>> GetDataForMultipleCustomer(IList<Guid> id);
        Task<float> AverageOfPrice();
        Task<double> AverageOfQuantity();
        Task<int> SumOfAllQuantity();
        Task<float> SumOfAllPrice();
        Task<int> TotalOrdersOfCustomer(Guid id);
        Task<IList<OrderCount>> TotalOrdersOfCustomers();
    }
}
