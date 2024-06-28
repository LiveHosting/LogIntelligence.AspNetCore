using LogIntelligence.PublicApi.Client;
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
            services.AddHttpClient<LogIntelligenceApiClient>();
            return services;
        }

        public static IServiceCollection AddLogIntelligence(this IServiceCollection services, IOptions<LogIntelligenceApiClientOptions> options)
        {
            services.AddHttpClient<LogIntelligenceApiClient>();
            return services;
        }

        public static IServiceCollection AddLogIntelligence(this IServiceCollection services, Action<LogIntelligenceApiClientOptions> options)
        {
            services.AddHttpClient<LogIntelligenceApiClient>();
            return services;
        }
    }
}
