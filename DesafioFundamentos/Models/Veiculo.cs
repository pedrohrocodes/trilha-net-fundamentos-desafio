using System.Text.Json.Serialization;

namespace DesafioFundamentos.Models;

public class Veiculo
{
    [JsonPropertyName("pk")]
    public string Pk => Id.ToString();

    [JsonPropertyName("sk")]
    public string SK => Id.ToString();
    
    public Guid Id { get; init; } = Guid.Empty!;

    public string Placa { get; set; } = string.Empty!;
    
    public DateTime UpdatedAt { get; set; }
}