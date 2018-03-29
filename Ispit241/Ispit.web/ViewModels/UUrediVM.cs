using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UUrediVM
    {
        public int OdrzaniCasDetaljiId { get; set; }
        public string Ucenik { get; set; }
        public bool Prisustvo { get; set; } 
        public int Ocjena { get; set; }
        public string OpravdanoOdsutan { get; set; }
    }
}
