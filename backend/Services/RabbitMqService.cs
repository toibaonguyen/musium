
using JobNet.Interfaces.Services;
using JobNet.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace JobNet.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration;
    public RabbitMqService(IOptions<RabbitMqConfiguration> options)
    {
        _configuration = options.Value;
    }
    public IConnection CreateConnection()
    {
        ConnectionFactory factory = new()
        {
            UserName = _configuration.Username,
            Password = _configuration.Password,
            HostName = _configuration.HostName,
            DispatchConsumersAsync = true
        };
        return factory.CreateConnection();
    }
}