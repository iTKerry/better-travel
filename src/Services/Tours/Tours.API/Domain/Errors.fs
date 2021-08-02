module Errors

open System.Net

type ParserError =
    | InvalidJsonResponse of string
    | InvalidJsonRequest  of string

type ClientError =
    | RequestError        of url : string * code : HttpStatusCode * errorMessage : string

type DomainError =
    | DirectionNotFound   of int

type AppError =
    | Domain of DomainError
    | Client of ClientError
    | Parser of ParserError
    static member create (e : DomainError) = e |> Domain
    static member create (e : ClientError) = e |> Client
    static member create (e : ParserError) = e |> Parser
    static member createResult (e : DomainError) = e |> Domain |> Error
    static member createResult (e : ClientError) = e |> Client |> Error
    static member createResult (e : ParserError) = e |> Parser |> Error