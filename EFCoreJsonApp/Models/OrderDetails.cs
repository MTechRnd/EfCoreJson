using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ItemName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
        public Order Order { get; set; }
    }
}
