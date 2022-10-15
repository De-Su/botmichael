using BotMichael.ConsoleApp;
using Microsoft.Extensions.Configuration;

using var cts = new CancellationTokenSource();
var env = Environment.GetEnvironmentVariable("BOT_ENVIRONMENT");

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");

if (!string.IsNullOrWhiteSpace(env) && File.Exists(Path.Combine(AppContext.BaseDirectory, $"appsettings.{env}.json" )))
{
    builder.AddJsonFile($"appsettings.{env}.json");
}

var config = builder.AddEnvironmentVariables().Build();

var settings = config.GetRequiredSection("BotSettings").Get<BotSettings>();

await Bot.Start(settings.Token, cts.Token);

cts.Cancel();