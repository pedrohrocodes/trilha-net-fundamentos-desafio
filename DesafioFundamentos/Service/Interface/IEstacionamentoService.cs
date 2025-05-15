using DesafioFundamentos.Models;

namespace DesafioFundamentos.Service.Interface;

public interface IEstacionamentoService
{
    Task CriarVeiculoAsync(decimal precoInicial, decimal precoPorHora);
    Task ListarVeiculosAsync();
    Task<bool> UpdateAsync(Estacionamento estacionamento);
    Task RemoverVeiculoAsync();

    Task ManageCarParing();
}