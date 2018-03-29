using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UDodajVM
    {
        public List<SelectListItem> Ljekari { get; set; }
        public List<SelectListItem> Pacijent { get; set; }
        public List<SelectListItem> VrstaPretrage { get; set; }
    }
}
