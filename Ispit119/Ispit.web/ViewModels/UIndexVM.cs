using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UIndexVM
    {
        public int OdjeljenjeId { get; set; }
        public List<Row> Ucenici { get; set; }
        public class Row
        {
            public int OdjeljenjeStavkaId { get; set; }
            public int BrojUDnevniku { get; set; }
            public string Ucenik { get; set; }
            public int BrojZakljucenihOcjena { get; set; }
        }
    }
}
