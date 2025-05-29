using L2Empacotamento.Application.DTOs;
using L2Empacotamento.Application.Interfaces;
using L2Empacotamento.Application.Interfaces.Repositories;
using L2Empacotamento.Domain.Entities;
using L2Empacotamento.Domain.Validations;

namespace L2Empacotamento.Application.Services
{
    public class EmbalagemService : IEmbalagemService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly List<CaixaDisponivel> _caixasDisponiveis = new()
        {
            new("Caixa 1", 30, 40, 80),
            new("Caixa 2", 80, 50, 40),
            new("Caixa 3", 50, 80, 60),
        };

        public EmbalagemService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<EmpacotarPedidoResponse> EmpacotarAsync(EmpacotarPedidoRequest request)
        {
            ValidarRequisicao(request);

            var response = new EmpacotarPedidoResponse
            {
                Pedidos = new List<PedidoEmpacotadoDTO>()
            };

            foreach (var pedido in request.Pedidos)
            {
                var caixasUsadas = new List<CaixaEmpacotadaDTO>();
                var produtosNaoEmpacotados = new List<ProdutoDTO>(pedido.Produtos);

                foreach (var caixa in _caixasDisponiveis.OrderBy(c => c.Volume))
                {
                    var caixaAtual = new CaixaEmpacotadaDTO
                    {
                        CaixaId = caixa.Id,
                        Produtos = new List<string>()
                    };

                    foreach (var produto in produtosNaoEmpacotados.ToList())
                    {
                        if (produto.Dimensoes.Altura <= caixa.Altura &&
                            produto.Dimensoes.Largura <= caixa.Largura &&
                            produto.Dimensoes.Comprimento <= caixa.Comprimento)
                        {
                            caixaAtual.Produtos.Add(produto.ProdutoId);
                            produtosNaoEmpacotados.Remove(produto);
                        }
                    }

                    if (caixaAtual.Produtos.Any())
                        caixasUsadas.Add(caixaAtual);
                }

                foreach (var restante in produtosNaoEmpacotados)
                {
                    caixasUsadas.Add(new CaixaEmpacotadaDTO
                    {
                        CaixaId = null,
                        Produtos = new List<string> { restante.ProdutoId },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                }

                var pedidoEntity = new Pedido
                {
                    Produtos = pedido.Produtos.Select(prodDto => new Produto
                    {
                        ProdutoId = prodDto.ProdutoId,
                        Dimensoes = new Dimensoes
                        {
                            Altura = prodDto.Dimensoes.Altura,
                            Largura = prodDto.Dimensoes.Largura,
                            Comprimento = prodDto.Dimensoes.Comprimento
                        }
                    }).ToList()
                };

                var sucesso = await _pedidoRepository.AdicionarPedidoAsync(pedidoEntity);
                if (!sucesso)
                    throw new Exception($"Erro ao salvar o pedido {pedido.PedidoId} no banco de dados.");

                response.Pedidos.Add(new PedidoEmpacotadoDTO
                {
                    PedidoId = pedido.PedidoId,
                    Caixas = caixasUsadas
                });
            }

            return response;
        }

        private void ValidarRequisicao(EmpacotarPedidoRequest request)
        {
            if (request?.Pedidos == null || !request.Pedidos.Any())
                throw new ArgumentException("A lista de pedidos não pode ser nula ou vazia.");

            foreach (var pedido in request.Pedidos)
            {
                if (pedido.Produtos == null || !pedido.Produtos.Any())
                    throw new ArgumentException($"O pedido {pedido.PedidoId} não contém produtos.");

                foreach (var produto in pedido.Produtos)
                {
                    if (string.IsNullOrWhiteSpace(produto.ProdutoId))
                        throw new ArgumentException("ProdutoId não pode ser nulo ou vazio.");

                    if (produto.Dimensoes == null)
                        throw new ArgumentException($"Produto {produto.ProdutoId} está com dimensões nulas.");

                    if (produto.Dimensoes.Altura <= 0 ||
                        produto.Dimensoes.Largura <= 0 ||
                        produto.Dimensoes.Comprimento <= 0)
                    {
                        throw new ArgumentException($"Produto {produto.ProdutoId} possui dimensões inválidas (valores devem ser maiores que zero).");
                    }
                }
            }
        }
    }

    internal class CaixaDisponivel
    {
        public string Id { get; }
        public int Altura { get; }
        public int Largura { get; }
        public int Comprimento { get; }
        public int Volume => Altura * Largura * Comprimento;

        public CaixaDisponivel(string id, int altura, int largura, int comprimento)
        {
            Id = id;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }
    }
}
