using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutoface.Model;

namespace WebAutoface.Middleware
{
    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecretSettings _settings;

        public HelloWorldMiddleware(RequestDelegate next, IOptions<SecretSettings> options)
        {
            _next = next;
            _settings = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var jsonSettings = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            //await context.Response.WriteAsync(jsonSettings);
            await _next(context);
            //context.Response.Cookies.Append("jim", "nimahoe");


        }
    }


    public static class UseHelloWorldInClassExtensions
    {
        public static IApplicationBuilder UseHelloWorld(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HelloWorldMiddleware>();
        }
    }
}
