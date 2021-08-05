using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using CommandLine;

namespace WebApiServer
{
    public class Program
    {
        private static IConfigurationRoot _configurationRoot;
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
                        .AddJsonFile("appsettings.json", true, true);

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

                    configBuilder.AddJsonFile(_environmentalAppsettings, true, true);
                    Console.WriteLine($"TDSWebAPI Environment : {option.Environment}");
                }

                _configurationRoot = configBuilder.Build();
                var dbConfig = _configurationRoot.GetSection("Database");
                ServerConst.RegistDBConfig(DB.ACCOUNT, dbConfig.GetValue<string>("Account"));
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
