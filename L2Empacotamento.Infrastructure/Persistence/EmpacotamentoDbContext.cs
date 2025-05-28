using L2Empacotamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace L2Empacotamento.Infrastructure.Persistence
{
    public class EmpacotamentoDbContext : DbContext
    {
        public EmpacotamentoDbContext(DbContextOptions<EmpacotamentoDbContext> options) : base(options) {}

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().HasKey(p => p.PedidoId);
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Produtos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Produto>().HasKey(p => p.ProdutoId);
            modelBuilder.Entity<Produto>().OwnsOne(p => p.Dimensoes);
        }
    }
}
