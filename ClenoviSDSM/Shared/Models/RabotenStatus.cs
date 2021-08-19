using System;
using System.ComponentModel.DataAnnotations;

namespace ClenoviSDSM.Shared.Models
{
    public class RabotenStatus
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Описот е задолжителен")]
        public string OpisStatus { get; set; }
    }
}
