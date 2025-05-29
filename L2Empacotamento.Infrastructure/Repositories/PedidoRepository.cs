using L2Empacotamento.Application.Interfaces.Repositories;
using L2Empacotamento.Domain.Entities;
using L2Empacotamento.Infrastructure.Persistence;

namespace L2Empacotamento.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly EmpacotamentoDbContext _context;
        public PedidoRepository(EmpacotamentoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdicionarPedidoAsync(Pedido pedido)
        {
            try
            {
                await _context.Pedidos.AddAsync(pedido);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar pedido: {ex.Message}");
                return false;
            }
        }
    }
}
