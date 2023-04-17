using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AcilYardimVeritabani.Models
{
    public class AcilYardimContext : DbContext
    {
        public DbSet<Operator> Operatorler { get; set; }
        public DbSet<Ekip> Ekipler { get; set; }
        public DbSet<OperatorMesaj> OperatorMesajlari { get; set; }
        public DbSet<EkipGeriDonus> EkipGeriDonusleri { get; set; }

        public AcilYardimContext(DbContextOptions<AcilYardimContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperatorMesaj>()
                .HasOne(om => om.Operator)
                .WithMany(o => o.OperatorMesajlari)
                .HasForeignKey(om => om.OperatorId);

            modelBuilder.Entity<OperatorMesaj>()
                .HasOne(om => om.Ekip)
                .WithMany(e => e.OperatorMesajlari)
                .HasForeignKey(om => om.EkipId);

            modelBuilder.Entity<EkipGeriDonus>()
                .HasOne(egd => egd.Ekip)
                .WithMany(e => e.EkipGeriDonusleri)
                .HasForeignKey(egd => egd.EkipId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
