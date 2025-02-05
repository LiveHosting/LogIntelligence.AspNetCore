using LogIntelligence.Client;
using LogIntelligence.Client.Requests;

namespace LogIntelligence.AspNetCore
{
    public interface ILogQueue
    {
        void Enqueue(CreateLogMessageRequest logEntry);
        bool TryDequeue(out CreateLogMessageRequest? logEntry);
    }
}
