using System;
using System.ComponentModel;

namespace ClenoviSDSM.Shared.Models
{
    public class ClenExcel
    {
        [DisplayName("Ime")]
        public string Ime { get; set; }
        [DisplayName("Презиме")]
        public string Prezime { get; set; }
        [DisplayName("ЕМБГ")]
        public string EMBG { get; set; }
        [DisplayName("Членска карта бр.")]
        public string BrClenskaKarta { get; set; }
        [DisplayName("Дата на раѓање")]
        public DateTime? DataRagjanje { get; set; }
        [DisplayName("Телефон")]
        public string Telefon1 { get; set; }
        [DisplayName("e-Mail")]
        public string Email { get; set; }
        [DisplayName("Работен статус")]
        public string OpisStatus { get; set; }
        [DisplayName("Работодавач")]
        public string RabotenStatusOpis { get; set; }
        [DisplayName("Степен на образование")]
        public string StepenObrOpis { get; set; }
        [DisplayName("Образовна институција")]
        public string ObrazovnaInstitucija { get; set; }
        [DisplayName("Насока")]
        public string ObrNasoka { get; set; }
        [DisplayName("Избирачко место")]
        public string IzbirackoMesto { get; set; }
        [DisplayName("Куќна слава")]
        public string SlavaOpis { get; set; }
        [DisplayName("Националност")]
        public string NacOpis { get; set; }
        [DisplayName("Пол")]
        public string Pol { get; set; }
    }

}
