using Ispit.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.data
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        }
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<Odjeljenje> Odjeljenje { get; set; }
        public DbSet<UpisUOdjeljenje> UpisUOdjeljenje { get; set; }
        public DbSet<Ucenik> Ucenik { get; set; }
        public DbSet<OdrzaniCas> OdrzaniCas { get; set; }
        public DbSet<OdrzaniCasDetalj> OdrzaniCasDetalj { get; set; }
        public DbSet<Angazovan> Angazovan { get; set; }
    }
}
