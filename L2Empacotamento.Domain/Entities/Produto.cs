using L2Empacotamento.Domain.Validations;

namespace L2Empacotamento.Domain.Entities
{
    public class Produto
    {
        public string ProdutoId { get; set; }
        public Dimensoes Dimensoes { get; set; }
    }
}
