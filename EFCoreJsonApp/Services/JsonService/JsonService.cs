using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.AggregateOperations;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCoreJsonApp.Services.JsonService
{
    public class JsonService : IJsonService
    {
        private JsonDataContext _context;

        public JsonService(JsonDataContext context)
        {
            _context = context;
        }
        public async Task<AverageOfPriceResult> AverageOfPrice()
        {
            var query = @"
                    SELECT AVG(CAST(JSON_VALUE(item.Value, '$.Price') AS decimal(10,2))) AS AverageOfPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var averageOfPrice = await _context.Set<AverageOfPriceResult>()
                .FromSqlRaw(query)
                .Select(x => x.AverageOfPrice)
                .FirstOrDefaultAsync();
            var result = new AverageOfPriceResult { AverageOfPrice = averageOfPrice };
            return result;
        }

        public async Task<AverageOfQuantityResult> AverageOfQuantity()
        {
            var query = @"
                    SELECT AVG(CAST(JSON_VALUE(item.Value, '$.Quantity') AS decimal(10,2))) AS AverageOfQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var averageQuantity = await _context.Set<AverageOfQuantityResult>()
                .FromSqlRaw(query)
                .Select(x => x.AverageOfQuantity)
                .FirstOrDefaultAsync();
            var result = new AverageOfQuantityResult { AverageOfQuantity = averageQuantity };
            return result;
        }

        public async Task<TotalPriceResult> SumOfAllPrice()
        {
            var query = @"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Price') AS decimal(10,2))) AS TotalPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var totalPrice = await _context.Set<TotalPriceResult>()
                .FromSqlRaw(query)
                .Select(x => x.TotalPrice)
                .FirstOrDefaultAsync();
            var result = new TotalPriceResult { TotalPrice = totalPrice };
            return result;
        }

        public async Task<TotalQuantityResult> SumOfAllQuantity()
        {
            var query = @"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS TotalQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var totalQuantity = await _context.Set<TotalQuantityResult>()
                .FromSqlRaw(query)
                .Select(x => x.TotalQuantity)
                .FirstOrDefaultAsync();
            var result = new TotalQuantityResult { TotalQuantity = totalQuantity };
            return result;
        }

        public async Task<MaxQuantityResult> GetMaxQuantityByOrderId(Guid id)
        {
            var query = @$"
                    SELECT MAX(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS MaximumQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var maxQuantity = await _context.Set<MaxQuantityResult>()
                .FromSqlRaw(query)
                .Select(x => x.MaximumQuantity)
                .FirstOrDefaultAsync();
            var result = new MaxQuantityResult { MaximumQuantity= maxQuantity };
            return result;
        }

        public async Task<MinQuantityResult> GetMinQuantityByOrderId(Guid id)
        {
            var query = @$"
                    SELECT MIN(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS MinimumQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var minQuantity = await _context.Set<MinQuantityResult>()
                .FromSqlRaw(query)
                .Select(x => x.MinimumQuantity)
                .FirstOrDefaultAsync();
            var result = new MinQuantityResult { MinimumQuantity = minQuantity };
            return result;
        }

        public async Task<TotalByOrderResult> GetTotalByOrderId(Guid id)
        {
            var query = @$"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Total') As Decimal(10,2))) AS TotalByOrderId
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) As item
                    WHERE Id = '{id}'
                ";
            var totalByOrderId = await _context.Set<TotalByOrderResult>()
                .FromSqlRaw(query)
                .Select(x => x.TotalByOrderId)
                .FirstOrDefaultAsync();
            var result = new TotalByOrderResult { TotalByOrderId = totalByOrderId };
            return result;
        }

        public async Task<MaxPriceResult> GetMaxPriceByOrderId(Guid id)
        {
            var query = @$"
                    SELECT MAX(CAST(JSON_VALUE(item.Value, '$.Price') AS Decimal(10,2))) AS MaximumPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var maxPrice = await _context
                .Set<MaxPriceResult>()
                .FromSqlRaw(query)
                .Select(x => x.MaximumPrice)
                .FirstOrDefaultAsync();
            var result = new MaxPriceResult { MaximumPrice = maxPrice };
            return result;
        }

        public async Task<MinPriceResult> GetMinPriceByOrderId(Guid id)
        {
            var query = @$"
                    SELECT MIN(CAST(JSON_VALUE(item.Value, '$.Price') AS Decimal(10,2))) AS MinimumPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var minPrice = await _context.Set<MinPriceResult>()
                .FromSqlRaw(query)
                .Select(x => x.MinimumPrice)
                .FirstOrDefaultAsync();
            var result = new MinPriceResult { MinimumPrice = minPrice };
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetAllData()
        {
            var query = @"
                    SELECT Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) AS OrderDetailsJson 
                    FROM OrderWithOrderDetails";
            var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomer(IList<Guid> customerIds)
        {
            var query = @$"
                    SELECT Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) AS OrderDetailsJson 
                    FROM OrderWithOrderDetails 
                    WHERE Id IN ({string.Join(',', customerIds.Select(id => $"'{id}'"))})";
           var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).ToListAsync();
            return result;
        }

        public async Task<OrderWithOrderDetailEntity> GetDataForSingleCustomer(Guid id)
        {
            var query = @$"
                    SELECT Id,CustomerName,OrderDate, OrderDetailsJson 
                    FROM OrderWithOrderDetails where Id = '{id}'";
           var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TotalOrderByCustomerResult> TotalOrdersOfCustomer(Guid id)
        {
            var query = @$"
                    SELECT count(*) as TotalOrderByCustomerId
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    where Id = '{id}'
            ";
            var totalOrderByCustomerId = await _context.Set<TotalOrderByCustomerResult>()
                .FromSqlRaw(query)
                .Select(x => x.TotalOrderByCustomerId)
                .FirstOrDefaultAsync();

            var result = new TotalOrderByCustomerResult { TotalOrderByCustomerId = totalOrderByCustomerId };
            return result;
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
