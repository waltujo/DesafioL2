using L2Empacotamento.Application.DTOs;
using L2Empacotamento.Application.Interfaces.Repositories;
using L2Empacotamento.Application.Services;
using Moq;

namespace L2Empacotamento.Test
{
    public class EmbalagemServiceTests
    {
        [Fact]
        public async Task DeveEmpacotarProdutoQueCabeNaCaixa()
        {
            // Arrange
            var mockRepo = new Mock<IPedidoRepository>();
            var service = new EmbalagemService(mockRepo.Object);

            var pedido = new PedidoDTO
            {
                PedidoId = 1,
                Produtos = new List<ProdutoDTO>
                {
                    new ProdutoDTO
                    {
                        ProdutoId = "PS5",
                        Dimensoes = new DimensoesDTO { Altura = 30, Largura = 30, Comprimento = 30 }
                    }
                }
            };

            var request = new EmpacotarPedidoRequest
            {
                Pedidos = new List<PedidoDTO> { pedido }
            };

            // Act
            var resultado = await service.EmpacotarAsync(request);

            // Assert
            Assert.Single(resultado.Pedidos);
            var caixas = resultado.Pedidos.First().Caixas;
            Assert.Single(caixas);
            Assert.Equal("PS5", caixas.First().Produtos.First());
            Assert.NotNull(caixas.First().CaixaId);
            Assert.Null(caixas.First().Observacao);
        }

        [Fact]
        public async Task DeveRetornarObservacaoParaProdutoGrande()
        {
            var mockRepo = new Mock<IPedidoRepository>();
            var service = new EmbalagemService(mockRepo.Object);

            var pedido = new PedidoDTO
            {
                PedidoId = 2,
                Produtos = new List<ProdutoDTO>
                {
                    new ProdutoDTO
                    {
                        ProdutoId = "Cadeira Gamer",
                        Dimensoes = new DimensoesDTO { Altura = 120, Largura = 60, Comprimento = 70 }
                    }
                }
            };

            var request = new EmpacotarPedidoRequest
            {
                Pedidos = new List<PedidoDTO> { pedido }
            };

            var resultado = await service.EmpacotarAsync(request);

            Assert.Single(resultado.Pedidos);
            var caixa = resultado.Pedidos.First().Caixas.First();
            Assert.Null(caixa.CaixaId);
            Assert.Contains("Cadeira Gamer", caixa.Produtos);
            Assert.Equal("Produto não cabe em nenhuma caixa disponível.", caixa.Observacao);
        }
    }
}
