using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DesafioFundamentos.Models;
using DesafioFundamentos.Repository.Interface;

namespace DesafioFundamentos.Repository;

public class EstacionamentoRepository : IEstacionamentoRepository
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly string _tableName = "car_parking"; // Table name created in Dynamo DB

    public EstacionamentoRepository(IAmazonDynamoDB dynamoDB)
    {
        _dynamoDB = dynamoDB;
    }

    public async Task<bool> CreateAsync(Estacionamento estacionamento)
    {
        estacionamento.UpdatedAt = DateTime.UtcNow;
        var estacionamentoAsJson = JsonSerializer.Serialize(estacionamento);
        var estacionamentoAsAttributes = Document.FromJson(estacionamentoAsJson).ToAttributeMap();

        var createItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = estacionamentoAsAttributes
        };

        var response = await _dynamoDB.PutItemAsync(createItemRequest);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<Estacionamento> GetByPlacaAsync(string placa)
    {
        var getItemRequest = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = placa } },
                { "sk", new AttributeValue { S = placa } }
            }
        };

        var response = await _dynamoDB.GetItemAsync(getItemRequest);

        if (response.Item is null || response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);

        return JsonSerializer.Deserialize<Estacionamento>(itemAsDocument.ToJson());
    }

    public async Task<IEnumerable<Estacionamento>> GetAllAsync()
    {
        var scanRequest = new ScanRequest
        {
            TableName = _tableName,
        };

        var response = await _dynamoDB.ScanAsync(scanRequest);
        
        return response.Items.Select(x =>
        {
            var json = Document.FromAttributeMap(x).ToJson();
            return JsonSerializer.Deserialize<Estacionamento>(json);
        });
    }

    public async Task<bool> UpdateAsync(Estacionamento estacionamento)
    {
        estacionamento.UpdatedAt = DateTime.UtcNow;
        var estacionamentoAsJson = JsonSerializer.Serialize(estacionamento);
        var estacionamentoAsAttributes = Document.FromJson(estacionamentoAsJson).ToAttributeMap();

        var updateItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = estacionamentoAsAttributes
        };

        var response = await _dynamoDB.PutItemAsync(updateItemRequest);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAsync(string placa)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = placa } },
                { "sk", new AttributeValue { S = placa } }
            }
        };

        var response = await _dynamoDB.DeleteItemAsync(deleteItemRequest);
        
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}