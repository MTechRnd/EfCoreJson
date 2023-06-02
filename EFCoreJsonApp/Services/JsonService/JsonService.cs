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
                    SELECT AVG(item.Price) AS AverageOfPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Price decimal(10,2) '$.Price') AS item";
            var result = await _context.Set<AverageOfPriceResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<AverageOfQuantityResult> AverageOfQuantityAsync()
        {
            var query = @"                    
                    SELECT AVG(item.Quantity) AS AverageOfQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Quantity decimal(10,2) '$.Quantity') AS item";
            var result = await _context.Set<AverageOfQuantityResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<TotalPriceResult> SumOfAllPriceAsync()
        {
            var query = @"
                    SELECT SUM(item.Price) AS TotalPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Price decimal(10,2) '$.Price') AS item";
            var result = await _context.Set<TotalPriceResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<TotalQuantityResult> SumOfAllQuantityAsync()
        {
            var query = @"
                    SELECT SUM(item.Quantity) AS TotalQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Quantity int '$.Quantity') AS item";
            var result = await _context.Set<TotalQuantityResult>()
                .FromSqlRaw(query)
                .ToListAsync();
            return result[0];
        }

        public async Task<MaxQuantityResult> GetMaxQuantityByOrderIdAsync(Guid id)
        {
            var query = @$"
                    SELECT MAX(item.Quantity) AS MaximumQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Quantity int '$.Quantity') AS item
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
                    SELECT MIN(item.Quantity) AS MaximumQuantity
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Quantity int '$.Quantity') AS item
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
                    SELECT SUM(item.Total) AS TotalByOrderId
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Total Decimal(10,2) '$.Total') AS item
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
                    SELECT MAX(item.Price) AS MaximumPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Price Decimal(10,2) '$.Price') AS item
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
                    SELECT MIN(item.Price) AS MaximumPrice
                    FROM OrderWithOrderDetails
                    CROSS APPLY OPENJSON(OrderDetailsJson)
                    WITH (Price Decimal(10,2) '$.Price') AS item
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
                    SELECT *
                    FROM OrderWithOrderDetails";
            var result = await _context.OrderWithOrderDetails
                .FromSqlRaw(query)
                .ToListAsync();
            return result;
        }

        public async Task<IList<OrderWithOrderDetailEntity>> GetDataForMultipleCustomerAsync(IList<Guid> customerIds)
        {
            var query = @$"
                    SELECT *
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
                    SELECT *
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
