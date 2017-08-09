module API

open Suave  
open Suave.Filters  
open Suave.Operators  
open Suave.Successful
open System.Text
open Newtonsoft.Json
open CommandTypes
open System
open Utils
open Common
open CommandHandler
open Persistence
open PersistenceTypes
open DtoTypes
open QueryTypes
open QueryHandler
let connection:DbConnection = "User ID=postgres;Password=postgres;Host=127.0.0.1;Port=5432;Database=postgres;Pooling=true;"

let createTemperatureAction request =
  request
  |> getBody
  |> deserialize<RegisterTemperatureDto>

let createRegisterTemperatureCommand action = 
  {
    Id = createId
    Action = (RegisterTemperatureReading action)
    TimeStamp = currentTime
  }


let createTemperaturesQuery : TemperatureQuery  = 
  GetTemperaturesQuery ({Date = None})

let responseCommand result  = 
  match result with
    | Ok value -> value |> DtoTypes.TemperatureDto.fromDomain |> serialize  |> OK |> createJsonMimeType
    | Error list -> serializeErrors list |> RequestErrors.BAD_REQUEST |> createJsonMimeType

let responseQuery result = 
  match result with
  | Temperatures list -> list |> List.map DtoTypes.TemperatureDto.fromDomain |> serialize |>  OK |> createJsonMimeType
  | _ -> "" |> OK |> createJsonMimeType

let app =  
  let save reading =
    saveTemperatureReading connection reading

  let getAll date =
    getTemperatureReadings connection None

  let temperatureCommandHandler =  commandhandler save

  let processRegisterTemperatureRequest = createTemperatureAction >> createRegisterTemperatureCommand >> temperatureCommandHandler >> responseCommand

  let temperatureQueryhandler = queryhandler getAll

  let processGetAlleTemperaturesQueryRequest = createTemperaturesQuery |> temperatureQueryhandler |> responseQuery 

  choose
      [ GET >=> choose
          [  path "/temperatures" >=> processGetAlleTemperaturesQueryRequest ]
        POST >=> choose
          [
            path "/temperatures" >=> request(processRegisterTemperatureRequest)
          ]
      ]   
[<EntryPoint>]
let main argv =  
    startWebServer defaultConfig app
    0