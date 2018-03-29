using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data.Models
{
    public class Nastavnik
    {
        [Key]
        public int NastavnikID { get; set; }
        public string ImePrezime { get; set; }
    }
}
