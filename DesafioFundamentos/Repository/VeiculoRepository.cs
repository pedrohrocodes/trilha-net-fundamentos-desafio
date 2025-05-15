using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DesafioFundamentos.Models;
using DesafioFundamentos.Repository.Interface;

namespace DesafioFundamentos.Repository;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly string _tableName = "cars"; //Table name we created in Dynamo DB

    public VeiculoRepository(IAmazonDynamoDB dynamoDB)
    {
        _dynamoDB = dynamoDB;
    }

    public async Task<bool> CreateAsync(Veiculo veiculo)
    {
        veiculo.UpdatedAt = DateTime.UtcNow;
        var veiculoAsJson = JsonSerializer.Serialize(veiculo);
        var veiculoAsAttributes = Document.FromJson(veiculoAsJson).ToAttributeMap();

        var createItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = veiculoAsAttributes
        };

        var response = await _dynamoDB.PutItemAsync(createItemRequest);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<Veiculo?> GetAsync(Guid id)
    {
        var getItemRequest = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDB.GetItemAsync(getItemRequest);

        if (response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);

        return JsonSerializer.Deserialize<Veiculo>(itemAsDocument.ToJson());
    }

    public async Task<IEnumerable<Veiculo>> GetAllAsync()
    {
        var scanRequest = new ScanRequest
        {
            TableName = _tableName,
        };

        var response = await _dynamoDB.ScanAsync(scanRequest);

        return response.Items.Select(x =>
        {
            var json = Document.FromAttributeMap(x).ToJson();
            return JsonSerializer.Deserialize<Veiculo>(json);
        });
    }

    public async Task<bool> UpdateAsync(Veiculo veiculo)
    {
        veiculo.UpdatedAt = DateTime.UtcNow;
        var veiculoAsJson = JsonSerializer.Serialize(veiculo);
        var veiculoAsAttributes = Document.FromJson(veiculoAsJson).ToAttributeMap();

        var updateItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = veiculoAsAttributes
        };

        var response = await _dynamoDB.PutItemAsync(updateItemRequest);

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDB.DeleteItemAsync(deleteItemRequest);
        
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}