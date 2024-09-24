using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class UserLogin
    {
        [Required (ErrorMessage = "Te rugam sa introduci adresa e email")]
        public string Email { get; set; } = string.Empty;
        [Required (ErrorMessage = "Te rugam sa introduci parola")]
        public string Password { get; set; } = string.Empty;
    }
}
