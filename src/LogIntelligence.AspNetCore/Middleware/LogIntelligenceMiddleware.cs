using LogIntelligence.Client;
using LogIntelligence.Client.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.AspNetCore.Middleware
{
    public class LogIntelligenceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogQueue _logQueue;
        private readonly LogIntelligenceOptions _options;

        public LogIntelligenceMiddleware(RequestDelegate next, ILogQueue logQueue, IOptions<LogIntelligenceOptions> options)
        {
            _next = next;
            _logQueue = logQueue;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                CreateLogMessageRequest createMessageRequest = new CreateLogMessageRequest
                {
                    ApplicationName = _options.ApplicationName,
                    LogID=_options.LogID,
                    LogLevel = LogLevel.Error,
                    Title = exception.Message,
                    MachineName = Environment.MachineName,
                    Message = exception.GetBaseException().Message,
                    Timestamp = DateTime.UtcNow,
                    Version= Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown",
                    Source=exception.GetBaseException().Source
                };

                _logQueue.Enqueue(createMessageRequest);
                
                throw;
            }
        }
    }
}
