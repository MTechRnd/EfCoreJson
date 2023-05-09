using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Models
{
    public class OrderWithOrderDetails
    {
        public int Id { get; set; }
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
