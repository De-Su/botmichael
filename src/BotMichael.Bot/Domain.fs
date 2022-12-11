namespace Domain

module User =
    type UserId = UserId of int64
    module UserId =
        let create input = UserId input
        let value (UserId id) = id

module Parser =
    open User
    open Funogram.Telegram.Bot
    open Funogram.Telegram.Types

    type ParsedMessage =
        | Text of msg: string
        | CallbackQuery of callback: string
        | Photo of photos: PhotoSize []

    let private getTextMessage ctx =
        ctx.Update.Message
        |> Option.bind (fun msg -> msg.Text)

    let private getCallbackQueryMessage ctx =
        ctx.Update.CallbackQuery
        |> Option.bind (fun msg -> msg.Data)

    let private getPhotoMessage ctx =
        ctx.Update.Message
        |> Option.bind (fun msg -> msg.Photo)

    let private getUser ctx =
        ctx.Update.Message
        |> Option.bind (fun msg -> msg.From)
        |> Option.orElseWith (fun () ->
            ctx.Update.CallbackQuery
            |> Option.map (fun query -> query.From))

    let getUserId ctx =
        getUser ctx |> Option.map (fun u -> UserId u.Id)

    let parse ctx =
        match getTextMessage ctx, getCallbackQueryMessage ctx, getPhotoMessage ctx with
        | Some t, _, _ -> Some <| Text t
        | _, Some cq, _ -> Some <| CallbackQuery cq
        | _, _, Some p -> Some <| Photo p
        | _ -> None

module Dialog =
    type DialogState =
        | Start
        | WaitingEmail
        | WaitingCode
        | WaitingCreatePublication
        | WaitingPart
        | WaitingPublicationType
        | WaitingTitle
        | WaitingHashTags
        | WaitingBody
        | WaitingImage
        | WaitingPublish

    let getNextState state =
        match state with
        | Start -> WaitingEmail
        | WaitingEmail -> WaitingCode
        | WaitingCode -> WaitingCreatePublication
        | WaitingCreatePublication -> WaitingPart
        | WaitingPart -> WaitingPublicationType
        | WaitingPublicationType -> WaitingTitle
        | WaitingTitle -> WaitingHashTags
        | WaitingHashTags -> WaitingBody
        | WaitingBody -> WaitingImage
        | WaitingImage -> WaitingPublish
        | WaitingPublish -> WaitingCreatePublication

module Publication =

    type Publication =
        { Part: string
          Type: string
          Title: string
          HashTags: string
          Body: string
          Images: string }

    let getDefault =
        { Part = ""
          Type = ""
          Title = ""
          HashTags = ""
          Body = ""
          Images = "" }
