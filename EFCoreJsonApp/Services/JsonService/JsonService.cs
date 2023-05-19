using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.AggregateOperations;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJsonApp.Services.JsonService
{
    public class JsonService : IJsonService
    {
        private JsonDataContext _context;

        public JsonService(JsonDataContext context)
        {
            _context = context;
        }
        public async Task<decimal> AverageOfPrice()
        {
            var query = @"
                    SELECT AVG(CAST(JSON_VALUE(item.Value, '$.Price') AS decimal(10,2))) AS AverageOfPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<AverageOfPriceResult>().FromSqlRaw(query).Select(x => x.AverageOfPrice).FirstOrDefaultAsync();
            return result;
        }

        public async Task<decimal> AverageOfQuantity()
        {
            var query = @"
                    SELECT AVG(CAST(JSON_VALUE(item.Value, '$.Quantity') AS decimal(10,2))) AS AverageOfQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<AverageOfQuantityResult>().FromSqlRaw(query).Select(x => x.AverageOfQuantity).FirstOrDefaultAsync();
            return result;
        }

        public async Task<decimal> SumOfAllPrice()
        {
            //var qury = _context.OrderWithOrderDetails.FromSqlRaw("select Id, CustomerName, OrderDetailsJson, OrderDate from OrderWithOrderDetails");
            var query = @"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Price') AS decimal(10,2))) AS TotalPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<TotalPriceResult>().FromSqlRaw(query).Select(x => x.TotalPrice).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SumOfAllQuantity()
        {
            var query = @"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS TotalQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<TotalQuantityResult>().FromSqlRaw(query).Select(x => x.TotalQuantity).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetAllData()
        {
            var query = @"
                    select Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) as OrderDetailsJson 
                    from OrderWithOrderDetails";
            var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(IList<Guid> customerIds)
        {
            var query = @$"
                    SELECT Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) as OrderDetailsJson 
                    FROM OrderWithOrderDetails 
                    WHERE Id IN ({string.Join(',', customerIds.Select(id => $"'{id}'"))})";
           var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).ToListAsync();
            return result;
        }

        public async Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(Guid id)
        {
            var query = @$"
                    select Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) as OrderDetailsJson 
                    from OrderWithOrderDetails where Id = '{id}'";
           var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).FirstAsync();
            return result;
        }

        public async Task<int> TotalOrdersOfCustomer(Guid id)
        {
            var query = @$"select * from OrderWithOrderDetails where Id = '{id}'";
            var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).FirstAsync();
            
            if (result != null)
                return result.OrderDetailsJson.Count();
            return -1;
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomers()
        {
            var query = @"
                SELECT Id,
                COUNT(*) AS TotalOrder
                FROM OrderWithOrderDetails
                CROSS APPLY OPENJSON(OrderDetailsJson) AS items
                WHERE ISJSON(OrderDetailsJson) > 0
                GROUP BY Id;
            ";
            var result = await _context.Set<OrderCount>().FromSqlRaw(query).ToListAsync();
            return result;
        }
    }
}
