using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class FIndexVM
    {
        public List<Row> Fakture { get; set; }
        public class Row
        {
            public int FakutraId { get; set; }
            public DateTime Datum { get; set; }
            public string Klijent { get; set; }
        }
    }
}
