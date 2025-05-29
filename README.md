# 📦 L2Empacotamento

API em .NET 8 que automatiza o empacotamento de pedidos da loja online de jogos do seu Manoel. A aplicação recebe uma lista de produtos com dimensões e retorna quais caixas disponíveis devem ser usadas para empacotar os produtos de forma eficiente.

---

## 🧰 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (via Docker)
- Swagger (documentação da API)
- JWT (Autenticação)

---

## 🚀 Como Rodar o Projeto Localmente

### Passo a Passo

1. **Clone o repositório**

```bash
git clone https://github.com/waltujo/L2Empacotamento.git
cd DesafioL2
```

2. **Execute o Docker Compose**

```bash
docker-compose up --build
```

3. **Faça autenticação no Swagger**

```bash
http://localhost:8080/swagger

POST /api/auth/login
{
  "username": "l2",
  "password": "TesteL2@123"
}
```

4. **Exemplo de requisição**

```bash
POST /api/embalagem

{
  "pedidos": [
    {
      "pedidoId": "123",
      "produtos": [
        {
          "produtoId": "A1",
          "dimensoes": {
            "altura": 30,
            "largura": 20,
            "comprimento": 10
          }
        }
      ]
    }
  ]
}

```

## 🧪 Funcionalidades da API

 - POST /api/embalagem: Recebe pedidos com produtos e retorna caixas usadas para empacotar.
 - POST /api/auth/login: Gera token JWT de autenticação.



##  ✅ Validações

Antes do empacotamento, a API valida:

 - Pedido sem produtos
 - Produtos com identificador nulo ou vazio
 - Dimensões nulas ou menores ou iguais a zero
