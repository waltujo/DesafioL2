namespace L2Empacotamento.Application.DTOs
{
    public class CaixaEmpacotadaDTO
    {
        public string? CaixaId { get; set; }
        public List<string> Produtos { get; set; } = new();
        public string? Observacao { get; set; }
        }
}
