module Persistence

open Dapper
open DomainTypes
open PersistenceTypes
open DbTypes
open System
open Npgsql

let createTemperature (temperatureReading: TemperatureReading) = 
  let (ReadingId id ) = temperatureReading.Id
  let (Temperature  temperature) = temperatureReading.Temperature
  let (RegisteredTime registered) = temperatureReading.RegisteredAt

  {
    Id = id
    Temperature = temperature
    Registered = registered
  }

let saveTemperatureReading (connection:DbConnection) (temperatureReading: TemperatureReading)  = 
  use con = new NpgsqlConnection(connection)

  let temperature = createTemperature temperatureReading
  
  con.Execute("INSERT INTO public.temperatures (Id, Temperature, Registered) VALUES(@Id, @Temperature, @Registered)", temperature)
  |> ignore
