# 🏦 FintechBank

Aplicação de console em **C# / .NET 10** que implementa as regras de negócio de um banco digital fictício, acompanhada de suítes de **testes unitários parametrizados com xUnit**.

Projeto desenvolvido para a disciplina **Garantia da Qualidade de Software** (Gestão e Qualidade de Software, Ânima Educação), com foco em boas práticas de testes automatizados, versionamento e documentação.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![xUnit](https://img.shields.io/badge/tests-xUnit-5E2D91)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📑 Sumário

- [Visão geral](#-visão-geral)
- [Requisitos técnicos](#-requisitos-técnicos)
- [Estrutura do projeto](#-estrutura-do-projeto)
- [Regras de negócio](#-regras-de-negócio)
- [Como clonar e compilar a solução](#-como-clonar-e-compilar-a-solução)
- [Como executar os testes unitários](#-como-executar-os-testes-unitários)
- [Estrutura dos testes parametrizados](#-estrutura-dos-testes-parametrizados)
- [Tecnologias utilizadas](#-tecnologias-utilizadas)
- [Contribuidores](#-contribuidores)
- [Licença](#-licença)

---

## 📖 Visão geral

O **FintechBank** simula o núcleo de regras de negócio de um banco digital. A lógica está dividida em duas classes de serviço no projeto `FintechBank.App`:

| Classe | Responsabilidade |
|--------|------------------|
| `ContaCorrenteService` | Regras de conta corrente: cálculo da taxa de manutenção e validação de saques. |
| `EmprestimoService` | Regras de crédito: cálculo do valor da parcela conforme o score e aprovação por margem consignável. |

A qualidade das regras é garantida por testes unitários automatizados com **xUnit**, utilizando `[Theory]` e `[InlineData]` para validar múltiplos cenários com um único método de teste.

---

## 🛠 Requisitos técnicos

- **[.NET SDK 10.0](https://dotnet.microsoft.com/download)** (ou superior)
- **[Git](https://git-scm.com/downloads)**
- Terminal (PowerShell, CMD, Bash, Zsh etc.)
- IDE opcional: Visual Studio 2022+, VS Code (com extensão C# Dev Kit) ou JetBrains Rider

Para verificar a versão do .NET instalada:

```bash
dotnet --version
```

---

## 📂 Estrutura do projeto

```text
fintech-bank-xunit/
├── FintechBank.sln
├── FintechBank.App/
│   ├── FintechBank.App.csproj
│   ├── Program.cs
│   ├── ContaCorrenteService.cs
│   └── EmprestimoService.cs
├── FintechBank.Tests/
│   ├── FintechBank.Tests.csproj
│   ├── ContaCorrenteServiceTests.cs
│   └── EmprestimoServiceTests.cs
├── .gitignore
├── LICENSE
└── README.md
```

- **`FintechBank.App`**: projeto de console (`net10.0`) com as regras de negócio.
- **`FintechBank.Tests`**: projeto de testes xUnit (`net10.0`) que referencia o projeto `FintechBank.App`.

---

## 💼 Regras de negócio

### 1. `ContaCorrenteService`

#### `CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)`

Calcula a taxa de manutenção mensal da conta.

| Condição | Taxa |
|----------|:----:|
| Saldo maior ou igual a R$ 5.000,00 **OU** `clienteEspecial == true` | R$ 0,00 (isento) |
| Saldo entre R$ 1.000,00 e R$ 4.999,99 (cliente não especial) | R$ 12,50 |
| Saldo menor que R$ 1.000,00 (cliente não especial) | R$ 25,00 |

#### `PodeRealizarSaque(decimal saldo, decimal limiteChequeEspecial, decimal valorSaque)`

Retorna `true` **apenas** se o `valorSaque` for **menor ou igual** à soma de `saldo + limiteChequeEspecial`.

### 2. `EmprestimoService`

#### `CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)`

Aplica juros totais sobre o valor financiado conforme o score de crédito e retorna o valor de **cada parcela** (`valorComJuros / parcelas`).

| Score de crédito | Juros totais | Multiplicador |
|:----------------:|:------------:|:-------------:|
| Maior ou igual a 700 | 5% | `1.05m` |
| Entre 500 e 699 | 12% | `1.12m` |
| Menor que 500 | 20% | `1.20m` |

#### `AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)`

O empréstimo só é aprovado (`true`) se a parcela comprometer **no máximo 30%** da renda mensal, ou seja, `valorParcela <= rendaMensal * 0.30m`.

---

## 🚀 Como clonar e compilar a solução

### 1. Clonar o repositório

```bash
git clone https://github.com/KdAndrade/fintech-bank-xunit.git
```

### 2. Acessar a pasta do projeto

```bash
cd fintech-bank-xunit
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Compilar a solução

```bash
dotnet build
```

Se tudo estiver correto, a saída final indicará **Build succeeded** (compilação bem-sucedida), com 0 erros.

---

## ✅ Como executar os testes unitários

Na raiz do repositório (onde está o arquivo `FintechBank.sln`), execute:

```bash
dotnet test
```

Para uma saída mais detalhada, com o nome de cada cenário executado:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Para executar apenas o projeto de testes:

```bash
dotnet test FintechBank.Tests/FintechBank.Tests.csproj
```

Ao final da execução, **100% dos testes de ambas as suítes** devem ser aprovados, com uma saída semelhante a:

```text
Aprovado!  – Com falha: 0, Aprovado: 11, Ignorado: 0, Total: 11
```

---

## 🧪 Estrutura dos testes parametrizados

Os testes utilizam os atributos do xUnit:

- **`[Theory]`**: marca um método de teste que roda várias vezes, uma para cada conjunto de dados.
- **`[InlineData(...)]`**: define cada conjunto de parâmetros de entrada e o resultado esperado, evitando a duplicação de métodos de teste.

São **4 métodos de teste** distribuídos em **2 classes**, que cobrem **11 cenários** no total.

### `ContaCorrenteServiceTests`

#### Teste 1: Taxa de manutenção (`CalcularTaxaManutencao`)

| Saldo | Cliente especial | Resultado esperado | Cenário |
|:-----:|:----------------:|:------------------:|---------|
| 6000.00 | `false` | 0.00 | Saldo alto → isento |
| 500.00 | `true` | 0.00 | Cliente especial → isento |
| 2500.00 | `false` | 12.50 | Saldo intermediário → R$ 12,50 |
| 300.00 | `false` | 25.00 | Saldo baixo → R$ 25,00 |

#### Teste 2: Validação de saque (`PodeRealizarSaque`)

| Saldo | Limite cheque especial | Valor do saque | Resultado esperado | Cenário |
|:-----:|:----------------------:|:--------------:|:------------------:|---------|
| 1000.00 | 500.00 | 1200.00 | `true` | Dentro do limite total |
| 1000.00 | 500.00 | 1600.00 | `false` | Excede saldo + limite |

### `EmprestimoServiceTests`

#### Teste 1: Cálculo de parcela com juros por score (`CalcularValorParcela`)

| Valor total | Parcelas | Score | Resultado esperado | Cenário |
|:-----------:|:--------:|:-----:|:------------------:|---------|
| 10000.00 | 10 | 800 | 1050.00 | Score alto: 5% → R$ 10.500 / 10 |
| 10000.00 | 10 | 600 | 1120.00 | Score médio: 12% → R$ 11.200 / 10 |
| 10000.00 | 10 | 400 | 1200.00 | Score baixo: 20% → R$ 12.000 / 10 |

#### Teste 2: Aprovação por margem consignável (`AprovarEmprestimo`)

| Renda mensal | Valor da parcela | Resultado esperado | Cenário |
|:------------:|:----------------:|:------------------:|---------|
| 5000.00 | 1500.00 | `true` | Parcela = 30% da renda → aprovado |
| 5000.00 | 1600.00 | `false` | Parcela > 30% da renda → recusado |

---

## 🧰 Tecnologias utilizadas

- [C#](https://learn.microsoft.com/dotnet/csharp/)
- [.NET 10](https://dotnet.microsoft.com/)
- [xUnit](https://xunit.net/)
- [Git](https://git-scm.com/) e [GitHub](https://github.com/)

---

## 👥 Contribuidores

Os contribuidores do projeto podem ser visualizados na [página de contribuidores do GitHub](https://github.com/KdAndrade/fintech-bank-xunit/graphs/contributors). 

---

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
