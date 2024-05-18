
using Azure.Storage.Blobs.Models;
using JobNet.Interfaces.Services;
using JobNet.Settings;
using Microsoft.Extensions.Options;

namespace JobNet.Services.Background;
public class BackgroundScaleTokensRemover : BackgroundService
{
    private readonly ILogger<BackgroundScaleTokensRemover> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval;
    public BackgroundScaleTokensRemover(IServiceProvider serviceProvider, ILogger<BackgroundScaleTokensRemover> logger)
    {
        _logger = logger;
        _interval = TimeSpan.FromHours(24);
        // _interval = TimeSpan.FromSeconds(30);
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Recurring BackgroundScaleTokensRemover is starting.");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<ICloudMessageTokenHandlerService>();
                    _logger.LogInformation("Recurring BackgroundScaleTokensRemover is working. Time: {time}", DateTimeOffset.Now);
                    await scopedService.RemoveStaleTokensAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing background work.");
            }

            await Task.Delay(_interval, stoppingToken);
        }


        _logger.LogInformation("Recurring BackgroundScaleTokensRemover is stopping.");
    }
}