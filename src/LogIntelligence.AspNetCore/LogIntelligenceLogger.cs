using LogIntelligence.Client;
using LogIntelligence.Client.Requests;
using Microsoft.Extensions.Logging;
using System.Reflection;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace LogIntelligence.AspNetCore
{
    public class LogIntelligenceLogger : ILogger
    {
        private readonly ILogQueue _logQueue;
        private readonly string _categoryName;
        private readonly LogIntelligenceOptions _options;

        public LogIntelligenceLogger(ILogQueue logQueue, string categoryName, LogIntelligenceOptions options)
        {
            _logQueue = logQueue;
            _categoryName = categoryName;
            _options = options;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return new LoggerScope<TState>(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Warning;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message) && exception == null)
            {
                return;
            }

            var logEntry = new CreateLogMessageRequest
            {
                
                Title = "INSERT TITLE HERE SOMEHOW",
                Timestamp = DateTime.UtcNow,
                ApplicationName= _options.ApplicationName,
                LogID= _options.LogID,
                Message = message,
                EventID = eventId.Id,
                EventName = eventId.Name,
                MachineName = Environment.MachineName,
                LogLevel=Client.LogLevel.Information,
                Version= Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown",
                //EventId = eventId.Id,
                //Message = message,
                //Exception = exception?.ToString(),
                Category = _categoryName,
                Source=exception?.GetBaseException().StackTrace
            };

            _logQueue.Enqueue(logEntry);
        }

        private class LoggerScope<TState> : IDisposable
        {
            private readonly TState _state;

            public LoggerScope(TState state)
            {
                _state = state;
            }

            public void Dispose()
            {
                // Clean up scope if necessary
            }
        }
    }
}
