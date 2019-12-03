using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Pgcode
{
    public static class Program
    { 
#if DEBUG
        public static bool IsDebug { get; set; } = true;
#else
        public static bool IsDebug { get; set; } = false;
#endif
        public static IWebHostEnvironment Environment { get; set; }
        public static Settings Settings { get; set; }

        public static async Task Main(string[] args)
        {
            if (ArgsInclude(args, "-h", "--help"))
            {
                PrintHelp();
                return;
            }

            PrintStartMessages();

            //
            // see source code for default config
            // https://github.com/aspnet/AspNetCore/tree/master/src/DefaultBuilder/src
            //

            var builder = new WebHostBuilder();
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddCommandLine(args)
                .AddEnvironmentVariables("PGCODE_");

            Settings = new Settings();
            var config = configBuilder.Build();
            //config.GetConnectionString()
            config.Bind(Settings);

            var url = $"http://{Settings.Address}:{Settings.Port}";

            builder
                .UseSetting("URLS", url)
                .ConfigureAppConfiguration((ctx, appConfigBuilder) =>
                {
                    var env = ctx.HostingEnvironment;
                    env.EnvironmentName = IsDebug ? "Development" : "Production";
                    Environment = env;
                    foreach (var source in configBuilder.Sources)
                    {
                        appConfigBuilder.Add(source);
                    }
                })
                .ConfigureLogging((ctx, logging) => logging.AddConsole()
                    .AddFilter("Microsoft", LogLevel.Information).AddFilter("System", LogLevel.Information)
                    .AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning))
                .UseKestrel()
                .ConfigureServices((ctx, services) => services.AddRouting())
                .SuppressStatusMessages(true)
                .CaptureStartupErrors(true)
                .UseStartup<Startup>();

            var host = builder.Build();

            if (Environment.IsDevelopment())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Development build, serving from: {0}", DevelopmentMiddleware.DevelopmentPath);
                Console.ResetColor();
            }

            if (!PrintAvailableUrlsFromHost(host))
            {
                return;
            }

            Console.WriteLine("Hit CTRL-C to stop the server");

            if (ArgsInclude(args, "-o", "--open") || Environment.IsDevelopment())
            {
                OpenDefaultBrowser(url);
            }
            await host.RunAsync();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("");
            Console.WriteLine("Usage: pgcode [options]");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  --port=[port]");
            Console.ResetColor();
            Console.WriteLine("              Port to use [5000]");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  --address=[address]");
            Console.ResetColor();
            Console.WriteLine("        Address to use [localhost]");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  --connection=[connection]");
            Console.ResetColor();
            Console.WriteLine("  Connection name to use in this instance");

            Console.WriteLine("");
            Console.WriteLine("Additional options:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  -o --open");
            Console.ResetColor();
            Console.WriteLine("                  Try to open default browser window after starting the server");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  -h --help");
            Console.ResetColor();
            Console.WriteLine("                  Print this list and exit.");

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Note: ");

            Console.WriteLine("Key/values above can be set in appsettings.json file in root folder. Also, as environment variables with PGCODE_ prefix.");
            Console.WriteLine("For multiple configuration sources, order of precedence is: 1) environment variables 2) command line arguments 3) configuration file.");
            Console.WriteLine("");
            Console.ResetColor();
        }

        private static void PrintStartMessages()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Starting up pgcode ...");
            Console.ResetColor();
        }

        private static bool PrintAvailableUrlsFromHost(IWebHost host)
        {
            var address = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            if (address == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: listening port is not configured properly");
                Console.ResetColor();
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Listening on: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(address);
            Console.ResetColor();
            return true;
        }

        private static void OpenDefaultBrowser(string url)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Launching default browser...");
            Console.ResetColor();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //
                // TODO: Unhandled exception. System.ComponentModel.Win32Exception (2): No such file or directory
                //
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
        }

        private static bool ArgsInclude(string[] args, params string[] values)
        {
            var lower = values.Select(v => v.ToLower()).ToList();
            var upper = values.Select(v => v.ToUpper()).ToList();
            foreach (var arg in args)
            {
                if (lower.Contains(arg))
                {
                    return true;
                }
                if (upper.Contains(arg))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Services.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCookieMiddleware();
            if (env.IsDevelopment())
            {
                app.UseDevelopmentMiddleware();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseResourceMiddleware();
            }
        }
    }
}
