using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class OCUrediVM
    {
        public int OdrzaniCasId { get; set; }
        public string Nastavnik { get; set; }
        public DateTime Datum { get; set; }
        public string AkademskaGodina { get; set; }
        public string Predmet { get; set; }
    }
}
