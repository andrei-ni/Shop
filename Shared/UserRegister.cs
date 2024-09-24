using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
	public class UserRegister
	{
        [Required(ErrorMessage = "Te rugam sa introduci o adresa e email"), EmailAddress(ErrorMessage = "Adresa de email nu este corecta")]
		public string Email { get; set; } = string.Empty;
		[Required(ErrorMessage = "Te rugam sa introduci o parola"), StringLength(20, MinimumLength = 6, ErrorMessage = "Parola trebuie sa aiba intre 6 si 20 caractere")]
		public string Password { get; set; } = string.Empty;
		[Compare("Password", ErrorMessage = "Parolele nu se potrivesc")]
		public string ConfirmPassword { get; set; } = string.Empty;
        public string? VerificationToken { get; set; }
    }
}
