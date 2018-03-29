using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UIndexVM
    {
        public int MaturskiIspitId { get; set; }
        public List<Row> Ucenici { get; set; }
        public class Row
        {
            public int MaturskiIspitStavkaId { get; set; }
            public string Ucenik { get; set; }
            public int OpciUspjeh { get; set; }
            public string Bodovi { get; set; }
            public string Oslobodjen { get; set; }
        }
    }
}
