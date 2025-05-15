# Gerenciador de Estacionamento - Console App (.NET)

Aplicativo de console desenvolvido em .NET para gerenciamento de um estacionamento. Permite registrar entrada e saída de veículos, calcular o tempo de permanência e o valor final a ser cobrado.
Desenvolvido para o desafio da DIO - Trilha .NET - Fundamentos

## 🛠 Funcionalidades

- Registro de entrada e saída de veículos  
- Cálculo automático de tempo estacionado e valor total 
- Persistência de dados com **AWS DynamoDB**  
- Arquitetura baseada em **Injeção de Dependência (DI)**  
- Integração com **AWS CLI** para configuração do ambiente

## 💻 Tecnologias Utilizadas

- .NET 6 ou superior
- C#
- AWS DynamoDB
- AWS CLI
- Programação Orientada a Objetos (POO)
- Injeção de Dependência (Microsoft.Extensions.DependencyInjection)

## ⚙️ Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/)
- Conta AWS com permissões para uso do DynamoDB
- [AWS CLI instalado e configurado](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)

## 🚀 Como executar

1. Clone o repositório:
  ```bash
  git clone https://github.com/pedrohrocodes/estacionamento-console-app.git
  cd trilha-net-fundamentos-desafio/DesafioFundamentos
  ```

2. Configure suas credenciais AWS via CLI:
  ```bash
  aws configure
  ```

3. Restaure e execute o projeto:
  ```bash
  dotnet restore
  dotnet run
  ```

## 📁 Estrutura do Projeto
   ```pgsql
    /DesafioFundamentos
    │
    ├── Models/
    ├── Services/
    ├── Repositories/
    ├── Program.cs
   ```

## 📌 Observações

- A tabela no DynamoDB será criada automaticamente se não existir (configurável).
- O cálculo do valor pode ser facilmente adaptado para diferentes regras (por hora, fração, diária etc).

## 📄 Licença
Este projeto está sob a licença MIT.

