using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class PIndexVM
    {
        public int FakturaId { get; set; }
        public List<Row> Proizvodi { get; set; }
        public class Row
        {
            public int StavkaId { get; set; }
            public string Proizvod { get; set; }
            public float Cijena { get; set; }
            public int Kolicina { get; set; }
            public float Popust { get; set; }
            public float Iznos { get; set; }
        }
    }
}
