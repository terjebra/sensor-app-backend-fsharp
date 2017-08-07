module CommandHandler 

open CommandTypes
open DomainTypes
open PersistenceTypes
open System

let saveTemperatureReading (saveTemperature: SaveTemperature) id reading =
  saveTemperature reading
  reading

let createReading id (data:RegisterTemperatureReading) =
  {
    Id = (ReadingId id)
    Temperature = (Temperature data.Temperature)
    RegisteredAt  = (RegisteredTime DateTime.Now)
  }

let commandhandler 
   (saveTemperature: SaveTemperature)
   (command: TemperatureCommand) 
  : TemperatureReading =

  match command.Action with
    | RegisterTemperatureReading data ->
      saveTemperatureReading saveTemperature command.Id  (createReading command.Id data)