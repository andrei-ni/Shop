using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class OrderDetailsResponse
    {
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
		public string Message { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public double TotalPrice { get; set; }
        public List<OrderDetailsProductResponse>? Products { get; set; }
    }
}
