using LogIntelligence.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLogIntelligence(this IServiceCollection services) 
        {
            services.AddHttpClient<LogIntelligenceClient>();
            return services;
        }

        public static IServiceCollection AddLogIntelligence(this IServiceCollection services, IOptions<LogIntelligenceOptions> options)
        {
            services.AddHttpClient<LogIntelligenceClient>();
            return services;
        }

        public static IServiceCollection AddLogIntelligence(this IServiceCollection services, Action<LogIntelligenceOptions> options)
        {
            services.AddHttpClient<LogIntelligenceClient>();
            return services;
        }
    }
}
