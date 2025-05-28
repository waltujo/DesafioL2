using L2Empacotamento.Application.DTOs;
using L2Empacotamento.Domain.Entities;

namespace L2Empacotamento.Application.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task AdicionarPedidoAsync(Pedido pedido);
        Task SalvarAsync();
    }
}
