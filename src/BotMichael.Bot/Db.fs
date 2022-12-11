module Db

open System.Collections.Concurrent
open Domain
open Domain.Dialog
open Domain.User

[<AbstractClass; Sealed>]
type private Cache() =
    static member val DialogStates = ConcurrentDictionary<UserId, DialogState>()
    static member val UserCodes = ConcurrentDictionary<UserId, string>()
    static member val Publications = ConcurrentDictionary<UserId, Publication.Publication>()

let getDialogState userId =
    match Cache.DialogStates.TryGetValue userId with
    | true, value -> value
    | _ -> DialogState.Start

let saveDialogState userId state =
    match Cache.DialogStates.TryGetValue userId with
    | true, _ -> Cache.DialogStates[ userId ] <- state
    | _ -> Cache.DialogStates.TryAdd(userId, state) |> ignore

let getUserCode userId =
    match Cache.UserCodes.TryGetValue userId with
    | true, value -> value
    | _ -> "123"

let saveUserCode userId code =
    match Cache.UserCodes.TryGetValue userId with
    | true, _ -> Cache.UserCodes[ userId ] <- code
    | _ -> Cache.UserCodes.TryAdd(userId, code) |> ignore

let dropUserCode userId =
    match Cache.UserCodes.TryGetValue userId with
    | true, _ -> Cache.UserCodes.TryRemove userId |> ignore
    | _ -> ()

let getPublication userId =
    match Cache.Publications.TryGetValue userId with
    | true, value -> value
    | _ -> Publication.getDefault

let savePublication userId p =
    match Cache.Publications.TryGetValue userId with
    | true, _ -> Cache.Publications[ userId ] <- p
    | _ -> Cache.Publications.TryAdd(userId, p) |> ignore

let dropPublication userId =
    match Cache.Publications.TryGetValue userId with
    | true, _ -> Cache.Publications.TryRemove userId |> ignore
    | _ -> ()
