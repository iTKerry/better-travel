module Providers

open System.IO
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
    |> Option.map    ^fun x -> x.Resorts
    
let private enrichWithParams dirId resIds term (req : RestRequest) =
    Configs.getHotelsParam dirId resIds term
    |> Map.iter ^fun k v -> req.AddParameter(k, v) |> ignore
    req

let private getHotels (req : RestRequest) =
    asyncResult {
        let parseResponse (response : IRestResponse) =
            JObject.Parse(response.Content).["Items"].Value<JObject>().Properties()
            |> Seq.map ^fun x -> x.Value
            |> Seq.map ^fun x -> { Id = x.["Id"].Value<int>(); Name = x.["Name"].Value<string>() }
            |> Seq.toList
        
        return! AuthorizedHttpClient.executeRequestAsync (Urls.getHotelsUri, req)
        |> AsyncResult.map parseResponse
    }
    
let hotels directionId resorts term =
    asyncResult {
        return! AuthorizedHttpClient.createRequestAsync Method.POST
        |> AsyncResult.map (enrichWithParams directionId resorts term)
        |> AsyncResult.bind getHotels
    }