using DesafioFundamentos.Models;

namespace DesafioFundamentos.Repository.Interface;

public interface IVeiculoRepository
{
    Task<bool> CreateAsync(Veiculo customer);

    Task<Veiculo?> GetAsync(Guid id);

    Task<IEnumerable<Veiculo>> GetAllAsync();

    Task<bool> UpdateAsync(Veiculo customer);

    Task<bool> DeleteAsync(Guid id);
}