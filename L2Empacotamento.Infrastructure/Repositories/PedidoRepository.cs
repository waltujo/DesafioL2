using L2Empacotamento.Domain.Entities;
using L2Empacotamento.Infrastructure.Persistence;
using L2Empacotamento.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L2Empacotamento.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly EmpacotamentoDbContext _context;
        public PedidoRepository(EmpacotamentoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            return await _context.Pedidos.Include(p => p.Produtos).ToListAsync();
        }
        public async Task<Pedido?> GetPedidoPorIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Produtos).FirstOrDefaultAsync(p => p.PedidoId == id);
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
