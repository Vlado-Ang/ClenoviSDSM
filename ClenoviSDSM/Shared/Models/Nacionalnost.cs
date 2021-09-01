using System.ComponentModel.DataAnnotations;

namespace ClenoviSDSM.Shared.Models
{
    public class Nacionalnost
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Националноста е задолжителна")]
        public string NacOpis { get; set; }
    }
}
