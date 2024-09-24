using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class UserResetPassword
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, StringLength(20, MinimumLength = 6, ErrorMessage = "Parola trebuie sa aiba intre 6 si 20 caractere")]
        public string NewPassword { get; set; } = string.Empty;
        [Required, Compare("NewPassword", ErrorMessage = "Parolele nu se potrivesc")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
