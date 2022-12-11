[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module ReplyContent

open Domain.User
open Funogram.Telegram
open Funogram.Telegram.Types

let private createTextContent userId msg = Req.SendMessage.Make(UserId.value userId, msg)
let private createMarkupContent userId msg replyMarkup =
    Req.SendMessage.Make(UserId.value userId, msg, parseMode = ParseMode.Markdown, replyMarkup = replyMarkup)
    
let defaultError userId = createTextContent userId "Что-то пошло не так, попробуйте еще раз" |> Error
let start userId = createTextContent userId "Добро пожаловать! Укажите, пожалуйста, свою электронную почту" |> Ok
let setEmail userId = createTextContent userId "На Вашу электронную почту был отправлен код. Укажите его" |> Ok

let setCode userId =
    let markup =
        [| [| InlineKeyboardButton.Create("Создать", callbackData = "/create_publication") |] |]
        |> InlineKeyboardMarkup.Create
        |> Markup.InlineKeyboardMarkup

    createMarkupContent userId "Отлично! Чтобы создать запись, нажмите на кнопку Создать" markup |> Ok

let createPublication userId = createTextContent userId "Укажите раздел" |> Ok
let setPart userId = createTextContent userId "Укажите тип" |> Ok
let setType userId = createTextContent userId "Укажите заголовок" |> Ok
let setTitle userId = createTextContent userId "Укажите хэштеги" |> Ok
let setHashTags userId = createTextContent userId "Укажите тело" |> Ok
let setBody userId = createTextContent userId "Укажите изображения" |> Ok

let setImages userId =
    let markup =
        [| [| InlineKeyboardButton.Create("Опубликовать", callbackData = "/publish") |] |]
        |> InlineKeyboardMarkup.Create
        |> Markup.InlineKeyboardMarkup

    createMarkupContent userId "Нажми на кнопку, чтобы опубликовать" markup |> Ok

let publish userId =
    let markup =
        [| [| InlineKeyboardButton.Create("Создать", callbackData = "/create_publication") |] |]
        |> InlineKeyboardMarkup.Create
        |> Markup.InlineKeyboardMarkup

    createMarkupContent userId "Опубликовано!\n Чтобы создать новую запись, нажмите на кнопку" markup |> Ok