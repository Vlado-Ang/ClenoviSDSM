using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClenoviSDSM.Shared.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Внесете корисник")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Внесете лозинка")]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
