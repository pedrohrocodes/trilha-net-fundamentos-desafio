using DesafioFundamentos.Models;

namespace DesafioFundamentos.Repository.Interface;

public interface IEstacionamentoRepository
{
    Task<bool> CreateAsync(Estacionamento estacionamento);
    
    Task<Estacionamento?> GetByPlacaAsync(string placa);

    Task<IEnumerable<Estacionamento>> GetAllAsync();

    Task<bool> UpdateAsync(Estacionamento veiculo);

    Task<bool> DeleteAsync(string placa);
}