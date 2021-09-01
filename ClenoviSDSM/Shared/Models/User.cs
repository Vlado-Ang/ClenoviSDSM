using System;
using System.ComponentModel.DataAnnotations;


namespace ClenoviSDSM.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Корисничкото име е задолжително")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Лозинката е задолжителна")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password),ErrorMessage =" Внесените лозинки не се еднакви")]
        public string Password2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Ролјата е задолжителна")]
        public string RoleName { get; set; }
        public bool IsDisabled { get; set; }
        public string RefreshToken { get; set; }
    }
}
