using System.ComponentModel.DataAnnotations;
using DesafioFundamentos.Models;
using DesafioFundamentos.Repository.Interface;
using DesafioFundamentos.Service.Interface;

namespace DesafioFundamentos.Service;

public class EstacionamentoService : IEstacionamentoService
{
    private readonly IEstacionamentoRepository _estacionamentoRepository;

    public EstacionamentoService(IEstacionamentoRepository estacionamentoRepository)
    {
        _estacionamentoRepository = estacionamentoRepository;
    }

    public async Task CriarVeiculoAsync(decimal precoInicial, decimal precoPorHora)
    {
        Console.WriteLine("Digite a placa do veículo para estacionar:");
        var placa = Console.ReadLine();

        var existingEstacionamento = await _estacionamentoRepository.GetByPlacaAsync(placa);

        if (existingEstacionamento is null)
        {
            var estacionamento = new Estacionamento(precoInicial, precoPorHora, placa);

            await _estacionamentoRepository.CreateAsync(estacionamento);
        }

        var message = $"A car with id {existingEstacionamento.Id} already exists";
        throw new ValidationException(message);
    }

    public async Task ListarVeiculosAsync()
    {
        var estacionamento = await _estacionamentoRepository.GetAllAsync();

        // Verifica se há veículos no estacionamento
        if (estacionamento.Any())
        {
            Console.WriteLine("Os veículos estacionados são:");
            foreach (var veiculo in estacionamento)
            {
                Console.WriteLine(veiculo.PlacaVeiculo);
            }
        }
        else
        {
            Console.WriteLine("Não há veículos estacionados.");
        }
    }

    public async Task<bool> UpdateAsync(Estacionamento existingEstacionamento)
    {
        return await _estacionamentoRepository.UpdateAsync(existingEstacionamento);
    }

    public async Task RemoverVeiculoAsync()
    {
        Console.WriteLine("Digite a placa do veículo para remover:");
        var placa = Console.ReadLine();

        // Verifica se o veículo existe
        var estacionamento = await _estacionamentoRepository.GetByPlacaAsync(placa);

        if (estacionamento is not null)
        {
            Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

            var horas = int.Parse(Console.ReadLine());
            var valorTotal = estacionamento.PrecoInicial + estacionamento.PrecoPorHora * horas;

            var success = await _estacionamentoRepository.DeleteAsync(estacionamento.PlacaVeiculo);

            var message = success
                ? $"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}"
                : $"Não foi possível remover o veículo {placa}";

            Console.WriteLine(message);
        }
        else
        {
            Console.WriteLine(
                "Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
        }
    }

    public async Task ManageCarParing()
    {
        // Coloca o encoding para UTF8 para exibir acentuação
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Seja bem vindo ao sistema de estacionamento (com AWS DynamoDB)!\n" +
                          "Digite o preço inicial:");
        var precoInicial = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Agora digite o preço por hora:");
        var precoPorHora = Convert.ToDecimal(Console.ReadLine());
        
        var exibirMenu = true;

        // Realiza o loop do menu
        while (exibirMenu)
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");


            switch (Console.ReadLine())
            {
                case "1":
                    await CriarVeiculoAsync(precoInicial, precoPorHora);
                    break;

                case "2":
                    await RemoverVeiculoAsync();
                    break;

                case "3":
                    await ListarVeiculosAsync();
                    break;

                case "4":
                    exibirMenu = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadLine();
        }

        Console.WriteLine("O programa se encerrou");
    }
}