using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data.Models
{
    public class OdrzaniCas
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int? AngazovanId { get; set; }
        public Angazovan Angazovan { get; set; }
    }
}
