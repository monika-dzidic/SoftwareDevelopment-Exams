using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class IDetaljiVM
    {
        public int MaturskiIspitId { get; set; }
        public string Ispitivac { get; set; }
        public DateTime Datum { get; set; }
        public string Odjeljenje { get; set; }
    }
}
