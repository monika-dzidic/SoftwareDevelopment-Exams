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
        public MyContext(DbContextOptions<MyContext> options): base(options)
        {

        }
        public DbSet<Ucenik> Ucenik { get; set; }
        public DbSet<UpisUOdjeljenje> UpisUOdjeljenje { get; set; }
        public DbSet<Odjeljenje> Odjeljenje { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<MaturskiIspit> MaturskiIspiti { get; set; }
        public DbSet<MaturskiIspitStavka> MaturskiIspitiStavke { get; set; }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MaturskiIspit>().HasOne(x => x.Odjeljenje).WithMany().HasForeignKey(x => x.OdjeljenjeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaturskiIspitStavka>().HasOne(x => x.UpisUOdjeljenje).WithMany().HasForeignKey(x => x.UpisUOdjeljenjeId).OnDelete(DeleteBehavior.Restrict);
        }

        public object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
