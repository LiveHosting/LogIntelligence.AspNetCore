using LogIntelligence.Client;
using LogIntelligence.Client.Requests;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LogIntelligence.AspNetCore
{
    public class LogProcessingService : BackgroundService
    {
        private readonly ILogQueue _logQueue;
        private readonly LogIntelligenceClient _logIntelligenceClient;
        private readonly ILogger<LogProcessingService> _logger;

        public LogProcessingService(ILogQueue logQueue, LogIntelligenceClient logIntelligenceClient, ILogger<LogProcessingService> logger)
        {
            _logQueue = logQueue;
            _logIntelligenceClient = logIntelligenceClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logQueue.TryDequeue(out CreateLogMessageRequest? logEntry))
                {
                    if (logEntry!=null)
                    {
                        try
                        {
                            await _logIntelligenceClient.SendLogMessageAsync(logEntry);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to send log entry.");
                            // Optionally, re-enqueue the log entry or handle it accordingly.
                        }
                    }
                }
                else
                {
                    await Task.Delay(1000, stoppingToken); // Adjust delay as needed
                }
            }
        }
    }
}
