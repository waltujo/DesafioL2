using L2Empacotamento.Application.DTOs;
using L2Empacotamento.Application.Interfaces.Repositories;
using L2Empacotamento.Domain.Entities;
using L2Empacotamento.Infrastructure.Persistence;
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

        public async Task AdicionarPedidoAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
