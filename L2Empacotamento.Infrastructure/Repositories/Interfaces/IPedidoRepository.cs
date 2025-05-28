using L2Empacotamento.Domain.Entities;

namespace L2Empacotamento.Infrastructure.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> GetPedidosAsync();
        Task<Pedido?> GetPedidoPorIdAsync(int id);
        Task AdicionarAsync(Pedido pedido);
        Task SalvarAsync();
    }
}
