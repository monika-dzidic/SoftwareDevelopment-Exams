using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UDetaljiVM
    {
        public int UputnicaId { get; set; }
        public DateTime Datum { get; set; }
        public string Pacijent { get; set; }
        public DateTime? DatumRezultata { get; set; }
    }
}
