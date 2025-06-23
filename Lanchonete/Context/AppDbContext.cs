using Lanchonete.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lanchonete.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<LancheMateriaPrima> LanchesMateriasPrimas { get; set; }
        public DbSet<RegistroCaixaDiario> RegistrosCaixaDiario { get; set; } = null!;
        public DbSet<TransacaoFinanceira> TransacoesFinanceiras { get; set; } = null!;
        public DbSet<CategoriaFinanceira> CategoriasFinanceiras { get; set; } = null!;

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LancheMateriaPrima>()
                .HasKey(lmp => new { lmp.LancheId, lmp.MateriaPrimaId });

            modelBuilder.Entity<LancheMateriaPrima>()
                .HasOne(lmp => lmp.Lanche)
                .WithMany(l => l.LanchesMateriasPrimas)
                .HasForeignKey(lmp => lmp.LancheId);

            modelBuilder.Entity<LancheMateriaPrima>()
                .HasOne(lmp => lmp.MateriaPrima)
                .WithMany(mp => mp.LanchesMateriasPrimas)
                .HasForeignKey(lmp => lmp.MateriaPrimaId);
        }
    }
}