
using System.Text.Json;
using Azure.Storage.Blobs.Models;
using JobNet.Contants;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Common;
using JobNet.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JobNet.Services.Background;
public class BackgroundEmailSenderService : BackgroundService
{
    const string _queueName = "Email";
    private readonly IModel _model;
    private readonly IConnection _connection;
    private readonly IEmailSenderService _emailSenderService;
    private readonly ILogger<BackgroundEmailSenderService> _logger;

    public BackgroundEmailSenderService(ILogger<BackgroundEmailSenderService> logger, IRabbitMqService rabbitMqService, IEmailSenderService emailSenderService)
    {
        _connection = rabbitMqService.CreateConnection();
        _model = _connection.CreateModel();
        _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare("EmailExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind(_queueName, "EmailExchange", string.Empty);
        _emailSenderService = emailSenderService;
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
                EmailMessageBrokerModel? message = JsonSerializer.Deserialize<EmailMessageBrokerModel>(textMessage) ?? throw new Exception();
                switch (message.Type)
                {
                    case EmailTypes.ResetPasswordConfirmation:
                        await _emailSenderService.SendResetPasswordConfirmationAsync(message.ToEmail, message.Content);
                        break;
                    case EmailTypes.NewPassword:
                        await _emailSenderService.SendNewResetPasswordEmailAsync(message.ToEmail, message.Content);
                        break;
                    case EmailTypes.EmailVerification:
                        await _emailSenderService.SendEmailVerificationAsync(message.ToEmail, message.Content);
                        break;
                }
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