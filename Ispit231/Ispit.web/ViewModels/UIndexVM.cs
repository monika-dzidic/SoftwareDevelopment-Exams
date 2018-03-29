using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UIndexVM
    {
        public List<Row> Uputnice { get; set; }
        public class Row
        {
            public int UputnicaId { get; set; }
            public DateTime Datum { get; set; }
            public string UputioLjekar { get; set; }
            public string Pacijent { get; set; }
            public string VrstaPretrage { get; set; }
            public DateTime? DatumRezultata { get; set; }
        }
    }
}
