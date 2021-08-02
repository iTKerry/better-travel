module Providers

open System.IO
open Errors
open FsToolkit.ErrorHandling.Operator.AsyncResult
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open RestSharp
open Utils
open FsToolkit.ErrorHandling
open Models

let private directionsWithResorts =
    File.ReadAllText("directions_with_resorts.json")
    |> JsonConvert.DeserializeObject<Direction array>

let directions =
    directionsWithResorts
    |> Array.map ^fun x -> {| Id = x.Id; Name = x.Name |}
    
let resorts directionId =
    directionsWithResorts
    |> Array.tryFind ^fun x -> x.Id = directionId
    |> Option.map    ^fun x -> Ok x.Resorts
    |> Option.defaultValue (DirectionNotFound directionId |> AppError.createResult)
    
let private enrichByParams (req : HotelsRequest) (rest : RestRequest) =
    Configs.getHotelsParam req.DirectionId req.ResortIds req.SearchTerm
    |> Map.iter ^fun k v -> rest.AddParameter(k, v) |> ignore
    
let private parseResponse (response : IRestResponse) =
    try
        JObject.Parse(response.Content).["Items"].Value<JObject>().Properties()
        |> Seq.map ^fun x -> x.Value
        |> Seq.map ^fun x -> { Id = x.["Id"].Value<int>(); Name = x.["Name"].Value<string>() }
        |> Seq.toList
        |> Ok
    with
    | exn -> InvalidJsonResponse exn.Message |> AppError.createResult
        
let private getHotels (req : RestRequest) =
    Client.executeRequestAsync (Urls.getHotelsUri, req)
    >>= fun req -> Async.retn (parseResponse req)
    
let hotels (req : HotelsRequest) =
    Client.createRequestAsync Method.POST
    |> AsyncResult.tee (enrichByParams req)
    >>= getHotels
