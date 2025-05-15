using Amazon.DynamoDBv2;
using DesafioFundamentos.Repository;
using DesafioFundamentos.Repository.Interface;
using DesafioFundamentos.Service;
using DesafioFundamentos.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Registro de dependências
services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
services.AddSingleton<IEstacionamentoRepository, EstacionamentoRepository>();
services.AddSingleton<IEstacionamentoService, EstacionamentoService>();

var provider = services.BuildServiceProvider();

var service = provider.GetRequiredService<IEstacionamentoService>();

await service.ManageCarParing();