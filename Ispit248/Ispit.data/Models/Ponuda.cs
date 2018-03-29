using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ispit.data.Models
{
    public class Ponuda
    {
        public int Id { get; set; }
        public virtual Klijent Klijent { get; set; }
        public int KlijentId { get; set; }
        public DateTime Datum { get; set; }
        public int ? FakturaID { get; set; }
        [ForeignKey(nameof(FakturaID))]
        public Faktura Faktura { get; set; }
    }
}
