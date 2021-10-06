module Tours.API.QueryHandlers.GetDirectionsHandler

open Errors
open Microsoft.Extensions.Logging

type DirectionResponse =
    { Id   : int
      Name : string }

type GetDirectionsHandler(logger : ILogger) =

    let getDirections =
        try
            Providers.directions
            |> Array.map ^fun x -> { Id = x.Id; Name = x.Name }
            |> Ok
        with
        | exn ->
            logger.LogError(exn, "GetDirectionsHandler failed.")
            DataError exn.Message |> AppError.createResult

    member this.Handle () =
        retn getDirections
    