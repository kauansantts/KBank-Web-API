using KBank_Web_API.DTOs;
using KBank_Web_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KBank_Web_API.Infra
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<CompraParcelada> ComprasParceladas { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transacao>()
                .Property(x => x.CategoriaTransacao)
                .HasConversion<string>();

            modelBuilder.Entity<Transacao>()
                .Property(x => x.TipoTransacao)
                .HasConversion<string>();

            modelBuilder.Entity<Transacao>()
                .Property(x => x.Identificador)
                .HasConversion<string>();

            modelBuilder.Entity<Transacao>()
                .Property(x => x.Cancelada)
                .HasConversion<string>();




            modelBuilder.Entity<CompraParcelada>()
                .Property(x => x.CategoriaParcela)
                .HasConversion<string>();

            modelBuilder.Entity<CompraParcelada>()
                .Property(x => x.TipoCompra)
                .HasConversion<string>();
            modelBuilder.Entity<CompraParcelada>()
                .Property(x => x.Cancelada)
                .HasConversion<string>();



            modelBuilder.Entity<Parcela>()
                .Property(x => x.CategoriaParcela)
                .HasConversion<string>();

            modelBuilder.Entity<Parcela>()
                .Property(x => x.TipoParcela)
                .HasConversion<string>();
            modelBuilder.Entity<Parcela>()
                .Property(x => x.Processada)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
