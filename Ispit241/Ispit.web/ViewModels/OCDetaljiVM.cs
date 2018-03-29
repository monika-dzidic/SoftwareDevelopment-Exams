using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class OCDetaljiVM
    {
        public int OdrzaniCasId { get; set; }
        public string Nastavnik { get; set; }
        public DateTime Datum { get; set; }
        public string Odjeljenje { get; set; }
        public string Predmet { get; set; }
    }
}
