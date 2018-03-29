using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class RIndexVM
    {
        public int UputnicaId { get; set; }
        public List<Row> Rezultati { get; set; }
        public class Row
        {
            public int RezultatId { get; set; }
            public string Pretrage { get; set; }
            public bool IsNumericka { get; set; }
            public double? IzmjerenaVrijednost { get; set; }
            public List<SelectListItem> Modaliteti { get; set; }
            public int ModalitetId { get; set; }
            public string ModalitetiNabrajanja { get; set; }
            public string JMJ { get; set; }
            public double? ReferentnaVrijednostMin { get; set; }
            public double? ReferentnaVrijednostMax { get; set; }
        }
    }
}
