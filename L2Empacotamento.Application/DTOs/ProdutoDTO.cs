using System.Text.Json.Serialization;

namespace L2Empacotamento.Application.DTOs
{
    public class ProdutoDTO
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; }

        [JsonPropertyName("dimensoes")]
        public DimensoesDTO Dimensoes { get; set; }
    }
}
