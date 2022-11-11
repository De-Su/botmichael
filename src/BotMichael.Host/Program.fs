namespace BotMichael.Host

open System
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Configuration
open Serilog

module Program =
    [<EntryPoint>]
    let main args =
        HostBuilder()
            .ConfigureAppConfiguration(fun context config ->
                let env =
                    Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")

                config
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{env}.json", true, true)
                    .AddEnvironmentVariables()
                |> ignore)
            .ConfigureServices(fun context services ->
                Log.Logger <-
                    LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .CreateLogger()

                services
                    .AddOptions()
                    .AddHostedService<Worker>()
                |> ignore)
            .UseSerilog()
            .RunConsoleAsync()
            .GetAwaiter()
            .GetResult()

        0
