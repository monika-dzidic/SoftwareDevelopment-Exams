using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UDodajVM
    {
        public int OdjeljenjeId { get; set; }
        public List<SelectListItem> Ucenici { get; set; }
    }
}
