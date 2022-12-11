module Handler

open Domain.User
open Domain.Parser
open Domain.Dialog
open Domain.Publication
open Funogram.Api

let sendMessage req config =
    req
    |> api config
    |> Async.RunSynchronously
    |> ignore

let errorReply userId = ReplyContent.defaultError userId

let getStartReply =
    fun userId msg ->
        match msg with
        | Text cq when cq = "/start" ->

            ReplyContent.start userId
        | _ -> errorReply userId

let getEmailReply saveCode =
    fun userId msg ->
        match msg with
        | Text t when t = "privet@poka.ru" ->

            "321" |> saveCode userId
            // todo sent code to email

            ReplyContent.setEmail userId
        | _ -> errorReply userId

let getCodeReply getCode =
    fun userId msg ->
        match msg with
        | Text t when t = getCode userId ->

            ReplyContent.setCode userId
        | _ -> errorReply userId

let getCreatePublicationReply savePub =
    fun userId msg ->
        match msg with
        | CallbackQuery cq when cq = "/create_publication" ->
            getDefault |> savePub userId

            ReplyContent.createPublication userId
        | _ -> errorReply userId

let getPartReply (getPub: UserId -> Publication) savePub =
    fun userId msg ->
        match msg with
        | Text t when t <> "" ->
            { getPub userId with Part = t } |> savePub userId

            ReplyContent.setPart userId
        | _ -> errorReply userId

let getTypeReply (getPub: UserId -> Publication) savePub =
    fun userId msg ->
        match msg with
        | Text t when t <> "" ->
            { getPub userId with Type = t } |> savePub userId

            ReplyContent.setType userId
        | _ -> errorReply userId

let getTitleReply (getPub: UserId -> Publication) savePub =
    fun userId msg ->
        match msg with
        | Text t when t <> "" ->
            { getPub userId with Title = t } |> savePub userId

            ReplyContent.setTitle userId
        | _ -> errorReply userId

let getHashTagsReply (getPub: UserId -> Publication) savePub =
    fun userId msg ->
        match msg with
        | Text t when t <> "" ->
            { getPub userId with HashTags = t }
            |> savePub userId

            ReplyContent.setHashTags userId
        | _ -> errorReply userId

let getBodyReply (getPub: UserId -> Publication) savePub =
    fun userId msg ->
        match msg with
        | Text t when t <> "" ->
            { getPub userId with Body = t } |> savePub userId

            ReplyContent.setBody userId
        | _ -> errorReply userId

let getImageReply (getPub: UserId -> Publication) savePub =
    fun userId msg ->
        match msg with
        | Photo p ->
            { getPub userId with Images = string p }
            |> savePub userId

            ReplyContent.setImages userId
        | _ -> errorReply userId

let getPublishReply getPub dropPub publish =
    fun userId msg ->
        match msg with
        | CallbackQuery cq when cq = "/publish" ->
            
            getPub userId
            |> publish
            
            dropPub userId
            ReplyContent.publish userId
        | _ -> errorReply userId


let flow state userId msg =
    let getPub = Db.getPublication
    let savePub = Db.savePublication
    let dropPub = Db.dropPublication
    let getCode = Db.getUserCode
    let saveCode = Db.saveUserCode
    let publish = fun (p: Publication) -> System.Console.WriteLine (string p)

    let getReply =
        match state with
        | Start -> getStartReply
        | WaitingEmail -> getEmailReply saveCode
        | WaitingCode -> getCodeReply getCode
        | WaitingCreatePublication -> getCreatePublicationReply savePub
        | WaitingPart -> getPartReply getPub savePub
        | WaitingPublicationType -> getTypeReply getPub savePub
        | WaitingTitle -> getTitleReply getPub savePub
        | WaitingHashTags -> getHashTagsReply getPub savePub
        | WaitingBody -> getBodyReply getPub savePub
        | WaitingImage -> getImageReply getPub savePub
        | WaitingPublish -> getPublishReply getPub dropPub publish

    getReply userId msg


let updateArrived ctx =
    match getUserId ctx, parse ctx with
    | Some userId, Some msg ->
        let state = userId |> Db.getDialogState

        match flow state userId msg with
        | Ok ok ->
            state |> getNextState |> Db.saveDialogState userId
            sendMessage ok ctx.Config
        | Error err ->
            //log
            sendMessage err ctx.Config

    | _ -> () //log