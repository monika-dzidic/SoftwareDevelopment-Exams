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
            public string Predmet { get; set; }
            public int UkupnoUcenika { get; set; }
            public int PrisutnihUcenika { get; set; }
            public string NajboljiUcenik { get; set; }
            public string Odjeljenje { get; set; }
        }
    }
}
