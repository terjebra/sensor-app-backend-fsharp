module Common

open Result
open Newtonsoft.Json
open System
open CommonTypes

let deserialize<'a> string =
  JsonConvert.DeserializeObject<'a> string
 
let serialize obj =
  JsonConvert.SerializeObject obj

let serializeErrors (list: TemperatureError list) = 
  list |> 
    List.map (fun item -> 
      match item with
        | InvalidDateError error ->  {Type = "InvalidDateError"; Message = error}
        | ValidationError error ->  {Type = "ValidationError"; Message = error}
    )
  |>  JsonConvert.SerializeObject

let createId  = System.Guid.NewGuid()

let currentTime = DateTime.Now
let tryParseDate str = 
  DateTime.TryParse str
 
let parseDate str = 
  let (parsed, date) = tryParseDate str
  match parsed with
    | true -> 
        Ok date
     | false ->
       Error "Could not parse date"

let apply fResult xResult =
  match fResult,xResult with
  | Ok f, Ok x -> Ok (f x)
  | Error err1, Ok x -> Error err1
  | Ok f, Error err2 -> Error err2
  | Error err1, Error err2 -> Error (err1 @ err2)


let (<!>) = Result.map
let (<*>) = apply

let lift3 f x y z =
  (((f <!> x ) <*> y) <*> z )