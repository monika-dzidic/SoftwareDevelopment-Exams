using Ispit.data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit.data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
        public DbSet<AkcijskiKatalog> AkcijskiKatalog { get; set; }
        public DbSet<KatalogStavka> KatalogStavka { get; set; }
        public DbSet<Klijent> Klijent { get; set; }
        public DbSet<Ponuda> Ponuda { get; set; }
        public DbSet<PonudaStavka> PonudaStavka { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Faktura> Faktura { get; set; }
        public DbSet<FakturaStavka> FakturaStavka { get; set; }
    }
}
