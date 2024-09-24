using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class OrderDetailsProductResponse
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
