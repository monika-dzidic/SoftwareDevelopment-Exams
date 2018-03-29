using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ispit.data.Models
{
    public class MaturskiIspit
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }

        [ForeignKey(nameof(IspitivacId))]
        public int IspitivacId { get; set; }
        public Nastavnik Ispitivac { get; set; }

        [ForeignKey(nameof(OdjeljenjeId))]
        public int OdjeljenjeId { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
    }
}
