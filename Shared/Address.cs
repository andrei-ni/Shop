using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Te rugam sa introduci un nume valid.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Te rugam sa introduci un numar de telefon valid, de forma: 07xxxxxxxx")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Te rugam sa introduci o strada valida.")]
        public string Street { get; set; } = string.Empty;
        [Required(ErrorMessage = "Te rugam sa introduci un oras valid.")]
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "Te rugam sa introduci un cod postal valid.")]
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyVat { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
    }
}
