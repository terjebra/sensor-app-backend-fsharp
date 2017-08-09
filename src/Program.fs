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

let response result  = 
  match result with
    | Ok value -> value |> DtoTypes.TemperatureDto.fromDomain |> serialize  |> OK |> createJsonMimeType
    | Error list -> serializeErrors list |> RequestErrors.BAD_REQUEST |> createJsonMimeType

let app =  
  let save reading =
    saveTemperatureReading connection reading

  let temperatureCommandHandler =  commandhandler save

  let processRegisterTemperatureRequest = createTemperatureAction >> createRegisterTemperatureCommand >> temperatureCommandHandler >> response

  choose
      [ GET >=> choose
          [ path "/" >=> OK "Index"
            path "/hello" >=> OK "Hello!" ]
        POST >=> choose
          [
            path "/temperatures" >=> request(processRegisterTemperatureRequest)
          ]
      ]   
[<EntryPoint>]
let main argv =  
    startWebServer defaultConfig app
    0