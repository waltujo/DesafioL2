using L2Empacotamento.Application.DTOs;

namespace L2Empacotamento.Application.Interfaces
{
    public interface IEmbalagemService
    {
        Task<EmpacotarPedidoResponse> EmpacotarAsync(EmpacotarPedidoRequest request);
    }
}
