using Microsoft.AspNetCore.Builder;
using LogIntelligence.AspNetCore.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLogIntelligence(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            return app.UseMiddleware<LogIntelligenceMiddileware>();
        }
    }
}
