using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data.Models
{
    public class Odjeljenje
    {
        public int Id { get; set; }
        public string Oznaka { get; set; }
        public int Razred { get; set; }
        public int? NastavnikId { get; set; }
        public Nastavnik Nastavnik { get; set; }
    }
}
