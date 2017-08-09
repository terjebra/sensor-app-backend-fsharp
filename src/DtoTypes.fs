module DtoTypes

open DomainTypes
open System
open Common
open CommonTypes

type TemperatureDto = {
  Id : Guid
  Temperature: float
  TimeStamp: string
}

type RegisterTemperatureDto = {
  Temperature: float
  TimeStamp: string
}

module TemperatureDto  =
  let fromDomain (reading: TemperatureReading) : TemperatureDto  = 
    let (ReadingId id ) = reading.Id
    let (Temperature  temperature) = reading.Temperature
    let (RegisteredTime registered) = reading.RegisteredAt

    {Id = id; Temperature = temperature; TimeStamp = registered.ToString()}

  let createTemperatureReadingRegistered timestamp =
    timestamp 
    |> parseDate 
    |> Result.map RegisteredTime
    |> Result.mapError InvalidDateError


  let createTemperature temperature =
    Ok temperature
    |>  Result.map Temperature
    |>  Result.mapError ValidationError

  let createReadingId  =
    Ok createId
    |>  Result.map ReadingId
    |>  Result.mapError ValidationError
  
  let createTemperatureModel registeredAt temperature  id : TemperatureReading =  
    {Id =  id; Temperature = temperature; RegisteredAt = registeredAt }

  let toDomain (dto: RegisterTemperatureDto ) =  
    let mapToErrorList result = result |> Result.mapError (fun err -> [err])

    let temperatureReadingRegisteredResult = dto.TimeStamp |> createTemperatureReadingRegistered |> mapToErrorList
    let temperatureResult = dto.Temperature |> createTemperature |> mapToErrorList
    let idResult = createReadingId |> mapToErrorList

    lift3 createTemperatureModel temperatureReadingRegisteredResult temperatureResult idResult



    

