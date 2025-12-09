using App.Interfaces.IQueueService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.HostedServices;

public class BackgroundWorkerService : BackgroundService
{

    private readonly IBackgroundTaskQueue _backgroundTaskQueue;
    private readonly ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(IBackgroundTaskQueue backgroundTaskQueue, ILogger<BackgroundWorkerService> logger)
    {
        _backgroundTaskQueue = backgroundTaskQueue;
        _logger = logger;
    }
    
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background worker started.");
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _backgroundTaskQueue.DequeueAsync(stoppingToken);

            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing work item.");
            }
        }
    }
}