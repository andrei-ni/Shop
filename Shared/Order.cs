using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrPrint.Shared
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId{ get; set; }
        public DateTime OrderDate{ get; set; } = DateTime.Now;
        [Column]
        public double TotalPrice{ get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
