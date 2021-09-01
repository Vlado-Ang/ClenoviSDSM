using System;
using System.ComponentModel.DataAnnotations;

namespace ClenoviSDSM.Shared.Models
{
    public class Clen
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Името е задолжително")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Презимето е задолжително")]
        public string Prezime { get; set; }
        [RegularExpression(@"^[0-9]{13}$",ErrorMessage = "ЕМБГ мора да има 13 цифри")]
        public string EMBG { get; set; }
        [RegularExpression(@"^[0-9]+$|^$", ErrorMessage = "Дозволени се само цифри")]
        public string BrClenskaKarta { get; set; }
        public DateTime? DataRagjanje { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Hobi { get; set; }
        public int? RabotenStatusId { get; set; }
        public int? StepenObrId { get; set; }
        public string RabotenStatusOpis { get; set; }
        public string FbAccount { get; set; }
        public string TwitterAccount { get; set; }
        public string InstagramAccount { get; set; }
        public string LinkedInAccount { get; set; }
        public string IzbirackoMesto { get; set; }
        public string IzbOpstina { get; set; }
        public string ObrazovnaInstitucija { get; set; }
        public string ObrNasoka { get; set; }
        public string IzbSpisok { get; set; }
        public int? SlavaId { get; set; }
        [Required(ErrorMessage = "Полот е задолжителен")]
        public string Pol { get; set; }
        public int? NacionalnostId { get; set; }
        public string OpisStatus { get; set; }
        public string StepenObrOpis { get; set; }
        public string SlavaOpis { get; set; }
        public string NacOpis { get; set; }
    }

}
