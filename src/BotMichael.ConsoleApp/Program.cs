using BotMichael.ConsoleApp;
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
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (!string.IsNullOrWhiteSpace(env) &&
                File.Exists(Path.Combine(AppContext.BaseDirectory, $"appsettings.{env}.json")))
            {
                config.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
            }

            config.AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            services.AddOptions();
            services.AddHostedService<Daemon>();
        })
        .ConfigureLogging((context, logging) =>
        {
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            logging.AddConsole();
        });