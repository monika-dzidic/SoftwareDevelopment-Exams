using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class ODodajVM
    {
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public List<SelectListItem> Nastavnici { get; set; }
        public List<SelectListItem> NizaOdjeljenja { get; set; }
    }
}
