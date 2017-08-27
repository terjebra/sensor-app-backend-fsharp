module Persistence

open Dapper
open DomainTypes
open PersistenceTypes
open DbTypes
open System
open Npgsql
 
let insertStatement = "INSERT INTO public.temperatures (Id, Temperature, Registered) VALUES(@Id, @Temperature, @Registered)"
let selectStatement ="SELECT Id, Temperature, Registered FROM public.temperatures"
let selectWhereStatement (date:string) = String.Format("SELECT Id, Temperature, Registered FROM public.temperatures WHERE Registered > '{0}'", date)

let createTemperature (temperatureReading: TemperatureReading) = 
  let (ReadingId id ) = temperatureReading.Id
  let (Temperature  temperature) = temperatureReading.Temperature
  let (RegisteredTime registered) = temperatureReading.RegisteredAt

  {
    Id = id
    Temperature = decimal temperature
    Registered = registered
  }

let saveTemperatureReading (connection:DbConnection) (temperatureReading: TemperatureReading)  = 
  use con = new NpgsqlConnection(connection)

  let temperature = createTemperature temperatureReading
  
  con.Execute(insertStatement, temperature) |> ignore

  temperatureReading

let createDomain temperature = 
  {Id =  (ReadingId temperature.Id); Temperature = (Temperature (float temperature.Temperature)); RegisteredAt = (RegisteredTime temperature.Registered) }

let getTemperatureReadings (connection:DbConnection) (date: Option<String>)  = 
  use con = new NpgsqlConnection(connection)

  let query = match  date with
  | Some value -> selectWhereStatement value
  | None -> selectStatement

  con.Query<Temperature>(query) |> List.ofSeq<Temperature> |> List.map (createDomain)
  
