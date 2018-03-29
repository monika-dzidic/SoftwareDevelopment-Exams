using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class PDodajVM
    {
        public int FakturaId { get; set; }
        public List<SelectListItem> Proizvodi { get; set; }
        public int Kolicina { get; set; }
    }
}
