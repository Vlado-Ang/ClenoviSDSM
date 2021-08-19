using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClenoviSDSM.Shared.Models
{
    public class ClenoviFilter
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string EMBG { get; set; }
        public string BrClenskaKarta { get; set; }
        public DateTime? DataRagjanjeOd { get; set; }
        public DateTime? DataRagjanjeDo { get; set; }
    }
}
