module Tours.API.QueryHandlers.GetResortsHandler

open Microsoft.Extensions.Logging
open FsToolkit.ErrorHandling.Operator.Result
open Models

type ResortResponse =
    { Id   : int
      Name : string }

type GetResortsHandler(logger : ILogger) =
    
    let getResorts directionId =
        let mapResponse (resort : Resort) =
            { Id   = resort.Id
              Name = resort.Name }
        fun resorts -> resorts |> Array.map mapResponse
        <!> Providers.resorts directionId
    
    member this.Handle directionId =
        getResorts directionId
        |> retn