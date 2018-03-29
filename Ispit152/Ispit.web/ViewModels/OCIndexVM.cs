using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class OCIndexVM
    {
        public int NastavnikId { get; set; }
        public List<Row> OdrzaniCasovi { get; set; }
        public class Row
        {
            public int OdrzaniCasId { get; set; }
            public DateTime Datum { get; set; }
            public string AkademskaGodina { get; set; }
            public string Predmet { get; set; }
            public int BrojPrisutnih { get; set; }
            public int BrojURazredu { get; set; }
            public double ProsjecnaOcjena { get; set; }
        }
    }
}
