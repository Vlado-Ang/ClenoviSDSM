using System;

namespace ClenoviSDSM.Shared.Models
{
    public class ClenoviFilter
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon1 { get; set; }
        public DateTime? DataRagjanjeOd { get; set; }
        public DateTime? DataRagjanjeDo { get; set; }
        public string RabotenStatusOpis { get; set; }
        public string ObrazovnaInstitucija { get; set; }
        public string OpisStatus { get; set; }
        public string IzbirackoMesto { get; set; }
        public string SlavaOpis { get; set; }
        public string NacOpis { get; set; }
        public string Pol { get; set; }
    }
}
