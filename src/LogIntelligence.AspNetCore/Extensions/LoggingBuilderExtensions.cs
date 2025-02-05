using LogIntelligence.Client;
using LogIntelligence.Client.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.AspNetCore.Extensions
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddLogIntelligence(this ILoggingBuilder builder, Action<LogIntelligenceOptions> configure)
        {
            // Register the LogIntelligenceClient
            builder.Services.AddLogIntelligenceClient(configure);

            // Register log queue as Singleton
            builder.Services.AddSingleton<ILogQueue, LogQueue>();

            // Register the CustomLoggerProvider as a Singleton
            builder.Services.AddSingleton<LogIntelligenceLoggerProvider>();

            // Register the hosted service for log processing
            builder.Services.AddHostedService<LogProcessingService>();

            // Register IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // Configure logging to use the custom provider
            builder.Services.AddSingleton<ILoggerProvider>(serviceProvider =>
            {
                var logQueue = serviceProvider.GetRequiredService<ILogQueue>();
                var logOptions = serviceProvider.GetRequiredService<IOptions<LogIntelligenceOptions>>();
                //var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return new LogIntelligenceLoggerProvider(logQueue, logOptions);
            });

            return builder;
        }
    }
}
