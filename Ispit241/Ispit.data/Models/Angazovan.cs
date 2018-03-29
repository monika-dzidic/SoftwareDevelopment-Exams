using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data.Models
{
    public class Angazovan
    {
        public int Id { get; set; }
        public int? PredmetId { get; set; }
        public Predmet Predmet { get; set; }
        public int? NastavnikId { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public int? OdjeljenjeId { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
    }
}
