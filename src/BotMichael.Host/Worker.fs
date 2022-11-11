namespace BotMichael.Host

open System
open System.Threading
open System.Threading.Tasks
open Funogram.Telegram.Bot
open Funogram.Api
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

type Worker(logger: ILogger<Worker>) =
    inherit BackgroundService()
    
    override _.ExecuteAsync(ct: CancellationToken) =
        async {
            let config = {Config.defaultConfig with Token = Environment.GetEnvironmentVariable("BOT_TOKEN")}
            let! _ = Funogram.Telegram.Api.deleteWebhookBase () |> api config
            let! me = Funogram.Telegram.Api.getMe |> api config
            
            match me with
            | Ok ok -> logger.LogInformation("Starting bot @{BotName}", ok.Username.Value)
            | _ -> logger.LogError("Bot not started!")
            
            return! startBot config Handler.updateArrived None
        }
        |> Async.StartAsTask
        :> Task