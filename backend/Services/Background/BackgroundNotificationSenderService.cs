
using System.Text.Json;
using FirebaseAdmin.Messaging;
using JobNet.Interfaces.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JobNet.Services.Background;
public class BackgroundNotificationSenderService : BackgroundService
{
    const string _queueName = "Notifications";
    private readonly IModel _model;
    private readonly IConnection _connection;
    private readonly IFirebaseCloudNotificationService _notificationSenderService;
    private readonly ILogger<BackgroundNotificationSenderService> _logger;

    public BackgroundNotificationSenderService(ILogger<BackgroundNotificationSenderService> logger, IRabbitMqService rabbitMqService, IFirebaseCloudNotificationService notificationSenderService)
    {
        _connection = rabbitMqService.CreateConnection();
        _model = _connection.CreateModel();
        _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare("NotificationExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind(_queueName, "NotificationExchange", string.Empty);
        _notificationSenderService = notificationSenderService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                string textMessage = System.Text.Encoding.UTF8.GetString(body);
                MulticastMessage? message = JsonSerializer.Deserialize<MulticastMessage>(textMessage) ?? throw new Exception();
                await _notificationSenderService.SendMulticastMessageAsync(message);
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }
    public override void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}