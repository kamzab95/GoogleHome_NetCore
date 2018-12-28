using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ClientApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5002); //IPAddress.Loopback, 6000);  // http:localhost:5000
                options.Listen(IPAddress.Any, 5003, listenOptions =>
                {
                    listenOptions.UseHttps("https/certificate.pfx", "password");
                });
            })
            .UseSetting("https_port", "443")
            .UseStartup<Startup>();
    }
}
