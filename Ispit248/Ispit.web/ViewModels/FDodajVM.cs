using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class FDodajVM
    {
        public List<SelectListItem> Klijenti { get; set; }
        public DateTime Datum { get; set; }
        public List<SelectListItem> PonudeBezFakture { get; set; }
    }
}
