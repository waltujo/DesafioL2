using System.Text.Json.Serialization;

namespace L2Empacotamento.Application.DTOs
{
    public class DimensoesDTO
    {
        [JsonPropertyName("altura")]
        public int Altura { get; set; }

        [JsonPropertyName("largura")]
        public int Largura { get; set; }

        [JsonPropertyName("comprimento")]
        public int Comprimento { get; set; }

        public int Volume => Altura * Largura * Comprimento;
    }
}
