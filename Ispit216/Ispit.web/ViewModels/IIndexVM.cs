using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class IIndexVM
    {
        public List<Row> MaturskiIspiti { get; set; }
        public class Row
        {
            public int IspitId { get; set; }
            public DateTime Datum { get; set; }
            public string Odjeljenje { get; set; }
            public string Ispitivac { get; set; }
            public float ProsjecniBodovi { get; set; }
        }
    }
}
