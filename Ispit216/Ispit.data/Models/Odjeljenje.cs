﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data.Models
{ 
    public class Odjeljenje
    {
        public int Id { get; set; }

        public Nastavnik Nastavnik { get; set; }
        public int NastavnikId { get; set; }

        public string Naziv { get; set; }
    }
}
