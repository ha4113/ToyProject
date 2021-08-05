using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;

namespace WebApiServer
{
    public class Program
    {
        public static IConfigurationRoot ConfigurationRoot { get; private set; }
        private static string _environmentalAppsettings;
        private static bool _isPlatformWindows;
        
        public static void Main(string[] args)
        {
            

            try
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

                var configBuilder =
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                StartOption option = null;
                Parser.Default.ParseArguments<StartOption>(args).WithParsed(o => option = o ?? new StartOption());
                _isPlatformWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
                if (option == null)
                {
                    throw new Exception($"Invalid option({args})");
                }

                if (!string.IsNullOrEmpty(option.Environment))
                {
                    _environmentalAppsettings = $"appsettings.{option.Environment}.json";

                    configBuilder.AddJsonFile(_environmentalAppsettings, optional: true, reloadOnChange: true);
                    Console.WriteLine($"TDSWebAPI Environment : {option.Environment}");
                }

                ConfigurationRoot = configBuilder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (_isPlatformWindows)
                {
                    Console.WriteLine("Press any key End....");
                    Console.ReadKey();
                }
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
