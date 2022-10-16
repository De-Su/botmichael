using BotMichael.ConsoleApp;
using BotMichael.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = CreateHostBuilder();
await builder.RunConsoleAsync();

IHostBuilder CreateHostBuilder() =>
    new HostBuilder()
        .ConfigureAppConfiguration((context, config) =>
        {
            var env = Environment.GetEnvironmentVariable("BOT_ENVIRONMENT");
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            services.AddOptions();
            services.AddOptions<BotSettings>().Bind(context.Configuration.GetSection("BotSettings"));
            services.AddHostedService<Worker>();
            services.AddCoreServices();
        })
        .ConfigureLogging((context, logging) =>
        {
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            logging.AddConsole();
        });