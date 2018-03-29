using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class ODetaljiVM
    {
        public int OdjeljenjeId { get; set; }
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public string Razrednik { get; set; }
        public int BrojPredmeta { get; set; }
    }
}
