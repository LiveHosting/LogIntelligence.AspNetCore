using LogIntelligence.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogIntelligence.AspNetCore
{
    public class LogIntelligenceLoggerProvider : ILoggerProvider
    {
        private readonly ILogQueue _logQueue;
        private readonly IOptions<LogIntelligenceOptions> _options;
        public LogIntelligenceLoggerProvider(ILogQueue logQueue, IOptions<LogIntelligenceOptions> options)
        {
            _logQueue = logQueue;
            _options=options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new LogIntelligenceLogger(_logQueue, categoryName, _options.Value);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
