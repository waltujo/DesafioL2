namespace L2Empacotamento.Application.DTOs
{
    public class EmpacotarPedidoRequest
    {
        public List<PedidoDTO> Pedidos { get; set; } = new();
    }
}
