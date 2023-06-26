using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Models.Records
{
    public record AverageOfPriceResult(decimal AverageOfPrice);
    public record AverageOfQuantityResult(decimal AverageOfQuantity);
    public record MaxPriceResult(decimal MaximumPrice);
    public record MaxQuantityResult(int MaximumQuantity);
    public record MinPriceResult(decimal MinimumPrice);
    public record MinQuantityResult(int MinimumQuantity);
    public record TotalByOrderResult(decimal TotalByOrderId);
    public record TotalOrderByCustomerResult(int TotalOrderByCustomerId);
    public record TotalPriceResult(decimal TotalPrice);
    public record TotalQuantityResult(int TotalQuantity);

}
