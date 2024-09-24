using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu."), StringLength(62, MinimumLength = 16, ErrorMessage = "Numele trebuie sa aiba intre 16 si 62 caractere.")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new List<Image>();
        [Required(ErrorMessage = "Pretul este obligatoriu.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Pretul original este obligatoriu.")]
        public double OriginalPrice { get; set; }
        public int View { get; set; }
        public int Stock { get; set; }
        public bool Featured { get; set; } = false;
        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
