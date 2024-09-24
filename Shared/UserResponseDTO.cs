using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrPrint.Shared
{
    public class UserResponseDTO
    {
        public List<User> Users { get; set; } = new List<User>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
