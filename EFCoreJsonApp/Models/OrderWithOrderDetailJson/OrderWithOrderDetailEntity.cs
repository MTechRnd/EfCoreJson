using EFCoreJsonApp.Comman;

namespace EFCoreJsonApp.Models.OrderWithOrderDetail
{
    public class OrderWithOrderDetailEntity: BasicEntity
    {
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailsJson> OrderDetailsJson { get; set; } = new();
    }

    public class OrderDetailsJson
    {
        public string ItemName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
    }
}
