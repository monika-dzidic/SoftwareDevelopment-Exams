using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ispit.data.Models
{
    public class MaturskiIspitStavka
    {
        public int Id { get; set; }
        public float? Bodovi { get; set; }
        public bool Oslobodjen { get; set; }

        [ForeignKey(nameof(MaturskiIspitId))]
        public int MaturskiIspitId { get; set; }
        public MaturskiIspit MaturskiIspit { get; set; }

        [ForeignKey(nameof(UpisUOdjeljenjeId))]
        public int UpisUOdjeljenjeId { get; set; }
        public UpisUOdjeljenje UpisUOdjeljenje{ get; set; }
    }
}
