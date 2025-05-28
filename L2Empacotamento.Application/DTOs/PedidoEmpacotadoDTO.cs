namespace L2Empacotamento.Application.DTOs
{
    public class PedidoEmpacotadoDTO
    {
        public int PedidoId { get; set; }
        public List<CaixaEmpacotadaDTO> Caixas { get; set; } = new();
    }
}
