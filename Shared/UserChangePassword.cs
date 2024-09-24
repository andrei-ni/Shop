using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public  class UserChangePassword
    {
        [Required, StringLength(20, MinimumLength = 6, ErrorMessage = "Parola trebuie sa aiba intre 6 si 20 caractere")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Parolele nu se potrivesc")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
