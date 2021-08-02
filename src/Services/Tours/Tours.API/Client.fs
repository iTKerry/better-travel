module Client

open System
open System.Net
open Errors
open RestSharp
open FsToolkit.ErrorHandling
open Utils
open FsToolkit.ErrorHandling.Operator.AsyncResult

let private client (timeout : int) (providerUrl : string) =
    RestClient(providerUrl, Timeout = TimeSpan.FromSeconds(float timeout).Milliseconds)
    
let private defaultClient providerUrl =
    client 10 providerUrl

let executeRequestAsync (url : string, request : RestRequest) =
    
    async {
        match! (defaultClient url).ExecuteAsync(request) |> Async.AwaitTask with
        | response when response.StatusCode = HttpStatusCode.OK ->
            return response |> Ok 
        | response ->
            return RequestError(url, response.StatusCode, response.ErrorMessage) |> AppError.createResult
    }

let private getCookiesAsync (url : string) (content : Map<string, _>) =
    async {
        let request = RestRequest(url, Method.POST, AlwaysMultipartFormData = true)
        content |> Map.iter ^fun k v -> request.AddParameter(k, v) |> ignore
        
        match! executeRequestAsync (url, request) with
        | Ok response -> return response.Cookies |> Seq.toList |> Ok
        | Error error -> return Error error
    }

let private createRequest (method : Method) (cookies : RestResponseCookie list) =
    let request = RestRequest(method)
    cookies |> List.iter ^fun cookie -> request.AddCookie(cookie.Name, cookie.Value) |> ignore
    request

let createRequestAsync (method : Method) =
    createRequest method
    <!> getCookiesAsync Urls.loginUri Configs.loginCredentials
