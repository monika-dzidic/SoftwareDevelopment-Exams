using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.web.ViewModels
{
    public class UIndexVM
    {
        public int OdrzaniCasId { get; set; }
        public List<Row> Ucenici { get; set; }
        public class Row
        {
            public int OdrzaniCasDetaljiId { get; set; }
            public string Ucenik { get; set; }
            public int Ocjena { get; set; }
            public string Prisutan { get; set; }
            public string OpravdanoOdsutan { get; set; }
        }
    }
}
