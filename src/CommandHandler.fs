module CommandHandler 

open System

open CommandTypes
open DomainTypes
open CommonTypes
open PersistenceTypes
open Common
open EventPublisher
open EventTypes


let notify (eventPublisher: EventPublisher<TemperatureEvent>) (reading:TemperatureReading) =
  (toNewTemperatureEvent reading)
  |> eventPublisher.Notify
  reading
let saveTemperatureReading (eventPublisher: EventPublisher<TemperatureEvent>) (saveTemperature: SaveTemperature) (result: Result<TemperatureReading, TemperatureError list>) =  
  result
  |> Result.map saveTemperature
  |> Result.map (notify eventPublisher)
  
let commandhandler 
   (saveTemperature: SaveTemperature)
   (eventPublisher: EventPublisher<TemperatureEvent>)
   (command: TemperatureCommand) 
  : Result<TemperatureReading, TemperatureError list> =

  match command.Action with
    | RegisterTemperatureReading data ->
      saveTemperatureReading eventPublisher saveTemperature (DtoTypes.TemperatureDto.toDomain data)
      