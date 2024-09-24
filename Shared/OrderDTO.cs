using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class OrderDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Address Address { get; set; } = new Address();
    }
}
