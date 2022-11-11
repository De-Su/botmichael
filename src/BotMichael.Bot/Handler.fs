module Handler

open Domain
open Domain.User
open Domain.Dialog
open Funogram.Telegram
open Funogram.Telegram.Bot
open Funogram.Telegram.Types
open Funogram.Api

let sendText userId msg config =
    Req.SendMessage.Make(UserId.value userId, msg)
    |> api config
    |> Async.RunSynchronously
    |> ignore

let sendMarkup userId msg replyMarkup config =
    Req.SendMessage.Make(UserId.value userId, msg, parseMode = ParseMode.Markdown, replyMarkup = replyMarkup)
    |> api config
    |> Async.RunSynchronously
    |> ignore

let handleStart state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s = "/start" ->
            state |> getNextState |> Db.saveDialogState userId

            "Добро пожаловать! Укажите, пожалуйста, свою электронную почту"
        | _ -> "Ожидалась команда /start"

    sendText userId text

let handleEmail state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s = "privet@poka.ru" ->
            state |> getNextState |> Db.saveDialogState userId

            //todo: generate code and send code to email
            Db.saveUserCode userId "321"

            "На Вашу электронную почту был отправлен код. Укажите его"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handleCode state (userId: UserId, msg: string option) =
    match msg with
    | Some s when s = Db.getUserCode userId ->
        state |> getNextState |> Db.saveDialogState userId

        Db.dropUserCode userId

        let markup =
            [| [| InlineKeyboardButton.Create("Создать", callbackData = "/create_publication") |] |]
            |> InlineKeyboardMarkup.Create
            |> Markup.InlineKeyboardMarkup

        sendMarkup userId "Отлично! Чтобы создать запись, нажмите на кнопку" markup
    | _ -> sendText userId "Код неверный, попробуйте еще раз"

let handleCreatePublication state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s = "/create_publication" ->

            Publication.getDefault
            |> Db.savePublication userId

            state |> getNextState |> Db.saveDialogState userId

            "Укажите раздел"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handlePart state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s <> "" ->

            { Db.getPublication userId with
                  Part = s }
            |> Db.savePublication userId

            state |> getNextState |> Db.saveDialogState userId

            "Укажите тип"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handleType state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s <> "" ->

            { Db.getPublication userId with
                  Type = s }
            |> Db.savePublication userId

            state |> getNextState |> Db.saveDialogState userId

            "Укажите заголовок"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handleTitle state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s <> "" ->

            { Db.getPublication userId with
                  Title = s }
            |> Db.savePublication userId

            state |> getNextState |> Db.saveDialogState userId

            "Укажите хэштеги"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handleHashTags state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s <> "" ->

            { Db.getPublication userId with
                  HashTags = s }
            |> Db.savePublication userId

            state |> getNextState |> Db.saveDialogState userId

            "Укажите тело"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handleBody state (userId: UserId, msg: string option) =
    let text =
        match msg with
        | Some s when s <> "" ->

            { Db.getPublication userId with
                  Body = s }
            |> Db.savePublication userId

            state |> getNextState |> Db.saveDialogState userId

            "Укажите изображения"
        | _ -> "Что-то пошло не так"

    sendText userId text

let handleImage state (userId: UserId, msg: string option) =
    match msg with
    | Some s when s <> "" ->

        { Db.getPublication userId with
              Images = s }
        |> Db.savePublication userId

        state |> getNextState |> Db.saveDialogState userId

        let markup =
            [| [| InlineKeyboardButton.Create("Опубликовать", callbackData = "/publish") |] |]
            |> InlineKeyboardMarkup.Create
            |> Markup.InlineKeyboardMarkup

        sendMarkup userId "Нажми на кнопку, чтобы опубликовать" markup
    | _ -> sendText userId "Что-то пошло не так"

let handlePublish state (userId: UserId, msg: string option) =
    match msg with
    | Some s when s = "/publish" ->

        let p = string <| Db.getPublication userId
        Db.dropPublication userId
        state |> getNextState |> Db.saveDialogState userId

        let markup =
            [| [| InlineKeyboardButton.Create("Создать", callbackData = "/create_publication") |] |]
            |> InlineKeyboardMarkup.Create
            |> Markup.InlineKeyboardMarkup

        sendMarkup userId $"Опубликовано!\n {p} \n  Чтобы создать новую запись, нажмите на кнопку" markup
    | _ -> sendText userId "Что-то пошло не так"

let updateArrived (ctx: UpdateContext) =
    let (userId, msg) =
        match (ctx.Update.Message, ctx.Update.CallbackQuery) with
        | (Some m, _) -> UserId.create m.From.Value.Id, m.Text
        | (_, Some c) -> UserId.create c.From.Id, c.Data
        | _ -> UserId.create 0, None

    let dialogState = Db.getDialogState userId

    let handle =
        match dialogState with
        | Start -> handleStart
        | WaitingEmail -> handleEmail
        | WaitingCode -> handleCode
        | WaitingCreatePublication -> handleCreatePublication
        | WaitingPart -> handlePart
        | WaitingPublicationType -> handleType
        | WaitingTitle -> handleTitle
        | WaitingHashTags -> handleHashTags
        | WaitingBody -> handleBody
        | WaitingImage -> handleImage
        | WaitingPublish -> handlePublish

    handle dialogState (userId, msg) ctx.Config
