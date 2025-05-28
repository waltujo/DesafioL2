namespace L2Empacotamento.Domain.Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public List<Produto> Produtos { get; set; } = new();
    }
}
