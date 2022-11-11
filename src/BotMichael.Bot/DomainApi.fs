namespace Domain

module User =
    type UserId = private UserId of int64

    module UserId =
        let create input = UserId input
        let value (UserId id) = id

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

    let getNextState (state: DialogState) =
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
        {
          Part: string
          Type: string
          Title: string
          HashTags: string
          Body: string
          Images: string }

    let getDefault =
        { 
          Part = ""
          Type = ""
          Title = ""
          HashTags = ""
          Body = ""
          Images = "" }