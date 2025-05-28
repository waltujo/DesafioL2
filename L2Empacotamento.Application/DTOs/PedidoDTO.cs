namespace L2Empacotamento.Application.DTOs
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }
        public List<ProdutoDTO> Produtos { get; set; } = new();
    }
}
