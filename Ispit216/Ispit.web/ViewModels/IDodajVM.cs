using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class IDodajVM
    {
        public List<SelectListItem> Nastavnici { get; set; }
        public DateTime Datum { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }
    }
}
