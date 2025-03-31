using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.Authentication
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public Guid UserID { get; set; }
        public string NameUser { get; set; }
    }
}
