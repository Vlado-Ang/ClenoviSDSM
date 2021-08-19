using System.ComponentModel.DataAnnotations;

namespace ClenoviSDSM.Shared.Models
{
    public class StepenObrazovanie
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Описот е задолжителен")]
        public string StepenObrOpis { get; set; }
    }
}
