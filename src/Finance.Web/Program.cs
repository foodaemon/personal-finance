using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Finance.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) 
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                                 .ConfigureLogging(ConfigureLogger)
                                 .UseStartup<Startup>()
                                 .Build();
            return builder;
        }

        static void ConfigureLogger(WebHostBuilderContext context, ILoggingBuilder logging) 
        {
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
        }
    }
}
