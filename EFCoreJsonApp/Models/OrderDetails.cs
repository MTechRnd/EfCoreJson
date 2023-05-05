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
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [MaxLength(100)]
        public string ItemName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Total { get; set; }

        public Order Order { get; set; }
    }
}
