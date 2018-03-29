using Microsoft.EntityFrameworkCore;
using Ispit.data.Models;

namespace Ispit.data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RezultatPretrage>().HasOne(x => x.Uputnica)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<VrstaPretrage> VrstaPretrage { get; set; }
        public DbSet<Ljekar> Ljekar { get; set; }
        public DbSet<Pacijent> Pacijent { get; set; }
        public DbSet<RezultatPretrage> RezultatPretrage { get; set; }
        public DbSet<Uputnica> Uputnica { get; set; }
        public DbSet<LabPretraga> LabPretraga { get; set; }
        public DbSet<Modalitet> Modalitet { get; set; }
    }
}
