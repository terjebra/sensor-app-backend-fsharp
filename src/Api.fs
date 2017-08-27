module Api

open Suave  
open Suave.Successful
open Newtonsoft.Json
open CommandTypes
open Utils
open Common
open CommandHandler
open Persistence
open PersistenceTypes
open DtoTypes
open QueryTypes
open QueryHandler
open System
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


let createTemperaturesQuery (request:HttpRequest) : TemperatureQuery  = 
  let dateParameter =
    match request.queryParam "date" with
    | Choice1Of2 date -> Some date
    | Choice2Of2 no -> None

  GetTemperaturesQuery ({Date = dateParameter})
let responseCommand result  = 
  match result with
    | Ok value -> value |> DtoTypes.TemperatureDto.fromDomain |> serialize  |> OK |> createJsonMimeType
    | Error list -> serializeErrors list |> RequestErrors.BAD_REQUEST |> createJsonMimeType

let responseQuery result = 
  match result with
  | Temperatures list -> list |> List.map DtoTypes.TemperatureDto.fromDomain |> serialize |>  OK |> createJsonMimeType
  | _ -> "" |> OK |> createJsonMimeType

let save reading =
    saveTemperatureReading connection reading

let getAll date =
    getTemperatureReadings connection date

let temperatureCommandHandler eventPublisher =  commandhandler save eventPublisher

let temperatureQueryhandler = queryhandler getAll

let registerTemperature eventPublisher = createTemperatureAction >> createRegisterTemperatureCommand >> (temperatureCommandHandler eventPublisher)>> responseCommand 

let getTemperatures =  createTemperaturesQuery >> temperatureQueryhandler >> responseQuery

