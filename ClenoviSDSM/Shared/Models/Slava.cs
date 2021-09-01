using System.ComponentModel.DataAnnotations;

namespace ClenoviSDSM.Shared.Models
{
    public class Slava
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Описот е задолжителен")]
        public string SlavaOpis { get; set; }
    }
}
