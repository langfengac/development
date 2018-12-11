using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config)=> {
                config.SetBasePath(Directory.GetCurrentDirectory());
                //config.AddJsonFile("json_array.json", optional: false, reloadOnChange: false);
                //config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
                //config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);
                config.AddCommandLine(args);
            })
                .UseStartup<Startup>()
                .Build();
    }
}
