using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class OCDodajVM
    {
        public int NastavnikId { get; set; }
        public string Nastavnik { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }
    }
}
