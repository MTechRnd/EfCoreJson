using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Models.OrderWithOrderDetailJson
{
    public record OrderWithOrderDetailJsonUpdateDto(Guid Id, string CustomerName, List<OrderDetailsJsonDto> OrderDetails);
    public record OrderDetailsJsonDto(int ListIndex, float Price, int Quantity);
}
