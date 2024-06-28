using LogIntelligence.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.AspNetCore.Middleware
{
    public class LogIntelligenceMiddileware
    {
        private readonly RequestDelegate next;
        private readonly LogIntelligenceOptions options;

        public LogIntelligenceMiddileware(RequestDelegate Next, IOptions<LogIntelligenceOptions> Options)
        {
            this.next = Next;
            this.options = Options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception exception)
            {
                MessageShipper.Ship(exception, exception.GetBaseException().Message, context, options, queue);
                throw;
            }
        }
    }
}
