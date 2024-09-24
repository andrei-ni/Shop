using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class EmailDTO
    {
        public string To { get; set; } = string.Empty;
        public string Cc { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
