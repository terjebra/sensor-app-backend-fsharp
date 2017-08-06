module Persistence

open Dapper
open DomainTypes
open PersistenceTypes
open DbTypes
open System
open Npgsql


let saveTemperatureReading (connection:DbConnection)(temperatureReading: TemperatureReading)  = 
  let con = new NpgsqlConnection(connection)

  let (ReadingId id ) = temperatureReading.Id
  let (Temperature  temperature) = temperatureReading.Temperature
  let (RegisteredTime registered) = temperatureReading.RegisteredAt

  let temperature: Temperature = {
    Id = id
    Temperature = temperature
    Registered = registered
  }

  con.Execute("INSERT INTO public.temperatures (Id,Temperature,Registered) VALUES(@Id,@Temperature,@Registered)", temperature)
  |> ignore




