# SalesTechinalAssign API

Esta API foi desenvolvida como parte de um teste técnico para simular um sistema de vendas, com arquitetura limpa e aplicação de boas práticas de desenvolvimento.

## Funcionalidades

- CRUD completo de vendas
- Aplicação de desconto:
  - 4 a 9 unidades: 10%
  - 10 a 20 unidades: 20%
  - Acima de 20 unidades: venda proibida
- Cálculo automático do valor total da venda
- Cancelamento lógico de vendas
- Registro de eventos de venda (criação, alteração, cancelamento)
- Suporte a filtros por cliente e filial
- Ordenação por data, cliente ou filial
- Paginação de resultados
- Camadas separadas por responsabilidade (Models, DTOs, Interfaces, Services, Controllers)
- Testes automatizados cobrindo as regras de negócio
- Utilização de AutoMapper para simplificar mapeamentos

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (InMemory)
- AutoMapper
- xUnit
- Swagger

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Clonar e executar

```Git Bash
git clone https://github.com/GFGimenes/SalesTechnicalAssignment
cd salestechinalassign
dotnet run
```

A aplicação estará disponível em:

- https://localhost:5001
- http://localhost:5000

### Documentação da API

Acesse: `https://localhost:5001/swagger` para visualizar a documentação interativa gerada via Swagger.

## Executando os Testes

Certifique-se de instalar os pacotes necessários antes de executar os testes:

```bash
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Microsoft.NET.Test.Sdk
```

Depois, execute:

```bash
dotnet test
```

## Endpoints

### POST /api/sales

Cria uma nova venda.

Exemplo de corpo da requisição:

```json
{
  "customer": "João",
  "branch": "São Paulo",
  "items": [
    {
      "productId": 1,
      "productName": "Notebook",
      "quantity": 5,
      "unitPrice": 2000
    }
  ]
}
```

### GET /api/sales

Lista todas as vendas com suporte a filtros e paginação.

Parâmetros disponíveis:
- `customer`
- `branch`
- `orderBy`: `date`, `customer`, `branch` (aceita `desc`)
- `page`, `size`

### GET /api/sales/{id}

Busca uma venda específica pelo ID.

### PUT /api/sales/{id}

Atualiza os dados de uma venda existente.

### DELETE /api/sales/{id}

Cancela logicamente uma venda.

## Estrutura do Projeto

```
/Controllers
/Data
/DTO
/Interface
/Mapping
/Models
/Service
/Tests
```

## Observações

- O projeto utiliza um banco de dados em memória para facilitar a execução e os testes.
- As regras de negócio foram implementadas com testes automatizados.
- O AutoMapper é utilizado para facilitar a conversão entre DTOs e entidades.
- A arquitetura facilita a manutenção e a evolução futura da aplicação.