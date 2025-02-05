using LogIntelligence.Client;
using LogIntelligence.Client.Requests;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace LogIntelligence.AspNetCore
{
    public class LogQueue : ILogQueue
    {
        private readonly ConcurrentQueue<CreateLogMessageRequest> _logEntries = new();
        private const int _maxCapacity = 1000;

        public void Enqueue(CreateLogMessageRequest logEntry)
        {
            ArgumentNullException.ThrowIfNull(logEntry);

            if (_logEntries.Count >= _maxCapacity)
            {
                return;
            }

            _logEntries.Enqueue(logEntry);
        }

        public bool TryDequeue(out CreateLogMessageRequest? logEntry)
        {
            return _logEntries.TryDequeue(out logEntry);
        }
    }
}
