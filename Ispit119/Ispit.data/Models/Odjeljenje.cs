using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ispit.data.Models { 
    public class Odjeljenje
    {
        public int Id { get; set; }
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public bool IsPrebacenuViseOdjeljenje { get; set; }
        public int? NastavnikID { get; set; }
        [ForeignKey(nameof(NastavnikID))]
        public Nastavnik Nastavnik { get; set; }
    }
}
