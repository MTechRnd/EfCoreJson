using EFCoreJsonApp.Comman;
using EFCoreJsonApp.Models.OrderDetails;

namespace EFCoreJsonApp.Models.Order
{
    public class OrderEntity : BasicEntity
    {
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailEntity> OrderDetails { get; set; } = new List<OrderDetailEntity>();
    }

}
