await new HostBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = Environment.GetEnvironmentVariable("BOT_ENVIRONMENT");
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(context.Configuration)
            .CreateLogger();
        
        services
            .AddOptions()
            .AddHostedService<TelegramBotWorker>()
            .AddHostedService<TelegramPublishWorker>()
            
            //todo: вытащить в AddTelegramApplication, использовать не конфиг, а ENVIRONMENT
            .AddSingleton<ITelegramBotClient>(_ =>
                new TelegramBotClient(context.Configuration.GetRequiredSection("BotSettings").Get<BotSettings>().Token))
            .AddTelegramApplication();
    })
    .UseSerilog()
    .RunConsoleAsync();