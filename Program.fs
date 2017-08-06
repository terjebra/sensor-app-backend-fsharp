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
let connection:DbConnection = "User ID=postgres;Password=postgres;Host=127.0.0.1;Port=5432;Database=postgres;Pooling=true;"

let createTemperatureAction request =
  request
  |> getBody
  |> deserialize<RegisterTemperatureReading>

let createRegisterTemperatureCommand action = 
  {
    Id = System.Guid.NewGuid() 
    Action = (RegisterTemperatureReading action)
    TimeStamp = DateTime.Now
  }

let serialize ob = 
  serialize ob
  |> OK

let mimeTypeJson body = 
  createJsonMimeType body

let app =  
  let save reading =
    saveTemperatureReading connection reading

  let handler =  commandhandler save
  choose
      [ GET >=> choose
          [ path "/" >=> OK "Index"
            path "/hello" >=> OK "Hello!" ]
        POST >=> choose
          [
            path "/temperatures" >=> request(createTemperatureAction >> createRegisterTemperatureCommand >> handler >> serialize >> mimeTypeJson)
          ]
      ]   
[<EntryPoint>]
let main argv =  
    startWebServer defaultConfig app
    0