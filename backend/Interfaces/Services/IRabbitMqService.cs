using RabbitMQ.Client;

namespace JobNet.Interfaces.Services;

public interface IRabbitMqService
{
    IConnection CreateConnection();
}