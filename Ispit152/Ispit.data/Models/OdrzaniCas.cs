using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data.Models
{
    public class OdrzaniCas
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey("Angazovan")]
        public int AngazovanId { get; set; }
        public virtual Angazovan Angazovan { get; set; }
    }
}
