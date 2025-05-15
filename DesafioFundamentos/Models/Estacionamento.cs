using System.Text.Json.Serialization;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        [JsonPropertyName("pk")]
        public string PK => PlacaVeiculo;
        [JsonPropertyName("sk")]
        public string SK => PlacaVeiculo;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PlacaVeiculo { get; set; }
        public decimal PrecoInicial { get; set; }
        public decimal PrecoPorHora { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Estacionamento()
        {
            
        }

        public Estacionamento(decimal precoInicial, decimal precoPorHora, string placa)
        {
            this.PrecoInicial = precoInicial;
            this.PrecoPorHora = precoPorHora;
            this.PlacaVeiculo = placa;
        }
    }
}
