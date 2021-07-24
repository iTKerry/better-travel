module Providers

open System.IO
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open RestSharp
open Utils
open FsToolkit.ErrorHandling

type Resort =
    { Id   : int
      Name : string }

type Direction =
    { Id      : int
      Name    : string
      Resorts : Resort [] }

let private directionsWithResorts =
    let json = File.ReadAllText("directions_with_resorts.json")
    JsonConvert.DeserializeObject<Direction array>(json)

let directions =
    directionsWithResorts
    |> Array.map (fun x -> {| Id = x.Id; Name = x.Name |})
    
let resorts directionId =
    directionsWithResorts
    |> Array.tryFind (fun x -> x.Id = directionId)
    |> Option.map    (fun x -> x.Resorts)
    
let private enrichWithParams dirId resIds term (req : RestRequest) =
    Configs.getHotelsParam dirId resIds term
    |> Map.iter (fun k v -> req.AddParameter(k, v) |> ignore)
    req

let private getHotels (req : RestRequest) =
    async {
        let parseResponse (response : IRestResponse) =
            JObject.Parse(response.Content).["Items"].Value<JObject>().Properties()
            |> Seq.map (fun x -> x.Value)
            |> Seq.map (fun x -> { Id = x.["Id"].Value<int>(); Name = x.["Name"].Value<string>() })
            |> Seq.toList
        
        let! response = AuthorizedHttpClient.executeRequestAsync (Urls.getHotelsUri, req)
        return response |> Result.map parseResponse
    }
    
let hotels directionId resorts term =
    asyncResult {
        let! request = AuthorizedHttpClient.createRequestAsync Method.POST
        let request = enrichWithParams directionId resorts term request
        return! getHotels request
    }