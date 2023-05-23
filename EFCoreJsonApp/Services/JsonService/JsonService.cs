using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using EFCoreJsonApp.Models.Records;
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
        public async Task<AverageOfPriceResult> AverageOfPriceAsync()
        {
            var query = @"
                    SELECT AVG(CAST(JSON_VALUE(item.Value, '$.Price') AS decimal(10,2))) AS AverageOfPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<AverageOfPriceResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<AverageOfQuantityResult> AverageOfQuantityAsync()
        {
            var query = @"
                    SELECT AVG(CAST(JSON_VALUE(item.Value, '$.Quantity') AS decimal(10,2))) AS AverageOfQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<AverageOfQuantityResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<TotalPriceResult> SumOfAllPriceAsync()
        {
            var query = @"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Price') AS decimal(10,2))) AS TotalPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<TotalPriceResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<TotalQuantityResult> SumOfAllQuantityAsync()
        {
            var query = @"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS TotalQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item";
            var result = await _context.Set<TotalQuantityResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<MaxQuantityResult> GetMaxQuantityByOrderIdAsync(Guid id)
        {
            var query = @$"
                    SELECT MAX(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS MaximumQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var result = await _context.Set<MaxQuantityResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<MinQuantityResult> GetMinQuantityByOrderIdAsync(Guid id)
        {
            var query = @$"
                    SELECT MIN(CAST(JSON_VALUE(item.Value, '$.Quantity') AS int)) AS MinimumQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var result = await _context.Set<MinQuantityResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<TotalByOrderResult> GetTotalByOrderIdAsync(Guid id)
        {
            var query = @$"
                    SELECT SUM(CAST(JSON_VALUE(item.Value, '$.Total') As Decimal(10,2))) AS TotalByOrderId
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) As item
                    WHERE Id = '{id}'
                ";
            var result = await _context.Set<TotalByOrderResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<MaxPriceResult> GetMaxPriceByOrderIdAsync(Guid id)
        {
            var query = @$"
                    SELECT MAX(CAST(JSON_VALUE(item.Value, '$.Price') AS Decimal(10,2))) AS MaximumPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var result = await _context
                .Set<MaxPriceResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<MinPriceResult> GetMinPriceByOrderIdAsync(Guid id)
        {
            var query = @$"
                    SELECT MIN(CAST(JSON_VALUE(item.Value, '$.Price') AS Decimal(10,2))) AS MinimumPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    WHERE Id = '{id}'
                ";
            var result = await _context.Set<MinPriceResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetAllDataAsync()
        {
            var query = @"
                    SELECT Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) AS OrderDetailsJson 
                    FROM OrderWithOrderDetails";
            var result = await _context.OrderWithOrderDetails
                .FromSqlRaw(query)
                .ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomerAsync(IList<Guid> customerIds)
        {
            var query = @$"
                    SELECT Id,CustomerName,OrderDate, JSON_QUERY(OrderDetailsJson) AS OrderDetailsJson 
                    FROM OrderWithOrderDetails 
                    WHERE Id IN ({string.Join(',', customerIds.Select(id => $"'{id}'"))})";
           var result = await _context.OrderWithOrderDetails
                .FromSqlRaw(query)
                .ToListAsync();
            return result;
        }

        public async Task<OrderWithOrderDetailEntity> GetDataForSingleCustomerAsync(Guid id)
        {
            var query = @$"
                    SELECT Id,CustomerName,OrderDate, OrderDetailsJson 
                    FROM OrderWithOrderDetails where Id = '{id}'";
           var result = await _context.OrderWithOrderDetails.FromSqlRaw(query).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TotalOrderByCustomerResult> TotalOrdersOfCustomerAsync(Guid id)
        {
            var query = @$"
                    SELECT count(*) as TotalOrderByCustomerId
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson) AS item
                    where Id = '{id}'
            ";
            var result = await _context.Set<TotalOrderByCustomerResult>()
                .FromSqlRaw(query)
                .ToListAsync();

            return result[0];
        }

        public async Task<IList<OrderCount>> TotalOrdersOfCustomersAsync()
        {
            var query = @"
                SELECT Id,
                COUNT(*) AS TotalOrder
                FROM OrderWithOrderDetails
                CROSS APPLY OPENJSON(OrderDetailsJson) AS items
                WHERE ISJSON(OrderDetailsJson) > 0
                GROUP BY Id;
            ";
            var result = await _context.Set<OrderCount>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result;
        }


    }
}
