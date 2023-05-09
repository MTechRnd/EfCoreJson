using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }

}
