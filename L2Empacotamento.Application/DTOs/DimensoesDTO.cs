namespace L2Empacotamento.Application.DTOs
{
    public class DimensoesDTO
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }

        public int Volume => Altura * Largura * Comprimento;
    }
}
