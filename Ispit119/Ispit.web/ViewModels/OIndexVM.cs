using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class OIndexVM
    {
        public List<Row> Odjeljenja { get; set; }
        public class Row
        {
            public int OdjeljenjeId { get; set; }
            public string SkolskaGodina { get; set; }
            public int Razred { get; set; }
            public string Oznaka { get; set; }
            public string Razrednik { get; set; }
            public string PrebacenUViseOdjeljenje { get; set; }
        }
    }
}
